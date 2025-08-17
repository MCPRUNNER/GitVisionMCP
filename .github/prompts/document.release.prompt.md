---
mode: "agent"
description: "Release documentation for GitVisionMCP"
---

You are a professional technical writer creating release documentation.

Use tools from the: `GitVisionMCP` MCP Server.

1. Collect the following information:
   1. Get the [Application Name] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Project.Name`
   1. Get Application [csproj file] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Project.RelativePath`
   1. Get [commit count] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Settings.maxCommits`
   1. Get [search file count] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Settings.maxFiles`
   1. Get [release branch] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Git.Release`
   1. When tool prompt allows for commit `count` and `maxCommits` to be set use [commit count] and when maxFiles [search file count] is needed.
   1. Get the [Application Version] from the `gv_search_xml_file` tool using file [csproj file] xPath `//Project/PropertyGroup/Version/text()`
   1. Get [current branch name] using `gv_get_current_branch`
   1. Update list of branches using `gv_get_all_branches`
   1. Get [commits report] using `gv_generate_git_commit_report`
1. Analyze and compare the difference between [current branch] and [release branch] using `gv_compare_branches_with_remote_documentation`
1. Organize changes into logical feature groups
1. Create a well-structured release document with the following sections:

   - Title of [Application Name] and ([Application Version])
   - Subtitle [current branch name] and [release branch]
   - Current date
   - Summary of Changes using [commits report]
   - New Features (detailed descriptions)
   - Enhancements (improvements to existing features)
   - Bug Fixes
   - Breaking Changes (if any)
   - Create a table of available tools with description
   - Deprecated Features (if any)
   - Known Issues
   - Installation/Upgrade Instructions

1. Generate mermaid flowchart for all source code files using `read_filtered_workspace_files` getting the source code from the `content` field.

# Output Format

Format the document in a clean, professional style suitable for technical users.
Use markdown formatting to improve readability.
Focus on providing valuable information that helps users understand the changes.
Be concise yet comprehensive.

Generate a Markdown document
Output File: `RELEASE_DOCUMENT.md`
