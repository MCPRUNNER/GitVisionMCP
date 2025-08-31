using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GitVisionMCP.Models;
namespace GitVisionMCP.Services;

public interface IWorkspaceService
{


    /// <summary>
    /// Searches a CSV file for values using JSONPath query with support for wildcards and structured
    /// results.
    /// /// </summary>
    /// <param name="csvFilePath">The path to the CSV file to search</param>
    /// <param name="jsonPath">The JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')</param>
    /// <param name="hasHeaderRecord">Indicates if the CSV file has a header record</param>
    /// <param name="ignoreBlankLines">Indicates if blank lines should be ignored</param>
    /// <returns>A string representation of the search results, or an empty string if no matches are found</returns>
    /// <remarks>
    /// This method supports searching CSV files using JSONPath queries, allowing for flexible
    /// searching of CSV data.
    /// It can handle CSV files with or without header records and can ignore blank lines.
    /// The results can be formatted as structured JSON with key paths, values, and keys.
    string? SearchCsvFile(string csvFilePath, string jsonPath, bool hasHeaderRecord = true, bool ignoreBlankLines = true);

    /// <summary>
    /// Searches for values in an Excel file (.xlsx) using JSONPath queries by converting worksheet data to JSON.
    /// Processes all worksheets and returns results for each.
    /// </summary>
    /// <param name="excelFilePath">Path to the Excel file relative to workspace root</param>
    /// <param name="jsonPath">JSONPath query string (e.g., '$[*].ServerName')</param>
    /// <returns>JSON search result or null if not found</returns>
    string? SearchExcelFile(string excelFilePath, string jsonPath);



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
    /// Processes a Scriban template with the provided JSON string data and saves the output to a file.
    /// </summary>
    /// <param name="jsonString">The JSON string data to use for processing the template</param>
    /// <param name="templateFilePath">The path to the Scriban template file</param>
    /// <param name="outputFilePath">The path to the output file where the result will be saved</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task<string?> ProcessScribanFromJsonStringAsync(string jsonString, string templateFilePath, string outputFilePath);

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
    /// <param name="destinationFilePath">Optional path to save the transformed XML to a file</param>
    /// <returns>The transformed XML as a string, or null if an error occurs</returns>
    string? TransformXmlWithXslt(string xmlFilePath, string xsltFilePath, string? destinationFilePath = null);



    /// <summary>
    /// Reads a file from the Prompts directory within the workspace root
    /// </summary>
    /// <param name="filename">The name of the file to read from the Prompts directory</param>
    /// <returns>The content of the file as a string, or null if the file doesn't exist or an error occurs</returns>
    string? GetGitHubPromptFileContent(string filename);

    /// <summary>
    /// Processes the autodocumentation configuration file and calls the DeconstructToFile method for each entry
    /// </summary>
    /// <param name="configFilePath">Path to the autodocument.json file (default: .gitvision/autodocument.json)</param>
    /// <param name="jsonPath">JSONPath to locate file mappings (default: $.Document.Files)</param>
    /// <param name="deconstructionService">The IDeconstructionService instance to use</param>
    /// <returns>A list of paths to the generated JSON files, or null if an error occurs</returns>
    List<string>? GenerateAutoDocumentationTempJson(
        string configFilePath = ".gitvision/autodocument.json",
        string jsonPath = "$.Documentation",
        string? templatePath = ".gitvision/.templates/autodoc.template.sbn",
        string? templateOutputPath = "Documentation/autodoc.md",
        IDeconstructionService? deconstructionService = null);
}