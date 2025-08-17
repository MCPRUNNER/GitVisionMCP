namespace GitVisionMCP.Services;

public interface IUtilityService
{
    string? GetAppVersion(string? projectFile);
    object? GetEnvironmentVariableValue(string variableName);
    Task<(bool Success, string StdOut, string StdErr, int ExitCode)> RunProcessAsync(string workingDirectory, string fileName, string arguments, int timeoutMs = 60000);
}