# MCP Integration Examples

This document shows comprehensive examples of how to integrate and use the GitVisionMCP server with VS Code and Copilot, including **advanced commit search capabilities** and remote branch features.

## 🔥 Featured: Advanced Commit Search

The latest enhancement includes powerful commit search functionality that revolutionizes repository analysis:

- **Deep Search**: Search through commit messages AND file contents simultaneously
- **Comprehensive Results**: Get commit details, file locations, line numbers, and content
- **Performance Optimized**: Smart filtering and configurable search depth
- **Rich Output**: Detailed markdown reports with match summaries and statistics

### Quick Search Examples

```bash
@copilot Search all commits for "authentication" and show detailed results
@copilot Find commits mentioning "bug fix" in messages or code
@copilot Look for "TODO" comments across entire commit history
@copilot Search for "deprecated" functions with line details
```

## VS Code Configuration

### 1. Production MCP Server Configuration (Recommended)

Add this to your VS Code MCP configuration file:

```json
{
  "mcpServers": {
    "GitVisionMCP": {
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "c:\\path\\to\\GitVisionMCP\\GitVisionMCP.csproj",
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

### 2. Development Configuration

For development and debugging:

```json
{
  "mcpServers": {
    "GitVisionMCP": {
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "c:\\path\\to\\GitVisionMCP\\GitVisionMCP.csproj"
      ],
      "env": {
        "DOTNET_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Using with Copilot

Once configured, you can use natural language commands with Copilot:

### Core Documentation Commands

#### Generate Git Documentation

```
@copilot Generate documentation from the last 20 git commits
@copilot Create a summary of recent changes in HTML format
@copilot Show me the git history for the past 50 commits
```

#### Save Documentation to File

```
@copilot Generate git documentation and save it to docs/project-history.md
@copilot Create a change log from the last 30 commits and save to CHANGELOG.md
@copilot Export git history to docs/development-summary.html in HTML format
```

### Branch Comparison Commands

#### Local Branch Comparison

```
@copilot Compare changes between main and feature-branch and save to docs/branch-diff.md
@copilot Show differences between dev and release branches
@copilot Compare my current branch with main and save the analysis
```

#### Remote Branch Comparison (🆕 New Features)

```
@copilot Compare my feature branch with origin/main and save to analysis.md
@copilot Show differences between origin/release/v2.0 and origin/main
@copilot Compare local main with origin/main to check if we're synchronized
@copilot Fetch from origin and compare branches with remote support
```

### Git Analysis Commands

#### Recent Commits Analysis

```
@copilot Show me the last 10 commits with details
@copilot Get recent commit information for code review
@copilot List the most recent commits in this repository
```

#### File Change Analysis

```
@copilot Show me what files changed between these two commits: abc123 and def456
@copilot List all files modified between commit hashes
@copilot Get detailed diff between two specific commits
```

#### Branch Discovery

```
@copilot List all branches in this repository
@copilot Show me all remote branches
@copilot What local branches do we have?
@copilot Fetch latest changes from origin
```

### 🔥 Commit and Code Search (Advanced)

**Basic Search Commands:**

```
@copilot Search all commits for "authentication" to find related changes
@copilot Find all commits that mention "bug fix" in messages or code
@copilot Search for "deprecated" across all commit history
@copilot Look for "TODO" comments in all commits and show me where they are
@copilot Find commits containing "API" and show detailed line matches
```

**Advanced Search Use Cases:**

```
@copilot Search last 50 commits for "HttpClient" usage with detailed line info
@copilot Find all instances of "password" in commit history for security audit
@copilot Search for "Exception" in messages and code to track error handling
@copilot Look for "database" references across development history
@copilot Find commits mentioning "performance" with full context details
```

**Targeted Search Examples:**

```
@copilot Search recent 25 commits for "refactor" to see code improvements
@copilot Find all commits with "config" changes in last 100 commits
@copilot Search for "test" additions across entire project history
@copilot Look for "security" mentions with line-by-line details
```

**Results Include:**

- Commit hash, author, and timestamp
- Exact file names containing matches
- Line numbers and full line content
- Summary statistics (commits searched, matches found)
- File-by-file breakdown with detailed context

### Read File Contents:

```
@copilot Read all C# files in the Services directory and show their contents
@copilot Get content of all JSON configuration files
@copilot Read all markdown files and show their content
@copilot Show me the contents of all files modified today
```

**Advanced Content Analysis:**

```
@copilot Read all TypeScript files and analyze the code structure
@copilot Get contents of all configuration files for review
@copilot Show me all SQL files and their queries
@copilot Read recent test files and analyze test coverage
```

### 🆕 Workspace File Management

**List All Workspace Files:**

```
@copilot List all files in the current workspace
@copilot Show me all files in the project with their details
@copilot Get a complete file listing of the workspace
```

**Filtered File Searches:**

```
@copilot List all .cs files in the workspace
@copilot Show me all JSON configuration files
@copilot Find all files in the Services directory
@copilot List files modified in the last week
@copilot Show me all files larger than 1MB
```

**Advanced File Analysis:**

```
@copilot List all .dll files and their sizes
@copilot Find all files with "test" in their path
@copilot Show me recent changes to documentation files
@copilot List all executable files in the project
```

**Export and Documentation:**

```
@copilot Export workspace file listing to XML
@copilot Create a file inventory report
@copilot Generate project structure documentation
```

### Advanced Use Cases

#### Release Planning

```
@copilot Compare release/v2.0 with main and create release notes
@copilot Analyze differences between our release branch and main
@copilot Generate documentation for the upcoming release
```

#### Code Review Preparation

```
@copilot Compare my feature/user-auth branch with origin/main for code review
@copilot Show me comprehensive diff information between two commits
@copilot Prepare documentation for pull request review
```

#### Team Synchronization

```
@copilot Fetch from origin and compare main with origin/main
@copilot Check if our local branches are up to date with remote
@copilot Compare upstream/main with our fork's main branch
```

#### Project Analysis and File Management

```
@copilot List all files in the workspace and analyze the project structure
@copilot Show me all C# files and their sizes for code review
@copilot Find all configuration files that might need updates
@copilot Generate a complete project file inventory
```

## Manual Testing

### JSON-RPC Examples

#### Initialize the Server

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "method": "initialize",
  "params": {
    "protocolVersion": "2024-11-05",
    "capabilities": {
      "roots": {
        "listChanged": true
      }
    },
    "clientInfo": {
      "name": "vscode",
      "version": "1.0.0"
    }
  }
}
```

#### List Available Tools

```json
{
  "jsonrpc": "2.0",
  "id": 2,
  "method": "tools/list",
  "params": {}
}
```

### Core Documentation Tools

#### Generate Documentation

```json
{
  "jsonrpc": "2.0",
  "id": 3,
  "method": "tools/call",
  "params": {
    "name": "generate_git_documentation",
    "arguments": {
      "maxCommits": 10,
      "outputFormat": "markdown"
    }
  }
}
```

#### Save Documentation to File

```json
{
  "jsonrpc": "2.0",
  "id": 4,
  "method": "tools/call",
  "params": {
    "name": "generate_git_documentation_to_file",
    "arguments": {
      "filePath": "docs/git-history.md",
      "maxCommits": 25,
      "outputFormat": "markdown"
    }
  }
}
```

### Branch Comparison Tools

#### Compare Local Branches

```json
{
  "jsonrpc": "2.0",
  "id": 5,
  "method": "tools/call",
  "params": {
    "name": "compare_branches_documentation",
    "arguments": {
      "branch1": "main",
      "branch2": "feature-branch",
      "filePath": "docs/branch-comparison.md",
      "outputFormat": "html"
    }
  }
}
```

#### Compare with Remote Branches (🆕 New)

```json
{
  "jsonrpc": "2.0",
  "id": 6,
  "method": "tools/call",
  "params": {
    "name": "compare_branches_with_remote",
    "arguments": {
      "branch1": "feature/user-auth",
      "branch2": "origin/main",
      "filePath": "docs/remote-comparison.md",
      "outputFormat": "markdown",
      "fetchRemote": true
    }
  }
}
```

### Git Analysis Tools

#### Get Recent Commits

```json
{
  "jsonrpc": "2.0",
  "id": 7,
  "method": "tools/call",
  "params": {
    "name": "get_recent_commits",
    "arguments": {
      "count": 15
    }
  }
}
```

#### 🔥 Search Commits for String (Advanced Search)

**Basic Search Example:**

```json
{
  "jsonrpc": "2.0",
  "id": 8,
  "method": "tools/call",
  "params": {
    "name": "search_commits_for_string",
    "arguments": {
      "searchString": "authentication",
      "maxCommits": 50
    }
  }
}
```

**Advanced Search Examples:**

```json
{
  "jsonrpc": "2.0",
  "id": 8,
  "method": "tools/call",
  "params": {
    "name": "search_commits_for_string",
    "arguments": {
      "searchString": "bug fix",
      "maxCommits": 100
    }
  }
}
```

**Security Audit Search:**

```json
{
  "jsonrpc": "2.0",
  "id": 8,
  "method": "tools/call",
  "params": {
    "name": "search_commits_for_string",
    "arguments": {
      "searchString": "password",
      "maxCommits": 200
    }
  }
}
```

**Expected Response Format:**

- Search summary (commits searched, matches found, total line matches)
- Per-commit details (hash, author, timestamp, message)
- File-by-file breakdown (file names, match counts)
- Line-level details (line numbers, full content, exact matches)

## 🆕 Enhanced JSON Search Examples

The JSON search tool now supports advanced features including wildcard patterns and structured results with path context.

### Extract Configuration Values

#### Copilot Command:

```bash
@copilot Search appsettings.json for the database connection string using JSONPath $.Database.ConnectionString
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 9,
  "method": "tools/call",
  "params": {
    "name": "search_json_file",
    "arguments": {
      "jsonFilePath": "appsettings.json",
      "jsonPath": "$.Database.ConnectionString",
      "indented": true,
      "showKeyPaths": false
    }
  }
}
```

### Extract Multiple Values with Wildcards

#### Copilot Command:

```bash
@copilot Get all user email addresses from users.json using JSONPath $.users[*].email
```

#### JSON-RPC Call (Basic Results):

```json
{
  "jsonrpc": "2.0",
  "id": 10,
  "method": "tools/call",
  "params": {
    "name": "search_json_file",
    "arguments": {
      "jsonFilePath": "data/users.json",
      "jsonPath": "$.users[*].email",
      "indented": true,
      "showKeyPaths": false
    }
  }
}
```

**Response:**

```json
["john@example.com", "jane@example.com"]
```

#### JSON-RPC Call (Structured Results with Path Context):

```json
{
  "jsonrpc": "2.0",
  "id": 11,
  "method": "tools/call",
  "params": {
    "name": "search_json_file",
    "arguments": {
      "jsonFilePath": "data/users.json",
      "jsonPath": "$.users[*].email",
      "indented": true,
      "showKeyPaths": true
    }
  }
}
```

**Response:**

```json
[
  {
    "path": "users[0].email",
    "value": "john@example.com",
    "key": "email"
  },
  {
    "path": "users[1].email",
    "value": "jane@example.com",
    "key": "email"
  }
]
```

### Advanced JSONPath Patterns

#### Recursive Search with Wildcards

```bash
@copilot Find all email addresses anywhere in config.json using JSONPath $..email
```

```json
{
  "jsonrpc": "2.0",
  "id": 12,
  "method": "tools/call",
  "params": {
    "name": "search_json_file",
    "arguments": {
      "jsonFilePath": "config.json",
      "jsonPath": "$..email",
      "indented": true,
      "showKeyPaths": true
    }
  }
}
```

#### Property Wildcards

```bash
@copilot Get all configuration values from settings.json using JSONPath $.configuration.*
```

```json
{
  "jsonrpc": "2.0",
  "id": 13,
  "method": "tools/call",
  "params": {
    "name": "search_json_file",
    "arguments": {
      "jsonFilePath": "settings.json",
      "jsonPath": "$.configuration.*",
      "indented": true,
      "showKeyPaths": true
    }
  }
}
```

### Complex JSONPath Queries with Filtering

#### Copilot Command:

```bash
@copilot Find all products with price over 100 from catalog.json using JSONPath $.products[?(@.price > 100)].name
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 14,
  "method": "tools/call",
  "params": {
    "name": "search_json_file",
    "arguments": {
      "jsonFilePath": "catalog.json",
      "jsonPath": "$.products[?(@.price > 100)].name",
      "indented": true,
      "showKeyPaths": false
    }
  }
}
```

### Use Cases for showKeyPaths

The `showKeyPaths` parameter is particularly useful when you need to:

- **Track data sources**: Know exactly where each value came from in complex JSON
- **Generate reports**: Create structured summaries with location context
- **Debug configurations**: Identify the path to problematic settings
- **Data migration**: Map old structure paths to new structure requirements

**Expected Response Formats:**

- **showKeyPaths=false**: Direct JSON values (arrays for multiple results)
- **showKeyPaths=true**: Structured objects with `path`, `value`, and `key` properties
- **Single results**: Individual values or objects
- **Multiple results**: Arrays of values or structured objects
- **No matches**: "No matches found" message
- **Errors**: Descriptive error messages for invalid JSON or malformed JSONPath queries

#### Get Changed Files Between Commits

```json
{
  "jsonrpc": "2.0",
  "id": 9,
  "method": "tools/call",
  "params": {
    "name": "get_changed_files_between_commits",
    "arguments": {
      "commit1": "abc123def",
      "commit2": "456ghi789"
    }
  }
}
```

#### Get Detailed Diff

```json
{
  "jsonrpc": "2.0",
  "id": 10,
  "method": "tools/call",
  "params": {
    "name": "get_detailed_diff_between_commits",
    "arguments": {
      "commit1": "abc123def",
      "commit2": "456ghi789",
      "specificFiles": ["Program.cs", "Services/GitService.cs"]
    }
  }
}
```

#### Get Comprehensive Diff Info

```json
{
  "jsonrpc": "2.0",
  "id": 10,
  "method": "tools/call",
  "params": {
    "name": "get_commit_diff_info",
    "arguments": {
      "commit1": "abc123def",
      "commit2": "456ghi789"
    }
  }
}
```

#### Get File Line Diff

```json
{
  "jsonrpc": "2.0",
  "id": 11,
  "method": "tools/call",
  "params": {
    "name": "get_file_line_diff_between_commits",
    "arguments": {
      "commit1": "abc123def",
      "commit2": "456ghi789",
      "filePath": "Services/GitService.cs"
    }
  }
}
```

## 🆕 Enhanced XML Search Examples

The XML search tool provides powerful XML querying capabilities using XPath with comprehensive element and attribute support.

### Extract Configuration Values

#### Copilot Command:

```bash
@copilot Search test-config.xml for the database connection string using XPath //database/connectionString
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 12,
  "method": "tools/call",
  "params": {
    "name": "search_xml_file",
    "arguments": {
      "xmlFilePath": "test-config.xml",
      "xPath": "//database/connectionString",
      "indented": true
    }
  }
}
```

#### Expected Response:

```json
{
  "jsonrpc": "2.0",
  "id": 12,
  "result": {
    "content": [
      {
        "type": "text",
        "text": "<connectionString>Server=localhost;Database=TestDB</connectionString>"
      }
    ],
    "isError": false
  }
}
```

### Extract Multiple Values with Element Queries

#### Copilot Command:

```bash
@copilot Search test-config.xml for all user elements with path context using XPath //user
```

#### JSON-RPC Call (Structured Results with Path Context):

```json
{
  "jsonrpc": "2.0",
  "id": 13,
  "method": "tools/call",
  "params": {
    "name": "search_xml_file",
    "arguments": {
      "xmlFilePath": "test-config.xml",
      "xPath": "//user",
      "indented": true,
      "showKeyPaths": true
    }
  }
}
```

#### Expected Response:

```json
{
  "jsonrpc": "2.0",
  "id": 13,
  "result": {
    "content": [
      {
        "type": "text",
        "text": "[\n  {\n    \"path\": \"/configuration/users/user\",\n    \"value\": \"<user id=\\\"1\\\" name=\\\"John Doe\\\" email=\\\"john@example.com\\\" />\",\n    \"key\": \"user\"\n  },\n  {\n    \"path\": \"/configuration/users/user[2]\",\n    \"value\": \"<user id=\\\"2\\\" name=\\\"Jane Smith\\\" email=\\\"jane@example.com\\\" />\",\n    \"key\": \"user\"\n  }\n]"
      }
    ],
    "isError": false
  }
}
```

### Advanced XPath Patterns

#### Filter Features by Attribute

```json
{
  "jsonrpc": "2.0",
  "id": 14,
  "method": "tools/call",
  "params": {
    "name": "search_xml_file",
    "arguments": {
      "xmlFilePath": "test-config.xml",
      "xPath": "//feature[@enabled='true']",
      "showKeyPaths": true
    }
  }
}
```

#### Get All Email Attributes

```json
{
  "jsonrpc": "2.0",
  "id": 15,
  "method": "tools/call",
  "params": {
    "name": "search_xml_file",
    "arguments": {
      "xmlFilePath": "test-config.xml",
      "xPath": "//user/@email",
      "indented": true
    }
  }
}
```

### Complex XPath Queries with Filtering

#### Get User by Specific ID

```json
{
  "jsonrpc": "2.0",
  "id": 16,
  "method": "tools/call",
  "params": {
    "name": "search_xml_file",
    "arguments": {
      "xmlFilePath": "test-config.xml",
      "xPath": "//user[@id='1']/@name"
    }
  }
}
```

### Use Cases for showKeyPaths

The `showKeyPaths` parameter is particularly useful when you need to:

- **Track element sources**: Know exactly where each XML element originated in complex documents
- **Generate reports**: Create structured summaries with XPath location context
- **Debug configurations**: Identify the path to problematic XML elements or attributes
- **Data migration**: Map old XML structure paths to new schema requirements

**Expected Response Formats:**

- **showKeyPaths=false**: Direct XML content (arrays for multiple results)
- **showKeyPaths=true**: Structured objects with `path`, `value`, and `key` properties
- **Single results**: Individual XML elements or attributes
- **Multiple results**: Arrays of XML elements or structured objects
- **No matches**: "No matches found" message
- **Errors**: Descriptive error messages for invalid XML or malformed XPath queries

#### JSONPath vs XPath Comparison Table

| Operation | JSON (JSONPath) | XML (XPath) | Description |
|-----------|-----------------|-------------|-------------|
| All users | `$.users[*]` | `//user` | Get all user records |
| User emails | `$.users[*].email` | `//user/@email` | Get all user email addresses |
| First user | `$.users[0]` | `//user[1]` | Get first user (XPath is 1-indexed) |
| Filter by attribute | `$.users[?(@.active)]` | `//user[@active='true']` | Filter by condition |
| Nested elements | `$.config.database.host` | `//config/database/host` | Navigate nested structure |
| All children | `$.settings.*` | `/configuration/settings/*` | Get all direct children |

### Branch Discovery Tools

#### Get All Branches

```json
{
  "jsonrpc": "2.0",
  "id": 11,
  "method": "tools/call",
  "params": {
    "name": "get_all_branches",
    "arguments": {}
  }
}
```

#### Get Local Branches Only

```json
{
  "jsonrpc": "2.0",
  "id": 12,
  "method": "tools/call",
  "params": {
    "name": "get_local_branches",
    "arguments": {}
  }
}
```

#### Get Remote Branches Only

```json
{
  "jsonrpc": "2.0",
  "id": 13,
  "method": "tools/call",
  "params": {
    "name": "get_remote_branches",
    "arguments": {}
  }
}
```

#### Fetch from Remote

```json
{
  "jsonrpc": "2.0",
  "id": 14,
  "method": "tools/call",
  "params": {
    "name": "fetch_from_remote",
    "arguments": {
      "remoteName": "origin"
    }
  }
}
```

### Commit Comparison

#### Compare Commits

```json
{
  "jsonrpc": "2.0",
  "id": 15,
  "method": "tools/call",
  "params": {
    "name": "compare_commits_documentation",
    "arguments": {
      "commit1": "abc123def",
      "commit2": "456ghi789",
      "filePath": "docs/commit-comparison.txt",
      "outputFormat": "text"
    }
  }
}
```

#### 🆕 List Workspace Files

**Basic File Listing:**

```json
{
  "jsonrpc": "2.0",
  "id": 16,
  "method": "tools/call",
  "params": {
    "name": "list_workspace_files",
    "arguments": {}
  }
}
```

**Filter by File Type:**

```json
{
  "jsonrpc": "2.0",
  "id": 17,
  "method": "tools/call",
  "params": {
    "name": "list_workspace_files",
    "arguments": {
      "fileType": "cs"
    }
  }
}
```

**Filter by Path:**

```json
{
  "jsonrpc": "2.0",
  "id": 18,
  "method": "tools/call",
  "params": {
    "name": "list_workspace_files",
    "arguments": {
      "relativePath": "Services",
      "fileType": "cs"
    }
  }
}
```

**Filter by Date Range:**

```json
{
  "jsonrpc": "2.0",
  "id": 19,
  "method": "tools/call",
  "params": {
    "name": "list_workspace_files",
    "arguments": {
      "lastModifiedAfter": "2025-07-01",
      "lastModifiedBefore": "2025-07-10"
    }
  }
}
```

**Advanced Filtering:**

```json
{
  "jsonrpc": "2.0",
  "id": 20,
  "method": "tools/call",
  "params": {
    "name": "list_workspace_files",
    "arguments": {
      "fileType": "json",
      "relativePath": "appsettings",
      "lastModifiedAfter": "2025-07-01"
    }
  }
}
```

#### 🆕 Read Filtered File Contents

**Read All C# Files:**

```json
{
  "jsonrpc": "2.0",
  "id": 21,
  "method": "tools/call",
  "params": {
    "name": "read_filtered_workspace_files",
    "arguments": {
      "fileType": "cs",
      "maxFiles": 10
    }
  }
}
```

**Read Recent Configuration Files:**

```json
{
  "jsonrpc": "2.0",
  "id": 22,
  "method": "tools/call",
  "params": {
    "name": "read_filtered_workspace_files",
    "arguments": {
      "fileType": "json",
      "relativePath": "appsettings",
      "lastModifiedAfter": "2025-07-01",
      "maxFiles": 5,
      "maxFileSize": 102400
    }
  }
}
```

**Read Files from Specific Directory:**

```json
{
  "jsonrpc": "2.0",
  "id": 23,
  "method": "tools/call",
  "params": {
    "name": "read_filtered_workspace_files",
    "arguments": {
      "relativePath": "Services",
      "fileType": "cs",
      "maxFiles": 20
    }
  }
}
```

### Workspace File Listing Response

```json
{
  "jsonrpc": "2.0",
  "id": 16,
  "result": {
    "content": [
      {
        "type": "text",
        "text": "[{\"RelativePath\":\"Program.cs\",\"FileType\":\"cs\",\"FullPath\":\"C:\\\\Users\\\\U00001\\\\source\\\\repos\\\\MCP\\\\GitVisionMCP\\\\Program.cs\",\"Size\":2048,\"LastModified\":\"2025-07-10T14:30:00\"},{\"RelativePath\":\"appsettings.json\",\"FileType\":\"json\",\"FullPath\":\"C:\\\\Users\\\\U00001\\\\source\\\\repos\\\\MCP\\\\GitVisionMCP\\\\appsettings.json\",\"Size\":512,\"LastModified\":\"2025-07-09T10:15:00\"},{\"RelativePath\":\"Services\\\\GitService.cs\",\"FileType\":\"cs\",\"FullPath\":\"C:\\\\Users\\\\U00001\\\\source\\\\repos\\\\MCP\\\\GitVisionMCP\\\\Services\\\\GitService.cs\",\"Size\":15360,\"LastModified\":\"2025-07-10T16:45:00\"}]"
      }
    ],
    "isError": false
  }
}
```

### File Content Reading Response

```json
{
  "jsonrpc": "2.0",
  "id": 21,
  "result": {
    "content": [
      {
        "type": "text",
        "text": "[{\"RelativePath\":\"Program.cs\",\"FullPath\":\"C:\\\\Users\\\\U00001\\\\source\\\\repos\\\\MCP\\\\GitVisionMCP\\\\Program.cs\",\"FileType\":\"cs\",\"Size\":2048,\"LastModified\":\"2025-07-10T14:30:00\",\"Content\":\"using Microsoft.Extensions.Hosting;\\nusing GitVisionMCP.Services;\\n\\nvar builder = Host.CreateApplicationBuilder(args);\\n\\n// Add services\\nbuilder.Services.AddSingleton<IGitService, GitService>();\\nbuilder.Services.AddSingleton<ILocationService, LocationService>();\\nbuilder.Services.AddSingleton<IMcpServer, McpServer>();\\n\\nvar host = builder.Build();\\nvar mcpServer = host.Services.GetRequiredService<IMcpServer>();\\n\\nawait mcpServer.StartAsync();\\n\",\"ErrorMessage\":null,\"IsError\":false},{\"RelativePath\":\"appsettings.json\",\"FullPath\":\"C:\\\\Users\\\\U00001\\\\source\\\\repos\\\\MCP\\\\GitVisionMCP\\\\appsettings.json\",\"FileType\":\"json\",\"Size\":512,\"LastModified\":\"2025-07-09T10:15:00\",\"Content\":\"{\\n  \\\"Logging\\\": {\\n    \\\"LogLevel\\\": {\\n      \\\"Default\\\": \\\"Information\\\",\\n      \\\"Microsoft.AspNetCore\\\": \\\"Warning\\\"\\n    }\\n  }\\n}\",\"ErrorMessage\":null,\"IsError\":false}]"
      }
    ],
    "isError": false
  }
}
```

### Git Documentation Response

```json
{
  "jsonrpc": "2.0",
  "id": 3,
  "result": {
    "content": [
      {
        "type": "text",
        "text": "# Git Documentation\n\n## Recent Changes\n\n### Commit abc123def\n\n- **Author**: John Doe <john.doe@example.com>\n- **Date**: 2025-07-10 14:30:00\n- **Message**: Fix issue with authentication flow\n\n#### Changed Files:\n\n- `Program.cs`\n- `appsettings.json`\n\n#### Detailed Changes:\n\n- **Program.cs**:\n  - Line 42: `if (user == null) {` changed to `if (user == null) throw new Exception(\"User not found\");`\n  - Line 56: Added null check for `token`\n\n- **appsettings.json**:\n  - Updated connection string for production\n\n### Commit 456ghi789\n\n- **Author**: Jane Smith <jane.smith@example.com>\n- **Date**: 2025-07-09 10:15:00\n- **Message**: Update README and fix typos\n\n#### Changed Files:\n\n- `README.md`\n\n#### Detailed Changes:\n\n- **README.md**:\n  - Line 10: Fixed typo in installation instructions\n  - Line 25: Updated link to documentation\n\n## Commit Search Results\n\n### Search Query: \"authentication\"\n\n- **Commit abc123def**:\n  - **Message**: Fix issue with authentication flow\n  - **Files Changed**: Program.cs, appsettings.json\n  - **Line Matches**:\n    - Program.cs:42: `if (user == null) {`\n    - Program.cs:56: `token`\n\n### Search Query: \"bug fix\"\n\n- **Commit 789jkl012**:\n  - **Message**: Bug fix for null reference exception\n  - **Files Changed**: Services/GitService.cs\n  - **Line Matches**:\n    - Services/GitService.cs:78: `if (data == null) {`\n\n### Search Query: \"TODO\"\n\n- **Commit mno345pqr**:\n  - **Message**: TODO comments cleanup\n  - **Files Changed**: All files\n  - **Line Matches**:\n    - Program.cs:10: `// TODO: Implement logging`\n    - Services/GitService.cs:20: `// TODO: Handle exceptions`\n\n## Statistics\n\n- **Total Commits Searched**: 100\n- **Total Matches Found**: 15\n- **Files Involved**: 10\n- **Lines Involved**: 25\n\n---\n\nGenerated by GitVisionMCP",
        "isError": false
      }
    ]
  }
}
```

# Test workspace file listing

Test-JsonRpc '{"jsonrpc":"2.0","id":6,"method":"tools/call","params":{"name":"list_workspace_files","arguments":{"fileType":"cs"}}}' '"content"'

# Test reading file contents

Test-JsonRpc '{"jsonrpc":"2.0","id":7,"method":"tools/call","params":{"name":"read_filtered_workspace_files","arguments":{"fileType":"json","maxFiles":3}}}' '"content"'

Write-Host "Testing complete!" -ForegroundColor Green
