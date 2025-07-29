using ModelContextProtocol.Server;
using System.ComponentModel;
using GitVisionMCP.Models;
using GitVisionMCP.Services;

namespace GitVisionMCP.Tools;

/// <summary>
/// Interface for Git-related tools implementation using ModelContextProtocol attributes
/// </summary>
public interface IGitServiceTools
{
    /// <summary>
    /// Fetch latest changes from remote repository
    /// </summary>
    Task<bool> FetchFromRemoteAsync(string? remoteName = "origin");

    /// <summary>
    /// Generate documentation from git logs for the current workspace
    /// </summary>
    Task<string> GenerateGitDocumentationAsync(int? maxCommits = 50, string? outputFormat = "markdown");

    /// <summary>
    /// Generate documentation from git logs and write to a file
    /// </summary>
    Task<string> GenerateGitDocumentationToFileAsync(string filePath, int? maxCommits = 50, string? outputFormat = "markdown");

    /// <summary>
    /// Generate documentation comparing differences between two branches
    /// </summary>
    Task<string> CompareBranchesDocumentationAsync(string branch1, string branch2, string filePath, string? outputFormat = "markdown");

    /// <summary>
    /// Generate documentation comparing differences between two branches with remote support
    /// </summary>
    Task<string> CompareBranchesWithRemote(string branch1, string branch2, string filePath, bool fetchRemote = true, string? outputFormat = "markdown");

    /// <summary>
    /// Generate documentation comparing differences between two commits
    /// </summary>
    Task<string> CompareCommitsDocumentation(string commit1, string commit2, string filePath, string? outputFormat = "markdown");

    /// <summary>
    /// Get the current active branch in the repository
    /// </summary>
    Task<string> GetCurrentBranchAsync();

    /// <summary>
    /// Get list of local branches in the repository
    /// </summary>
    Task<List<string>> GetLocalBranchesAsync();

    /// <summary>
    /// Get list of remote branches in the repository
    /// </summary>
    Task<List<string>> GetRemoteBranchesAsync();

    /// <summary>
    /// Get list of all branches (local and remote) in the repository
    /// </summary>
    Task<List<string>> GetAllBranchesAsync();

    /// <summary>
    /// Get recent commits from the current repository
    /// </summary>
    Task<List<Models.GitCommitInfo>> GetRecentCommitsAsync(int? count = 10);

    /// <summary>
    /// Get list of files changed between two commits
    /// </summary>
    Task<List<string>> GetChangedFilesBetweenCommits(string commit1, string commit2);

    /// <summary>
    /// Get comprehensive diff information between two commits including file changes and statistics
    /// </summary>
    Task<Models.GitCommitDiffInfo> GetCommitDiffInfo(string commit1, string commit2);

    /// <summary>
    /// Get detailed diff content between two commits
    /// </summary>
    Task<string> GetDetailedDiffBetweenCommits(string commit1, string commit2, List<string>? specificFiles = null);

    /// <summary>
    /// Get line-by-line file diff between two commits
    /// </summary>
    Task<Models.FileLineDiffInfo> GetFileLineDiffBetweenCommits(string commit1, string commit2, string filePath);

    /// <summary>
    /// Search all commits for a specific string and return commit details, filenames, and line matches
    /// </summary>
    Task<Models.CommitSearchResponse> SearchCommitsForStringAsync(string searchString, int? maxCommits = 100);

    /// <summary>
    /// List all files in the workspace with optional filtering
    /// </summary>
    Task<List<WorkspaceFileInfo>> ListWorkspaceFilesAsync(string? fileType = null, string? relativePath = null, string? fullPath = null, string? lastModifiedAfter = null, string? lastModifiedBefore = null);

    /// <summary>
    /// List workspace files with optional filtering using pre-fetched file data to improve performance
    /// </summary>
    /// <param name="cachedFiles">Pre-fetched list of workspace files</param>
    /// <param name="fileType">Filter by file type (extension without dot, e.g., 'cs', 'json')</param>
    /// <param name="relativePath">Filter by relative path (contains search)</param>
    /// <param name="fullPath">Filter by full path (contains search)</param>
    /// <param name="lastModifiedAfter">Filter by last modified date (ISO format: yyyy-MM-dd)</param>
    /// <param name="lastModifiedBefore">Filter by last modified date (ISO format: yyyy-MM-dd)</param>
    /// <returns>A filtered list of workspace files</returns>
    Task<List<WorkspaceFileInfo>> ListWorkspaceFilesWithCachedDataAsync(
        List<WorkspaceFileInfo> cachedFiles,
        string? fileType = null,
        string? relativePath = null,
        string? fullPath = null,
        string? lastModifiedAfter = null,
        string? lastModifiedBefore = null);

    /// <summary>
    /// Read contents of all files from filtered workspace results
    /// </summary>
    Task<List<Models.FileContentInfo>> ReadFilteredWorkspaceFilesAsync(string? fileType = null, string? relativePath = null, string? fullPath = null, string? lastModifiedAfter = null, string? lastModifiedBefore = null, int? maxFiles = 50, long? maxFileSize = 1048576);

    /// <summary>
    /// Search for JSON values in a JSON file using JSONPath
    /// </summary>
    /// <param name="jsonFilePath">Path to the JSON file relative to workspace root</param>
    /// <param name="jsonPath">JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')</param>
    /// <param name="indented">Whether to format the output with indentation (default: true)</param>
    /// <param name="showKeyPaths">Whether to return structured results with path, value, and key information (default: false)</param>
    /// <returns>JSON search result or null if not found</returns>
    Task<string?> SearchJsonFileAsync(string jsonFilePath, string jsonPath, bool? indented = true, bool? showKeyPaths = false);

    /// <summary>
    /// Search for XML values in an XML file using XPath
    /// </summary>
    /// <param name="xmlFilePath">Path to the XML file relative to workspace root</param>
    /// <param name="xPath">XPath query string (e.g., '//users/user/@email', '/configuration/database/host')</param>
    /// <param name="indented">Whether to format the output with indentation (default: true)</param>
    /// <param name="showKeyPaths">Whether to return structured results with path, value, and key information (default: false)</param>
    /// <returns>XML search result or null if not found</returns>

    Task<string?> SearchXmlFileAsync(string xmlFilePath, string xPath, bool? indented = true, bool? showKeyPaths = false);

    /// <summary>
    /// Search for CSV values in a CSV file using JSONPath queries by converting CSV to JSON
    /// </summary>
    /// <param name="csvFilePath">Path to the CSV file relative to workspace root</param>
    /// <param name="jsonPath">JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')</param>
    /// <param name="hasHeaderRecord">Whether the CSV has a header record (default: true)</param>
    /// <param name="ignoreBlankLines">Whether to ignore blank lines (default: true)</param>
    /// <returns>CSV search result or null if not found</returns>

    Task<string?> SearchCsvFileAsync(string csvFilePath, string jsonPath, bool? hasHeaderRecord = true, bool? ignoreBlankLines = true);

    /// <summary>
    /// Deconstruct a C# Controller, Service or Repository and returns its structure as JSON
    /// </summary>
    /// <param name="filePath">Path to the source code file relative to workspace root</param>
    /// <returns>JSON representation of the source code structure</returns>

    Task<string?> DeconstructAsync(string filePath);

    /// <summary>
    /// Deconstruct a C# Controller, Service or Repository file and saves the structure to a JSON file in the workspace directory
    /// </summary>
    /// <param name="filePath">Path to the source code file relative to workspace root</param>
    /// <param name="outputFileName">The name of the output JSON file (optional, defaults to controller name + '_analysis.json')</param>
    /// <returns>Success message with the output file path</returns>
    Task<string?> DeconstructToJsonAsync(string filePath, string? outputFileName = null);
}
