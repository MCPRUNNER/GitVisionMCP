using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using GitVisionMCP.Services;
using GitVisionMCP.Models;
using System.ComponentModel;

namespace GitVisionMCP.Tools;

/// <summary>
/// Git-related tools implementation using ModelContextProtocol attributes
/// </summary>
[McpServerToolType]
public class GitServiceTools
{
    private readonly IGitService _gitService;
    private readonly ILogger<GitServiceTools> _logger;

    public GitServiceTools(IGitService gitService, ILogger<GitServiceTools> logger)
    {
        _gitService = gitService;
        _logger = logger;
    }

    [McpServerToolAttribute]
    [Description("Fetch latest changes from remote repository")]
    public async Task<bool> FetchFromRemoteAsync(
        [Description("Name of the remote (default: origin)")] string? remoteName = "origin")
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.FetchFromRemoteAsync(repoPath, remoteName ?? "origin");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching from remote");
            return false;
        }
    }

    [McpServerToolAttribute]
    [Description("Generate documentation from git logs for the current workspace")]
    public async Task<string> GenerateGitDocumentationAsync(
        [Description("Maximum number of commits to include (default: 50)")] int? maxCommits = 50,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            var commits = await _gitService.GetGitLogsAsync(repoPath, maxCommits ?? 50);
            return await _gitService.GenerateDocumentationAsync(commits, outputFormat ?? "markdown");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating git documentation");
            return $"Error generating documentation: {ex.Message}";
        }
    }

    [McpServerToolAttribute]
    [Description("Generate documentation from git logs and write to a file")]
    public async Task<string> GenerateGitDocumentationToFileAsync(
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Maximum number of commits to include (default: 50)")] int? maxCommits = 50,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            var commits = await _gitService.GetGitLogsAsync(repoPath, maxCommits ?? 50);
            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat ?? "markdown");

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);
            return success
                ? $"Documentation successfully written to {filePath}"
                : $"Failed to write documentation to {filePath}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating git documentation to file");
            return $"Error generating documentation: {ex.Message}";
        }
    }

    [McpServerToolAttribute]
    [Description("Generate documentation comparing differences between two branches")]
    public async Task<string> CompareBranchesDocumentationAsync(
        [Description("First branch name")] string branch1,
        [Description("Second branch name")] string branch2,
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            var commits = await _gitService.GetGitLogsBetweenBranchesAsync(repoPath, branch1, branch2);
            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat ?? "markdown");

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);
            return success
                ? $"Branch comparison documentation successfully written to {filePath}"
                : $"Failed to write branch comparison to {filePath}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating branch comparison documentation");
            return $"Error generating branch comparison: {ex.Message}";
        }
    }

    [McpServerToolAttribute]
    [Description("Generate documentation comparing differences between two branches with remote support")]
    public async Task<string> CompareBranchesWithRemoteAsync(
        [Description("First branch name (can be local or remote, e.g., 'main' or 'origin/main')")] string branch1,
        [Description("Second branch name (can be local or remote, e.g., 'feature/xyz' or 'origin/feature/xyz')")] string branch2,
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Whether to fetch from remote before comparison (default: true)")] bool? fetchRemote = true,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            var commits = await _gitService.GetGitLogsBetweenBranchesWithRemoteAsync(
                repoPath, branch1, branch2, fetchRemote ?? true);
            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat ?? "markdown");

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);
            return success
                ? $"Branch comparison documentation successfully written to {filePath}"
                : $"Failed to write branch comparison to {filePath}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating branch comparison with remote documentation");
            return $"Error generating branch comparison: {ex.Message}";
        }
    }

    [McpServerToolAttribute]
    [Description("Generate documentation comparing differences between two commits")]
    public async Task<string> CompareCommitsDocumentationAsync(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2,
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            var commits = await _gitService.GetGitLogsBetweenCommitsAsync(repoPath, commit1, commit2);
            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat ?? "markdown");

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);
            return success
                ? $"Commit comparison documentation successfully written to {filePath}"
                : $"Failed to write commit comparison to {filePath}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating commit comparison documentation");
            return $"Error generating commit comparison: {ex.Message}";
        }
    }

    [McpServerToolAttribute]
    [Description("Get recent commits from the current repository")]
    public async Task<List<GitCommitInfo>> GetRecentCommitsAsync(
        [Description("Number of recent commits to retrieve (default: 10)")] int? count = 10)
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetRecentCommitsAsync(repoPath, count ?? 10);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting recent commits");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get list of local branches in the repository")]
    public async Task<List<string>> GetLocalBranchesAsync()
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetLocalBranchesAsync(repoPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting local branches");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get list of remote branches in the repository")]
    public async Task<List<string>> GetRemoteBranchesAsync()
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetRemoteBranchesAsync(repoPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting remote branches");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get list of all branches (local and remote) in the repository")]
    public async Task<List<string>> GetAllBranchesAsync()
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetAllBranchesAsync(repoPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all branches");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get list of files changed between two commits")]
    public async Task<List<string>> GetChangedFilesBetweenCommitsAsync(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2)
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetChangedFilesBetweenCommitsAsync(repoPath, commit1, commit2);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting changed files between commits");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get comprehensive diff information between two commits including file changes and statistics")]
    public async Task<GitCommitDiffInfo> GetCommitDiffInfoAsync(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2)
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetCommitDiffInfoAsync(repoPath, commit1, commit2);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting commit diff info");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get detailed diff content between two commits")]
    public async Task<string> GetDetailedDiffBetweenCommitsAsync(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2,
        [Description("Optional: specific files to diff")] List<string>? specificFiles = null)
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetDetailedDiffBetweenCommitsAsync(repoPath, commit1, commit2, specificFiles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting detailed diff between commits");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get line-by-line file diff between two commits")]
    public async Task<FileLineDiffInfo> GetFileLineDiffBetweenCommitsAsync(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2,
        [Description("Path to the file to diff")] string filePath)
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.GetFileLineDiffBetweenCommitsAsync(repoPath, commit1, commit2, filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting file line diff between commits");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Search all commits for a specific string and return commit details, filenames, and line matches")]
    public async Task<CommitSearchResponse> SearchCommitsForStringAsync(
        [Description("The string to search for in commit messages and file contents")] string searchString,
        [Description("Maximum number of commits to search through (default: 100)")] int? maxCommits = 100)
    {
        try
        {
            var repoPath = Directory.GetCurrentDirectory();
            return await _gitService.SearchCommitsForStringAsync(repoPath, searchString, maxCommits ?? 100);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching commits for string");
            throw;
        }
    }
}
