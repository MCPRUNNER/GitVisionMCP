namespace GitVisionMCP.Models;


public class FileSearchMatch
{
    public string FileName { get; set; } = string.Empty;
    public List<LineSearchMatch> LineMatches { get; set; } = new();
}
