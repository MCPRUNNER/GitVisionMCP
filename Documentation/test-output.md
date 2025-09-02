# TEST TEMPLATE
This is a test - if you see this, the template system is working!

Data count: 9
FileRepository has 12 actions
GitRepository has 19 actions
UtilityRepository has 2 actions
DeconstructionService has 2 actions
FileService has 10 actions
GitService has 19 actions
UtilityService has 2 actions
WorkspaceService has 10 actions
GitServiceTools has 31 actions

FILTERS WORKING TEST:
Class: FileRepository
Methods with params:
  - GetFullPath (1 params)
  - ReadFile (1 params)
  - GetAllFilesMatching (1 params)
  - SaveAllFilesToXml (1 params)
  - GetFileContentsAsync (1 params)
  - IsPathMatchingPattern (2 params)
  - IsFileExcludedAsync (1 params)
Class: GitRepository
Methods with params:
  - ReadGitConflictMarkers (1 params)
  - FindAllGitConflictMarkers (1 params)
  - GetGitLogsAsync (2 params)
  - GetGitLogsBetweenBranchesAsync (3 params)
  - GetGitLogsBetweenCommitsAsync (3 params)
  - GenerateCommitDocumentationAsync (2 params)
  - WriteDocumentationToFileAsync (2 params)
  - GetRecentCommitsAsync (2 params)
  - GetChangedFilesBetweenCommitsAsync (3 params)
  - GetDetailedDiffBetweenCommitsAsync (4 params)
  - GetCommitDiffInfoAsync (3 params)
  - GetFileLineDiffBetweenCommitsAsync (4 params)
  - GetLocalBranchesAsync (1 params)
  - GetRemoteBranchesAsync (1 params)
  - GetAllBranchesAsync (1 params)
  - GetCurrentBranchAsync (1 params)
  - FetchFromRemoteAsync (2 params)
  - GetGitLogsBetweenBranchesWithRemoteAsync (4 params)
  - SearchCommitsForStringAsync (3 params)
Class: UtilityRepository
Methods with params:
  - GetEnvironmentVariableValue (1 params)
  - GetAppVersion (1 params)
Class: DeconstructionService
Methods with params:
  - Deconstruct (1 params)
  - DeconstructToFile (2 params)
Class: FileService
Methods with params:
  - GetFullPath (1 params)
  - ReadFile (1 params)
  - GetAllFilesMatching (1 params)
  - SaveAllFilesToXml (1 params)
  - GetFileContentsAsync (1 params)
  - IsPathMatchingPattern (2 params)
  - IsFileExcludedAsync (1 params)
Class: GitService
Methods with params:
  - ReadGitConflictMarkers (1 params)
  - FindAllGitConflictMarkers (1 params)
  - GetGitLogsAsync (2 params)
  - GetGitLogsBetweenBranchesAsync (3 params)
  - GetGitLogsBetweenCommitsAsync (3 params)
  - GenerateCommitDocumentationAsync (2 params)
  - WriteDocumentationToFileAsync (2 params)
  - GetRecentCommitsAsync (2 params)
  - GetChangedFilesBetweenCommitsAsync (3 params)
  - GetDetailedDiffBetweenCommitsAsync (4 params)
  - GetCommitDiffInfoAsync (3 params)
  - GetFileLineDiffBetweenCommitsAsync (4 params)
  - GetLocalBranchesAsync (1 params)
  - GetRemoteBranchesAsync (1 params)
  - GetAllBranchesAsync (1 params)
  - GetCurrentBranchAsync (1 params)
  - FetchFromRemoteAsync (2 params)
  - GetGitLogsBetweenBranchesWithRemoteAsync (4 params)
  - SearchCommitsForStringAsync (3 params)
Class: UtilityService
Methods with params:
  - GetEnvironmentVariableValue (1 params)
  - GetAppVersion (1 params)
Class: WorkspaceService
Methods with params:
  - GetGitHubPromptFileContent (1 params)
  - SearchJsonFile (4 params)
  - SearchCsvFile (4 params)
  - SearchXmlFile (4 params)
  - TransformXmlWithXslt (3 params)
  - SearchYamlFile (4 params)
  - GenerateAutoDocumentationTempJson (5 params)
  - GenerateAutoDocumentationTempJsonWithTemplate (5 params)
  - SearchExcelFile (2 params)
  - ProcessScribanFromJsonStringAsync (3 params)
Class: GitServiceTools
Methods with params:
  - gv_fetch_from_remote (1 params)
  - gv_generate_git_commit_report (2 params)
  - gv_generate_git_commit_report_to_file (3 params)
  -  (6 params)
  - gv_compare_branches_documentation (4 params)
  - gv_compare_branches_with_remote_documentation (5 params)
  - gv_compare_commits_documentation (4 params)
  - gv_get_recent_commits (1 params)
  - gv_get_changed_files_between_commits (2 params)
  - gv_get_commit_diff_info (2 params)
  - gv_get_detailed_diff_between_commits (3 params)
  - gv_search_commits_for_string (2 params)
  - gv_get_file_line_diff_between_commits (3 params)
  - gv_list_workspace_files (5 params)
  - gv_list_workspace_files_with_cached_data (6 params)
  - gv_search_json_file (4 params)
  - gv_search_xml_file (4 params)
  - gv_transform_xml_with_xslt (3 params)
  - gv_search_csv_file (4 params)
  - gv_search_excel_file (2 params)
  - gv_run_sbn_template (3 params)
  - gv_search_yaml_file (4 params)
  - gv_deconstruct_to_file (1 params)
  - gv_deconstruct_to_json (2 params)
  - gv_get_app_version (1 params)
  - gv_generate_autodoc (4 params)
