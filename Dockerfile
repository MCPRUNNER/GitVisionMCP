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
RUN apt-get update && apt-get install -y git && rm -rf /var/lib/apt/lists/*
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

WORKDIR /app/repo
ENTRYPOINT ["dotnet", "../GitVisionMCP.dll"]
