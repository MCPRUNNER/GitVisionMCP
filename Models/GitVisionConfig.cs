using Newtonsoft.Json;

namespace GitVisionMCP.Models;

public class GitVisionConfig : IGitVisionConfig
{
    [JsonProperty("Project")]
    public Project? Project
    {
        get; set;
    }

    [JsonProperty("Settings")]
    public Settings? Settings
    {
        get; set;
    }

    [JsonProperty("Git")]
    public Git? Git
    {
        get; set;
    }

    [JsonProperty("Kestrel")]
    public Kestrel? Kestrel
    {
        get; set;
    }
}

public class Project
{
    [JsonProperty("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("RelativePath")]
    public string RelativePath { get; set; } = string.Empty;

    [JsonProperty("Version")]
    public string Version { get; set; } = string.Empty;
}

public class Settings
{
    [JsonProperty("maxCommits")]
    public int MaxCommits { get; set; } = 100;

    [JsonProperty("maxFiles")]
    public int MaxFiles { get; set; } = 1000;

    [JsonProperty("maxFileSize")]
    public int MaxFileSize { get; set; } = 1048576;
}

public class Git
{
    [JsonProperty("Release")]
    public string Release { get; set; } = "origin/main";
}

public class Kestrel
{
    [JsonProperty("Endpoints")]
    public Endpoints Endpoints { get; set; } = new Endpoints();

    [JsonProperty("Limits")]
    public Limits Limits { get; set; } = new Limits();
}

public class Endpoints
{
    [JsonProperty("Http")]
    public Http Http { get; set; } = new Http();

    [JsonProperty("Https")]
    public Https Https { get; set; } = new Https();
}

public class Http
{
    [JsonProperty("Url")]
    public string Url { get; set; } = "http://localhost:5000/mcp";
}

public class Https
{
    [JsonProperty("Url")]
    public string Url { get; set; } = "https://localhost:5001/mcp";
}

public class Limits
{
    [JsonProperty("MaxRequestBodySize")]
    public int MaxRequestBodySize { get; set; } = 10485760;

    [JsonProperty("MaxConcurrentConnections")]
    public int MaxConcurrentConnections { get; set; } = 100;

    [JsonProperty("RequestHeadersTimeout")]
    public string RequestHeadersTimeout { get; set; } = "00:01:00";
}