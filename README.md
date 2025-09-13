# GitVisionMCP 1.0.9.2

> **🚀 Fully Automated Model Context Protocol (MCP) Server for Git Analysis & Documentation**

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![MCP](https://img.shields.io/badge/protocol-MCP-purple)](https://modelcontextprotocol.io/)

---

## About

GitVisionMCP is designed to enhance the productivity of software development teams by providing powerful tools for git analysis and source documentation. By leveraging the Model Context Protocol (MCP), it enables seamless integration with various APIs and services, streamlining workflows and improving collaboration.

## GitVisionMCP Benefits

1. **_Cost Reduction_** by minimizing tokens sent to your LLM. Instead of uploading entire files, only relevant snippets are sent.
1. **_Enhanced LLM Security_** by restricting access to sensitive data, including source code and configuration files. Deconstructor and Search capabilities help achieve this.
1. **_Facilitated Supportability_** by automating documentation for code, processes, and workflows.

## Table of Contents

1. [Quick Start](#quick-start)
2. [Core Features](#core-features)
3. [Available Tools](#available-tools)
4. [Getting Started](#getting-started)
5. [Configuration](#configuration)
6. [Advanced Usage](#advanced-usage)
7. [Documentation](#documentation)

---

## Quick Start

GitVisionMCP provides **35 powerful tools** for git analysis, documentation generation, and file searching:

- 🔍 **Search commits**, branches, and files with advanced filtering
- 📝 **Generate documentation** from git history automatically
- 🌿 **Compare branches** and commits with detailed analysis
- 📊 **Query files** using JSONPath (JSON/YAML/CSV/Excel) and XPath (XML)
- ⚡ **Smart workspace** operations with configurable exclusions

**Basic Commands:**

```bash
@copilot Generate documentation from the last 20 commits
@copilot Search all commits for 'authentication'
@copilot Compare my feature branch with origin/main
@copilot Extract all Docker service names from docker-compose.yml
@copilot Run command 'npm test' in the project directory
@copilot Get the value of NODE_ENV environment variable
```

---

## Core Features

### 🎯 **Git Analysis & Documentation**

- **Automated Documentation**: Generate comprehensive docs from git logs
- **Commit Search**: Deep search across messages and file contents
- **Branch Comparison**: Compare local/remote branches with diff analysis
- **Historical Analysis**: Line-by-line precision for change tracking

### 🔍 **Advanced File Search**

- **JSON/YAML/CSV/Excel**: JSONPath querying with filters and wildcards
- **XML**: XPath querying with element/attribute access
- **XSLT Transformation**: Transform XML files with custom stylesheets
- **Smart Filtering**: Automatic exclusion of build artifacts and IDE files

### ⚡ **Workspace Intelligence**

- **Performance Optimized**: Efficient operations with size/count limits
- **Configurable Exclusions**: Custom `.gitvision/exclude.json` patterns
- **Multi-sheet Excel**: Process entire workbooks with structured results
- **Remote Support**: Full GitHub/GitLab integration

---

## Available Tools

### 📝 **Documentation & Analysis**

| Tool                                            | Description                                                                           |
| ----------------------------------------------- | ------------------------------------------------------------------------------------- |
| `gv_generate_git_commit_report`                 | Generate git commit report for current branch                                         |
| `gv_generate_git_commit_report_to_file`         | Generate git commit report for current branch and write to a file                     |
| `gv_compare_branches_documentation`             | Generate documentation comparing differences between two branches                     |
| `gv_compare_branches_with_remote_documentation` | Generate documentation comparing differences between two branches with remote support |
| `gv_compare_commits_documentation`              | Generate documentation comparing differences between two commits                      |
| `gv_get_recent_commits`                         | Get recent commits from the current repository                                        |
| `gv_get_commit_diff_info`                       | Get comprehensive diff information between two commits                                |
| `gv_run_sbn_template`                           | Run a Scriban, Jinja or Jinja2 template with provided input data                      |

### 🔍 **Search & Discovery**

| Tool                                       | Description                              | Query Language |
| ------------------------------------------ | ---------------------------------------- | -------------- |
| `gv_search_commits_for_string`             | Search all commits for specific text     | Text search    |
| `gv_search_json_file`                      | Query JSON files with advanced filtering | JSONPath       |
| `gv_search_yaml_file`                      | Query YAML files (Docker, K8s, CI/CD)    | JSONPath       |
| `gv_search_xml_file`                       | Query XML with element/attribute access  | XPath          |
| `gv_search_csv_file`                       | Query CSV data with header support       | JSONPath       |
| `gv_search_excel_file`                     | Multi-sheet Excel analysis               | JSONPath       |
| `gv_transform_xml_with_xslt`               | Transform XML with XSLT stylesheets      | XSLT           |
| `gv_list_workspace_files`                  | Smart file discovery with exclusions     | Glob patterns  |
| `gv_list_workspace_files_with_cached_data` | High-performance file operations         | Filters        |
| `gv_read_filtered_workspace_files`         | Bulk file reading with limits            | Filters        |
| `gv_git_find_merge_conflicts`              | Locate Git merge conflicts in code       | Text search    |

### 🌿 **Branch & Repository Management**

| Tool                     | Description                                                   |
| ------------------------ | ------------------------------------------------------------- |
| `gv_get_local_branches`  | Get list of local branches in the repository                  |
| `gv_get_remote_branches` | Get list of remote branches in the repository                 |
| `gv_get_all_branches`    | Get list of all branches (local and remote) in the repository |
| `gv_get_current_branch`  | Get the current active branch in the repository               |
| `gv_fetch_from_remote`   | Fetch latest changes from remote repository                   |

### ⚡ **Advanced Analysis**

| Tool                                    | Description                                                              |
| --------------------------------------- | ------------------------------------------------------------------------ |
| `gv_get_changed_files_between_commits`  | Get list of files changed between two commits                            |
| `gv_get_detailed_diff_between_commits`  | Get detailed diff content between two commits                            |
| `gv_get_file_line_diff_between_commits` | Get line-by-line file diff between two commits                           |
| `gv_deconstruct_to_json`                | Deconstruct a C# file and save structure to a JSON file                  |
| `gv_deconstruct_to_file`                | Deconstruct a C# Service, Repository or Controller file and returns JSON |
| `gv_get_app_version`                    | Extract application version from a project file                          |

### 🔧 **Utility & Process Management**

| Tool                          | Description                                         |
| ----------------------------- | --------------------------------------------------- |
| `gv_run_process`              | Run an external process and capture its output      |
| `gv_run_plugin`               | Run a plugin and capture its output                 |
| `gv_get_environment_variable` | Get the value of an environment variable            |
| `gv_set_environment_variable` | Set an environment variable for the current process |

---

## Getting Started

### � **Prerequisites**

- **.NET 9.0 SDK** - Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Git repository** in workspace
- **VS Code with Copilot** for optimal experience
- **Remote access** (optional, for GitHub/GitLab features)

### ⚡ **Quick Setup**

```powershell
# Build the project
dotnet restore
dotnet build --configuration Release

# Run with optimal settings
$env:DOTNET_ENVIRONMENT="Production"
dotnet run --no-build --verbosity quiet
```

### 🎯 **Usage Examples**

#### **Documentation Generation**

```bash
@copilot Generate documentation from the last 20 commits
@copilot Create a release summary comparing main with release/v2.0
@copilot Generate project history and save to docs/changelog.md
```

#### **Search & Discovery**

```bash
@copilot Search all commits for 'authentication' and show results
@copilot Find all commits that mention 'bug fix' in messages or code
@copilot Extract all Docker service names from docker-compose.yml
@copilot Search config.yaml for database settings using $.database.*
@copilot Find all environment variables in Kubernetes manifests
```

#### **Branch Analysis**

```bash
@copilot Compare my feature branch with origin/main and save to analysis.md
@copilot Show me what files changed between these two commits
@copilot List all remote branches in this repository
@copilot Fetch latest changes from origin and compare branches
```

#### **Advanced File Operations**

```bash
@copilot Get line-by-line diff for Services/GitService.cs between commits
@copilot Search test.csv for all server names using $[*].ServerName
@copilot Transform config.xml using my stylesheet.xslt
@copilot Extract IT department budget from budget.xlsx
@copilot Find all merge conflicts in the codebase
@copilot Run Scriban template commit.template.sbn with my JSON data
```

#### **Utility & Process Management**

```bash
@copilot Run external command 'git status' in the current directory
@copilot Execute Python script analytics.py with custom environment variables
@copilot Get the value of the PATH environment variable
@copilot Set CUSTOM_CONFIG environment variable to production mode
@copilot Run plugin 'codegen' to generate documentation
```

---

## Configuration

### 🔧 **VS Code MCP Setup**

Add to your VS Code MCP configuration file:

```json
{
  "servers": {
    "GitVisionMCP": {
      "type": "stdio",
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
        "DOTNET_ENVIRONMENT": "Production",
        "GIT_REPOSITORY_DIRECTORY": "c:\\path\\to\\your\\repo"
      }
    }
  }
}
```

### ⚙️ **Smart File Exclusions**

Create `.gitvision/exclude.json` in your workspace root for custom exclusions:

```json
{
  "excludePatterns": [
    "**/node_modules/**",
    "**/bin/**",
    "**/obj/**",
    "**/.git/**",
    "**/*.log",
    "**/temp/**"
  ]
}
```

**Default Exclusions:**

- Build artifacts (`**/bin/**`, `**/obj/**`, `**/Debug/**`, `**/Release/**`)
- IDE files (`.vs/**`, `.vscode/**`, `*.cache`)
- Version control (`.git/**`)
- Package managers (`node_modules/**`)
- Log files (`*.log`)

### 📊 **Query Language Reference**

#### **JSONPath (JSON/YAML/CSV/Excel)**

```bash
$                    # Root element
$.property           # Direct property
$[0]                 # First array element
$[*]                 # All array elements
$..property          # Recursive search
$[?(@.prop == val)]  # Filter by value
$['prop1','prop2']   # Multiple properties
```

#### **XPath (XML)**

```bash
/                    # Root
//element           # All elements
//@attr             # All attributes
//elem[@attr='val'] # Filter by attribute
//elem[1]           # First element
//elem[last()]      # Last element
```

---

## Advanced Usage

### 🚀 **New Features Highlights**

#### **🔍 Commit Search Tool**

Deep search capabilities across commit history:

- Search commit messages AND file contents simultaneously
- Case-insensitive with automatic binary file filtering
- Exact line numbers and comprehensive match statistics

**Use Cases:**

- Bug tracking: `"Find all commits mentioning 'authentication error'"`
- Security audits: `"Look for 'password' or 'secret' in commit history"`
- Code archaeology: `"Find all references to deprecated API functions"`

#### **📈 Excel File Analysis**

Advanced spreadsheet querying with multi-sheet support:

- Extract metrics and KPIs from financial reports
- Query product catalogs and business data
- Analyze inventory and procurement information

#### **📄 YAML & DevOps Integration**

Infrastructure as code analysis:

- Docker Compose: `"Extract all service images from docker-compose.yml"`
- Kubernetes: `"Find all container ports in k8s configurations"`
- CI/CD: `"Get all environment variables from GitHub Actions"`

#### **🧾 Scriban/Jinja Template Processing**

Generate custom formatted output using powerful templating:

- Process Scriban/Jinja templates with JSON input data
- Create custom documentation formats from git data
- Transform structured data into human-readable reports
- Generate Markdown, HTML, or custom formatted output

**Use Cases:**

- Release notes: `"Generate release notes using our template and recent commits"`
- Documentation: `"Create contributor report from git history using our template"`
- Reports: `"Render monthly activity report using commit data and our template"`
- Data transformation: `"Convert JSON data to Markdown with our custom template"`

**Using with Copilot:**

```bash
@copilot Get the last 50 commits as JSON data with a "data" root property
@copilot Run the template .github/templates/commit2.template.sbn with the commit data and save to .temp/commit2.md
```

### 🎯 **MCP Prompts & Automation**

GitVisionMCP provides specialized prompts for release documentation:

```bash
@copilot Using the release_document prompt, create release notes for our project
@copilot Using the release_document_with_version prompt with version 2.0.0 and release date 2025-07-10, create release notes
```

---

## Documentation

### 📚 **Additional Resources**

- **[Search Tools Guide](Documentation/SEARCH_OVERVIEW.md)** - Comprehensive guide to file search capabilities
- **[Examples & Use Cases](Documentation/EXAMPLES.md)** - Practical examples and workflows
- **[Controller Analysis](Documentation/DECONSTRUCTION_SERVICE.md)** - ASP.NET Core analysis features
- **[Exclude Functionality](Documentation/EXCLUDE_FUNCTIONALITY.md)** - File filtering configuration
- **[Run Process Examples](Documentation/RUN_PROCESS_EXAMPLES.md)** - Examples for calling `IUtilityService.RunProcessAsync` from tools
- **[Git Authentication & Tokens](Documentation/GIT_AUTH.md)** - Guide for setting `GITHUB_TOKEN` and fetch fallback behavior

### 🔧 **Technical Details**

- **MCP Protocol**: Compatible with Model Context Protocol standard
- **VS Code Integration**: Optimized for Copilot Agent usage
- **Performance**: Clean JSON-RPC communication with production settings
- **Logging**: File-based logging to prevent protocol interference

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**GitVisionMCP** - A comprehensive Model Context Protocol (MCP) Server that provides advanced git analysis and documentation tools. Designed as a Copilot Agent in VS Code for comprehensive repository analysis and automated documentation generation.

## 🎯 Quick Start Examples

### Basic Documentation Generation

```bash
# Generate git documentation
mcp_tool generate_git_documentation

# Save to specific file
mcp_tool generate_git_documentation_to_file --filePath "RELEASE_NOTES.md"
```

### Search Operations

```bash
# Search commits for specific content
mcp_tool search_commits_for_string --searchString "authentication"

# Query JSON configuration
mcp_tool search_json_file --jsonFilePath "config.json" --jsonPath "$.database.host"

# Search Excel data
mcp_tool search_excel_file --excelFilePath "data.xlsx" --jsonPath "$[*].ServerName"
```

### Branch Comparison

```bash
# Compare branches with documentation
mcp_tool compare_branches_documentation --branch1 "main" --branch2 "feature/new-api" --filePath "comparison.md"
```

### Template Rendering

```bash
# Process Scriban/Jinja template with JSON data
mcp_tool run_sbn_template --templateFilePath ".github/templates/commit.template.sbn" --jsonData '{"data": [...]}' --outputFilePath "output.md"
```

---

## 📖 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**GitVisionMCP** - A comprehensive Model Context Protocol (MCP) Server designed as a Copilot Agent in VS Code for repository analysis and documentation generation.

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

Example calling the Scriban template tool:

```json
{
  "jsonrpc": "2.0",
  "id": 2,
  "method": "tools/call",
  "params": {
    "name": "run_sbn_template",
    "arguments": {
      "templateFilePath": ".github/templates/commit.template.sbn",
      "jsonData": "{\"data\": [{\"hash\": \"796a5e1\", \"author\": \"Developer\", \"message\": \"Example commit\"}]}",
      "outputFilePath": "output.md"
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
- "Search config.yaml for database settings using JSONPath $.database.\*"
- "Extract all Docker service names from docker-compose.yml"
- "Find all environment variables in Kubernetes manifests"

**🌿 Branch Analysis:**

- "Compare my feature branch with origin/main and save to analysis.md"
- "Show me what files changed between these two commits"
- "List all remote branches in this repository"
- "Fetch latest changes from origin and compare branches"

**⚡ Advanced Analysis:**

- "Get line-by-line diff for Services/GitService.cs between two commits"
- "Show me recent commits with detailed change information"
- "Find all merge conflicts in my code"
- "Locate merge conflicts that need to be resolved"

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

### Template Processing Tools

#### run_sbn_template

Process a Scriban or Jinja template with JSON data and save the output to a file.

**Parameters:**

- `templateFilePath` (required): Path to the Scriban/Jinja template file relative to workspace root
- `jsonData` (required): JSON input string data for the template
- `outputFilePath` (required): Path to the output file relative to workspace root

**Example:** Process a commit template with git history data

```bash
# Two-step flow example:
# 1. Get recent commits
gv_get_recent_commits --maxCommits 50

# 2. Process through template (wrap commits in a "data" root object)
gv_run_sbn_template --templateFilePath ".github/templates/commit.template.sbn" --jsonData '{"data":[...commits...]}' --outputFilePath "docs/release-notes.md"
```

**JSON Structure Example:**

```json
{
  "data": [
    {
      "hash": "796a5e1",
      "author": "Developer Name",
      "date": "2025-08-24T18:25:36",
      "message": "Adding example templates",
      "changedFiles": [".github/templates/example.sbn"]
    }
  ]
}
```

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

#### git_find_merge_conflicts

Searches for Git merge conflict markers in the current workspace.

**Parameters:** None

**Returns:** List of files containing merge conflicts with line numbers and conflict details

**Example:** Find all unresolved merge conflicts in the workspace

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

Searches for JSON values in a JSON file using JSONPath queries with advanced wildcard support and structured results.

**Parameters:**

- `jsonFilePath` (required): Path to the JSON file relative to workspace root
- `jsonPath` (required): JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')
- `indented` (optional): Whether to format the output with indentation (default: true)
- `showKeyPaths` (optional): Whether to return structured results with path, value, and key information (default: false)

**Returns:** JSON values matching the JSONPath query, or "No matches found" if the query returns no results

**Enhanced JSONPath Support:**

- **Wildcards**: `$.users[*].email` - Get all user email addresses
- **Recursive Descent**: `$..author` - Get all author properties at any depth
- **Property Wildcards**: `$.configuration.*` - Get all configuration values
- **Conditional Filtering**: `$.items[?(@.price > 100)].name` - Conditional queries
- **Multiple Results**: Automatically returns arrays for queries with multiple matches
- **Single Results**: Returns individual values when only one match is found

**Structured Results with showKeyPaths=true:**

When `showKeyPaths` is enabled, the tool returns structured objects containing:

- `path`: The JSONPath location of the found value
- `value`: The actual value found
- `key`: The property name extracted from the path

**Example without showKeyPaths (default):**

```json
["john@example.com", "jane@example.com"]
```

**Example with showKeyPaths=true:**

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

**JSONPath Examples:**

- `$.name` - Get the root-level name property
- `$.users[*].email` - Get all user email addresses (wildcard array access)
- `$.configuration.database.host` - Get nested configuration values
- `$.items[?(@.price > 100)].name` - Get names of items with price > 100 (filtering)
- `$..author` - Get all author properties at any level (recursive)
- `$.configuration.*` - Get all values under configuration (property wildcard)

**Use Cases:**

- **Configuration Management**: Extract API keys, database settings, feature flags
- **Data Analysis**: Query complex JSON datasets with precise filtering
- **Documentation**: Generate configuration summaries with path context
- **Debugging**: Locate specific values within large JSON structures

#### search_xml_file

Searches for XML values in an XML file using XPath queries with comprehensive element and attribute support.

**Parameters:**

- `xmlFilePath` (required): Path to the XML file relative to workspace root
- `xPath` (required): XPath query string (e.g., '//users/user/@email', '/configuration/database/host')
- `indented` (optional): Whether to format the output with indentation (default: true)
- `showKeyPaths` (optional): Whether to return structured results with path, value, and key information (default: false)

**Returns:** XML elements or attributes matching the XPath query, or "No matches found" if the query returns no results

**Enhanced XPath Support:**

- **Element Queries**: `//user` - Get all user elements
- **Attribute Queries**: `//user/@email` - Get all user email attributes
- **Path Navigation**: `/configuration/settings/database` - Direct path navigation
- **Conditional Filtering**: `//feature[@enabled='true']` - Filter by attribute values
- **Multiple Results**: Automatically returns arrays for queries with multiple matches
- **Single Results**: Returns individual elements/attributes when only one match is found

**Structured Results with showKeyPaths=true:**

When `showKeyPaths` is enabled, the tool returns structured objects containing:

- `path`: The XPath location of the found element/attribute
- `value`: The actual XML content or attribute value found
- `key`: The element or attribute name extracted from the path

**Example without showKeyPaths (default):**

```xml
<user id="1" name="John Doe" email="john@example.com" />
<user id="2" name="Jane Smith" email="jane@example.com" />
```

**Example with showKeyPaths=true:**

```json
[
  {
    "path": "/configuration/users/user",
    "value": "<user id=\"1\" name=\"John Doe\" email=\"john@example.com\" />",
    "key": "user"
  },
  {
    "path": "/configuration/users/user[2]",
    "value": "<user id=\"2\" name=\"Jane Smith\" email=\"jane@example.com\" />",
    "key": "user"
  }
]
```

**XPath Examples:**

- `//connectionString` - Get all connectionString elements
- `//user` - Get all user elements
- `//user/@email` - Get all user email attributes
- `//feature[@enabled='true']/@name` - Get names of enabled features
- `/configuration/settings/*` - Get all direct children of settings
- `//user[@id='1']/@name` - Get name of specific user by ID

**JSONPath vs XPath Comparison:**

| Operation        | JSON (JSONPath)            | XML (XPath)                  |
| ---------------- | -------------------------- | ---------------------------- |
| All users        | `$.users[*]`               | `//user`                     |
| User emails      | `$.users[*].email`         | `//user/@email`              |
| First user       | `$.users[0]`               | `//user[1]`                  |
| Enabled features | `$.features[?(@.enabled)]` | `//feature[@enabled='true']` |

**Use Cases:**

- **Configuration Analysis**: Extract database settings, API endpoints, feature flags
- **Data Extraction**: Query structured XML documents with precise filtering
- **Documentation**: Generate configuration references with element context
- **System Integration**: Parse XML configuration files and web service responses

#### transform_xml_with_xslt

Transforms an XML file using an XSLT stylesheet and returns the transformed result.

**Parameters:**

- `xmlFilePath` (required): Path to the XML file relative to workspace root
- `xsltFilePath` (required): Path to the XSLT stylesheet file relative to workspace root

**Returns:** The transformed XML/HTML/text output as a string, or error message if transformation fails

**XSLT Transformation Features:**

- **XSLT 1.0 Support**: Full compatibility with XSLT 1.0 standard using XslCompiledTransform
- **Multiple Output Formats**: XML, HTML, text, and custom formats based on xsl:output method
- **Template Processing**: Support for xsl:template, xsl:for-each, xsl:if, and other XSLT elements
- **Variable and Parameter Support**: Use xsl:variable and xsl:param for dynamic transformations
- **Function Library**: Access to XSLT built-in functions like count(), position(), substring()
- **Namespace Handling**: Proper namespace processing for complex XML documents

**Common Transformation Patterns:**

- **XML to HTML**: Generate web pages, reports, and documentation from XML data
- **XML to XML**: Restructure, filter, or reformat XML documents
- **Data Extraction**: Extract specific elements and create summary documents
- **Format Conversion**: Transform between different XML schemas and formats

**Example Usage:**

```xml
<!-- books.xml -->
<catalog>
    <book id="1">
        <title>The Great Gatsby</title>
        <author>F. Scott Fitzgerald</author>
        <price>12.99</price>
        <published>1925</published>
    </book>
</catalog>

<!-- transform.xslt -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" indent="yes"/>
    <xsl:template match="/">
        <html>
            <body>
                <h1>Book Catalog</h1>
                <table border="1">
                    <tr><th>Title</th><th>Author</th><th>Price</th></tr>
                    <xsl:for-each select="catalog/book">
                        <tr>
                            <td><xsl:value-of select="title"/></td>
                            <td><xsl:value-of select="author"/></td>
                            <td>$<xsl:value-of select="price"/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
```

**Error Handling:**

- **File Validation**: Checks for existence of both XML and XSLT files
- **XML Parsing**: Comprehensive error reporting for malformed XML
- **XSLT Compilation**: Detailed error messages for invalid XSLT syntax
- **Transformation Errors**: Runtime error handling during template processing

**Use Cases:**

- **Report Generation**: Transform data XML into formatted HTML reports
- **Documentation**: Generate human-readable documentation from XML configuration
- **Data Migration**: Convert XML data between different schemas and formats
- **Web Development**: Generate static HTML pages from XML content management
- **Configuration Processing**: Transform XML configurations for different environments

#### search_yaml_file

Searches for YAML values in a YAML file using JSONPath queries with YAML-to-JSON conversion.

**Parameters:**

- `yamlFilePath` (required): Path to the YAML file relative to workspace root
- `jsonPath` (required): JSONPath query string (e.g., '$.application.name', '$.database.connections[*].host')
- `indented` (optional): Whether to format the output with indentation (default: true)
- `showKeyPaths` (optional): Whether to return structured results with path, value, and key information (default: false)

**Returns:** JSON values matching the JSONPath query (converted from YAML), or "No matches found" if the query returns no results

**Enhanced YAML Support:**

- **Configuration Files**: Query Docker Compose, Kubernetes manifests, application.yml
- **Infrastructure as Code**: Search Ansible playbooks, Helm charts, CI/CD pipelines
- **API Specifications**: Query OpenAPI/Swagger definitions in YAML format
- **Documentation**: Extract metadata from YAML front matter
- **Human-Readable**: Works with the most readable configuration format

**JSONPath Examples for YAML:**

- `$.application.name` - Get application name from config
- `$.database.connections[*].host` - Get all database hosts
- `$.users[*].roles` - Get all user role arrays
- `$.features[?(@.enabled)].name` - Get names of enabled features
- `$.services.*.image` - Get all Docker service images

**YAML Search Results:**

- Extracted values in JSON format (converted from YAML)
- Formatted output (indented or compact JSON)
- Full compatibility with existing JSONPath infrastructure
- Support for complex YAML structures including arrays, objects, and nested data

**Use Cases:**

- **DevOps Configuration**: Extract settings from Docker Compose, Kubernetes, Ansible
- **Application Configuration**: Query application.yml, database.yml, config files
- **Infrastructure Management**: Search Terraform, Helm charts, CI/CD configurations
- **API Documentation**: Extract information from OpenAPI/Swagger YAML specifications

### Commit Search Tool

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

### Utility & Process Management Tools

#### gv_run_process

Runs an external process and captures the output with comprehensive error handling and environment variable support.

**Parameters:**

- `workingDirectory` (optional): The working directory for the process
- `fileName` (required): The name or path of the process to run
- `arguments` (optional): The command line arguments to pass to the process
- `timeoutMs` (optional): The timeout in milliseconds (default: 60000)
- `environmentVariables` (optional): Dictionary of environment variables to set

**Returns:** Dictionary containing success flag, stdout, stderr, and exit code

**Example:** Run git status command with custom environment

#### gv_run_plugin

Runs a configured plugin from the `.gitvision/config.json` file and captures its output.

**Parameters:**

- `pluginName` (required): Name of plugin to execute

**Returns:** Dictionary containing success flag, output, and error information

**Example:** Execute a custom code generation or documentation plugin

#### gv_get_environment_variable

Gets the value of an environment variable.

**Parameters:**

- `variableName` (required): The name of the environment variable

**Returns:** The value of the environment variable as string, or null if not set

**Example:** Get PATH or custom application configuration variables

#### gv_set_environment_variable

Sets an environment variable for the current process.

**Parameters:**

- `name` (required): The name of the environment variable
- `value` (required): The value to set

**Returns:** Boolean indicating success

**Example:** Set configuration flags or temporary environment settings

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
      "indented": true,
      "showKeyPaths": false
    }
  }
}
```

Perfect for extracting configuration values, API keys, or any structured data from JSON files. The enhanced search supports:

- **Simple property access**: `$.propertyName`
- **Nested navigation**: `$.level1.level2.property`
- **Array wildcards**: `$.users[0].name` or `$.users[*].email`
- **Complex queries**: `$.items[?(@.price > 100)].name`
- **Recursive searches**: `$..author` (all author properties at any level)
- **Structured results**: Enable `showKeyPaths` for path context and metadata

**New showKeyPaths Feature:**

When `showKeyPaths=true`, returns structured objects with:

- `path`: JSONPath location of the value
- `value`: The actual JSON value
- `key`: Extracted property name

This is invaluable for:

- **Configuration tracking**: Know exactly where values originate
- **Complex JSON analysis**: Navigate large configuration files with context
- **Data migration**: Map old paths to new structure requirements
- **Documentation generation**: Create structured reports with location details

Use cases include:

- **Configuration validation**: Check environment-specific settings with path context
- **Security audits**: Extract API keys with their exact locations
- **Documentation**: Generate configuration references from JSON files
- **Data analysis**: Extract specific data points with full context preservation

### 9. YAML Configuration Analysis

#### Copilot Command:

```bash
@copilot Search config.yaml for database configuration using JSONPath $.database.connections[*].host
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 8,
  "method": "tools/call",
  "params": {
    "name": "search_yaml_file",
    "arguments": {
      "yamlFilePath": "config.yaml",
      "jsonPath": "$.database.connections[*].host",
      "indented": true,
      "showKeyPaths": false
    }
  }
}
```

Perfect for extracting configuration values from YAML files. The YAML search supports:

- **Simple property access**: `$.propertyName`
- **Nested navigation**: `$.level1.level2.property`
- **Array wildcards**: `$.connections[0].host` or `$.connections[*].port`
- **Complex queries**: `$.services[?(@.enabled)].name`
- **Recursive searches**: `$..database` (all database properties at any level)
- **Structured results**: Enable `showKeyPaths` for path context and metadata

**YAML-specific Use Cases:**

- **DevOps Configuration**: Extract settings from Docker Compose, Kubernetes manifests
- **Infrastructure as Code**: Query Ansible playbooks, Terraform configurations
- **CI/CD Pipelines**: Extract job configurations, environment variables, deployment settings
- **Application Configuration**: Parse application.yml, database.yml, feature flags
- **API Specifications**: Query OpenAPI/Swagger YAML definitions

Example YAML file structure:

```yaml
application:
  name: "MyApp"
  version: "1.0.0"
database:
  connections:
    - host: "localhost"
      port: 5432
      ssl: true
    - host: "backup.example.com"
      port: 5432
      ssl: false
services:
  web:
    image: "nginx:latest"
    enabled: true
  api:
    image: "myapp:v1.0"
    enabled: true
```

**Common YAML Queries:**

- `$.application.name` → `"MyApp"`
- `$.database.connections[*].host` → `["localhost", "backup.example.com"]`
- `$.services[?(@.enabled)].image` → `["nginx:latest", "myapp:v1.0"]`

### 10. XSLT XML Transformation

#### Copilot Command:

```bash
@copilot Transform books.xml using catalog-to-html.xslt to generate an HTML report
```

#### JSON-RPC Call:

```json
{
  "jsonrpc": "2.0",
  "id": 9,
  "method": "tools/call",
  "params": {
    "name": "transform_xml_with_xslt",
    "arguments": {
      "xmlFilePath": "data/books.xml",
      "xsltFilePath": "templates/catalog-to-html.xslt"
    }
  }
}
```

Perfect for transforming XML documents into different formats. The XSLT transformation supports:

- **XML to HTML**: Generate web pages and reports from XML data
- **XML to XML**: Restructure and reformat XML documents
- **Data Extraction**: Extract specific elements using XSLT filtering
- **Multi-format Output**: HTML, XML, text, or custom formats based on XSLT output method

**XSLT-specific Use Cases:**

- **Report Generation**: Transform data catalogs into formatted HTML tables
- **Documentation**: Generate human-readable docs from XML configuration files
- **Data Migration**: Convert XML between different schemas and formats
- **Web Development**: Generate static HTML pages from XML content
- **Configuration Processing**: Transform XML configs for different environments

Example XSLT transformation:

```xml
<!-- Input: books.xml -->
<catalog>
    <book id="1">
        <title>The Great Gatsby</title>
        <author>F. Scott Fitzgerald</author>
        <price>12.99</price>
    </book>
</catalog>

<!-- XSLT: catalog-to-html.xslt -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html"/>
    <xsl:template match="/">
        <html><body>
            <h1>Book Catalog</h1>
            <xsl:for-each select="catalog/book">
                <p><xsl:value-of select="title"/> by <xsl:value-of select="author"/></p>
            </xsl:for-each>
        </body></html>
    </xsl:template>
</xsl:stylesheet>

<!-- Output: HTML -->
<html><body>
    <h1>Book Catalog</h1>
    <p>The Great Gatsby by F. Scott Fitzgerald</p>
</body></html>
```

### 11. Line-by-Line Analysis

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

### 12. External Process Execution & Environment Management

#### Copilot Commands:

```bash
# Execute external processes
@copilot Run command 'npm install' in the current project directory
@copilot Execute Python script 'data_analysis.py' with custom timeout of 120 seconds
@copilot Run git status command in the repository directory

# Environment variable management
@copilot Get the value of PATH environment variable
@copilot Set NODE_ENV environment variable to 'production'
@copilot Check if DOCKER_HOST is set in the environment

# Plugin execution
@copilot Run plugin 'codegen' to generate API documentation
@copilot Execute plugin 'deployment' for automated deployment tasks
```

#### JSON-RPC Calls:

```json
{
  "jsonrpc": "2.0",
  "id": 10,
  "method": "tools/call",
  "params": {
    "name": "gv_run_process",
    "arguments": {
      "workingDirectory": "/path/to/project",
      "fileName": "npm",
      "arguments": "install",
      "timeoutMs": 60000
    }
  }
}
```

Perfect for:

- **Build automation**: Execute build scripts, test suites, and deployment commands
- **Environment setup**: Manage configuration variables and runtime settings
- **Plugin integration**: Execute custom tools and automation scripts
- **Cross-platform compatibility**: Run platform-specific commands and tools
- **CI/CD integration**: Automate development and deployment workflows

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
