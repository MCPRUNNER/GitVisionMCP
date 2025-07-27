using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using GitVisionMCP.Services;
using GitVisionMCP.Models;
using System.ComponentModel;
using Microsoft.Extensions.AI;
using Newtonsoft.Json;
using YamlDotNet.Core;
namespace GitVisionMCP.Tools;

/// <summary>
/// Git-related tools implementation using ModelContextProtocol attributes
/// </summary>
[McpServerToolType]
public class GitServiceTools : IGitServiceTools
{
    private readonly IGitService _gitService;
    private readonly ILocationService _locationService;
    private readonly IDeconstructionService _deconstructionService;
    private readonly ILogger<GitServiceTools> _logger;

    public GitServiceTools(IGitService gitService, ILocationService locationService, IDeconstructionService deconstructionService, ILogger<GitServiceTools> logger)
    {
        _gitService = gitService;
        _locationService = locationService;
        _deconstructionService = deconstructionService;
        _logger = logger;
    }

    [McpServerToolAttribute]
    [Description("Fetch latest changes from remote repository")]
    public async Task<bool> FetchFromRemoteAsync(
        [Description("Name of the remote (default: origin)")] string? remoteName = "origin")
    {
        try
        {
            var repoPath = _locationService.GetWorkspaceRoot();
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
            // Validate inputs
            outputFormat = ValidateOutputFormat(outputFormat);
            var commitCount = maxCommits ?? 50;
            if (commitCount <= 0)
            {
                _logger.LogWarning("Invalid commit count {CommitCount}, defaulting to 50", commitCount);
                commitCount = 50;
            }

            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Generating git documentation for last {CommitCount} commits in {Format} format",
                commitCount, outputFormat);

            var commits = await _gitService.GetGitLogsAsync(repoPath, commitCount);
            return await _gitService.GenerateDocumentationAsync(commits, outputFormat);
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "Repository directory not found");
            return $"Error: Repository directory not found. Please ensure you're in a valid git repository.";
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error accessing git repository");
            return $"Error: Unable to access git repository: {ex.Message}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating git documentation for {CommitCount} commits in {Format} format",
                maxCommits ?? 50, outputFormat ?? "markdown");
            return $"Error generating documentation: {ex.Message}. Please check the logs for more details.";
        }
    }

    /// <summary>
    /// Ensures the directory exists for the specified file path
    /// </summary>
    /// <param name="filePath">The file path to check</param>
    private void EnsureDirectoryExists(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            _logger.LogInformation("Creating directory {Directory}", directory);
            Directory.CreateDirectory(directory);
        }
    }

    /// <summary>
    /// Validates and normalizes the output format
    /// </summary>
    /// <param name="outputFormat">The output format to validate</param>
    /// <returns>Validated output format</returns>
    private string ValidateOutputFormat(string? outputFormat)
    {
        outputFormat = outputFormat?.ToLowerInvariant() ?? "markdown";

        if (outputFormat != "markdown" && outputFormat != "html" && outputFormat != "text")
        {
            _logger.LogWarning("Invalid output format {OutputFormat}, using default (markdown)", outputFormat);
            return "markdown";
        }

        return outputFormat;
    }

    [McpServerToolAttribute]
    [Description("Generate documentation from git logs and write to a file")]
    public async Task<string> GenerateGitDocumentationToFileAsync(
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Maximum number of commits to include (default: 50)")] int? maxCommits = 50,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        // Validate file path
        if (string.IsNullOrWhiteSpace(filePath))
        {
            _logger.LogError("File path cannot be null or empty");
            return "Error: File path must be specified";
        }

        try
        {
            // Ensure directory exists
            EnsureDirectoryExists(filePath);

            // Use the helper method for output format validation
            outputFormat = ValidateOutputFormat(outputFormat);
            var commitCount = maxCommits ?? 50;

            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Generating git documentation to file {FilePath} for last {CommitCount} commits",
                filePath, commitCount);

            var commits = await _gitService.GetGitLogsAsync(repoPath, commitCount);
            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat);

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);

            if (success)
            {
                _logger.LogInformation("Documentation successfully written to {FilePath}", filePath);
                return $"Documentation successfully written to {filePath}";
            }
            else
            {
                _logger.LogWarning("Failed to write documentation to {FilePath}", filePath);
                return $"Failed to write documentation to {filePath}. Please check file permissions.";
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied when writing to {FilePath}", filePath);
            return $"Error: Access denied when writing to {filePath}. Please check file permissions.";
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "Directory not found for {FilePath}", filePath);
            return $"Error: Directory not found for {filePath}";
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "IO error when writing to {FilePath}", filePath);
            return $"Error: File system error when writing to {filePath}: {ex.Message}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating git documentation to file {FilePath}", filePath);
            return $"Error generating documentation: {ex.Message}. Please check the logs for more details.";
        }
    }
    /*
        [McpServerTool(Name = "Sampling"), Description("Run Sampling chat to process predefined User and System prompts")]
        public async Task<string> SamplingLLM(
               ModelContextProtocol.Server.IMcpServer thisServer,
               CancellationToken cancellationToken,
               [Description("User Prompt")] string user_prompt = "Sampling_USR.md",
               [Description("System Prompt")] string system_prompt = "Sampling_SYS.md",
               [Description("Model")] string model = "gpt-4.1",
               [Description("Maximum number of tokens to generate")] int maxTokens = 1000)
        {
            // Create messages list with system prompt and user message
            var sysPrompt = _locationService.GetGitHubPromptFileContent(system_prompt);
            var userPrompt = _locationService.GetGitHubPromptFileContent(user_prompt);
            var messages = new List<ChatMessage>
            {
                new(ChatRole.System, sysPrompt ?? "Default system prompt"),
                new(ChatRole.User, userPrompt ?? "Default user prompt")
            };

            var options = new ChatOptions
            {
                MaxOutputTokens = maxTokens,
                Temperature = 0.7f,
                ModelId = model, // Specify the model to use
            };

            var samplingResponse = await thisServer.AsSamplingChatClient().GetResponseAsync(messages, options, cancellationToken);
            //  return "LLM sampling result: " + prompt; //return $"LLM sampling result: {samplingResponse}";
            return $"LLM sampling result: {samplingResponse}";
        }
    */


    [McpServerToolAttribute]
    [Description("Generate documentation comparing differences between two branches")]
    public async Task<string> CompareBranchesDocumentationAsync(
        [Description("First branch name")] string branch1,
        [Description("Second branch name")] string branch2,
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(branch1))
        {
            _logger.LogError("First branch name cannot be null or empty");
            return "Error: First branch name must be specified";
        }

        if (string.IsNullOrWhiteSpace(branch2))
        {
            _logger.LogError("Second branch name cannot be null or empty");
            return "Error: Second branch name must be specified";
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            _logger.LogError("File path cannot be null or empty");
            return "Error: File path must be specified";
        }

        // Validate output format
        outputFormat = ValidateOutputFormat(outputFormat);

        try
        {
            // Ensure directory exists
            EnsureDirectoryExists(filePath);

            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Comparing branches {Branch1} and {Branch2}, writing to {FilePath}",
                branch1, branch2, filePath);

            var commits = await _gitService.GetGitLogsBetweenBranchesAsync(repoPath, branch1, branch2);

            if (commits == null || !commits.Any())
            {
                _logger.LogWarning("No commits found between branches {Branch1} and {Branch2}", branch1, branch2);
                return $"No differences found between branches {branch1} and {branch2}";
            }

            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat);

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);

            if (success)
            {
                _logger.LogInformation("Branch comparison documentation successfully written to {FilePath}", filePath);
                return $"Branch comparison documentation successfully written to {filePath}";
            }
            else
            {
                _logger.LogWarning("Failed to write branch comparison documentation to {FilePath}", filePath);
                return $"Failed to write branch comparison to {filePath}. Please check file permissions.";
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied when writing to {FilePath}", filePath);
            return $"Error: Access denied when writing to {filePath}. Please check file permissions.";
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "Directory not found for {FilePath}", filePath);
            return $"Error: Directory not found for {filePath}";
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "IO error when writing to {FilePath}", filePath);
            return $"Error: File system error when writing to {filePath}: {ex.Message}";
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error comparing branches {Branch1} and {Branch2}", branch1, branch2);
            return $"Error: {ex.Message}. Please ensure both branches exist.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating branch comparison documentation");
            return $"Error generating branch comparison: {ex.Message}. Please check the logs for more details.";
        }
    }

    [McpServerToolAttribute]
    [Description("Generate documentation comparing differences between two branches with remote support")]
    public async Task<string> CompareBranchesWithRemote(
        [Description("First branch name (can be local or remote, e.g., 'main' or 'origin/main')")] string branch1,
        [Description("Second branch name (can be local or remote, e.g., 'feature/xyz' or 'origin/feature/xyz')")] string branch2,
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Whether to fetch from remote before comparison (default: true)")] bool fetchRemote = true,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(branch1))
        {
            _logger.LogError("First branch name cannot be null or empty");
            return "Error: First branch name must be specified";
        }

        if (string.IsNullOrWhiteSpace(branch2))
        {
            _logger.LogError("Second branch name cannot be null or empty");
            return "Error: Second branch name must be specified";
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            _logger.LogError("File path cannot be null or empty");
            return "Error: File path must be specified";
        }

        // Validate output format
        outputFormat = ValidateOutputFormat(outputFormat);

        try
        {
            // Ensure directory exists
            EnsureDirectoryExists(filePath);

            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Comparing branches {Branch1} and {Branch2} with remote support, writing to {FilePath}",
                branch1, branch2, filePath);

            if (fetchRemote)
            {
                _logger.LogInformation("Fetching from remote before comparison");
                await _gitService.FetchFromRemoteAsync(repoPath);
            }

            var commits = await _gitService.GetGitLogsBetweenBranchesAsync(repoPath, branch1, branch2);

            if (commits == null || !commits.Any())
            {
                _logger.LogWarning("No commits found between branches {Branch1} and {Branch2}", branch1, branch2);
                return $"No differences found between branches {branch1} and {branch2}";
            }

            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat);

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);

            if (success)
            {
                _logger.LogInformation("Branch comparison documentation successfully written to {FilePath}", filePath);
                return $"Branch comparison documentation successfully written to {filePath}";
            }
            else
            {
                _logger.LogWarning("Failed to write branch comparison documentation to {FilePath}", filePath);
                return $"Failed to write branch comparison to {filePath}. Please check file permissions.";
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied when writing to {FilePath}", filePath);
            return $"Error: Access denied when writing to {filePath}. Please check file permissions.";
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "Directory not found for {FilePath}", filePath);
            return $"Error: Directory not found for {filePath}";
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "IO error when writing to {FilePath}", filePath);
            return $"Error: File system error when writing to {filePath}: {ex.Message}";
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error comparing branches {Branch1} and {Branch2}", branch1, branch2);
            return $"Error: {ex.Message}. Please ensure both branches exist.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating branch comparison documentation with remote support");
            return $"Error generating branch comparison: {ex.Message}. Please check the logs for more details.";
        }
    }

    [McpServerToolAttribute]
    [Description("Generate documentation comparing differences between two commits")]
    public async Task<string> CompareCommitsDocumentation(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2,
        [Description("Path where to save the documentation file")] string filePath,
        [Description("Output format: markdown, html, or text (default: markdown)")] string? outputFormat = "markdown")
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(commit1))
        {
            _logger.LogError("First commit hash cannot be null or empty");
            return "Error: First commit hash must be specified";
        }

        if (string.IsNullOrWhiteSpace(commit2))
        {
            _logger.LogError("Second commit hash cannot be null or empty");
            return "Error: Second commit hash must be specified";
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            _logger.LogError("File path cannot be null or empty");
            return "Error: File path must be specified";
        }

        // Validate output format
        outputFormat = ValidateOutputFormat(outputFormat);

        try
        {
            // Ensure directory exists
            EnsureDirectoryExists(filePath);

            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Comparing commits {Commit1} and {Commit2}, writing to {FilePath}",
                commit1, commit2, filePath);

            var commits = await _gitService.GetGitLogsBetweenCommitsAsync(repoPath, commit1, commit2);

            if (commits == null || !commits.Any())
            {
                _logger.LogWarning("No differences found between commits {Commit1} and {Commit2}", commit1, commit2);
                return $"No differences found between commits {commit1} and {commit2}";
            }

            var documentation = await _gitService.GenerateDocumentationAsync(commits, outputFormat);

            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);

            if (success)
            {
                _logger.LogInformation("Commit comparison documentation successfully written to {FilePath}", filePath);
                return $"Commit comparison documentation successfully written to {filePath}";
            }
            else
            {
                _logger.LogWarning("Failed to write commit comparison documentation to {FilePath}", filePath);
                return $"Failed to write commit comparison to {filePath}. Please check file permissions.";
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied when writing to {FilePath}", filePath);
            return $"Error: Access denied when writing to {filePath}. Please check file permissions.";
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "Directory not found for {FilePath}", filePath);
            return $"Error: Directory not found for {filePath}";
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "IO error when writing to {FilePath}", filePath);
            return $"Error: File system error when writing to {filePath}: {ex.Message}";
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid commit hash provided: {Commit1} or {Commit2}", commit1, commit2);
            return $"Error: Invalid commit hash provided: {ex.Message}";
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error comparing commits {Commit1} and {Commit2}", commit1, commit2);
            return $"Error: {ex.Message}. Please ensure both commit hashes exist.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating commit comparison documentation");
            return $"Error generating commit comparison: {ex.Message}. Please check the logs for more details.";
        }
    }

    [McpServerToolAttribute]
    [Description("Get recent commits from the current repository")]
    public async Task<List<GitCommitInfo>> GetRecentCommitsAsync(
        [Description("Number of recent commits to retrieve (default: 10)")] int? count = 10)
    {
        try
        {
            var repoPath = _locationService.GetWorkspaceRoot();
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
            var repoPath = _locationService.GetWorkspaceRoot();
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
            var repoPath = _locationService.GetWorkspaceRoot();
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
            var repoPath = _locationService.GetWorkspaceRoot();
            return await _gitService.GetAllBranchesAsync(repoPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all branches");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get the current active branch in the repository")]
    public async Task<string> GetCurrentBranchAsync()
    {
        try
        {
            var repoPath = _locationService.GetWorkspaceRoot();
            return await _gitService.GetCurrentBranchAsync(repoPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current branch");
            throw;
        }
    }

    [McpServerToolAttribute]
    [Description("Get list of files changed between two commits")]
    public async Task<List<string>> GetChangedFilesBetweenCommits(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(commit1))
        {
            _logger.LogError("First commit hash cannot be null or empty");
            throw new ArgumentException("First commit hash must be specified", nameof(commit1));
        }

        if (string.IsNullOrWhiteSpace(commit2))
        {
            _logger.LogError("Second commit hash cannot be null or empty");
            throw new ArgumentException("Second commit hash must be specified", nameof(commit2));
        }

        try
        {
            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Getting changed files between commits {Commit1} and {Commit2}",
                commit1, commit2);

            var changedFiles = await _gitService.GetChangedFilesBetweenCommitsAsync(repoPath, commit1, commit2);

            if (changedFiles == null || !changedFiles.Any())
            {
                _logger.LogInformation("No files changed between commits {Commit1} and {Commit2}", commit1, commit2);
                return new List<string>();
            }

            return changedFiles;
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid commit hash provided: {Commit1} or {Commit2}", commit1, commit2);
            throw;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error getting changed files between commits {Commit1} and {Commit2}", commit1, commit2);
            throw new InvalidOperationException($"Error getting changed files: {ex.Message}. Please ensure both commit hashes exist.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting changed files between commits");
            throw new InvalidOperationException("Failed to get changed files between commits. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Get comprehensive diff information between two commits including file changes and statistics")]
    public async Task<GitCommitDiffInfo> GetCommitDiffInfo(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(commit1))
        {
            _logger.LogError("First commit hash cannot be null or empty");
            throw new ArgumentException("First commit hash must be specified", nameof(commit1));
        }

        if (string.IsNullOrWhiteSpace(commit2))
        {
            _logger.LogError("Second commit hash cannot be null or empty");
            throw new ArgumentException("Second commit hash must be specified", nameof(commit2));
        }

        try
        {
            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Getting diff info between commits {Commit1} and {Commit2}",
                commit1, commit2);

            var diffInfo = await _gitService.GetCommitDiffInfoAsync(repoPath, commit1, commit2);

            if (diffInfo == null)
            {
                _logger.LogInformation("No differences found between commits {Commit1} and {Commit2}", commit1, commit2);
                return new GitCommitDiffInfo
                {
                    Commit1 = commit1,
                    Commit2 = commit2,
                    AddedFiles = new List<string>(),
                    ModifiedFiles = new List<string>(),
                    DeletedFiles = new List<string>(),
                    RenamedFiles = new List<string>(),
                    DetailedDiff = "No changes found between commits"
                };
            }

            return diffInfo;
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid commit hash provided: {Commit1} or {Commit2}", commit1, commit2);
            throw;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error getting diff info between commits {Commit1} and {Commit2}", commit1, commit2);
            throw new InvalidOperationException($"Error getting diff info: {ex.Message}. Please ensure both commit hashes exist.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting diff info between commits");
            throw new InvalidOperationException("Failed to get diff info between commits. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Get detailed diff content between two commits")]
    public async Task<string> GetDetailedDiffBetweenCommits(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2,
        [Description("Optional: specific files to diff")] List<string>? specificFiles = null)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(commit1))
        {
            _logger.LogError("First commit hash cannot be null or empty");
            throw new ArgumentException("First commit hash must be specified", nameof(commit1));
        }

        if (string.IsNullOrWhiteSpace(commit2))
        {
            _logger.LogError("Second commit hash cannot be null or empty");
            throw new ArgumentException("Second commit hash must be specified", nameof(commit2));
        }

        try
        {
            var repoPath = _locationService.GetWorkspaceRoot();

            if (specificFiles != null && specificFiles.Count > 0)
            {
                _logger.LogInformation("Getting detailed diff between commits {Commit1} and {Commit2} for {FileCount} specific files",
                    commit1, commit2, specificFiles.Count);

                // Validate that file paths are not empty
                specificFiles = specificFiles
                    .Where(f => !string.IsNullOrWhiteSpace(f))
                    .ToList();

                if (specificFiles.Count == 0)
                {
                    _logger.LogWarning("All provided file paths were empty or null");
                    throw new ArgumentException("All provided file paths were empty or null");
                }
            }
            else
            {
                _logger.LogInformation("Getting detailed diff between commits {Commit1} and {Commit2} for all files",
                    commit1, commit2);
            }

            var diff = await _gitService.GetDetailedDiffBetweenCommitsAsync(repoPath, commit1, commit2, specificFiles);

            if (string.IsNullOrWhiteSpace(diff))
            {
                _logger.LogInformation("No differences found between commits {Commit1} and {Commit2}", commit1, commit2);
                return "No changes found between commits";
            }

            return diff;
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument: {Message}", ex.Message);
            throw;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error getting detailed diff between commits {Commit1} and {Commit2}", commit1, commit2);
            throw new InvalidOperationException($"Error getting detailed diff: {ex.Message}. Please ensure both commit hashes exist.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting detailed diff between commits");
            throw new InvalidOperationException("Failed to get detailed diff between commits. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Search all commits for a specific string and return commit details, filenames, and line matches")]
    public async Task<CommitSearchResponse> SearchCommitsForStringAsync(
        [Description("The string to search for in commit messages and file contents")] string searchString,
        [Description("Maximum number of commits to search through (default: 100)")] int? maxCommits = 100)
    {
        // Validate search string
        if (string.IsNullOrWhiteSpace(searchString))
        {
            _logger.LogError("Search string cannot be null or empty");
            throw new ArgumentException("Search string cannot be null or empty", nameof(searchString));
        }

        // Validate commit count
        var commitCount = maxCommits ?? 100;
        if (commitCount <= 0)
        {
            _logger.LogWarning("Invalid commit count {CommitCount}, defaulting to 100", commitCount);
            commitCount = 100;
        }

        try
        {
            _logger.LogInformation("Searching for '{SearchString}' in last {CommitCount} commits",
                searchString, commitCount);

            var repoPath = _locationService.GetWorkspaceRoot();

            // Check if repository exists
            if (!Directory.Exists(Path.Combine(repoPath, ".git")))
            {
                _logger.LogError("Not a valid git repository at {RepoPath}", repoPath);
                throw new InvalidOperationException($"Not a valid git repository at {repoPath}");
            }

            // Add timeout protection
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
            try
            {
                // Since the original method doesn't accept a cancellation token,
                // we'll use a task with timeout
                var task = _gitService.SearchCommitsForStringAsync(repoPath, searchString, commitCount);
                if (await Task.WhenAny(task, Task.Delay(TimeSpan.FromMinutes(5), cancellationTokenSource.Token)) != task)
                {
                    _logger.LogError("Search operation timed out after 5 minutes");
                    throw new TimeoutException("Search operation timed out after 5 minutes. Try reducing the number of commits to search.");
                }

                return await task;
            }
            catch (OperationCanceledException)
            {
                _logger.LogError("Search operation timed out after 5 minutes");
                throw new TimeoutException("Search operation timed out after 5 minutes. Try reducing the number of commits to search.");
            }
        }
        catch (Exception ex) when (ex is not ArgumentException and not InvalidOperationException and not TimeoutException)
        {
            _logger.LogError(ex, "Error searching for '{SearchString}' in commits", searchString);
            throw new Exception($"Error searching commits: {ex.Message}", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Get line-by-line file diff between two commits")]
    public async Task<FileLineDiffInfo> GetFileLineDiffBetweenCommits(
        [Description("First commit hash")] string commit1,
        [Description("Second commit hash")] string commit2,
        [Description("Path to the file to diff")] string filePath)
    {
        // Validate input parameters
        if (string.IsNullOrWhiteSpace(commit1))
        {
            _logger.LogError("First commit hash cannot be null or empty");
            throw new ArgumentException("First commit hash must be specified", nameof(commit1));
        }

        if (string.IsNullOrWhiteSpace(commit2))
        {
            _logger.LogError("Second commit hash cannot be null or empty");
            throw new ArgumentException("Second commit hash must be specified", nameof(commit2));
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            _logger.LogError("File path cannot be null or empty");
            throw new ArgumentException("File path must be specified", nameof(filePath));
        }

        try
        {
            var repoPath = _locationService.GetWorkspaceRoot();
            _logger.LogInformation("Getting line-by-line diff for file {FilePath} between commits {Commit1} and {Commit2}",
                filePath, commit1, commit2);

            var diff = await _gitService.GetFileLineDiffBetweenCommitsAsync(repoPath, commit1, commit2, filePath);

            if (diff == null || (!string.IsNullOrEmpty(diff.ErrorMessage) && diff.Lines.Count == 0))
            {
                _logger.LogInformation("No differences found for file {FilePath} between commits {Commit1} and {Commit2}",
                    filePath, commit1, commit2);
                return new FileLineDiffInfo
                {
                    Commit1 = commit1,
                    Commit2 = commit2,
                    FilePath = filePath,
                    ErrorMessage = $"No changes found for file {filePath} between commits {commit1} and {commit2}"
                };
            }

            return diff;
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument: {Message}", ex.Message);
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "File not found: {FilePath}", filePath);
            throw new FileNotFoundException($"File {filePath} not found in one or both commits", filePath, ex);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error getting file line diff between commits {Commit1} and {Commit2}", commit1, commit2);
            throw new InvalidOperationException($"Error getting file line diff: {ex.Message}. Please ensure both commit hashes exist and the file exists in one or both commits.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting file line diff between commits");
            throw new InvalidOperationException("Failed to get file line diff between commits. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("List all files in the workspace with optional filtering")]
    public async Task<List<WorkspaceFileInfo>> ListWorkspaceFilesAsync(
        [Description("Filter by file type (extension without dot, e.g., 'cs', 'json')")] string? fileType = null,
        [Description("Filter by relative path (contains search)")] string? relativePath = null,
        [Description("Filter by full path (contains search)")] string? fullPath = null,
        [Description("Filter by last modified date (ISO format: yyyy-MM-dd)")] string? lastModifiedAfter = null,
        [Description("Filter by last modified date (ISO format: yyyy-MM-dd)")] string? lastModifiedBefore = null)
    {
        try
        {
            var allFiles = await _locationService.GetAllFilesAsync();
            var filteredFiles = allFiles.AsEnumerable();

            // Apply file type filter
            if (!string.IsNullOrWhiteSpace(fileType))
            {
                filteredFiles = filteredFiles.Where(f =>
                    f.FileType.Equals(fileType, StringComparison.OrdinalIgnoreCase));
            }

            // Apply relative path filter
            if (!string.IsNullOrWhiteSpace(relativePath))
            {
                filteredFiles = filteredFiles.Where(f =>
                    f.RelativePath.Contains(relativePath, StringComparison.OrdinalIgnoreCase));
            }

            // Apply full path filter
            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                filteredFiles = filteredFiles.Where(f =>
                    f.FullPath.Contains(fullPath, StringComparison.OrdinalIgnoreCase));
            }

            // Apply last modified after filter
            if (!string.IsNullOrWhiteSpace(lastModifiedAfter) && DateTime.TryParse(lastModifiedAfter, out var afterDate))
            {
                filteredFiles = filteredFiles.Where(f => f.LastModified >= afterDate);
            }

            // Apply last modified before filter
            if (!string.IsNullOrWhiteSpace(lastModifiedBefore) && DateTime.TryParse(lastModifiedBefore, out var beforeDate))
            {
                filteredFiles = filteredFiles.Where(f => f.LastModified < beforeDate);
            }

            var result = filteredFiles.ToList();
            _logger.LogInformation("Listed {FilteredCount} files out of {TotalCount} total files", result.Count, allFiles.Count);

            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing workspace files");
            throw new InvalidOperationException("Failed to list workspace files. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("List workspace files with optional filtering using pre-fetched file data to improve performance")]
    public async Task<List<WorkspaceFileInfo>> ListWorkspaceFilesWithCachedDataAsync(
        List<WorkspaceFileInfo> cachedFiles,
        [Description("Filter by file type (extension without dot, e.g., 'cs', 'json')")] string? fileType = null,
        [Description("Filter by relative path (contains search)")] string? relativePath = null,
        [Description("Filter by full path (contains search)")] string? fullPath = null,
        [Description("Filter by last modified date (ISO format: yyyy-MM-dd)")] string? lastModifiedAfter = null,
        [Description("Filter by last modified date (ISO format: yyyy-MM-dd)")] string? lastModifiedBefore = null)
    {
        try
        {
            var filteredFiles = cachedFiles.AsEnumerable();

            // Apply file type filter
            if (!string.IsNullOrWhiteSpace(fileType))
            {
                filteredFiles = filteredFiles.Where(f =>
                    f.FileType.Equals(fileType, StringComparison.OrdinalIgnoreCase));
            }

            // Apply relative path filter
            if (!string.IsNullOrWhiteSpace(relativePath))
            {
                filteredFiles = filteredFiles.Where(f =>
                    f.RelativePath.Contains(relativePath, StringComparison.OrdinalIgnoreCase));
            }

            // Apply full path filter
            if (!string.IsNullOrWhiteSpace(fullPath))
            {
                filteredFiles = filteredFiles.Where(f =>
                    f.FullPath.Contains(fullPath, StringComparison.OrdinalIgnoreCase));
            }

            // Apply last modified after filter
            if (!string.IsNullOrWhiteSpace(lastModifiedAfter) && DateTime.TryParse(lastModifiedAfter, out var afterDate))
            {
                filteredFiles = filteredFiles.Where(f => f.LastModified >= afterDate);
            }

            // Apply last modified before filter
            if (!string.IsNullOrWhiteSpace(lastModifiedBefore) && DateTime.TryParse(lastModifiedBefore, out var beforeDate))
            {
                filteredFiles = filteredFiles.Where(f => f.LastModified <= beforeDate.AddDays(1).AddTicks(-1));
            }

            var result = filteredFiles.ToList();
            _logger.LogInformation("Listed {FilteredCount} files using cached data with total {TotalCount} files",
                result.Count, cachedFiles.Count);

            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing workspace files with cached data");
            throw new InvalidOperationException("Failed to list workspace files with cached data. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Read contents of all files from filtered workspace results")]
    public async Task<List<Models.FileContentInfo>> ReadFilteredWorkspaceFilesAsync(
        [Description("Filter by file type (extension without dot, e.g., 'cs', 'json')")] string? fileType = null,
        [Description("Filter by relative path (contains search)")] string? relativePath = null,
        [Description("Filter by full path (contains search)")] string? fullPath = null,
        [Description("Filter by last modified date (ISO format: yyyy-MM-dd)")] string? lastModifiedAfter = null,
        [Description("Filter by last modified date (ISO format: yyyy-MM-dd)")] string? lastModifiedBefore = null,
        [Description("Maximum number of files to read (default: 500, max: 1000)")] int? maxFiles = 500,
        [Description("Maximum file size to read in bytes (default: 1MB)")] long? maxFileSize = 1048576)
    {
        try
        {
            // Validate limits to prevent memory issues
            var fileLimit = Math.Min(maxFiles ?? 500, 1000);
            var sizeLimit = Math.Min(maxFileSize ?? 1048576, 10485760); // Max 10MB per file

            _logger.LogInformation("Reading filtered workspace files with limits: {FileLimit} files, {SizeLimit} bytes per file",
                fileLimit, sizeLimit);

            var filteredFiles = await ListWorkspaceFilesAsync(fileType, relativePath, fullPath, lastModifiedAfter, lastModifiedBefore);

            // Limit number of files
            filteredFiles = filteredFiles.Take(fileLimit).ToList();

            var result = new List<Models.FileContentInfo>();

            foreach (var file in filteredFiles)
            {
                try
                {
                    var fileInfo = new FileInfo(file.FullPath);

                    // Create FileContentInfo with basic properties
                    var fileContentInfo = new Models.FileContentInfo
                    {
                        RelativePath = file.RelativePath,
                        FileType = file.FileType,
                        FullPath = file.FullPath,
                        Size = file.Size,
                        LastModified = file.LastModified
                    };

                    // Skip files that are too large - check size from file info we have
                    // Note: In test scenarios, the file might not exist but we still need to check its size
                    if (file.Size > sizeLimit)
                    {
                        fileContentInfo.IsError = true;
                        fileContentInfo.ErrorMessage = $"File exceeds maximum allowed size of {sizeLimit} bytes";
                        result.Add(fileContentInfo);
                        continue;
                    }

                    // Check if file exists
                    if (!File.Exists(file.FullPath))
                    {
                        fileContentInfo.IsError = true;
                        fileContentInfo.ErrorMessage = "File not found";
                        result.Add(fileContentInfo);
                        continue;
                    }

                    // Skip binary files
                    if (IsBinaryFile(file.FullPath))
                    {
                        fileContentInfo.IsError = true;
                        fileContentInfo.ErrorMessage = "Binary file not supported";
                        result.Add(fileContentInfo);
                        continue;
                    }

                    // Read content if file exists and meets criteria
                    try
                    {
                        var content = _locationService.ReadFile(file.FullPath);
                        // var content = await File.ReadAllTextAsync(file.FullPath);
                        fileContentInfo.Content = content;
                        result.Add(fileContentInfo);
                    }
                    catch (Exception ex)
                    {
                        fileContentInfo.IsError = true;
                        fileContentInfo.ErrorMessage = $"Error reading file: {ex.Message}";
                        result.Add(fileContentInfo);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error processing file FullPath {FilePath} RelativePath {RelativePath}", file.FullPath, file.RelativePath);
                    result.Add(new Models.FileContentInfo
                    {
                        RelativePath = file.RelativePath,
                        FileType = file.FileType,
                        FullPath = file.FullPath,
                        Size = file.Size,
                        LastModified = file.LastModified,
                        IsError = true,
                        ErrorMessage = $"Error processing file: {ex.Message}"
                    });
                }
            }

            _logger.LogInformation("Read {ReadCount} files out of {FilteredCount} filtered files",
                result.Count, filteredFiles.Count);

            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading filtered workspace files");
            throw new InvalidOperationException("Failed to read filtered workspace files. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Search for JSON values in a JSON file using JSONPath")]
    public Task<string?> SearchJsonFileAsync(
        [Description("Path to the JSON file relative to workspace root")] string jsonFilePath,
        [Description("JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')")] string jsonPath,
        [Description("Whether to format the output with indentation (default: true)")] bool? indented = true,
        [Description("Whether to return structured results with path, value, and key information (default: false)")] bool? showKeyPaths = false)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                _logger.LogError("JSON file path cannot be null or empty");
                throw new ArgumentException("JSON file path must be specified", nameof(jsonFilePath));
            }

            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                _logger.LogError("JSONPath cannot be null or empty");
                throw new ArgumentException("JSONPath must be specified", nameof(jsonPath));
            }

            _logger.LogInformation("Searching JSON file {JsonFilePath} with JSONPath {JsonPath}, showKeyPaths: {ShowKeyPaths}",
                jsonFilePath, jsonPath, showKeyPaths ?? false);

            var result = _locationService.SearchJsonFile(jsonFilePath, jsonPath, indented ?? true, showKeyPaths ?? false);

            if (string.IsNullOrEmpty(result))
            {
                _logger.LogInformation("No matches found for JSONPath {JsonPath} in file {JsonFilePath}",
                    jsonPath, jsonFilePath);
                return Task.FromResult<string?>("No matches found");
            }

            _logger.LogInformation("Successfully found matches for JSONPath {JsonPath} in file {JsonFilePath}",
                jsonPath, jsonFilePath);
            return Task.FromResult<string?>(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument for JSON search");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "JSON file not found: {JsonFilePath}", jsonFilePath);
            throw new FileNotFoundException($"JSON file not found: {jsonFilePath}", jsonFilePath, ex);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Invalid JSON format in file: {JsonFilePath}", jsonFilePath);
            throw new InvalidDataException($"Invalid JSON format in file: {jsonFilePath}. Details: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching JSON file {JsonFilePath}", jsonFilePath);
            throw new InvalidOperationException($"Error searching JSON file: {ex.Message}. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Search for XML values in an XML file using XPath")]
    public Task<string?> SearchXmlFileAsync(
        [Description("Path to the XML file relative to workspace root")] string xmlFilePath,
        [Description("XPath query string (e.g., '//users/user/@email', '/configuration/database/host')")] string xPath,
        [Description("Whether to format the output with indentation (default: true)")] bool? indented = true,
        [Description("Whether to return structured results with path, value, and key information (default: false)")] bool? showKeyPaths = false)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(xmlFilePath))
            {
                _logger.LogError("XML file path cannot be null or empty");
                throw new ArgumentException("XML file path must be specified", nameof(xmlFilePath));
            }

            if (string.IsNullOrWhiteSpace(xPath))
            {
                _logger.LogError("XPath cannot be null or empty");
                throw new ArgumentException("XPath must be specified", nameof(xPath));
            }

            _logger.LogInformation("Searching XML file {XmlFilePath} with XPath {XPath}, showKeyPaths: {ShowKeyPaths}",
                xmlFilePath, xPath, showKeyPaths ?? false);

            var result = _locationService.SearchXmlFile(xmlFilePath, xPath, indented ?? true, showKeyPaths ?? false);

            if (string.IsNullOrEmpty(result))
            {
                _logger.LogInformation("No matches found for XPath {XPath} in file {XmlFilePath}",
                    xPath, xmlFilePath);
                return Task.FromResult<string?>("No matches found");
            }

            _logger.LogInformation("Successfully found matches for XPath {XPath} in file {XmlFilePath}",
                xPath, xmlFilePath);
            return Task.FromResult<string?>(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument for XML search");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "XML file not found: {XmlFilePath}", xmlFilePath);
            throw new FileNotFoundException($"XML file not found: {xmlFilePath}. Please check the file path.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            _logger.LogError(ex, "Invalid XML format in file {XmlFilePath}", xmlFilePath);
            throw new InvalidDataException($"Invalid XML format in file: {xmlFilePath}. Details: {ex.Message}", ex);
        }
        catch (System.Xml.XPath.XPathException ex)
        {
            _logger.LogError(ex, "Invalid XPath expression: {XPath}", xPath);
            throw new InvalidDataException($"Invalid XPath expression: {xPath}. Details: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching XML file {XmlFilePath}", xmlFilePath);
            throw new InvalidOperationException($"Error searching XML file: {ex.Message}. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Transform an XML file using an XSLT stylesheet")]
    public Task<string?> TransformXmlWithXsltAsync(
        [Description("Path to the XML file relative to workspace root")] string xmlFilePath,
        [Description("Path to the XSLT stylesheet file relative to workspace root")] string xsltFilePath)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(xmlFilePath))
            {
                _logger.LogError("XML file path cannot be null or empty");
                throw new ArgumentException("XML file path must be specified", nameof(xmlFilePath));
            }

            if (string.IsNullOrWhiteSpace(xsltFilePath))
            {
                _logger.LogError("XSLT file path cannot be null or empty");
                throw new ArgumentException("XSLT file path must be specified", nameof(xsltFilePath));
            }

            _logger.LogInformation("Transforming XML file {XmlFilePath} with XSLT {XsltFilePath}",
                xmlFilePath, xsltFilePath);

            var result = _locationService.TransformXmlWithXslt(xmlFilePath, xsltFilePath);

            if (string.IsNullOrEmpty(result))
            {
                _logger.LogError("XSLT transformation failed for XML file {XmlFilePath} with XSLT {XsltFilePath}",
                    xmlFilePath, xsltFilePath);
                return Task.FromResult<string?>("XSLT transformation failed");
            }

            _logger.LogInformation("Successfully transformed XML file {XmlFilePath} with XSLT {XsltFilePath}",
                xmlFilePath, xsltFilePath);
            return Task.FromResult<string?>(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument for XSLT transformation");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "File not found during XSLT transformation");
            throw new FileNotFoundException($"File not found during XSLT transformation. Please check the file paths. Details: {ex.Message}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            _logger.LogError(ex, "Invalid XML format during XSLT transformation");
            throw new InvalidDataException($"Invalid XML format during XSLT transformation. Details: {ex.Message}", ex);
        }
        catch (System.Xml.Xsl.XsltException ex)
        {
            _logger.LogError(ex, "XSLT transformation error");
            throw new InvalidDataException($"XSLT transformation error. Details: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during XSLT transformation");
            throw new InvalidOperationException($"Error during XSLT transformation: {ex.Message}. See inner exception for details.", ex);
        }
    }
    // string? SearchCsvFile(string csvFilePath, string jsonPath, bool hasHeaderRecord = true, bool ignoreBlankLines = true)
    [McpServerToolAttribute]
    [Description("Search for CSV values in a CSV file using JSONPath")]
    public Task<string?> SearchCsvFileAsync(
        [Description("Path to the CSV file relative to workspace root")] string csvFilePath,
        [Description("JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')")] string jsonPath,
        [Description("Whether the CSV has headers (default: true)")] bool? hasHeaderRecord = true,
        [Description("Whether to ignore blank lines (default: false)")] bool? ignoreBlankLines = true)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(csvFilePath))
            {
                _logger.LogError("CSV file path cannot be null or empty");
                throw new ArgumentException("CSV file path must be specified", nameof(csvFilePath));
            }

            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                _logger.LogError("JSONPath cannot be null or empty");
                throw new ArgumentException("JSONPath must be specified", nameof(jsonPath));
            }

            _logger.LogInformation("Searching CSV file {CsvFilePath} with JSONPath {JsonPath}, hasHeaderRecord: {HasHeaderRecord}, ignoreBlankLines: {IgnoreBlankLines}",
                csvFilePath, jsonPath, hasHeaderRecord ?? true, ignoreBlankLines ?? true);

            var result = _locationService.SearchCsvFile(csvFilePath, jsonPath, hasHeaderRecord ?? true, ignoreBlankLines ?? true);

            if (string.IsNullOrEmpty(result))
            {
                _logger.LogInformation("No matches found for JSONPath {JsonPath} in CSV file {CsvFilePath}",
                    jsonPath, csvFilePath);
                return Task.FromResult<string?>("No matches found");
            }

            _logger.LogInformation("Successfully found matches for JSONPath {JsonPath} in CSV file {CsvFilePath}",
                jsonPath, csvFilePath);
            return Task.FromResult<string?>(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument for CSV search");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "CSV file not found: {CsvFilePath}", csvFilePath);
            throw new FileNotFoundException($"CSV file not found: {csvFilePath}. Please check the file path.", ex);
        }
        catch (Newtonsoft.Json.JsonException ex)
        {
            _logger.LogError(ex, "Invalid JSON format in file {JsonFilePath}", csvFilePath);
            throw new InvalidDataException($"Invalid JSON format in file: {csvFilePath}. Details: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching CSV file {CsvFilePath}", csvFilePath);
            throw new InvalidOperationException($"Error searching CSV file: {ex.Message}. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Search for YAML values in a YAML file using JSONPath")]
    public Task<string?> SearchYamlFileAsync(
        [Description("Path to the YAML file relative to workspace root")] string yamlFilePath,
        [Description("JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')")] string jsonPath,
        [Description("Whether to format the output with indentation (default: true)")] bool? indented = true,
        [Description("Whether to return structured results with path, value, and key information (default: false)")] bool? showKeyPaths = false)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(yamlFilePath))
            {
                _logger.LogError("YAML file path cannot be null or empty");
                throw new ArgumentException("YAML file path must be specified", nameof(yamlFilePath));
            }

            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                _logger.LogError("JSONPath cannot be null or empty");
                throw new ArgumentException("JSONPath must be specified", nameof(jsonPath));
            }

            _logger.LogInformation("Searching YAML file {YamlFilePath} with JSONPath {JsonPath}, showKeyPaths: {ShowKeyPaths}",
                yamlFilePath, jsonPath, showKeyPaths ?? false);

            var result = _locationService.SearchYamlFile(yamlFilePath, jsonPath, indented ?? true, showKeyPaths ?? false);

            if (string.IsNullOrEmpty(result))
            {
                _logger.LogInformation("No matches found for JSONPath {JsonPath} in YAML file {YamlFilePath}",
                    jsonPath, yamlFilePath);
                return Task.FromResult<string?>("No matches found");
            }

            _logger.LogInformation("Successfully found matches for JSONPath {JsonPath} in YAML file {YamlFilePath}",
                jsonPath, yamlFilePath);
            return Task.FromResult<string?>(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument for YAML search");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "YAML file not found: {YamlFilePath}", yamlFilePath);
            throw new FileNotFoundException($"YAML file not found: {yamlFilePath}. Please check the file path.", ex);
        }
        catch (YamlDotNet.Core.YamlException ex)
        {
            _logger.LogError(ex, "Invalid YAML format in file {YamlFilePath}", yamlFilePath);
            throw new InvalidDataException($"Invalid YAML format in file: {yamlFilePath}. Details: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching YAML file {YamlFilePath}", yamlFilePath);
            throw new InvalidOperationException($"Error searching YAML file: {ex.Message}. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Deconstruct a C# Service, Repository or Controller file and returns its structure as JSON")]
    public Task<string?> DeconstructAsync(
        [Description("Path to the c# file relative to workspace root")] string filePath)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogError("Controller file path cannot be null or empty");
                throw new ArgumentException("Controller file path must be specified", nameof(filePath));
            }

            _logger.LogInformation("Deconstructing file: {FilePath}", filePath);

            var result = _deconstructionService.Deconstruct(filePath);

            if (string.IsNullOrEmpty(result))
            {
                _logger.LogWarning("Failed to Deconstruct file: {FilePath}", filePath);
                return Task.FromResult<string?>("Failed to Deconstruct file");
            }

            _logger.LogInformation("Successfully Deconstructed ile: {FilePath}", filePath);
            return Task.FromResult<string?>(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument for Deconstruct");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "Deconstruct source file not found: {FilePath}", filePath);
            throw new FileNotFoundException($"Deconstruct file not found: {filePath}. Please check the file path.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Deconstructing file {FilePath}", filePath);
            throw new InvalidOperationException($"Error Deconstruct file: {ex.Message}. See inner exception for details.", ex);
        }
    }

    [McpServerToolAttribute]
    [Description("Deconstruct a C# Service, Repository or Controller file and saves the structure to a JSON file in the workspace directory")]
    public Task<string?> DeconstructToJsonAsync(
        [Description("Path to the source file relative to workspace root")] string filePath,
        [Description("The name of the output JSON file (optional, defaults to controller name + '_analysis.json')")] string? outputFileName = null)
    {
        try
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogError("Deconstruct file path cannot be null or empty");
                throw new ArgumentException("Deconstruct file path must be specified", nameof(filePath));
            }

            _logger.LogInformation("Deconstructing file to save: {FilePath}, Output: {OutputFileName}",
                filePath, outputFileName ?? "auto-generated");

            var outputPath = _deconstructionService.DeconstructToFile(filePath, outputFileName);

            if (string.IsNullOrEmpty(outputPath))
            {
                _logger.LogWarning("Failed to Deconstruct and save file: {FilePath}", filePath);
                return Task.FromResult<string?>("Failed to Deconstruct and save file");
            }

            _logger.LogInformation("Successfully Deconstruct and saved file to: {OutputPath}", outputPath);
            return Task.FromResult<string?>($"Deconstruct analysis saved to: {outputPath}");
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument for Deconstruct analysis to file");
            throw;
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "Deconstruct file not found: {FilePath}", filePath);
            throw new FileNotFoundException($"Deconstruct file not found: {filePath}. Please check the file path.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Deconstruct file to file {FilePath}", filePath);
            throw new InvalidOperationException($"Error Deconstruct file to file: {ex.Message}. See inner exception for details.", ex);
        }
    }

    private bool IsBinaryFile(string filePath)
    {
        try
        {
            // Check if file is likely binary by examining first few bytes
            var buffer = new byte[512];
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (stream.Length == 0)
                return false; // Empty file

            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0)
                return false;

            // Count null bytes and other control characters
            var nullCount = 0;
            var controlCount = 0;

            for (var i = 0; i < bytesRead; i++)
            {
                if (buffer[i] == 0)
                    nullCount++;

                // Control characters except common ones like CR, LF, tab
                if (buffer[i] < 8 || (buffer[i] > 13 && buffer[i] < 32))
                    controlCount++;
            }

            // If more than 10% of the content is null bytes or control characters,
            // consider it a binary file
            if ((double)nullCount / bytesRead > 0.1 || (double)controlCount / bytesRead > 0.3)
                return true;

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error checking if file is binary: {FilePath}", filePath);
            return true; // Safer to assume binary on error
        }
    }
}
