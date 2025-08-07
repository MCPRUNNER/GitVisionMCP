namespace GitVisionMCP.Services;

public interface IUtilityService
{
    string? GetAppVersion(string? projectFile);
    object? GetEnvironmentVariableValue(string variableName);
}