# Running External Processes from Tools

This page shows how to call `UtilityService.RunProcessAsync` from tools and services in this repository.

## Goal

Provide a small, safe example for tool authors to run external processes (for example `git fetch`) using the service layer (`IUtilityService`).

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

File: `Documentation/RUN_PROCESS_EXAMPLES.md` — add or link this page from other docs as needed.
