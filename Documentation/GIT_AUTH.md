# Git authentication for GitVisionMCP

This project uses LibGit2Sharp for in-process git operations and falls back to the `git` CLI when necessary (for example when the native libgit2 build doesn't support SSH transports).

Recommended approaches:

1. HTTPS + token (recommended for CI / automated runs)

- Create a Personal Access Token (PAT) with repo access on GitHub.
- Set the token in your environment before running GitVisionMCP.

Windows PowerShell (single session):

```powershell
$env:GITHUB_TOKEN = "<your_token_here>"
# Run the server in the same shell so it inherits the variable
dotnet run --project .\GitVisionMCP.csproj
```

Or set an environment variable persistently (example for the current user):

```powershell
setx GITHUB_TOKEN "<your_token_here>"
# New shells will see the variable
```

The server will use this token for HTTPS fetches and for the CLI fallback by passing an Authorization header to `git`.

2. SSH remotes

If your remote uses SSH (`git@github.com:owner/repo.git`) the embedded libgit2 may not support SSH in your environment. In that case either:

- Use the CLI fallback (ensure `git` is available on PATH and you have SSH keys configured), or
- Switch the remote to HTTPS and use a token (preferred for automation):

```powershell
git remote set-url origin https://github.com/<owner>/<repo>.git
```

3. Troubleshooting

- If fetches fail, check the server logs for errors and the CLI fallback output.
- Ensure `git` is installed and accessible on PATH for the fallback to work.

Security note: treat tokens as secrets. Do not hard-code tokens in source or commit them to the repository.
