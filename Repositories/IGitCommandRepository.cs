using System.Threading;
using System.Threading.Tasks;
using GitVisionMCP.Models;
namespace GitVisionMCP.Services;

public interface IGitCommandRepository
{
    Task<List<GitCommitInfo>> GetGitLogsAsync(string repositoryPath, int maxCommits = 50);
    Task<List<GitCommitInfo>> GetGitLogsBetweenBranchesAsync(string repositoryPath, string branch1, string branch2);
    Task<List<GitCommitInfo>> GetGitLogsBetweenCommitsAsync(string repositoryPath, string commit1, string commit2);
    Task<bool> WriteDocumentationToFileAsync(string content, string filePath);

    // New methods for enhanced git operations
    Task<List<GitCommitInfo>> GetRecentCommitsAsync(string repositoryPath, int count = 10);
    Task<List<string>> GetChangedFilesBetweenCommitsAsync(string repositoryPath, string commit1, string commit2);
    Task<string> GetDetailedDiffBetweenCommitsAsync(string repositoryPath, string commit1, string commit2, List<string>? specificFiles = null);
    Task<GitCommitDiffInfo> GetCommitDiffInfoAsync(string repositoryPath, string commit1, string commit2);
    Task<FileLineDiffInfo> GetFileLineDiffBetweenCommitsAsync(string repositoryPath, string commit1, string commit2, string filePath);
    Task<List<ConflictResult>> FindAllGitConflictMarkers(IEnumerable<FileContentInfo> fileList);
    Task<ConflictResult> ReadGitConflictMarkers(FileContentInfo file);
    // New methods for remote branch support
    Task<List<string>> GetLocalBranchesAsync(string repositoryPath);
    Task<List<string>> GetRemoteBranchesAsync(string repositoryPath);
    Task<List<string>> GetAllBranchesAsync(string repositoryPath);
    Task<string> GetCurrentBranchAsync(string repositoryPath);
    Task<bool> FetchFromRemoteAsync(string repositoryPath, string remoteName = "origin");
    Task<List<GitCommitInfo>> GetGitLogsBetweenBranchesWithRemoteAsync(string repositoryPath, string branch1, string branch2, bool fetchRemote = true);
    Task<string> GenerateCommitDocumentationAsync(List<GitCommitInfo> commits, string format = "markdown");
    // New method for searching commits
    Task<CommitSearchResponse> SearchCommitsForStringAsync(string repositoryPath, string searchString, int maxCommits = 100);
}