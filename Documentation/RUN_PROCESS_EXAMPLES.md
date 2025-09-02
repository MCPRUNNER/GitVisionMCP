# Running External Processes from Tools

This page shows how to call `UtilityService.RunProcessAsync` from tools and services in this repository, as well as how to use the new MCP tool `gv_run_process` to run processes directly.

## Goal

Provide a small, safe example for tool authors to run external processes (for example `git fetch`) using the service layer (`IUtilityService`) or the MCP tool.

## Dependency injection (constructor)

Inject `IUtilityService` into your tool or service via constructor injection.

```csharp
// Example tool or service class
public class MyTool
{
    private readonly IUtilityService _utilityService;
    private readonly ILogger<MyTool> _logger;

    public MyTool(IUtilityService utilityService, ILogger<MyTool> logger)
    {
        _utilityService = utilityService;
        _logger = logger;
    }

    public async Task RunFetchAsync(string repositoryPath)
    {
        // Simple fetch using the system git in the repository working directory
        var (success, stdout, stderr, exitCode) = await _utilityService.RunProcessAsync(
            repositoryPath,
            "git",
            "fetch origin --no-tags",
            timeoutMs: 60_000
        );

        if (!success || exitCode != 0)
        {
            _logger.LogWarning("git fetch failed (code={ExitCode}) stderr={Stderr}", exitCode, stderr);
            return;
        }

        _logger.LogInformation("git fetch completed stdout={Stdout}", stdout);
    }
}
```

## Passing an authorization token (example)

If you need to provide a bearer token for HTTPS fetches, prefer using environment variables or Git credential helpers rather than embedding secrets in command-line arguments. Example using `http.extraheader` (works but exposes token to process listings on some platforms):

```csharp
var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
if (!string.IsNullOrEmpty(token))
{
    // Note: escape quotes correctly for the argument string
    var args = $"-c http.extraheader=\"Authorization: bearer {token}\" fetch origin --no-tags";
    var (success, stdout, stderr, exitCode) = await _utilityService.RunProcessAsync(repositoryPath, "git", args);
    // handle result
}
```

Security note: putting a token on the command line may expose it to other processes on the same machine via process listings. For higher security, configure a credential helper or use a secure mechanism to provide credentials.

## Error handling pattern

- Check `Success` and `ExitCode`.
- Log `StdErr` on failure.
- Consider retry/backoff for transient network errors.

## PowerShell quick test (developer)

To reproduce locally from PowerShell (not from inside the tool), run in the repository folder:

```powershell
# run a git fetch in the current directory and show output
git fetch origin --no-tags 2>&1 | Out-String
```

If you need to add a token for a one-off test (PowerShell):

```powershell
$env:GITHUB_TOKEN = 'ghp_...'
git -c http.extraheader="Authorization: bearer $env:GITHUB_TOKEN" fetch origin --no-tags
# then remove the env var
Remove-Item Env:\GITHUB_TOKEN
```

## Notes and best practices

- Prefer `IUtilityService.RunProcessAsync` over directly starting processes in tools; it centralizes logging and timeouts.
- Avoid passing secrets in plain arguments if possible.
- Keep timeouts conservative but reasonable for network operations (e.g., 30–120 seconds depending on repo size).

---

# Using the MCP Tool

GitVisionMCP now provides a dedicated MCP tool for running external processes directly:

## MCP Tool: gv_run_process

The MCP tool allows you to execute external processes and capture their output directly from GitVisionMCP.

### Parameters

| Parameter              | Type                       | Description                                         | Default            |
| ---------------------- | -------------------------- | --------------------------------------------------- | ------------------ |
| `workingDirectory`     | string                     | The working directory for the process               | (Required)         |
| `fileName`             | string                     | The name or path of the process to run              | (Required)         |
| `arguments`            | string                     | The command line arguments to pass to the process   | (Required)         |
| `timeoutMs`            | int                        | The timeout in milliseconds                         | 60000 (60 seconds) |
| `environmentVariables` | Dictionary<string, string> | Optional dictionary of environment variables to set | null               |

### Return Value

The tool returns a dictionary with the following properties:

| Property   | Type    | Description                                                  |
| ---------- | ------- | ------------------------------------------------------------ |
| `success`  | boolean | Indicates if the process executed successfully (exit code 0) |
| `stdout`   | string  | Standard output from the process                             |
| `stderr`   | string  | Standard error output from the process                       |
| `exitCode` | int     | The process exit code                                        |

### Usage Examples

#### Basic Example

```json
{
  "workingDirectory": "C:\\Projects\\MyApp",
  "fileName": "dotnet",
  "arguments": "build"
}
```

#### Example with Timeout

```json
{
  "workingDirectory": "C:\\Projects\\MyApp",
  "fileName": "dotnet",
  "arguments": "test",
  "timeoutMs": 120000
}
```

#### Example with Environment Variables

```json
{
  "workingDirectory": "C:\\Projects\\MyApp",
  "fileName": "npm",
  "arguments": "run build",
  "environmentVariables": {
    "NODE_ENV": "production",
    "DEBUG": "false"
  }
}
```

## Additional MCP Tools

GitVisionMCP also provides three additional MCP tools for managing environment variables and plugins:

### Run Plugin (gv_run_plugin)

- Parameter: `pluginName` (string) - The name of the plugin defined in `.gitvision/config.json`
- Returns: Dictionary containing success flag, output, and error information
- Purpose: Execute configured plugins for automation tasks

### Get Environment Variable (gv_get_environment_variable)

- Parameter: `variableName` (string) - The name of the environment variable
- Returns: The value of the environment variable, or null if not set

### Set Environment Variable (gv_set_environment_variable)

- Parameters:
  - `name` (string) - The name of the environment variable
  - `value` (string) - The value to set
- Returns: Boolean indicating success or failure

File: `Documentation/RUN_PROCESS_EXAMPLES.md` — add or link this page from other docs as needed.
