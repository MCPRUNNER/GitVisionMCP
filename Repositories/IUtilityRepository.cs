namespace GitVisionMCP.Repositories;

public interface IUtilityRepository
{
    string? GetAppVersion(string? projectFile);
    object? GetEnvironmentVariableValue(string variableName);
}