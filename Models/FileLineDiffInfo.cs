namespace GitVisionMCP.Models;
public class FileLineDiffInfo
{
    public string FilePath { get; set; } = string.Empty;
    public string Commit1 { get; set; } = string.Empty;
    public string Commit2 { get; set; } = string.Empty;
    public bool FileExistsInBothCommits
    {
        get; set;
    }
    public int TotalLines
    {
        get; set;
    }
    public int AddedLines
    {
        get; set;
    }
    public int DeletedLines
    {
        get; set;
    }
    public int ModifiedLines
    {
        get; set;
    }
    public List<LineDiff> Lines { get; set; } = new();
    public string ErrorMessage { get; set; } = string.Empty;
}