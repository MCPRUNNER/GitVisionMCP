| Commit | Author | Date | Message and Changes |
|---|---|---|---|

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

| `571c21c` | 7045kHz | 08/17/2025 14:45:44 | **Update Tools/GitServiceTools.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>**
<ul>

<li>Modified: Tools/GitServiceTools.cs</li>

</ul> |

| `3b03345` | 7045kHz | 08/17/2025 14:45:36 | **Update Tools/GitServiceTools.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>**
<ul>

<li>Modified: Tools/GitServiceTools.cs</li>

</ul> |

| `3b76c43` | 7045kHz | 08/17/2025 14:24:35 | **Updating Docker Documentation
**
<ul>

<li>Modified: Documentation/DOCKER.md</li>

</ul> |

| `ad10b21` | 7045kHz | 08/17/2025 14:19:38 | **Updating Docker Documentation
**
<ul>

<li>Modified: Documentation/ARCHITECTURE.md</li>

<li>Modified: Documentation/DOCKER.md</li>

<li>Modified: TODO.md</li>

</ul> |

| `1858469` | 7045kHz | 08/17/2025 13:57:18 | **Adding architecture prompt and updating release.prompt
**
<ul>

<li>Added: .github/prompts/document.architecture.prompt.md</li>

<li>Modified: Documentation/ARCHITECTURE.md</li>

<li>Deleted: MD/BRANCH_COMPARISON_remove_handler_vs_master_with_remote.md</li>

<li>Deleted: MD/COMMIT_REPORT.md</li>

<li>Deleted: MD/ReportFooter.md</li>

<li>Deleted: MD/ReportHeader.md</li>

<li>Modified: RELEASE_DOCUMENT.md</li>

<li>Modified: TODO.md</li>

</ul> |

| `2f51b07` | 7045kHz | 08/17/2025 10:19:06 | **Fixing gv_compare_branches*documentation tools
**
<ul>

<li>Modified: Documentation/index.md</li>

<li>Added: MD/BRANCH_COMPARISON_remove_handler_vs_master_with_remote.md</li>

<li>Modified: RELEASE_DOCUMENT.md</li>

<li>Modified: Repositories/GitRepository.cs</li>

</ul> |

| `5508d6c` | 7045kHz | 08/17/2025 09:52:39 | **Working through git comparisons
**
<ul>

<li>Modified: .github/prompts/document.release.prompt.md</li>

<li>Added: MD/COMMIT_REPORT.md</li>

</ul> |

| `c6ac322` | 7045kHz | 08/17/2025 09:31:29 | **Fixed Git Fetch issues by adding https git pat and git exe fallback
**
<ul>

<li>Modified: .github/prompts/document.release.prompt.md</li>

<li>Modified: .gitvision/exclude.json</li>

<li>Modified: Documentation/EXAMPLES.md</li>

<li>Added: Documentation/GIT_AUTH.md</li>

<li>Added: Documentation/RUN_PROCESS_EXAMPLES.md</li>

<li>Modified: README.md</li>

<li>Modified: Repositories/GitRepository.cs</li>

<li>Modified: Repositories/IGitRepository.cs</li>

<li>Modified: Repositories/IUtilityRepository.cs</li>

<li>Modified: Repositories/UtilityRepository.cs</li>

<li>Modified: Services/IUtilityService.cs</li>

<li>Modified: Services/UtilityService.cs</li>

</ul> |

| `095a09d` | 7045kHz | 08/16/2025 17:33:32 | **Removed Handler MCP and migrated to a common GitServiceTools for http/stdio
**
<ul>

<li>Modified: .github/prompts/document.release.prompt.md</li>

<li>Added: .gitvision/config.json</li>

<li>Modified: Configuration/ConfigLoader.cs</li>

<li>Added: Configuration/ConfigurationReloader.cs</li>

<li>Modified: Configuration/IConfigLoader.cs</li>

<li>Added: GitVisionMCP.Tests/Configuration/ConfigLoaderTests.cs</li>

<li>Added: GitVisionMCP.Tests/Configuration/ConfigurationReloaderTests.cs</li>

<li>Deleted: GitVisionMCP.Tests/Services/McpServerTests.cs</li>

<li>Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs</li>

<li>Modified: GitVisionMCP.csproj</li>

<li>Modified: GitVisionMCP.sln</li>

<li>Deleted: Handlers/IMcpHandler.cs</li>

<li>Deleted: Handlers/McpHandler.cs</li>

<li>Added: Models/GitVisionConfig.cs</li>

<li>Added: Models/IGitVisionConfig.cs</li>

<li>Added: Models/ReloadableGitVisionConfig.cs</li>

<li>Modified: Program.cs</li>

<li>Modified: RELEASE_DOCUMENT.md</li>

<li>Modified: Tools/GitServiceTools.cs</li>

</ul> |
