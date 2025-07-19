using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace GitVisionMCP.Services;

/// <summary>
/// Represents a controller action method
/// </summary>
public class ControllerAction
{
    public string Name { get; set; } = string.Empty;
    public string ReturnType { get; set; } = string.Empty;
    public string Accessibility { get; set; } = string.Empty;
    public List<string> Attributes { get; set; } = new();
    public List<ActionParameter> Parameters { get; set; } = new();
    public string HttpMethod { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public bool IsAsync
    {
        get; set;
    }
}

/// <summary>
/// Represents an action method parameter
/// </summary>
public class ActionParameter
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<string> Attributes { get; set; } = new();
    public bool HasDefaultValue
    {
        get; set;
    }
    public string? DefaultValue
    {
        get; set;
    }
}

/// <summary>
/// Represents a controller property
/// </summary>
public class ControllerProperty
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Accessibility { get; set; } = string.Empty;
    public List<string> Attributes { get; set; } = new();
    public bool HasGetter
    {
        get; set;
    }
    public bool HasSetter
    {
        get; set;
    }
}

/// <summary>
/// Represents the complete structure of a controller
/// </summary>
public class ControllerStructure
{
    public string ControllerName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string Namespace { get; set; } = string.Empty;
    public string BaseClass { get; set; } = string.Empty;
    public List<string> Interfaces { get; set; } = new();
    public List<string> UsingDirectives { get; set; } = new();
    public List<string> ClassAttributes { get; set; } = new();
    public List<ControllerAction> Actions { get; set; } = new();
    public List<ControllerProperty> Properties { get; set; } = new();
    public string RoutePrefix { get; set; } = string.Empty;
    public bool IsApiController
    {
        get; set;
    }
    public string FilePath { get; set; } = string.Empty;
    public DateTime AnalyzedAt
    {
        get; set;
    }
}

/// <summary>
/// Service for deconstructing ASP.NET Core controller files and extracting their structure
/// </summary>
public class DeconstructionService : IDeconstructionService
{
    private readonly ILogger<DeconstructionService> _logger;
    private readonly ILocationService _locationService;

    public DeconstructionService(ILogger<DeconstructionService> logger, ILocationService locationService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
    }

    /// <summary>
    /// Analyzes a C# ASP.NET Core controller file and returns its structure as JSON
    /// </summary>
    /// <param name="filePath">The path to the controller file relative to workspace root</param>
    /// <returns>JSON string representation of the controller structure</returns>
    public string? AnalyzeController(string filePath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogError("File path cannot be null or empty");
                return null;
            }

            // Read the controller file content
            var fullPath = Path.Combine(_locationService.GetWorkspaceRoot(), filePath);
            var fileContent = _locationService.ReadFile(fullPath);

            if (string.IsNullOrWhiteSpace(fileContent))
            {
                _logger.LogError("Controller file content is empty or null: {FilePath}", filePath);
                return null;
            }

            // Parse the controller structure
            var controllerStructure = ParseControllerFile(fileContent, filePath);

            // Convert to JSON
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonResult = JsonConvert.SerializeObject(controllerStructure, jsonSettings);

            _logger.LogInformation("Successfully analyzed controller: {FilePath}", filePath);
            return jsonResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error analyzing controller file: {FilePath}", filePath);
            return null;
        }
    }

    /// <summary>
    /// Parses the controller file content and extracts structure information
    /// </summary>
    private ControllerStructure ParseControllerFile(string fileContent, string filePath)
    {
        var structure = new ControllerStructure
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
    private void ExtractClassInformation(string[] lines, ControllerStructure structure)
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

                // Extract controller name (remove "Controller" suffix if present)
                structure.ControllerName = structure.ClassName.EndsWith("Controller")
                    ? structure.ClassName[..^10] // Remove "Controller"
                    : structure.ClassName;

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
    private void ParseInheritance(string inheritance, ControllerStructure structure)
    {
        var parts = inheritance.Split(',').Select(p => p.Trim()).ToList();

        if (parts.Any())
        {
            // First part is typically the base class
            var firstPart = parts[0];
            if (firstPart.Contains("Controller") || firstPart.Contains("ControllerBase"))
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
    private void ExtractActionsAndProperties(string fileContent, ControllerStructure structure)
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
    /// Parses a method into a ControllerAction
    /// </summary>
    private ControllerAction ParseAction(Match methodMatch, List<string> attributes)
    {
        var action = new ControllerAction
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
    private bool IsControllerAction(ControllerAction action)
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
    private List<ActionParameter> ParseParameters(string parametersString)
    {
        var parameters = new List<ActionParameter>();

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
    private ActionParameter? ParseSingleParameter(string paramString)
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

        var parameter = new ActionParameter
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
    private ControllerProperty ParseProperty(Match propertyMatch, List<string> attributes)
    {
        var property = new ControllerProperty
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
