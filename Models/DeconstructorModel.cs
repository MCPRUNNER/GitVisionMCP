
using GitVisionMCP.Models;
using GitVisionMCP.Services;
namespace GitVisionMCP.Models;
/// <summary>
/// Represents a controller property
/// </summary>

/// <summary>
/// Represents the complete structure of a controller
/// </summary>
public class DeconstructorModel
{
    public string ControllerName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string Namespace { get; set; } = string.Empty;
    public string BaseClass { get; set; } = string.Empty;
    public List<string> Interfaces { get; set; } = new();
    public List<string> UsingDirectives { get; set; } = new();
    public List<string> ClassAttributes { get; set; } = new();
    public List<DeconstructorActionModel> Actions { get; set; } = new();
    public List<DeconstructorPropertyModel> Properties { get; set; } = new();
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