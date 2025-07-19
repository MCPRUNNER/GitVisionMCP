using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
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
    private string JTokenToString(JToken token, bool indented = false)
    {
        if (token == null)
        {
            return string.Empty;
        }

        // Use the ToString method with a Formatting option. [1, 2]
        Newtonsoft.Json.Formatting formatting = indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
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
                return string.Empty; // Return an empty string if no matches are found
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

    /// <summary>
    /// Searches for XML values in an XML file using XPath queries with support for namespaces and structured results.
    /// </summary>
    /// <param name="xmlFilePath">The path to the XML file relative to workspace root</param>
    /// <param name="xPath">The XPath query string (e.g., '//users/user/@email', '/configuration/database/host')</param>
    /// <param name="indented">Whether to format the output as indented XML</param>
    /// <param name="showKeyPaths">Whether to return structured results with path, value, and key information</param>
    /// <returns>A string representation of the search results, or an empty string if no matches are found</returns>
    public string? SearchXmlFile(string xmlFilePath, string xPath, bool indented = true, bool showKeyPaths = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(xmlFilePath))
            {
                _logger.LogError("XML file path cannot be null or empty");
                return null;
            }

            if (string.IsNullOrWhiteSpace(xPath))
            {
                _logger.LogError("XPath cannot be null or empty");
                return null;
            }

            // Read the entire content of the XML file into a string
            var filePath = Path.Combine(_workspaceRoot, xmlFilePath);
            var xmlContent = ReadFile(filePath);

            if (string.IsNullOrWhiteSpace(xmlContent))
            {
                _logger.LogError("XML file content is empty or null: {XmlFilePath}", xmlFilePath);
                return null;
            }

            // Parse the XML string into an XDocument
            XDocument xmlDoc = XDocument.Parse(xmlContent);

            // Use XPath to find matching nodes
            var results = xmlDoc.XPathSelectElements(xPath).ToList();

            if (!results.Any())
            {
                // Try selecting attributes or text nodes
                var attributeResults = xmlDoc.XPathEvaluate(xPath);

                if (attributeResults is IEnumerable<object> enumerable)
                {
                    var resultList = enumerable.ToList();
                    if (resultList.Any())
                    {
                        return FormatXmlResults(resultList, indented, showKeyPaths, xPath);
                    }
                }

                _logger.LogWarning("No matches found for XPath: {XPath} in file: {XmlFilePath}", xPath, xmlFilePath);
                return string.Empty; // Return an empty string if no matches are found
            }

            // Handle multiple element results
            return FormatXmlResults(results.Cast<object>().ToList(), indented, showKeyPaths, xPath);
        }
        catch (FileNotFoundException)
        {
            _logger.LogError("The file '{XmlFilePath}' was not found", xmlFilePath);
            return null;
        }
        catch (XmlException ex)
        {
            _logger.LogError(ex, "Invalid XML format in '{XmlFilePath}'. Details: {Message}", xmlFilePath, ex.Message);
            return null;
        }
        catch (System.Xml.XPath.XPathException ex)
        {
            _logger.LogError(ex, "Invalid XPath expression '{XPath}'. Details: {Message}", xPath, ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while searching XML file '{XmlFilePath}': {Message}", xmlFilePath, ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Formats XML search results into the appropriate output format
    /// </summary>
    private string FormatXmlResults(List<object> results, bool indented, bool showKeyPaths, string xPath)
    {
        if (showKeyPaths)
        {
            // When preserving keys, create a structured result that shows the path and value
            var structuredResults = new JArray();

            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                var pathInfo = new JObject();

                string path = "";
                string value = "";
                string key = "";

                if (result is XElement element)
                {
                    path = GetXElementPath(element);
                    value = element.ToString(indented ? SaveOptions.None : SaveOptions.DisableFormatting);
                    key = element.Name.LocalName;
                }
                else if (result is XAttribute attribute)
                {
                    path = GetXAttributePath(attribute);
                    value = attribute.Value;
                    key = attribute.Name.LocalName;
                }
                else if (result is XText text)
                {
                    path = GetXTextPath(text);
                    value = text.Value;
                    key = "text";
                }
                else
                {
                    path = $"{xPath}[{i}]";
                    value = result.ToString() ?? "";
                    key = ExtractKeyFromXPath(xPath);
                }

                pathInfo["path"] = path;
                pathInfo["value"] = value;
                pathInfo["key"] = key;

                structuredResults.Add(pathInfo);
            }

            if (structuredResults.Count == 1)
            {
                return structuredResults[0].ToString(indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
            }
            else
            {
                return structuredResults.ToString(indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
            }
        }
        else
        {
            // Original behavior - return values only
            if (results.Count == 1)
            {
                // Single result - return as-is
                var result = results[0];

                if (result is XElement element)
                {
                    return element.ToString(indented ? SaveOptions.None : SaveOptions.DisableFormatting);
                }
                else if (result is XAttribute attribute)
                {
                    return attribute.Value;
                }
                else
                {
                    return result.ToString() ?? "";
                }
            }
            else
            {
                // Multiple results - return as JSON array for consistency
                var resultArray = new JArray();

                foreach (var result in results)
                {
                    if (result is XElement element)
                    {
                        resultArray.Add(element.ToString(SaveOptions.DisableFormatting));
                    }
                    else if (result is XAttribute attribute)
                    {
                        resultArray.Add(attribute.Value);
                    }
                    else
                    {
                        resultArray.Add(result.ToString());
                    }
                }

                return resultArray.ToString(indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
            }
        }
    }

    /// <summary>
    /// Gets the XPath for an XElement
    /// </summary>
    private string GetXElementPath(XElement element)
    {
        var path = new List<string>();
        var current = element;

        while (current != null)
        {
            var index = current.ElementsBeforeSelf(current.Name).Count();
            var name = current.Name.LocalName;

            if (index > 0)
            {
                path.Insert(0, $"{name}[{index + 1}]");
            }
            else
            {
                path.Insert(0, name);
            }

            current = current.Parent;
        }

        return "/" + string.Join("/", path);
    }

    /// <summary>
    /// Gets the XPath for an XAttribute
    /// </summary>
    private string GetXAttributePath(XAttribute attribute)
    {
        var elementPath = GetXElementPath(attribute.Parent ?? throw new InvalidOperationException("Attribute has no parent element"));
        return $"{elementPath}/@{attribute.Name.LocalName}";
    }

    /// <summary>
    /// Gets the XPath for an XText node
    /// </summary>
    private string GetXTextPath(XText text)
    {
        var elementPath = GetXElementPath(text.Parent ?? throw new InvalidOperationException("Text node has no parent element"));
        return $"{elementPath}/text()";
    }

    /// <summary>
    /// Extracts a meaningful key name from an XPath expression
    /// </summary>
    private string ExtractKeyFromXPath(string xPath)
    {
        // Remove leading slashes and extract the last meaningful part
        var parts = xPath.TrimStart('/').Split('/');
        var lastPart = parts.LastOrDefault()?.Split('@').LastOrDefault()?.Split('[').FirstOrDefault();
        return !string.IsNullOrEmpty(lastPart) ? lastPart : "result";
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
