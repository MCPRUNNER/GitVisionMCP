#!/usr/bin/env pwsh

# Test script to verify Docker logging functionality
Write-Host "=== GitVisionMCP Docker Logging Test ===" -ForegroundColor Green
Write-Host "Testing log file creation and mounting in Docker container"

# Clean up existing logs
$logsDir = "logs"
if (Test-Path $logsDir) {
    Remove-Item "$logsDir/*" -Force -ErrorAction SilentlyContinue
    Write-Host "Cleaned up existing log files" -ForegroundColor Yellow
}

# Create logs directory if it doesn't exist
if (!(Test-Path $logsDir)) {
    New-Item -ItemType Directory -Path $logsDir -Force | Out-Null
    Write-Host "Created logs directory" -ForegroundColor Yellow
}

Write-Host "Running Docker container with log mounting..." -ForegroundColor Cyan

# Run the container with a more complex JSON-RPC request to generate more logging
$jsonRequest = @{
    jsonrpc = "2.0"
    id      = 1
    method  = "initialize"
    params  = @{
        protocolVersion = "2024-11-05"
        capabilities    = @{
            roots = @{
                listChanged = $false
            }
        }
        clientInfo      = @{
            name    = "test-logging-client"
            version = "1.0.0"
        }
    }
} | ConvertTo-Json -Depth 10

try {
    $result = $jsonRequest | docker run --rm -i --init --stop-timeout 10 -v "${PWD}:/app/repo:ro" -v "${PWD}/logs:/app/logs" -w "/app/repo" mcprunner/gitvisionmcp:latest
    
    Write-Host "Container output:" -ForegroundColor Green
    Write-Host $result
    
}
catch {
    Write-Host "Error running container: $_" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== Log File Analysis ===" -ForegroundColor Green

# Check for log files
$logFiles = Get-ChildItem -Path $logsDir -Filter "*.log" -ErrorAction SilentlyContinue

if ($logFiles.Count -eq 0) {
    Write-Host "❌ No log files found in $logsDir directory" -ForegroundColor Red
}
else {
    Write-Host "✅ Found $($logFiles.Count) log file(s):" -ForegroundColor Green
    foreach ($logFile in $logFiles) {
        Write-Host "  📄 $($logFile.Name) (Size: $($logFile.Length) bytes, Modified: $($logFile.LastWriteTime))" -ForegroundColor White
        
        if ($logFile.Length -gt 0) {
            Write-Host "  📝 Content:" -ForegroundColor Yellow
            Get-Content $logFile.FullName | ForEach-Object { Write-Host "    $_" -ForegroundColor Gray }
        }
        else {
            Write-Host "  ⚠️  File is empty" -ForegroundColor Yellow
        }
        Write-Host ""
    }
}

Write-Host "=== Summary ===" -ForegroundColor Green
if ($logFiles.Count -gt 0 -and ($logFiles | Where-Object { $_.Length -gt 0 })) {
    Write-Host "🎉 SUCCESS: Docker logging is working correctly!" -ForegroundColor Green
    Write-Host "   ✅ Logs are being written to the mounted /app/logs directory" -ForegroundColor Green
    Write-Host "   ✅ Log files are accessible from the host system" -ForegroundColor Green
}
else {
    Write-Host "❌ FAILED: Docker logging is not working properly" -ForegroundColor Red
    Write-Host "   Check Docker volume mount and logging configuration" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "GitVisionMCP Docker logging test complete!" -ForegroundColor Green
