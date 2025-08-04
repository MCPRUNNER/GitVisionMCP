using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using GitVisionMCP.Models;
using YamlDotNet.Serialization;
using ClosedXML.Excel;

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace GitVisionMCP.Services;



/// <summary>
/// Implementation of location service that provides search and transformation capabilities
/// </summary>
public class LocationService : ILocationService

{
    private readonly ILogger<LocationService> _logger;
    private readonly IFileService _fileService;

    public LocationService(ILogger<LocationService> logger, IFileService fileService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    /// <summary>
    /// Gets the value of an environment variable by name and returns it as an object.
    /// </summary>
    /// <param name="variableName">The name of the environment variable</param>
    /// <returns>The value of the environment variable as an object, or null if not set</returns>
    public object? GetEnvironmentVariableValue(string variableName)
    {
        if (string.IsNullOrWhiteSpace(variableName))
        {
            _logger.LogError("Environment variable name cannot be null or empty");
            return null;
        }
        var value = Environment.GetEnvironmentVariable(variableName);
        return value != null ? (object)value : null;
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

            for (var i = 0; i < results.Count; i++)
            {
                var result = results[i];
                var pathInfo = new JObject();

                string path;
                string value;
                string key;

                if (result is XElement element)
                {
                    path = GetXElementPath(element);
                    value = element.ToString(indented ? System.Xml.Linq.SaveOptions.None : System.Xml.Linq.SaveOptions.DisableFormatting);
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
                    return element.ToString(indented ? System.Xml.Linq.SaveOptions.None : System.Xml.Linq.SaveOptions.DisableFormatting);
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
                        resultArray.Add(element.ToString(System.Xml.Linq.SaveOptions.DisableFormatting));
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
    /// <summary>
    /// Reads a file from the workspace root directory
    /// </summary>
    /// <param name="fullCsvPath">The full path to the CSV file</param>
    /// <param name="hasHeaderRecord">Whether the CSV has a header record</param>
    /// <param name="ignoreBlankLines">Whether to ignore blank lines</param>
    /// <returns>A list of dynamic objects representing the CSV records</returns>
    private List<dynamic> ReadCsvRecords(string fullCsvPath, bool hasHeaderRecord, bool ignoreBlankLines)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = hasHeaderRecord,
            IgnoreBlankLines = ignoreBlankLines,
            TrimOptions = TrimOptions.Trim
        };

        using var reader = new StreamReader(fullCsvPath);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<dynamic>().ToList();
    }
    /// <summary>
    /// Extracts JSON tokens from a JSON string using a JSONPath query
    /// </summary>
    /// <param name="jsonContent">The JSON content as a string</param>
    /// <param name="jsonPath">The JSONPath query string</param>
    /// <returns>A collection of JToken objects matching the JSONPath query</returns>
    private IEnumerable<JToken> ExtractJTokens(string jsonContent, string jsonPath)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                _logger.LogError("JSON Path cannot be null or empty");
                return Enumerable.Empty<JToken>();
            }
            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                _logger.LogError("JSON content is empty or null: {JsonContent}", jsonContent);
                return new List<JToken>();
            }

            JToken jsonToken = JToken.Parse(jsonContent);
            return jsonToken.SelectTokens(jsonPath).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching JSON file: {JsonContent}", jsonContent);
            return new List<JToken>();
        }
    }

    /// <summary>
    /// Extracts JSON tokens from a list of records using a JSONPath query
    /// </summary>
    /// <param name="records">The list of records to search</param>
    /// <param name="jsonPath">The JSONPath query string</param>
    /// <returns>A collection of JToken objects matching the JSONPath query</returns>
    private IEnumerable<JToken> ExtractJTokens(List<object> records, string jsonPath)
    {


        var jsonContent = JsonConvert.SerializeObject(records, Newtonsoft.Json.Formatting.None);
        if (string.IsNullOrWhiteSpace(jsonContent))
        {
            _logger.LogError("JSON content is empty or null: {JsonContent}", jsonContent);
            return new List<JToken>();
        }
        var jsonArray = JArray.Parse(jsonContent);

        if (string.IsNullOrWhiteSpace(jsonPath))
        {
            _logger.LogError("JSON Path cannot be null or empty");
            return new List<JToken>();
        }
        var results = jsonArray.SelectTokens(jsonPath).ToList();
        if (results == null || !results.Any())
        {
            _logger.LogWarning("No matches found for JSONPath: {JsonPath} in content", jsonPath);
            return new List<JToken>();
        }

        return results;
    }


    /// <summary>
    /// Converts a JToken to a string with optional indentation
    /// </summary>
    /// <param name="token">The JToken to convert</param>
    /// <param name="indented">Whether to indent the output (default: false)</param>
    /// <returns>The string representation of the JToken</returns>
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

    /// <summary>
    /// Validates the input parameters for the CSV to JSON conversion
    /// </summary>
    /// <param name="csvFilePath"></param>
    /// <param name="jsonPath"></param>
    /// <returns></returns>
    private bool ValidateInputs(string csvFilePath, string jsonPath)
    {
        if (string.IsNullOrWhiteSpace(csvFilePath))
        {
            _logger.LogError("CSV file path cannot be null or empty");
            return false;
        }

        if (string.IsNullOrWhiteSpace(jsonPath))
        {
            _logger.LogError("JSONPath cannot be null or empty");
            return false;
        }

        return true;
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
            var promptsDirectory = Path.Combine(_fileService.GetWorkspaceRoot(), ".github/prompts");
            var filePath = Path.Combine(promptsDirectory, filename);
            if (string.IsNullOrEmpty(filePath))
            {
                _logger.LogWarning("Prompt file does not exist: {FilePath}", filePath);
                return null;
            }

            var content = _fileService.ReadFile(filePath);
            _logger.LogInformation("Successfully read prompt file: {FilePath}", filePath);
            return content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading prompt file: {Filename}", filename);
            return null;
        }
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
            var filePath = _fileService.GetFullPath(jsonFilePath);

            if (string.IsNullOrEmpty(filePath))
            {
                _logger.LogWarning("JSON file does not exist: {FilePath}", filePath);
                return null;
            }
            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                _logger.LogError("JSONPath cannot be null or empty");
                return null;
            }
            var jsonContent = _fileService.ReadFile(filePath);

            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                _logger.LogError("JSON file content is empty or null: {JsonFilePath}", jsonFilePath);
                return null;
            }

            // Parse the JSON string into a JObject
            IEnumerable<JToken> results = ExtractJTokens(jsonContent, jsonPath);
            if (results == null || !results.Any())
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


    public string? SearchCsvFile(string csvFilePath, string jsonPath, bool hasHeaderRecord = true, bool ignoreBlankLines = true)
    {
        if (!ValidateInputs(csvFilePath, jsonPath)) return null;

        var fullCsvPath = _fileService.GetFullPath(csvFilePath);
        if (string.IsNullOrEmpty(fullCsvPath))
        {
            _logger.LogWarning("CSV file does not exist: {CsvFilePath}", csvFilePath);
            return null;
        }

        try
        {
            var records = ReadCsvRecords(fullCsvPath, hasHeaderRecord, ignoreBlankLines);
            if (records == null || !records.Any())
            {
                _logger.LogWarning("No records found in CSV file: {CsvFilePath}", csvFilePath);
                return null;
            }
            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                _logger.LogError("JSONPath cannot be null or empty");
                return null;
            }
            IEnumerable<JToken> tokens = ExtractJTokens(records, jsonPath);
            if (tokens == null || !tokens.Any())
            {
                _logger.LogWarning("No matches found for JSONPath: {JsonPath} in CSV file: {CsvFilePath}", jsonPath, csvFilePath);
                return string.Empty; // Return an empty string if no matches are found
            }
            var results = tokens.ToList();
            if (!results.Any())
            {
                _logger.LogWarning("No matches found for JSONPath: {JsonPath} in CSV file: {CsvFilePath}", jsonPath, csvFilePath);
                return string.Empty;
            }

            return results.Count == 1
                ? results[0].ToString(Newtonsoft.Json.Formatting.Indented)
                : new JArray(results).ToString(Newtonsoft.Json.Formatting.Indented);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching CSV file: {CsvFilePath}", csvFilePath);
            return null;
        }
    }


    /// <summary>
    /// Extracts version information from a project file (e.g., .csproj) using XPath.
    /// </summary>
    /// <param name="projectFile"></param>
    /// <returns></returns>
    public string? GetAppVersion(string? projectFile)
    {
        try
        {
            if (string.IsNullOrEmpty(projectFile))
            {
                return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
            }

            var version = SearchXmlFile(projectFile, "/Project/PropertyGroup/Version/text()");
            return version;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting application version");
            return "Unknown Version";
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
            var filePath = Path.Combine(_fileService.GetWorkspaceRoot(), xmlFilePath);
            var xmlContent = _fileService.ReadFile(filePath);

            if (string.IsNullOrWhiteSpace(xmlContent))
            {
                _logger.LogError("XML file content is empty or null: {XmlFilePath}", xmlFilePath);
                return null;
            }

            // Parse the XML string into an XDocument
            XDocument xmlDoc = XDocument.Parse(xmlContent);

            // Use XPath to evaluate and get results (handles elements, attributes, and text)
            var xPathResults = xmlDoc.XPathEvaluate(xPath);
            var resultsList = new List<object>();

            // Handle different types of XPath results
            if (xPathResults is IEnumerable<object> enumerable)
            {
                resultsList.AddRange(enumerable);
            }
            else if (xPathResults != null)
            {
                resultsList.Add(xPathResults);
            }

            if (!resultsList.Any())
            {
                _logger.LogWarning("No matches found for XPath: {XPath} in file: {XmlFilePath}", xPath, xmlFilePath);
                return string.Empty; // Return an empty string if no matches are found
            }

            _logger.LogInformation("Successfully found {Count} matches for XPath: {XPath} in file: {XmlFilePath}",
                resultsList.Count, xPath, xmlFilePath);

            // Handle results
            return FormatXmlResults(resultsList, indented, showKeyPaths, xPath);
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
    /// Transforms an XML file using an XSLT stylesheet and returns the transformed result.
    /// </summary>
    /// <param name="xmlFilePath">The path to the XML file to transform</param>
    /// <param name="xsltFilePath">The path to the XSLT stylesheet file</param>
    /// <param name="destinationFilePath">Optional path to save the transformed XML to a file</param>
    /// <returns>The transformed XML as a string, or null if an error occurs</returns>
    public string? TransformXmlWithXslt(string xmlFilePath, string xsltFilePath, string? destinationFilePath = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(xmlFilePath))
            {
                _logger.LogError("XML file path cannot be null or empty");
                return null;
            }

            if (string.IsNullOrWhiteSpace(xsltFilePath))
            {
                _logger.LogError("XSLT file path cannot be null or empty");
                return null;
            }

            // Get full paths relative to workspace root
            var fullXmlPath = Path.Combine(_fileService.GetWorkspaceRoot(), xmlFilePath);
            var fullXsltPath = Path.Combine(_fileService.GetWorkspaceRoot(), xsltFilePath);

            // Check if XML file exists
            if (!File.Exists(fullXmlPath))
            {
                _logger.LogError("XML file not found: {XmlFilePath}", xmlFilePath);
                return null;
            }

            // Check if XSLT file exists
            if (!File.Exists(fullXsltPath))
            {
                _logger.LogError("XSLT file not found: {XsltFilePath}", xsltFilePath);
                return null;
            }

            // Load the XSLT stylesheet
            var xslt = new XslCompiledTransform();
            xslt.Load(fullXsltPath);

            // Load the XML document
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fullXmlPath);

            // Perform the transformation
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings);

            xslt.Transform(xmlDoc, xmlWriter);

            var result = stringWriter.ToString();

            // Save to destination file if path is provided
            if (!string.IsNullOrWhiteSpace(destinationFilePath))
            {
                try
                {
                    var fullDestinationPath = Path.IsPathRooted(destinationFilePath)
                        ? destinationFilePath
                        : Path.Combine(_fileService.GetWorkspaceRoot(), destinationFilePath);

                    // Create directory if it doesn't exist
                    var directory = Path.GetDirectoryName(fullDestinationPath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Write the transformed result to the destination file
                    File.WriteAllText(fullDestinationPath, result);

                    _logger.LogInformation("Successfully saved transformed XML to: {DestinationFilePath}", destinationFilePath);
                }
                catch (Exception saveEx)
                {
                    _logger.LogError(saveEx, "Error saving transformed XML to destination file: {DestinationFilePath}", destinationFilePath);
                    // Continue execution - don't fail the transformation if save fails
                }
            }

            _logger.LogInformation("Successfully transformed XML file '{XmlFilePath}' using XSLT '{XsltFilePath}'",
                xmlFilePath, xsltFilePath);

            return result;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "File not found during XSLT transformation: {Message}", ex.Message);
            return null;
        }
        catch (XmlException ex)
        {
            _logger.LogError(ex, "Invalid XML format during XSLT transformation. Details: {Message}", ex.Message);
            return null;
        }
        catch (XsltException ex)
        {
            _logger.LogError(ex, "XSLT transformation error. Details: {Message}", ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred during XSLT transformation: {Message}", ex.Message);
            return null;
        }
    }



    public string? SearchYamlFile(string yamlFilePath, string jsonPath, bool indented = true, bool showKeyPaths = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(yamlFilePath))
            {
                _logger.LogError("YAML file path cannot be null or empty");
                return null;
            }

            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                _logger.LogError("JSONPath cannot be null or empty");
                return null;
            }

            // Read the entire content of the YAML file into a string
            var filePath = _fileService.GetFullPath(yamlFilePath);
            if (string.IsNullOrEmpty(filePath))
            {
                _logger.LogWarning("YAML file does not exist: {YamlFilePath}", yamlFilePath);
                return null;
            }
            var yamlContent = _fileService.ReadFile(filePath);

            if (string.IsNullOrWhiteSpace(yamlContent))
            {
                _logger.LogError("YAML file content is empty or null: {YamlFilePath}", yamlFilePath);
                return null;
            }

            // Parse YAML and convert to JSON
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(yamlContent);

            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();
            var jsonContent = serializer.Serialize(yamlObject);

            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                _logger.LogError("Failed to convert YAML content to JSON: {YamlFilePath}", yamlFilePath);
                return null;
            }
            var results = ExtractJTokens(jsonContent, jsonPath);

            if (results == null || !results.Any())
            {
                _logger.LogWarning("No matches found for JSONPath: {JsonPath} in YAML file: {YamlFilePath}", jsonPath, yamlFilePath);
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
                // Format the results based on the count
                if (resultsList.Count == 1)
                {
                    // Single result, format nicely
                    return JTokenToString(resultsList[0], indented);
                }
                else
                {
                    // Multiple results, return as array
                    var array = new JArray(resultsList);
                    return JTokenToString(array, indented);
                }
            }
        }
        catch (FileNotFoundException)
        {
            _logger.LogError("The YAML file '{YamlFilePath}' was not found", yamlFilePath);
            return null;
        }
        catch (YamlDotNet.Core.YamlException ex)
        {
            _logger.LogError(ex, "Invalid YAML format in '{YamlFilePath}'. Details: {Message}", yamlFilePath, ex.Message);
            return null;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error processing YAML conversion to JSON in '{YamlFilePath}'. Details: {Message}", yamlFilePath, ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while searching YAML file '{YamlFilePath}': {Message}", yamlFilePath, ex.Message);
            return null;
        }
    }



    public async Task<List<Models.FileContentInfo>> GetFileContentsAsync(List<WorkspaceFileInfo> workspaceFileList)
    {
        return await _fileService.GetFileContentsAsync(workspaceFileList);
    }
    /// <summary>
    /// Searches for values in an Excel file (.xlsx) using JSONPath queries by converting worksheet data to JSON.
    /// Processes all worksheets and returns results for each.
    /// </summary>
    /// <param name="excelFilePath">Path to the Excel file relative to workspace root</param>
    /// <param name="jsonPath">JSONPath query string (e.g., '$[*].ServerName')</param>
    /// <returns>JSON search result or null if not found</returns>
    public string? SearchExcelFile(string excelFilePath, string jsonPath)
    {
        if (!ValidateInputs(excelFilePath, jsonPath)) return null;

        var fullExcelPath = _fileService.GetFullPath(excelFilePath);
        if (string.IsNullOrEmpty(fullExcelPath))
        {
            _logger.LogWarning("Excel file does not exist: {ExcelFilePath}", excelFilePath);
            return null;
        }

        try
        {
            using var workbook = new XLWorkbook(fullExcelPath);
            // Build a JSON object mapping each sheet name to its full row array
            var workbookJson = new JObject();

            foreach (var worksheet in workbook.Worksheets)
            {
                var rows = new List<Dictionary<string, object>>();
                var firstRow = worksheet.FirstRowUsed();
                if (firstRow == null) continue;

                var headerRow = firstRow.RowUsed();
                var headers = headerRow.Cells().Select(c => c.GetString()).ToList();
                var headerCells = headerRow.Cells().ToList();

                // Map header index to column letter
                var headerLetters = headerCells.Select(c => c.Address.ColumnLetter).ToList();

                foreach (var dataRow in worksheet.RowsUsed().Skip(1))
                {
                    var rowDict = new Dictionary<string, object>();
                    var cells = dataRow.Cells().ToList();
                    for (var i = 0; i < headers.Count && i < cells.Count; i++)
                    {
                        var value = cells[i].GetString();
                        rowDict[headers[i]] = value;
                        // Only add column letter if it does not conflict with a header name
                        if (!headers.Contains(headerLetters[i]))
                        {
                            rowDict[headerLetters[i]] = value;
                        }
                    }
                    rows.Add(rowDict);
                }

                // Add full sheet array under its name
                workbookJson[worksheet.Name] = JArray.FromObject(rows);
            }

            // Apply JSONPath query against the entire workbook JSON
            var fullJson = workbookJson.ToString(Newtonsoft.Json.Formatting.None);
            var matchedTokens = ExtractJTokens(fullJson, jsonPath);
            var resultToken = (matchedTokens != null && matchedTokens.Any())
                ? (matchedTokens.Count() == 1 ? matchedTokens.First() : new JArray(matchedTokens))
                : new JArray();

            return resultToken.ToString(Newtonsoft.Json.Formatting.Indented);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching Excel file: {ExcelFilePath}", excelFilePath);
            return null;
        }
    }
}
