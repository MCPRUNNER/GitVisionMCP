# GitVisionMCP Release Notes

## Version 1.0.6 - CSV Search & Documentation Enhancements

**Release Date**: July 27, 2025

### 🆕 Major Features

#### SearchCsvFile Tool

**New Feature**: Advanced CSV file querying using JSONPath

- **Flexible Extraction**: Query CSV files with JSONPath for targeted data retrieval
- **Integration**: Available via MCP Server and VS Code Copilot Agent
- **Performance**: Optimized for large CSV files and fast search
- **Use Case**: Ideal for server inventories, logs, and tabular data analysis

**Benefits**:

- 🔎 **Instant filtering** of CSV data for analytics and reporting
- 🚀 **Fast results** even on large datasets
- 🧩 **Seamless integration** with other workspace tools

### 🔧 Technical Implementation

- **Added**: `SearchCsvFile` tool to `GitServiceTools.cs`
- **Enhanced**: Tool table and documentation to include CSV search
- **Improved**: Automated documentation generation and tool discoverability

### 📚 Documentation

- **Updated [README.md](README.md)**: Added SearchCsvFile usage and examples
- **Updated [RELEASE_DOCUMENT.md](RELEASE_DOCUMENT.md)**: Refactored to include new tool and architecture

### 🛠️ MCP Tool List (v1.0.6)

Below are the MCP tools available in this release, as defined in `GitServiceTools.cs`:

| Tool Name                        | Description                                                                 |
| -------------------------------- | --------------------------------------------------------------------------- |
| FetchFromRemote                  | Fetch latest changes from remote repository                                 |
| GenerateGitDocumentation         | Generate documentation from git logs for the current workspace              |
| GenerateGitDocumentationToFile   | Generate documentation from git logs and write to a file                    |
| CompareBranchesDocumentation     | Generate documentation comparing differences between two branches           |
| CompareBranchesWithRemote        | Generate documentation comparing differences between two branches (remote)  |
| CompareCommitsDocumentation      | Generate documentation comparing differences between two commits            |
| GetRecentCommits                 | Get recent commits from the current repository                              |
| GetLocalBranches                 | Get list of local branches in the repository                                |
| GetRemoteBranches                | Get list of remote branches in the repository                               |
| GetAllBranches                   | Get list of all branches (local and remote) in the repository               |
| GetCurrentBranch                 | Get the current active branch in the repository                             |
| GetChangedFilesBetweenCommits    | Get list of files changed between two commits                               |
| GetCommitDiffInfo                | Get comprehensive diff information between two commits                      |
| GetDetailedDiffBetweenCommits    | Get detailed diff content between two commits                               |
| SearchCommitsForString           | Search all commits for a specific string and return commit details          |
| GetFileLineDiffBetweenCommits    | Get line-by-line file diff between two commits                              |
| ListWorkspaceFiles               | List all files in the workspace with optional filtering                     |
| ListWorkspaceFilesWithCachedData | List workspace files with optional filtering using cached data              |
| ReadFilteredWorkspaceFiles       | Read contents of all files from filtered workspace results                  |
| SearchJsonFile                   | Search for JSON values in a JSON file using JSONPath                        |
| SearchCsvFile                    | Search for CSV values in a CSV file using JSONPath queries                  |
| SearchXmlFile                    | Search for XML values in an XML file using XPath                            |
| TransformXmlWithXslt             | Transform an XML file using an XSLT stylesheet                              |
| SearchYamlFile                   | Search for YAML values in a YAML file using JSONPath                        |
| Deconstruct                      | Deconstruct a C# Service, Repository or Controller file (JSON output)       |
| DeconstructToJson                | Deconstruct a C# Service, Repository or Controller file (save to JSON file) |
| SamplingLLM                      | Run Sampling chat to process predefined User and System prompts             |

See [EXAMPLES.md](Documentation/EXAMPLES.md) and [DECONSTRUCTION_SERVICE.md](Documentation/DECONSTRUCTION_SERVICE.md) for usage details and JSON-RPC examples.

---

## Version 1.0.5 - Initial Release

**Release Date**: July 22, 2025

### 🆕 Major Features

#### Smart File Exclusion System

**New Feature**: Intelligent file filtering for workspace operations

- **Automatic Exclusions**: Built-in patterns for common build artifacts, IDE files, and dependencies
- **Configurable Patterns**: Customizable `.gitvision/exclude.json` configuration file
- **Performance Optimized**: Significant improvements in file operation speed and memory usage
- **Pattern Matching**: Full glob pattern support with wildcards (`*`, `**`, `?`)

**Benefits**:

- ⚡ **50-90% reduction** in files processed during workspace operations
- 🚀 **Significantly faster** file listing and content reading
- 💾 **Lower memory usage** for large projects
- 🎯 **Cleaner results** focused on relevant source code files

#### Default Exclude Patterns

```json
{
  "excludePatterns": [
    ".git/**", // Git repository data
    "node_modules/**", // Node.js dependencies
    "**/bin/**", // Build output directories
    "**/obj/**", // Build intermediate files
    "**/Debug/**", // Debug build artifacts
    "**/Release/**", // Release build artifacts
    ".vs/**", // Visual Studio files
    ".vscode/**", // VS Code files
    "*.cache", // Cache files
    "*.log", // Log files
    "**/.gitvision/**" // GitVision configuration
  ]
}
```

### 🔧 Technical Implementation

#### LocationService Enhancements

- **Added**: `GetAllFilesAsync()` method with exclude filtering
- **Added**: `IsFileExcludedAsync()` for pattern matching
- **Added**: `LoadExcludeConfigurationAsync()` with caching
- **Enhanced**: Pattern matching with compiled regex for performance
- **Maintained**: Backward compatibility with existing `GetAllFiles()` method

#### GitServiceTools Updates

- **Updated**: `ListWorkspaceFilesAsync()` to use exclude filtering
- **Enhanced**: `ReadFilteredWorkspaceFilesAsync()` benefits from automatic exclusions
- **Improved**: Performance across all workspace file operations

#### Configuration Management

- **Location**: `.gitvision/exclude.json` in workspace root
- **Caching**: Automatic configuration caching with file modification tracking
- **Validation**: Comprehensive error handling for invalid configurations
- **Flexibility**: Runtime configuration updates without service restart

### 📚 Documentation

#### New Documentation

- **[EXCLUDE_FUNCTIONALITY.md](Documentation/EXCLUDE_FUNCTIONALITY.md)**: Comprehensive guide to exclude functionality
- **Updated [README.md](README.md)**: Include exclude functionality overview and benefits
- **Updated [EXAMPLES.md](Documentation/EXAMPLES.md)**: Added exclude configuration examples

#### Usage Examples

```bash
# VS Code Copilot commands automatically benefit from exclusions
@copilot List all source code files (build artifacts automatically excluded)
@copilot Analyze project structure without IDE clutter
@copilot Read configuration files for review
```

### 🏗️ Architecture Improvements

#### Interface Updates

- **Added**: `GetAllFilesAsync()` to `ILocationService`
- **Added**: `IsFileExcludedAsync()` to `ILocationService`
- **Maintained**: All existing interface contracts for compatibility

#### Error Handling

- **Enhanced**: Graceful fallback when exclude configuration is missing
- **Improved**: Comprehensive logging for debugging exclude patterns
- **Added**: Clear error messages for configuration issues

### 🔄 Migration Guide

#### Automatic Migration

- **No action required**: Exclude functionality works automatically
- **Default patterns**: Sensible defaults applied out-of-the-box
- **Backward compatibility**: Existing tools continue to work

#### Custom Configuration

1. Create `.gitvision/exclude.json` in workspace root
2. Add custom patterns using glob syntax
3. Configuration automatically loaded on next operation

#### Example Migration

```json
{
  "excludePatterns": [
    ".git/**",
    "node_modules/**",
    "**/bin/**",
    "**/obj/**",
    "custom/temp/**",
    "*.tmp"
  ],
  "description": "Custom exclude patterns for our project",
  "version": "1.0.0"
}
```

### 🐛 Bug Fixes

- **Fixed**: Pattern matching now properly handles Windows and Unix path separators
- **Fixed**: Configuration caching properly updates when file is modified
- **Fixed**: Error handling for corrupted or invalid JSON configuration files

### ⚠️ Breaking Changes

**None**: This release maintains full backward compatibility.

### 📋 Tool Count Update

- **Total Tools**: Updated from 15 to 23 tools available
- **New Categories**: Added workspace file operations section
- **Enhanced**: All workspace tools now benefit from exclude functionality

### 🛠️ MCP Tool List (v1.0.5)

Below are the MCP tools available in this release, as defined in `GitServiceTools.cs`:

| Tool Name                        | Description                                                                 |
| -------------------------------- | --------------------------------------------------------------------------- |
| FetchFromRemote                  | Fetch latest changes from remote repository                                 |
| GenerateGitDocumentation         | Generate documentation from git logs for the current workspace              |
| GenerateGitDocumentationToFile   | Generate documentation from git logs and write to a file                    |
| CompareBranchesDocumentation     | Generate documentation comparing differences between two branches           |
| CompareBranchesWithRemote        | Generate documentation comparing differences between two branches (remote)  |
| CompareCommitsDocumentation      | Generate documentation comparing differences between two commits            |
| GetRecentCommits                 | Get recent commits from the current repository                              |
| GetLocalBranches                 | Get list of local branches in the repository                                |
| GetRemoteBranches                | Get list of remote branches in the repository                               |
| GetAllBranches                   | Get list of all branches (local and remote) in the repository               |
| GetCurrentBranch                 | Get the current active branch in the repository                             |
| GetChangedFilesBetweenCommits    | Get list of files changed between two commits                               |
| GetCommitDiffInfo                | Get comprehensive diff information between two commits                      |
| GetDetailedDiffBetweenCommits    | Get detailed diff content between two commits                               |
| SearchCommitsForString           | Search all commits for a specific string and return commit details          |
| GetFileLineDiffBetweenCommits    | Get line-by-line file diff between two commits                              |
| ListWorkspaceFiles               | List all files in the workspace with optional filtering                     |
| ListWorkspaceFilesWithCachedData | List workspace files with optional filtering using cached data              |
| ReadFilteredWorkspaceFiles       | Read contents of all files from filtered workspace results                  |
| SearchJsonFile                   | Search for JSON values in a JSON file using JSONPath                        |
| SearchCsvFile                    | Search for CSV values in a CSV file using JSONPath queries                  |
| SearchXmlFile                    | Search for XML values in an XML file using XPath                            |
| TransformXmlWithXslt             | Transform an XML file using an XSLT stylesheet                              |
| SearchYamlFile                   | Search for YAML values in a YAML file using JSONPath                        |
| Deconstruct                      | Deconstruct a C# Service, Repository or Controller file (JSON output)       |
| DeconstructToJson                | Deconstruct a C# Service, Repository or Controller file (save to JSON file) |

See [EXAMPLES.md](Documentation/EXAMPLES.md) and [DECONSTRUCTION_SERVICE.md](Documentation/DECONSTRUCTION_SERVICE.md) for usage details and JSON-RPC examples.

---
