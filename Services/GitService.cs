using Microsoft.Extensions.Logging;
using GitVisionMCP.Models;
namespace GitVisionMCP.Services;



public class GitService : IGitService
{
    private readonly ILogger<GitService> _logger;
    private readonly IWorkspaceService _workspaceService;
    private readonly IGitRepository _gitCommandRepository;


    public GitService(ILogger<GitService> logger, IWorkspaceService workspaceService, IGitRepository gitCommandRepository)
    {
        _logger = logger;
        _workspaceService = workspaceService;
        _gitCommandRepository = gitCommandRepository;
    }
    public async Task<ConflictResult> ReadGitConflictMarkers(FileContentInfo file)
    {
        return await _gitCommandRepository.ReadGitConflictMarkers(file);
    }

    public async Task<List<ConflictResult>> FindAllGitConflictMarkers(IEnumerable<FileContentInfo> fileList)
    {
        return await _gitCommandRepository.FindAllGitConflictMarkers(fileList);
    }
    public async Task<List<GitCommitInfo>> GetGitLogsAsync(string repositoryPath, int maxCommits = 50)
    {
        return await _gitCommandRepository.GetGitLogsAsync(repositoryPath, maxCommits);
    }

    public async Task<List<GitCommitInfo>> GetGitLogsBetweenBranchesAsync(string repositoryPath, string branch1, string branch2)
    {
        return await _gitCommandRepository.GetGitLogsBetweenBranchesAsync(repositoryPath, branch1, branch2);
    }

    public async Task<List<GitCommitInfo>> GetGitLogsBetweenCommitsAsync(string repositoryPath, string commit1, string commit2)
    {
        return await _gitCommandRepository.GetGitLogsBetweenCommitsAsync(repositoryPath, commit1, commit2);
    }
    public async Task<string> GenerateCommitDocumentationAsync(List<GitCommitInfo> commits, string format = "markdown")
    {
        return await _gitCommandRepository.GenerateCommitDocumentationAsync(commits, format);
    }

    public async Task<bool> WriteDocumentationToFileAsync(string content, string filePath)
    {
        return await _gitCommandRepository.WriteDocumentationToFileAsync(content, filePath);
    }

    public async Task<List<GitCommitInfo>> GetRecentCommitsAsync(string repositoryPath, int count = 10)
    {
        return await _gitCommandRepository.GetRecentCommitsAsync(repositoryPath, count);
    }

    public async Task<List<string>> GetChangedFilesBetweenCommitsAsync(string repositoryPath, string commit1, string commit2)
    {
        return await _gitCommandRepository.GetChangedFilesBetweenCommitsAsync(repositoryPath, commit1, commit2);
    }

    public async Task<string> GetDetailedDiffBetweenCommitsAsync(string repositoryPath, string commit1, string commit2, List<string>? specificFiles = null)
    {
        return await _gitCommandRepository.GetDetailedDiffBetweenCommitsAsync(repositoryPath, commit1, commit2, specificFiles);
    }

    public async Task<GitCommitDiffInfo> GetCommitDiffInfoAsync(string repositoryPath, string commit1, string commit2)
    {
        return await _gitCommandRepository.GetCommitDiffInfoAsync(repositoryPath, commit1, commit2);
    }

    public Task<FileLineDiffInfo> GetFileLineDiffBetweenCommitsAsync(string repositoryPath, string commit1, string commit2, string filePath)
    {
        return _gitCommandRepository.GetFileLineDiffBetweenCommitsAsync(repositoryPath, commit1, commit2, filePath);
    }

    // New methods for remote branch support
    public async Task<List<string>> GetLocalBranchesAsync(string repositoryPath)
    {
        return await _gitCommandRepository.GetLocalBranchesAsync(repositoryPath);
    }

    public async Task<List<string>> GetRemoteBranchesAsync(string repositoryPath)
    {
        return await _gitCommandRepository.GetRemoteBranchesAsync(repositoryPath);
    }

    public async Task<List<string>> GetAllBranchesAsync(string repositoryPath)
    {
        return await _gitCommandRepository.GetAllBranchesAsync(repositoryPath);
    }

    public async Task<string> GetCurrentBranchAsync(string repositoryPath)
    {
        return await _gitCommandRepository.GetCurrentBranchAsync(repositoryPath);
    }

    public async Task<bool> FetchFromRemoteAsync(string repositoryPath, string remoteName = "origin")
    {
        return await _gitCommandRepository.FetchFromRemoteAsync(repositoryPath, remoteName);
    }

    public async Task<List<GitCommitInfo>> GetGitLogsBetweenBranchesWithRemoteAsync(string repositoryPath, string branch1, string branch2, bool fetchRemote = true)
    {
        return await _gitCommandRepository.GetGitLogsBetweenBranchesWithRemoteAsync(repositoryPath, branch1, branch2, fetchRemote);
    }

    public async Task<CommitSearchResponse> SearchCommitsForStringAsync(string repositoryPath, string searchString, int maxCommits = 100)
    {
        return await _gitCommandRepository.SearchCommitsForStringAsync(repositoryPath, searchString, maxCommits);
    }
}
