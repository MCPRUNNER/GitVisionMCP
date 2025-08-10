using GitVisionMCP.Models;
namespace GitVisionMCP.Configuration;

public interface IConfigLoader
{
    List<ApiConnection> LoadApiConnections();
    public ApiConnection GetApiConnectionSettings(string apiName);
}
