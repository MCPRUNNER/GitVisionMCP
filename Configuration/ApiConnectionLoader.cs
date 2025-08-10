using System.IO;
using Newtonsoft.Json;
using GitVisionMCP.Models;
namespace GitVisionMCP.Configurations;
public static class ApiConnectionLoader
{
    public static Dictionary<string, ApiConnection> Load(string path)
    {
        var json = File.ReadAllText(path);
        var config = JsonConvert.DeserializeObject<ApiConfig>(json);
        return config.ApiConnect;
    }
}
