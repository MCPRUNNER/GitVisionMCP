
using GitVisionMCP.Models;
using GitVisionMCP.Services;
namespace GitVisionMCP.Models;
/// <summary>
/// Represents an action method parameter
/// </summary>
public class DeconstructionActionParameterModel
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