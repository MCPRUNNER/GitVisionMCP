#!/usr/bin/env pwsh

# Test script to simulate MCP server restart scenario
Write-Host "=== GitVisionMCP Restart Test ===" -ForegroundColor Green
Write-Host "Testing multiple rapid starts to simulate VS Code MCP restart behavior"

# Function to test MCP server startup
function Test-McpStartup {
    param([int]$TestNumber)
    
    Write-Host "Test ${TestNumber}: Starting MCP server..." -ForegroundColor Yellow
    
    # Start the container and send initialize request
    $initRequest = @{
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
                name    = "test-client"
                version = "1.0.0"
            }
        }
    } | ConvertTo-Json -Depth 10
    
    try {
        # Use the same arguments as in .vscode/mcp.json but with timeout
        $result = $initRequest | docker run --rm -i --init --stop-timeout 10 -v "${PWD}:/app/repo:ro" -v "${PWD}/logs:/app/logs" -w "/app/repo" mcprunner/gitvisionmcp:latest
        
        if ($result -match '"result"') {
            Write-Host "✅ Test ${TestNumber}: Success - MCP server responded correctly" -ForegroundColor Green
            return $true
        }
        else {
            Write-Host "❌ Test ${TestNumber}: Failed - No valid response" -ForegroundColor Red
            return $false
        }
    }
    catch {
        Write-Host "❌ Test ${TestNumber}: Error - $_" -ForegroundColor Red
        return $false
    }
}

# Test rapid restart scenario
$successCount = 0
$totalTests = 3

Write-Host "Running $totalTests rapid startup tests..." -ForegroundColor Cyan

for ($i = 1; $i -le $totalTests; $i++) {
    if (Test-McpStartup -TestNumber $i) {
        $successCount++
    }
    
    # Small delay between tests
    Start-Sleep -Milliseconds 500
}

Write-Host ""
Write-Host "=== Test Results ===" -ForegroundColor Green
Write-Host "Total tests: $totalTests"
Write-Host "Successful: $successCount"
Write-Host "Failed: $($totalTests - $successCount)"

if ($successCount -eq $totalTests) {
    Write-Host "🎉 All tests passed! MCP server handles restarts correctly." -ForegroundColor Green
}
else {
    Write-Host "⚠️  Some tests failed. Check configuration." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "=== Container Status ===" -ForegroundColor Cyan
docker ps -a --filter "ancestor=mcprunner/gitvisionmcp:latest" --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"

Write-Host ""
Write-Host "GitVisionMCP restart test complete!" -ForegroundColor Green
