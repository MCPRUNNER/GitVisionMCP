namespace GitVisionMCP.Services;

/// <summary>
/// Interface for deconstructing ASP.NET Core controller files
/// </summary>
public interface IDeconstructionService
{
    /// <summary>
    /// Analyzes a C# ASP.NET Core controller file and returns its structure as JSON
    /// </summary>
    /// <param name="filePath">The path to the controller file relative to workspace root</param>
    /// <returns>JSON string representation of the controller structure</returns>
    string? AnalyzeController(string filePath);
}
