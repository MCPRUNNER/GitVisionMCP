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
using System.Diagnostics;
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
    /// Runs an external process and captures stdout/stderr and exit code.
    /// </summary>
    public async Task<(bool Success, string StdOut, string StdErr, int ExitCode)> RunProcessAsync(string? workingDirectory, string fileName, string? arguments, int timeoutMs = 60000)
    {
        try
        {
            if (arguments == null)
            {
                arguments = string.Empty;
            }
            var psi = new ProcessStartInfo(fileName, arguments)
            {
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = new Process { StartInfo = psi };
            proc.Start();

            var stdoutTask = proc.StandardOutput.ReadToEndAsync();
            var stderrTask = proc.StandardError.ReadToEndAsync();

            await Task.WhenAll(stdoutTask, stderrTask, proc.WaitForExitAsync());

            var stdout = await stdoutTask;
            var stderr = await stderrTask;

            return (proc.ExitCode == 0, stdout ?? string.Empty, stderr ?? string.Empty, proc.ExitCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error running process {FileName} {Arguments} in {WorkingDirectory}", fileName, arguments, workingDirectory);
            return (false, string.Empty, ex.Message, -1);
        }
    }

    /// <summary>
    /// Runs an external process and captures stdout/stderr and exit code.
    /// </summary>
    public async Task<(bool Success, string StdOut, string StdErr, int ExitCode)> RunPluginAsync(Plugin plugin)
    {
        try
        {
            var args = string.Empty;
            if (plugin.Arguments.Any())
            {
                args = string.Join(" ", plugin.Arguments.Select(a => $"{a.Key} {a.Value}"));
            }
            var psi = new ProcessStartInfo(plugin.Executable, args)
            {
                WorkingDirectory = plugin.WorkingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = new Process { StartInfo = psi };
            foreach (var env in plugin.Environment)
            {
                proc.StartInfo.EnvironmentVariables[env.Key] = env.Value;
            }
            proc.Start();

            var stdoutTask = proc.StandardOutput.ReadToEndAsync();
            var stderrTask = proc.StandardError.ReadToEndAsync();

            await Task.WhenAll(stdoutTask, stderrTask, proc.WaitForExitAsync());

            var stdout = await stdoutTask;
            var stderr = await stderrTask;

            return (proc.ExitCode == 0, stdout ?? string.Empty, stderr ?? string.Empty, proc.ExitCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error running process {FileName} {Arguments} in {WorkingDirectory}", plugin.Name, plugin.Arguments, plugin.WorkingDirectory);
            return (false, string.Empty, ex.Message, -1);
        }
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
    /// Creates a dictionary of environment variables by combining the current environment
    /// with additional variables provided.
    /// </summary>
    /// <param name="additionalVariables">Dictionary of additional environment variables to include</param>
    /// <param name="overwriteExisting">Whether to overwrite existing variables with the same name</param>
    /// <param name="includeCurrentEnvironment">Whether to include the current process environment variables</param>
    /// <returns>A dictionary containing the combined environment variables</returns>
    public Dictionary<string, string> CreateEnvironmentVariables(
        Dictionary<string, string>? additionalVariables = null,
        bool overwriteExisting = true,
        bool includeCurrentEnvironment = true)
    {
        var environmentVars = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Add current environment variables if requested
        if (includeCurrentEnvironment)
        {
            foreach (var envVar in Environment.GetEnvironmentVariables())
            {
                if (envVar is System.Collections.DictionaryEntry entry &&
                    entry.Key is string key &&
                    entry.Value is string value)
                {
                    environmentVars[key] = value;
                }
            }
        }

        // Add or overwrite with additional variables
        if (additionalVariables != null)
        {
            foreach (var variable in additionalVariables)
            {
                if (overwriteExisting || !environmentVars.ContainsKey(variable.Key))
                {
                    environmentVars[variable.Key] = variable.Value;
                }
            }
        }

        return environmentVars;
    }

    /// <summary>
    /// Sets an environment variable for the current process.
    /// </summary>
    /// <param name="name">The name of the environment variable</param>
    /// <param name="value">The value to set</param>
    /// <param name="target">The environment variable target (Process, User, or Machine)</param>
    /// <returns>True if the operation was successful</returns>
    public bool SetEnvironmentVariable(string name, string value, EnvironmentVariableTarget target = EnvironmentVariableTarget.Process)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogError("Environment variable name cannot be null or empty");
                return false;
            }

            Environment.SetEnvironmentVariable(name, value, target);
            _logger.LogDebug("Set environment variable {Name}={Value} for target {Target}", name, value, target);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting environment variable {Name}={Value} for target {Target}",
                name, value, target);
            return false;
        }
    }
    /// <summary>
    /// Executes a program with the specified parameters and returns the results.
    /// </summary>
    /// <param name="programPath">The full path to the program executable</param>
    /// <param name="parameters">Command line parameters to pass to the program</param>
    /// <param name="workingDirectory">Working directory for the process (defaults to current directory if null)</param>
    /// <param name="timeoutMilliseconds">Timeout in milliseconds (default 60 seconds)</param>
    /// <param name="environmentVariables">Dictionary of environment variables to set for the process</param>
    /// <returns>A tuple containing success flag, standard output, standard error, and exit code</returns>
    public async Task<(bool Success, string Output, string Error, int ExitCode)> ExecutePluginProgramAsync(
        string programPath,
        string? parameters,
        string? workingDirectory = null,
        int timeoutMilliseconds = 60000,
        Dictionary<string, string>? environmentVariables = null)
    {
        if (string.IsNullOrEmpty(programPath))
        {
            _logger.LogError("Program path cannot be null or empty");
            return (false, string.Empty, "Program path was not provided", -1);
        }

        // Use the current directory if no working directory is specified
        var effectiveWorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory();

        // Ensure the program exists
        if (!File.Exists(programPath))
        {
            _logger.LogError("Program not found at path: {ProgramPath}", programPath);
            return (false, string.Empty, $"Program not found: {programPath}", -1);
        }

        _logger.LogInformation("Executing program: {ProgramPath} with parameters: {Parameters}",
            programPath, parameters);

        try
        {
            if (parameters == null)
            {
                parameters = string.Empty;
            }

            var psi = new ProcessStartInfo(programPath, parameters)
            {
                WorkingDirectory = effectiveWorkingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Add environment variables if provided
            if (environmentVariables != null)
            {
                foreach (var envVar in environmentVariables)
                {
                    _logger.LogDebug("Setting environment variable: {Key}={Value}", envVar.Key, envVar.Value);
                    psi.Environment[envVar.Key] = envVar.Value;
                }
            }

            using var proc = new Process { StartInfo = psi };
            proc.Start();

            var stdoutTask = proc.StandardOutput.ReadToEndAsync();
            var stderrTask = proc.StandardError.ReadToEndAsync();

            await Task.WhenAll(stdoutTask, stderrTask, proc.WaitForExitAsync());

            var stdout = await stdoutTask;
            var stderr = await stderrTask;

            return (proc.ExitCode == 0, stdout ?? string.Empty, stderr ?? string.Empty, proc.ExitCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing program {ProgramPath} {Parameters} in {WorkingDirectory}",
                programPath, parameters, effectiveWorkingDirectory);
            return (false, string.Empty, ex.Message, -1);
        }
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
