using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using GitVisionMCP.Models;
using GitVisionMCP.Services;
namespace GitVisionMCP.Configuration;


public class ConfigLoader : IConfigLoader
{
    private readonly ILogger<ConfigLoader> _logger;
    private readonly IFileService _fileService;


    public ConfigLoader(ILogger<ConfigLoader> logger, IFileService fileService)
    {
        _logger = logger;
        _fileService = fileService;
    }
    /// <summary>
    /// Loads API connections from the specified configuration file.
    /// </summary>
    /// <returns>List of ApiConnection objects</returns>
    public List<ApiConnection> LoadApiConnections()
    {
        var configFile = _fileService.GetFullPath(".gitvision/apiconnect.json");
        try
        {
            if (string.IsNullOrEmpty(configFile))
            {
                _logger.LogWarning("API connection configuration file path is not set.");
                return new List<ApiConnection>();
            }
            if (string.IsNullOrEmpty(_fileService.GetFullPath(configFile)))
            {
                _logger.LogWarning("API connection configuration file not found at {Path}", configFile);
                return new List<ApiConnection>();
            }
            var json = _fileService.ReadFile(configFile);
            if (string.IsNullOrEmpty(json))
            {
                _logger.LogError("Failed to read API connection configuration from {Path}", configFile);
                return new List<ApiConnection>();
            }
            var config = JsonConvert.DeserializeObject<ApiConfig>(json);
            if (config == null || config.ApiConnect == null)
            {
                _logger.LogError("Invalid API connection configuration format in {Path}", configFile);
                return new List<ApiConnection>();
            }
            return config.ApiConnect;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading API connection configuration from {Path}", configFile);
            return new List<ApiConnection>();
        }
    }
    /// <summary>
    /// Gets the API connection settings for a given API name.
    /// </summary>
    /// <param name="apiName">The name of the API.</param>
    /// <returns>ApiConnection object if found, otherwise a new ApiConnection.</returns>
    public ApiConnection GetApiConnectionSettings(string apiName)
    {
        try
        {
            var apiConnections = LoadApiConnections();
            if (apiConnections == null || apiConnections.Count == 0)
            {
                _logger.LogWarning("API connection configuration not found.");
                return new ApiConnection();
            }
            var apiConn = apiConnections.FirstOrDefault(x => x.Name == apiName);
            if (apiConn == null)
            {
                _logger.LogWarning("API connection '{ApiName}' not found in configuration.", apiName);
                return new ApiConnection();
            }
            return apiConn;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "API connection configuration not found.");
            return new ApiConnection();
        }
    }

    /// <summary>
    /// Loads the main configuration from .gitvision/config.json
    /// </summary>
    /// <returns>Config object with project, settings, and git configuration</returns>
    public GitVisionConfig LoadConfig()
    {
        var configFile = _fileService.GetFullPath(".gitvision/config.json");
        try
        {
            if (string.IsNullOrEmpty(configFile))
            {
                _logger.LogWarning("Main configuration file path is not set.");
                return new GitVisionConfig();
            }

            if (string.IsNullOrEmpty(_fileService.GetFullPath(configFile)))
            {
                _logger.LogWarning("Main configuration file not found at {Path}", configFile);
                return new GitVisionConfig();
            }

            var json = _fileService.ReadFile(configFile);
            if (string.IsNullOrEmpty(json))
            {
                _logger.LogError("Failed to read main configuration from {Path}", configFile);
                return new GitVisionConfig();
            }

            // Use Newtonsoft.Json for consistency with the rest of the codebase
            var config = JsonConvert.DeserializeObject<GitVisionConfig>(json);
            if (config == null)
            {
                _logger.LogError("Invalid main configuration format in {Path}", configFile);
                return new GitVisionConfig();
            }

            _logger.LogInformation("Successfully loaded main configuration from {Path}", configFile);
            return config;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading main configuration from {Path}", configFile);
            return new GitVisionConfig();
        }
    }
}
