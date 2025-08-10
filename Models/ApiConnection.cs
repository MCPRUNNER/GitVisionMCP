using System.Collections.Generic;
namespace GitVisionMCP.Models;


/// <summary>
/// Represents an API connection configuration.
/// </summary>
public class ApiConnection
{
    public string Description
    {
        get; set;
    }
    public string BaseUrl
    {
        get; set;
    }
    public Dictionary<string, string> Headers
    {
        get; set;
    }
    public ApiAuthentication Authentication
    {
        get; set;
    }
}
