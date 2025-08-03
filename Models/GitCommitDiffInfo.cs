namespace GitVisionMCP.Models;

public class GitCommitDiffInfo
{
    public string Commit1 { get; set; } = string.Empty;
    public string Commit2 { get; set; } = string.Empty;
    public List<string> AddedFiles { get; set; } = new();
    public List<string> ModifiedFiles { get; set; } = new();
    public List<string> DeletedFiles { get; set; } = new();
    public List<string> RenamedFiles { get; set; } = new();
    public int TotalChanges => AddedFiles.Count + ModifiedFiles.Count + DeletedFiles.Count + RenamedFiles.Count;
    public string DetailedDiff { get; set; } = string.Empty;
}
