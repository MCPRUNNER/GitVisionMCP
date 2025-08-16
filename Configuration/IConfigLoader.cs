using GitVisionMCP.Models;
namespace GitVisionMCP.Configuration;

public interface IConfigLoader
{
    List<ApiConnection> LoadApiConnections();
    ApiConnection GetApiConnectionSettings(string apiName);
    GitVisionConfig LoadConfig();
}
