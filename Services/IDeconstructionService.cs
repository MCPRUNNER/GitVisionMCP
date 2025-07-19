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

    /// <summary>
    /// Analyzes a C# ASP.NET Core controller file and saves the structure to a JSON file in the workspace directory
    /// </summary>
    /// <param name="filePath">The path to the controller file relative to workspace root</param>
    /// <param name="outputFileName">The name of the output JSON file (optional, defaults to controller name + '_analysis.json')</param>
    /// <returns>The full path to the saved JSON file, or null if the operation failed</returns>
    string? AnalyzeControllerToFile(string filePath, string? outputFileName = null);
}
