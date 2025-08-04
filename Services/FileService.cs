using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using GitVisionMCP.Models;
using GitVisionMCP.Repositories;

namespace GitVisionMCP.Services;

/// <summary>
/// Implementation of file service that provides file operations and workspace management
/// </summary>
public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly IFileRepository _fileRepository;

    private DateTime _lastExcludeConfigLoad = DateTime.MinValue;

    public FileService(ILogger<FileService> logger, IFileRepository fileRepository)
    {
        _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Determines the workspace root directory by checking environment variable first
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    private string DetermineWorkspaceRoot()
    {
        return _fileRepository.DetermineWorkspaceRoot();
    }

    /// <summary>
    /// Gets the workspace root directory
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    public string GetWorkspaceRoot()
    {
        return _fileRepository.GetWorkspaceRoot();
    }

    /// <summary>
    /// Gets the full path of a file based on its relative path within the workspace root directory.
    /// This method resolves the relative path in the workspace root to provide the absolute path.
    /// </summary>
    /// <param name="relativePath">The relative path to resolve</param>
    /// <returns>The full path to the file, or null if the file doesn't exist</returns>
    public string? GetFullPath(string relativePath)
    {
        return _fileRepository.GetFullPath(relativePath);
    }

    /// <summary>
    /// Reads the contents of a file
    /// </summary>
    /// <param name="filePath">The path to the file to read</param>
    /// <returns>The content of the file as a string, or null if the file doesn't exist or an error occurs</returns>
    public string? ReadFile(string filePath)
    {
        return _fileRepository.ReadFile(filePath);

    }

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types
    /// </summary>
    /// <returns>A list of file information including relative path and file type</returns>
    public List<WorkspaceFileInfo> GetAllFiles()
    {
        return _fileRepository.GetAllFiles();
    }

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types, excluding files matching exclude patterns
    /// </summary>
    /// <returns>A list of file information including relative path and file type, excluding excluded files</returns>
    public async Task<List<WorkspaceFileInfo>> GetAllFilesAsync()
    {
        return await _fileRepository.GetAllFilesAsync();
    }

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types that match a specific search pattern.
    /// </summary>
    /// <param name="searchPattern">The search pattern to match files against</param>
    /// <returns>A list of file information for files matching the pattern</returns>
    public List<WorkspaceFileInfo> GetAllFilesMatching(string searchPattern)
    {
        return _fileRepository.GetAllFilesMatching(searchPattern);
    }

    /// <summary>
    /// Saves the output of GetAllFiles() to an XML file
    /// </summary>
    /// <param name="xmlFilePath">The path where the XML file will be saved</param>
    /// <returns>True if the file was saved successfully, false otherwise</returns>
    public bool SaveAllFilesToXml(string xmlFilePath)
    {
        return _fileRepository.SaveAllFilesToXml(xmlFilePath);
    }

    /// <summary>
    /// Gets the contents of files in the workspace based on a list of WorkspaceFileInfo.
    /// This method retrieves the contents of files specified in the provided list. 
    /// </summary>
    /// <param name="workspaceFileList">List of workspace files to read</param>
    /// <returns>The List of FileContentInfo, or null if not found</returns>
    public Task<List<FileContentInfo>> GetFileContentsAsync(List<WorkspaceFileInfo> workspaceFileList)
    {
        return _fileRepository.GetFileContentsAsync(workspaceFileList);
    }

    /// <summary>
    /// Loads the exclude configuration from .gitvision/exclude.json
    /// </summary>
    /// <returns>The exclude configuration or null if not found or invalid</returns>
    private async Task<ExcludeConfiguration?> LoadExcludeConfigurationAsync()
    {
        return await _fileRepository.LoadExcludeConfigurationAsync();

    }

    /// <summary>
    /// Matches a path against a glob pattern
    /// </summary>
    /// <param name="path">The path to check</param>
    /// <param name="pattern">The glob pattern</param>
    /// <returns>True if the path matches the pattern</returns>
    public bool IsPathMatchingPattern(string path, string pattern)
    {
        return _fileRepository.IsPathMatchingPattern(path, pattern);

    }

    /// <summary>
    /// Checks if a file path should be excluded based on the exclude patterns
    /// </summary>
    /// <param name="relativePath">The relative path to check</param>
    /// <returns>True if the file should be excluded, false otherwise</returns>
    public async Task<bool> IsFileExcludedAsync(string relativePath)
    {
        return await _fileRepository.IsFileExcludedAsync(relativePath);
    }
}
