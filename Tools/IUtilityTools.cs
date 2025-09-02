using ModelContextProtocol.Server;
using System.ComponentModel;

namespace GitVisionMCP.Tools;

/// <summary>
/// Interface for utility tools implementation using ModelContextProtocol attributes
/// </summary>
public interface IUtilityTools
{
    Task<Dictionary<string, object>> RunPluginAsync(
        [Description("Name of plugin")] string? pluginName);

    /// <summary>
    /// Runs an external process and captures the output
    /// </summary>
    /// <param name="workingDirectory">The working directory for the process</param>
    /// <param name="fileName">The name or path of the process to run</param>
    /// <param name="arguments">The command line arguments to pass to the process</param>
    /// <param name="timeoutMs">The timeout in milliseconds (default: 60000)</param>
    /// <param name="environmentVariables">Optional dictionary of environment variables to set</param>
    /// <returns>A dictionary containing the process output, including success flag, stdout, stderr, and exit code</returns>
    Task<Dictionary<string, object>> RunProcessAsync(
        string? workingDirectory,
        string fileName,
        string? arguments,
        int? timeoutMs = 60000,
        Dictionary<string, string>? environmentVariables = null);

    /// <summary>
    /// Gets the value of an environment variable
    /// </summary>
    /// <param name="variableName">The name of the environment variable</param>
    /// <returns>The value of the environment variable, or null if not set</returns>
    Task<string?> GetEnvironmentVariableAsync(string variableName);

    /// <summary>
    /// Sets an environment variable for the current process
    /// </summary>
    /// <param name="name">The name of the environment variable</param>
    /// <param name="value">The value to set</param>
    /// <returns>True if the operation was successful</returns>
    Task<bool> SetEnvironmentVariableAsync(string name, string value);
}
