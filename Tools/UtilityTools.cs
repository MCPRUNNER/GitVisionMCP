using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using GitVisionMCP.Services;
using GitVisionMCP.Models;
using GitVisionMCP.Repositories;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace GitVisionMCP.Tools;

/// <summary>
/// Utility tools implementation using ModelContextProtocol attributes
/// </summary>
[McpServerToolType]
public class UtilityTools : IUtilityTools
{
    private readonly IUtilityRepository _utilityRepository;
    private readonly IFileService _fileService;
    private readonly IWorkspaceService _workspaceService;
    private readonly ILogger<UtilityTools> _logger;

    /// <summary>
    /// Creates a new instance of the UtilityTools class
    /// </summary>
    /// <param name="utilityRepository">The utility repository implementation</param>
    /// <param name="logger">The logger instance</param>
    public UtilityTools(IUtilityRepository utilityRepository, ILogger<UtilityTools> logger, IFileService fileService, IWorkspaceService workspaceService)
    {
        _utilityRepository = utilityRepository ?? throw new ArgumentNullException(nameof(utilityRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _workspaceService = workspaceService ?? throw new ArgumentNullException(nameof(workspaceService));
    }
    [McpServerToolAttribute(Name = "gv_run_plugin")]
    [Description("Run a plugin and capture its output")]
    public async Task<Dictionary<string, object>> RunPluginAsync(
        [Description("Name of plugin")] string? pluginName)
    {
        if (string.IsNullOrWhiteSpace(pluginName))
        {
            _logger.LogError("Plugin name cannot be null or empty");
            return new Dictionary<string, object>
                {
                    { "success", false },
                    { "error", "Plugin name was not provided" }
                };
        }
        var pluginConfig = _workspaceService.SearchJsonFile(".gitvision/config.json", @$"$.Plugins[?(@.Name == '{pluginName}'  && @.Enabled == true)]");
        if (pluginConfig == null || !pluginConfig.Any())
        {
            _logger.LogError("Plugin '{PluginName}' not found", pluginName);
            return new Dictionary<string, object>
            {
                { "success", false },
                { "error", "Plugin not found" }
            };
        }
        Plugin? plugin = null;
        try
        {
            var token = JToken.Parse(pluginConfig);
            JObject? pluginObj = null;
            if (token.Type == JTokenType.Array)
            {
                var arr = (JArray)token;
                if (arr.Count > 0 && arr[0].Type == JTokenType.Object)
                {
                    pluginObj = (JObject)arr[0];
                }
            }
            else if (token.Type == JTokenType.Object)
            {
                pluginObj = (JObject)token;
            }

            if (pluginObj != null)
            {
                plugin = pluginObj.ToObject<Plugin>();
            }
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Failed to parse plugin configuration JSON");
        }
        if (plugin == null)
        {
            _logger.LogError("Failed to parse plugin configuration");
            return new Dictionary<string, object>
            {
                { "success", false },
                { "error", "Failed to parse plugin configuration" }
            };
        }
        var result = await _utilityRepository.RunPluginAsync(plugin);
        return new Dictionary<string, object>
            {
                { "success", result.Success },
                { "output", result.StdOut },
                { "error", result.StdErr }
            };
    }

    /// <summary>
    /// Runs an external process and captures the output
    /// </summary>
    /// <param name="workingDirectory">The working directory for the process</param>
    /// <param name="fileName">The name or path of the process to run</param>
    /// <param name="arguments">The command line arguments to pass to the process</param>
    /// <param name="timeoutMs">The timeout in milliseconds (default: 60000)</param>
    /// <param name="environmentVariables">Optional dictionary of environment variables to set</param>
    /// <returns>A dictionary containing the process output, including success flag, stdout, stderr, and exit code</returns>
    [McpServerToolAttribute(Name = "gv_run_process")]
    [Description("Run an external process and capture its output")]
    public async Task<Dictionary<string, object>> RunProcessAsync(
    [Description("The working directory for the process")] string? workingDirectory,
    [Description("The name or path of the process to run")] string fileName,
    [Description("The command line arguments to pass to the process")] string? arguments = null,
    [Description("The timeout in milliseconds (default: 60000)")] int? timeoutMs = 60000,
    [Description("Optional dictionary of environment variables to set")] Dictionary<string, string>? environmentVariables = null)
    {
        try
        {
            _logger.LogInformation("Running process {FileName} with arguments {Arguments} in {WorkingDirectory}",
                fileName, arguments, workingDirectory);
            if (workingDirectory == null)
            {
                var fullFilename = _fileService.GetFullPath(fileName);
                workingDirectory = System.IO.Path.GetDirectoryName(fullFilename);
            }
            var stdout = string.Empty;
            var stderr = string.Empty;
            var exitCode = -1;
            var success = false;

            if (environmentVariables != null)
            {
                var result = await _utilityRepository.ExecutePluginProgramAsync(
                    fileName,
                    arguments,
                    workingDirectory,
                    timeoutMs ?? 60000,
                    environmentVariables);

                success = result.Success;
                stdout = result.Output;
                stderr = result.Error;
                exitCode = result.ExitCode;
            }
            else
            {
                var result = await _utilityRepository.RunProcessAsync(
                    workingDirectory,
                    fileName,
                    arguments,
                    timeoutMs ?? 60000);

                success = result.Success;
                stdout = result.StdOut;
                stderr = result.StdErr;
                exitCode = result.ExitCode;
            }

            var response = new Dictionary<string, object>
            {
                ["success"] = success,
                ["stdout"] = stdout,
                ["stderr"] = stderr,
                ["exitCode"] = exitCode
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error running process {FileName} with arguments {Arguments}", fileName, arguments);

            return new Dictionary<string, object>
            {
                ["success"] = false,
                ["stdout"] = string.Empty,
                ["stderr"] = $"Error running process: {ex.Message}",
                ["exitCode"] = -1
            };
        }
    }

    /// <summary>
    /// Gets the value of an environment variable
    /// </summary>
    /// <param name="variableName">The name of the environment variable</param>
    /// <returns>The value of the environment variable, or null if not set</returns>
    [McpServerToolAttribute(Name = "gv_get_environment_variable")]
    [Description("Get the value of an environment variable")]
    public Task<string?> GetEnvironmentVariableAsync(
        [Description("The name of the environment variable")] string variableName)
    {
        try
        {
            _logger.LogInformation("Getting environment variable {VariableName}", variableName);

            var value = _utilityRepository.GetEnvironmentVariableValue(variableName);
            return Task.FromResult(value?.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting environment variable {VariableName}", variableName);
            throw new InvalidOperationException($"Error getting environment variable: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets an environment variable for the current process
    /// </summary>
    /// <param name="name">The name of the environment variable</param>
    /// <param name="value">The value to set</param>
    /// <returns>True if the operation was successful</returns>
    [McpServerToolAttribute(Name = "gv_set_environment_variable")]
    [Description("Set an environment variable for the current process")]
    public Task<bool> SetEnvironmentVariableAsync(
        [Description("The name of the environment variable")] string name,
        [Description("The value to set")] string value)
    {
        try
        {
            _logger.LogInformation("Setting environment variable {Name}={Value}", name, value);

            var result = _utilityRepository.SetEnvironmentVariable(name, value);
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting environment variable {Name}={Value}", name, value);
            throw new InvalidOperationException($"Error setting environment variable: {ex.Message}", ex);
        }
    }
}
