# DEBUG TEMPLATE

## Test Data
Class: FileRepository
Total Actions: 12
  Method: DetermineWorkspaceRoot - Accessibility: public - Params: 0
  Method: GetWorkspaceRoot - Accessibility: public - Params: 0
  Method: GetFullPath - Accessibility: public - Params: 1
  Method: ReadFile - Accessibility: public - Params: 1
  Method: GetAllFiles - Accessibility: public - Params: 0
  Method: GetAllFilesAsync - Accessibility: public - Params: 0
  Method: GetAllFilesMatching - Accessibility: public - Params: 1
  Method: SaveAllFilesToXml - Accessibility: public - Params: 1
  Method: GetFileContentsAsync - Accessibility: public - Params: 1
  Method: LoadExcludeConfigurationAsync - Accessibility: public - Params: 0
  Method: IsPathMatchingPattern - Accessibility: public - Params: 2
  Method: IsFileExcludedAsync - Accessibility: public - Params: 1
Class: GitRepository
Total Actions: 19
  Method: ReadGitConflictMarkers - Accessibility: public - Params: 1
  Method: FindAllGitConflictMarkers - Accessibility: public - Params: 1
  Method: GetGitLogsAsync - Accessibility: public - Params: 2
  Method: GetGitLogsBetweenBranchesAsync - Accessibility: public - Params: 3
  Method: GetGitLogsBetweenCommitsAsync - Accessibility: public - Params: 3
  Method: GenerateCommitDocumentationAsync - Accessibility: public - Params: 2
  Method: WriteDocumentationToFileAsync - Accessibility: public - Params: 2
  Method: GetRecentCommitsAsync - Accessibility: public - Params: 2
  Method: GetChangedFilesBetweenCommitsAsync - Accessibility: public - Params: 3
  Method: GetDetailedDiffBetweenCommitsAsync - Accessibility: public - Params: 4
  Method: GetCommitDiffInfoAsync - Accessibility: public - Params: 3
  Method: GetFileLineDiffBetweenCommitsAsync - Accessibility: public - Params: 4
  Method: GetLocalBranchesAsync - Accessibility: public - Params: 1
  Method: GetRemoteBranchesAsync - Accessibility: public - Params: 1
  Method: GetAllBranchesAsync - Accessibility: public - Params: 1
  Method: GetCurrentBranchAsync - Accessibility: public - Params: 1
  Method: FetchFromRemoteAsync - Accessibility: public - Params: 2
  Method: GetGitLogsBetweenBranchesWithRemoteAsync - Accessibility: public - Params: 4
  Method: SearchCommitsForStringAsync - Accessibility: public - Params: 3
Class: UtilityRepository
Total Actions: 2
  Method: GetEnvironmentVariableValue - Accessibility: public - Params: 1
  Method: GetAppVersion - Accessibility: public - Params: 1
Class: DeconstructionService
Total Actions: 2
  Method: Deconstruct - Accessibility: public - Params: 1
  Method: DeconstructToFile - Accessibility: public - Params: 2
Class: FileService
Total Actions: 10
  Method: GetWorkspaceRoot - Accessibility: public - Params: 0
  Method: GetFullPath - Accessibility: public - Params: 1
  Method: ReadFile - Accessibility: public - Params: 1
  Method: GetAllFiles - Accessibility: public - Params: 0
  Method: GetAllFilesAsync - Accessibility: public - Params: 0
  Method: GetAllFilesMatching - Accessibility: public - Params: 1
  Method: SaveAllFilesToXml - Accessibility: public - Params: 1
  Method: GetFileContentsAsync - Accessibility: public - Params: 1
  Method: IsPathMatchingPattern - Accessibility: public - Params: 2
  Method: IsFileExcludedAsync - Accessibility: public - Params: 1
Class: GitService
Total Actions: 19
  Method: ReadGitConflictMarkers - Accessibility: public - Params: 1
  Method: FindAllGitConflictMarkers - Accessibility: public - Params: 1
  Method: GetGitLogsAsync - Accessibility: public - Params: 2
  Method: GetGitLogsBetweenBranchesAsync - Accessibility: public - Params: 3
  Method: GetGitLogsBetweenCommitsAsync - Accessibility: public - Params: 3
  Method: GenerateCommitDocumentationAsync - Accessibility: public - Params: 2
  Method: WriteDocumentationToFileAsync - Accessibility: public - Params: 2
  Method: GetRecentCommitsAsync - Accessibility: public - Params: 2
  Method: GetChangedFilesBetweenCommitsAsync - Accessibility: public - Params: 3
  Method: GetDetailedDiffBetweenCommitsAsync - Accessibility: public - Params: 4
  Method: GetCommitDiffInfoAsync - Accessibility: public - Params: 3
  Method: GetFileLineDiffBetweenCommitsAsync - Accessibility: public - Params: 4
  Method: GetLocalBranchesAsync - Accessibility: public - Params: 1
  Method: GetRemoteBranchesAsync - Accessibility: public - Params: 1
  Method: GetAllBranchesAsync - Accessibility: public - Params: 1
  Method: GetCurrentBranchAsync - Accessibility: public - Params: 1
  Method: FetchFromRemoteAsync - Accessibility: public - Params: 2
  Method: GetGitLogsBetweenBranchesWithRemoteAsync - Accessibility: public - Params: 4
  Method: SearchCommitsForStringAsync - Accessibility: public - Params: 3
Class: UtilityService
Total Actions: 2
  Method: GetEnvironmentVariableValue - Accessibility: public - Params: 1
  Method: GetAppVersion - Accessibility: public - Params: 1
Class: WorkspaceService
Total Actions: 10
  Method: GetGitHubPromptFileContent - Accessibility: public - Params: 1
  Method: SearchJsonFile - Accessibility: public - Params: 4
  Method: SearchCsvFile - Accessibility: public - Params: 4
  Method: SearchXmlFile - Accessibility: public - Params: 4
  Method: TransformXmlWithXslt - Accessibility: public - Params: 3
  Method: SearchYamlFile - Accessibility: public - Params: 4
  Method: GenerateAutoDocumentationTempJson - Accessibility: public - Params: 5
  Method: GenerateAutoDocumentationTempJsonWithTemplate - Accessibility: public - Params: 5
  Method: SearchExcelFile - Accessibility: public - Params: 2
  Method: ProcessScribanFromJsonStringAsync - Accessibility: public - Params: 3
Class: GitServiceTools
Total Actions: 31
  Method: gv_fetch_from_remote - Accessibility: public - Params: 1
  Method: gv_generate_git_commit_report - Accessibility: public - Params: 2
  Method: gv_generate_git_commit_report_to_file - Accessibility: public - Params: 3
  Method:  - Accessibility: public - Params: 6
  Method: gv_compare_branches_documentation - Accessibility: public - Params: 4
  Method: gv_compare_branches_with_remote_documentation - Accessibility: public - Params: 5
  Method: gv_compare_commits_documentation - Accessibility: public - Params: 4
  Method: gv_get_recent_commits - Accessibility: public - Params: 1
  Method: gv_get_local_branches - Accessibility: public - Params: 0
  Method: gv_get_remote_branches - Accessibility: public - Params: 0
  Method: gv_get_all_branches - Accessibility: public - Params: 0
  Method: gv_get_current_branch - Accessibility: public - Params: 0
  Method: gv_get_changed_files_between_commits - Accessibility: public - Params: 2
  Method: gv_get_commit_diff_info - Accessibility: public - Params: 2
  Method: gv_get_detailed_diff_between_commits - Accessibility: public - Params: 3
  Method: gv_search_commits_for_string - Accessibility: public - Params: 2
  Method: gv_get_file_line_diff_between_commits - Accessibility: public - Params: 3
  Method: gv_list_workspace_files - Accessibility: public - Params: 5
  Method: gv_list_workspace_files_with_cached_data - Accessibility: public - Params: 6
  Method: gv_git_find_merge_conflicts - Accessibility: public - Params: 0
  Method: gv_search_json_file - Accessibility: public - Params: 4
  Method: gv_search_xml_file - Accessibility: public - Params: 4
  Method: gv_transform_xml_with_xslt - Accessibility: public - Params: 3
  Method: gv_search_csv_file - Accessibility: public - Params: 4
  Method: gv_search_excel_file - Accessibility: public - Params: 2
  Method: gv_run_sbn_template - Accessibility: public - Params: 3
  Method: gv_search_yaml_file - Accessibility: public - Params: 4
  Method: gv_deconstruct_to_file - Accessibility: public - Params: 1
  Method: gv_deconstruct_to_json - Accessibility: public - Params: 2
  Method: gv_get_app_version - Accessibility: public - Params: 1
  Method: gv_generate_autodoc - Accessibility: public - Params: 4

## Filtered Methods Test
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
