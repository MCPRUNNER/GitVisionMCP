| Commit | Author | Date | Message and Changes |
|---|---|---|---|

| `796a5e1` | 7045kHz | 08/24/2025 18:25:36 | **Adding .github/prompts/document.commits.prompt.md as an example prompt for running gv_run_sbn_template
**
<ul>

<li>Added: .github/prompts/document.commits.prompt.md</li>

</ul> |

| `1c0f1ef` | 7045kHz | 08/24/2025 18:21:32 | **Adding Scriban/JINJA template processing, with example templates under .github/templates/
**
<ul>

<li>Added: .github/templates/commit.template.sbn</li>

<li>Added: .github/templates/commit2.template.sbn</li>

<li>Modified: .gitvision/config.json</li>

<li>Added: .temp/commit.md</li>

<li>Added: .temp/commit2.md</li>

<li>Added: .temp/extracted-data.json</li>

<li>Added: .temp/minimal-test.json</li>

<li>Modified: GitVisionMCP.csproj</li>

<li>Modified: Prompts/ReleaseDocumentPrompts.cs</li>

<li>Modified: Services/IWorkspaceService.cs</li>

<li>Modified: Services/WorkspaceService.cs</li>

<li>Modified: Tools/GitServiceTools.cs</li>

<li>Added: test-sbn-data.json</li>

</ul> |

| `5458fa4` | 7045kHz | 08/17/2025 14:47:01 | **Merge pull request #47 from MCPRUNNER/remove_handler

refactor tools, update documentation, add github PAT and backup git commands.**
<ul>

<li>Added: .github/prompts/document.architecture.prompt.md</li>

<li>Modified: .github/prompts/document.release.prompt.md</li>

<li>Added: .gitvision/config.json</li>

<li>Modified: .gitvision/exclude.json</li>

<li>Modified: Configuration/ConfigLoader.cs</li>

<li>Added: Configuration/ConfigurationReloader.cs</li>

<li>Modified: Configuration/IConfigLoader.cs</li>

<li>Modified: Documentation/ARCHITECTURE.md</li>

<li>Modified: Documentation/DOCKER.md</li>

<li>Modified: Documentation/EXAMPLES.md</li>

<li>Added: Documentation/GIT_AUTH.md</li>

<li>Added: Documentation/RUN_PROCESS_EXAMPLES.md</li>

<li>Modified: Documentation/index.md</li>

<li>Added: GitVisionMCP.Tests/Configuration/ConfigLoaderTests.cs</li>

<li>Added: GitVisionMCP.Tests/Configuration/ConfigurationReloaderTests.cs</li>

<li>Deleted: GitVisionMCP.Tests/Services/McpServerTests.cs</li>

<li>Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs</li>

<li>Modified: GitVisionMCP.csproj</li>

<li>Modified: GitVisionMCP.sln</li>

<li>Deleted: Handlers/IMcpHandler.cs</li>

<li>Deleted: Handlers/McpHandler.cs</li>

<li>Deleted: MD/ReportFooter.md</li>

<li>Deleted: MD/ReportHeader.md</li>

<li>Added: Models/GitVisionConfig.cs</li>

<li>Added: Models/IGitVisionConfig.cs</li>

<li>Added: Models/ReloadableGitVisionConfig.cs</li>

<li>Modified: Program.cs</li>

<li>Modified: README.md</li>

<li>Modified: RELEASE_DOCUMENT.md</li>

<li>Modified: Repositories/GitRepository.cs</li>

<li>Modified: Repositories/IGitRepository.cs</li>

<li>Modified: Repositories/IUtilityRepository.cs</li>

<li>Modified: Repositories/UtilityRepository.cs</li>

<li>Modified: Services/IUtilityService.cs</li>

<li>Modified: Services/UtilityService.cs</li>

<li>Modified: TODO.md</li>

<li>Modified: Tools/GitServiceTools.cs</li>

</ul> |
