using GitVisionMCP.Models;

namespace GitVisionMCP.Repositories;

public interface IFileRepository
{
    List<WorkspaceFileInfo> GetAllFiles();
    Task<List<WorkspaceFileInfo>> GetAllFilesAsync();
    Task<List<WorkspaceFileInfo>> GetAllFilesMatching(string searchPattern);
    Task<List<FileContentInfo>> GetFileContentsAsync(List<WorkspaceFileInfo> workspaceFileList);
    string? GetFullPath(string relativePath);
    string GetWorkspaceRoot();
    Task<bool> IsFileExcludedAsync(string relativePath);

    string? ReadFile(string filePath);
    bool SaveAllFilesToXml(string xmlFilePath);
    bool IsPathMatchingPattern(string path, string pattern);
    string DetermineWorkspaceRoot();
    Task<ExcludeConfiguration?> LoadExcludeConfigurationAsync();
}