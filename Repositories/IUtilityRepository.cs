using GitVisionMCP.Models;
namespace GitVisionMCP.Repositories;

public interface IUtilityRepository
{
    string? GetAppVersion(string? projectFile);
    object? GetEnvironmentVariableValue(string variableName);
    bool SetEnvironmentVariable(string name, string value, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process);
    Dictionary<string, string> CreateEnvironmentVariables(Dictionary<string, string>? additionalVariables = null, bool overwriteExisting = true, bool includeCurrentEnvironment = true);
    Task<(bool Success, string StdOut, string StdErr, int ExitCode)> RunProcessAsync(string? workingDirectory, string fileName, string? arguments, int timeoutMs = 60000);
    Task<(bool Success, string Output, string Error, int ExitCode)> ExecutePluginProgramAsync(string programPath, string? parameters, string? workingDirectory = null, int timeoutMilliseconds = 60000, Dictionary<string, string>? environmentVariables = null);
    Task<(bool Success, string StdOut, string StdErr, int ExitCode)> RunPluginAsync(Plugin plugin);
}