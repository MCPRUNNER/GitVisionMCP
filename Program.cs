using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using ModelContextProtocol.AspNetCore;
using GitVisionMCP.Services;
using GitVisionMCP.Prompts;
using GitVisionMCP.Tools;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using GitVisionMCP.Handlers;

// Ensure UTF-8 encoding for stdout
Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
var logDirectory = Environment.GetEnvironmentVariable("GIT_APP_LOG_DIRECTORY") ??
    Path.Combine(Directory.GetCurrentDirectory(), "logs");
Directory.CreateDirectory(logDirectory);
var logFilePattern = Path.Combine(logDirectory, "gitvisionmcp-.log");


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()

    .WriteTo.File(logFilePattern,
        shared: true,
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 10 * 1024 * 1024, // 10MB
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

// Add Serilog to logging providers
builder.Logging.ClearProviders(); // Clear default providers including console

builder.Logging.AddSerilog(Log.Logger);

// Determine transport type from environment variable
var transportType = Environment.GetEnvironmentVariable("GITVISION_MCP_TRANSPORT") ?? "unset";
Log.Information($"Getting transport type: {transportType.ToLowerInvariant()}");
switch (transportType.ToLowerInvariant())
{
    case "http":
        Log.Information($"Matched http transport type: {transportType}");
        break;
    case "stdio":
        Log.Information($"Matched stdio transport type: {transportType}");
        break;
    default:
        Log.Warning($"Unknown transport type '{transportType}' specified. Defaulting to Stdio transport.");
        transportType = "Stdio";
        break;
}
Log.Information($"Using transport type: {transportType}");
// Add MCP services based on transport type
if (transportType.Equals("Http", StringComparison.OrdinalIgnoreCase))
{
    Log.Information("Using HTTP transport for GitVision MCP server.");
    builder.Services.AddMcpServer().WithHttpTransport()
        .WithTools<GitServiceTools>()
        .WithPrompts<ReleaseDocumentPrompts>();
}
else if (transportType.Equals("Stdio", StringComparison.OrdinalIgnoreCase))
{
    Log.Information("Using Stdio transport for GitVision MCP server.");
    builder.Services.AddMcpServer().WithStdioServerTransport()
        .WithTools<GitServiceTools>()
        .WithPrompts<ReleaseDocumentPrompts>();
}
else
{
    Log.Error($"Invalid GITVISION_MCP_TRANSPORT: {transportType}. Defaulting to Stdio transport.");
    builder.Services.AddMcpServer().WithStdioServerTransport()
        .WithTools<GitServiceTools>()
        .WithPrompts<ReleaseDocumentPrompts>();
}

// Add our GitVision MCP services
builder.Services.AddSingleton<ILocationService, LocationService>();
builder.Services.AddSingleton<IGitService, GitService>();
builder.Services.AddSingleton<IDeconstructionService, DeconstructionService>();

builder.Services.AddTransient<IGitServiceTools, GitServiceTools>();
builder.Services.AddSingleton<IMcpHandler, McpHandler>();


// Configure JSON options
if (transportType.Equals("Http", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.WriteIndented = true;
        });

    // Configure HTTP server options
    builder.Services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
    {
        options.AllowSynchronousIO = true;
        options.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
        options.Limits.MaxConcurrentConnections = 100;
        options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
    });
}
var app = builder.Build();

// Configure HTTP middleware if using HTTP transport
if (!transportType.Equals("Stdio", StringComparison.OrdinalIgnoreCase))
{
    app.UseRouting();

    // Add endpoint for testing
    app.MapGet("/api/test", () => "GitVision MCP Server is running!");

    // Configure exception handling
    app.UseExceptionHandler(appError =>
    {
        appError.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var exceptionHandlerFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
            var error = exceptionHandlerFeature?.Error;

            if (error is OperationCanceledException)
            {
                Log.Debug("Client disconnected - operation canceled");
                context.Response.StatusCode = 499; // Client Closed Request
                await context.Response.WriteAsync("{\"error\": \"Client disconnected\"}");
                return;
            }

            // Generic error handler
            Log.Error(error, "Unhandled exception");
            await context.Response.WriteAsync("{\"error\": \"An unexpected error occurred. Please try again later.\"}");
        });
    });
}

// Run the application
app.Lifetime.ApplicationStarted.Register(() => Log.Information("GitVision MCP Server application started"));
app.Lifetime.ApplicationStopping.Register(() => Log.Information("GitVision MCP Server application stopping"));
app.Lifetime.ApplicationStopped.Register(() => Log.Information("GitVision MCP Server application stopped"));

if (transportType.Equals("Stdio", StringComparison.OrdinalIgnoreCase))
{
    Log.Information("Configuring to run with Stdio transport.");
    // In stdio mode, we only want to use the MCP server transport
    try
    {
        Log.Information("Attempting to retrieve IMcpServer service.");
        var mcpServer = app.Services.GetRequiredService<ModelContextProtocol.Server.IMcpServer>();
        Log.Information("Successfully retrieved IMcpServer service.");

        // Register shutdown handlers
        var cancellationTokenSource = new CancellationTokenSource();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            Log.Information("Ctrl+C received, stopping server...");
            cancellationTokenSource.Cancel();
        };

        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            Log.Information("Process exit event received, stopping server...");
            cancellationTokenSource.Cancel();
        };

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            PosixSignalRegistration.Create(PosixSignal.SIGTERM, _ =>
            {
                Log.Information("SIGTERM received, stopping server...");
                cancellationTokenSource.Cancel();
            });
        }

        Log.Information("Starting MCP server run loop.");
        await mcpServer.RunAsync(cancellationTokenSource.Token);
        Log.Information("MCP server run loop finished.");
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "MCP Server terminated unexpectedly");
    }
    finally
    {
        Log.Information("MCP Server shutting down");
        Log.CloseAndFlush();
    }
}
else
{
    // For HTTP mode, start the web server
    app.MapMcp("/mcp");
    // JSON-RPC mapping will be handled by the MCP server when app.MapMcp is called
    Log.Information("Starting GitVision MCP server with HTTP transport");
    app.Run();
}


