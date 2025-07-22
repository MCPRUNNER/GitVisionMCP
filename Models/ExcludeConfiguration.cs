using Newtonsoft.Json;

namespace GitVisionMCP.Models;

/// <summary>
/// Configuration for file and directory exclusions in workspace operations
/// </summary>
public class ExcludeConfiguration
{
    /// <summary>
    /// Glob patterns for files and directories to exclude
    /// </summary>
    [JsonProperty("excludePatterns")]
    public List<string> ExcludePatterns { get; set; } = new();

    /// <summary>
    /// Description of the exclude configuration
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Version of the exclude configuration format
    /// </summary>
    [JsonProperty("version")]
    public string Version { get; set; } = "1.0.0";
}
