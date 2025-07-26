
using GitVisionMCP.Models;
using GitVisionMCP.Services;
namespace GitVisionMCP.Models;
/// <summary>
/// Represents a controller action method
/// </summary>
public class DeconstructorActionModel
{
    public string Name { get; set; } = string.Empty;
    public string ReturnType { get; set; } = string.Empty;
    public string Accessibility { get; set; } = string.Empty;
    public List<string> Attributes { get; set; } = new();
    public List<DeconstructionActionParameterModel> Parameters { get; set; } = new();
    public string HttpMethod { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public bool IsAsync
    {
        get; set;
    }
}
