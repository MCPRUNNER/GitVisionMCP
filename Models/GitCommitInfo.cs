namespace GitVisionMCP.Models;

public class GitCommitInfo
{
    public string Hash { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public DateTime Date
    {
        get; set;
    }
    public List<string> ChangedFiles { get; set; } = new();
    public List<string> Changes { get; set; } = new();
}