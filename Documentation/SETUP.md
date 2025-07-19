# Quick Setup Guide for GitVisionMCP

A comprehensive Model Context Protocol (MCP) Server with advanced git analysis and remote branch support.

## Step 1: Build the Project

```powershell
cd "c:\Users\U00001\source\repos\MCP\GitVisionMCP"
dotnet build --configuration Release
```

## Step 2: Test MCP Communication

```powershell
# Set environment and test initialize
$env:DOTNET_ENVIRONMENT="Production"
echo '{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0.0"}}}' | dotnet run --no-build --verbosity quiet
```

**Expected output** (clean JSON only):

```json
{
  "jsonrpc": "2.0",
  "id": 1,
  "result": {
    "protocolVersion": "2024-11-05",
    "capabilities": {
      "tools": {},
      "logging": {}
    },
    "serverInfo": {
      "name": "selfDocumentMCP",
      "version": "1.0.0"
    }
  }
}
```

## Step 3: Configure VS Code MCP

Update your VS Code MCP configuration file (`.vscode/mcp.json` or similar):

```json
{
  "servers": {
    "GitVisionMCP": {
      "type": "stdio",
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "c:\\Users\\U00001\\source\\repos\\MCP\\GitVisionMCP\\GitVisionMCP.csproj",
        "--no-build",
        "--verbosity",
        "quiet"
      ],
      "env": {
        "DOTNET_ENVIRONMENT": "Production",
        "GIT_REPOSITORY_DIRECTORY": "c:\\Users\\my\\source\\repos\\gitrepo_directory"
      }
    },
    "GitVisionMCP-Docker": {
      "type": "stdio",
      "command": "docker",
      "args": [
        "run",
        "--rm",
        "-i",
        "--init",
        "--stop-timeout",
        "10",
        "-e",
        "GITVISION_MCP_TRANSPORT=Stdio",
        "-e",
        "GIT_APP_LOG_DIRECTORY=/app/logs",
        "-e",
        "GIT_REPOSITORY_DIRECTORY=c:\\Users\\my\\source\\repos\\gitrepo_directory",
        "-v",
        "c:\\Users\\my\\source\\repos\\gitrepo_directory:/app/repo:ro",
        "-v",
        "c:\\Users\\my\\source\\repos\\gitrepo_directory\\logs:/app/logs",
        "mcprunner/gitvisionmcp:latest"
      ],
      "env": {
        "DOTNET_ENVIRONMENT": "Production"
      }
    }
  }
}
```

### Configuration Parameters Explained

#### Environment Variables

**DOTNET_ENVIRONMENT**

- **Purpose**: Controls the .NET application environment and logging behavior
- **Values**: `Production`, `Development`, `Staging`
- **Recommended**: `Production` for clean JSON-only output without debug messages
- **Effect**: In Production mode, suppresses verbose logging to ensure clean MCP communication

**GIT_REPOSITORY_DIRECTORY**

- **Purpose**: Specifies the target git repository to analyze and document
- **Format**: Absolute path to a git repository directory
- **Example**: `c:\\Users\\my\\source\\repos\\gitrepo_directory`
- **Required**: Yes - this is the repository that GitVisionMCP will analyze
- **Note**: Must be a valid git repository with `.git` folder

**GITVISION_MCP_TRANSPORT** (Docker only)

- **Purpose**: Specifies the communication transport method for the MCP server
- **Value**: `Stdio` (standard input/output for VS Code communication)
- **Required**: Yes for Docker container communication
- **Effect**: Enables proper JSON-RPC communication between VS Code and the container

**GIT_APP_LOG_DIRECTORY** (Docker only)

- **Purpose**: Directory path inside container where application logs are written
- **Value**: `/app/logs` (container internal path)
- **Effect**: Separates application logs from MCP communication output
- **Mount**: Maps to host directory via volume mount for log persistence

#### Command Arguments Explained

**GitVisionMCP (Direct .NET) Configuration:**

- `"command": "dotnet"` - Uses the .NET CLI to run the application
- `"--project"` - Specifies the exact .csproj file to run
- `"--no-build"` - Skips building, uses existing compiled binaries (faster startup)
- `"--verbosity quiet"` - Suppresses build output for clean JSON communication

**GitVisionMCP-Docker Configuration:**

- `"command": "docker"` - Uses Docker to run containerized version
- `"--rm"` - Automatically removes container when it stops
- `"-i"` - Keeps stdin open for interactive communication
- `"--init"` - Uses proper init process for signal handling
- `"--stop-timeout 10"` - Allows 10 seconds for graceful shutdown

#### Volume Mounts (Docker)

**Repository Mount:**

```
-v "c:\\Users\\my\\source\\repos\\gitrepo_directory:/app/repo:ro"
```

- **Host Path**: `c:\\Users\\my\\source\\repos\\gitrepo_directory` (the git repository to analyze)
- **Container Path**: `/app/repo` (where the repository appears inside container)
- **Mode**: `:ro` (read-only - container cannot modify your source code)
- **Purpose**: Provides container access to analyze your git repository

**Logs Mount:**

```
-v "c:\\Users\\my\\source\\repos\\gitrepo_directory\\logs:/app/logs"
```

- **Host Path**: `c:\\Users\\my\\source\\repos\\gitrepo_directory\\logs` (where logs are saved on host)
- **Container Path**: `/app/logs` (where container writes logs)
- **Mode**: Read-write (container can write log files)
- **Purpose**: Persists application logs on host filesystem

#### Path Requirements

**Critical Paths to Update:**

1. **Project Path**: `c:\\Users\\U00001\\source\\repos\\MCP\\GitVisionMCP\\GitVisionMCP.csproj`
   - Must point to your actual GitVisionMCP installation location
2. **Repository Directory**: `c:\\Users\\my\\source\\repos\\gitrepo_directory`

   - Must point to the git repository you want to analyze
   - Must be a valid git repository (contains `.git` folder)
   - Can be any git repository on your system

3. **Docker Image**: `mcprunner/gitvisionmcp:latest`
   - Pre-built Docker image with GitVisionMCP
   - Alternative to running .NET directly
   - Ensures consistent environment
