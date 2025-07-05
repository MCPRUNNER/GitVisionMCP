# Dockerfile for GitVisionMCP (MCP Copilot Agent)
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "GitVisionMCP.csproj"
RUN dotnet publish "GitVisionMCP.csproj" -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install tini for proper signal handling and git
RUN apt-get update && apt-get install -y tini git && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
# Copy configuration files
COPY appsettings*.json ./
COPY mcp.json ./
COPY .gitignore ./
# Create logs directory
RUN mkdir -p /app/logs
RUN mkdir -p /app/repo

ENV DOTNET_ENVIRONMENT=Production
ENV GIT_REPO_DIRECTORY=/app/repo
# Configure tini to act as a child subreaper to avoid warnings
ENV TINI_SUBREAPER=1

WORKDIR /app/repo

# Use tini with -s flag for child subreaper functionality
ENTRYPOINT ["/usr/bin/tini", "-s", "--", "dotnet", "../GitVisionMCP.dll"]
