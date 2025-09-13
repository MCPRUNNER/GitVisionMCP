# GitVisionMCP v1.0.9.2 Release Notes

**Release Date:** September 13, 2025

## Summary of Changes

GitVisionMCP v1.0.9.2 is the second official release of our Model Context Protocol (MCP) server for git repository documentation. This release introduces a comprehensive set of tools for generating documentation from git history, comparing branches and commits, and providing insights into repository changes. The server is designed to work seamlessly with VS Code as a Copilot Agent.

Key highlights of this release:

- Full MCP server implementation with STDIO and JSON-RPC 2.0 support
- Git documentation generation tools
- Branch and commit comparison capabilities
- Docker containerization support
- Comprehensive logging system

## New Features

### MCP Server Implementation

- **JSON-RPC 2.0 Interface**: Implements the Model Context Protocol Server with support for both STDIO and HTTP transports
- **Prompt System**: Includes customizable prompts for generating different types of documentation
- **Tool Registration**: Features a clean attribute-based approach for registering MCP tools

### Git Documentation Tools

- **Documentation Generation**: Generate comprehensive documentation from git logs with configurable output formats (markdown, HTML, text)
- **Branch Comparisons**: Create documentation comparing differences between two branches
- **Commit Comparisons**: Generate detailed reports of changes between specific commits
- **Remote Repository Support**: Work with both local and remote branches using fetch capabilities

### Repository Analysis

- **File Change Detection**: Identify files changed between commits
- **Detailed Diffs**: Get comprehensive line-by-line diffs of changes
- **Commit Searching**: Search all commits for specific strings with detailed results

### Docker Support

- **Container Integration**: Run GitVisionMCP in a containerized environment
- **Volume Mounting**: Configure git repository access and log storage through Docker volumes
- **Environment Variables**: Control server behavior using environment variables

## Enhancements

### Logging System

- **Serilog Integration**: Comprehensive logging with Serilog
- **Log Rotation**: Automatic daily log rotation with size limits
- **Configurable Paths**: Set custom log directories using environment variables

### Server Configuration

- **Multiple Transport Types**: Choose between STDIO (default) and HTTP transports
- **Environment-specific Settings**: Different configurations for Development and Production environments
- **Signal Handling**: Proper handling of shutdown signals for clean termination

### Project Structure

- **Clean Architecture**: Well-organized project structure with proper separation of concerns
- **Dependency Injection**: Modern DI pattern for all services
- **Testability**: Unit tests for key components

### Scriban Template Support

- **Template Engine**: Use Scriban templates for flexible documentation generation
- **Customizable Outputs**: Easily modify templates to fit project needs

### Example Github Prompts

- **Predefined Prompts**: Example prompts for common documentation tasks
- **Custom Prompts**: Define your own prompts for specific use cases

### Plugin Support

- **Extensibility**: Easily add new features and tools through a plugin system
- **Dynamic Loading**: Load plugins at runtime without restarting the server
- **Configuration**: Customize plugin behavior using configuration files

### Process Support

- **Process Management**: Tools for managing system processes and resources
- **Environment Variables**: Access and modify environment variables for the server process
- **Cross-Platform**: Compatible with Windows, Linux, and macOS environments

### Auto Documentation Support

- **Automatic Documentation**: Generate documentation automatically based on git activity

## Documentation Exclude Functionality

The exclude functionality allows users to define patterns for files and directories that should be ignored during documentation generation and analysis. This is particularly useful for large repositories with many irrelevant files.

## Bug Fixes

- Fixed issues with UTF-8 encoding in JSON-RPC communication
- Resolved problems with log file creation in containerized environments
- Addressed signal handling on both Windows and Unix environments

## Breaking Changes

None - this is the first official release.

## Deprecated Features

None - this is the first official release.

## Known Issues

- When using Docker, ensure that volume paths are correctly mapped to avoid permission issues
- HTTP transport mode is experimental and may not support all features

## Installation/Upgrade Instructions

### Prerequisites

- .NET 9.0 SDK or later
- Git (for accessing repositories)
- Docker (optional, for containerized usage)

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

1. Configure the `.vscode/mcp.json` file to include:

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
         },
         "prompts": {
           "release_document": true,
           "release_document_with_version": {
             "parameters": {
               "version": "1.0.0",
               "releaseDate": "2025-07-06"
             }
           }
         }
       }
     }
   }
   ```

2. Reload VS Code and select GitVisionMCP as your Copilot agent

## Documentation

For more information, please refer to:

### See Also

- [DECONSTRUCTION_SERVICE.md](DECONSTRUCTION_SERVICE.md) — Details and examples for deconstruction tools
- [EXAMPLES.md](EXAMPLES.md) — Usage examples for all MCP tools
