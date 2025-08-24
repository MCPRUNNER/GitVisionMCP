---
mode: "agent"
description: "ASP.NET Core API Documentation Generator"
---

# Instructions

Use `GitVisionMCP` MCP Server.

1. Run `gv_get_recent_commits` with maxCommits set to 100 and use output as a json string with data as root.
2. Then run `gv_run_sbn_template` with template .github/templates/commit2.template.sbn and output to .temp/commit2.md
