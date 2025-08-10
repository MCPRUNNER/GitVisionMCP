# GitVisionMCP (1.0.8) Release Documentation

## Version and Release Date

- **Application Name:** GitVisionMCP
- **Version:** 1.0.8
- **Release Date:** August 27, 2025
- **Current Branch:** master

---

## Summary of Changes

Renaming generate_git_documentation to generate_git_commit_report tool.

---

## New Features

---

## MCP Tools Available in This Release

| Tool Name                                  | Description                                                                   |
| ------------------------------------------ | ----------------------------------------------------------------------------- |
| fetch_from_remote                          | Fetch latest changes from remote repository                                   |
| generate_git_commit_report                 | Generate git commit report for current branch                                 |
| generate_git_commit_report_to_file         | Generate git commit report for current branch and write to a file             |
| compare_branches_documentation             | Generate documentation comparing differences between two branches             |
| compare_branches_with_remote_documentation | Generate documentation comparing differences between two branches with remote |
| compare_commits_documentation              | Generate documentation comparing differences between two commits              |
| get_recent_commits                         | Get recent commits from the current repository                                |
| get_local_branches                         | Get list of local branches in the repository                                  |
| get_remote_branches                        | Get list of remote branches in the repository                                 |
| get_all_branches                           | Get list of all branches (local and remote) in the repository                 |
| get_current_branch                         | Get the current active branch in the repository                               |
| get_changed_files_between_commits          | Get list of files changed between two commits                                 |
| get_commit_diff_info                       | Get comprehensive diff information between two commits                        |
| get_detailed_diff_between_commits          | Get detailed diff content between two commits                                 |
| search_commits_for_string                  | Search all commits for a specific string                                      |
| get_file_line_diff_between_commits         | Get line-by-line file diff between two commits                                |
| list_workspace_files                       | List all files in the workspace with optional filtering                       |
| list_workspace_files_with_cached_data      | List workspace files with optional filtering using cached data                |
| read_filtered_workspace_files              | Read contents of all files from filtered workspace results                    |
| git_find_merge_conflicts                   | Search for Git merge conflicts in source code                                 |
| search_json_file                           | Search for JSON values in a JSON file using JSONPath                          |
| search_xml_file                            | Search for XML values in an XML file using XPath                              |
| transform_xml_with_xslt                    | Transform an XML file using an XSLT stylesheet                                |
| search_yaml_file                           | Search for YAML values in a YAML file using JSONPath                          |
| search_csv_file                            | Search for CSV values in a CSV file using JSONPath                            |
| search_excel_file                          | Search for values in an Excel (.xlsx) file using JSONPath                     |
| deconstruct_to_file                        | Deconstruct a C# Service, Repository or Controller file and returns JSON      |
| deconstruct_to_json                        | Deconstruct a C# file and save structure to a JSON file                       |
| get_app_version                            | Extract application version from a project file                               |

---

## Architecture and Process Flow

[Note: The Documentation/ARCHITECTURE.md illustrates the architecture of the GitVisionMCP workspace, showing the relationships between various services, tools and models, along with their http vs stdio process flows.](Documentation/ARCHITECTURE.md)

---

No differences found between branches search_csv and master.
