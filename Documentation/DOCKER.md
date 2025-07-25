# GitVisionMCP Docker Guide

This guide explains how to use GitVisionMCP in a Docker container with VS Code as a Copilot Agent.

## Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop/) installed and running
- [Visual Studio Code](https://code.visualstudio.com/) with GitHub Copilot extension installed
- A git repository to analyze

## Docker Container Setup

### Pull the Docker Image

```powershell
docker pull mcprunner/gitvisionmcp:latest
```

### Test the Docker Container

```powershell
docker run --rm -i --init mcprunner/gitvisionmcp:latest
```

Type the following JSON-RPC request to verify the container is working (press Enter after typing the JSON):

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "method": "initialize",
  "params": {
    "protocolVersion": "2024-11-05",
    "capabilities": {},
    "clientInfo": { "name": "test", "version": "1.0.0" }
  }
}
```

You should receive a response similar to:

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "result": {
    "protocolVersion": "2024-11-05",
    "capabilities": {
      "tools": {},
      "logging": {}
    },
    "serverInfo": {
      "name": "GitVisionMCP",
      "version": "1.0.0"
    }
  }
}
```

Press Ctrl+C to exit the container.

## Configure VS Code for Docker MCP

### Create/Update .vscode/mcp.json

Create a `.vscode/mcp.json` file in your workspace with the following content (adjust paths to match your system):

```json
{
  "servers": {
    "GitVisionMCP-Docker": {
      "type": "stdio",
      "command": "docker",
      "args": [
        "run",
        "--rm",
        "-i",
        "--init",
        "--stop-timeout",
        "10",
        "-e",
        "GITVISION_MCP_TRANSPORT=Stdio",
        "-e",
        "GIT_APP_LOG_DIRECTORY=/app/logs",
        "-v",
        "C:/your/repository/path:/app/repo:ro",
        "-v",
        "C:/your/repository/path/logs:/app/logs",
        "mcprunner/gitvisionmcp:latest"
      ],
      "env": {
        "DOTNET_ENVIRONMENT": "Production",
        "GITVISION_MCP_TRANSPORT": "Stdio",
        "GIT_APP_LOG_DIRECTORY": "/app/logs"
      }
    }
  }
}
```

### Important Configuration Details

1. **Volume Mounts**:

   - `-v C:/your/repository/path:/app/repo:ro`: Mount your git repository read-only inside the container
   - `-v C:/your/repository/path/logs:/app/logs`: Mount a logs directory for persistent logs

2. **Environment Variables**:

   - `DOTNET_ENVIRONMENT`: Set to "Production" for optimized performance
   - `GITVISION_MCP_TRANSPORT`: Set to "Stdio" for VS Code communication
   - `GIT_APP_LOG_DIRECTORY`: Container path for logs

3. **Path Format**:
   - Windows paths should use forward slashes in the Docker volume mounts
   - Example: `C:/Users/username/source/repos/project`

## Using GitVisionMCP with Copilot

After configuring the mcp.json file, restart VS Code to load the new MCP configuration. You can now use GitVisionMCP with Copilot by asking questions like:

### Basic Git Documentation

- "Generate documentation from git logs"
- "Create git documentation and save it to docs/changelog.md"

### Branch Management

- "Show me all branches in this repository"
- "Compare my feature branch with main"
- "List all remote branches in this repository"

### Commit Analysis

- "Get recent commits with detailed information"
- "Compare two commits and show me what files changed"
- "Get comprehensive diff information between commits"

## Available Docker MCP Tools

GitVisionMCP in Docker provides 13 tools for comprehensive git analysis:

### Documentation Tools

- `generate_git_documentation` - Generate docs from git logs
- `generate_git_documentation_to_file` - Save docs to file

### Branch Tools

- `get_all_branches` - List all branches
- `get_local_branches` - List local branches
- `get_remote_branches` - List remote branches
- `compare_branches_documentation` - Compare local branches
- `compare_branches_with_remote` - Compare with remote support
- `fetch_from_remote` - Fetch from remote

### Commit Analysis Tools

- `get_recent_commits` - Get recent commits
- `compare_commits_documentation` - Compare commits
- `get_changed_files_between_commits` - List changed files
- `get_detailed_diff_between_commits` - Detailed diffs
- `get_commit_diff_info` - Comprehensive diff stats

## Troubleshooting

### Common Issues

1. **Container fails to start**

   - Check if Docker is running
   - Verify paths in volume mounts

2. **Cannot access repository**

   - Check if volume paths are correct
   - Ensure the mounted directory is a valid git repository

3. **No logs generated**

   - Verify logs directory exists and is writable
   - Check GIT_APP_LOG_DIRECTORY environment variable

4. **VS Code cannot connect**

   - Restart VS Code after configuration changes
   - Check for syntax errors in mcp.json

5. **Performance issues**
   - Consider using local version for large repositories
   - Limit maxCommits parameter in commands

## Performance Considerations

The Docker container approach is ideal for:

- Cross-platform consistency
- Isolation from local environment
- No need to install .NET SDK locally

However, for large repositories, the local installation may offer better performance.

## Example: Testing Docker Container Directly

You can test the Docker container directly with a specific git command:

```powershell
echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"get_all_branches","arguments":{}}}' | docker run --rm -i --init -v C:/your/repository/path:/app/repo:ro mcprunner/gitvisionmcp:latest
```
