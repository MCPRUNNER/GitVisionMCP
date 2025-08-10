using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using GitVisionMCP.Models;
using GitVisionMCP.Services;
using Microsoft.Extensions.Logging;

namespace GitVisionMCP.Repositories;

public class UtilityRepository : IUtilityRepository
{
    private readonly ILogger<UtilityRepository> _logger;
    private readonly IFileService _fileService;

    public UtilityRepository(ILogger<UtilityRepository> logger, IFileService fileService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    /// <summary>
    /// Gets the value of an environment variable by name and returns it as an object.
    /// </summary>
    /// <param name="variableName">The name of the environment variable</param>
    /// <returns>The value of the environment variable as an object, or null if not set</returns>
    public object? GetEnvironmentVariableValue(string variableName)
    {
        if (string.IsNullOrWhiteSpace(variableName))
        {
            _logger.LogError("Environment variable name cannot be null or empty");
            return null;
        }
        var value = Environment.GetEnvironmentVariable(variableName);
        return value != null ? (object)value : null;
    }
    /// <summary>
    /// Extracts version information from a project file (e.g., .csproj) using XPath. 
    /// Defaults to version of GitVisionMCP if the Project file is not passed.
    /// </summary>
    /// <param name="projectFile"></param>
    /// <returns></returns>
    public string? GetAppVersion(string? projectFile)
    {
        try
        {
            if (string.IsNullOrEmpty(projectFile))
            {
                return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
            }

            // Read the project file directly using IFileService
            // If projectFile is not provided, fall back to the executing assembly version of GitVisionMCP

            var filePath = _fileService.GetFullPath(projectFile);
            if (string.IsNullOrEmpty(filePath))
            {
                _logger.LogWarning("Project file does not exist: {ProjectFile}", projectFile);
                return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
            }

            var xmlContent = _fileService.ReadFile(filePath);
            if (string.IsNullOrWhiteSpace(xmlContent))
            {
                _logger.LogWarning("Project file content is empty: {ProjectFile}", projectFile);
                return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
            }

            // Parse XML and extract version using XPath
            var xmlDoc = XDocument.Parse(xmlContent);
            var versionElement = xmlDoc.XPathSelectElement("/Project/PropertyGroup/Version");

            if (versionElement != null && !string.IsNullOrWhiteSpace(versionElement.Value))
            {
                return versionElement.Value.Trim();
            }

            // Fallback to assembly version
            return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting application version from project file: {ProjectFile}", projectFile);
            return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "Unknown Version";
        }
    }



}
