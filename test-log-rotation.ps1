#!/usr/bin/env pwsh

# test-log-rotation.ps1 - Test log rotation and timestamped filenames

Write-Host "Testing log rotation and timestamped filenames..." -ForegroundColor Green

# Clean up any existing containers
Write-Host "Cleaning up existing containers..." -ForegroundColor Yellow
docker ps -a --filter="ancestor=mcprunner/gitvisionmcp:latest" --format "table {{.ID}}\t{{.Status}}" | Select-Object -Skip 1 | ForEach-Object {
    if ($_ -match "^(\w+)") {
        $containerId = $matches[1]
        Write-Host "Stopping and removing container: $containerId" -ForegroundColor Yellow
        docker stop $containerId 2>$null
        docker rm $containerId 2>$null
    }
}

# Remove old logs to test clean
Write-Host "Cleaning up old logs..." -ForegroundColor Yellow
Remove-Item -Path "logs\gitvisionmcp*.log" -Force -ErrorAction SilentlyContinue

# Build the Docker image
Write-Host "Building Docker image..." -ForegroundColor Cyan
docker build -t mcprunner/gitvisionmcp:latest .

if ($LASTEXITCODE -ne 0) {
    Write-Host "Docker build failed!" -ForegroundColor Red
    exit 1
}

# Test 1: Run container and check for timestamped log creation
Write-Host "`nTest 1: Check timestamped log file creation..." -ForegroundColor Green

# Start container in background
Write-Host "Starting GitVision MCP container..." -ForegroundColor Cyan
$containerProcess = Start-Process -FilePath "docker" -ArgumentList @(
    "run", "-i", "--init", "--rm", 
    "-v", "$PWD/logs:/app/logs", 
    "-e", "DOTNET_ENVIRONMENT=Production", 
    "mcprunner/gitvisionmcp:latest"
) -PassThru -NoNewWindow

# Wait a moment for startup
Start-Sleep -Seconds 3

# Send initialize request
Write-Host "Sending initialize request..." -ForegroundColor Cyan
$initRequest = @{
    "jsonrpc" = "2.0"
    "id"      = 1
    "method"  = "initialize"
    "params"  = @{
        "protocolVersion" = "2024-11-05"
        "capabilities"    = @{
            "roots"    = @{
                "listChanged" = $true
            }
            "sampling" = @{}
        }
        "clientInfo"      = @{
            "name"    = "Test Client"
            "version" = "1.0.0"
        }
    }
} | ConvertTo-Json -Depth 10

# Send the request via stdin
$initRequest | docker exec -i $containerProcess.Id /bin/bash -c "cat"

# Wait for processing
Start-Sleep -Seconds 2

# Stop the container
Write-Host "Stopping container..." -ForegroundColor Yellow
$containerProcess.Kill()
Start-Sleep -Seconds 2

# Check for timestamped log file
Write-Host "Checking for timestamped log files..." -ForegroundColor Green
$logFiles = Get-ChildItem -Path "logs" -Filter "gitvisionmcp*.log" | Sort-Object Name

if ($logFiles.Count -gt 0) {
    Write-Host "✓ Found timestamped log files:" -ForegroundColor Green
    $logFiles | ForEach-Object {
        Write-Host "  - $($_.Name) (Size: $($_.Length) bytes)" -ForegroundColor White
        if ($_.Length -gt 0) {
            Write-Host "  - Content preview:" -ForegroundColor Gray
            Get-Content $_.FullName -Head 5 | ForEach-Object { Write-Host "    $_" -ForegroundColor Gray }
        }
    }
}
else {
    Write-Host "✗ No timestamped log files found!" -ForegroundColor Red
}

# Test 2: Run multiple times to test log rotation
Write-Host "`nTest 2: Testing log rotation behavior..." -ForegroundColor Green

for ($i = 1; $i -le 3; $i++) {
    Write-Host "Running container instance $i..." -ForegroundColor Cyan
    
    # Start container
    $containerProcess = Start-Process -FilePath "docker" -ArgumentList @(
        "run", "-i", "--init", "--rm", 
        "-v", "$PWD/logs:/app/logs", 
        "-e", "DOTNET_ENVIRONMENT=Production", 
        "mcprunner/gitvisionmcp:latest"
    ) -PassThru -NoNewWindow
    
    # Wait and send request
    Start-Sleep -Seconds 2
    
    # Send multiple requests to generate more log entries
    for ($j = 1; $j -le 3; $j++) {
        # Generate log entries by letting the container run
        # Each startup/shutdown cycle will create log entries
        Start-Sleep -Seconds 1
    }
    
    Start-Sleep -Seconds 2
    
    # Stop container
    $containerProcess.Kill()
    Start-Sleep -Seconds 1
    
    Write-Host "Instance $i completed" -ForegroundColor White
}

# Final check of all log files
Write-Host "`nFinal log file summary:" -ForegroundColor Green
$allLogFiles = Get-ChildItem -Path "logs" -Filter "gitvisionmcp*.log" | Sort-Object Name

if ($allLogFiles.Count -gt 0) {
    Write-Host "✓ Total log files created: $($allLogFiles.Count)" -ForegroundColor Green
    $allLogFiles | ForEach-Object {
        Write-Host "  - $($_.Name) (Size: $($_.Length) bytes, Modified: $($_.LastWriteTime))" -ForegroundColor White
    }
}
else {
    Write-Host "✗ No log files found!" -ForegroundColor Red
}

# Check log content quality
Write-Host "`nLog content analysis:" -ForegroundColor Green
$allLogFiles | ForEach-Object {
    $content = Get-Content $_.FullName -ErrorAction SilentlyContinue
    if ($content) {
        $timestampPattern = '^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3}'
        $timestampedLines = $content | Where-Object { $_ -match $timestampPattern }
        Write-Host "  - $($_.Name): $($content.Count) lines, $($timestampedLines.Count) with timestamps" -ForegroundColor White
    }
}

Write-Host "`nLog rotation test completed!" -ForegroundColor Green
Write-Host "Note: Daily rotation will occur when logs span multiple days." -ForegroundColor Yellow
Write-Host "File size rotation will occur when logs exceed 10MB." -ForegroundColor Yellow
