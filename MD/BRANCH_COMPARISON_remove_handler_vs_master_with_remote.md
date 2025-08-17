# Git Commit Documentation

Generated on: 2025-08-17 10:18:09
Total commits: 3

## Commit: 5508d6c3

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-17 09:52:39

**Message:**
```
Working through git comparisons

```

**Changed Files:**
- .github/prompts/document.release.prompt.md
- MD/COMMIT_REPORT.md

**Changes:**
- Modified: .github/prompts/document.release.prompt.md
- Added: MD/COMMIT_REPORT.md

---

## Commit: c6ac3229

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-17 09:31:29

**Message:**
```
Fixed Git Fetch issues by adding https git pat and git exe fallback

```

**Changed Files:**
- .github/prompts/document.release.prompt.md
- .gitvision/exclude.json
- Documentation/EXAMPLES.md
- Documentation/GIT_AUTH.md
- Documentation/RUN_PROCESS_EXAMPLES.md
- README.md
- Repositories/GitRepository.cs
- Repositories/IGitRepository.cs
- Repositories/IUtilityRepository.cs
- Repositories/UtilityRepository.cs
- Services/IUtilityService.cs
- Services/UtilityService.cs

**Changes:**
- Modified: .github/prompts/document.release.prompt.md
- Modified: .gitvision/exclude.json
- Modified: Documentation/EXAMPLES.md
- Added: Documentation/GIT_AUTH.md
- Added: Documentation/RUN_PROCESS_EXAMPLES.md
- Modified: README.md
- Modified: Repositories/GitRepository.cs
- Modified: Repositories/IGitRepository.cs
- Modified: Repositories/IUtilityRepository.cs
- Modified: Repositories/UtilityRepository.cs
- Modified: Services/IUtilityService.cs
- Modified: Services/UtilityService.cs

---

## Commit: 095a09d1

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-16 17:33:32

**Message:**
```
Removed Handler MCP and migrated to a common GitServiceTools for http/stdio

```

**Changed Files:**
- .github/prompts/document.release.prompt.md
- .gitvision/config.json
- Configuration/ConfigLoader.cs
- Configuration/ConfigurationReloader.cs
- Configuration/IConfigLoader.cs
- GitVisionMCP.Tests/Configuration/ConfigLoaderTests.cs
- GitVisionMCP.Tests/Configuration/ConfigurationReloaderTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- GitVisionMCP.sln
- Handlers/IMcpHandler.cs
- Handlers/McpHandler.cs
- Models/GitVisionConfig.cs
- Models/IGitVisionConfig.cs
- Models/ReloadableGitVisionConfig.cs
- Program.cs
- RELEASE_DOCUMENT.md
- Tools/GitServiceTools.cs

**Changes:**
- Modified: .github/prompts/document.release.prompt.md
- Added: .gitvision/config.json
- Modified: Configuration/ConfigLoader.cs
- Added: Configuration/ConfigurationReloader.cs
- Modified: Configuration/IConfigLoader.cs
- Added: GitVisionMCP.Tests/Configuration/ConfigLoaderTests.cs
- Added: GitVisionMCP.Tests/Configuration/ConfigurationReloaderTests.cs
- Deleted: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Modified: GitVisionMCP.sln
- Deleted: Handlers/IMcpHandler.cs
- Deleted: Handlers/McpHandler.cs
- Added: Models/GitVisionConfig.cs
- Added: Models/IGitVisionConfig.cs
- Added: Models/ReloadableGitVisionConfig.cs
- Modified: Program.cs
- Modified: RELEASE_DOCUMENT.md
- Modified: Tools/GitServiceTools.cs

---

