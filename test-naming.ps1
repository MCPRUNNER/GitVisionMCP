#!/usr/bin/env pwsh

# Simple test to verify no container naming conflicts during restart
Write-Host "=== GitVisionMCP Container Naming Test ===" -ForegroundColor Green
Write-Host "Testing rapid container starts to simulate VS Code MCP restart behavior"

# Clean up any existing containers
Write-Host "Cleaning up existing containers..." -ForegroundColor Yellow
docker container prune -f | Out-Null

Write-Host "Running 3 rapid container starts..." -ForegroundColor Cyan

$successCount = 0
for ($i = 1; $i -le 3; $i++) {
    Write-Host "Starting container $i..." -ForegroundColor Yellow
    
    # Start container in background and immediately stop it
    $containerId = docker run --rm -d --init --stop-timeout 10 -v "${PWD}:/app/repo:ro" -v "${PWD}/logs:/app/logs" -w "/app/repo" mcprunner/gitvisionmcp:latest
    
    if ($containerId) {
        Write-Host "✅ Container $i started successfully (ID: $($containerId.Substring(0,12)))" -ForegroundColor Green
        $successCount++
        
        # Stop the container
        docker stop $containerId | Out-Null
    } else {
        Write-Host "❌ Container $i failed to start" -ForegroundColor Red
    }
    
    # Small delay between tests
    Start-Sleep -Milliseconds 100
}

Write-Host ""
Write-Host "=== Test Results ===" -ForegroundColor Green
Write-Host "Total tests: 3"
Write-Host "Successful: $successCount"
Write-Host "Failed: $(3 - $successCount)"

if ($successCount -eq 3) {
    Write-Host "🎉 All tests passed! No container naming conflicts detected." -ForegroundColor Green
} else {
    Write-Host "⚠️  Some tests failed. Check Docker configuration." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "=== Final Container Status ===" -ForegroundColor Cyan
$runningContainers = docker ps --filter "ancestor=mcprunner/gitvisionmcp:latest" --format "table {{.Names}}\t{{.Status}}"
if ($runningContainers -eq "NAMES	STATUS") {
    Write-Host "✅ No containers running - cleanup successful" -ForegroundColor Green
} else {
    Write-Host "Running containers:"
    Write-Host $runningContainers
}

Write-Host ""
Write-Host "GitVisionMCP container naming test complete!" -ForegroundColor Green
