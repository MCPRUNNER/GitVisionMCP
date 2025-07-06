# GitVisionMCP v1.0.0 Release Notes

**Release Date:** July 6, 2025

## Summary of Changes

GitVisionMCP v1.0.0 represents the first official release of our Model Context Protocol (MCP) Server for git repository documentation and analysis. This tool integrates seamlessly with VS Code as a Copilot Agent to provide developers with powerful capabilities for understanding and documenting repository changes. It offers a comprehensive set of features for generating documentation from git history, comparing branches and commits, and performing detailed repository analysis.

Key highlights of this release:

- Full MCP server implementation with STDIO and HTTP transport support
- Git documentation generation tools with multiple output formats
- Branch and commit comparison with detailed diff analysis
- Docker containerization support for flexible deployment
- Comprehensive search functionality across commit history
- Unit testing framework ensuring reliability and stability

## New Features

### MCP Server Implementation

- **JSON-RPC 2.0 Interface**: Fully compliant Model Context Protocol Server implementation supporting both STDIO and HTTP transports, with HTTP transport targeted for future release.
- **Prompt-Based Documentation**: Specialized prompts for generating release documentation with customizable version information
- **Tool Registration**: Clean attribute-based approach for registering MCP tools and prompts
- **Extensible Architecture**: Modular design allowing easy addition of new tools and features

### Git Documentation Tools

- **Documentation Generation**: Create comprehensive documentation from git logs with configurable number of commits
- **Multiple Output Formats**: Support for markdown, HTML, and plain text documentation
- **Branch Comparison**: Generate detailed documentation comparing differences between two branches
- **Commit Comparison**: Create reports highlighting changes between specific commits
- **Remote Branch Support**: Full functionality for both local and remote branch analysis with automatic fetching

### Repository Analysis

- **File Change Detection**: Identify all files changed between specific commits
- **Detailed Diffs**: Get comprehensive line-by-line diffs showing exact changes
- **Commit Searching**: Powerful search functionality across commit messages and file contents
- **Match Highlighting**: Precise location information for search matches with line numbers and context

### Docker Support

- **Containerized Deployment**: Run GitVisionMCP in a Docker container for consistent execution
- **Volume Mounting**: Easily connect git repositories and log storage via Docker volumes
- **Environment Configuration**: Control server behavior using environment variables
- **Production-Ready**: Optimized container setup with proper shutdown handling

## Enhancements

### Logging System

- **Serilog Integration**: Comprehensive logging with Serilog for better diagnostics and troubleshooting
- **Log Rotation**: Automatic daily log rotation with configurable size limits
- **Environment Variable Control**: Set log directory via the GIT_APP_LOG_DIRECTORY environment variable
- **Structured Output**: Detailed logging with timestamps, log levels, and exception information

### Server Configuration

- **Transport Selection**: Choose between STDIO (default) and HTTP transports via environment variables
- **Environment-Specific Settings**: Different configurations for Development and Production environments
- **Signal Handling**: Proper handling of shutdown signals for clean termination
- **Performance Optimization**: Configurable request limits and timeouts for HTTP transport

### Project Structure

- **Clean Architecture**: Well-organized code with proper separation of concerns
- **Dependency Injection**: Modern DI pattern throughout the application
- **Unit Testing**: Comprehensive test coverage for models, services, and server components
- **Documentation**: Extensive documentation including setup guides, examples, and Docker instructions

## Bug Fixes

- Resolved issues with UTF-8 encoding in JSON-RPC communication
- Fixed problems with log file creation in containerized environments
- Addressed signal handling on both Windows and Unix platforms
- Corrected project naming and file structure for consistency

## Breaking Changes

None - this is the first official release.

## Deprecated Features

None - this is the first official release.

## Known Issues

- HTTP transport mode is experimental and may not support all features
- Large repositories may experience slower performance with deep search operations
- Docker volume mapping requires careful path configuration to avoid permission issues

## Installation/Upgrade Instructions

### Prerequisites

- .NET 9.0 SDK or later
- Git installation (for repository access)
- Docker (optional, for containerized deployment)
- VS Code with Copilot extension (for agent integration)

### Installation

#### Local Installation

1. Clone the repository:

   ```
   git clone https://github.com/MCPRUNNER/GitVisionMCP.git
   cd GitVisionMCP
   ```

2. Build the project:

   ```
   dotnet build
   ```

3. Run the server:
   ```
   dotnet run
   ```

#### Docker Installation

1. Pull the Docker image (or build using the included Dockerfile):

   ```
   docker pull mcprunner/gitvisionmcp:latest
   ```

2. Run the container:
   ```
   docker run --rm -i --init --stop-timeout 10 \
     -e GITVISION_MCP_TRANSPORT=Stdio \
     -e GIT_APP_LOG_DIRECTORY=/app/logs \
     -v /path/to/repo:/app/repo \
     -v /path/to/logs:/app/logs \
     mcprunner/gitvisionmcp:latest
   ```

### VS Code Integration

1. Create or update your `.vscode/mcp.json` file:

   ```json
   {
     "servers": {
       "GitVisionMCP": {
         "type": "stdio",
         "command": "dotnet",
         "args": [
           "run",
           "--project",
           "/path/to/GitVisionMCP.csproj",
           "--no-build",
           "--verbosity",
           "quiet"
         ],
         "env": {
           "DOTNET_ENVIRONMENT": "Production"
         }
       }
     }
   }
   ```

2. Restart VS Code and select GitVisionMCP as your Copilot agent

## Documentation

For more information, please refer to the following documentation:

- [README.md](README.md) - General overview and key capabilities
- [SETUP.md](Documentation/SETUP.md) - Detailed setup instructions
- [DOCKER.md](Documentation/DOCKER.md) - Docker deployment guide
- [EXAMPLES.md](Documentation/EXAMPLES.md) - Usage examples and scenarios
