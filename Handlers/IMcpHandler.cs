using System.Threading;
using System.Threading.Tasks;

namespace GitVisionMCP.Handlers;

/// <summary>
/// Interface for the Model Context Protocol server
/// </summary>
public interface IMcpHandler
{
    /// <summary>
    /// Starts the MCP server
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the MCP server
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task StopAsync(CancellationToken cancellationToken = default);
}
