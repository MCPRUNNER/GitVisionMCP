---
mode: "agent"
description: "Generate an up-to-date ARCHITECTURE.md for the GitVisionMCP project"
---

You are a technical writing agent that will produce an accurate, well-structured `Documentation/ARCHITECTURE.md` for the GitVisionMCP repository.

Your goal: read the code and repository metadata, extract the current runtime and architectural facts, and write a single, human-friendly Markdown architecture document that matches the project's style and contains mermaid diagrams and concise explanations.

Primary deliverable

- Output file: `Documentation/ARCHITECTURE.md`
- Temp file directory: `.temp`
- Format: Markdown with mermaid diagrams and clear sections (Overview, Architectural Relationship, Component Responsibilities, Transport Flows, Tool Discovery, Configuration, Repository Pattern, Key Differences, Redundancy Analysis, Best Practices, Conclusion)

Inputs and tools you MAY use (use whichever are available in the workspace):

- Get the [Application Name] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Project.Name`
- Get Application [csproj file] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Project.RelativePath`
- Get [Release Branch] from `gv_search_json_file` `.gitvision\config.json` `jsonPath` `$.Git.Release`
- Get [Current Branch] from `gv_get_current_branch`
- Get the [Application Version] from the `gv_search_xml_file` tool using file [csproj file] xPath `//Project/PropertyGroup/Version/text()`
- Read `Program.cs` and other top-level files to determine how the MCP server is configured (stdio vs http, IMcpServer, app.MapMcp, etc.)
- Read `GitServiceTools.cs` (or `Tools/GitServiceTools.*`) to list tool capabilities and attributes
- Read services and repository implementations under `Services/` and `Repositories/` to describe responsibilities
- Use any repository file-reading tool available to gather source snippets for inclusion in the diagrams or examples

Contract (what you're producing)

- Inputs: repository source files (Program.cs, GitServiceTools, Services, Repositories)
- Output: `Documentation/ARCHITECTURE.md` (complete, human-readable, mermaid diagrams)

Checklist (follow these steps)

1. Read `Program.cs` and confirm how GITVISION_MCP_TRANSPORT is used and how the app starts for both STDIO and HTTP.
1. Attempt to find `GitServiceTools` class and extract its public method names and any MCP-related attributes.
1. List service interfaces and repository types (IGitService, IFileService, IGitRepository, etc.) and summarize responsibilities.
1. Build mermaid diagrams that show the MCP framework -> GitServiceTools -> Services -> Repositories flows and the STDIO vs HTTP transport paths.
1. Produce the `Documentation/ARCHITECTURE.md` file with the following sections:
   - Title:
     - include [Application Name] and [Application Version]
   - Subtitle:
     - include [Release Branch] and [Current Branch]
   - Overview
   - Architectural Relationship (mermaid)
   - Component Responsibilities (GitServiceTools, MCP Framework, Services, Repositories)
   - Transport-Specific Behavior (STDIO and HTTP sequences)
   - Tool Discovery and Registration
   - Configuration Logic (summary reflecting Program.cs)
   - Repository Pattern Architecture and benefits
   - Key Differences Summary table (tools vs framework/transport)
   - Redundancy Analysis
   - Best Practices Observed
   - Conclusion
1. If any files were unreadable or missing, include a "Notes / Missing Files" subsection listing what couldn't be read.

Style and constraints

- Keep the document factual and concise.
- Use mermaid diagrams for architecture and sequence flows.
- If there are any URL reference in the mermaid diagrams,then escape any `:` with a `\:`"
- When describing code behavior, quote exact lines only if you read the source; otherwise paraphrase.
- If the repository contains multiple `GitServiceTools`-like classes, summarize them together.
- Do not add references to internal or proprietary systems outside the repository.

Failure handling

- If you cannot read `Program.cs`, create the document using best-effort assumptions and include a clearly marked "Assumptions" section listing what you assumed.

Output

- Write the final Markdown to `Documentation/ARCHITECTURE.md` and ensure it overwrites existing content.
- If any decision was made (for example: "McpHandler removed"), include the rationale and a pointer to the code lines or files that justify the statement.

Example short prompt summary to follow when generating the file:
"Generate `Documentation/ARCHITECTURE.md` by reading `Program.cs`, `Tools/GitServiceTools.*`, Services and Repositories. Document that the MCP framework handles STDIO and HTTP transports. If there is a URL reference in the mermaid diagrams,then escape any `:` with a `\:`"
