# GitVisionMCP

A comprehensive Model Context Protocol (MCP) Server that provides advanced git analysis and documentation tools, including powerful commit search capabilities. Designed to be used as a Copilot Agent in VS Code for comprehensive repository analysis and documentation generation.

## MCP Prompts

GitVisionMCP provides specialized MCP prompts for creating release documentation. These prompts help guide the AI in generating comprehensive and well-structured release notes based on git history.

### Available MCP Prompts and Documentation Generation

## 🔥 Key Capabilities

- **📝 Documentation Generation**: Create comprehensive documentation from git logs
- **🔍 Commit Search**: Search across all commits for specific strings with detailed match results
- **🌿 Branch Analysis**: Compare branches (local and remote) with detailed diff information
- **📊 Historical Analysis**: Analyze changes between commits with line-by-line precision
- **🌐 Remote Support**: Full support for remote repositories and branch comparison
- **🎯 Precision Tools**: Get exact file changes, line diffs, and comprehensive statistics

## 🆕 What's New - Commit Search Tool

**Latest Addition**: Powerful commit search functionality that revolutionizes how you explore repository history:

✅ **Deep Search Capabilities**

- Search through commit messages AND file contents simultaneously
- Case-insensitive search finds matches regardless of text case
- Automatic binary file filtering for optimal performance

✅ **Comprehensive Results**

- Exact line numbers and full line content for every match
- Commit metadata: hash, author, timestamp, and message
- File-by-file breakdown showing exactly where matches occur
- Summary statistics: total commits searched, matching commits, total line matches

✅ **Practical Applications**

- **Bug Tracking**: `"Find all commits mentioning 'authentication error'"`
- **Feature History**: `"Search for 'user registration' across all development"`
- **Security Audits**: `"Look for 'password' or 'secret' in commit history"`
- **Code Archaeology**: `"Find all references to deprecated API functions"`
- **Documentation**: `"Search for 'TODO' comments across the project"`

## ⚠️ Important Setup Note

To ensure clean JSON-RPC communication, the MCP server should be run with:

- Pre-built binaries (`--no-build` flag)
- Production environment (`DOTNET_ENVIRONMENT=Production`)
- Quiet verbosity (`--verbosity quiet`)

This prevents build messages and logging output from interfering with the JSON-RPC protocol.

## Features

### 🛠️ Complete Tool Suite (15 Tools Available)

This MCP server provides comprehensive git documentation and analysis capabilities through 15 specialized tools:

**📝 Documentation & Analysis (6 tools)**

- Documentation generation from git logs
- Branch and commit comparison with detailed analysis
- Remote repository integration and synchronization
- Historical change tracking and statistics

**🔍 Search & Discovery (3 tools)**

- Comprehensive commit search across messages and file contents
- Intelligent file change detection between commits
- JSON file search and query capabilities using JSONPath

**🌿 Branch Management (4 tools)**

- Local and remote branch discovery
- Cross-repository branch comparison
- Remote fetch and synchronization operations
- Multi-branch analysis and reporting

**⚡ Advanced Analysis (2 tools)**

- Line-by-line diff analysis for specific files
- Recent commit retrieval with detailed metadata

### Core Documentation Tools

- **generate_git_documentation**: Generate documentation from git logs for the current workspace
- **generate_git_documentation_to_file**: Generate documentation from git logs and write to a file

### Branch and Commit Comparison Tools

- **compare_branches_documentation**: Generate documentation comparing differences between two local branches
- **compare_branches_with_remote**: 🆕 Compare branches with full remote support (GitHub, GitLab, etc.)
- **compare_commits_documentation**: Generate documentation comparing differences between two commits

### Advanced Git Analysis Tools

- **get_recent_commits**: Get recent commits with detailed information
- **get_changed_files_between_commits**: List files changed between two commits
- **get_detailed_diff_between_commits**: Get detailed diff content between commits
- **get_commit_diff_info**: Get comprehensive diff statistics and file changes
- **get_file_line_diff_between_commits**: 🆕 Get line-by-line diff for a specific file between two commits
- **search_commits_for_string**: 🆕 Search all commits for a specific string and return detailed match information
- **search_json_file**: 🆕 Search for JSON values in a JSON file using JSONPath queries

### Commit Search Tool

The new **search_commits_for_string** tool provides comprehensive commit searching capabilities:

- **Search commit messages**: Find commits containing specific text in their messages
- **Search file contents**: Search through all files in each commit for the specified string
- **Detailed match information**: Returns commit hash, timestamp, author, line numbers, and full line content
- **File-by-file breakdown**: Shows exactly which files contain matches and where
- **Case-insensitive search**: Finds matches regardless of case

#### Search Results Include:

- Commit hash (short form)
- Commit timestamp
- Author information
- Commit message
- File names containing matches
- Line numbers where matches occur
- Full line content showing the match in context

### JSON Search Tool

The new **search_json_file** tool provides powerful JSON querying capabilities using JSONPath:

- **JSONPath queries**: Search JSON files using standard JSONPath syntax
- **Flexible file access**: Search any JSON file in the workspace
- **Formatted output**: Option to return results with or without indentation
- **Error handling**: Comprehensive error reporting for invalid files or queries

#### JSONPath Query Examples:

- `$.name` - Get the root-level name property
- `$.users[*].email` - Get all user email addresses (supports wildcards)
- `$.configuration.database.host` - Get nested configuration values
- `$.items[?(@.price > 100)].name` - Get names of items with price > 100 (conditional filtering)
- `$..author` - Get all author properties at any level (recursive descent)
- `$.configuration.*` - Get all values under configuration (wildcard properties)

#### Enhanced JSONPath Support:

- **Wildcards**: Use `[*]` to select all elements in arrays or `.*` for all properties
- **Recursive Descent**: Use `..` to search at any depth in the JSON structure
- **Conditional Filtering**: Use `[?(@.property == 'value')]` to filter based on conditions
- **Multiple Results**: Automatically returns JSON arrays for queries matching multiple items
- **Single Results**: Returns individual values when only one match is found

#### JSON Search Results:

- Extracted JSON values matching the JSONPath query
- Formatted output (indented or compact)
- "No matches found" message when query returns no results

### Branch Discovery and Remote Support

- **get_local_branches**: List all local branches in the repository
- **get_remote_branches**: List all remote branches (origin, upstream, etc.)
- **get_all_branches**: List both local and remote branches
- **fetch_from_remote**: Fetch latest changes from remote repository

## 🚀 New Remote Branch Support

The server now fully supports remote branches, enabling:

- **Cross-repository analysis**: Compare local development with remote main/master branches
- **Release planning**: Analyze differences between release branches and main
- **Code review preparation**: Document changes before creating pull requests
- **Team collaboration**: Compare feature branches with remote counterparts

### Remote Branch Examples

#### Using Copilot Commands

```bash
# Compare local feature branch with remote main
@copilot Compare my feature/new-api branch with origin/main and save to analysis.md

# Compare two remote branches
@copilot Compare origin/release/v2.0 with origin/main and save to release-diff.md

# Compare with automatic remote fetch
@copilot Fetch from origin and compare main with origin/main to check synchronization
```

#### JSON-RPC Tool Calls

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "method": "tools/call",
  "params": {
    "name": "compare_branches_with_remote",
    "arguments": {
      "branch1": "feature/new-api",
      "branch2": "origin/main",
      "filePath": "analysis.md",
      "fetchRemote": true
    }
  }
}
```

## Output Formats

The server supports multiple output formats:

- **Markdown** (default): Human-readable markdown format with tables and code blocks
- **HTML**: Rich HTML format with styling and navigation
- **Text**: Plain text format for integration with other tools

## Installation and Setup

### Prerequisites

- .NET 9.0 SDK
- Git repository in the workspace
- VS Code with Copilot
- Access to remote repositories (for remote branch features)

### Building the Project

```powershell
dotnet restore; dotnet build --configuration Release
```

### Running the MCP Server

For development:

```powershell
dotnet run
```

For production (recommended for Copilot integration):

```powershell
$env:DOTNET_ENVIRONMENT="Production"; dotnet run --no-build --verbosity quiet
```

## VS Code Integration

### MCP Configuration

Create or update your MCP configuration to include this server. Here are example configurations:

#### For Development (.vscode/mcp.json)

```json
{
  "mcpServers": {
    "GitVisionMCP": {
      "command": "dotnet",
      "args": ["run", "--project", "c:\\path\\to\\GitVisionMCP.csproj"],
      "env": {
        "DOTNET_ENVIRONMENT": "Production",
        "GIT_REPOSITORY_DIRECTORY": "c:\\Users\\my\\source\\repos\\gitrepo_directory"
      }
    }
  }
}
```

#### For Production (recommended)

```json
{
  "servers": {
    "GitVisionMCP": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "c:\\Users\\U00001\\source\\repos\\MCP\\GitVisionMCP\\GitVisionMCP.csproj",
        "--no-build",
        "--verbosity",
        "quiet"
      ],
      "env": {
        "DOTNET_ENVIRONMENT": "Production",
        "GIT_REPOSITORY_DIRECTORY": "c:\\Users\\my\\source\\repos\\gitrepo_directory"
      }
    },
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
        "-e",
        "GIT_REPOSITORY_DIRECTORY=c:\\Users\\my\\source\\repos\\gitrepo_directory",
        "-v",
        "c:\\Users\\my\\source\\repos\\MCP\\GitVisionMCP:/app/repo:ro",
        "-v",
        "c:\\Users\\my\\source\\repos\\MCP\\GitVisionMCP\\logs:/app/logs",
        "mcprunner/gitvisionmcp:latest"
      ],
      "env": {
        "DOTNET_ENVIRONMENT": "Production"
      }
    }
  }
}
```

### Using with Copilot

Once configured, you can use natural language commands with Copilot:

**📝 Documentation Generation:**

- "Generate documentation from the last 20 commits"
- "Create a release summary comparing main with release/v2.0"
- "Generate project history and save to docs/changelog.md"

**🔍 Search & Discovery:**

- "Search all commits for 'authentication' to find related changes"
- "Find all commits that mention 'bug fix' in messages or code"
- "Look for 'TODO' comments across the entire commit history"
- "Search for 'deprecated' functions and show me where they were used"

**🌿 Branch Analysis:**

- "Compare my feature branch with origin/main and save to analysis.md"
- "Show me what files changed between these two commits"
- "List all remote branches in this repository"
- "Fetch latest changes from origin and compare branches"

**⚡ Advanced Analysis:**

- "Get line-by-line diff for Services/GitService.cs between two commits"
- "Show me recent commits with detailed change information"

## � System Prompts

GitVisionMCP provides specialized system prompts for creating release documentation. These prompts help guide the AI in generating comprehensive and well-structured release notes based on git history.

### Available System Prompts

#### release_document

A prompt that guides the AI in creating a comprehensive release document based on git history.

```json
{
  "prompts": {
    "release_document": true
  }
}
```

Example usage with Copilot:

```
@copilot Using the release_document prompt, create release notes for our project.
```

#### release_document_with_version

A more specific prompt that creates release documentation with explicit version and release date information.

```json
{
  "prompts": {
    "release_document_with_version": {
      "parameters": {
        "version": "1.0.0",
        "releaseDate": "2025-07-06"
      }
    }
  }
}
```

Example usage with Copilot:

```
@copilot Using the release_document_with_version prompt with version 2.0.0 and release date 2025-07-10, create release notes.
```

### Configuring MCP Prompts in mcp.json

To enable MCP prompts in your VS Code environment, add the prompts section to your mcp.json:

```json
{
  "servers": {
    "GitVisionMCP": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "c:\\path\\to\\GitVisionMCP.csproj",
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

## 🚀 Quick Start

### Test the Search Feature

Once the MCP server is running, try these commands to test the powerful search functionality:

#### Copilot Commands

```bash
# Search for "authentication" across all commits
@copilot Search all commits for "authentication" and show me the results

# Find bug-related commits
@copilot Find all commits that mention "fix" in messages or code

# Search for specific API usage
@copilot Look for "HttpClient" usage across commit history

# Security audit search
@copilot Search for "password" or "secret" patterns in all commits
```

#### Direct JSON-RPC Testing

```bash
# Test the search tool directly
echo '{"jsonrpc":"2.0","id":1,"method":"tools/call","params":{"name":"search_commits_for_string","arguments":{"searchString":"authentication","maxCommits":20}}}' | dotnet run --no-build --verbosity quiet
```

### Test Documentation Generation

#### Copilot Commands

```bash
# Generate recent commit documentation
@copilot Generate documentation from the last 10 commits

# Compare branches and save results
@copilot Compare main branch with origin/main and save analysis to sync-check.md

# Create comprehensive git history
@copilot Generate git documentation from last 50 commits and save to docs/project-history.md
```

#### Direct JSON-RPC Testing

```bash
# Test documentation generation
echo '{"jsonrpc":"2.0","id":2,"method":"tools/call","params":{"name":"generate_git_documentation","arguments":{"maxCommits":10,"outputFormat":"markdown"}}}' | dotnet run --no-build --verbosity quiet

# Test branch comparison
echo '{"jsonrpc":"2.0","id":3,"method":"tools/call","params":{"name":"get_all_branches","arguments":{}}}' | dotnet run --no-build --verbosity quiet
```

## Tool Reference

### Core Documentation Tools

#### generate_git_documentation

Generates documentation from recent git commits.

**Parameters:**

- `maxCommits` (optional): Maximum number of commits to include (default: 50)
- `outputFormat` (optional): Output format: markdown, html, or text (default: markdown)

**Example:** Generate documentation from last 25 commits in HTML format

#### generate_git_documentation_to_file

Generates documentation and saves it to a file.

**Parameters:**

- `filePath` (required): Path where to save the documentation file
- `maxCommits` (optional): Maximum number of commits to include (default: 50)
- `outputFormat` (optional): Output format: markdown, html, or text (default: markdown)

**Example:** Save git history to "project-history.md"

### Branch Comparison Tools

#### compare_branches_documentation

Compares two local branches and generates documentation.

**Parameters:**

- `branch1` (required): First branch name
- `branch2` (required): Second branch name
- `filePath` (required): Path where to save the documentation file
- `outputFormat` (optional): Output format: markdown, html, or text (default: markdown)

**Example:** Compare "feature/api" with "main" branch

#### compare_branches_with_remote 🆕

Compares branches with full remote support, including automatic fetching.

**Parameters:**

- `branch1` (required): First branch name (local or remote, e.g., 'main' or 'origin/main')
- `branch2` (required): Second branch name (local or remote, e.g., 'feature/xyz' or 'origin/feature/xyz')
- `filePath` (required): Path where to save the documentation file
- `outputFormat` (optional): Output format: markdown, html, or text (default: markdown)
- `fetchRemote` (optional): Whether to fetch from remote before comparison (default: true)

**Examples:**

- Compare local branch with remote: `("feature/new-api", "origin/main", "analysis.md")`
- Compare two remote branches: `("origin/release/v2.0", "origin/main", "release-diff.md")`

#### compare_commits_documentation

Compares two specific commits and generates documentation.

**Parameters:**

- `commit1` (required): First commit hash
- `commit2` (required): Second commit hash
- `filePath` (required): Path where to save the documentation file
- `outputFormat` (optional): Output format: markdown, html, or text (default: markdown)

**Example:** Compare two commit hashes

### Git Analysis Tools

#### get_recent_commits

Retrieves recent commits with detailed information.

**Parameters:**

- `count` (optional): Number of recent commits to retrieve (default: 10)

**Returns:** List of commits with hash, author, date, and message

#### get_changed_files_between_commits

Lists files that changed between two commits.

**Parameters:**

- `commit1` (required): First commit hash
- `commit2` (required): Second commit hash

**Returns:** List of changed files with change type (added, modified, deleted)

#### get_detailed_diff_between_commits

Gets detailed diff content between two commits.

**Parameters:**

- `commit1` (required): First commit hash
- `commit2` (required): Second commit hash
- `specificFiles` (optional): Array of specific files to diff

**Returns:** Detailed diff content showing exact changes

#### get_commit_diff_info

Gets comprehensive diff statistics between two commits.

**Parameters:**

- `commit1` (required): First commit hash
- `commit2` (required): Second commit hash

**Returns:** Statistics including files changed, insertions, deletions, and file-by-file breakdown

#### get_file_line_diff_between_commits

Gets line-by-line diff for a specific file between two commits.

**Parameters:**

- `commit1` (required): First commit hash
- `commit2` (required): Second commit hash
- `filePath` (required): Path to the file to diff

**Returns:** Detailed line-by-line comparison with syntax highlighting showing added, deleted, and context lines

#### search_commits_for_string

Searches all commits for a specific string and returns detailed match information.

**Parameters:**

- `searchString` (required): The string to search for in commit messages and file contents
- `maxCommits` (optional): Maximum number of commits to search through (default: 100)

**Returns:** Detailed information about each match, including:

- Commit hash, timestamp, author, and message
- File names containing matches
- Line numbers where matches occur
- Full line content showing the match in context

#### search_json_file

Searches for JSON values in a JSON file using JSONPath queries.

**Parameters:**

- `jsonFilePath` (required): Path to the JSON file relative to workspace root
- `jsonPath` (required): JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')
- `indented` (optional): Whether to format the output with indentation (default: true)

**Returns:** JSON values matching the JSONPath query, or "No matches found" if the query returns no results

**Enhanced JSONPath Support:**

- **Wildcards**: `$.users[*].email` - Get all user email addresses
- **Recursive Descent**: `$..author` - Get all author properties at any depth
- **Property Wildcards**: `$.configuration.*` - Get all configuration values
- **Conditional Filtering**: `$.items[?(@.price > 100)].name` - Conditional queries
- **Multiple Results**: Automatically returns arrays for queries with multiple matches
- **Single Results**: Returns individual values when only one match is found

**JSONPath Examples:**

- `$.name` - Get the root-level name property
- `$.users[*].email` - Get all user email addresses (wildcard array access)
- `$.configuration.database.host` - Get nested configuration values
- `$.items[?(@.price > 100)].name` - Get names of items with price > 100 (filtering)
- `$..author` - Get all author properties at any level (recursive)
- `$.configuration.*` - Get all values under configuration (property wildcard)

**Example:** Extract API configuration from settings file

- Summary statistics (total commits searched, matching commits, total line matches)

**Search Capabilities:**

- Case-insensitive search through commit messages and file contents
- Searches all text files in each commit (automatically skips binary files)
- Returns comprehensive match details with exact line numbers and content
- Configurable search depth to control performance on large repositories

### Branch Discovery Tools

#### get_local_branches

Lists all local branches in the repository.

**Returns:** Array of local branch names

#### get_remote_branches

Lists all remote branches in the repository.

**Returns:** Array of remote branch names (e.g., origin/main, upstream/dev)

#### get_all_branches

Lists both local and remote branches.

**Returns:** Comprehensive list of all branches with indicators for local/remote

#### fetch_from_remote

Fetches latest changes from remote repository.

**Parameters:**

- `remoteName` (optional): Name of the remote (default: "origin")

**Returns:** Success message and fetch summary

## Use Cases and Examples

### 1. Release Planning and Analysis

#### Copilot Command:

```bash
@copilot Compare the release/v2.0 branch with main and save the analysis to release-notes.md
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "method": "tools/call",
  "params": {
    "name": "compare_branches_with_remote",
    "arguments": {
      "branch1": "release/v2.0",
      "branch2": "main",
      "filePath": "release-notes.md",
      "outputFormat": "markdown"
    }
  }
}
```

Perfect for understanding what's included in a release and generating release notes.

### 2. Feature Branch Review

#### Copilot Command:

```bash
@copilot Compare my feature/user-authentication branch with origin/main and save to feature-review.md
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 2,
  "method": "tools/call",
  "params": {
    "name": "compare_branches_with_remote",
    "arguments": {
      "branch1": "feature/user-authentication",
      "branch2": "origin/main",
      "filePath": "feature-review.md",
      "fetchRemote": true
    }
  }
}
```

Great for preparing pull requests and understanding the scope of changes.

### 3. Code Review Preparation

#### Copilot Command:

```bash
@copilot Show me what files changed between commits abc123 and def456
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 3,
  "method": "tools/call",
  "params": {
    "name": "get_changed_files_between_commits",
    "arguments": {
      "commit1": "abc123",
      "commit2": "def456"
    }
  }
}
```

Quickly identify which files need attention during code review.

### 4. Team Synchronization

#### Copilot Command:

```bash
@copilot Fetch from origin and compare main with origin/main to see if we're up to date
```

#### JSON-RPC Calls:

```json
{
  "jsonrpc": "2.0",
  "id": 4,
  "method": "tools/call",
  "params": {
    "name": "fetch_from_remote",
    "arguments": {
      "remoteName": "origin"
    }
  }
}
```

Stay synchronized with remote repository changes.

### 5. Historical Analysis

#### Copilot Command:

```bash
@copilot Generate documentation from the last 100 commits and save to project-history.md
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 5,
  "method": "tools/call",
  "params": {
    "name": "generate_git_documentation_to_file",
    "arguments": {
      "filePath": "project-history.md",
      "maxCommits": 100,
      "outputFormat": "markdown"
    }
  }
}
```

Create comprehensive project history documentation.

### 6. Cross-Repository Comparison

#### Copilot Command:

```bash
@copilot Compare origin/main with upstream/main to see differences from the original repo
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 6,
  "method": "tools/call",
  "params": {
    "name": "compare_branches_with_remote",
    "arguments": {
      "branch1": "origin/main",
      "branch2": "upstream/main",
      "filePath": "fork-comparison.md"
    }
  }
}
```

Useful for forks and understanding differences from upstream repositories.

### 7. Advanced Commit Search

#### Copilot Commands:

```bash
# Security audit
@copilot Search all commits for 'password' to find potential security issues

# Bug tracking
@copilot Find all commits that mention 'authentication error' in messages or code

# Feature history
@copilot Search for 'user registration' across all development history

# Code archaeology
@copilot Look for 'deprecated' functions and show me where they were used
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 7,
  "method": "tools/call",
  "params": {
    "name": "search_commits_for_string",
    "arguments": {
      "searchString": "authentication",
      "maxCommits": 100
    }
  }
}
```

Perfect for finding all instances where specific features, bugs, or keywords were addressed across the project history. The search will return:

- Which commits mention the term
- Which files contain the term
- Exact line numbers and content
- Commit timestamps and authors

This is especially useful for:

- **Bug tracking**: Find all commits related to a specific bug or error message
- **Feature history**: Trace the development of a specific feature across time
- **Code review**: Find all instances of deprecated functions or patterns
- **Security audits**: Search for sensitive patterns or keywords
- **Documentation**: Locate all references to specific APIs or configurations

### 8. Configuration Analysis

#### Copilot Command:

```bash
@copilot Search appsettings.json for database configuration using JSONPath $.Database.ConnectionString
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 8,
  "method": "tools/call",
  "params": {
    "name": "search_json_file",
    "arguments": {
      "jsonFilePath": "appsettings.json",
      "jsonPath": "$.Database.ConnectionString",
      "indented": true
    }
  }
}
```

Perfect for extracting configuration values, API keys, or any structured data from JSON files. The search supports:

- **Simple property access**: `$.propertyName`
- **Nested navigation**: `$.level1.level2.property`
- **Array access**: `$.users[0].name` or `$.users[*].email`
- **Complex queries**: `$.items[?(@.price > 100)].name`
- **Wildcard searches**: `$..author` (all author properties at any level)

Use cases include:

- **Configuration validation**: Check environment-specific settings
- **Security audits**: Extract API keys or sensitive configuration
- **Documentation**: Generate configuration references from JSON files
- **Data analysis**: Extract specific data points from JSON datasets

### 9. Line-by-Line Analysis

#### Copilot Command:

```bash
@copilot Get line-by-line diff for Services/GitService.cs between commits abc123 and def456
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 9,
  "method": "tools/call",
  "params": {
    "name": "get_file_line_diff_between_commits",
    "arguments": {
      "commit1": "abc123",
      "commit2": "def456",
      "filePath": "Services/GitService.cs"
    }
  }
}
```

Get detailed line-by-line changes for specific files to understand exact modifications.

## Configuration

The server uses standard .NET configuration with environment-specific settings:

### appsettings.json (Production)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "GitVisionMCP": "Information"
    }
  },
  "Serilog": {
    "Using": ["Serilog.Sinks.File"],
    "MinimumLevel": "Information",
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/gitvisionmcp.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7,
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  },
  "GitVisionMCP": {
    "DefaultMaxCommits": 50,
    "DefaultOutputFormat": "markdown",
    "SupportedFormats": ["markdown", "html", "text"],
    "DefaultRemoteName": "origin"
  }
}
```

### appsettings.Development.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "GitVisionMCP": "Debug"
    }
  },
  "Serilog": {
    "MinimumLevel": "Debug",
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/gitvisionmcp-dev.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7,
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}
```

## Architecture

The project follows clean architecture principles:

### Core Components

- **Models/**: JSON-RPC and MCP data models
- **Services/**: Core business logic
  - `GitService`: Handles git operations, remote branches, and documentation generation
  - `McpServer`: Implements the MCP JSON-RPC 2.0 protocol
- **Program.cs**: Application entry point with dependency injection

### Key Features

- **Robust Error Handling**: Comprehensive error handling for all git operations
- **Remote Branch Support**: Full support for remote repository operations
- **Flexible Output**: Multiple output formats (Markdown, HTML, Text)
- **Configurable**: Environment-based configuration with sensible defaults
- **Logging**: File-based logging to avoid JSON-RPC interference

## Dependencies

- **LibGit2Sharp**: For comprehensive git operations including remote branches
- **Microsoft.Extensions.\*\*\***: For logging, configuration, and dependency injection
- **System.Text.Json**: For JSON serialization with optimal performance
- **Serilog.Extensions.Logging.File**: For file-based logging with rotation

## Development

### Logging Strategy

The application uses Serilog with file output to avoid interfering with JSON-RPC communication:

- **Production**: Logs written to `logs/gitvisionmcp.log` at Information level
- **Development**: Logs written to `logs/gitvisionmcp-dev.log` at Debug level
- **Log Rotation**: Daily log files with 7-day retention
- **Output Isolation**: No console logging to ensure clean JSON-RPC communication

### Error Handling

All tools include comprehensive error handling:

- Git operation failures (repository not found, invalid refs, etc.)
- Remote access issues (network problems, authentication)
- File system errors (permissions, disk space)
- Invalid parameters and edge cases

### Testing

Test the server manually:

```powershell
# Set production environment
$env:DOTNET_ENVIRONMENT="Production"

# Test basic communication
echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}' | dotnet run --no-build --verbosity quiet

# Test tools list
echo '{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}' | dotnet run --no-build --verbosity quiet
```

- **Production**: Logs written to `logs/gitvisionmcp.log` at Information level
- **Development**: Logs written to `logs/gitvisionmcp-dev.log` at Debug level
- **Log Configuration**: Configurable via `appsettings.json` and environment-specific files
- **Log Rotation**: Daily log files with automatic cleanup (via Serilog.Extensions.Logging.File)

The logs directory is automatically created and ignored by git (.gitignore).

- Production: Information level
- Development: Debug/Trace level

## Troubleshooting

### Common Issues and Solutions

#### "Failed to parse message" warnings

If you see warnings like:

```
[warning] Failed to parse message: "Using launch settings from..."
[warning] Failed to parse message: "Building..."
[warning] Failed to parse message: "info: Program[0]"
```

**Solution**: Build messages or logging output is interfering with JSON-RPC communication.

1. **Build the project first**:

   ```powershell
   dotnet build --configuration Release
   ```

2. **Use the correct MCP configuration** (production settings):

   ```json
   {
     "mcpServers": {
       "GitVisionMCP": {
         "command": "dotnet",
         "args": [
           "run",
           "--project",
           "c:\\path\\to\\GitVisionMCP.csproj",
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

3. **Verify clean output**:
   ```powershell
   $env:DOTNET_ENVIRONMENT="Production"
   echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{}}' | dotnet run --no-build --verbosity quiet
   ```
   You should see only JSON output, no log messages.

#### Remote branch access issues

**Error**: "Remote branch not found" or "Authentication failed"

**Solutions**:

1. **Verify remote access**:

   ```powershell
   git remote -v
   git ls-remote origin
   ```

2. **Fetch latest remote references**:

   ```powershell
   git fetch --all
   ```

3. **Check authentication** (for private repositories):
   - Ensure SSH keys are configured
   - Or use personal access tokens for HTTPS

#### Invalid branch or commit references

**Error**: "Branch not found" or "Invalid commit hash"

**Solutions**:

1. **List available branches**:
   Use the `get_all_branches` tool to see available branches

2. **Verify commit hashes**:

   ```powershell
   git log --oneline -10
   ```

3. **Use correct branch names**:
   - Local: `main`, `feature/api`
   - Remote: `origin/main`, `upstream/dev`

#### Performance issues with large repositories

**Issue**: Slow responses or timeouts

**Solutions**:

1. **Limit commit count**: Use smaller `maxCommits` values (10-50)
2. **Use specific file filters**: When getting diffs, specify `specificFiles`
3. **Fetch selectively**: Use specific remote names instead of fetching all

#### Permission issues

**Error**: "Access denied" or "Permission denied"

**Solutions**:

1. **Check file permissions**: Ensure write access to output directory
2. **Run as administrator**: If needed for system directories
3. **Use relative paths**: Avoid system directories, use workspace-relative paths

### Performance Tips

1. **Start with smaller datasets**: Use lower `maxCommits` values initially
2. **Use caching**: The server caches git operations within a session
3. **Fetch strategically**: Only fetch when comparing with remote branches
4. **Monitor logs**: Check log files for performance insights

### Debug Mode

For detailed debugging, use development environment:

```powershell
$env:DOTNET_ENVIRONMENT="Development"
dotnet run
```

This enables detailed logging to `logs/gitvisionmcp-dev.log`.

## Best Practices

### Branch Naming

- Use descriptive branch names: `feature/user-auth`, `bugfix/login-error`
- Follow team conventions for remote references

### Output Organization

- Create dedicated documentation folders: `docs/`, `analysis/`
- Use timestamp prefixes for historical comparisons: `2024-01-15-release-analysis.md`

### Remote Repository Management

- Regularly fetch remote changes: `fetch_from_remote`
- Keep local branches synchronized with remote counterparts
- Use meaningful remote names: `origin`, `upstream`, `fork`

## License

This project is open source. Please refer to the license file for details.

## Contributing

Contributions are welcome! Please follow these guidelines:

1. **Code Standards**: Follow standard .NET coding practices
2. **Testing**: Include tests for new features
3. **Documentation**: Update README and inline documentation
4. **Remote Support**: Ensure new features work with remote repositories
5. **Error Handling**: Include comprehensive error handling

### Development Setup

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/new-tool`
3. Make changes and test thoroughly
4. Update documentation
5. Submit a pull request

## Roadmap

Future enhancements planned:

- **Advanced Filtering**: Filter commits by author, date range, file patterns
- **Integration Support**: Export to external documentation systems
- **Interactive Mode**: Command-line interface for direct usage
- **Performance Optimization**: Caching and incremental updates
- **Multi-Repository Support**: Compare across different repositories

## Support

For issues, feature requests, or questions:

1. Check the troubleshooting section above
2. Review log files (`logs/` directory)
3. Create an issue with detailed information including:
   - Environment details (.NET version, OS)
   - Git repository structure
   - Error messages and log excerpts
   - Steps to reproduce
