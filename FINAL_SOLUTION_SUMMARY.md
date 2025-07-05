# GitVisionMCP Docker Signal Handling - Final Solution Summary

## Overview

Successfully implemented robust signal handling for the GitVisionMCP server to work seamlessly with VS Code MCP agent integration. The solution ensures automatic container shutdown when VS Code stops the agent, without requiring manual intervention.

## Key Components

### 1. Signal Handling in Program.cs

- **Ctrl+C (SIGINT)**: Graceful shutdown with cancellation token
- **SIGTERM**: Proper handling for Docker container termination
- **ApplicationStopping**: ASP.NET Core application lifecycle integration
- **PosixSignalRegistration**: Cross-platform signal handling

### 2. Docker Configuration

- **Multi-stage build**: Optimized Docker image with separate build and runtime stages
- **tini init system**: Proper signal handling and process management
- **Child subreaper**: Eliminates tini warnings with `TINI_SUBREAPER=1` and `-s` flag
- **Git integration**: Includes git in container for repository operations

### 3. Log Rotation and Management

- **Timestamped filenames**: Log files use pattern `gitvisionmcp-yyyyMMdd.log`
- **Daily rotation**: Automatically creates new log files each day
- **Size-based rotation**: Rotates when logs exceed 10MB
- **Retention policy**: Keeps last 30 log files automatically
- **Environment-specific paths**: `/app/logs` for Docker, `./logs` for local development
- **Serilog integration**: Robust logging framework with file sinks

### 4. MCP Server Integration

- **Stdin monitoring**: Detects when VS Code agent disconnects
- **Graceful shutdown**: Proper cleanup of resources and connections
- **JSON-RPC 2.0**: Standard MCP protocol implementation
- **Cancellation token propagation**: Ensures all async operations can be cancelled

## File Changes

### Dockerfile

```dockerfile
# Uses tini as init system with child subreaper functionality
ENV TINI_SUBREAPER=1
ENTRYPOINT ["/usr/bin/tini", "-s", "--", "dotnet", "../GitVisionMCP.dll"]
```

### Program.cs

```csharp
// Comprehensive signal handling
builder.Services.AddHostedService<SignalHandlingService>();
PosixSignalRegistration.Create(PosixSignal.SIGTERM, OnSignalReceived);
Console.CancelKeyPress += (sender, e) => { /* Handle Ctrl+C */ };

// Environment-specific log directory
var logDirectory = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Production"
    ? "/app/logs"  // Absolute path for Docker
    : Path.Combine(Directory.GetCurrentDirectory(), "logs");  // Relative path for local

// Configure Serilog with timestamped files and log rotation
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(Path.Combine(logDirectory, "gitvisionmcp-.log"),
        shared: true,
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 10 * 1024 * 1024, // 10MB
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
```

### .vscode/mcp.json

```json
{
  "mcpServers": {
    "GitVisionMCP": {
      "command": "docker",
      "args": [
        "run",
        "--rm",
        "-i",
        "--init",
        "--stop-timeout",
        "10",
        "-v",
        "${workspaceFolder}:/app/repo",
        "mcprunner/gitvisionmcp:latest"
      ]
    }
  }
}
```

## Docker Logging Configuration

### Issue

When running in Docker, logs were not being written to the mounted `/app/logs` directory due to incorrect path configuration.

### Solution

Updated logging configuration in `Program.cs` to use absolute paths for Docker environment:

```csharp
// Determine log path - use absolute path for Docker, relative for local
var logPath = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Production"
    ? "/app/logs/gitvisionmcp.log"  // Absolute path for Docker
    : Path.Combine(Directory.GetCurrentDirectory(), "logs", "gitvisionmcp.log");  // Relative path for local

// Configure Serilog with proper file logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(logPath,
        shared: true,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
```

### Package Updates

- Replaced `Serilog.Extensions.Logging.File` with proper Serilog packages:
  - `Serilog` 4.0.0
  - `Serilog.Extensions.Logging` 8.0.0
  - `Serilog.Sinks.File` 6.0.0

### Benefits

- **✅ Correct log file location**: Logs are written to `/app/logs/gitvisionmcp.log` in Docker
- **✅ Host system access**: Log files are accessible via Docker volume mount
- **✅ Environment-specific paths**: Different paths for local vs Docker environments
- **✅ Structured logging**: Proper timestamps and log levels with Serilog

## Container Naming Conflict Resolution

### Issue

When VS Code MCP agent restarts the GitVisionMCP server, it tries to create a new Docker container with the same name, causing conflicts:

```
Error response from daemon: Conflict. The container name "/gitvisionmcp-mcp" is already in use
```

### Solution

Removed the `--name` parameter from the Docker run command in `.vscode/mcp.json`:

**Before:**

```json
"args": [
    "run", "--rm", "-i", "--init",
    "--name", "gitvisionmcp-mcp",
    "--stop-timeout", "10",
    // ... other args
]
```

**After:**

```json
"args": [
    "run", "--rm", "-i", "--init",
    "--stop-timeout", "10",
    // ... other args
]
```

### Benefits

- **No naming conflicts**: Each container gets a unique auto-generated name
- **Proper cleanup**: `--rm` flag ensures containers are removed when stopped
- **Rapid restarts**: VS Code can restart the MCP server without conflicts
- **Simplified configuration**: No need to manage container names manually

## Test Results

### ✅ Container Startup

- No tini warnings
- Clean application startup
- Proper signal handling initialization

### ✅ MCP Protocol

- Responds to initialize requests
- Proper JSON-RPC 2.0 communication
- Tool discovery and execution

### ✅ Graceful Shutdown

- Automatic shutdown when VS Code stops agent
- No manual Ctrl+C required
- Proper resource cleanup

### Log Rotation Verification

The updated logging configuration successfully creates timestamped log files with automatic rotation:

**Log File Pattern**: `gitvisionmcp-yyyyMMdd.log`

- Example: `gitvisionmcp-20250705.log`

**Sample Log Content**:

```
2025-07-05 19:55:41.290 +00:00 [INF] Starting MCP Server...
2025-07-05 19:55:46.015 +00:00 [INF] MCP Server stopped
```

**Rotation Features**:

- Daily rotation creates new files automatically
- 10MB size limit triggers rotation when needed
- 30-day retention policy removes old logs
- Shared file access allows concurrent reading
- Environment-specific paths for Docker vs local development

**Testing Process**:

1. Cleared existing logs
2. Started container multiple times
3. Verified timestamped filenames
4. Confirmed proper log content and formatting
5. Validated automatic cleanup and shutdown logging

## Production Readiness

The GitVisionMCP server is now production-ready with:

1. **Robust signal handling** across all platforms
2. **Docker best practices** with proper init system
3. **VS Code MCP agent compatibility** with automatic lifecycle management
4. **Comprehensive testing** with PowerShell test scripts
5. **Zero-warning startup** and clean shutdown

## Usage

To run with VS Code MCP agent:

1. Ensure Docker is running
2. Build the image: `docker build -t mcprunner/gitvisionmcp:latest .`
3. Configure VS Code with the provided mcp.json
4. Start VS Code - the container will start automatically
5. Stop VS Code - the container will shutdown automatically

## Testing

Use the provided PowerShell scripts:

- `simple-test.ps1`: Quick functionality test
- `test-complete.ps1`: Comprehensive MCP protocol test
- `test-shutdown.ps1`: Signal handling verification

The solution is now complete and ready for production use with VS Code MCP agent integration.
