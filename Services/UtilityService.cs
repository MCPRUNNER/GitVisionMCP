using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GitVisionMCP.Services;
using Microsoft.Extensions.Logging;

namespace GitVisionMCP.Services;
public class UtilityService : IUtilityService
{
    private readonly ILogger<UtilityService> _logger;
    private readonly IFileService _fileService;
    private readonly IWorkspaceService _workspaceService;

    public UtilityService(ILogger<UtilityService> logger, IFileService fileService, IWorkspaceService workspaceService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _workspaceService = workspaceService ?? throw new ArgumentNullException(nameof(workspaceService));
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

            var version = _workspaceService.SearchXmlFile(projectFile, "/Project/PropertyGroup/Version/text()");
            return version;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting application version");
            return "Unknown Version";
        }
    }
}
