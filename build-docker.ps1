#!/usr/bin/env pwsh
# build-docker.ps1 - Build the GitVisionMCP Docker image

Write-Host "Building GitVisionMCP Docker image..." -ForegroundColor Green
docker build -t gitvisionmcp:latest .

Write-Host "Docker image built successfully!" -ForegroundColor Green
Write-Host "You can now use the GitVisionMCP-Docker server configuration in your .vscode/mcp.json file."
