using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace GitVisionMCP.Services;

/// <summary>
/// Represents file information with relative path and metadata
/// </summary>
public class WorkspaceFileInfo
{
    /// <summary>
    /// Gets or sets the relative path from the workspace root
    /// </summary>
    public string RelativePath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file type (extension without the dot)
    /// </summary>
    public string FileType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full path to the file
    /// </summary>
    public string FullPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file size in bytes
    /// </summary>
    public long Size
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the last modified date
    /// </summary>
    public DateTime LastModified
    {
        get; set;
    }
}


/// <summary>
/// Implementation of location service that checks for GIT_REPOSITORY_DIRECTORY environment variable
/// </summary>
public class LocationService : ILocationService
{
    private readonly ILogger<LocationService> _logger;
    private readonly string _workspaceRoot;

    public LocationService(ILogger<LocationService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _workspaceRoot = DetermineWorkspaceRoot();
    }

    /// <summary>
    /// Gets the workspace root directory
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    public string GetWorkspaceRoot()
    {
        return _workspaceRoot;
    }
    public List<WorkspaceFileInfo> GetAllFilesMatching(string searchPattern)
    {
        var files = new List<WorkspaceFileInfo>();

        try
        {
            var workspaceRoot = new DirectoryInfo(_workspaceRoot);
            var allFiles = workspaceRoot.GetFiles(searchPattern, SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                var relativePath = Path.GetRelativePath(_workspaceRoot, file.FullName);
                var fileType = Path.GetExtension(file.Name).ToLowerInvariant();

                files.Add(new WorkspaceFileInfo
                {
                    RelativePath = relativePath,
                    FileType = string.IsNullOrEmpty(fileType) ? "no extension" : fileType.TrimStart('.'),
                    FullPath = file.FullName,
                    Size = file.Length,
                    LastModified = file.LastWriteTime
                });
            }

            _logger.LogInformation("Retrieved {FileCount} files from workspace root: {WorkspaceRoot}", files.Count, _workspaceRoot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving files from workspace root: {WorkspaceRoot}", _workspaceRoot);
        }

        return files;
    }
    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types
    /// </summary>
    /// <returns>A list of file information including relative path and file type</returns>
    public List<WorkspaceFileInfo> GetAllFiles()
    {
        var files = new List<WorkspaceFileInfo>();

        try
        {
            var workspaceRoot = new DirectoryInfo(_workspaceRoot);
            var allFiles = workspaceRoot.GetFiles("*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                var relativePath = Path.GetRelativePath(_workspaceRoot, file.FullName);
                var fileType = Path.GetExtension(file.Name).ToLowerInvariant();

                files.Add(new WorkspaceFileInfo
                {
                    RelativePath = relativePath,
                    FileType = string.IsNullOrEmpty(fileType) ? "no extension" : fileType.TrimStart('.'),
                    FullPath = file.FullName,
                    Size = file.Length,
                    LastModified = file.LastWriteTime
                });
            }

            _logger.LogInformation("Retrieved {FileCount} files from workspace root: {WorkspaceRoot}", files.Count, _workspaceRoot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving files from workspace root: {WorkspaceRoot}", _workspaceRoot);
        }

        return files;
    }

    /// <summary>
    /// Saves the output of GetAllFiles() to an XML file
    /// </summary>
    /// <param name="xmlFilePath">The path where the XML file will be saved</param>
    /// <returns>True if the file was saved successfully, false otherwise</returns>
    public bool SaveAllFilesToXml(string xmlFilePath)
    {
        if (string.IsNullOrWhiteSpace(xmlFilePath))
        {
            _logger.LogError("XML file path cannot be null or empty");
            return false;
        }

        try
        {
            var files = GetAllFiles();
            var xmlContent = new System.Text.StringBuilder();

            xmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xmlContent.AppendLine("<WorkspaceFiles>");
            xmlContent.AppendLine($"  <WorkspaceRoot>{System.Security.SecurityElement.Escape(_workspaceRoot)}</WorkspaceRoot>");
            xmlContent.AppendLine($"  <GeneratedAt>{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}</GeneratedAt>");
            xmlContent.AppendLine($"  <FileCount>{files.Count}</FileCount>");
            xmlContent.AppendLine("  <Files>");

            foreach (var file in files)
            {
                xmlContent.AppendLine("    <File>");
                xmlContent.AppendLine($"      <RelativePath>{System.Security.SecurityElement.Escape(file.RelativePath)}</RelativePath>");
                xmlContent.AppendLine($"      <FileType>{System.Security.SecurityElement.Escape(file.FileType)}</FileType>");
                xmlContent.AppendLine($"      <FullPath>{System.Security.SecurityElement.Escape(file.FullPath)}</FullPath>");
                xmlContent.AppendLine($"      <Size>{file.Size}</Size>");
                xmlContent.AppendLine($"      <LastModified>{file.LastModified:yyyy-MM-ddTHH:mm:ssZ}</LastModified>");
                xmlContent.AppendLine("    </File>");
            }

            xmlContent.AppendLine("  </Files>");
            xmlContent.AppendLine("</WorkspaceFiles>");

            // Create directory if it doesn't exist
            var directory = Path.GetDirectoryName(xmlFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(xmlFilePath, xmlContent.ToString());
            _logger.LogInformation("Successfully saved {FileCount} files to XML file: {XmlFilePath}", files.Count, xmlFilePath);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving files to XML file: {XmlFilePath}", xmlFilePath);
            return false;
        }
    }

    /// <summary>
    /// Reads a file from the Prompts directory within the workspace root
    /// </summary>
    /// <param name="filename">The name of the file to read from the Prompts directory</param>
    /// <returns>The content of the file as a string, or null if the file doesn't exist or an error occurs</returns>
    public string? GetGitHubPromptFileContent(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            _logger.LogError("Filename cannot be null or empty");
            return null;
        }

        try
        {
            var promptsDirectory = Path.Combine(_workspaceRoot, ".github/prompts");
            var filePath = Path.Combine(promptsDirectory, filename);

            if (!File.Exists(filePath))
            {
                _logger.LogWarning("Prompt file does not exist: {FilePath}", filePath);
                return null;
            }

            var content = ReadFile(filePath);
            _logger.LogInformation("Successfully read prompt file: {FilePath}", filePath);
            return content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading prompt file: {Filename}", filename);
            return null;
        }
    }
    string JTokenToString(JToken token, bool indented = false)
    {
        if (token == null)
        {
            return string.Empty;
        }

        // Use the ToString method with a Formatting option. [1, 2]
        Formatting formatting = indented ? Formatting.Indented : Formatting.None;
        return token.ToString(formatting);
    }
    public string? SearchJsonFile(string jsonFilePath, string jsonPath, bool indented = true, bool showKeyPaths = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                _logger.LogError("JSON file path cannot be null or empty");
                return null;
            }

            // Read the entire content of the JSON file into a string
            var filePath = Path.Combine(_workspaceRoot, jsonFilePath);
            var jsonContent = ReadFile(filePath);

            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                _logger.LogError("JSON file content is empty or null: {JsonFilePath}", jsonFilePath);
                return null;
            }

            // Parse the JSON string into a JObject
            JObject jsonObj = JObject.Parse(jsonContent);

            // Use SelectTokens to support wildcards and multiple results
            IEnumerable<JToken> results = jsonObj.SelectTokens(jsonPath);

            if (!results.Any())
            {
                _logger.LogWarning("No matches found for JSONPath: {JsonPath} in file: {JsonFilePath}", jsonPath, jsonFilePath);
                return String.Empty; // Return an empty string if no matches are found
            }

            // Handle multiple results
            var resultsList = results.ToList();

            if (showKeyPaths)
            {
                // When preserving keys, create a structured result that shows the path and value
                var structuredResults = new JArray();

                foreach (var result in resultsList)
                {
                    var pathInfo = new JObject();
                    pathInfo["path"] = result.Path;
                    pathInfo["value"] = result;
                    
                    // Try to extract a meaningful key name from the path
                    var pathParts = result.Path.Split('.');
                    if (pathParts.Length > 0)
                    {
                        var lastPart = pathParts.Last().Replace("[", "").Replace("]", "");
                        if (!string.IsNullOrEmpty(lastPart) && !char.IsDigit(lastPart[0]))
                        {
                            pathInfo["key"] = lastPart;
                        }
                    }
                    
                    structuredResults.Add(pathInfo);
                }

                if (structuredResults.Count == 1)
                {
                    return JTokenToString(structuredResults[0], indented);
                }
                else
                {
                    return JTokenToString(structuredResults, indented);
                }
            }
            else
            {
                // Original behavior - return values only
                if (resultsList.Count == 1)
                {
                    // Single result - return as-is
                    return JTokenToString(resultsList[0], indented);
                }
                else
                {
                    // Multiple results - return as JSON array
                    JArray resultArray = new JArray(resultsList);
                    return JTokenToString(resultArray, indented);
                }
            }
        }
        catch (FileNotFoundException)
        {
            _logger.LogError("The file '{JsonFilePath}' was not found", jsonFilePath);
            return null;
        }
        catch (JsonReaderException ex)
        {
            _logger.LogError(ex, "Invalid JSON format in '{JsonFilePath}'. Details: {Message}", jsonFilePath, ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while searching JSON file '{JsonFilePath}': {Message}", jsonFilePath, ex.Message);
            return null;
        }
    }

    public string? ReadFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))

        {
            _logger.LogError("Filename cannot be null or empty");
            return null;
        }

        try
        {


            if (!File.Exists(filePath))
            {
                _logger.LogWarning("Prompt file does not exist: {FilePath}", filePath);
                return null;
            }

            var content = File.ReadAllText(filePath);
            _logger.LogInformation("Successfully read file: {FilePath}", filePath);


            return content;
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error reading file: {Filename}", filePath);

            return null;
        }
    }
    /// <summary>
    /// Determines the workspace root directory by checking environment variable first
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    private string DetermineWorkspaceRoot()
    {
        var gitRepositoryDirectory = Environment.GetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY");

        if (!string.IsNullOrWhiteSpace(gitRepositoryDirectory))
        {
            if (Directory.Exists(gitRepositoryDirectory))
            {
                _logger.LogInformation("Using GIT_REPOSITORY_DIRECTORY environment variable: {GitRepositoryDirectory}", gitRepositoryDirectory);
                return gitRepositoryDirectory;
            }
            else
            {
                _logger.LogWarning("GIT_REPOSITORY_DIRECTORY environment variable is set but directory does not exist: {GitRepositoryDirectory}. Falling back to current directory.", gitRepositoryDirectory);
            }
        }

        var currentDirectory = Environment.CurrentDirectory;
        _logger.LogInformation("Using current directory as workspace root: {CurrentDirectory}", currentDirectory);
        return currentDirectory;
    }
}
