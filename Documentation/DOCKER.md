# GitVisionMCP 1.0.8.2 Docker Guide

This guide explains how to use GitVisionMCP in a Docker container with VS Code as a Copilot Agent.

## About

GitVisionMCP is a comprehensive Model Context Protocol (MCP) Server that provides advanced git analysis and documentation tools, including powerful commit, Excel, XML, YAML and JSON search capabilities. Designed to be used as a Copilot Agent in VS Code for comprehensive repository analysis and documentation generation.

## Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop/) installed and running
- [Visual Studio Code](https://code.visualstudio.com/) with GitHub Copilot extension installed
- A git repository to analyze
- Repository Specific Custom Configuration files: [.gitvision](https://github.com/MCPRUNNER/GitVisionMCP/tree/master/.gitvision)
- Repository Specific Custom Prompts and Instructions: [.github](https://github.com/MCPRUNNER/GitVisionMCP/tree/master/.github)

## Docker Container Setup

### Pull the Docker Image

```powershell
docker pull mcprunner/gitvisionmcp:latest
```

## Configure VS Code for Docker MCP

### Create/Update .vscode/mcp.json

Path format dependent on your OS. For Windows, use forward slashes or double backslashes in paths.
GITHUB_TOKEN is optional.

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
        "/your/repository/path:/app/repo",
        "-v",
        "/your/repository/path/logs:/app/logs",
        "mcprunner/gitvisionmcp:latest"
      ],
      "env": {
        "DOTNET_ENVIRONMENT": "Production",
        "GITVISION_MCP_TRANSPORT": "Stdio",
        "GIT_APP_LOG_DIRECTORY": "/app/logs",
        "GITHUB_TOKEN": "ghp_example_token"
      }
    }
  }
}
```

### Important Configuration Details

1. **Volume Mounts**:

   - `-v C:/your/repository/path:/app/repo`: Mount your git repository inside the container
   - `-v C:/your/repository/path/logs:/app/logs`: Mount a logs directory for persistent logs

2. **Environment Variables**:

   - `DOTNET_ENVIRONMENT`: Set to "Production" for optimized performance
   - `GITVISION_MCP_TRANSPORT`: Set to "Stdio" or "Http" for VS Code communication (defaults to Stdio)
   - `GIT_APP_LOG_DIRECTORY`: Container path for logs
   - `GITHUB_TOKEN`: **_Optional_** Github PAT, will default to git commands if unavailable.

3. **Path Format**:

   - Windows paths should use forward slashes or double backslashes in the Docker volume mounts
   - Example 1: `"c:\\Users\\username\\source\\repos\\project:/app/repo",`
   - Example 2: `/c/Users/username/source/repos/project`

4. **GITHUB_TOKEN**: see [Git authentication for GitVisionMCP](#git-authentication-for-gitvisionmcp)

### Git authentication for GitVisionMCP

This project uses LibGit2Sharp for in-process git operations and falls back to the `git` CLI when necessary (for example when the native libgit2 build doesn't support SSH transports).

Recommended approaches:

1. HTTPS + token (recommended for CI / automated runs)

- Create a Personal Access Token (PAT) with repo access on GitHub.
- Set the token in your environment before running GitVisionMCP.

Windows PowerShell (single session):

```powershell
$env:GITHUB_TOKEN = "<your_token_here>"
# Run the server in the same shell so it inherits the variable
dotnet run --project .\GitVisionMCP.csproj
```

Or set an environment variable persistently (example for the current user):

```powershell
setx GITHUB_TOKEN "<your_token_here>"
# New shells will see the variable
```

The server will use this token for HTTPS fetches and for the CLI fallback by passing an Authorization header to `git`.

2. SSH remotes

If your remote uses SSH (`git@github.com:owner/repo.git`) the embedded libgit2 may not support SSH in your environment. In that case either:

- Use the CLI fallback (ensure `git` is available on PATH and you have SSH keys configured), or
- Switch the remote to HTTPS and use a token (preferred for automation):

```powershell
git remote set-url origin https://github.com/<owner>/<repo>.git
```

3. Troubleshooting

- If fetches fail, check the server logs for errors and the CLI fallback output.
- Ensure `git` is installed and accessible on PATH for the fallback to work.

Security note: treat tokens as secrets. Do not hard-code tokens in source or commit them to the repository.

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

GitVisionMCP in Docker provides 35+ tools for comprehensive git analysis and file operations:

### Documentation Tools

- `gv_generate_git_commit_report` - Generate commit report for current branch
- `gv_generate_git_commit_report_to_file` - Generate commit report and save to file

### Branch Tools

- `gv_get_all_branches` - List all branches (local and remote)
- `gv_get_local_branches` - List local branches
- `gv_get_remote_branches` - List remote branches
- `gv_get_current_branch` - Get current active branch
- `gv_compare_branches_documentation` - Compare differences between two branches
- `gv_compare_branches_with_remote_documentation` - Compare branches with remote support
- `gv_fetch_from_remote` - Fetch latest changes from remote repository

### Commit Analysis Tools

- `gv_get_recent_commits` - Get recent commits from the current repository
- `gv_compare_commits_documentation` - Generate documentation comparing differences between two commits
- `gv_get_changed_files_between_commits` - Get list of files changed between two commits
- `gv_get_detailed_diff_between_commits` - Get detailed diff content between two commits
- `gv_get_commit_diff_info` - Get comprehensive diff information between two commits
- `gv_get_file_line_diff_between_commits` - Get line-by-line file diff between two commits
- `gv_search_commits_for_string` - Search all commits for a specific string

### File Analysis Tools

- `gv_list_workspace_files` - List all files in the workspace with optional filtering
- `gv_list_workspace_files_with_cached_data` - List workspace files using pre-fetched file data
- `gv_read_filtered_workspace_files` - Read contents of all files from filtered workspace results
- `gv_search_json_file` - Search for JSON values using JSONPath
- `gv_search_yaml_file` - Search for YAML values using JSONPath
- `gv_search_xml_file` - Search for XML values using XPath
- `gv_search_csv_file` - Search for CSV values using JSONPath
- `gv_search_excel_file` - Search for values in Excel files using JSONPath
- `gv_transform_xml_with_xslt` - Transform XML file using XSLT stylesheet

### Code Analysis Tools

- `gv_deconstruct_to_file` - Deconstruct C# file and return structure as JSON
- `gv_deconstruct_to_json` - Deconstruct C# file and save structure to JSON file
- `gv_git_find_merge_conflicts` - Search for Git merge conflicts in source code

### Utility Tools

- `gv_get_app_version` - Extract application version from project file
- `gv_run_process` - Run an external process and capture its output
- `gv_run_plugin` - Run a plugin and capture its output
- `gv_get_environment_variable` - Get the value of an environment variable
- `gv_set_environment_variable` - Set an environment variable for the current process

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
echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"gv_get_all_branches","arguments":{}}}' | docker run --rm -i --init -v C:/your/repository/path:/app/repo:ro mcprunner/gitvisionmcp:latest
```
