using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using GitVisionMCP.Models; // For CallToolRequest, CallToolResponse, ToolContent, etc.
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GitVisionMCP.Services;
using GitVisionMCP.Tools;
using GitVisionMCP.Prompts;

namespace GitVisionMCP.Handlers;

public class McpHandler : IMcpHandler
{
    private readonly ILogger<McpHandler> _logger;
    private readonly IConfiguration _configuration;
    private readonly IGitService _gitService;
    private readonly IWorkspaceService _workspaceService;
    private readonly IFileService _fileService;
    private readonly IGitServiceTools _gitServiceTools;
    private readonly IUtilityService _utilityService;
    private readonly JsonSerializerSettings _jsonSettings;
    private readonly JsonSerializerSettings _outputJsonSettings;
    private bool _isRunning;

    public McpHandler(
        ILogger<McpHandler> logger,
        IConfiguration configuration,
        IGitService gitService,
        IWorkspaceService workspaceService,
        IFileService fileService,
        IGitServiceTools gitServiceTools, IUtilityService utilityService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _gitService = gitService ?? throw new ArgumentNullException(nameof(gitService));
        _workspaceService = workspaceService ?? throw new ArgumentNullException(nameof(workspaceService));
        _gitServiceTools = gitServiceTools ?? throw new ArgumentNullException(nameof(gitServiceTools));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        _utilityService = utilityService ?? throw new ArgumentNullException(nameof(utilityService));
        _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };
        _outputJsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting MCP Server...");
        _isRunning = true;

        try
        {
            while (_isRunning && !cancellationToken.IsCancellationRequested)
            {
                await ProcessRequestAsync(cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogDebug("MCP Server stopped by cancellation request");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MCP Server main loop");
            throw;
        }
        finally
        {
            _logger.LogInformation("MCP Server stopped");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Stopping MCP Server...");
        _isRunning = false;
        await Task.CompletedTask;
    }

    private async Task ProcessRequestAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Check cancellation before reading
            cancellationToken.ThrowIfCancellationRequested();

            var input = await Console.In.ReadLineAsync(cancellationToken);
            if (string.IsNullOrEmpty(input))
            {
                // If we get null/empty input, it might mean the input stream is closed
                // This is a normal termination condition for stdio transport
                _logger.LogDebug("Received null/empty input, stopping server");
                _isRunning = false;
                return;
            }

            _logger.LogTrace("Received request: {Input}", input);

            var request = JsonConvert.DeserializeObject<JsonRpcRequest>(input, _jsonSettings);
            if (request == null)
            {
                await SendErrorResponseAsync(null, -32700, "Parse error");
                return;
            }

            var response = await HandleRequestAsync(request);
            if (response != null)
            {
                // Create truly compact JSON manually to ensure no formatting
                var compactJson = CreateCompactJsonResponse(response);

                // Write directly to stdout as UTF-8 bytes
                var jsonBytes = Encoding.UTF8.GetBytes(compactJson);
                await Console.OpenStandardOutput().WriteAsync(jsonBytes, 0, jsonBytes.Length);
                await Console.OpenStandardOutput().WriteAsync(Encoding.UTF8.GetBytes("\n"), 0, 1);
                await Console.OpenStandardOutput().FlushAsync();

                _logger.LogTrace("Sent response: {Response}", compactJson);
            }
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON parsing error");
            await SendErrorResponseAsync(null, -32700, "Parse error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing request");
            await SendErrorResponseAsync(null, -32603, "Internal error");
        }
    }

    private async Task<JsonRpcResponse?> HandleRequestAsync(JsonRpcRequest request)
    {
        try
        {
            return request.Method switch
            {
                "initialize" => await HandleInitializeAsync(request),
                "initialized" => null, // Notification, no response needed
                "tools/list" => await HandleToolsListAsync(request),
                "tools/call" => await HandleToolCallAsync(request),
                "prompts/get" => await HandlePromptGetAsync(request),
                _ => CreateErrorResponse(request.Id, -32601, "Method not found")
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling request method: {Method}", request.Method);
            return CreateErrorResponse(request.Id, -32603, "Internal error", ex.Message);
        }
    }

    private async Task<JsonRpcResponse> HandleInitializeAsync(JsonRpcRequest request)
    {
        _logger.LogDebug("Handling initialize request");
        var appVersion = _utilityService.GetAppVersion("GitVisionMCP.csproj");
        var initResponse = new InitializeResponse
        {
            ProtocolVersion = "2024-11-05",
            Capabilities = new ServerCapabilities
            {
                Tools = new { },
                Resources = null,
                Prompts = new
                {
                    release_document = new
                    {
                        description = "Creates a comprehensive release document from git history",
                        parameters = new
                        {
                        }
                    },
                    release_document_with_version = new
                    {
                        description = "Creates a release document with specific version information",
                        parameters = new
                        {
                            type = "object",
                            properties = new
                            {
                                version = new
                                {
                                    type = "string",
                                    description = "The version number of the release (e.g., 1.0.5)"
                                },
                                releaseDate = new
                                {
                                    type = "string",
                                    description = "The release date (e.g., 2025-07-06)"
                                }
                            },
                            required = new[] { "version", "releaseDate" }
                        }
                    }
                },
                Logging = new { }
            },
            ServerInfo = new ServerInfo
            {
                Name = "GitVisionMCP",
                Version = appVersion ?? "1.0.8"
            }
        };

        return await Task.FromResult(new JsonRpcResponse
        {
            Id = request.Id,
            Result = initResponse
        });
    }

    private async Task<JsonRpcResponse> HandleToolsListAsync(JsonRpcRequest request)
    {
        _logger.LogDebug("Handling tools/list request");

        var tools = new[]
        {
            new Tool
            {
                Name = "gv_generate_git_commit_report",
                Description = "Generate git commit report for current branch",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        maxCommits = new { type = "integer", description = "Maximum number of commits to include (default: 50)" },
                        outputFormat = new { type = "string", description = "Output format: markdown, html, or text (default: markdown)" }
                    }
                }
            },
            new Tool
            {
                Name = "gv_generate_git_commit_report_to_file",
                Description = "Generate git commit report for current branch and write to a file",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        filePath = new { type = "string", description = "Path where to save the documentation file" },
                        maxCommits = new { type = "integer", description = "Maximum number of commits to include (default: 50)" },
                        outputFormat = new { type = "string", description = "Output format: markdown, html, or text (default: markdown)" }
                    },
                    required = new[] { "filePath" }
                }
            },
            new Tool
            {
                Name = "gv_compare_branches_documentation",
                Description = "Generate documentation comparing differences between two branches",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        branch1 = new { type = "string", description = "First branch name" },
                        branch2 = new { type = "string", description = "Second branch name" },
                        filePath = new { type = "string", description = "Path where to save the documentation file" },
                        outputFormat = new { type = "string", description = "Output format: markdown, html, or text (default: markdown)" }
                    },
                    required = new[] { "branch1", "branch2", "filePath" }
                }
            },
            new Tool
            {
                Name = "gv_compare_commits_documentation",
                Description = "Generate documentation comparing differences between two commits",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        commit1 = new { type = "string", description = "First commit hash" },
                        commit2 = new { type = "string", description = "Second commit hash" },
                        filePath = new { type = "string", description = "Path where to save the documentation file" },
                        outputFormat = new { type = "string", description = "Output format: markdown, html, or text (default: markdown)" }
                    },
                    required = new[] { "commit1", "commit2", "filePath" }
                }
            },
            new Tool
            {
                Name = "gv_get_recent_commits",
                Description = "Get recent commits from the current repository",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        count = new { type = "integer", description = "Number of recent commits to retrieve (default: 10)" }
                    }
                }
            },
            new Tool
            {
                Name = "gv_get_changed_files_between_commits",
                Description = "Get list of files changed between two commits",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        commit1 = new { type = "string", description = "First commit hash" },
                        commit2 = new { type = "string", description = "Second commit hash" }
                    },
                    required = new[] { "commit1", "commit2" }
                }
            },
            new Tool
            {
                Name = "gv_get_detailed_diff_between_commits",
                Description = "Get detailed diff content between two commits",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        commit1 = new { type = "string", description = "First commit hash" },
                        commit2 = new { type = "string", description = "Second commit hash" },
                        specificFiles = new { type = "array", items = new { type = "string" }, description = "Optional: specific files to diff" }
                    },
                    required = new[] { "commit1", "commit2" }
                }
            },
            new Tool
            {
                Name = "gv_get_commit_diff_info",
                Description = "Get comprehensive diff information between two commits including file changes and statistics",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        commit1 = new { type = "string", description = "First commit hash" },
                        commit2 = new { type = "string", description = "Second commit hash" }
                    },
                    required = new[] { "commit1", "commit2" }
                }
            },
            new Tool
            {
                Name = "gv_get_file_line_diff_between_commits",
                Description = "Get line-by-line file diff between two commits",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        commit1 = new { type = "string", description = "First commit hash" },
                        commit2 = new { type = "string", description = "Second commit hash" },
                        filePath = new { type = "string", description = "Path to the file to diff" }
                    },
                    required = new[] { "commit1", "commit2", "filePath" }
                }
            },
            new Tool
            {
                Name = "gv_get_local_branches",
                Description = "Get list of local branches in the repository",
                InputSchema = new
                {
                    type = "object",
                    properties = new { }
                }
            },
            new Tool
            {
                Name = "gv_get_remote_branches",
                Description = "Get list of remote branches in the repository",
                InputSchema = new
                {
                    type = "object",
                    properties = new { }
                }
            },
            new Tool
            {
                Name = "gv_get_all_branches",
                Description = "Get list of all branches (local and remote) in the repository",
                InputSchema = new
                {
                    type = "object",
                    properties = new { }
                }
            },
            new Tool
            {
                Name = "gv_get_current_branch",
                Description = "Get the current active branch in the repository",
                InputSchema = new
                {
                    type = "object",
                    properties = new { }
                }
            },
            new Tool
            {
                Name = "gv_fetch_from_remote",
                Description = "Fetch latest changes from remote repository",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        remoteName = new { type = "string", description = "Name of the remote (default: origin)" }
                    }
                }
            },
            new Tool
            {
                Name = "gv_compare_branches_with_remote",
                Description = "Generate documentation comparing differences between two branches with remote support",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        branch1 = new { type = "string", description = "First branch name (can be local or remote, e.g., 'main' or 'origin/main')" },
                        branch2 = new { type = "string", description = "Second branch name (can be local or remote, e.g., 'feature/xyz' or 'origin/feature/xyz')" },
                        filePath = new { type = "string", description = "Path where to save the documentation file" },
                        outputFormat = new { type = "string", description = "Output format: markdown, html, or text (default: markdown)" },
                        fetchRemote = new { type = "boolean", description = "Whether to fetch from remote before comparison (default: true)" }
                    },
                    required = new[] { "branch1", "branch2", "filePath" }
                }
            },
            new Tool
            {
                Name = "gv_search_commits_for_string",
                Description = "Search all commits for a specific string and return commit details, file names, and line matches",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        searchString = new { type = "string", description = "The string to search for in commit messages and file contents" },
                        maxCommits = new { type = "integer", description = "Maximum number of commits to search through (default: 100)" }
                    },
                    required = new[] { "searchString" }
                }
            },
            new Tool
            {
                Name = "gv_list_workspace_files",
                Description = "List all files in the workspace with optional filtering",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        fileType = new { type = "string", description = "Filter by file type (extension without dot, e.g., 'cs', 'json')" },
                        relativePath = new { type = "string", description = "Filter by relative path (contains search)" },
                        fullPath = new { type = "string", description = "Filter by full path (contains search)" },
                        lastModifiedAfter = new { type = "string", description = "Filter by last modified date (ISO format: yyyy-MM-dd)" },
                        lastModifiedBefore = new { type = "string", description = "Filter by last modified date (ISO format: yyyy-MM-dd)" }
                    }
                }
            },
            new Tool
            {
                Name = "gv_read_filtered_workspace_files",
                Description = "Read contents of all files from filtered workspace results",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        fileType = new { type = "string", description = "Filter by file type (extension without dot, e.g., 'cs', 'json')" },
                        relativePath = new { type = "string", description = "Filter by relative path (contains search)" },
                        fullPath = new { type = "string", description = "Filter by full path (contains search)" },
                        lastModifiedAfter = new { type = "string", description = "Filter by last modified date (ISO format: yyyy-MM-dd)" },
                        lastModifiedBefore = new { type = "string", description = "Filter by last modified date (ISO format: yyyy-MM-dd)" },
                        maxFiles = new { type = "integer", description = "Maximum number of files to read (default: 500, max: 1000)" },
                        maxFileSize = new { type = "integer", description = "Maximum file size to read in bytes (default: 1MB)" }
                    }
                }
            },
            new Tool
            {
                Name = "gv_search_json_file",
                Description = "Search for JSON values in a JSON file using JSONPath",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        jsonFilePath = new { type = "string", description = "Path to the JSON file relative to workspace root" },
                        jsonPath = new { type = "string", description = "JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')" },
                        indented = new { type = "boolean", description = "Whether to format the output with indentation (default: true)" },
                        showKeyPaths = new { type = "boolean", description = "Whether to return structured results with path, value, and key information (default: false)" }
                    },
                    required = new[] { "jsonFilePath", "jsonPath" }
                }
            },
            new Tool
            {
                Name = "gv_search_csv_file",
                Description = "Search for values in a CSV file using a query",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        csvFilePath = new { type = "string", description = "Path to the CSV file relative to workspace root" },
                        jsonPath = new { type = "string", description = "JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')" },
                        hasHeaderRecord = new { type = "boolean", description = "Whether the CSV has a header record (default: true)" },
                        ignoreBlankLines = new { type = "boolean", description = "Whether to ignore blank lines (default: true)" }
                    },
                    required = new[] { "csvFilePath", "jsonPath" }
                }
            },
            new Tool
            {
                Name = "gv_search_yaml_file",
                Description = "Search for values in a YAML file using a query",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        yamlFilePath = new { type = "string", description = "Path to the YAML file relative to workspace root" },
                        jsonPath = new { type = "string", description = "JSONPath query string (e.g., '$.users[*].name', '$.configuration.apiKey')" },
                        indented = new { type = "boolean", description = "Whether the YAML output should be indented (default: true)" },
                        showKeyPaths = new { type = "boolean", description = "Whether to show key paths in the output (default: false)" }
                    },
                    required = new[] { "yamlFilePath", "jsonPath" }
                }
            },
            new Tool
            {
                Name = "gv_search_xml_file",
                Description = "Search for XML values in an XML file using XPath",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        xmlFilePath = new { type = "string", description = "Path to the XML file relative to workspace root" },
                        xPath = new { type = "string", description = "XPath query string (e.g., '//users/user/@email', '/configuration/database/host')" },
                        indented = new { type = "boolean", description = "Whether to format the output with indentation (default: true)" },
                        showKeyPaths = new { type = "boolean", description = "Whether to return structured results with path, value, and key information (default: false)" }
                    },
                    required = new[] { "xmlFilePath", "xPath" }
                }
            },
            new Tool
            {
                Name = "gv_deconstruct_to_json",
                Description = "Deconstructs a C# ASP.NET Service, Controller or repository file and returns its structure as JSON",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        filePath = new { type = "string", description = "Path to the Service, Controller or repository file relative to workspace root" }
                    },
                    required = new[] { "filePath" }
                }
            },
            new Tool
            {
                Name = "gv_deconstruct_to_file",
                Description = "Deconstructs a C# ASP.NET Service, Controller or repository file and saves the structure to a JSON file in the workspace directory",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        filePath = new { type = "string", description = "Path to the Service, Controller or repository file relative to workspace root" },
                        outputFileName = new { type = "string", description = "The name of the output JSON file (optional, defaults to controller name + '_analysis.json')" }
                    },
                    required = new[] { "filePath" }
                }
            },
            new Tool
            {
                Name = "gv_get_app_version",
                Description = "Get the application version from the project file",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        projectFile = new { type = "string", description = "Path to the project file (e.g., .csproj) relative to workspace root" }
                    },
                    required = new[] { "projectFile" }
                }
            },
            new Tool
            {
                Name = "gv_search_excel_file",
                Description = "Search for values in an Excel (.xlsx) file using JSONPath. Processes all worksheets and returns results for each.",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        excelFilePath = new { type = "string", description = "Path to the Excel file relative to workspace root" },
                        jsonPath = new { type = "string", description = "JSONPath query string (e.g., '$[*].ServerName')" }
                    },
                    required = new[] { "excelFilePath", "jsonPath" }
                }
            },
            new Tool
            {
                Name = "gv_list_workspace_files_with_cached_data",
                Description = "List workspace files with optional filtering using pre-fetched file data to improve performance",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        cachedFiles = new { type = "array", items = new { type = "object", properties = new { relativePath = new { type = "string" }, fullPath = new { type = "string" }, fileType = new { type = "string" }, size = new { type = "integer" }, lastModified = new { type = "string" } }, additionalProperties = false }, description = "Pre-fetched file data" },
                        fileType = new { type = "string", description = "Filter by file type (extension without dot, e.g., 'cs', 'json')" },
                        relativePath = new { type = "string", description = "Filter by relative path (contains search)" },
                        fullPath = new { type = "string", description = "Filter by full path (contains search)" },
                        lastModifiedAfter = new { type = "string", description = "Filter by last modified date (ISO format: yyyy-MM-dd)" },
                        lastModifiedBefore = new { type = "string", description = "Filter by last modified date (ISO format: yyyy-MM-dd)" }
                    },
                    required = new[] { "cachedFiles" }
                }
            },
            new Tool
            {
                Name = "gv_transform_xml_with_xslt",
                Description = "Transform an XML file using an XSLT stylesheet",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                        xmlFilePath = new { type = "string", description = "Path to the XML file relative to workspace root" },
                        xsltFilePath = new { type = "string", description = "Path to the XSLT stylesheet file relative to workspace root" },
                        destinationFilePath = new { type = "string", description = "Optional path to save the transformed XML to a file" }
                    },
                    required = new[] { "xmlFilePath", "xsltFilePath" }
                }
            },
            new Tool
            {
                Name = "gv_git_find_merge_conflicts",
                Description = "Find merge conflict markers in workspace files and return detailed conflict information",
                InputSchema = new
                {
                    type = "object",
                    properties = new
                    {
                    }
                }
            }
        };

        var response = new ToolsListResponse { Tools = tools };

        return await Task.FromResult(new JsonRpcResponse
        {
            Id = request.Id,
            Result = response
        });
    }

    private async Task<JsonRpcResponse> HandleToolCallAsync(JsonRpcRequest request)
    {
        _logger.LogDebug("Handling tools/call request");

        if (request.Params == null)
        {
            return CreateErrorResponse(request.Id, -32602, "Invalid params");
        }

        var toolRequest = JsonConvert.DeserializeObject<CallToolRequest>(
            JsonConvert.SerializeObject(request.Params, _jsonSettings), _jsonSettings);

        if (toolRequest == null)
        {
            return CreateErrorResponse(request.Id, -32602, "Invalid tool request");
        }

        var response = toolRequest.Name switch
        {
            "gv_compare_branches_documentation" => await HandleCompareBranchesDocumentationAsync(toolRequest),
            "gv_compare_branches_with_remote_documentation" => await HandleCompareBranchesWithRemoteAsync(toolRequest),
            "gv_compare_commits_documentation" => await HandleCompareCommitsDocumentationAsync(toolRequest),
            "gv_deconstruct_to_file" => await HandleDeconstructSourceToFileAsync(toolRequest),
            "gv_deconstruct_to_json" => await HandleDeconstructSourceAsync(toolRequest),
            "gv_fetch_from_remote" => await HandleFetchFromRemoteAsync(toolRequest),
            "gv_generate_git_commit_report" => await HandleGenerateGitDocumentationAsync(toolRequest),
            "gv_generate_git_documentation_to_file" => await HandleGenerateGitDocumentationToFileAsync(toolRequest),
            "gv_get_all_branches" => await HandleGetAllBranchesAsync(toolRequest),
            "gv_get_app_version" => await HandleGetAppVersionAsync(toolRequest),
            "gv_get_changed_files_between_commits" => await HandleGetChangedFilesBetweenCommitsAsync(toolRequest),
            "gv_get_commit_diff_info" => await HandleGetCommitDiffInfoAsync(toolRequest),
            "gv_get_current_branch" => await HandleGetCurrentBranchAsync(toolRequest),
            "gv_get_detailed_diff_between_commits" => await HandleGetDetailedDiffBetweenCommitsAsync(toolRequest),
            "gv_get_file_line_diff_between_commits" => await HandleGetFileLineDiffBetweenCommitsAsync(toolRequest),
            "gv_get_local_branches" => await HandleGetLocalBranchesAsync(toolRequest),
            "gv_get_recent_commits" => await HandleGetRecentCommitsAsync(toolRequest),
            "gv_get_remote_branches" => await HandleGetRemoteBranchesAsync(toolRequest),
            "gv_git_find_merge_conflicts" => await HandleGitFindMergeConflictsAsync(toolRequest),
            "gv_list_workspace_files" => await HandleListWorkspaceFilesAsync(toolRequest),
            "gv_list_workspace_files_with_cached_data" => await HandleListWorkspaceFilesWithCachedDataAsync(toolRequest),
            "gv_read_filtered_workspace_files" => await HandleReadFilteredWorkspaceFilesAsync(toolRequest),
            "gv_search_commits_for_string" => await HandleSearchCommitsForStringAsync(toolRequest),
            "gv_search_csv_file" => await HandleSearchCsvFileAsync(toolRequest),
            "gv_search_excel_file" => await HandleSearchExcelFileAsync(toolRequest),
            "gv_search_json_file" => await HandleSearchJsonFileAsync(toolRequest),
            "gv_search_xml_file" => await HandleSearchXmlFileAsync(toolRequest),
            "gv_search_yaml_file" => await HandleSearchYamlFileAsync(toolRequest),
            "gv_transform_xml_with_xslt" => await HandleTransformXmlWithXsltAsync(toolRequest),

            _ => new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = "Unknown tool" } }
            }
        };

        return new JsonRpcResponse
        {
            Id = request.Id,
            Result = response
        };
    }

    private async Task<CallToolResponse> HandleGenerateGitDocumentationAsync(CallToolRequest toolRequest)
    {
        try
        {
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var maxCommits = GetArgumentValue(toolRequest.Arguments, "maxCommits", 50);
            var outputFormat = GetArgumentValue(toolRequest.Arguments, "outputFormat", "markdown");

            var commits = await _gitService.GetGitLogsAsync(workspaceRoot, maxCommits);
            var documentation = await _gitService.GenerateCommitDocumentationAsync(commits, outputFormat);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = documentation } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating git documentation");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGenerateGitDocumentationToFileAsync(CallToolRequest toolRequest)
    {
        try
        {
            var filePath = GetArgumentValue(toolRequest.Arguments, "filePath", "");
            if (string.IsNullOrEmpty(filePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "filePath argument is required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var maxCommits = GetArgumentValue(toolRequest.Arguments, "maxCommits", 50);
            var outputFormat = GetArgumentValue(toolRequest.Arguments, "outputFormat", "markdown");

            // Make path relative to workspace if not absolute
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(workspaceRoot, filePath);
            }

            var commits = await _gitService.GetGitLogsAsync(workspaceRoot, maxCommits);
            var documentation = await _gitService.GenerateCommitDocumentationAsync(commits, outputFormat);
            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent
                {
                    Type = "text",
                    Text = success ? $"Documentation successfully written to {filePath}" : "Failed to write documentation to file"
                } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating git documentation to file");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleCompareBranchesDocumentationAsync(CallToolRequest toolRequest)
    {
        try
        {
            var branch1 = GetArgumentValue(toolRequest.Arguments, "branch1", "");
            var branch2 = GetArgumentValue(toolRequest.Arguments, "branch2", "");
            var filePath = GetArgumentValue(toolRequest.Arguments, "filePath", "");

            if (string.IsNullOrEmpty(branch1) || string.IsNullOrEmpty(branch2) || string.IsNullOrEmpty(filePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "branch1, branch2, and filePath arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var outputFormat = GetArgumentValue(toolRequest.Arguments, "outputFormat", "markdown");

            // Make path relative to workspace if not absolute
            var fullPath = _fileService.GetFullPath(filePath);
            if (string.IsNullOrEmpty(fullPath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "Invalid file path" } }
                };
            }


            var commits = await _gitService.GetGitLogsBetweenBranchesAsync(workspaceRoot, branch1, branch2);
            var documentation = await _gitService.GenerateCommitDocumentationAsync(commits, outputFormat);
            var success = await _gitService.WriteDocumentationToFileAsync(documentation, fullPath);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent
                {
                    Type = "text",
                    Text = success ? $"Branch comparison documentation successfully written to {filePath}" : "Failed to write documentation to file"
                } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating branch comparison documentation");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleCompareCommitsDocumentationAsync(CallToolRequest toolRequest)
    {
        try
        {
            var commit1 = GetArgumentValue(toolRequest.Arguments, "commit1", "");
            var commit2 = GetArgumentValue(toolRequest.Arguments, "commit2", "");
            var filePath = GetArgumentValue(toolRequest.Arguments, "filePath", "");

            if (string.IsNullOrEmpty(commit1) || string.IsNullOrEmpty(commit2) || string.IsNullOrEmpty(filePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "commit1, commit2, and filePath arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var outputFormat = GetArgumentValue(toolRequest.Arguments, "outputFormat", "markdown");



            // Make path relative to workspace if not absolute
            var fullPath = _fileService.GetFullPath(filePath);
            if (string.IsNullOrEmpty(fullPath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "Invalid file path" } }
                };
            }

            var commits = await _gitService.GetGitLogsBetweenCommitsAsync(workspaceRoot, commit1, commit2);
            var documentation = await _gitService.GenerateCommitDocumentationAsync(commits, outputFormat);
            var success = await _gitService.WriteDocumentationToFileAsync(documentation, fullPath);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent
                {
                    Type = "text",
                    Text = success ? $"Commit comparison documentation successfully written to {fullPath}" : "Failed to write documentation to file"
                } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating commit comparison documentation");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetRecentCommitsAsync(CallToolRequest toolRequest)
    {
        try
        {
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var count = GetArgumentValue(toolRequest.Arguments, "count", 10);

            var commits = await _gitService.GetRecentCommitsAsync(workspaceRoot, count);
            var commitSummary = string.Join("\n", commits.Select(c =>
                $"• {c.Hash[..8]} - {c.Message.Split('\n')[0]} ({c.Author}, {c.Date:yyyy-MM-dd})"));

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = $"Recent {count} commits:\n\n{commitSummary}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting recent commits");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetChangedFilesBetweenCommitsAsync(CallToolRequest toolRequest)
    {
        try
        {
            var commit1 = GetArgumentValue(toolRequest.Arguments, "commit1", "");
            var commit2 = GetArgumentValue(toolRequest.Arguments, "commit2", "");

            if (string.IsNullOrEmpty(commit1) || string.IsNullOrEmpty(commit2))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "commit1 and commit2 arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var changedFiles = await _gitService.GetChangedFilesBetweenCommitsAsync(workspaceRoot, commit1, commit2);
            var filesList = string.Join("\n", changedFiles.Select(f => $"• {f}"));

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = $"Files changed between {commit1[..8]} and {commit2[..8]}:\n\n{filesList}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting changed files between commits");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetDetailedDiffBetweenCommitsAsync(CallToolRequest toolRequest)
    {
        try
        {
            var commit1 = GetArgumentValue(toolRequest.Arguments, "commit1", "");
            var commit2 = GetArgumentValue(toolRequest.Arguments, "commit2", "");

            if (string.IsNullOrEmpty(commit1) || string.IsNullOrEmpty(commit2))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "commit1 and commit2 arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var specificFilesArg = GetArgumentValue<object?>(toolRequest.Arguments, "specificFiles", null);
            List<string>? specificFiles = null;

            if (specificFilesArg is JArray jsonArray)
            {
                specificFiles = jsonArray
                    .Select(e => e.Type == JTokenType.String ? (string?)e : e.ToString())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => s ?? string.Empty)
                    .ToList();
            }

            var detailedDiff = await _gitService.GetDetailedDiffBetweenCommitsAsync(workspaceRoot, commit1, commit2, specificFiles);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = detailedDiff } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting detailed diff between commits");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetCommitDiffInfoAsync(CallToolRequest toolRequest)
    {
        try
        {
            var commit1 = GetArgumentValue(toolRequest.Arguments, "commit1", "");
            var commit2 = GetArgumentValue(toolRequest.Arguments, "commit2", "");

            if (string.IsNullOrEmpty(commit1) || string.IsNullOrEmpty(commit2))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "commit1 and commit2 arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var diffInfo = await _gitService.GetCommitDiffInfoAsync(workspaceRoot, commit1, commit2);

            var summary = $"Commit Diff Summary ({commit1[..8]} → {commit2[..8]}):\n\n" +
                         $"📊 **Statistics:**\n" +
                         $"• Added files: {diffInfo.AddedFiles.Count}\n" +
                         $"• Modified files: {diffInfo.ModifiedFiles.Count}\n" +
                         $"• Deleted files: {diffInfo.DeletedFiles.Count}\n" +
                         $"• Renamed files: {diffInfo.RenamedFiles.Count}\n" +
                         $"• Total changes: {diffInfo.TotalChanges}\n\n";

            if (diffInfo.AddedFiles.Any())
                summary += $"➕ **Added Files:**\n{string.Join("\n", diffInfo.AddedFiles.Select(f => $"  • {f}"))}\n\n";

            if (diffInfo.ModifiedFiles.Any())
                summary += $"✏️ **Modified Files:**\n{string.Join("\n", diffInfo.ModifiedFiles.Select(f => $"  • {f}"))}\n\n";

            if (diffInfo.DeletedFiles.Any())
                summary += $"🗑️ **Deleted Files:**\n{string.Join("\n", diffInfo.DeletedFiles.Select(f => $"  • {f}"))}\n\n";

            if (diffInfo.RenamedFiles.Any())
                summary += $"📝 **Renamed Files:**\n{string.Join("\n", diffInfo.RenamedFiles.Select(f => $"  • {f}"))}\n\n";

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = summary } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting commit diff info");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetLocalBranchesAsync(CallToolRequest toolRequest)
    {
        try
        {
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var localBranches = await _gitService.GetLocalBranchesAsync(workspaceRoot);
            var branchList = string.Join("\n", localBranches.Select(b => $"• {b}"));

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = $"Local branches:\n\n{branchList}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting local branches");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetRemoteBranchesAsync(CallToolRequest toolRequest)
    {
        try
        {
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var remoteBranches = await _gitService.GetRemoteBranchesAsync(workspaceRoot);
            var branchList = string.Join("\n", remoteBranches.Select(b => $"• {b}"));

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = $"Remote branches:\n\n{branchList}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting remote branches");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetAllBranchesAsync(CallToolRequest toolRequest)
    {
        try
        {
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var allBranches = await _gitService.GetAllBranchesAsync(workspaceRoot);
            var branchList = string.Join("\n", allBranches.Select(b => $"• {b}"));

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = $"All branches:\n\n{branchList}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all branches");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGetCurrentBranchAsync(CallToolRequest toolRequest)
    {
        try
        {
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var currentBranch = await _gitService.GetCurrentBranchAsync(workspaceRoot);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = $"Current branch: {currentBranch}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current branch");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleFetchFromRemoteAsync(CallToolRequest toolRequest)
    {
        try
        {
            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var remoteName = GetArgumentValue(toolRequest.Arguments, "remoteName", "origin");
            var success = await _gitService.FetchFromRemoteAsync(workspaceRoot, remoteName);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent
                {
                    Type = "text",
                    Text = success ? $"Successfully fetched from remote '{remoteName}'" : $"Failed to fetch from remote '{remoteName}'"
                } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching from remote");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleCompareBranchesWithRemoteAsync(CallToolRequest toolRequest)
    {
        try
        {
            var branch1 = GetArgumentValue(toolRequest.Arguments, "branch1", "");
            var branch2 = GetArgumentValue(toolRequest.Arguments, "branch2", "");
            var filePath = GetArgumentValue(toolRequest.Arguments, "filePath", "");

            if (string.IsNullOrEmpty(branch1) || string.IsNullOrEmpty(branch2) || string.IsNullOrEmpty(filePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "branch1, branch2, and filePath arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var outputFormat = GetArgumentValue(toolRequest.Arguments, "outputFormat", "markdown");
            var fetchRemote = GetArgumentValue(toolRequest.Arguments, "fetchRemote", true);

            // Make path relative to workspace if not absolute
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(workspaceRoot, filePath);
            }

            var commits = await _gitService.GetGitLogsBetweenBranchesWithRemoteAsync(workspaceRoot, branch1, branch2, fetchRemote);
            var documentation = await _gitService.GenerateCommitDocumentationAsync(commits, outputFormat);
            var success = await _gitService.WriteDocumentationToFileAsync(documentation, filePath);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent
                {
                    Type = "text",
                    Text = success ? $"Remote branch comparison documentation successfully written to {filePath}" : "Failed to write documentation to file"
                } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating remote branch comparison documentation");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleSearchCommitsForStringAsync(CallToolRequest toolRequest)
    {
        try
        {
            var searchString = GetArgumentValue(toolRequest.Arguments, "searchString", "");

            if (string.IsNullOrEmpty(searchString))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "searchString argument is required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var maxCommits = GetArgumentValue(toolRequest.Arguments, "maxCommits", 100);

            var searchResults = await _gitService.SearchCommitsForStringAsync(workspaceRoot, searchString, maxCommits);

            if (!string.IsNullOrEmpty(searchResults.ErrorMessage))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = $"Search error: {searchResults.ErrorMessage}" } }
                };
            }

            // Format the results as a comprehensive text response
            var resultText = FormatSearchResults(searchResults);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = resultText } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching commits for string");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private string FormatSearchResults(CommitSearchResponse searchResults)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"# Search Results for: '{searchResults.SearchString}'");
        sb.AppendLine();
        sb.AppendLine($"**Summary:**");
        sb.AppendLine($"- Total commits searched: {searchResults.TotalCommitsSearched}");
        sb.AppendLine($"- Commits with matches: {searchResults.TotalMatchingCommits}");
        sb.AppendLine($"- Total line matches: {searchResults.TotalLineMatches}");
        sb.AppendLine();

        if (!searchResults.Results.Any())
        {
            sb.AppendLine("No matches found.");
            return sb.ToString();
        }

        sb.AppendLine("## Matching Commits");
        sb.AppendLine();

        foreach (var result in searchResults.Results.OrderByDescending(r => r.Timestamp))
        {
            sb.AppendLine($"### Commit: {result.CommitHash[..8]}");
            sb.AppendLine($"**Author:** {result.Author}");
            sb.AppendLine($"**Date:** {result.Timestamp:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"**Message:** {result.CommitMessage}");
            sb.AppendLine($"**Total matches in this commit:** {result.TotalMatches}");
            sb.AppendLine();

            foreach (var fileMatch in result.FileMatches)
            {
                sb.AppendLine($"#### File: {fileMatch.FileName}");
                sb.AppendLine($"**Matches in this file:** {fileMatch.LineMatches.Count}");
                sb.AppendLine();

                foreach (var lineMatch in fileMatch.LineMatches)
                {
                    sb.AppendLine($"- **Line {lineMatch.LineNumber}:** `{lineMatch.LineContent}`");
                }
                sb.AppendLine();
            }

            sb.AppendLine("---");
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private async Task<CallToolResponse> HandleListWorkspaceFilesAsync(CallToolRequest toolRequest)
    {
        try
        {
            var fileType = GetArgumentValue<string?>(toolRequest.Arguments, "fileType", null);
            var relativePath = GetArgumentValue<string?>(toolRequest.Arguments, "relativePath", null);
            var fullPath = GetArgumentValue<string?>(toolRequest.Arguments, "fullPath", null);
            var lastModifiedAfter = GetArgumentValue<string?>(toolRequest.Arguments, "lastModifiedAfter", null);
            var lastModifiedBefore = GetArgumentValue<string?>(toolRequest.Arguments, "lastModifiedBefore", null);

            // Get all files once and pass them to GitServiceTools to avoid redundant calls
            var myPath = "*";

            if (!string.IsNullOrEmpty(relativePath) && relativePath != "*")
            {
                myPath = relativePath;
            }
            var allFiles = _fileService.GetAllFilesMatching(myPath);

            // Capture the total count before filtering
            var totalCount = allFiles.Count;

            // Modify GitServiceTools implementation to accept the pre-fetched file list
            var result = await _gitServiceTools.ListWorkspaceFilesWithCachedDataAsync(
                allFiles,
                fileType,
                relativePath,
                fullPath,
                lastModifiedAfter,
                lastModifiedBefore);

            _logger.LogInformation("Listed {FilteredCount} files out of {TotalCount} total files", result.Count, totalCount);

            var response = new CallToolResponse
            {
                Content = new[]
                {
                    new ToolContent
                    {
                        Type = "text",
                        Text = JsonConvert.SerializeObject(result, _jsonSettings)
                    }
                }
            };

            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing workspace files");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error listing workspace files: {ex.Message}" } }
            };
        }
    }

    /// <summary>
    /// Handles the read_filtered_workspace_files tool, reading contents of filtered workspace files.
    /// </summary>
    /// <param name="toolRequest">The tool request containing filter parameters.</param>
    /// <returns>A CallToolResponse containing the file contents or error message.</returns>
    private async Task<CallToolResponse> HandleReadFilteredWorkspaceFilesAsync(CallToolRequest toolRequest)
    {
        try
        {
            var fileType = GetArgumentValue<string?>(toolRequest.Arguments, "fileType", null);
            var relativePath = GetArgumentValue<string?>(toolRequest.Arguments, "relativePath", null);
            var fullPath = GetArgumentValue<string?>(toolRequest.Arguments, "fullPath", null);
            var lastModifiedAfter = GetArgumentValue<string?>(toolRequest.Arguments, "lastModifiedAfter", null);
            var lastModifiedBefore = GetArgumentValue<string?>(toolRequest.Arguments, "lastModifiedBefore", null);
            var maxFiles = GetArgumentValue<int?>(toolRequest.Arguments, "maxFiles", 500);
            var maxFileSize = GetArgumentValue<long?>(toolRequest.Arguments, "maxFileSize", 1048576);

            _logger.LogInformation("Reading filtered workspace files with parameters: fileType={FileType}, relativePath={RelativePath}, maxFiles={MaxFiles}, maxFileSize={MaxFileSize}",
                fileType, relativePath, maxFiles, maxFileSize);

            var result = await _gitServiceTools.ReadFilteredWorkspaceFilesAsync(
                fileType,
                relativePath,
                fullPath,
                lastModifiedAfter,
                lastModifiedBefore,
                maxFiles,
                maxFileSize);

            _logger.LogInformation("Successfully read {FileCount} filtered workspace files", result.Count);

            return new CallToolResponse
            {
                Content = new[]
                {
                    new ToolContent
                    {
                        Type = "text",
                        Text = JsonConvert.SerializeObject(result, _jsonSettings)
                    }
                }
            };
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid arguments for read_filtered_workspace_files");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Invalid arguments: {ex.Message}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading filtered workspace files");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error reading filtered workspace files: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleSearchCsvFileAsync(CallToolRequest toolRequest)
    {
        try
        {
            var csvFilePath = GetArgumentValue(toolRequest.Arguments, "csvFilePath", "");
            var jsonPath = GetArgumentValue(toolRequest.Arguments, "jsonPath", "");
            var hasHeaderRecord = GetArgumentValue(toolRequest.Arguments, "hasHeaderRecord", true);
            var ignoreBlankLines = GetArgumentValue(toolRequest.Arguments, "ignoreBlankLines", true);

            if (string.IsNullOrEmpty(csvFilePath) || string.IsNullOrEmpty(jsonPath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "csvFilePath and jsonPath arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            // Make path relative to workspace if not absolute
            if (!Path.IsPathRooted(csvFilePath))
            {
                csvFilePath = Path.Combine(workspaceRoot, csvFilePath);
            }

            var result = await _gitServiceTools.SearchCsvFileAsync(csvFilePath, jsonPath, hasHeaderRecord, ignoreBlankLines);
            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = result ?? "No results found" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching CSV file");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleSearchExcelFileAsync(CallToolRequest toolRequest)
    {
        try
        {
            var excelFilePath = GetArgumentValue(toolRequest.Arguments, "excelFilePath", "");
            var jsonPath = GetArgumentValue(toolRequest.Arguments, "jsonPath", "");

            if (string.IsNullOrEmpty(excelFilePath) || string.IsNullOrEmpty(jsonPath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "excelFilePath and jsonPath arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            // Make path relative to workspace if not absolute
            if (!Path.IsPathRooted(excelFilePath))
            {
                excelFilePath = Path.Combine(workspaceRoot, excelFilePath);
            }

            var result = await _gitServiceTools.SearchExcelFileAsync(excelFilePath, jsonPath);
            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = result ?? "No results found" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching Excel file");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleSearchJsonFileAsync(CallToolRequest toolRequest)
    {
        try
        {
            var jsonFilePath = GetArgumentValue(toolRequest.Arguments, "jsonFilePath", "");
            var jsonPath = GetArgumentValue(toolRequest.Arguments, "jsonPath", "");
            var indented = GetArgumentValue<bool?>(toolRequest.Arguments, "indented", true);
            var showKeyPaths = GetArgumentValue<bool?>(toolRequest.Arguments, "showKeyPaths", false);

            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "jsonFilePath argument is required" } }
                };
            }

            if (string.IsNullOrWhiteSpace(jsonPath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "jsonPath argument is required" } }
                };
            }

            var result = await _gitServiceTools.SearchJsonFileAsync(jsonFilePath, jsonPath, indented, showKeyPaths);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = result ?? "No results found" } }
            };
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid arguments for SearchJsonFile");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Invalid arguments: {ex.Message}" } }
            };
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "JSON file not found");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"File not found: {ex.Message}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching JSON file");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error searching JSON file: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleSearchXmlFileAsync(CallToolRequest toolRequest)
    {
        try
        {
            var xmlFilePath = GetArgumentValue(toolRequest.Arguments, "xmlFilePath", "");
            var xPath = GetArgumentValue(toolRequest.Arguments, "xPath", "");
            var indented = GetArgumentValue<bool?>(toolRequest.Arguments, "indented", true);
            var showKeyPaths = GetArgumentValue<bool?>(toolRequest.Arguments, "showKeyPaths", false);

            if (string.IsNullOrWhiteSpace(xmlFilePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "xmlFilePath argument is required" } }
                };
            }

            if (string.IsNullOrWhiteSpace(xPath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "xPath argument is required" } }
                };
            }

            var result = await _gitServiceTools.SearchXmlFileAsync(xmlFilePath, xPath, indented, showKeyPaths);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = result ?? "No results found" } }
            };
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid arguments for SearchXmlFile");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Invalid arguments: {ex.Message}" } }
            };
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "XML file not found");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"File not found: {ex.Message}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching XML file");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error searching XML file: {ex.Message}" } }
            };
        }
    }

    private Task<CallToolResponse> HandleSearchYamlFileAsync(CallToolRequest toolRequest)
    {
        try
        {
            var yamlFilePath = GetArgumentValue(toolRequest.Arguments, "yamlFilePath", "");
            var jsonPath = GetArgumentValue(toolRequest.Arguments, "jsonPath", "");
            var indented = GetArgumentValue(toolRequest.Arguments, "indented", true);
            var showKeyPaths = GetArgumentValue(toolRequest.Arguments, "showKeyPaths", false);

            if (string.IsNullOrEmpty(yamlFilePath) || string.IsNullOrEmpty(jsonPath))
            {
                return Task.FromResult(new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "yamlFilePath and jsonPath arguments are required" } }
                });
            }

            var result = _workspaceService.SearchYamlFile(yamlFilePath, jsonPath, indented, showKeyPaths);
            if (result == null)
            {
                return Task.FromResult(new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "No results found or error searching YAML file." } }
                });
            }

            return Task.FromResult(new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = result } }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching YAML file");
            return Task.FromResult(new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            });
        }
    }

    private async Task<CallToolResponse> HandleDeconstructSourceAsync(CallToolRequest toolRequest)
    {
        try
        {
            var filePath = GetArgumentValue(toolRequest.Arguments, "filePath", "");

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "filePath argument is required" } }
                };
            }

            var result = await _gitServiceTools.DeconstructAsync(filePath);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = result ?? "Failed to deconstruct source" } }
            };
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid arguments for Deconstruct");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Invalid arguments: {ex.Message}" } }
            };
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "Controller file not found");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"File not found: {ex.Message}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error analyzing controller file");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error deconstructing source: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleDeconstructSourceToFileAsync(CallToolRequest toolRequest)
    {
        try
        {
            var filePath = GetArgumentValue(toolRequest.Arguments, "filePath", "");
            var outputFileName = GetArgumentValue<string?>(toolRequest.Arguments, "outputFileName", null);

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "filePath argument is required" } }
                };
            }

            var result = await _gitServiceTools.DeconstructToJsonAsync(filePath, outputFileName);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = result ?? "Failed to deconstruct and save json" } }
            };
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid arguments for DeconstructToFile");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Invalid arguments: {ex.Message}" } }
            };
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, "Source file not found");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"File not found: {ex.Message}" } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deconstructing source to file");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error deconstructing source to file: {ex.Message}" } }
            };
        }
    }

    /// <summary>
    /// Handles the get_app_version tool, extracting version info from a project file (e.g., .csproj).
    /// </summary>
    /// <param name="toolRequest">The RPC request with arguments, expects "projectFile".</param>
    /// <returns>A CallToolResponse containing the version string or error message.</returns>
    private Task<CallToolResponse> HandleGetAppVersionAsync(CallToolRequest toolRequest)
    {
        try
        {
            // Extract project file path argument
            var projectFile = GetArgumentValue(toolRequest.Arguments, "projectFile", string.Empty);
            // Call the location service to get the version

            var version = _utilityService.GetAppVersion(projectFile) ?? string.Empty;
            var response = new CallToolResponse
            {
                IsError = false,
                Content = new[] { new ToolContent { Type = "text", Text = version } }
            };
            return Task.FromResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing get_app_version");
            var errorResponse = new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = ex.Message } }
            };
            return Task.FromResult(errorResponse);
        }
    }

    private async Task<CallToolResponse> HandleListWorkspaceFilesWithCachedDataAsync(CallToolRequest toolRequest)
    {
        try
        {
            var cachedFilesArg = GetArgumentValue<object?>(toolRequest.Arguments, "cachedFiles", null);
            if (cachedFilesArg is not JArray cachedFilesElement)
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "cachedFiles argument is required and must be an array" } }
                };
            }

            // Convert JArray to List<WorkspaceFileInfo>
            var cachedFiles = cachedFilesElement
                .Select(item => new WorkspaceFileInfo
                {
                    RelativePath = item["relativePath"]?.ToString() ?? "",
                    FullPath = item["fullPath"]?.ToString() ?? "",
                    FileType = item["fileType"]?.ToString() ?? "",
                    Size = item["size"]?.Value<long>() ?? 0,
                    LastModified = DateTime.TryParse(item["lastModified"]?.ToString(), out var lastModified) ? lastModified : DateTime.MinValue
                })
                .ToList();

            var fileType = GetArgumentValue(toolRequest.Arguments, "fileType", "");
            var relativePath = GetArgumentValue(toolRequest.Arguments, "relativePath", "");
            var fullPath = GetArgumentValue(toolRequest.Arguments, "fullPath", "");
            var lastModifiedAfter = GetArgumentValue(toolRequest.Arguments, "lastModifiedAfter", "");
            var lastModifiedBefore = GetArgumentValue(toolRequest.Arguments, "lastModifiedBefore", "");

            var result = await _gitServiceTools.ListWorkspaceFilesWithCachedDataAsync(
                cachedFiles, fileType, relativePath, fullPath, lastModifiedAfter, lastModifiedBefore);

            var jsonResult = JsonConvert.SerializeObject(result, _outputJsonSettings);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = jsonResult } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing workspace files with cached data");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleTransformXmlWithXsltAsync(CallToolRequest toolRequest)
    {
        try
        {
            var xmlFilePath = GetArgumentValue(toolRequest.Arguments, "xmlFilePath", "");
            var xsltFilePath = GetArgumentValue(toolRequest.Arguments, "xsltFilePath", "");
            var destinationFilePath = GetArgumentValue(toolRequest.Arguments, "destinationFilePath", "");

            if (string.IsNullOrEmpty(xmlFilePath) || string.IsNullOrEmpty(xsltFilePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "xmlFilePath and xsltFilePath arguments are required" } }
                };
            }

            var result = await _gitServiceTools.TransformXmlWithXsltAsync(xmlFilePath, xsltFilePath,
                string.IsNullOrEmpty(destinationFilePath) ? null : destinationFilePath);

            var responseMessage = result ?? "Transformation failed";
            if (!string.IsNullOrEmpty(destinationFilePath) && !string.IsNullOrEmpty(result))
            {
                responseMessage += $"\nTransformed XML saved to: {destinationFilePath}";
            }

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = responseMessage } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error transforming XML with XSLT");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }

    private async Task<CallToolResponse> HandleGitFindMergeConflictsAsync(CallToolRequest toolRequest)
    {
        try
        {


            var results = new List<ConflictResult>();
            var workspaceFileList = await _fileService.GetAllFilesAsync();
            var fileContents = await _fileService.GetFileContentsAsync(workspaceFileList);
            results = await _gitService.FindAllGitConflictMarkers(fileContents);

            if (results == null || !results.Any())
            {
                _logger.LogInformation("No merge conflicts found in the workspace");
                return new CallToolResponse
                {
                    Content = new[] { new ToolContent { Type = "text", Text = "No merge conflicts found." } }
                };
            }

            var count = results.Count;
            var jsonResult = JsonConvert.SerializeObject(results, _outputJsonSettings);

            _logger.LogInformation("Found {ConflictCount} merge conflicts in {FileCount} files", count, workspaceFileList.Count);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = jsonResult } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding merge conflicts");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error finding merge conflicts: {ex.Message}" } }
            };
        }
    }

    private T GetArgumentValue<T>(object? arguments, string key, T defaultValue)
    {
        if (arguments is JObject obj)
        {
            var property = obj[key];
            if (property != null)
            {
                try
                {
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)(property.Type == JTokenType.Null ? string.Empty : property.ToString());
                    }
                    else if (typeof(T) == typeof(int))
                    {
                        if (property.Type == JTokenType.Integer && property.Value<int?>() != null)
                            return (T)(object)property.Value<int?>().GetValueOrDefault();
                        return defaultValue;
                    }
                    else if (typeof(T) == typeof(bool))
                    {
                        if (property.Type == JTokenType.Boolean && property.Value<bool?>() != null)
                            return (T)(object)property.Value<bool?>().GetValueOrDefault();
                        return defaultValue;
                    }
                    else if (typeof(T) == typeof(object))
                    {
                        return (T)(object)property;
                    }
                    else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var underlyingType = Nullable.GetUnderlyingType(typeof(T));
                        if (underlyingType == typeof(string))
                        {
                            var value = property.Type == JTokenType.Null ? null : property.ToString();
                            return property.Type == JTokenType.Null ? defaultValue : (T)(object)value!;
                        }
                    }
                    // Handle nullable reference types
                    else if (!typeof(T).IsValueType)
                    {
                        if (property.Type == JTokenType.Null)
                        {
                            return defaultValue;
                        }
                        if (typeof(T).Name.Contains("String"))
                        {
                            var value = property.ToString();
                            return (T)(object)(value ?? string.Empty);
                        }
                        return (T)(object)property;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error converting argument {Key} to type {Type}", key, typeof(T));
                }
            }
        }

        return defaultValue;
    }

    private JsonRpcResponse CreateErrorResponse(object? id, int code, string message, string? data = null)
    {
        return new JsonRpcResponse
        {
            Id = id,
            Error = new JsonRpcError
            {
                Code = code,
                Message = message,
                Data = data
            }
        };
    }

    private async Task SendErrorResponseAsync(object? id, int code, string message)
    {
        var errorResponse = CreateErrorResponse(id, code, message);
        var compactJson = CreateCompactJsonResponse(errorResponse);

        var jsonBytes = Encoding.UTF8.GetBytes(compactJson);
        await Console.OpenStandardOutput().WriteAsync(jsonBytes, 0, jsonBytes.Length);
        await Console.OpenStandardOutput().WriteAsync(Encoding.UTF8.GetBytes("\n"), 0, 1);
        await Console.OpenStandardOutput().FlushAsync();
    }

    private string CreateCompactJsonResponse(JsonRpcResponse response)
    {
        return JsonConvert.SerializeObject(response, _outputJsonSettings);
    }

    private async Task<JsonRpcResponse> HandlePromptGetAsync(JsonRpcRequest request)
    {
        try
        {
            if (request.Params == null)
            {
                return CreateErrorResponse(request.Id, -32602, "Invalid params");
            }

            var promptRequest = JsonConvert.DeserializeObject<PromptGetRequest>(
                JsonConvert.SerializeObject(request.Params, _jsonSettings), _jsonSettings);

            if (promptRequest == null || string.IsNullOrEmpty(promptRequest.Name))
            {
                return CreateErrorResponse(request.Id, -32602, "Invalid prompt request");
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var commits = await _gitService.GetGitLogsAsync(workspaceRoot, 50);

            var promptContent = promptRequest.Name switch
            {
                "release_document" => "Release document prompt not implemented",
                "release_document_with_version" => "Release document with version prompt not implemented",
                _ => "Unknown prompt"
            };

            return await Task.FromResult(new JsonRpcResponse
            {
                Id = request.Id,
                Result = new
                {
                    prompt = promptContent
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling prompt request");
            return CreateErrorResponse(request.Id, -32603, "Internal error", ex.Message);
        }
    }

    private async Task<CallToolResponse> HandleGetFileLineDiffBetweenCommitsAsync(CallToolRequest toolRequest)
    {
        try
        {
            var commit1 = GetArgumentValue(toolRequest.Arguments, "commit1", "");
            var commit2 = GetArgumentValue(toolRequest.Arguments, "commit2", "");
            var filePath = GetArgumentValue(toolRequest.Arguments, "filePath", "");

            if (string.IsNullOrEmpty(commit1) || string.IsNullOrEmpty(commit2) || string.IsNullOrEmpty(filePath))
            {
                return new CallToolResponse
                {
                    IsError = true,
                    Content = new[] { new ToolContent { Type = "text", Text = "commit1, commit2, and filePath arguments are required" } }
                };
            }

            var workspaceRoot = _fileService.GetWorkspaceRoot();
            var lineDiff = await _gitService.GetFileLineDiffBetweenCommitsAsync(workspaceRoot, commit1, commit2, filePath);

            return new CallToolResponse
            {
                Content = new[] { new ToolContent { Type = "text", Text = JsonConvert.SerializeObject(lineDiff, _jsonSettings) } }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting file line diff between commits");
            return new CallToolResponse
            {
                IsError = true,
                Content = new[] { new ToolContent { Type = "text", Text = $"Error: {ex.Message}" } }
            };
        }
    }
}

// Helper classes for JSON-RPC
public class PromptGetRequest
{
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, string>? Arguments
    {
        get; set;
    }
}
