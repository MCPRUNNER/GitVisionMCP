namespace GitVisionMCP.Models;
public class LineDiff
{
    public int LineNumber
    {
        get; set;
    }
    public string OldLineNumber { get; set; } = string.Empty;
    public string NewLineNumber { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Added, Deleted, Context, Modified
}