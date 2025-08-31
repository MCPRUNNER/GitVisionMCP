# Autodoc Example Overview

## GitVisionMCP Documentation
*This tool helps manage multiple GitHub repositories and Source Documentation with ease.*
GitVisionMCP is a multi-repo GitHub project management tool.

## Repositories and Services
| Name | Architecture Model | Description | Path |
|------------|-------------------|-----------|----------------|
| `FileRepository` | Repository | Handles file system operations |   Repositories/FileRepository.cs |
| `GitRepository` | Repository | Handles Git operations |   Repositories/GitRepository.cs |
| `UtilityRepository` | Repository | Handles utility functions |   Repositories/UtilityRepository.cs |
| `DeconstructionService` | Service | Handles code deconstruction |   Services/DeconstructionService.cs |
| `FileService` | Service | Handles file operations |   Services/FileService.cs |
| `GitService` | Service | Handles Git operations |   Services/GitService.cs |
| `UtilityService` | Service | Handles utility functions |   Services/UtilityService.cs |
| `WorkspaceService` | Service | Handles workspace operations |   Services/WorkspaceService.cs |
| `GitServiceTools` | Tools | Handles Tool Definitions for Git Services |   Tools/GitServiceTools.cs |

## Components and Public Methods with Arguments

| Class Name | Architecture Model | Namespace | Public Methods with Arguments |
|------------|-------------------|-----------|----------------|
| `FileRepository` | Repository | GitVisionMCP.Repositories |GetFullPath : string?;ReadFile : string?;GetAllFilesMatching : Task<List<WorkspaceFileInfo>>;SaveAllFilesToXml : bool;GetFileContentsAsync : Task<List<FileContentInfo>>;IsPathMatchingPattern : bool;IsFileExcludedAsync : Task<bool>; |
| `GitRepository` | Repository | GitVisionMCP.Services |ReadGitConflictMarkers : Task<ConflictResult>;FindAllGitConflictMarkers : Task<List<ConflictResult>>;GetGitLogsAsync : Task<List<GitCommitInfo>>;GetGitLogsBetweenBranchesAsync : Task<List<GitCommitInfo>>;GetGitLogsBetweenCommitsAsync : Task<List<GitCommitInfo>>;GenerateCommitDocumentationAsync : Task<string>;WriteDocumentationToFileAsync : Task<bool>;GetRecentCommitsAsync : Task<List<GitCommitInfo>>;GetChangedFilesBetweenCommitsAsync : Task<List<string>>;GetDetailedDiffBetweenCommitsAsync : Task<string>;GetCommitDiffInfoAsync : Task<GitCommitDiffInfo>;GetFileLineDiffBetweenCommitsAsync : Task<FileLineDiffInfo>;GetLocalBranchesAsync : Task<List<string>>;GetRemoteBranchesAsync : Task<List<string>>;GetAllBranchesAsync : Task<List<string>>;GetCurrentBranchAsync : Task<string>;FetchFromRemoteAsync : Task<bool>;GetGitLogsBetweenBranchesWithRemoteAsync : Task<List<GitCommitInfo>>;SearchCommitsForStringAsync : Task<CommitSearchResponse>; |
| `UtilityRepository` | Repository | GitVisionMCP.Repositories |GetEnvironmentVariableValue : object?;GetAppVersion : string?; |
| `DeconstructionService` | Service | GitVisionMCP.Services |Deconstruct : string?;DeconstructToFile : string?; |
| `FileService` | Service | GitVisionMCP.Services |GetFullPath : string?;ReadFile : string?;GetAllFilesMatching : Task<List<WorkspaceFileInfo>>;SaveAllFilesToXml : bool;GetFileContentsAsync : Task<List<FileContentInfo>>;IsPathMatchingPattern : bool;IsFileExcludedAsync : Task<bool>; |
| `GitService` | Service | GitVisionMCP.Services |ReadGitConflictMarkers : Task<ConflictResult>;FindAllGitConflictMarkers : Task<List<ConflictResult>>;GetGitLogsAsync : Task<List<GitCommitInfo>>;GetGitLogsBetweenBranchesAsync : Task<List<GitCommitInfo>>;GetGitLogsBetweenCommitsAsync : Task<List<GitCommitInfo>>;GenerateCommitDocumentationAsync : Task<string>;WriteDocumentationToFileAsync : Task<bool>;GetRecentCommitsAsync : Task<List<GitCommitInfo>>;GetChangedFilesBetweenCommitsAsync : Task<List<string>>;GetDetailedDiffBetweenCommitsAsync : Task<string>;GetCommitDiffInfoAsync : Task<GitCommitDiffInfo>;GetFileLineDiffBetweenCommitsAsync : Task<FileLineDiffInfo>;GetLocalBranchesAsync : Task<List<string>>;GetRemoteBranchesAsync : Task<List<string>>;GetAllBranchesAsync : Task<List<string>>;GetCurrentBranchAsync : Task<string>;FetchFromRemoteAsync : Task<bool>;GetGitLogsBetweenBranchesWithRemoteAsync : Task<List<GitCommitInfo>>;SearchCommitsForStringAsync : Task<CommitSearchResponse>; |
| `UtilityService` | Service | GitVisionMCP.Services |GetEnvironmentVariableValue : object?;GetAppVersion : string?; |
| `WorkspaceService` | Service | GitVisionMCP.Services |GetGitHubPromptFileContent : string?;SearchJsonFile : string?;SearchCsvFile : string?;SearchXmlFile : string?;TransformXmlWithXslt : string?;SearchYamlFile : string?;GenerateAutoDocumentationTempJson : List<string>?;GenerateAutoDocumentationTempJsonWithTemplate : List<string>?;SearchExcelFile : string?;ProcessScribanFromJsonStringAsync : Task<string?>; |
| `GitServiceTools` | MCP Tool | GitVisionMCP.Tools |gv_fetch_from_remote : Task<bool>;gv_generate_git_commit_report : Task<string>;gv_generate_git_commit_report_to_file : Task<string>; : Task<string>;gv_compare_branches_documentation : Task<string>;gv_compare_branches_with_remote_documentation : Task<string>;gv_compare_commits_documentation : Task<string>;gv_get_recent_commits : Task<List<GitCommitInfo>>;gv_get_changed_files_between_commits : Task<List<string>>;gv_get_commit_diff_info : Task<GitCommitDiffInfo>;gv_get_detailed_diff_between_commits : Task<string>;gv_search_commits_for_string : Task<CommitSearchResponse>;gv_get_file_line_diff_between_commits : Task<FileLineDiffInfo>;gv_list_workspace_files : Task<List<WorkspaceFileInfo>>;gv_list_workspace_files_with_cached_data : Task<List<WorkspaceFileInfo>>;gv_search_json_file : Task<string?>;gv_search_xml_file : Task<string?>;gv_transform_xml_with_xslt : Task<string?>;gv_search_csv_file : Task<string?>;gv_search_excel_file : Task<string?>;gv_run_sbn_template : Task<string?>;gv_search_yaml_file : Task<string?>;gv_deconstruct_to_file : Task<string?>;gv_deconstruct_to_json : Task<string?>;gv_get_app_version : Task<string?>;gv_generate_autodoc : Task<List<string>?>; |
