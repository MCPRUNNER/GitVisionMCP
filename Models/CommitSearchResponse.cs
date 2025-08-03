namespace GitVisionMCP.Models;

public class CommitSearchResponse
{
    public string SearchString { get; set; } = string.Empty;
    public int TotalCommitsSearched
    {
        get; set;
    }
    public int TotalMatchingCommits => Results.Count;
    public int TotalLineMatches => Results.Sum(r => r.TotalMatches);
    public List<CommitSearchResult> Results { get; set; } = new();
    public string ErrorMessage { get; set; } = string.Empty;
}