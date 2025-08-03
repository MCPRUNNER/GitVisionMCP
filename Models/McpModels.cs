
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace GitVisionMCP.Models;

public class JsonRpcRequest
{
    [JsonProperty("jsonrpc")]
    public string JsonRpc { get; set; } = "2.0";

    [JsonProperty("id")]
    public object? Id
    {
        get; set;
    }

    [JsonProperty("method")]
    public string Method { get; set; } = string.Empty;

    [JsonProperty("params")]
    public object? Params
    {
        get; set;
    }
}

public class JsonRpcResponse
{
    [JsonProperty("jsonrpc")]
    public string JsonRpc { get; set; } = "2.0";

    [JsonProperty("id")]
    public object? Id
    {
        get; set;
    }

    [JsonProperty("result")]
    public object? Result
    {
        get; set;
    }

    [JsonProperty("error")]
    public JsonRpcError? Error
    {
        get; set;
    }
}

public class JsonRpcError
{
    [JsonProperty("code")]
    public int Code
    {
        get; set;
    }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("data")]
    public object? Data
    {
        get; set;
    }
}

public class InitializeRequest
{
    [JsonProperty("protocolVersion")]
    public string ProtocolVersion { get; set; } = "2024-11-05";

    [JsonProperty("capabilities")]
    public ClientCapabilities Capabilities { get; set; } = new();

    [JsonProperty("clientInfo")]
    public ClientInfo ClientInfo { get; set; } = new();
}

public class ClientCapabilities
{
    [JsonProperty("roots")]
    public RootsCapability? Roots
    {
        get; set;
    }

    [JsonProperty("sampling")]
    public object? Sampling
    {
        get; set;
    }
}

public class RootsCapability
{
    [JsonProperty("listChanged")]
    public bool ListChanged
    {
        get; set;
    }
}

public class ClientInfo
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("version")]
    public string Version { get; set; } = string.Empty;
}

public class InitializeResponse
{
    [JsonProperty("protocolVersion")]
    public string ProtocolVersion { get; set; } = "2024-11-05";

    [JsonProperty("capabilities")]
    public ServerCapabilities Capabilities { get; set; } = new();

    [JsonProperty("serverInfo")]
    public ServerInfo ServerInfo { get; set; } = new();
}

public class ServerCapabilities
{
    [JsonProperty("tools")]
    public object? Tools { get; set; } = new { };

    [JsonProperty("resources")]
    public object? Resources
    {
        get; set;
    }

    [JsonProperty("prompts")]
    public object? Prompts
    {
        get; set;
    }

    [JsonProperty("logging")]
    public object? Logging
    {
        get; set;
    }
}

public class ServerInfo
{
    [JsonProperty("name")]
    public string Name { get; set; } = "GitVisionMCP";

    [JsonProperty("version")]
    public string Version { get; set; } = "1.0.0";
}

public class Tool
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("inputSchema")]
    public object InputSchema { get; set; } = new();
}

public class ToolsListResponse
{
    [JsonProperty("tools")]
    public Tool[] Tools { get; set; } = Array.Empty<Tool>();
}

public class CallToolRequest
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("arguments")]
    public Dictionary<string, object>? Arguments
    {
        get; set;
    }
}

public class CallToolResponse
{
    [JsonProperty("content")]
    public ToolContent[] Content { get; set; } = Array.Empty<ToolContent>();

    [JsonProperty("isError")]
    public bool IsError
    {
        get; set;
    }
}

public class ToolContent
{
    [JsonProperty("type")]
    public string Type { get; set; } = "text";

    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;
}


















