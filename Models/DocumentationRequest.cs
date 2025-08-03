namespace GitVisionMCP.Models;

public class DocumentationRequest
{
    public string? FilePath
    {
        get; set;
    }
    public string? Branch1
    {
        get; set;
    }
    public string? Branch2
    {
        get; set;
    }
    public string? Commit1
    {
        get; set;
    }
    public string? Commit2
    {
        get; set;
    }
    public int? MaxCommits
    {
        get; set;
    }
    public string? OutputFormat
    {
        get; set;
    }
}