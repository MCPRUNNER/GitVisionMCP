namespace GitVisionMCP.Models;
/// <summary>
/// Represents a controller property
/// </summary>
public class DeconstructorPropertyModel
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