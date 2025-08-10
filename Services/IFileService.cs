using GitVisionMCP.Models;

namespace GitVisionMCP.Services;

/// <summary>
/// Interface for file operations and workspace management
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Gets the workspace root directory
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    string GetWorkspaceRoot();

    /// <summary>
    /// Gets the full path of a file based on its relative path within the workspace root directory.
    /// This method resolves the relative path in the workspace root to provide the absolute path.
    /// </summary>
    /// <param name="relativePath">The relative path to resolve</param>
    /// <returns>The full path to the file, or null if the file doesn't exist</returns>
    string? GetFullPath(string relativePath);

    /// <summary>
    /// Reads the contents of a file
    /// </summary>
    /// <param name="filePath">The path to the file to read</param>
    /// <returns>The content of the file as a string, or null if the file doesn't exist or an error occurs</returns>
    string? ReadFile(string filePath);

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types
    /// </summary>
    /// <returns>A list of file information including relative path and file type</returns>
    List<WorkspaceFileInfo> GetAllFiles();

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types, excluding files matching exclude patterns
    /// </summary>
    /// <returns>A list of file information including relative path and file type, excluding excluded files</returns>
    Task<List<WorkspaceFileInfo>> GetAllFilesAsync();

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types that match a specific search pattern.
    /// </summary>
    /// <param name="searchPattern">The search pattern to match files against</param>
    /// <returns>A list of file information for files matching the pattern</returns>
    Task<List<WorkspaceFileInfo>> GetAllFilesMatching(string searchPattern);

    /// <summary>
    /// Saves the output of GetAllFiles() to an XML file
    /// </summary>
    /// <param name="xmlFilePath">The path where the XML file will be saved</param>
    /// <returns>True if the file was saved successfully, false otherwise</returns>
    bool SaveAllFilesToXml(string xmlFilePath);

    /// <summary>
    /// Gets the contents of files in the workspace based on a list of WorkspaceFileInfo.
    /// This method retrieves the contents of files specified in the provided list. 
    /// </summary>
    /// <param name="workspaceFileList">List of workspace files to read</param>
    /// <returns>The List of FileContentInfo, or null if not found</returns>
    Task<List<FileContentInfo>> GetFileContentsAsync(List<WorkspaceFileInfo> workspaceFileList);

    /// <summary>
    /// Checks if a file path should be excluded based on the exclude patterns
    /// </summary>
    /// <param name="relativePath">The relative path to check</param>
    /// <returns>True if the file should be excluded, false otherwise</returns>
    Task<bool> IsFileExcludedAsync(string relativePath);
}
