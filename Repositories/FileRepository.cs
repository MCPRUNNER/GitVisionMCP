using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using GitVisionMCP.Models;

namespace GitVisionMCP.Repositories;

/// <summary>
/// Implementation of file service that provides file operations and workspace management
/// </summary>
public class FileRepository : IFileRepository
{
    private readonly ILogger<FileRepository> _logger;
    private readonly string _workspaceRoot;
    private ExcludeConfiguration? _excludeConfiguration;
    private DateTime _lastExcludeConfigLoad = DateTime.MinValue;

    public FileRepository(ILogger<FileRepository> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _workspaceRoot = DetermineWorkspaceRoot();
    }

    /// <summary>
    /// Determines the workspace root directory by checking environment variable first
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    public string DetermineWorkspaceRoot()
    {
        var gitRepositoryDirectory = Environment.GetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY");

        if (!string.IsNullOrWhiteSpace(gitRepositoryDirectory))
        {
            if (Directory.Exists(gitRepositoryDirectory))
            {
                _logger.LogInformation("Using GIT_REPOSITORY_DIRECTORY environment variable: {GitRepositoryDirectory}", gitRepositoryDirectory);
                return gitRepositoryDirectory;
            }
            else
            {
                _logger.LogWarning("GIT_REPOSITORY_DIRECTORY environment variable is set but directory does not exist: {GitRepositoryDirectory}. Falling back to current directory.", gitRepositoryDirectory);
            }
        }

        var currentDirectory = Environment.CurrentDirectory;
        _logger.LogInformation("Using current directory as workspace root: {CurrentDirectory}", currentDirectory);
        return currentDirectory;
    }

    /// <summary>
    /// Gets the workspace root directory
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    public string GetWorkspaceRoot()
    {
        return _workspaceRoot;
    }

    /// <summary>
    /// Gets the full path of a file based on its relative path within the workspace root directory.
    /// This method resolves the relative path in the workspace root to provide the absolute path.
    /// </summary>
    /// <param name="relativePath">The relative path to resolve</param>
    /// <returns>The full path to the file, or null if the file doesn't exist</returns>
    public string? GetFullPath(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            _logger.LogError("GetFileFullPath: Filename cannot be null or empty");
            return null;
        }

        try
        {
            string fullPath;

            // If path is already absolute, use it as-is
            if (Path.IsPathRooted(relativePath))
            {
                fullPath = relativePath;
            }
            else
            {
                // If relative, combine with workspace root
                fullPath = Path.Combine(_workspaceRoot, relativePath);
            }

            if (!File.Exists(fullPath))
            {
                _logger.LogInformation("GetFileFullPath: file not found at: {FullPath}", fullPath);
                return null;
            }
            return fullPath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetFileFullPath: Error processing file path: {RelativePath}", relativePath);
            return null;
        }
    }

    /// <summary>
    /// Reads the contents of a file
    /// </summary>
    /// <param name="filePath">The path to the file to read</param>
    /// <returns>The content of the file as a string, or null if the file doesn't exist or an error occurs</returns>
    public string? ReadFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            _logger.LogError("Filename cannot be null or empty");
            return null;
        }

        try
        {
            var fullPath = GetFullPath(filePath);
            if (string.IsNullOrEmpty(fullPath))
            {
                _logger.LogWarning("ReadFile: file does not exist: {FilePath}", filePath);
                return null;
            }

            var content = File.ReadAllText(fullPath);
            _logger.LogInformation("Successfully read file: {FilePath}", filePath);

            return content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ReadFile: Error reading file: {Filename}", filePath);
            return null;
        }
    }

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types
    /// </summary>
    /// <returns>A list of file information including relative path and file type</returns>
    public List<WorkspaceFileInfo> GetAllFiles()
    {
        var files = new List<WorkspaceFileInfo>();

        try
        {
            var workspaceRoot = new DirectoryInfo(_workspaceRoot);
            var allFiles = workspaceRoot.GetFiles("*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                var relativePath = Path.GetRelativePath(_workspaceRoot, file.FullName);
                var fileType = Path.GetExtension(file.Name).ToLowerInvariant();

                files.Add(new WorkspaceFileInfo
                {
                    RelativePath = relativePath,
                    FileType = string.IsNullOrEmpty(fileType) ? "no extension" : fileType.TrimStart('.'),
                    FullPath = file.FullName,
                    Size = file.Length,
                    LastModified = file.LastWriteTime
                });
            }

            _logger.LogInformation("Retrieved {FileCount} files from workspace root: {WorkspaceRoot}", files.Count, _workspaceRoot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving files from workspace root: {WorkspaceRoot}", _workspaceRoot);
        }

        return files;
    }

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types, excluding files matching exclude patterns
    /// </summary>
    /// <returns>A list of file information including relative path and file type, excluding excluded files</returns>
    public async Task<List<WorkspaceFileInfo>> GetAllFilesAsync()
    {
        var files = new List<WorkspaceFileInfo>();

        try
        {
            var workspaceRoot = new DirectoryInfo(_workspaceRoot);
            var allFiles = workspaceRoot.GetFiles("*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                var relativePath = Path.GetRelativePath(_workspaceRoot, file.FullName);

                // Check if file should be excluded
                if (await IsFileExcludedAsync(relativePath))
                {
                    continue;
                }

                var fileType = Path.GetExtension(file.Name).ToLowerInvariant();

                files.Add(new WorkspaceFileInfo
                {
                    RelativePath = relativePath,
                    FileType = string.IsNullOrEmpty(fileType) ? "no extension" : fileType.TrimStart('.'),
                    FullPath = file.FullName,
                    Size = file.Length,
                    LastModified = file.LastWriteTime
                });
            }

            _logger.LogInformation("Retrieved {FileCount} files from workspace root (after exclusions): {WorkspaceRoot}", files.Count, _workspaceRoot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving files from workspace root: {WorkspaceRoot}", _workspaceRoot);
        }

        return files;
    }

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types that match a specific search pattern.
    /// </summary>
    /// <param name="searchPattern">The search pattern to match files against</param>
    /// <returns>A list of file information for files matching the pattern</returns>
    public List<WorkspaceFileInfo> GetAllFilesMatching(string searchPattern)
    {
        var files = new List<WorkspaceFileInfo>();

        try
        {
            var workspaceRoot = new DirectoryInfo(_workspaceRoot);
            var allFiles = workspaceRoot.GetFiles(searchPattern, SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                var relativePath = Path.GetRelativePath(_workspaceRoot, file.FullName);
                var fileType = Path.GetExtension(file.Name).ToLowerInvariant();

                files.Add(new WorkspaceFileInfo
                {
                    RelativePath = relativePath,
                    FileType = string.IsNullOrEmpty(fileType) ? "no extension" : fileType.TrimStart('.'),
                    FullPath = file.FullName,
                    Size = file.Length,
                    LastModified = file.LastWriteTime
                });
            }

            _logger.LogInformation("Retrieved {FileCount} files from workspace root: {WorkspaceRoot}", files.Count, _workspaceRoot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving files from workspace root: {WorkspaceRoot}", _workspaceRoot);
        }

        return files;
    }

    /// <summary>
    /// Saves the output of GetAllFiles() to an XML file
    /// </summary>
    /// <param name="xmlFilePath">The path where the XML file will be saved</param>
    /// <returns>True if the file was saved successfully, false otherwise</returns>
    public bool SaveAllFilesToXml(string xmlFilePath)
    {
        if (string.IsNullOrWhiteSpace(xmlFilePath))
        {
            _logger.LogError("XML file path cannot be null or empty");
            return false;
        }

        try
        {
            var files = GetAllFiles();
            var xmlContent = new System.Text.StringBuilder();

            xmlContent.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xmlContent.AppendLine("<WorkspaceFiles>");
            xmlContent.AppendLine($"  <WorkspaceRoot>{System.Security.SecurityElement.Escape(_workspaceRoot)}</WorkspaceRoot>");
            xmlContent.AppendLine($"  <GeneratedAt>{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}</GeneratedAt>");
            xmlContent.AppendLine($"  <FileCount>{files.Count}</FileCount>");
            xmlContent.AppendLine("  <Files>");

            foreach (var file in files)
            {
                xmlContent.AppendLine("    <File>");
                xmlContent.AppendLine($"      <RelativePath>{System.Security.SecurityElement.Escape(file.RelativePath)}</RelativePath>");
                xmlContent.AppendLine($"      <FileType>{System.Security.SecurityElement.Escape(file.FileType)}</FileType>");
                xmlContent.AppendLine($"      <FullPath>{System.Security.SecurityElement.Escape(file.FullPath)}</FullPath>");
                xmlContent.AppendLine($"      <Size>{file.Size}</Size>");
                xmlContent.AppendLine($"      <LastModified>{file.LastModified:yyyy-MM-ddTHH:mm:ssZ}</LastModified>");
                xmlContent.AppendLine("    </File>");
            }

            xmlContent.AppendLine("  </Files>");
            xmlContent.AppendLine("</WorkspaceFiles>");

            // Create directory if it doesn't exist
            var directory = Path.GetDirectoryName(xmlFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(xmlFilePath, xmlContent.ToString());
            _logger.LogInformation("Successfully saved {FileCount} files to XML file: {XmlFilePath}", files.Count, xmlFilePath);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving files to XML file: {XmlFilePath}", xmlFilePath);
            return false;
        }
    }

    /// <summary>
    /// Gets the contents of files in the workspace based on a list of WorkspaceFileInfo.
    /// This method retrieves the contents of files specified in the provided list. 
    /// </summary>
    /// <param name="workspaceFileList">List of workspace files to read</param>
    /// <returns>The List of FileContentInfo, or null if not found</returns>
    public Task<List<FileContentInfo>> GetFileContentsAsync(List<WorkspaceFileInfo> workspaceFileList)
    {
        if (workspaceFileList == null || !workspaceFileList.Any())
        {
            _logger.LogWarning("GetFileContentsAsync: No files provided to read");
            return Task.FromResult(new List<FileContentInfo>());
        }

        var fileContents = new List<FileContentInfo>();

        foreach (var file in workspaceFileList)
        {
            try
            {
                var fullPath = file.FullPath;
                if (string.IsNullOrEmpty(fullPath) || !File.Exists(fullPath))
                {
                    _logger.LogWarning("GetFileContentsAsync: file does not exist: {FilePath}", file.RelativePath);
                    fileContents.Add(new FileContentInfo
                    {
                        RelativePath = file.RelativePath,
                        FileType = file.FileType,
                        IsError = true,
                        ErrorMessage = "File does not exist"
                    });
                    continue;
                }

                var content = ReadFile(fullPath);
                fileContents.Add(new FileContentInfo
                {
                    RelativePath = file.RelativePath,
                    FullPath = file.FullPath,
                    FileType = file.FileType,
                    Content = content,
                    IsError = false
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFileContentsAsync: Error reading file: {FilePath}", file.RelativePath);
                fileContents.Add(new FileContentInfo
                {
                    RelativePath = file.RelativePath,
                    FileType = file.FileType,
                    IsError = true,
                    ErrorMessage = $"Error reading file: {ex.Message}"
                });
            }
        }

        return Task.FromResult(fileContents);
    }

    /// <summary>
    /// Loads the exclude configuration from .gitvision/exclude.json
    /// </summary>
    /// <returns>The exclude configuration or null if not found or invalid</returns>
    public async Task<ExcludeConfiguration?> LoadExcludeConfigurationAsync()
    {
        try
        {
            var excludeConfigPath = Path.Combine(_workspaceRoot, ".gitvision", "exclude.json");
            var fullPath = GetFullPath(excludeConfigPath);
            if (string.IsNullOrEmpty(fullPath))
            {
                _logger.LogInformation("Exclude configuration file not found at: {ExcludeConfigPath}", excludeConfigPath);
                return null;
            }

            var lastWriteTime = File.GetLastWriteTime(fullPath);

            // Check if we need to reload the configuration
            if (_excludeConfiguration != null && _lastExcludeConfigLoad >= lastWriteTime)
            {
                return _excludeConfiguration;
            }

            var jsonContent = await File.ReadAllTextAsync(fullPath);
            var configuration = JsonConvert.DeserializeObject<ExcludeConfiguration>(jsonContent);
            if (configuration == null)
            {
                _logger.LogWarning("Exclude configuration is null after deserialization");
                return null;
            }
            _excludeConfiguration = configuration;
            _lastExcludeConfigLoad = lastWriteTime;

            _logger.LogInformation("Loaded exclude configuration with {PatternCount} patterns from: {ExcludeConfigPath}",
                configuration?.ExcludePatterns?.Count ?? 0, excludeConfigPath);

            return configuration;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading exclude configuration");
            return null;
        }
    }

    /// <summary>
    /// Matches a path against a glob pattern
    /// </summary>
    /// <param name="path">The path to check</param>
    /// <param name="pattern">The glob pattern</param>
    /// <returns>True if the path matches the pattern</returns>
    public bool IsPathMatchingPattern(string path, string pattern)
    {
        // Convert glob pattern to regex
        var regexPattern = "^" + Regex.Escape(pattern)
            .Replace("\\*\\*", ".*")  // ** matches any number of directories
            .Replace("\\*", "[^/]*")  // * matches any characters except directory separator
            .Replace("\\?", ".")      // ? matches any single character
            + "$";

        try
        {
            return Regex.IsMatch(path, regexPattern, RegexOptions.IgnoreCase);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error matching pattern {Pattern} against path {Path}", pattern, path);
            return false;
        }
    }

    /// <summary>
    /// Checks if a file path should be excluded based on the exclude patterns
    /// </summary>
    /// <param name="relativePath">The relative path to check</param>
    /// <returns>True if the file should be excluded, false otherwise</returns>
    public async Task<bool> IsFileExcludedAsync(string relativePath)
    {
        var excludeConfig = await LoadExcludeConfigurationAsync();

        if (excludeConfig?.ExcludePatterns == null || !excludeConfig.ExcludePatterns.Any())
        {
            return false;
        }

        var normalizedPath = relativePath.Replace('\\', '/');

        foreach (var pattern in excludeConfig.ExcludePatterns)
        {
            if (IsPathMatchingPattern(normalizedPath, pattern))
            {
                return true;
            }
        }

        return false;
    }
}
