namespace GitVisionMCP.Models;
public class CommitSearchResult
{
    public string CommitHash { get; set; } = string.Empty;
    public string CommitMessage { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime Timestamp
    {
        get; set;
    }
    public List<FileSearchMatch> FileMatches { get; set; } = new();
    public int TotalMatches => FileMatches.Sum(f => f.LineMatches.Count);
}