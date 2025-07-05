# Test script to verify automatic Docker container shutdown
# This simulates how VS Code MCP agent interacts with the container

Write-Host "Starting GitVisionMCP Docker container..."

# Start the container in the background
$job = Start-Job -ScriptBlock {
    docker run --rm -i --init --name gitvisionmcp-shutdown-test --stop-timeout 5 `
        -v "c:\Users\U00001\source\repos\MCP\GitVisionMCP:/app/repo:ro" `
        -v "c:\Users\U00001\source\repos\MCP\GitVisionMCP\logs:/app/logs" `
        -w "/app/repo" `
        mcprunner/gitvisionmcp:latest
}

# Wait a moment for the container to start
Start-Sleep -Seconds 2

# Send an initialize request
Write-Host "Sending initialize request..."
$initRequest = '{"jsonrpc": "2.0", "id": 1, "method": "initialize", "params": {"protocolVersion": "2024-11-05", "capabilities": {"roots": {"listChanged": false}}, "clientInfo": {"name": "TestClient", "version": "1.0.0"}}}'

# Send the request to the container
$initRequest | docker exec -i gitvisionmcp-shutdown-test /bin/bash -c "cat"

# Wait a bit more
Start-Sleep -Seconds 1

# Now stop the container via Docker signal (simulating VS Code stopping the MCP agent)
Write-Host "Stopping container via Docker signal..."
docker stop gitvisionmcp-shutdown-test

# Wait for the job to complete
Wait-Job $job

# Get the job results
$result = Receive-Job $job

Write-Host "Container output:"
Write-Host $result

# Cleanup
Remove-Job $job

Write-Host "Test completed. Container should have shut down gracefully."
