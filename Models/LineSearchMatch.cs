namespace GitVisionMCP.Models;

public class LineSearchMatch
{
    public int LineNumber
    {
        get; set;
    }
    public string LineContent { get; set; } = string.Empty;
    public string SearchString { get; set; } = string.Empty;
}