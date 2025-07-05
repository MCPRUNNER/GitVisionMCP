using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using GitVisionMCP.Services;
using System.Text;
using System.Runtime.InteropServices;
using Serilog;
using Serilog.Extensions.Logging;

// Ensure UTF-8 encoding for stdout
Console.OutputEncoding = Encoding.UTF8;

// Determine transport type (similar to mssqlMCP pattern)
var transportType = Environment.GetEnvironmentVariable("GITVISION_MCP_TRANSPORT") ?? "Stdio";

// Add simple console logging for transport selection
if (transportType.Equals("Stdio", StringComparison.OrdinalIgnoreCase))
{
    Console.Error.WriteLine("Using Stdio transport for GitVision MCP server.");
}
else
{
    Console.Error.WriteLine("Using HTTP transport for GitVision MCP server.");
}

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddLogging(builder =>
        {
            builder.ClearProviders();

            // Determine log path - use absolute path for Docker, relative for local
            var logDirectory = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Production"
                ? "/app/logs"  // Absolute path for Docker
                : Path.Combine(Directory.GetCurrentDirectory(), "logs");  // Relative path for local

            Directory.CreateDirectory(logDirectory);

            // Create timestamped log file pattern
            var logFilePattern = Path.Combine(logDirectory, "gitvisionmcp-.log");

            // Configure Serilog with timestamped file names and log rotation
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(logFilePattern,
                    shared: true,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 10 * 1024 * 1024, // 10MB
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            builder.AddSerilog(Log.Logger);
        });

        services.AddSingleton<IGitService, GitService>();

        // Register MCP server components following mssqlMCP pattern
        services.AddSingleton<IMcpServer, McpServer>();
    })
    .UseConsoleLifetime(options =>
    {
        options.SuppressStatusMessages = true;
    })
    .Build();

// Run the application based on transport type
if (transportType.Equals("Stdio", StringComparison.OrdinalIgnoreCase))
{
    // In stdio mode, use the MCP server directly for JSON-RPC over stdio
    try
    {
        var mcpServer = host.Services.GetRequiredService<IMcpServer>();

        // Create master cancellation token source for all shutdown scenarios
        var cancellationTokenSource = new CancellationTokenSource();
        var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

        // Combine all cancellation tokens
        var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationTokenSource.Token,
            lifetime.ApplicationStopping);

        // Handle Ctrl+C (SIGINT)
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            Console.Error.WriteLine("Ctrl+C (SIGINT) received, stopping MCP server...");
            cancellationTokenSource.Cancel();
        };

        // Handle SIGTERM (Docker container termination)
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            Console.Error.WriteLine("SIGTERM received, stopping MCP server...");
            cancellationTokenSource.Cancel();
        };

        // Handle application shutdown events from the host
        lifetime.ApplicationStopping.Register(() =>
        {
            Console.Error.WriteLine("Application stopping event received...");
            cancellationTokenSource.Cancel();
        });

        // For Unix systems, handle SIGTERM explicitly
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // Register for SIGTERM
            PosixSignalRegistration.Create(PosixSignal.SIGTERM, context =>
            {
                Console.Error.WriteLine("SIGTERM signal received, stopping MCP server...");
                cancellationTokenSource.Cancel();
            });
        }

        // Start the MCP server and wait for cancellation
        await mcpServer.StartAsync(combinedToken.Token);

        // If we reach here, the server stopped gracefully
        Console.Error.WriteLine("MCP server stopped gracefully.");
        Log.CloseAndFlush(); // Ensure logs are flushed before shutdown
        return 0;
    }
    catch (OperationCanceledException)
    {
        // Expected when cancellation is requested
        Console.Error.WriteLine("MCP server cancelled successfully.");
        Log.CloseAndFlush(); // Ensure logs are flushed before shutdown
        return 0;
    }
    catch (Exception ex)
    {
        // Log to stderr
        await Console.Error.WriteLineAsync($"Error: {ex.Message}");
        Log.CloseAndFlush(); // Ensure logs are flushed before shutdown
        return 1;
    }
}
else
{
    // For HTTP mode (future extension), run the host
    try
    {
        // Create cancellation token that responds to signals
        var cancellationTokenSource = new CancellationTokenSource();

        // Handle Ctrl+C (SIGINT)
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            Console.Error.WriteLine("Ctrl+C (SIGINT) received, stopping HTTP server...");
            cancellationTokenSource.Cancel();
        };

        // Handle SIGTERM (Docker container termination)
        AppDomain.CurrentDomain.ProcessExit += (_, _) =>
        {
            Console.Error.WriteLine("SIGTERM received, stopping HTTP server...");
            cancellationTokenSource.Cancel();
        };

        // For Unix systems, handle SIGTERM explicitly
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // Register for SIGTERM
            PosixSignalRegistration.Create(PosixSignal.SIGTERM, context =>
            {
                Console.Error.WriteLine("SIGTERM signal received, stopping HTTP server...");
                cancellationTokenSource.Cancel();
            });
        }

        await host.RunAsync(cancellationTokenSource.Token);
        Console.Error.WriteLine("HTTP server stopped gracefully.");
        Log.CloseAndFlush(); // Ensure logs are flushed before shutdown
        return 0;
    }
    catch (OperationCanceledException)
    {
        // Expected when cancellation is requested
        Console.Error.WriteLine("HTTP server cancelled successfully.");
        Log.CloseAndFlush(); // Ensure logs are flushed before shutdown
        return 0;
    }
    catch (Exception ex)
    {
        // Log to stderr
        await Console.Error.WriteLineAsync($"Error: {ex.Message}");
        Log.CloseAndFlush(); // Ensure logs are flushed before shutdown
        return 1;
    }
}
