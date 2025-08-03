namespace GitVisionMCP.Models;

public class GitFileChange
{
    public string Path { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int AddedLines
    {
        get; set;
    }
    public int DeletedLines
    {
        get; set;
    }
}