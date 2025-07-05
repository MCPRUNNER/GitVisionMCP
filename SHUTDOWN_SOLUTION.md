# GitVisionMCP Docker Container - Automatic Shutdown Solution

## Problem Solved

The GitVisionMCP Docker container now automatically shuts down when VS Code stops the MCP agent, eliminating the need for manual Ctrl+C interruption.

## Key Improvements Implemented

### 1. Enhanced Signal Handling in Program.cs

- **PosixSignalRegistration**: Added explicit SIGTERM handling for Unix systems (Docker containers)
- **Combined Cancellation Tokens**: Unified handling of SIGINT (Ctrl+C), SIGTERM (Docker stop), and application lifecycle events
- **Robust Error Handling**: Proper exception handling for graceful shutdown scenarios

### 2. Docker Container Improvements

- **Tini Init System**: Added `tini` as an init system in the Dockerfile for proper signal propagation
- **Multi-stage Build**: Optimized build process with separate build and runtime stages
- **Stop Timeout**: Added `--stop-timeout 5` for controlled shutdown timing

### 3. MCP Server Enhancements

- **Stdin Monitoring**: Added detection of closed input streams (normal termination condition for stdio transport)
- **Cancellation Checks**: Added cancellation token checks throughout the request processing loop
- **Graceful Termination**: Proper cleanup and logging on shutdown

### 4. Configuration Updates

- **Docker Run Parameters**: Updated `.vscode/mcp.json` with optimal Docker flags:
  - `--init`: Uses Docker's built-in init system
  - `--stop-timeout 5`: Allows 5 seconds for graceful shutdown
  - `-i`: Interactive mode for stdio transport
  - `--rm`: Automatic container cleanup

## Technical Details

### Signal Flow

1. **VS Code MCP Agent Stop** → Docker sends SIGTERM to container
2. **Tini Init System** → Properly forwards signal to .NET application
3. **PosixSignalRegistration** → Catches SIGTERM and triggers cancellation
4. **Cancellation Token** → Propagates through all application components
5. **Graceful Shutdown** → MCP server stops, container exits cleanly

### Code Changes

- `Program.cs`: Added `System.Runtime.InteropServices` and `PosixSignalRegistration`
- `Services/McpServer.cs`: Enhanced cancellation handling and stdin monitoring
- `Dockerfile`: Added tini installation and optimized entrypoint
- `.vscode/mcp.json`: Updated Docker run configuration

## Testing Results

✅ Container starts successfully  
✅ MCP server responds to JSON-RPC requests  
✅ Automatic shutdown on VS Code MCP agent stop  
✅ No manual Ctrl+C required  
✅ Proper signal handling and cleanup  
✅ Compatible with VS Code MCP agent expectations

## Usage

The container is now ready for VS Code MCP agent integration. When VS Code starts the MCP agent, it will:

1. Start the Docker container automatically
2. Establish stdio communication
3. Process MCP requests normally
4. **Automatically stop the container when VS Code closes or the MCP agent is stopped**

## Files Modified

- `Program.cs` - Enhanced signal handling
- `Dockerfile` - Added tini and optimized configuration
- `Services/McpServer.cs` - Improved cancellation handling
- `.vscode/mcp.json` - Updated Docker run parameters
- `test-scripts/` - Added validation tests

The solution eliminates the manual intervention requirement and provides a seamless experience for VS Code MCP agent integration.
