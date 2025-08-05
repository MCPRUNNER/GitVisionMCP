# GitServiceTools vs McpHandler - Architecture Documentation

## Overview

This document explains the functional differences between `GitServiceTools` and `McpHandler` in the GitVisionMCP project. These components serve distinct but complementary roles in the Model Context Protocol (MCP) server architecture.

## Architectural Relationship

```mermaid
graph TB
    subgraph "Client Layer (mcp.json)"
        VSCode[STDIO Configuration<br/>command line run]
        HTTPClient[HTTP Configuration<br/>url http\://ip\:port\/mcp]
    end

    subgraph "Transport Layer"
        STDIO[STDIO Transport]
        HTTP[HTTP Transport]
    end

    subgraph "Protocol Layer"
        McpHandler[McpHandler<br/>JSON-RPC Handler]
        McpFramework[MCP Framework<br/>Built-in Routing]
    end

    subgraph "Business Logic Layer"
        GitServiceTools[GitServiceTools<br/>Tool Implementations]
    end

    subgraph "Service Layer"
        GitService[GitService]
        FileService[FileService]
        LocationService[LocationService]
        DeconstructionService[DeconstructionService]
    end
    subgraph "Repository Layer"
        FileRepository[FileRepository]
        GitRepository[GitCommandRepository]
    end
    VSCode -.->|JSON-RPC over STDIO| STDIO
    HTTPClient -.->|JSON-RPC over HTTP| HTTP

    STDIO --> McpHandler
    HTTP --> McpFramework

    McpHandler --> GitServiceTools
    McpFramework --> GitServiceTools

    GitServiceTools --> GitService
    GitServiceTools --> FileService
    GitServiceTools --> LocationService
    GitServiceTools --> DeconstructionService
    FileService --> FileRepository
    GitService --> GitRepository


```

## Component Responsibilities

### GitServiceTools

**Purpose**: Business logic layer containing MCP tool implementations

**Key Characteristics**:

- **Transport Agnostic**: Works with both STDIO and HTTP transports
- **Business Logic**: Contains actual implementation of git operations, file operations, etc.
- **MCP Annotations**: Decorated with `[McpServerToolAttribute]` for automatic discovery
- **Service Dependencies**: Consumes other services (GitService, FileService, etc.)

```mermaid
classDiagram
    class GitServiceTools {
        <<Business Logic Layer>>
        +GenerateGitDocumentationAsync()
        +CompareBranchesDocumentationAsync()
        +GetRecentCommitsAsync()
        +ListWorkspaceFilesAsync()
        +SearchCommitsForStringAsync()
        +SearchJsonFileAsync()
        +DeconstructAsync()
        -ValidateOutputFormat()
        -EnsureDirectoryExists()
    }

    class IGitService {
        <<Interface>>
        +GetGitLogsAsync()
        +GenerateCommitDocumentationAsync()
        +FetchFromRemoteAsync()
    }

    class IFileService {
        <<Interface>>
        +GetWorkspaceRoot()
        +GetAllFilesAsync()
        +GetFileContentsAsync()
    }

    class ILocationService {
        <<Interface>>
        +SearchJsonFile()
        +SearchXmlFile()
        +GetAppVersion()
    }

    GitServiceTools --> IGitService
    GitServiceTools --> IFileService
    GitServiceTools --> ILocationService
```

### McpHandler

**Purpose**: STDIO-specific protocol handler for JSON-RPC communication

**Key Characteristics**:

- **Transport Specific**: Only used for STDIO transport
- **Protocol Handler**: Manages JSON-RPC message parsing and routing
- **No Business Logic**: Routes requests to GitServiceTools
- **Communication**: Handles STDIO input/output streams

```mermaid
classDiagram
    class McpHandler {
        <<Protocol Handler>>
        +StartAsync()
        +StopAsync()
        -ProcessRequestAsync()
        -HandleRequestAsync()
        -HandleInitializeAsync()
        -HandleToolsListAsync()
        -HandleToolCallAsync()
        -CreateErrorResponse()
        -SendErrorResponseAsync()
    }

    class JsonRpcRequest {
        +Id: object
        +Method: string
        +Params: object
    }

    class JsonRpcResponse {
        +Id: object
        +Result: object
        +Error: object
    }

    class CallToolRequest {
        +Name: string
        +Arguments: object
    }

    McpHandler --> JsonRpcRequest
    McpHandler --> JsonRpcResponse
    McpHandler --> CallToolRequest
    McpHandler --> GitServiceTools : routes to
```

## Transport-Specific Behavior

### STDIO Transport Flow

```mermaid
sequenceDiagram
    participant Client as VS Code/Copilot
    participant Handler as McpHandler
    participant Tools as GitServiceTools
    participant Service as GitService

    Client->>Handler: JSON-RPC via STDIO
    Handler->>Handler: Parse JSON-RPC Request
    Handler->>Handler: Route to Tool Method
    Handler->>Tools: Call Tool Method
    Tools->>Service: Execute Business Logic
    Service-->>Tools: Return Result
    Tools-->>Handler: Return Tool Response
    Handler->>Handler: Format JSON-RPC Response
    Handler->>Client: Send Response via STDIO
```

### HTTP Transport Flow

```mermaid
sequenceDiagram
    participant Client as HTTP Client
    participant Framework as MCP Framework
    participant Tools as GitServiceTools
    participant Service as GitService

    Client->>Framework: HTTP JSON-RPC Request
    Framework->>Framework: Parse & Route Request
    Framework->>Tools: Call Tool Method Directly
    Tools->>Service: Execute Business Logic
    Service-->>Tools: Return Result
    Tools-->>Framework: Return Tool Response
    Framework->>Framework: Format HTTP Response
    Framework->>Client: Send HTTP Response
```

## Tool Discovery Mechanisms

### STDIO Discovery (via McpHandler)

```mermaid
flowchart TD
    A[Client Requests tools/list] --> B[McpHandler.HandleToolsListAsync]
    B --> C[Hardcoded Tool Definitions]
    C --> D[Return Tool Schema]

    E[Client Calls Tool] --> F[McpHandler.HandleToolCallAsync]
    F --> G[Parse Tool Name & Arguments]
    G --> H[Route to GitServiceTools Method]
    H --> I[Execute Business Logic]
```

### HTTP Discovery (via MCP Framework)

```mermaid
flowchart TD
    A[Application Startup] --> B[Framework Scans Assemblies]
    B --> C[Find Classes with McpServerToolType]
    C --> D[Discover Methods with McpServerToolAttribute]
    D --> E[Build Tool Registry]

    F[Client Requests tools/list] --> G[Framework Returns Discovered Tools]

    H[Client Calls Tool] --> I[Framework Routes Directly to Method]
    I --> J[Execute Business Logic]
```

## Configuration Logic in Program.cs

```mermaid
flowchart TD
    A[Application Starts] --> B{Check GITVISION_MCP_TRANSPORT}

    B -->|"http"| C[Configure HTTP Transport]
    B -->|"stdio"| D[Configure STDIO Transport]
    B -->|"unset" or invalid| E[Default to STDIO]

    C --> F[builder.Services.AddMcpServer().WithHttpTransport()]
    C --> G[Add HTTP Middleware & Controllers]
    C --> H[app.MapMcp("/mcp")]
    C --> I[app.Run()]

    D --> J[builder.Services.AddMcpServer().WithStdioServerTransport()]
    E --> J
    J --> K[Register IMcpHandler as McpHandler]
    J --> L[Get IMcpServer Service]
    L --> M[await mcpServer.RunAsync()]

    style C fill:#e8f5e8
    style D fill:#fff2cc
    style F fill:#e8f5e8
    style J fill:#fff2cc
```

## Key Differences Summary

| Aspect                | GitServiceTools                                | McpHandler                               |
| --------------------- | ---------------------------------------------- | ---------------------------------------- |
| **Purpose**           | Business logic implementation                  | Protocol communication handler           |
| **Transport Support** | Both HTTP and STDIO                            | STDIO only                               |
| **Responsibilities**  | Tool functionality, validation, error handling | JSON-RPC parsing, routing, serialization |
| **Dependencies**      | Service layer (GitService, FileService, etc.)  | GitServiceTools for actual work          |
| **Discovery**         | MCP attributes for auto-discovery              | Manual tool registration                 |
| **Lifecycle**         | Transient per request                          | Singleton for application lifetime       |
| **Error Handling**    | Business logic errors                          | Protocol and communication errors        |

## Redundancy Analysis

**No Functional Redundancy Exists**:

1. **GitServiceTools** contains the actual business logic and tool implementations
2. **McpHandler** provides STDIO-specific communication protocol handling
3. **HTTP transport** bypasses McpHandler entirely and uses the MCP framework's built-in routing
4. **Both are necessary** for supporting multiple transport protocols

## Best Practices Observed

### GitServiceTools Implementation

- ✅ Comprehensive input validation with detailed error messages
- ✅ Structured logging with contextual information
- ✅ Proper exception handling with specific exception types
- ✅ XML documentation for all public methods
- ✅ Defensive programming with null checks and boundary validation

### McpHandler Implementation

- ✅ Protocol-specific error handling for JSON-RPC
- ✅ Proper resource management and cleanup
- ✅ Cancellation token support for graceful shutdown
- ✅ Separation of concerns between protocol and business logic

## Conclusion

GitServiceTools and McpHandler serve complementary but distinct roles in the MCP server architecture. GitServiceTools provides transport-agnostic business logic, while McpHandler provides STDIO-specific protocol handling. This separation enables the application to support multiple transport protocols efficiently without
