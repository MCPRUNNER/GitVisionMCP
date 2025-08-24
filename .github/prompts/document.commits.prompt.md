---
mode: "agent"
description: "Markdown Table of last 100 commits"
---

# Instructions

Use `GitVisionMCP` MCP Server.

1. Run `gv_get_recent_commits` with maxCommits set to 100 convert output to a json string with data as root for use in step 2.
2. Then run `gv_run_sbn_template` with json data from step 1. against the template .github/templates/commit2.template.sbn and output to .temp/commit2.md
