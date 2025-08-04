using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using GitVisionMCP.Models;
using System.Text.RegularExpressions;

namespace GitVisionMCP.Services;







/// <summary>
/// Service for deconstructing a C# Service, Repository or Controller file and extracting their structure
/// </summary>
public class DeconstructionService : IDeconstructionService
{
    private readonly ILogger<DeconstructionService> _logger;
    private readonly ILocationService _locationService;
    private readonly IFileService _fileService;

    public DeconstructionService(ILogger<DeconstructionService> logger, ILocationService locationService, IFileService fileService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    /// <summary>
    /// Deconstructs a C# Repository, Service or Controller and returns its structure as JSON
    /// </summary>
    /// <param name="filePath">The path to the source file relative to workspace root</param>
    /// <returns>JSON string representation of the source structure</returns>
    public string? Deconstruct(string filePath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogError("File path cannot be null or empty");
                return null;
            }

            // Read the source file content
            var fullPath = _fileService.GetFullPath(filePath);
            if (string.IsNullOrWhiteSpace(fullPath) || !File.Exists(fullPath))
            {
                _logger.LogError("Source file not found: {FilePath}", filePath);
                return null;
            }
            var fileContent = _fileService.ReadFile(fullPath);

            if (string.IsNullOrWhiteSpace(fileContent))
            {
                _logger.LogError("Source file content is empty or null: {FilePath}", filePath);
                return null;
            }

            // Parse the source structure
            var sourceStructure = ParseSourceFile(fileContent, filePath);

            // Convert to JSON
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonResult = JsonConvert.SerializeObject(sourceStructure, jsonSettings);

            _logger.LogInformation("Successfully analyzed source: {FilePath}", filePath);
            return jsonResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error analyzing source file: {FilePath}", filePath);
            return null;
        }
    }

    /// <summary>
    /// Deconstructs a C# Repository, Service or Controller and saves the structure to a JSON file in the workspace directory
    /// </summary>
    /// <param name="filePath">The path to the controller file relative to workspace root</param>
    /// <param name="outputFileName">The name of the output JSON file (optional, defaults to controller name + '_analysis.json')</param>
    /// <returns>The full path to the saved JSON file, or null if the operation failed</returns>
    public string? DeconstructToFile(string filePath, string? outputFileName = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogError("File path cannot be null or empty");
                return null;
            }

            // Analyze the source
            var jsonResult = Deconstruct(filePath);
            if (string.IsNullOrEmpty(jsonResult))
            {
                _logger.LogError("Failed to deconstruct source: {FilePath}", filePath);
                return null;
            }

            // Generate output filename if not provided
            if (string.IsNullOrWhiteSpace(outputFileName))
            {
                var sourceFileName = Path.GetFileNameWithoutExtension(filePath);
                outputFileName = $"{sourceFileName}_analysis.json";
            }

            // Ensure .json extension
            if (!outputFileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                outputFileName += ".json";
            }

            // Create full output path in workspace directory
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var outputPath = Path.Combine(workspaceRoot, outputFileName);

            // Create directory if it doesn't exist
            var directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save to file
            File.WriteAllText(outputPath, jsonResult, Encoding.UTF8);

            _logger.LogInformation("Successfully saved source analysis to: {OutputPath}", outputPath);
            return outputPath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving source analysis to file: {FilePath}", filePath);
            return null;
        }
    }

    /// <summary>
    /// Parses the source file content and extracts structure information
    /// </summary>
    private DeconstructorModel ParseSourceFile(string fileContent, string filePath)
    {
        var structure = new DeconstructorModel
        {
            FilePath = filePath,
            AnalyzedAt = DateTime.UtcNow
        };

        var lines = fileContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        // Extract using directives
        structure.UsingDirectives = ExtractUsingDirectives(lines);

        // Extract namespace
        structure.Namespace = ExtractNamespace(lines);

        // Extract class information
        ExtractClassInformation(lines, structure);

        // Extract actions and properties
        ExtractActionsAndProperties(fileContent, structure);

        return structure;
    }

    /// <summary>
    /// Extracts using directives from the file
    /// </summary>
    private List<string> ExtractUsingDirectives(string[] lines)
    {
        var usingDirectives = new List<string>();
        var usingRegex = new Regex(@"^\s*using\s+(.+?);", RegexOptions.Multiline);

        foreach (var line in lines)
        {
            var match = usingRegex.Match(line);
            if (match.Success)
            {
                usingDirectives.Add(match.Groups[1].Value.Trim());
            }
        }

        return usingDirectives;
    }

    /// <summary>
    /// Extracts the namespace from the file
    /// </summary>
    private string ExtractNamespace(string[] lines)
    {
        var namespaceRegex = new Regex(@"^\s*namespace\s+(.+?)[\s{;]", RegexOptions.Multiline);

        foreach (var line in lines)
        {
            var match = namespaceRegex.Match(line);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// Extracts class information including name, base class, interfaces, and attributes
    /// </summary>
    private void ExtractClassInformation(string[] lines, DeconstructorModel structure)
    {
        var classRegex = new Regex(@"^\s*(?:public|internal|private|protected)?\s*(?:abstract|sealed)?\s*class\s+(\w+)(?:\s*:\s*(.+?))?\s*{?", RegexOptions.Multiline);
        var attributeRegex = new Regex(@"^\s*\[(.+?)\]", RegexOptions.Multiline);

        var classAttributes = new List<string>();

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();

            // Check for attributes above the class
            var attrMatch = attributeRegex.Match(line);
            if (attrMatch.Success)
            {
                var attribute = attrMatch.Groups[1].Value.Trim();
                classAttributes.Add(attribute);

                // Check for specific controller attributes
                if (attribute.Contains("ApiController"))
                {
                    structure.IsApiController = true;
                }

                if (attribute.StartsWith("Route"))
                {
                    structure.RoutePrefix = ExtractRouteValue(attribute);
                }

                continue;
            }

            // Check for class declaration
            var classMatch = classRegex.Match(line);
            if (classMatch.Success)
            {
                structure.ClassName = classMatch.Groups[1].Value.Trim();
                structure.ClassAttributes = classAttributes;

                switch (structure.ClassName)
                {
                    case string s when s.EndsWith("Controller", StringComparison.OrdinalIgnoreCase):
                    case string s1 when s1.EndsWith("ControllerBase", StringComparison.OrdinalIgnoreCase):
                        structure.Name = structure.ClassName[..^"Controller".Length];
                        structure.ArchitectureModel = "Controller";
                        break;
                    case string s when s.EndsWith("Service", StringComparison.OrdinalIgnoreCase):
                        structure.Name = structure.ClassName[..^"Service".Length];
                        structure.ArchitectureModel = "Service";
                        break;
                    case string s when s.EndsWith("Repository", StringComparison.OrdinalIgnoreCase):
                        structure.Name = structure.ClassName[..^"Repository".Length];
                        structure.ArchitectureModel = "Repository";
                        break;
                    default:
                        structure.Name = structure.ClassName;
                        structure.ArchitectureModel = "Unknown";
                        break;
                }



                // Parse base class and interfaces
                if (classMatch.Groups.Count > 2 && !string.IsNullOrEmpty(classMatch.Groups[2].Value))
                {
                    var inheritance = classMatch.Groups[2].Value.Trim();
                    ParseInheritance(inheritance, structure);
                }

                break;
            }
        }
    }

    /// <summary>
    /// Extracts route value from route attribute
    /// </summary>
    private string ExtractRouteValue(string routeAttribute)
    {
        var match = Regex.Match(routeAttribute, @"Route\(""([^""]+)""\)");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    /// <summary>
    /// Parses inheritance information (base class and interfaces)
    /// </summary>
    private void ParseInheritance(string inheritance, DeconstructorModel structure)
    {
        var parts = inheritance.Split(',').Select(p => p.Trim()).ToList();

        if (parts.Any())
        {
            // First part is typically the base class
            var firstPart = parts[0];
            if (firstPart.Contains("Controller") || firstPart.Contains("ControllerBase") || firstPart.Contains("Service") || firstPart.Contains("Repository"))
            {
                structure.BaseClass = firstPart;
                structure.Interfaces = parts.Skip(1).ToList();
            }
            else
            {
                // All parts are interfaces
                structure.Interfaces = parts;
            }
        }
    }

    /// <summary>
    /// Extracts controller actions and properties from the file content
    /// </summary>
    private void ExtractActionsAndProperties(string fileContent, DeconstructorModel structure)
    {
        // Method pattern for public methods (potential actions)
        var methodRegex = new Regex(@"^\s*(?:\[.*?\]\s*)*\s*(public|protected|private|internal)\s+(?:(async)\s+)?(?:(virtual|override|new)\s+)?(\w+(?:<[^>]+>)?(?:\[\])?)\s+(\w+)\s*\((.*?)\)\s*{?",
            RegexOptions.Multiline | RegexOptions.Singleline);

        // Property pattern
        var propertyRegex = new Regex(@"^\s*(?:\[.*?\]\s*)*\s*(public|protected|private|internal)\s+(\w+(?:<[^>]+>)?(?:\[\])?)\s+(\w+)\s*{\s*(.*?)\s*}",
            RegexOptions.Multiline | RegexOptions.Singleline);

        var lines = fileContent.Split('\n');
        var currentAttributes = new List<string>();

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();

            // Check for attributes
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                var attribute = line[1..^1]; // Remove [ and ]
                currentAttributes.Add(attribute);
                continue;
            }

            // Check for methods
            var methodMatch = methodRegex.Match(line);
            if (methodMatch.Success)
            {
                var action = ParseAction(methodMatch, currentAttributes);
                if (IsControllerAction(action))
                {
                    structure.Actions.Add(action);
                }
                currentAttributes.Clear();
                continue;
            }

            // Check for properties
            var propertyMatch = propertyRegex.Match(line);
            if (propertyMatch.Success)
            {
                var property = ParseProperty(propertyMatch, currentAttributes);
                structure.Properties.Add(property);
                currentAttributes.Clear();
                continue;
            }

            // If line doesn't match any pattern, clear accumulated attributes
            if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("//"))
            {
                currentAttributes.Clear();
            }
        }
    }

    /// <summary>
    /// Parses a method into a DeconstructorActionModel
    /// </summary>
    private DeconstructorActionModel ParseAction(Match methodMatch, List<string> attributes)
    {
        var action = new DeconstructorActionModel
        {
            Accessibility = methodMatch.Groups[1].Value,
            IsAsync = !string.IsNullOrEmpty(methodMatch.Groups[2].Value),
            ReturnType = methodMatch.Groups[4].Value,
            Name = methodMatch.Groups[5].Value,
            Attributes = new List<string>(attributes)
        };

        // Extract HTTP method and route from attributes
        foreach (var attr in attributes)
        {
            if (attr.StartsWith("Http"))
            {
                action.HttpMethod = ExtractHttpMethod(attr);
                action.Route = ExtractRouteFromHttpAttribute(attr);
            }
            else if (attr.StartsWith("Route"))
            {
                action.Route = ExtractRouteValue(attr);
            }
        }

        // Parse parameters
        var parametersString = methodMatch.Groups[6].Value;
        action.Parameters = ParseParameters(parametersString);

        return action;
    }

    /// <summary>
    /// Determines if a method is likely a controller action
    /// </summary>
    private bool IsControllerAction(DeconstructorActionModel action)
    {
        // Public methods in controllers are typically actions
        return action.Accessibility == "public" &&
               !action.Name.StartsWith("get_") &&
               !action.Name.StartsWith("set_");
    }

    /// <summary>
    /// Extracts HTTP method from HTTP attribute
    /// </summary>
    private string ExtractHttpMethod(string attribute)
    {
        var methods = new[] { "HttpGet", "HttpPost", "HttpPut", "HttpDelete", "HttpPatch", "HttpHead", "HttpOptions" };
        foreach (var method in methods)
        {
            if (attribute.StartsWith(method))
            {
                return method[4..]; // Remove "Http" prefix
            }
        }
        return "GET"; // Default
    }

    /// <summary>
    /// Extracts route from HTTP method attribute
    /// </summary>
    private string ExtractRouteFromHttpAttribute(string attribute)
    {
        var match = Regex.Match(attribute, @"\(""([^""]+)""\)");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    /// <summary>
    /// Parses method parameters
    /// </summary>
    private List<DeconstructionActionParameterModel> ParseParameters(string parametersString)
    {
        var parameters = new List<DeconstructionActionParameterModel>();

        if (string.IsNullOrWhiteSpace(parametersString))
        {
            return parameters;
        }

        // Split parameters by comma (simple approach - could be improved for complex generics)
        var paramStrings = parametersString.Split(',');

        foreach (var paramString in paramStrings)
        {
            var param = ParseSingleParameter(paramString.Trim());
            if (param != null)
            {
                parameters.Add(param);
            }
        }

        return parameters;
    }

    /// <summary>
    /// Parses a single parameter
    /// </summary>
    private DeconstructionActionParameterModel? ParseSingleParameter(string paramString)
    {
        if (string.IsNullOrWhiteSpace(paramString))
        {
            return null;
        }

        // Basic parameter parsing: [attributes] type name [= defaultValue]
        var paramRegex = new Regex(@"(?:\[.*?\]\s*)?(\w+(?:<[^>]+>)?(?:\[\])?)\s+(\w+)(?:\s*=\s*(.+))?");
        var match = paramRegex.Match(paramString);

        if (!match.Success)
        {
            return null;
        }

        var parameter = new DeconstructionActionParameterModel
        {
            Type = match.Groups[1].Value,
            Name = match.Groups[2].Value,
            HasDefaultValue = match.Groups.Count > 3 && !string.IsNullOrEmpty(match.Groups[3].Value),
            DefaultValue = match.Groups.Count > 3 ? match.Groups[3].Value : null
        };

        // Extract attributes (simplified)
        var attributeMatches = Regex.Matches(paramString, @"\[([^\]]+)\]");
        foreach (Match attrMatch in attributeMatches)
        {
            parameter.Attributes.Add(attrMatch.Groups[1].Value);
        }

        return parameter;
    }

    /// <summary>
    /// Parses a property
    /// </summary>
    private DeconstructorPropertyModel ParseProperty(Match propertyMatch, List<string> attributes)
    {
        var property = new DeconstructorPropertyModel
        {
            Accessibility = propertyMatch.Groups[1].Value,
            Type = propertyMatch.Groups[2].Value,
            Name = propertyMatch.Groups[3].Value,
            Attributes = new List<string>(attributes)
        };

        // Check for getter and setter
        var accessorsString = propertyMatch.Groups[4].Value;
        property.HasGetter = accessorsString.Contains("get");
        property.HasSetter = accessorsString.Contains("set");

        return property;
    }
}
