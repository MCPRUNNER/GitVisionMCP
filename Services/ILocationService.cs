using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace GitVisionMCP.Services;

public interface ILocationService
{
    /// <summary>
    /// Gets the workspace root directory, checking for environment variable first
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    string GetWorkspaceRoot();
    public string? ReadFile(string filePath);
    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types
    /// </summary>
    /// <returns>A list of file information including relative path and file type</returns>
    List<WorkspaceFileInfo> GetAllFiles();


    /// <summary>
    /// Searches a JSON file for values using JSONPath query with support for wildcards and recursive descent.
    /// </summary>
    /// <param name="jsonFilePath">The path to the JSON file to search</param>
    /// <param name="jsonPath">The JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey', '$..email')</param>
    /// <param name="indented">Whether to format the output as indented JSON</param>
    /// <param name="showKeyPaths">Whether to return structured results with path, value, and key information</param>
    /// <returns>A string representation of the search results, or an empty string if no matches are found</returns>
    string? SearchJsonFile(string jsonFilePath, string jsonPath, bool indented = true, bool showKeyPaths = false);

    /// <summary>
    ///  Gets all files under the workspace root directory with relative paths and file types that match a specific search pattern.
    /// </summary>
    /// <param name="searchPattern"></param>
    /// <returns></returns>
    List<WorkspaceFileInfo> GetAllFilesMatching(string searchPattern);

    /// <summary>
    /// Saves the output of GetAllFiles() to an XML file
    /// </summary>
    /// <param name="xmlFilePath">The path where the XML file will be saved</param>
    /// <returns>True if the file was saved successfully, false otherwise</returns>
    bool SaveAllFilesToXml(string xmlFilePath);

    /// <summary>
    /// Reads a file from the Prompts directory within the workspace root
    /// </summary>
    /// <param name="filename">The name of the file to read from the Prompts directory</param>
    /// <returns>The content of the file as a string, or null if the file doesn't exist or an error occurs</returns>
    string? GetGitHubPromptFileContent(string filename);
}