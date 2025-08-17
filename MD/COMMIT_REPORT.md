# Git Commit Documentation

Generated on: 2025-08-17 09:37:39
Total commits: 169

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

## Commit: a8add976

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-15 17:33:13

**Message:**
```
Merge pull request #46 from MCPRUNNER/normalized_pathing

Adding Unix/Windows file and path normalization
```

**Changed Files:**
- GitVisionMCP.Tests/Repositories/FileRepositoryTests.cs
- Repositories/FileRepository.cs

**Changes:**
- Added: GitVisionMCP.Tests/Repositories/FileRepositoryTests.cs
- Modified: Repositories/FileRepository.cs

---

## Commit: 54320646

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-15 17:20:22

**Message:**
```
Adding Unix/Windows file and path normalization

```

**Changed Files:**
- GitVisionMCP.Tests/Repositories/FileRepositoryTests.cs
- Repositories/FileRepository.cs

**Changes:**
- Added: GitVisionMCP.Tests/Repositories/FileRepositoryTests.cs
- Modified: Repositories/FileRepository.cs

---

## Commit: 893c0408

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-10 16:17:59

**Message:**
```
Merge pull request #45 from MCPRUNNER/api_util_services

Api util services
```

**Changed Files:**
- GitVisionMCP.csproj
- Handlers/McpHandler.cs
- README.md
- Repositories/FileRepository.cs
- Repositories/IFileRepository.cs
- Services/FileService.cs
- Services/IFileService.cs

**Changes:**
- Modified: GitVisionMCP.csproj
- Modified: Handlers/McpHandler.cs
- Modified: README.md
- Modified: Repositories/FileRepository.cs
- Modified: Repositories/IFileRepository.cs
- Modified: Services/FileService.cs
- Modified: Services/IFileService.cs

---

## Commit: 15327b17

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-10 16:15:58

**Message:**
```
Fixing async GetAllFilesMatching issue

```

**Changed Files:**
- Handlers/McpHandler.cs
- Repositories/FileRepository.cs
- Repositories/IFileRepository.cs
- Services/FileService.cs
- Services/IFileService.cs

**Changes:**
- Modified: Handlers/McpHandler.cs
- Modified: Repositories/FileRepository.cs
- Modified: Repositories/IFileRepository.cs
- Modified: Services/FileService.cs
- Modified: Services/IFileService.cs

---

## Commit: d6d68a5a

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-10 16:08:49

**Message:**
```
Merge branch 'master' into api_util_services

```

**Changed Files:**
- Handlers/McpHandler.cs
- Repositories/FileRepository.cs

**Changes:**
- Modified: Handlers/McpHandler.cs
- Modified: Repositories/FileRepository.cs

---

## Commit: 64939cf6

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-10 16:08:05

**Message:**
```
Readme and version update

```

**Changed Files:**
- GitVisionMCP.csproj
- README.md

**Changes:**
- Modified: GitVisionMCP.csproj
- Modified: README.md

---

## Commit: 825cb1a6

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-10 16:07:19

**Message:**
```
Merge pull request #44 from MCPRUNNER/api_util_services

Api util services
```

**Changed Files:**
- .gitvision/apiconnect.json
- .gitvision/exclude.json
- Configuration/ConfigLoader.cs
- Configuration/IConfigLoader.cs
- Documentation/API_SUPPORT.md
- Documentation/ControllerDocumentation.md
- Documentation/Example_mssqlMCP_Dataflows.md
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.csproj
- Handlers/McpHandler.cs
- Models/ApiAuthentication.cs
- Models/ApiConfig.cs
- Models/ApiConnection.cs
- Program.cs
- README.md
- RELEASE_DOCUMENT.md
- RELEASE_NOTES.md
- Repositories/ApiRepository.cs
- Repositories/FileRepository.cs
- Repositories/GitRepository.cs
- Repositories/UtilityRepository.cs
- Services/UtilityService.cs
- Services/WorkspaceService.cs
- TODO.md
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-data.json

**Changes:**
- Added: .gitvision/apiconnect.json
- Modified: .gitvision/exclude.json
- Added: Configuration/ConfigLoader.cs
- Added: Configuration/IConfigLoader.cs
- Added: Documentation/API_SUPPORT.md
- Deleted: Documentation/ControllerDocumentation.md
- Deleted: Documentation/Example_mssqlMCP_Dataflows.md
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.csproj
- Modified: Handlers/McpHandler.cs
- Added: Models/ApiAuthentication.cs
- Added: Models/ApiConfig.cs
- Added: Models/ApiConnection.cs
- Modified: Program.cs
- Modified: README.md
- Modified: RELEASE_DOCUMENT.md
- Deleted: RELEASE_NOTES.md
- Added: Repositories/ApiRepository.cs
- Modified: Repositories/FileRepository.cs
- Modified: Repositories/GitRepository.cs
- Modified: Repositories/UtilityRepository.cs
- Modified: Services/UtilityService.cs
- Modified: Services/WorkspaceService.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Deleted: test-data.json

---

## Commit: 46696a3d

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-10 16:07:04

**Message:**
```
Update Repositories/FileRepository.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Repositories/FileRepository.cs

**Changes:**
- Modified: Repositories/FileRepository.cs

---

## Commit: b0d5b767

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-10 16:06:41

**Message:**
```
Update Handlers/McpHandler.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Handlers/McpHandler.cs

**Changes:**
- Modified: Handlers/McpHandler.cs

---

## Commit: 0548239a

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-10 16:06:35

**Message:**
```
Update Handlers/McpHandler.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Handlers/McpHandler.cs

**Changes:**
- Modified: Handlers/McpHandler.cs

---

## Commit: fae05732

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-10 15:58:39

**Message:**
```
Updating documentaion for README.md

```

**Changed Files:**
- .gitvision/apiconnect.json
- Documentation/API_SUPPORT.md
- README.md

**Changes:**
- Modified: .gitvision/apiconnect.json
- Modified: Documentation/API_SUPPORT.md
- Modified: README.md

---

## Commit: 9f0164c8

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-10 15:18:10

**Message:**
```
Preping IConfigLoader and ApiRepository, refactored McpHandler to use NewtonSoft

```

**Changed Files:**
- .gitvision/apiconnect.json
- Configuration/ApiConnectionLoader.cs
- Configuration/ConfigLoader.cs
- Configuration/IConfigLoader.cs
- Handlers/McpHandler.cs
- Models/ApiAuthentication.cs
- Models/ApiConfig.cs
- Models/ApiConnection.cs
- Program.cs
- Repositories/ApiRepository.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: .gitvision/apiconnect.json
- Deleted: Configuration/ApiConnectionLoader.cs
- Added: Configuration/ConfigLoader.cs
- Added: Configuration/IConfigLoader.cs
- Modified: Handlers/McpHandler.cs
- Modified: Models/ApiAuthentication.cs
- Modified: Models/ApiConfig.cs
- Modified: Models/ApiConnection.cs
- Modified: Program.cs
- Modified: Repositories/ApiRepository.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: d9e321b8

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-10 11:24:42

**Message:**
```
Util refactor and Begin of Api work

```

**Changed Files:**
- .gitvision/apiconnect.json
- Configuration/ApiConnectionLoader.cs
- Documentation/API_SUPPORT.md
- Models/ApiAuthentication.cs
- Models/ApiConfig.cs
- Models/ApiConnection.cs
- Repositories/ApiRepository.cs
- Repositories/UtilityRepository.cs

**Changes:**
- Added: .gitvision/apiconnect.json
- Added: Configuration/ApiConnectionLoader.cs
- Added: Documentation/API_SUPPORT.md
- Added: Models/ApiAuthentication.cs
- Added: Models/ApiConfig.cs
- Added: Models/ApiConnection.cs
- Added: Repositories/ApiRepository.cs
- Modified: Repositories/UtilityRepository.cs

---

## Commit: c7b090e9

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-09 14:53:54

**Message:**
```
Cleanup and fix of Exclude file issue

```

**Changed Files:**
- .gitvision/exclude.json
- TODO.md
- test-data.json

**Changes:**
- Modified: .gitvision/exclude.json
- Modified: TODO.md
- Deleted: test-data.json

---

## Commit: 3a405c57

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-09 14:33:29

**Message:**
```
Working IsFileExcluded issue

```

**Changed Files:**
- GitVisionMCP.csproj
- README.md
- RELEASE_DOCUMENT.md
- Repositories/FileRepository.cs

**Changes:**
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: RELEASE_DOCUMENT.md
- Modified: Repositories/FileRepository.cs

---

## Commit: ca13a0b4

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-09 14:12:24

**Message:**
```
Renaming generate_git_documentation to generate_git_commit_report tool

```

**Changed Files:**
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Handlers/McpHandler.cs
- Repositories/GitRepository.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: Handlers/McpHandler.cs
- Modified: Repositories/GitRepository.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: 052e3e3a

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-09 13:45:57

**Message:**
```
Pre update of generate_git_documentation

```

**Changed Files:**
- Documentation/ControllerDocumentation.md
- Documentation/Example_mssqlMCP_Dataflows.md
- RELEASE_NOTES.md
- Repositories/GitRepository.cs
- Repositories/UtilityRepository.cs
- Services/UtilityService.cs
- Services/WorkspaceService.cs

**Changes:**
- Deleted: Documentation/ControllerDocumentation.md
- Deleted: Documentation/Example_mssqlMCP_Dataflows.md
- Deleted: RELEASE_NOTES.md
- Modified: Repositories/GitRepository.cs
- Modified: Repositories/UtilityRepository.cs
- Modified: Services/UtilityService.cs
- Modified: Services/WorkspaceService.cs

---

## Commit: a770b952

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-07 18:12:54

**Message:**
```
Merge pull request #43 from MCPRUNNER/document_update.v3

Document update.v3
```

**Changed Files:**
- .github/prompts/ReleaseNotesPrompt.md
- .github/prompts/document.ansible.prompt.md
- .github/prompts/document.controller.prompt.md
- .github/prompts/document.csharp.prompt.md
- .github/prompts/document.release.prompt.md
- Documentation/ARCHITECTURE.md
- Documentation/ControllerDocumentation.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Handlers/McpHandler.cs
- Program.cs
- Repositories/GitRepository.cs
- Repositories/IGitRepository.cs
- Repositories/IUtilityRepository.cs
- Repositories/UtilityRepository.cs
- Services/DeconstructionService.cs
- Services/GitService.cs
- Services/IUtilityService.cs
- Services/IWorkspaceService.cs
- Services/UtilityService.cs
- Services/WorkspaceService.cs
- Tools/GitServiceTools.cs

**Changes:**
- Deleted: .github/prompts/ReleaseNotesPrompt.md
- Modified: .github/prompts/document.ansible.prompt.md
- Modified: .github/prompts/document.controller.prompt.md
- Modified: .github/prompts/document.csharp.prompt.md
- Modified: .github/prompts/document.release.prompt.md
- Modified: Documentation/ARCHITECTURE.md
- Modified: Documentation/ControllerDocumentation.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Handlers/McpHandler.cs
- Modified: Program.cs
- Renamed: Repositories/GitRepository.cs
- Renamed: Repositories/IGitRepository.cs
- Added: Repositories/IUtilityRepository.cs
- Added: Repositories/UtilityRepository.cs
- Modified: Services/DeconstructionService.cs
- Modified: Services/GitService.cs
- Added: Services/IUtilityService.cs
- Renamed: Services/IWorkspaceService.cs
- Added: Services/UtilityService.cs
- Renamed: Services/WorkspaceService.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 1973e872

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-07 18:10:34

**Message:**
```
.ghthub/prompt improvements

```

**Changed Files:**
- .github/prompts/ReleaseNotesPrompt.md
- .github/prompts/document.csharp.prompt.md
- .github/prompts/document.release.prompt.md
- Documentation/ControllerDocumentation.md
- Repositories/UtilityRepository.cs

**Changes:**
- Deleted: .github/prompts/ReleaseNotesPrompt.md
- Modified: .github/prompts/document.csharp.prompt.md
- Modified: .github/prompts/document.release.prompt.md
- Modified: Documentation/ControllerDocumentation.md
- Modified: Repositories/UtilityRepository.cs

---

## Commit: 8ec90bef

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-07 16:48:55

**Message:**
```
Architecute docs updated

```

**Changed Files:**
- .github/prompts/document.ansible.prompt.md
- .github/prompts/document.controller.prompt.md
- .github/prompts/document.release.prompt.md
- Documentation/ARCHITECTURE.md

**Changes:**
- Modified: .github/prompts/document.ansible.prompt.md
- Modified: .github/prompts/document.controller.prompt.md
- Modified: .github/prompts/document.release.prompt.md
- Modified: Documentation/ARCHITECTURE.md

---

## Commit: 1f0d4815

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-06 10:43:09

**Message:**
```
Refactoring UtilityService and updating arch doc

```

**Changed Files:**
- Documentation/ARCHITECTURE.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Handlers/McpHandler.cs
- Program.cs
- Repositories/GitRepository.cs
- Repositories/IGitRepository.cs
- Repositories/IUtilityRepository.cs
- Repositories/UtilityRepository.cs
- Services/DeconstructionService.cs
- Services/GitService.cs
- Services/IUtilityService.cs
- Services/IWorkspaceService.cs
- Services/UtilityService.cs
- Services/WorkspaceService.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Documentation/ARCHITECTURE.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Handlers/McpHandler.cs
- Modified: Program.cs
- Renamed: Repositories/GitRepository.cs
- Renamed: Repositories/IGitRepository.cs
- Added: Repositories/IUtilityRepository.cs
- Added: Repositories/UtilityRepository.cs
- Modified: Services/DeconstructionService.cs
- Modified: Services/GitService.cs
- Added: Services/IUtilityService.cs
- Renamed: Services/IWorkspaceService.cs
- Added: Services/UtilityService.cs
- Renamed: Services/WorkspaceService.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 78ef19a8

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-05 19:00:37

**Message:**
```
Merge pull request #42 from MCPRUNNER/service_seperation

Service seperation
```

**Changed Files:**
- Documentation/ARCHITECTURE.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- Program.cs
- Repositories/GitCommandRepository.cs
- Repositories/IGitCommandRepository.cs
- Services/GitService.cs

**Changes:**
- Modified: Documentation/ARCHITECTURE.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Modified: Program.cs
- Added: Repositories/GitCommandRepository.cs
- Added: Repositories/IGitCommandRepository.cs
- Modified: Services/GitService.cs

---

## Commit: 3bb680e9

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-05 18:43:47

**Message:**
```
Refactoring GitCommandRepository and tests

```

**Changed Files:**
- Documentation/ARCHITECTURE.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- Program.cs
- Repositories/GitCommandRepository.cs
- Repositories/IGitCommandRepository.cs
- Services/GitService.cs

**Changes:**
- Modified: Documentation/ARCHITECTURE.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Modified: Program.cs
- Added: Repositories/GitCommandRepository.cs
- Added: Repositories/IGitCommandRepository.cs
- Modified: Services/GitService.cs

---

## Commit: ae10dfbf

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-05 16:52:05

**Message:**
```
Improved ARCHITECTURE.md graph

```

**Changed Files:**
- Documentation/ARCHITECTURE.md

**Changes:**
- Modified: Documentation/ARCHITECTURE.md

---

## Commit: 0cf0a859

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-04 19:30:39

**Message:**
```
Merge pull request #41 from MCPRUNNER/service_seperation

Service seperation
```

**Changed Files:**
- Documentation/ARCHITECTURE.md
- Documentation/index.md
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Handlers/McpHandler.cs
- Program.cs
- Repositories/FileRepository.cs
- Repositories/IFileRepository.cs
- Services/DeconstructionService.cs
- Services/FileService.cs
- Services/IFileService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Tools/GitServiceTools.cs

**Changes:**
- Added: Documentation/ARCHITECTURE.md
- Modified: Documentation/index.md
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Handlers/McpHandler.cs
- Modified: Program.cs
- Added: Repositories/FileRepository.cs
- Added: Repositories/IFileRepository.cs
- Modified: Services/DeconstructionService.cs
- Added: Services/FileService.cs
- Added: Services/IFileService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 28b0e46a

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-04 19:29:34

**Message:**
```
Updating and adding ARCHITECTURE.md documentaiton

```

**Changed Files:**
- Documentation/ARCHITECTURE.md
- Documentation/index.md

**Changes:**
- Added: Documentation/ARCHITECTURE.md
- Modified: Documentation/index.md

---

## Commit: aa68a774

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-04 19:22:28

**Message:**
```
Adding FileRepository

```

**Changed Files:**
- Program.cs
- Repositories/FileRepository.cs
- Repositories/IFileRepository.cs
- Services/FileService.cs

**Changes:**
- Modified: Program.cs
- Added: Repositories/FileRepository.cs
- Added: Repositories/IFileRepository.cs
- Modified: Services/FileService.cs

---

## Commit: 0ff1ab6c

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-04 18:00:13

**Message:**
```
LocationService seperation into LocationService and FileService, tests updated

```

**Changed Files:**
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Handlers/McpHandler.cs
- Program.cs
- Services/DeconstructionService.cs
- Services/FileService.cs
- Services/IFileService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Handlers/McpHandler.cs
- Modified: Program.cs
- Modified: Services/DeconstructionService.cs
- Added: Services/FileService.cs
- Added: Services/IFileService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 9e07532e

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-03 17:52:15

**Message:**
```
Merge pull request #40 from MCPRUNNER/cleanup_text_json

Cleanup text json
```

**Changed Files:**
- .gitvision/exclude.json
- Services/GitService.cs
- Services/LocationService.cs
- TODO.md

**Changes:**
- Modified: .gitvision/exclude.json
- Modified: Services/GitService.cs
- Modified: Services/LocationService.cs
- Modified: TODO.md

---

## Commit: d5ab2998

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-03 15:54:57

**Message:**
```
Working Excluded files issue

```

**Changed Files:**
- .gitvision/exclude.json
- Services/LocationService.cs
- TODO.md

**Changes:**
- Modified: .gitvision/exclude.json
- Modified: Services/LocationService.cs
- Modified: TODO.md

---

## Commit: cc7e5d71

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-03 14:39:00

**Message:**
```
fixed ReadGitConflictMarkers()

```

**Changed Files:**
- Services/GitService.cs

**Changes:**
- Modified: Services/GitService.cs

---

## Commit: 6748d2ce

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-03 14:36:03

**Message:**
```
Merge pull request #39 from MCPRUNNER/core_refactoring

Core refactoring
```

**Changed Files:**
- GitVisionMCP.Tests/Models/McpModelsTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- Handlers/IMcpHandler.cs
- Handlers/McpHandler.cs
- Models/CommitSearchResponse.cs
- Models/CommitSearchResult.cs
- Models/ConflictResult.cs
- Models/DocumentationRequest.cs
- Models/FileContentInfo.cs
- Models/FileLineDiffInfo.cs
- Models/FileSearchMatch.cs
- Models/GitCommitDiffInfo.cs
- Models/GitCommitInfo.cs
- Models/GitFileChange.cs
- Models/LineDiff.cs
- Models/LineSearchMatch.cs
- Models/McpModels.cs
- Program.cs
- README.md
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Models/McpModelsTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Renamed: Handlers/IMcpHandler.cs
- Renamed: Handlers/McpHandler.cs
- Added: Models/CommitSearchResponse.cs
- Added: Models/CommitSearchResult.cs
- Added: Models/ConflictResult.cs
- Added: Models/DocumentationRequest.cs
- Added: Models/FileContentInfo.cs
- Added: Models/FileLineDiffInfo.cs
- Added: Models/FileSearchMatch.cs
- Added: Models/GitCommitDiffInfo.cs
- Added: Models/GitCommitInfo.cs
- Added: Models/GitFileChange.cs
- Added: Models/LineDiff.cs
- Added: Models/LineSearchMatch.cs
- Modified: Models/McpModels.cs
- Modified: Program.cs
- Modified: README.md
- Modified: Services/GitService.cs
- Modified: Services/IGitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs

---

## Commit: 47ea16b8

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-03 14:35:42

**Message:**
```
Update Services/GitService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/GitService.cs

**Changes:**
- Modified: Services/GitService.cs

---

## Commit: a4c1b61d

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-03 14:35:23

**Message:**
```
Update Services/GitService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/GitService.cs

**Changes:**
- Modified: Services/GitService.cs

---

## Commit: 07b325d9

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-03 14:34:42

**Message:**
```
Update Services/GitService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/GitService.cs

**Changes:**
- Modified: Services/GitService.cs

---

## Commit: 4e81d7f2

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-03 14:34:26

**Message:**
```
Update Tools/GitServiceTools.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Tools/GitServiceTools.cs

---

## Commit: 0cae3a1e

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-03 14:31:34

**Message:**
```
Updating README.md and TODO.md

```

**Changed Files:**
- README.md
- TODO.md

**Changes:**
- Modified: README.md
- Modified: TODO.md

---

## Commit: df56b443

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-03 14:27:39

**Message:**
```
Fixed broken tests, and migrated McpServer to McpHandler

```

**Changed Files:**
- GitVisionMCP.Tests/Models/McpModelsTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- Handlers/IMcpHandler.cs
- Handlers/McpHandler.cs
- Program.cs
- Services/GitService.cs
- Services/LocationService.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Models/McpModelsTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Renamed: Handlers/IMcpHandler.cs
- Renamed: Handlers/McpHandler.cs
- Modified: Program.cs
- Modified: Services/GitService.cs
- Modified: Services/LocationService.cs

---

## Commit: 571aaee7

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-03 14:01:58

**Message:**
```
Added tool for discovering un-resolved merge conflict markers in files

```

**Changed Files:**
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- TODO.md
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Services/GitService.cs
- Modified: Services/IGitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs

---

## Commit: b63b85e8

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-03 09:27:03

**Message:**
```
Model extraction into Models directory, migration of System Json to Newton in McpServer.cs

```

**Changed Files:**
- Models/CommitSearchResponse.cs
- Models/CommitSearchResult.cs
- Models/ConflictResult.cs
- Models/DocumentationRequest.cs
- Models/FileContentInfo.cs
- Models/FileLineDiffInfo.cs
- Models/FileSearchMatch.cs
- Models/GitCommitDiffInfo.cs
- Models/GitCommitInfo.cs
- Models/GitFileChange.cs
- Models/LineDiff.cs
- Models/LineSearchMatch.cs
- Models/McpModels.cs
- Services/GitService.cs
- Services/McpServer.cs

**Changes:**
- Added: Models/CommitSearchResponse.cs
- Added: Models/CommitSearchResult.cs
- Added: Models/ConflictResult.cs
- Added: Models/DocumentationRequest.cs
- Added: Models/FileContentInfo.cs
- Added: Models/FileLineDiffInfo.cs
- Added: Models/FileSearchMatch.cs
- Added: Models/GitCommitDiffInfo.cs
- Added: Models/GitCommitInfo.cs
- Added: Models/GitFileChange.cs
- Added: Models/LineDiff.cs
- Added: Models/LineSearchMatch.cs
- Modified: Models/McpModels.cs
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs

---

## Commit: 0e4fa948

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-02 16:54:14

**Message:**
```
Merge pull request #38 from MCPRUNNER/doc_update

Updating Documentation and Tool list
```

**Changed Files:**
- .github/copilot-instructions.md
- Documentation/DOCKER.md
- Documentation/PROJECT_STATUS.md
- Documentation/SEARCH_OVERVIEW.md
- Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Documentation/VS_CODE_INTEGRATION_FIXES.md
- Documentation/XML_SEARCH_IMPLEMENTATION.md
- Documentation/index.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- README.md
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-transform.xslt

**Changes:**
- Modified: .github/copilot-instructions.md
- Modified: Documentation/DOCKER.md
- Modified: Documentation/PROJECT_STATUS.md
- Added: Documentation/SEARCH_OVERVIEW.md
- Deleted: Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Deleted: Documentation/VS_CODE_INTEGRATION_FIXES.md
- Deleted: Documentation/XML_SEARCH_IMPLEMENTATION.md
- Modified: Documentation/index.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: Services/GitService.cs
- Modified: Services/IGitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test-transform.xslt

---

## Commit: e82c2ba4

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-02 16:48:00

**Message:**
```
Refactor of tool names to snake_case, adding missing tools from tool list, updating documentation with better examples, refactoring

```

**Changed Files:**
- TRANSFORM_ENHANCEMENT_SUMMARY.md

**Changes:**
- Deleted: TRANSFORM_ENHANCEMENT_SUMMARY.md

---

## Commit: 0c0579d3

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-02 16:47:10

**Message:**
```
Refactor of tool names to snake_case, adding missing tools from tool list, updating documentation with better examples, refactoring

```

**Changed Files:**
- .github/copilot-instructions.md
- Documentation/SEARCH_OVERVIEW.md
- Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Documentation/XML_SEARCH_IMPLEMENTATION.md
- Documentation/index.md
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- TRANSFORM_ENHANCEMENT_SUMMARY.md
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-transform.xslt

**Changes:**
- Modified: .github/copilot-instructions.md
- Added: Documentation/SEARCH_OVERVIEW.md
- Deleted: Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Deleted: Documentation/XML_SEARCH_IMPLEMENTATION.md
- Modified: Documentation/index.md
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Added: TRANSFORM_ENHANCEMENT_SUMMARY.md
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test-transform.xslt

---

## Commit: ecc86c33

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-02 08:41:32

**Message:**
```
Added GetAppVersion Tool

```

**Changed Files:**
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: aaa4bc74

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-02 08:24:16

**Message:**
```
Pre GetAppVersion

```

**Changed Files:**
- Documentation/DOCKER.md
- Documentation/PROJECT_STATUS.md
- Documentation/VS_CODE_INTEGRATION_FIXES.md
- Documentation/index.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.csproj
- README.md
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Documentation/DOCKER.md
- Modified: Documentation/PROJECT_STATUS.md
- Deleted: Documentation/VS_CODE_INTEGRATION_FIXES.md
- Modified: Documentation/index.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: Services/GitService.cs
- Modified: Services/IGitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 746c3911

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-08-01 19:03:32

**Message:**
```
Merge pull request #37 from MCPRUNNER/location_refactor

Location refactor
```

**Changed Files:**
- .vscode/launch.json
- GitVisionMCP.csproj
- Models/WorkspaceFileInfo.cs
- Program.cs
- Properties/launchSettings.json
- RELEASE_NOTES.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test.xlsx

**Changes:**
- Deleted: .vscode/launch.json
- Modified: GitVisionMCP.csproj
- Added: Models/WorkspaceFileInfo.cs
- Modified: Program.cs
- Modified: Properties/launchSettings.json
- Added: RELEASE_NOTES.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test.xlsx

---

## Commit: 659eadb2

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-01 18:59:24

**Message:**
```
Improved SearchExcelFile features

```

**Changed Files:**
- Services/McpServer.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: c70f3eb3

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-08-01 09:00:37

**Message:**
```
Resolved issues with SearchExcelFile

```

**Changed Files:**
- .vscode/launch.json
- Program.cs
- Properties/launchSettings.json
- Services/ILocationService.cs
- Services/LocationService.cs
- test.xlsx

**Changes:**
- Deleted: .vscode/launch.json
- Modified: Program.cs
- Modified: Properties/launchSettings.json
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Added: test.xlsx

---

## Commit: 10cb6b83

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-31 07:02:34

**Message:**
```
Added Tool definition for SearchExcelFile added

```

**Changed Files:**
- Services/ILocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: 3a874418

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-31 06:46:20

**Message:**
```
Method for SearchExcelFile added

```

**Changed Files:**
- GitVisionMCP.csproj
- Services/LocationService.cs

**Changes:**
- Modified: GitVisionMCP.csproj
- Modified: Services/LocationService.cs

---

## Commit: fef2a6d6

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-29 18:21:04

**Message:**
```
Refactoring LocationService

```

**Changed Files:**
- Models/WorkspaceFileInfo.cs
- RELEASE_NOTES.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Added: Models/WorkspaceFileInfo.cs
- Added: RELEASE_NOTES.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: e6fb7767

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-27 14:35:25

**Message:**
```
Merge pull request #36 from MCPRUNNER/search_csv

Search csv
```

**Changed Files:**
- .github/prompts/document.release.prompt.md
- CHANGES.md
- Documentation/EXAMPLES.md
- Documentation/index.md
- GitVisionMCP.csproj
- README.md
- RELEASE_DOCUMENT.md
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test.csv

**Changes:**
- Modified: .github/prompts/document.release.prompt.md
- Renamed: CHANGES.md
- Modified: Documentation/EXAMPLES.md
- Added: Documentation/index.md
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: RELEASE_DOCUMENT.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test.csv

---

## Commit: 48836e0e

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-27 14:34:19

**Message:**
```
Pre-merge documentation update

```

**Changed Files:**
- CHANGES.md
- Documentation/EXAMPLES.md
- Documentation/index.md
- RELEASE_DOCUMENT.md
- TODO.md

**Changes:**
- Renamed: CHANGES.md
- Modified: Documentation/EXAMPLES.md
- Added: Documentation/index.md
- Modified: RELEASE_DOCUMENT.md
- Modified: TODO.md

---

## Commit: 5ec2df40

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-27 14:20:13

**Message:**
```
Adding SearchCsvFile tool

```

**Changed Files:**
- .github/prompts/document.release.prompt.md
- GitVisionMCP.csproj
- README.md
- RELEASE_DOCUMENT.md
- RELEASE_NOTES.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test.csv

**Changes:**
- Modified: .github/prompts/document.release.prompt.md
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: RELEASE_DOCUMENT.md
- Modified: RELEASE_NOTES.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test.csv

---

## Commit: b5e6f1d0

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-27 08:51:16

**Message:**
```
Merge pull request #35 from MCPRUNNER/deconstructor

Removing reference to Sampling tool until it's ready
```

**Changed Files:**
- RELEASE_NOTES.md
- Tools/GitServiceTools.cs

**Changes:**
- Modified: RELEASE_NOTES.md
- Modified: Tools/GitServiceTools.cs

---

## Commit: 23546f42

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-27 08:49:46

**Message:**
```
Removing reference to Sampling tool until it's ready

```

**Changed Files:**
- RELEASE_NOTES.md
- Tools/GitServiceTools.cs

**Changes:**
- Modified: RELEASE_NOTES.md
- Modified: Tools/GitServiceTools.cs

---

## Commit: 072f7489

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-26 17:05:47

**Message:**
```
Merge pull request #34 from MCPRUNNER/deconstructor

Deconstructor
```

**Changed Files:**
- .github/prompts/document.controller.prompt.md
- .github/prompts/document.csharp.prompt.md
- .github/prompts/document.release.prompt.md
- .gitvision/exclude.json
- Documentation/ControllerArchitecture.mmd
- Documentation/DECONSTRUCTION_SERVICE.md
- Documentation/EXAMPLES.md
- Documentation/RELEASE_NOTES_FINAL.md
- GitVisionMCP.csproj
- Models/DeconstructionActionParameterModel.cs
- Models/DeconstructorActionModel.cs
- Models/DeconstructorModel.cs
- Models/DeconstructorPropertyModel.cs
- Program.cs
- RELEASE_DOCUMENT.md
- RELEASE_NOTES.md
- Services/DeconstructionService.cs
- Services/IDeconstructionService.cs
- Services/McpServer.cs
- TODO.md
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Modified: .github/prompts/document.controller.prompt.md
- Modified: .github/prompts/document.csharp.prompt.md
- Renamed: .github/prompts/document.release.prompt.md
- Modified: .gitvision/exclude.json
- Deleted: Documentation/ControllerArchitecture.mmd
- Modified: Documentation/DECONSTRUCTION_SERVICE.md
- Modified: Documentation/EXAMPLES.md
- Modified: Documentation/RELEASE_NOTES_FINAL.md
- Modified: GitVisionMCP.csproj
- Added: Models/DeconstructionActionParameterModel.cs
- Added: Models/DeconstructorActionModel.cs
- Added: Models/DeconstructorModel.cs
- Added: Models/DeconstructorPropertyModel.cs
- Modified: Program.cs
- Modified: RELEASE_DOCUMENT.md
- Modified: RELEASE_NOTES.md
- Modified: Services/DeconstructionService.cs
- Modified: Services/IDeconstructionService.cs
- Modified: Services/McpServer.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: c066e903

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-26 17:02:19

**Message:**
```
Release Prompt [document.release.prompt] and RELEASE_DOCUMENT.md updated

```

**Changed Files:**
- RELEASE_DOCUMENT.md

**Changes:**
- Modified: RELEASE_DOCUMENT.md

---

## Commit: 3af3b488

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-26 16:21:39

**Message:**
```
Completed Deconstructor, updated prompts to use version number in csproj

```

**Changed Files:**
- .github/prompts/document.controller.prompt.md
- .github/prompts/document.csharp.prompt.md
- .github/prompts/document.release.prompt.md
- .gitvision/exclude.json
- Documentation/ControllerArchitecture.mmd
- Documentation/DECONSTRUCTION_SERVICE.md
- Documentation/EXAMPLES.md
- Documentation/RELEASE_NOTES_FINAL.md
- GitVisionMCP.csproj
- Program.cs
- RELEASE_DOCUMENT.md
- RELEASE_NOTES.md
- Services/McpServer.cs
- TODO.md

**Changes:**
- Modified: .github/prompts/document.controller.prompt.md
- Modified: .github/prompts/document.csharp.prompt.md
- Renamed: .github/prompts/document.release.prompt.md
- Modified: .gitvision/exclude.json
- Deleted: Documentation/ControllerArchitecture.mmd
- Modified: Documentation/DECONSTRUCTION_SERVICE.md
- Modified: Documentation/EXAMPLES.md
- Modified: Documentation/RELEASE_NOTES_FINAL.md
- Modified: GitVisionMCP.csproj
- Modified: Program.cs
- Modified: RELEASE_DOCUMENT.md
- Modified: RELEASE_NOTES.md
- Modified: Services/McpServer.cs
- Modified: TODO.md

---

## Commit: ef817e4b

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-26 15:03:25

**Message:**
```
Adding ArchitectureModel and refactoring deconstruction

```

**Changed Files:**
- Models/DeconstructorModel.cs
- Services/DeconstructionService.cs

**Changes:**
- Modified: Models/DeconstructorModel.cs
- Modified: Services/DeconstructionService.cs

---

## Commit: 7c4dc720

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-26 14:41:11

**Message:**
```
Move from Analysis of controller to Deconstructor

```

**Changed Files:**
- Models/DeconstructionActionParameterModel.cs
- Models/DeconstructorActionModel.cs
- Models/DeconstructorModel.cs
- Models/DeconstructorPropertyModel.cs
- Services/DeconstructionService.cs
- Services/IDeconstructionService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Added: Models/DeconstructionActionParameterModel.cs
- Added: Models/DeconstructorActionModel.cs
- Added: Models/DeconstructorModel.cs
- Added: Models/DeconstructorPropertyModel.cs
- Modified: Services/DeconstructionService.cs
- Modified: Services/IDeconstructionService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: 0336a7c9

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-24 17:03:05

**Message:**
```
Merge pull request #33 from MCPRUNNER/file_exists_issue

Updating GitServiceTools and GitServiceToolsTests with FullPath fix
```

**Changed Files:**
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Tools/GitServiceTools.Exclude.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Deleted: Tools/GitServiceTools.Exclude.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: f07f10c6

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-24 17:02:23

**Message:**
```
Updating GitServiceTools and GitServiceToolsTests with FullPath fix

```

**Changed Files:**
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Tools/GitServiceTools.Exclude.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Deleted: Tools/GitServiceTools.Exclude.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 1ea6d097

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-23 18:59:57

**Message:**
```
Merge pull request #32 from MCPRUNNER/file_exists_issue

Adding locationService.GetFullPath to resolve issues with relative pa…
```

**Changed Files:**
- Services/ILocationService.cs
- Services/LocationService.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs

---

## Commit: 7507c8b8

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-23 18:58:56

**Message:**
```
Adding locationService.GetFullPath to resolve issues with relative path sourcing

```

**Changed Files:**
- Services/ILocationService.cs
- Services/LocationService.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs

---

## Commit: 46064e82

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-23 08:09:00

**Message:**
```
Merge pull request #31 from MCPRUNNER/xlst_tool

Resolving file.exists issue in exclude configuration
```

**Changed Files:**
- Services/LocationService.cs
- Services/McpServer.cs

**Changes:**
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs

---

## Commit: a3b386d5

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-23 08:07:58

**Message:**
```
Resolving file.exists issue in exclude configuration

```

**Changed Files:**
- Services/LocationService.cs
- Services/McpServer.cs

**Changes:**
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs

---

## Commit: 26b405d2

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 20:23:50

**Message:**
```
Merge pull request #30 from MCPRUNNER/xlst_tool

Xlst tool
```

**Changed Files:**
- Documentation/ControllerArchitecture.mmd
- README.md

**Changes:**
- Added: Documentation/ControllerArchitecture.mmd
- Modified: README.md

---

## Commit: f7de4632

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-22 20:21:32

**Message:**
```
Updating documentation

```

**Changed Files:**
- Documentation/ControllerArchitecture.mmd

**Changes:**
- Added: Documentation/ControllerArchitecture.mmd

---

## Commit: 37fb15e8

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-22 20:19:29

**Message:**
```
Updating documentation

```

**Changed Files:**
- README.md

**Changes:**
- Modified: README.md

---

## Commit: b4b268c6

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 20:09:21

**Message:**
```
Merge pull request #29 from MCPRUNNER/xlst_tool

Adding XSLT Processing of XML file
```

**Changed Files:**
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.cs
- simple-transform.xslt
- test-data.xml
- transform-to-html.xslt

**Changes:**
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs
- Added: simple-transform.xslt
- Added: test-data.xml
- Added: transform-to-html.xslt

---

## Commit: 45f31ab8

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-22 20:07:05

**Message:**
```
Adding XSLT Processing of XML file

```

**Changed Files:**
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.cs
- simple-transform.xslt
- test-data.xml
- transform-to-html.xslt

**Changes:**
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs
- Added: simple-transform.xslt
- Added: test-data.xml
- Added: transform-to-html.xslt

---

## Commit: bdcf92ea

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 19:45:05

**Message:**
```
Merge pull request #28 from MCPRUNNER/yaml_search

Yaml search
```

**Changed Files:**
- Documentation/EXAMPLES.md
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.cs
- test-config.yaml

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs
- Added: test-config.yaml

---

## Commit: e4c7ae32

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-22 19:38:29

**Message:**
```
Merge branch 'master' into yaml_search

```

**Changed Files:**
- Services/LocationService.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Services/LocationService.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 7df40aca

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-22 19:37:55

**Message:**
```
Adding YAML processing

```

**Changed Files:**
- Documentation/EXAMPLES.md
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.cs
- test-config.yaml

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: TODO.md
- Modified: Tools/GitServiceTools.cs
- Added: test-config.yaml

---

## Commit: 45998a75

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 19:16:43

**Message:**
```
Merge pull request #27 from MCPRUNNER/cleanup

Adding exclusion lists to filesearches, and updating tests
```

**Changed Files:**
- .gitvision/exclude.json
- Documentation/ControllerArchitecture.mmd
- Documentation/EXAMPLES.md
- Documentation/EXCLUDE_FUNCTIONALITY.md
- Documentation/PROJECT_STATUS.md
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Models/ExcludeConfiguration.cs
- README.md
- RELEASE_NOTES.md
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.Exclude.cs
- Tools/GitServiceTools.cs
- asp_controllers.template.xml

**Changes:**
- Added: .gitvision/exclude.json
- Deleted: Documentation/ControllerArchitecture.mmd
- Modified: Documentation/EXAMPLES.md
- Added: Documentation/EXCLUDE_FUNCTIONALITY.md
- Modified: Documentation/PROJECT_STATUS.md
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Added: Models/ExcludeConfiguration.cs
- Modified: README.md
- Modified: RELEASE_NOTES.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Added: TODO.md
- Added: Tools/GitServiceTools.Exclude.cs
- Modified: Tools/GitServiceTools.cs
- Deleted: asp_controllers.template.xml

---

## Commit: 16a78c75

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 19:16:27

**Message:**
```
Update Services/LocationService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/LocationService.cs

**Changes:**
- Modified: Services/LocationService.cs

---

## Commit: 61d0c842

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 19:16:16

**Message:**
```
Update Services/LocationService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/LocationService.cs

**Changes:**
- Modified: Services/LocationService.cs

---

## Commit: f2f785df

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 19:16:06

**Message:**
```
Update Services/LocationService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/LocationService.cs

**Changes:**
- Modified: Services/LocationService.cs

---

## Commit: b71940ee

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 19:15:45

**Message:**
```
Update Tools/GitServiceTools.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Tools/GitServiceTools.cs

---

## Commit: 5eec80e3

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-22 19:12:24

**Message:**
```
Adding exclusion lists to filesearches, and updating tests

```

**Changed Files:**
- .gitvision/exclude.json
- Documentation/ControllerArchitecture.mmd
- Documentation/EXAMPLES.md
- Documentation/EXCLUDE_FUNCTIONALITY.md
- Documentation/PROJECT_STATUS.md
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Models/ExcludeConfiguration.cs
- README.md
- RELEASE_NOTES.md
- Services/ILocationService.cs
- Services/LocationService.cs
- TODO.md
- Tools/GitServiceTools.Exclude.cs
- Tools/GitServiceTools.cs
- asp_controllers.template.xml

**Changes:**
- Added: .gitvision/exclude.json
- Deleted: Documentation/ControllerArchitecture.mmd
- Modified: Documentation/EXAMPLES.md
- Added: Documentation/EXCLUDE_FUNCTIONALITY.md
- Modified: Documentation/PROJECT_STATUS.md
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Added: Models/ExcludeConfiguration.cs
- Modified: README.md
- Modified: RELEASE_NOTES.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Added: TODO.md
- Added: Tools/GitServiceTools.Exclude.cs
- Modified: Tools/GitServiceTools.cs
- Deleted: asp_controllers.template.xml

---

## Commit: a9f24cf0

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-22 14:16:18

**Message:**
```
Merge pull request #26 from MCPRUNNER/cleanup

Cleanup
```

**Changed Files:**
- .github/prompts/document.controller.prompt.md
- .gitvision/prompt_templates/asp_controllers.template.xml
- Documentation/ControllerArchitecture.mmd
- Documentation/ControllerDocumentation.md
- Documentation/Example_mssqlMCP_Dataflows.md
- Documentation/SETUP.md
- Documentation/VS_CODE_INTEGRATION_FIXES.md
- SampleController.cs
- TestApp/Program.cs
- TestApp/TestApp.csproj
- TestUsersController.cs
- asp_controllers.template.xml
- test-attribute-search.cs
- test-data.json
- test-deconstruction.cs
- test_xml_search.cs

**Changes:**
- Added: .github/prompts/document.controller.prompt.md
- Added: .gitvision/prompt_templates/asp_controllers.template.xml
- Added: Documentation/ControllerArchitecture.mmd
- Added: Documentation/ControllerDocumentation.md
- Renamed: Documentation/Example_mssqlMCP_Dataflows.md
- Modified: Documentation/SETUP.md
- Modified: Documentation/VS_CODE_INTEGRATION_FIXES.md
- Deleted: SampleController.cs
- Deleted: TestApp/Program.cs
- Deleted: TestApp/TestApp.csproj
- Deleted: TestUsersController.cs
- Added: asp_controllers.template.xml
- Deleted: test-attribute-search.cs
- Modified: test-data.json
- Deleted: test-deconstruction.cs
- Deleted: test_xml_search.cs

---

## Commit: d9f1acbd

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-22 14:14:20

**Message:**
```
Testing controller prompt

```

**Changed Files:**
- .github/prompts/document.controller.prompt.md
- .gitvision/prompt_templates/asp_controllers.template.xml
- Documentation/ControllerArchitecture.mmd
- Documentation/ControllerDocumentation.md
- Documentation/VS_CODE_INTEGRATION_FIXES.md
- asp_controllers.template.xml
- test-data.json

**Changes:**
- Added: .github/prompts/document.controller.prompt.md
- Added: .gitvision/prompt_templates/asp_controllers.template.xml
- Added: Documentation/ControllerArchitecture.mmd
- Added: Documentation/ControllerDocumentation.md
- Added: Documentation/VS_CODE_INTEGRATION_FIXES.md
- Added: asp_controllers.template.xml
- Added: test-data.json

---

## Commit: 1d885261

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 17:06:58

**Message:**
```
Updating SETUP.md and cleanup unused files

```

**Changed Files:**
- Documentation/Example_mssqlMCP_Dataflows.md
- Documentation/SETUP.md
- Documentation/VS_CODE_INTEGRATION_FIXES.md
- SampleController.cs
- TestApp/Program.cs
- TestApp/TestApp.csproj
- TestUsersController.cs
- test-attribute-search.cs
- test-data.json
- test-deconstruction.cs
- test_xml_search.cs

**Changes:**
- Renamed: Documentation/Example_mssqlMCP_Dataflows.md
- Modified: Documentation/SETUP.md
- Deleted: Documentation/VS_CODE_INTEGRATION_FIXES.md
- Deleted: SampleController.cs
- Deleted: TestApp/Program.cs
- Deleted: TestApp/TestApp.csproj
- Deleted: TestUsersController.cs
- Deleted: test-attribute-search.cs
- Deleted: test-data.json
- Deleted: test-deconstruction.cs
- Deleted: test_xml_search.cs

---

## Commit: 4af37f50

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-19 16:49:58

**Message:**
```
Merge pull request #25 from MCPRUNNER/github_prompt_addons

GitHub prompt addons
```

**Changed Files:**
- .github/prompts/document.ansible.prompt.md
- .github/prompts/document.csharp.prompt.md
- .github/prompts/document.golang.prompt.md
- .github/prompts/document.liquibase.prompt.md
- .github/prompts/document.python.prompt.md
- AI.Prompts/Ansible_Infrastructure_Documentation_Prompt.md
- AI.Prompts/CSharp_Application_Documentation_Prompt.md
- AI.Prompts/Go_Application_Documentation_Prompt.md
- AI.Prompts/Python_Application_Documentation_Prompt.md
- AI.Prompts/SQL_Liquibase_Documentation_Prompt.md
- TestApp/Program.cs
- TestApp/TestApp.csproj
- TestUsersController.cs
- test-attribute-search.cs
- test-deconstruction.cs
- test_xml_search.cs

**Changes:**
- Added: .github/prompts/document.ansible.prompt.md
- Added: .github/prompts/document.csharp.prompt.md
- Added: .github/prompts/document.golang.prompt.md
- Added: .github/prompts/document.liquibase.prompt.md
- Added: .github/prompts/document.python.prompt.md
- Added: AI.Prompts/Ansible_Infrastructure_Documentation_Prompt.md
- Added: AI.Prompts/CSharp_Application_Documentation_Prompt.md
- Added: AI.Prompts/Go_Application_Documentation_Prompt.md
- Added: AI.Prompts/Python_Application_Documentation_Prompt.md
- Added: AI.Prompts/SQL_Liquibase_Documentation_Prompt.md
- Added: TestApp/Program.cs
- Added: TestApp/TestApp.csproj
- Added: TestUsersController.cs
- Added: test-attribute-search.cs
- Added: test-deconstruction.cs
- Added: test_xml_search.cs

---

## Commit: 07a868bb

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-19 16:49:42

**Message:**
```
Update .github/prompts/document.csharp.prompt.md

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- .github/prompts/document.csharp.prompt.md

**Changes:**
- Modified: .github/prompts/document.csharp.prompt.md

---

## Commit: 88d3ba8f

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 16:46:48

**Message:**
```
setting default documentation for .github/prompts in the workspace Documentation directory

```

**Changed Files:**
- .github/prompts/document.ansible.prompt.md
- .github/prompts/document.csharp.prompt.md
- .github/prompts/document.golang.prompt.md
- .github/prompts/document.liquibase.prompt.md
- .github/prompts/document.python.prompt.md

**Changes:**
- Modified: .github/prompts/document.ansible.prompt.md
- Modified: .github/prompts/document.csharp.prompt.md
- Modified: .github/prompts/document.golang.prompt.md
- Modified: .github/prompts/document.liquibase.prompt.md
- Modified: .github/prompts/document.python.prompt.md

---

## Commit: 51939a03

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 16:44:04

**Message:**
```
Adding stock .github/prompts for documenting common language repositories

```

**Changed Files:**
- .github/prompts/document.ansible.prompt.md
- .github/prompts/document.csharp.prompt.md
- .github/prompts/document.golang.prompt.md
- .github/prompts/document.liquibase.prompt.md
- .github/prompts/document.python.prompt.md
- AI.Prompts/Ansible_Infrastructure_Documentation_Prompt.md
- AI.Prompts/CSharp_Application_Documentation_Prompt.md
- AI.Prompts/Go_Application_Documentation_Prompt.md
- AI.Prompts/Python_Application_Documentation_Prompt.md
- AI.Prompts/SQL_Liquibase_Documentation_Prompt.md
- TestApp/Program.cs
- TestApp/TestApp.csproj
- TestUsersController.cs
- test-attribute-search.cs
- test-deconstruction.cs
- test_xml_search.cs

**Changes:**
- Added: .github/prompts/document.ansible.prompt.md
- Added: .github/prompts/document.csharp.prompt.md
- Added: .github/prompts/document.golang.prompt.md
- Added: .github/prompts/document.liquibase.prompt.md
- Added: .github/prompts/document.python.prompt.md
- Added: AI.Prompts/Ansible_Infrastructure_Documentation_Prompt.md
- Added: AI.Prompts/CSharp_Application_Documentation_Prompt.md
- Added: AI.Prompts/Go_Application_Documentation_Prompt.md
- Added: AI.Prompts/Python_Application_Documentation_Prompt.md
- Added: AI.Prompts/SQL_Liquibase_Documentation_Prompt.md
- Added: TestApp/Program.cs
- Added: TestApp/TestApp.csproj
- Added: TestUsersController.cs
- Added: test-attribute-search.cs
- Added: test-deconstruction.cs
- Added: test_xml_search.cs

---

## Commit: 606b2441

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-19 10:22:19

**Message:**
```
Merge pull request #24 from MCPRUNNER/deconstructor

Deconstructor
```

**Changed Files:**
- .github/prompts/ReleaseNotesPrompt.md
- Documentation/DECONSTRUCTION_SERVICE.md
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Program.cs
- SampleController.cs
- Services/DeconstructionService.cs
- Services/IDeconstructionService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Added: .github/prompts/ReleaseNotesPrompt.md
- Added: Documentation/DECONSTRUCTION_SERVICE.md
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Program.cs
- Added: SampleController.cs
- Added: Services/DeconstructionService.cs
- Added: Services/IDeconstructionService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: f1b52a39

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-19 10:21:43

**Message:**
```
Update Services/DeconstructionService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/DeconstructionService.cs

**Changes:**
- Modified: Services/DeconstructionService.cs

---

## Commit: e4d6837e

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-19 10:21:36

**Message:**
```
Update Tools/GitServiceTools.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Tools/GitServiceTools.cs

---

## Commit: 19dea235

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 10:11:48

**Message:**
```
Updating tests

```

**Changed Files:**
- .github/prompts/ReleaseNotesPrompt.md
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Services/DeconstructionService.cs
- Services/IDeconstructionService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Added: .github/prompts/ReleaseNotesPrompt.md
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Services/DeconstructionService.cs
- Modified: Services/IDeconstructionService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: 697a825d

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 08:40:44

**Message:**
```
clearing test controller files

```

**Changed Files:**
- TestUsersController.cs
- test-deconstruction.cs

**Changes:**
- Deleted: TestUsersController.cs
- Deleted: test-deconstruction.cs

---

## Commit: eebebe97

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 08:39:09

**Message:**
```
Adding deconstructor service with method for ASP.NET controller to json tool

```

**Changed Files:**
- Documentation/DECONSTRUCTION_SERVICE.md
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Program.cs
- SampleController.cs
- Services/DeconstructionService.cs
- Services/IDeconstructionService.cs
- Services/McpServer.cs
- TestUsersController.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-deconstruction.cs

**Changes:**
- Added: Documentation/DECONSTRUCTION_SERVICE.md
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Program.cs
- Added: SampleController.cs
- Added: Services/DeconstructionService.cs
- Added: Services/IDeconstructionService.cs
- Modified: Services/McpServer.cs
- Added: TestUsersController.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test-deconstruction.cs

---

## Commit: 59b5dbbb

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-19 08:18:09

**Message:**
```
Merge pull request #23 from MCPRUNNER/content_read_fix

SearchXmlFile tool
```

**Changed Files:**
- Documentation/EXAMPLES.md
- Documentation/VS_CODE_INTEGRATION_FIXES.md
- Documentation/XML_SEARCH_IMPLEMENTATION.md
- Program.cs
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-attributes.xml
- test-config.xml

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Added: Documentation/VS_CODE_INTEGRATION_FIXES.md
- Added: Documentation/XML_SEARCH_IMPLEMENTATION.md
- Modified: Program.cs
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test-attributes.xml
- Added: test-config.xml

---

## Commit: cc6061e7

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 08:05:54

**Message:**
```
Adding SearchXmlFile tool documentation and XPath testing

```

**Changed Files:**
- Documentation/VS_CODE_INTEGRATION_FIXES.md
- Program.cs
- README.md
- Services/LocationService.cs
- test-attributes.xml

**Changes:**
- Added: Documentation/VS_CODE_INTEGRATION_FIXES.md
- Modified: Program.cs
- Modified: README.md
- Modified: Services/LocationService.cs
- Added: test-attributes.xml

---

## Commit: 4fdb3418

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 07:58:45

**Message:**
```
Adding SearchXmlFile tool

```

**Changed Files:**
- Documentation/EXAMPLES.md
- Documentation/XML_SEARCH_IMPLEMENTATION.md
- README.md

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: Documentation/XML_SEARCH_IMPLEMENTATION.md
- Modified: README.md

---

## Commit: 10fc8a17

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-19 07:58:35

**Message:**
```
Adding SearchXmlFile tool

```

**Changed Files:**
- Documentation/EXAMPLES.md
- Documentation/XML_SEARCH_IMPLEMENTATION.md
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-config.xml

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Added: Documentation/XML_SEARCH_IMPLEMENTATION.md
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test-config.xml

---

## Commit: 388e5de4

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-18 21:20:45

**Message:**
```
Merge pull request #22 from MCPRUNNER/search_improvements

SearchJsonFile tool added
```

**Changed Files:**
- Documentation/EXAMPLES.md
- Documentation/PROJECT_STATUS.md
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-data.json

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: Documentation/PROJECT_STATUS.md
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test-data.json

---

## Commit: f189814f

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-18 21:20:28

**Message:**
```
Update Services/LocationService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/LocationService.cs

**Changes:**
- Modified: Services/LocationService.cs

---

## Commit: a7db77d9

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-18 21:19:39

**Message:**
```
Update Services/LocationService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/LocationService.cs

**Changes:**
- Modified: Services/LocationService.cs

---

## Commit: 7597a5e4

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-18 21:19:33

**Message:**
```
Update Services/LocationService.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Services/LocationService.cs

**Changes:**
- Modified: Services/LocationService.cs

---

## Commit: 7ddae3e1

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-18 21:16:19

**Message:**
```
Added tool to process and search Json

```

**Changed Files:**
- Documentation/EXAMPLES.md
- README.md
- Services/ILocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: d36697b3

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-18 21:04:40

**Message:**
```
Added tool to process and search Json

```

**Changed Files:**
- Documentation/EXAMPLES.md
- Documentation/PROJECT_STATUS.md
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.csproj
- README.md
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test-data.json

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: Documentation/PROJECT_STATUS.md
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.csproj
- Modified: README.md
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs
- Added: test-data.json

---

## Commit: d830938d

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-18 07:43:33

**Message:**
```
Merge pull request #21 from MCPRUNNER/search_improvements

Updating ReadFilteredWorkspaceFiles with search pattern
```

**Changed Files:**
- Documentation/PROJECT_STATUS.md
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- README.md
- RELEASE_NOTES_FINAL.md
- Services/GitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Documentation/PROJECT_STATUS.md
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: README.md
- Deleted: RELEASE_NOTES_FINAL.md
- Modified: Services/GitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 7ad25ff6

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-18 07:36:45

**Message:**
```
Updating ReadFilteredWorkspaceFiles with search pattern

```

**Changed Files:**
- Documentation/PROJECT_STATUS.md
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- README.md
- RELEASE_NOTES_FINAL.md
- Services/GitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Documentation/PROJECT_STATUS.md
- Modified: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: README.md
- Deleted: RELEASE_NOTES_FINAL.md
- Modified: Services/GitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: e8987ba0

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-17 20:40:24

**Message:**
```
Merge pull request #20 from MCPRUNNER/prompt_loading

Prompt loading
```

**Changed Files:**
- Services/ILocationService.cs
- Services/LocationService.cs
- src/Tools/DocumentationTool.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Deleted: src/Tools/DocumentationTool.cs

---

## Commit: 7be081e9

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-17 20:38:40

**Message:**
```
Committing filePath change

```

---

## Commit: ce8465ae

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-17 20:36:34

**Message:**
```
Update variable names from filename to filePath in ReadFile

```

**Changed Files:**
- Services/LocationService.cs

**Changes:**
- Modified: Services/LocationService.cs

---

## Commit: 63a9a9c3

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-17 20:36:34

**Message:**
```
merge

```

**Changed Files:**
- Services/ILocationService.cs
- Services/LocationService.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs

---

## Commit: 983630dd

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-17 20:26:03

**Message:**
```
Clean up src/Tools and update locationservice file reader

```

**Changed Files:**
- Services/ILocationService.cs
- Services/LocationService.cs
- src/Tools/DocumentationTool.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Deleted: src/Tools/DocumentationTool.cs

---

## Commit: a4818f4d

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-17 20:26:03

**Message:**
```
Clean up src/Tools and update locationservice file reader

```

**Changed Files:**
- Services/ILocationService.cs
- Services/LocationService.cs
- src/Tools/DocumentationTool.cs

**Changes:**
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Deleted: src/Tools/DocumentationTool.cs

---

## Commit: de836ebd

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-16 18:46:15

**Message:**
```
Merge pull request #18 from MCPRUNNER/file_scanning

github instructions, and prompts added along with SamplingLLM Tool
```

**Changed Files:**
- .github/instructions/dotnet-api.instructions.md
- .github/prompts/document.prompt.md
- AI.Prompts/Sampling_SYS.md
- AI.Prompts/Sampling_USR.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.sln
- RELEASE_DOCUMENT.md
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Tools/GitServiceTools.cs
- src/Tools/DocumentationTool.cs
- test_location_service.cs
- test_workspace_files.cs

**Changes:**
- Added: .github/instructions/dotnet-api.instructions.md
- Added: .github/prompts/document.prompt.md
- Added: AI.Prompts/Sampling_SYS.md
- Added: AI.Prompts/Sampling_USR.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Added: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.sln
- Modified: RELEASE_DOCUMENT.md
- Modified: Services/GitService.cs
- Modified: Services/IGitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Tools/GitServiceTools.cs
- Added: src/Tools/DocumentationTool.cs
- Deleted: test_location_service.cs
- Deleted: test_workspace_files.cs

---

## Commit: d326c82f

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-16 18:45:17

**Message:**
```
github instructions, and prompts added along with SamplingLLM Tool

```

**Changed Files:**
- .github/instructions/dotnet-api.instructions.md
- .github/prompts/document.prompt.md
- AI.Prompts/Sampling_SYS.md
- AI.Prompts/Sampling_USR.md
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.Tests/Services/LocationServiceTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- GitVisionMCP.sln
- RELEASE_DOCUMENT.md
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/LocationService.cs
- Tools/GitServiceTools.cs
- src/Tools/DocumentationTool.cs
- test_location_service.cs
- test_workspace_files.cs

**Changes:**
- Added: .github/instructions/dotnet-api.instructions.md
- Added: .github/prompts/document.prompt.md
- Added: AI.Prompts/Sampling_SYS.md
- Added: AI.Prompts/Sampling_USR.md
- Modified: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Added: GitVisionMCP.Tests/Services/LocationServiceTests.cs
- Modified: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: GitVisionMCP.sln
- Modified: RELEASE_DOCUMENT.md
- Modified: Services/GitService.cs
- Modified: Services/IGitService.cs
- Modified: Services/ILocationService.cs
- Modified: Services/LocationService.cs
- Modified: Tools/GitServiceTools.cs
- Added: src/Tools/DocumentationTool.cs
- Deleted: test_location_service.cs
- Deleted: test_workspace_files.cs

---

## Commit: 89cf19b4

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-11 07:45:47

**Message:**
```
Merge pull request #17 from MCPRUNNER/file_scanning

File scanning
```

**Changed Files:**
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- MD/ReportFooter.md
- MD/ReportHeader.md
- Program.cs
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/IMcpServer.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test_location_service.cs
- test_workspace_files.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Added: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Added: MD/ReportFooter.md
- Added: MD/ReportHeader.md
- Modified: Program.cs
- Modified: Services/GitService.cs
- Added: Services/IGitService.cs
- Added: Services/ILocationService.cs
- Added: Services/IMcpServer.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Added: Tools/IGitServiceTools.cs
- Added: test_location_service.cs
- Added: test_workspace_files.cs

---

## Commit: 4ea86d33

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-11 07:38:50

**Message:**
```
Code refactoring, and prep work for automated header/footers with merge

```

**Changed Files:**
- Program.cs

**Changes:**
- Modified: Program.cs

---

## Commit: 9f4dc540

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-11 07:36:31

**Message:**
```
Code refactoring, and prep work for automated header/footers

```

**Changed Files:**
- MD/ReportFooter.md
- MD/ReportHeader.md
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs

**Changes:**
- Added: MD/ReportFooter.md
- Added: MD/ReportHeader.md
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: Tools/IGitServiceTools.cs

---

## Commit: 417a87d2

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-11 07:14:26

**Message:**
```
Refactoring interfaces and updating some dup code

```

**Changed Files:**
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Program.cs
- Services/GitService.cs
- Services/IGitService.cs
- Services/ILocationService.cs
- Services/IMcpServer.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/IGitServiceTools.cs
- test_location_service.cs
- test_workspace_files.cs

**Changes:**
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Added: GitVisionMCP.Tests/Tools/GitServiceToolsTests.cs
- Modified: Program.cs
- Modified: Services/GitService.cs
- Added: Services/IGitService.cs
- Added: Services/ILocationService.cs
- Added: Services/IMcpServer.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Added: Tools/IGitServiceTools.cs
- Added: test_location_service.cs
- Added: test_workspace_files.cs

---

## Commit: f2059405

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-10 20:36:51

**Message:**
```
Merge pull request #15 from MCPRUNNER/file_scanning

Adding file scanning
```

**Changed Files:**
- Documentation/EXAMPLES.md
- Models/McpModels.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- mssqlMCP_Dataflows.md

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: Models/McpModels.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Added: mssqlMCP_Dataflows.md

---

## Commit: 9d6be717

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-10 20:15:06

**Message:**
```
Adding file scanning

```

**Changed Files:**
- Documentation/EXAMPLES.md
- Models/McpModels.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- mssqlMCP_Dataflows.md

**Changes:**
- Modified: Documentation/EXAMPLES.md
- Modified: Models/McpModels.cs
- Modified: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Added: mssqlMCP_Dataflows.md

---

## Commit: 7feffc99

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-09 21:41:19

**Message:**
```
Merge pull request #14 from MCPRUNNER/locationService

Location service and get_current_branch tool
```

**Changed Files:**
- Documentation/SETUP.md
- GitVisionMCP.Tests/Services/McpServerTests.cs
- Program.cs
- Prompts/ReleaseDocumentPrompts.cs
- README.md
- RELEASE_NOTES.md
- RELEASE_NOTES_FINAL.md
- Services/GitService.cs
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- mcp.json
- mcp_fixed.json

**Changes:**
- Modified: Documentation/SETUP.md
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: Program.cs
- Modified: Prompts/ReleaseDocumentPrompts.cs
- Modified: README.md
- Added: RELEASE_NOTES.md
- Added: RELEASE_NOTES_FINAL.md
- Modified: Services/GitService.cs
- Added: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Modified: mcp.json
- Added: mcp_fixed.json

---

## Commit: d925dd76

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-09 21:41:08

**Message:**
```
Update Program.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Program.cs

**Changes:**
- Modified: Program.cs

---

## Commit: 92faf367

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-09 21:37:45

**Message:**
```
partial documentation update

```

**Changed Files:**
- Documentation/SETUP.md
- README.md

**Changes:**
- Modified: Documentation/SETUP.md
- Modified: README.md

---

## Commit: 2d6f07b5

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-09 21:33:21

**Message:**
```
added get_current_branch tool

```

**Changed Files:**
- Services/GitService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs

---

## Commit: 33517fdb

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-09 21:09:25

**Message:**
```
Minor testing done for locationService addition

```

**Changed Files:**
- GitVisionMCP.Tests/Services/McpServerTests.cs
- Prompts/ReleaseDocumentPrompts.cs
- Tools/GitServiceTools.cs
- Tools/GitTools.cs
- mcp.json

**Changes:**
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: Prompts/ReleaseDocumentPrompts.cs
- Modified: Tools/GitServiceTools.cs
- Deleted: Tools/GitTools.cs
- Modified: mcp.json

---

## Commit: 55fbf8b0

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-09 20:57:25

**Message:**
```
Pre test, locationService addition

```

**Changed Files:**
- GitVisionMCP.Tests/Services/McpServerTests.cs
- Program.cs
- RELEASE_NOTES.md
- RELEASE_NOTES_FINAL.md
- Services/LocationService.cs
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- Tools/GitTools.cs
- mcp_fixed.json

**Changes:**
- Modified: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: Program.cs
- Added: RELEASE_NOTES.md
- Added: RELEASE_NOTES_FINAL.md
- Added: Services/LocationService.cs
- Modified: Services/McpServer.cs
- Modified: Tools/GitServiceTools.cs
- Added: Tools/GitTools.cs
- Added: mcp_fixed.json

---

## Commit: f360a3c3

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-06 14:18:13

**Message:**
```
Merge pull request #13 from MCPRUNNER/prompt.v2

Adding better error control to GitServiceTools.cs
```

**Changed Files:**
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Tools/GitServiceTools.cs

---

## Commit: 8dbf7e4b

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-06 14:18:02

**Message:**
```
Update Tools/GitServiceTools.cs

Co-authored-by: Copilot <175728472+Copilot@users.noreply.github.com>
```

**Changed Files:**
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Tools/GitServiceTools.cs

---

## Commit: c7d7b183

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-06 14:03:08

**Message:**
```
Adding better error control to GitServiceTools.cs

```

**Changed Files:**
- Tools/GitServiceTools.cs

**Changes:**
- Modified: Tools/GitServiceTools.cs

---

## Commit: 2cc414d3

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-06 13:33:28

**Message:**
```
Merge pull request #12 from MCPRUNNER/prompt.v2

Prompt.v2
```

**Changed Files:**
- .vscode/settings.json
- Documentation/DOCKER.md
- Documentation/EXAMPLES.md
- Documentation/MERGE_SUMMARY.md
- Documentation/PROJECT_STATUS.md
- Documentation/RELEASE_NOTES_FINAL.md
- Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Documentation/SETUP.md
- Program.cs
- Prompts/ReleaseDocumentPrompts.cs
- Prompts/ReleaseNotesPrompt.md
- README.md
- RELEASE_DOCUMENT.md
- Services/McpServer.cs
- Tools/GitServiceTools.cs
- mcp.json

**Changes:**
- Modified: .vscode/settings.json
- Renamed: Documentation/DOCKER.md
- Renamed: Documentation/EXAMPLES.md
- Renamed: Documentation/MERGE_SUMMARY.md
- Renamed: Documentation/PROJECT_STATUS.md
- Added: Documentation/RELEASE_NOTES_FINAL.md
- Renamed: Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Renamed: Documentation/SETUP.md
- Modified: Program.cs
- Added: Prompts/ReleaseDocumentPrompts.cs
- Added: Prompts/ReleaseNotesPrompt.md
- Modified: README.md
- Added: RELEASE_DOCUMENT.md
- Modified: Services/McpServer.cs
- Added: Tools/GitServiceTools.cs
- Modified: mcp.json

---

## Commit: fda160aa

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-06 13:25:21

**Message:**
```
Fixing documentation and adding RELEASE_DOCUMENT.md

```

**Changed Files:**
- Program.cs
- RELEASE_DOCUMENT.md

**Changes:**
- Modified: Program.cs
- Added: RELEASE_DOCUMENT.md

---

## Commit: 40c592ef

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-06 13:10:37

**Message:**
```
Adding system prompts, and refactoring to be in line with other MCPRUNNER MCP Tools

```

**Changed Files:**
- README.md
- Tools/GitServiceTools.cs

**Changes:**
- Modified: README.md
- Renamed: Tools/GitServiceTools.cs

---

## Commit: bb832ee1

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-06 13:02:32

**Message:**
```
Adding system prompts, and refactoring to be in line with other MCPRUNNER MCP Tools

```

**Changed Files:**
- .vscode/settings.json
- Documentation/DOCKER.md
- Documentation/EXAMPLES.md
- Documentation/MERGE_SUMMARY.md
- Documentation/PROJECT_STATUS.md
- Documentation/RELEASE_NOTES_FINAL.md
- Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Documentation/SETUP.md
- Program.cs
- Prompts/ReleaseDocumentPrompts.cs
- Prompts/ReleaseNotesPrompt.md
- README.md
- Services/McpServer.cs
- Tools/GitTools.cs
- mcp.json

**Changes:**
- Modified: .vscode/settings.json
- Renamed: Documentation/DOCKER.md
- Renamed: Documentation/EXAMPLES.md
- Renamed: Documentation/MERGE_SUMMARY.md
- Renamed: Documentation/PROJECT_STATUS.md
- Added: Documentation/RELEASE_NOTES_FINAL.md
- Renamed: Documentation/SEARCH_TOOL_IMPLEMENTATION.md
- Renamed: Documentation/SETUP.md
- Modified: Program.cs
- Added: Prompts/ReleaseDocumentPrompts.cs
- Added: Prompts/ReleaseNotesPrompt.md
- Modified: README.md
- Modified: Services/McpServer.cs
- Added: Tools/GitTools.cs
- Modified: mcp.json

---

## Commit: 7daabc43

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-06 08:43:25

**Message:**
```
LICENSE.md

```

**Changed Files:**
- PROJECT_STATUS.md

**Changes:**
- Modified: PROJECT_STATUS.md

---

## Commit: e4b28370

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-06 08:38:48

**Message:**
```
Create LICENSE
```

**Changed Files:**
- LICENSE

**Changes:**
- Added: LICENSE

---

## Commit: c0307129

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-06 08:36:57

**Message:**
```
Merge pull request #9 from MCPRUNNER/prompt.v1

adding DOCKER.md and updating SETUP.md
```

**Changed Files:**
- DOCKER.md
- SETUP.md

**Changes:**
- Added: DOCKER.md
- Modified: SETUP.md

---

## Commit: a65cdc1a

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-06 08:07:27

**Message:**
```
adding DOCKER.md and updating SETUP.md

```

**Changed Files:**
- DOCKER.md
- SETUP.md

**Changes:**
- Added: DOCKER.md
- Modified: SETUP.md

---

## Commit: 42781a99

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-05 17:37:33

**Message:**
```
Merge pull request #8 from MCPRUNNER/docker_addon.v2

Docker related dependencies added and documented
```

**Changed Files:**
- .vscode/settings.json
- DOCUMENTATION_UPDATE_SUMMARY.md
- Dockerfile
- GitVisionMCP.csproj
- Program.cs
- README_UPDATE_SUMMARY.md
- Services/McpServer.cs
- appsettings.Production.json
- branch_comparison.md
- commit_comparison.md
- mcp.json

**Changes:**
- Modified: .vscode/settings.json
- Deleted: DOCUMENTATION_UPDATE_SUMMARY.md
- Added: Dockerfile
- Modified: GitVisionMCP.csproj
- Modified: Program.cs
- Deleted: README_UPDATE_SUMMARY.md
- Modified: Services/McpServer.cs
- Modified: appsettings.Production.json
- Deleted: branch_comparison.md
- Deleted: commit_comparison.md
- Modified: mcp.json

---

## Commit: d7aa73c5

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-05 17:33:07

**Message:**
```
Log directory override with variable GIT_APP_LOG_DIRECTORY added

```

**Changed Files:**
- DOCUMENTATION_UPDATE_SUMMARY.md
- FINAL_SOLUTION_SUMMARY.md
- Program.cs
- README_UPDATE_SUMMARY.md
- SHUTDOWN_SOLUTION.md
- mcp.json
- simple-test.ps1
- test-complete.ps1
- test-log-rotation.ps1
- test-logging.ps1
- test-naming.ps1
- test-restart.ps1
- test-shutdown.ps1

**Changes:**
- Deleted: DOCUMENTATION_UPDATE_SUMMARY.md
- Deleted: FINAL_SOLUTION_SUMMARY.md
- Modified: Program.cs
- Deleted: README_UPDATE_SUMMARY.md
- Deleted: SHUTDOWN_SOLUTION.md
- Modified: mcp.json
- Deleted: simple-test.ps1
- Deleted: test-complete.ps1
- Deleted: test-log-rotation.ps1
- Deleted: test-logging.ps1
- Deleted: test-naming.ps1
- Deleted: test-restart.ps1
- Deleted: test-shutdown.ps1

---

## Commit: 6d164b93

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-05 16:15:17

**Message:**
```
Pre setting LOG variable, working off of DOTNET_ENVIRONMENT instead. need to fix

```

**Changed Files:**
- .vscode/settings.json
- Dockerfile
- FINAL_SOLUTION_SUMMARY.md
- GitVisionMCP.csproj
- Program.cs
- SHUTDOWN_SOLUTION.md
- Services/McpServer.cs
- appsettings.Production.json
- branch_comparison.md
- build-docker.ps1
- commit_comparison.md
- mcp.json
- scripts/stop-mcp.ps1
- simple-test.ps1
- test-complete.ps1
- test-log-rotation.ps1
- test-logging.ps1
- test-naming.ps1
- test-restart.ps1
- test-shutdown.ps1

**Changes:**
- Modified: .vscode/settings.json
- Modified: Dockerfile
- Added: FINAL_SOLUTION_SUMMARY.md
- Modified: GitVisionMCP.csproj
- Modified: Program.cs
- Added: SHUTDOWN_SOLUTION.md
- Modified: Services/McpServer.cs
- Modified: appsettings.Production.json
- Deleted: branch_comparison.md
- Deleted: build-docker.ps1
- Deleted: commit_comparison.md
- Modified: mcp.json
- Deleted: scripts/stop-mcp.ps1
- Added: simple-test.ps1
- Added: test-complete.ps1
- Added: test-log-rotation.ps1
- Added: test-logging.ps1
- Added: test-naming.ps1
- Added: test-restart.ps1
- Added: test-shutdown.ps1

---

## Commit: 476e050f

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-05 13:56:16

**Message:**
```
Working but not cleanly stopping in mcp.json

```

**Changed Files:**
- Dockerfile
- build-docker.ps1
- scripts/stop-mcp.ps1

**Changes:**
- Added: Dockerfile
- Added: build-docker.ps1
- Added: scripts/stop-mcp.ps1

---

## Commit: 5a6af5c3

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-04 16:18:42

**Message:**
```
Merge pull request #7 from MCPRUNNER/tests

Tests
```

**Changed Files:**
- GitVisionMCP.Tests/GitVisionMCP.Tests.csproj
- GitVisionMCP.Tests/Models/McpModelsTests.cs
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.Tests/Services/McpServerTests.cs
- GitVisionMCP.csproj
- GitVisionMCP.sln
- Services/McpServer.cs

**Changes:**
- Added: GitVisionMCP.Tests/GitVisionMCP.Tests.csproj
- Added: GitVisionMCP.Tests/Models/McpModelsTests.cs
- Added: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Added: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: GitVisionMCP.csproj
- Modified: GitVisionMCP.sln
- Modified: Services/McpServer.cs

---

## Commit: c8dc1b08

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-04 16:15:05

**Message:**
```
csproj cleanup

```

**Changed Files:**
- GitVisionMCP.csproj

**Changes:**
- Modified: GitVisionMCP.csproj

---

## Commit: 51c07301

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-04 15:50:16

**Message:**
```
McpServerTests.cs

```

**Changed Files:**
- GitVisionMCP.Tests/Services/McpServerTests.cs
- Services/McpServer.cs

**Changes:**
- Added: GitVisionMCP.Tests/Services/McpServerTests.cs
- Modified: Services/McpServer.cs

---

## Commit: b20294a3

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-04 15:29:55

**Message:**
```
McpModelTests and GitServiceTests

```

**Changed Files:**
- GitVisionMCP.Tests/GitVisionMCP.Tests.csproj
- GitVisionMCP.Tests/Models/McpModelsTests.cs
- GitVisionMCP.Tests/Services/GitServiceTests.cs
- GitVisionMCP.csproj
- GitVisionMCP.sln

**Changes:**
- Added: GitVisionMCP.Tests/GitVisionMCP.Tests.csproj
- Added: GitVisionMCP.Tests/Models/McpModelsTests.cs
- Added: GitVisionMCP.Tests/Services/GitServiceTests.cs
- Modified: GitVisionMCP.csproj
- Modified: GitVisionMCP.sln

---

## Commit: 15eff804

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-03 14:14:27

**Message:**
```
Merge pull request #5 from MCPRUNNER/rename_repo

GitVisionMCP.sln fix
```

**Changed Files:**
- selfDocumentMCP.sln

**Changes:**
- Deleted: selfDocumentMCP.sln

---

## Commit: 4cf43373

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-03 14:12:38

**Message:**
```
GitVisionMCP.sln fix

```

**Changed Files:**
- selfDocumentMCP.sln

**Changes:**
- Deleted: selfDocumentMCP.sln

---

## Commit: 808661fe

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-03 13:38:32

**Message:**
```
Merge pull request #4 from MCPRUNNER/rename_repo

Using new GitVisionMCP project name
```

**Changed Files:**
- DOCUMENTATION_UPDATE_SUMMARY.md
- EXAMPLES.md
- GitVisionMCP.csproj
- GitVisionMCP.http
- GitVisionMCP.sln
- MERGE_SUMMARY.md
- Models/McpModels.cs
- PROJECT_STATUS.md
- Program.cs
- README.md
- README_UPDATE_SUMMARY.md
- SEARCH_TOOL_IMPLEMENTATION.md
- Services/GitService.cs
- Services/McpServer.cs
- appsettings.Development.json
- appsettings.Production.json
- appsettings.json
- branch_comparison.md
- mcp.json
- selfDocumentMCP.sln

**Changes:**
- Modified: DOCUMENTATION_UPDATE_SUMMARY.md
- Modified: EXAMPLES.md
- Renamed: GitVisionMCP.csproj
- Renamed: GitVisionMCP.http
- Added: GitVisionMCP.sln
- Modified: MERGE_SUMMARY.md
- Modified: Models/McpModels.cs
- Modified: PROJECT_STATUS.md
- Modified: Program.cs
- Modified: README.md
- Modified: README_UPDATE_SUMMARY.md
- Modified: SEARCH_TOOL_IMPLEMENTATION.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Modified: appsettings.Development.json
- Modified: appsettings.Production.json
- Modified: appsettings.json
- Modified: branch_comparison.md
- Modified: mcp.json
- Modified: selfDocumentMCP.sln

---

## Commit: 394eb861

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-03 13:28:36

**Message:**
```
Using new GitVisionMCP project name

```

**Changed Files:**
- DOCUMENTATION_UPDATE_SUMMARY.md
- EXAMPLES.md
- GitVisionMCP.csproj
- GitVisionMCP.http
- GitVisionMCP.sln
- MERGE_SUMMARY.md
- Models/McpModels.cs
- PROJECT_STATUS.md
- Program.cs
- README.md
- README_UPDATE_SUMMARY.md
- SEARCH_TOOL_IMPLEMENTATION.md
- Services/GitService.cs
- Services/McpServer.cs
- appsettings.Development.json
- appsettings.Production.json
- appsettings.json
- branch_comparison.md
- mcp.json
- selfDocumentMCP.sln

**Changes:**
- Modified: DOCUMENTATION_UPDATE_SUMMARY.md
- Modified: EXAMPLES.md
- Renamed: GitVisionMCP.csproj
- Renamed: GitVisionMCP.http
- Added: GitVisionMCP.sln
- Modified: MERGE_SUMMARY.md
- Modified: Models/McpModels.cs
- Modified: PROJECT_STATUS.md
- Modified: Program.cs
- Modified: README.md
- Modified: README_UPDATE_SUMMARY.md
- Modified: SEARCH_TOOL_IMPLEMENTATION.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Modified: appsettings.Development.json
- Modified: appsettings.Production.json
- Modified: appsettings.json
- Modified: branch_comparison.md
- Modified: mcp.json
- Modified: selfDocumentMCP.sln

---

## Commit: 4a7e6f47

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-03 13:10:10

**Message:**
```
Merge pull request #3 from MCPRUNNER/search_tool

Adding Search tool, and updating docs
```

**Changed Files:**
- DOCUMENTATION_UPDATE_SUMMARY.md
- EXAMPLES.md
- Models/McpModels.cs
- PROJECT_STATUS.md
- README.md
- README_UPDATE_SUMMARY.md
- SEARCH_TOOL_IMPLEMENTATION.md
- Services/GitService.cs
- Services/McpServer.cs
- obj/Debug/net9.0/.NETCoreApp,Version=v9.0.AssemblyAttributes.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- obj/Debug/net9.0/selfDocumentMCP.GlobalUsings.g.cs
- obj/Debug/net9.0/selfDocumentMCP.assets.cache
- obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- obj/project.assets.json
- obj/project.nuget.cache
- obj/selfDocumentMCP.csproj.nuget.dgspec.json
- obj/selfDocumentMCP.csproj.nuget.g.props
- obj/selfDocumentMCP.csproj.nuget.g.targets

**Changes:**
- Added: DOCUMENTATION_UPDATE_SUMMARY.md
- Modified: EXAMPLES.md
- Modified: Models/McpModels.cs
- Modified: PROJECT_STATUS.md
- Modified: README.md
- Added: README_UPDATE_SUMMARY.md
- Added: SEARCH_TOOL_IMPLEMENTATION.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Deleted: obj/Debug/net9.0/.NETCoreApp,Version=v9.0.AssemblyAttributes.cs
- Deleted: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Deleted: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Deleted: obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- Deleted: obj/Debug/net9.0/selfDocumentMCP.GlobalUsings.g.cs
- Deleted: obj/Debug/net9.0/selfDocumentMCP.assets.cache
- Deleted: obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- Deleted: obj/project.assets.json
- Deleted: obj/project.nuget.cache
- Deleted: obj/selfDocumentMCP.csproj.nuget.dgspec.json
- Deleted: obj/selfDocumentMCP.csproj.nuget.g.props
- Deleted: obj/selfDocumentMCP.csproj.nuget.g.targets

---

## Commit: 92772c70

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-03 13:09:09

**Message:**
```
updating docs and cleanup

```

**Changed Files:**
- DOCUMENTATION_UPDATE_SUMMARY.md
- EXAMPLES.md
- Models/McpModels.cs
- PROJECT_STATUS.md
- README.md
- README_UPDATE_SUMMARY.md
- SEARCH_TOOL_IMPLEMENTATION.md
- Services/GitService.cs
- Services/McpServer.cs
- obj/Debug/net9.0/.NETCoreApp,Version=v9.0.AssemblyAttributes.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- obj/Debug/net9.0/selfDocumentMCP.GlobalUsings.g.cs
- obj/Debug/net9.0/selfDocumentMCP.assets.cache
- obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- obj/project.assets.json
- obj/project.nuget.cache
- obj/selfDocumentMCP.csproj.nuget.dgspec.json
- obj/selfDocumentMCP.csproj.nuget.g.props
- obj/selfDocumentMCP.csproj.nuget.g.targets

**Changes:**
- Added: DOCUMENTATION_UPDATE_SUMMARY.md
- Modified: EXAMPLES.md
- Modified: Models/McpModels.cs
- Modified: PROJECT_STATUS.md
- Modified: README.md
- Added: README_UPDATE_SUMMARY.md
- Added: SEARCH_TOOL_IMPLEMENTATION.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Deleted: obj/Debug/net9.0/.NETCoreApp,Version=v9.0.AssemblyAttributes.cs
- Deleted: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Deleted: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Deleted: obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- Deleted: obj/Debug/net9.0/selfDocumentMCP.GlobalUsings.g.cs
- Deleted: obj/Debug/net9.0/selfDocumentMCP.assets.cache
- Deleted: obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- Deleted: obj/project.assets.json
- Deleted: obj/project.nuget.cache
- Deleted: obj/selfDocumentMCP.csproj.nuget.dgspec.json
- Deleted: obj/selfDocumentMCP.csproj.nuget.g.props
- Deleted: obj/selfDocumentMCP.csproj.nuget.g.targets

---

## Commit: d495fffe

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-03 12:25:03

**Message:**
```
adding documentaion: MERGE_SUMMARY.md

```

**Changed Files:**
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

**Changes:**
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

---

## Commit: 11dfaf0b

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-03 08:20:14

**Message:**
```
adding documentaion: MERGE_SUMMARY.md

```

**Changed Files:**
- JsonTest.cs
- MERGE_SUMMARY.md
- TestModels.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- output.txt
- test-mcp.ps1
- test_json_input.json
- test_output.txt
- test_recent_commits.json

**Changes:**
- Deleted: JsonTest.cs
- Added: MERGE_SUMMARY.md
- Deleted: TestModels.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Deleted: output.txt
- Deleted: test-mcp.ps1
- Deleted: test_json_input.json
- Deleted: test_output.txt
- Deleted: test_recent_commits.json

---

## Commit: 143acb2d

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-03 07:48:58

**Message:**
```
Merge pull request #2 from MCPRUNNER/line_commit_diffs

Line commit diffs
```

**Changed Files:**
- EXAMPLES.md
- JsonTest.cs
- Models/McpModels.cs
- PROJECT_STATUS.md
- README.md
- Services/GitService.cs
- Services/McpServer.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- test_json_input.json

**Changes:**
- Modified: EXAMPLES.md
- Added: JsonTest.cs
- Modified: Models/McpModels.cs
- Modified: PROJECT_STATUS.md
- Modified: README.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Added: test_json_input.json

---

## Commit: 7b559d81

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-03 07:39:16

**Message:**
```
Adding line numbering to  line diff between commits tool

```

**Changed Files:**
- Services/McpServer.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

**Changes:**
- Modified: Services/McpServer.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

---

## Commit: 6bf95589

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-03 07:24:08

**Message:**
```
Adding line diff between commits tool

```

**Changed Files:**
- EXAMPLES.md
- JsonTest.cs
- Models/McpModels.cs
- PROJECT_STATUS.md
- README.md
- Services/GitService.cs
- Services/McpServer.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- test_json_input.json

**Changes:**
- Modified: EXAMPLES.md
- Added: JsonTest.cs
- Modified: Models/McpModels.cs
- Modified: PROJECT_STATUS.md
- Modified: README.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Added: test_json_input.json

---

## Commit: bb71795c

**Author:** 7045kHz <66392808+7045kHz@users.noreply.github.com>
**Date:** 2025-07-02 20:11:43

**Message:**
```
Merge pull request #1 from MCPRUNNER/documentation

Updating documentation
```

**Changed Files:**
- EXAMPLES.md
- PROJECT_STATUS.md
- README.md
- SETUP.md

**Changes:**
- Modified: EXAMPLES.md
- Modified: PROJECT_STATUS.md
- Modified: README.md
- Modified: SETUP.md

---

## Commit: 8db83f92

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 20:08:12

**Message:**
```
Updating documentation

```

**Changed Files:**
- EXAMPLES.md
- PROJECT_STATUS.md
- README.md
- SETUP.md

**Changes:**
- Modified: EXAMPLES.md
- Modified: PROJECT_STATUS.md
- Modified: README.md
- Modified: SETUP.md

---

## Commit: 0d3ce588

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 20:02:09

**Message:**
```
Updated with remote repository capabilities

```

**Changed Files:**
- EXAMPLES.md
- PROJECT_STATUS.md
- README.md
- SETUP.md
- Services/GitService.cs
- Services/McpServer.cs
- branch_comparison.md
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

**Changes:**
- Modified: EXAMPLES.md
- Modified: PROJECT_STATUS.md
- Modified: README.md
- Modified: SETUP.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Added: branch_comparison.md
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

---

## Commit: 6e158e92

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 19:37:27

**Message:**
```
Updating for extended comparison of files

```

**Changed Files:**
- Models/McpModels.cs
- Services/GitService.cs
- Services/McpServer.cs
- commit_comparison.md
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- raw_output.json
- test_compact.json
- test_json_input.json
- test_recent_commits.json

**Changes:**
- Modified: Models/McpModels.cs
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Added: commit_comparison.md
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Deleted: raw_output.json
- Deleted: test_compact.json
- Deleted: test_json_input.json
- Added: test_recent_commits.json

---

## Commit: 521286fa

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 19:23:55

**Message:**
```
Serilog added

```

**Changed Files:**
- JsonTest.cs
- Program.cs
- README.md
- Services/McpServer.cs
- appsettings.Development.json
- appsettings.json
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- obj/Debug/net9.0/selfDocumentMCP.assets.cache
- obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- obj/project.assets.json
- obj/project.nuget.cache
- obj/selfDocumentMCP.csproj.nuget.dgspec.json
- obj/selfDocumentMCP.csproj.nuget.g.targets
- selfDocumentMCP.csproj
- test_compact.json
- test_json_input.json

**Changes:**
- Deleted: JsonTest.cs
- Modified: Program.cs
- Modified: README.md
- Modified: Services/McpServer.cs
- Modified: appsettings.Development.json
- Modified: appsettings.json
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Modified: obj/Debug/net9.0/selfDocumentMCP.assets.cache
- Modified: obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- Modified: obj/project.assets.json
- Modified: obj/project.nuget.cache
- Modified: obj/selfDocumentMCP.csproj.nuget.dgspec.json
- Modified: obj/selfDocumentMCP.csproj.nuget.g.targets
- Modified: selfDocumentMCP.csproj
- Added: test_compact.json
- Added: test_json_input.json

---

## Commit: 72e5d5dd

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 19:13:27

**Message:**
```
need to remove \r\n from STDIO JSON output

```

**Changed Files:**
- JsonTest.cs
- Program.cs
- Services/McpServer.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- obj/Debug/net9.0/selfDocumentMCP.assets.cache
- output.txt
- raw_output.json
- test_output.txt

**Changes:**
- Added: JsonTest.cs
- Modified: Program.cs
- Modified: Services/McpServer.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Modified: obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- Modified: obj/Debug/net9.0/selfDocumentMCP.assets.cache
- Added: output.txt
- Added: raw_output.json
- Added: test_output.txt

---

## Commit: 1f379345

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 18:59:47

**Message:**
```
working on initialization

```

**Changed Files:**
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- obj/Debug/net9.0/selfDocumentMCP.assets.cache

**Changes:**
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Modified: obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- Modified: obj/Debug/net9.0/selfDocumentMCP.assets.cache

---

## Commit: 6f0b0f79

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 18:55:04

**Message:**
```
Cleaned up STDIO output

```

**Changed Files:**
- EXAMPLES.md
- Models/McpModels.cs
- PROJECT_STATUS.md
- Program.cs
- README.md
- SETUP.md
- Services/GitService.cs
- Services/McpServer.cs
- TestModels.cs
- appsettings.Production.json
- appsettings.json
- mcp.json
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- obj/Debug/net9.0/selfDocumentMCP.assets.cache
- test-mcp.ps1

**Changes:**
- Modified: EXAMPLES.md
- Modified: Models/McpModels.cs
- Modified: PROJECT_STATUS.md
- Modified: Program.cs
- Modified: README.md
- Added: SETUP.md
- Modified: Services/GitService.cs
- Modified: Services/McpServer.cs
- Modified: TestModels.cs
- Added: appsettings.Production.json
- Modified: appsettings.json
- Modified: mcp.json
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Modified: obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- Modified: obj/Debug/net9.0/selfDocumentMCP.assets.cache
- Modified: test-mcp.ps1

---

## Commit: e8910405

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 18:41:58

**Message:**
```
Initial Code

```

**Changed Files:**
- .vscode/launch.json
- .vscode/settings.json
- .vscode/tasks.json
- EXAMPLES.md
- Models/McpModels.cs
- PROJECT_STATUS.md
- Program.cs
- README.md
- Services/GitService.cs
- Services/McpServer.cs
- TestModels.cs
- appsettings.Development.json
- appsettings.json
- mcp.json
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- obj/Debug/net9.0/selfDocumentMCP.GlobalUsings.g.cs
- obj/Debug/net9.0/selfDocumentMCP.assets.cache
- obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- obj/project.assets.json
- obj/project.nuget.cache
- obj/selfDocumentMCP.csproj.nuget.dgspec.json
- obj/selfDocumentMCP.csproj.nuget.g.props
- obj/selfDocumentMCP.csproj.nuget.g.targets
- selfDocumentMCP.csproj
- test-mcp.ps1

**Changes:**
- Added: .vscode/launch.json
- Added: .vscode/settings.json
- Added: .vscode/tasks.json
- Added: EXAMPLES.md
- Added: Models/McpModels.cs
- Added: PROJECT_STATUS.md
- Modified: Program.cs
- Added: README.md
- Added: Services/GitService.cs
- Added: Services/McpServer.cs
- Added: TestModels.cs
- Modified: appsettings.Development.json
- Modified: appsettings.json
- Added: mcp.json
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache
- Modified: obj/Debug/net9.0/selfDocumentMCP.GeneratedMSBuildEditorConfig.editorconfig
- Modified: obj/Debug/net9.0/selfDocumentMCP.GlobalUsings.g.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.assets.cache
- Modified: obj/Debug/net9.0/selfDocumentMCP.csproj.AssemblyReference.cache
- Modified: obj/project.assets.json
- Modified: obj/project.nuget.cache
- Modified: obj/selfDocumentMCP.csproj.nuget.dgspec.json
- Modified: obj/selfDocumentMCP.csproj.nuget.g.props
- Modified: obj/selfDocumentMCP.csproj.nuget.g.targets
- Modified: selfDocumentMCP.csproj
- Added: test-mcp.ps1

---

## Commit: 003f82ef

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 18:22:58

**Message:**
```
Initial Repo Creation

```

**Changed Files:**
- .gitignore
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

**Changes:**
- Added: .gitignore
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfo.cs
- Modified: obj/Debug/net9.0/selfDocumentMCP.AssemblyInfoInputs.cache

---

## Commit: 9cbe5227

**Author:** 7045kHz <7mhz.cw@gmail.com>
**Date:** 2025-07-02 18:22:01

**Message:**
```
Initial Repo Creation

```

---

