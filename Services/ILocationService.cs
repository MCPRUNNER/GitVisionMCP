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
    /// Gets all files under the workspace root directory with relative paths and file types, excluding files matching exclude patterns
    /// </summary>
    /// <returns>A list of file information including relative path and file type, excluding excluded files</returns>
    Task<List<WorkspaceFileInfo>> GetAllFilesAsync();
    /// <summary>
    /// Gets the full path of a file based on its relative path within the workspace root directory.
    /// This method resolves the relative path in the workspace root to provide the absolute path.
    /// </summary>
    /// <param name="relativePath"></param>
    /// <returns></returns>
    string? GetFullPath(string relativePath);
    /// <summary>
    /// Checks if a file path should be excluded based on the exclude patterns
    /// </summary>
    /// <param name="relativePath">The relative path to check</param>
    /// <returns>True if the file should be excluded, false otherwise</returns>
    Task<bool> IsFileExcludedAsync(string relativePath);


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
    /// Searches for YAML values in a YAML file using JSONPath queries by converting YAML to JSON.
    /// </summary>
    /// <param name="yamlFilePath">The path to the YAML file to search</param>
    /// <param name="jsonPath">The JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')</param>
    /// <param name="indented">Whether to format the output as indented JSON</param>
    /// <param name="showKeyPaths">Whether to return structured results with path, value, and key information</param>
    /// <returns>A string representation of the search results, or an empty string if no matches are found</returns>
    string? SearchYamlFile(string yamlFilePath, string jsonPath, bool indented = true, bool showKeyPaths = false);

    /// <summary>
    /// Searches for XML values in an XML file using XPath queries with support for namespaces and structured results.
    /// </summary>
    /// <param name="xmlFilePath">The path to the XML file to search</param>
    /// <param name="xPath">The XPath query string (e.g., '//users/user/@email', '/configuration/database/host')</param>
    /// <param name="indented">Whether to format the output as indented XML</param>
    /// <param name="showKeyPaths">Whether to return structured results with path, value, and key information</param>
    /// <returns>A string representation of the search results, or an empty string if no matches are found</returns>
    string? SearchXmlFile(string xmlFilePath, string xPath, bool indented = true, bool showKeyPaths = false);

    /// <summary>
    /// Transforms an XML file using an XSLT stylesheet and returns the transformed result.
    /// </summary>
    /// <param name="xmlFilePath">The path to the XML file to transform</param>
    /// <param name="xsltFilePath">The path to the XSLT stylesheet file</param>
    /// <returns>The transformed XML as a string, or null if an error occurs</returns>
    string? TransformXmlWithXslt(string xmlFilePath, string xsltFilePath);

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