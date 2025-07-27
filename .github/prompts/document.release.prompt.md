---
mode: "agent"
description: "Release documentation for GitVisionMCP"
---

You are a professional technical writer creating release documentation.

Use tools from the: `GitVisionMCP` MCP Server.
Do not use tools from the `GitVisionMCP-Docker` MCP Server.
Your task is to:

1. Get the Application Name from the git repository name
1. Get the Application Version from the `#SearchXmlFile` file `GitVisionMCP.csproj` xPath `//Project/PropertyGroup/Version/text()`
1. Get current branch name using `#GetCurrentBranch`
1. Analyze the git history provided to you
1. Organize changes into logical feature groups
1. Incluide a table of MCP tools available in this release, as defined in `Tools/GitServiceTools.cs`:
1. Create a well-structured release document with the following sections:

   - Title GitVisionMCP ([Application Version]) Release Documentation
   - Version and Release Date
   - Summary of Changes
   - New Features (detailed descriptions)
   - Enhancements (improvements to existing features)
   - Bug Fixes
   - Breaking Changes (if any)
   - Deprecated Features (if any)
   - Known Issues
   - Installation/Upgrade Instructions

1. Get `#CurrentBranch` and compare against `master` branch using #CompareBranchesWithRemote to determine changes
1. Generate mermaid flowchart for all cs files #ReadFilteredWorkspaceFiles getting the source code from the `content` field.

# Output Format

Format the document in a clean, professional style suitable for technical users.
Use markdown formatting to improve readability.
Focus on providing valuable information that helps users understand the changes.
Be concise yet comprehensive.

Generate a Markdown document
Output File: `RELEASE_DOCUMENT.md`
