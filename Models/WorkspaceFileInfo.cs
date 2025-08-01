namespace GitVisionMCP.Models;
/// <summary>
/// Represents file information with relative path and metadata
/// </summary>
public class WorkspaceFileInfo
{
    /// <summary>
    /// Gets or sets the relative path from the workspace root
    /// </summary>
    public string RelativePath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file type (extension without the dot)
    /// </summary>
    public string FileType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the full path to the file
    /// </summary>
    public string FullPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file size in bytes
    /// </summary>
    public long Size
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the last modified date
    /// </summary>
    public DateTime LastModified
    {
        get; set;
    }
}
