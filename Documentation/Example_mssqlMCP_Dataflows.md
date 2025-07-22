# SQL Server MCP Application - Mermaid Dataflows

Based on the analysis of the C# files from the mssqlMCP project, here are the key dataflow diagrams:

## 1. Overall System Architecture

```mermaid
graph TB
    A[Client Request] --> B[MCP Server]
    B --> C{Request Type}

    C -->|Connection Management| D[ConnectionManager]
    C -->|Database Queries| E[DatabaseMetadataProvider]
    C -->|Security Operations| F[ApiKeyManager]
    C -->|SSIS Operations| G[DatabaseMetadataProvider.Ssis]
    C -->|Azure DevOps| H[DatabaseMetadataProvider.AzureDevOps]

    D --> I[ConnectionRepository]
    D --> J[EncryptionService]

    E --> K[SQL Server Database]
    F --> L[ApiKeyRepository]
    G --> M[SSIS Catalog]
    H --> N[Azure DevOps API]

    I --> O[(SQLite Storage)]
    J --> P[Key Rotation Service]
    L --> O

    K --> Q[Database Metadata]
    M --> R[SSIS Package Info]
    N --> S[Work Items & Builds]

    Q --> T[Formatted Response]
    R --> T
    S --> T
    T --> B
    B --> U[Client Response]
```

## 2. Connection Management Flow

```mermaid
graph LR
    A[Add Connection Request] --> B[ConnectionManager]
    B --> C[Input Validation]
    C --> D[Connection String Provider]
    D --> E[Encryption Service]
    E --> F[ConnectionRepository]
    F --> G[(SQLite DB)]

    H[Test Connection] --> I[ConnectionManager]
    I --> J[Decrypt Connection String]
    J --> K[SQL Server]
    K --> L{Connection Result}
    L -->|Success| M[Return Success]
    L -->|Failure| N[Return Error]

    O[List Connections] --> P[ConnectionRepository]
    P --> Q[Decrypt Connection Strings]
    Q --> R[Return Connection List]
```

## 3. Database Metadata Retrieval Flow

```mermaid
graph TB
    A[Metadata Request] --> B[DatabaseMetadataProvider]
    B --> C{Metadata Type}

    C -->|Tables & Columns| D[GetTableMetadata]
    C -->|Functions & Procedures| E[GetDatabaseObjects]
    C -->|SSIS Packages| F[DatabaseMetadataProvider.Ssis]
    C -->|Azure DevOps| G[DatabaseMetadataProvider.AzureDevOps]

    D --> H[Query sys.tables]
    D --> I[Query sys.columns]
    D --> J[Query sys.foreign_keys]

    E --> K[Query sys.objects]
    E --> L[Query sys.sql_modules]

    F --> M[Query SSISDB catalog]
    F --> N[Get Package Details]

    G --> O[Azure DevOps REST API]
    G --> P[Get Projects & Repos]

    H --> Q[TableInfo Models]
    I --> Q
    J --> Q

    K --> R[Database Object Models]
    L --> R

    M --> S[SSIS Catalog Models]
    N --> S

    O --> T[Azure DevOps Models]
    P --> T

    Q --> U[JSON Response]
    R --> U
    S --> U
    T --> U
```

## 4. Security & Authentication Flow

```mermaid
graph LR
    A[API Request] --> B[ApiKeyAuthMiddleware]
    B --> C[Extract API Key]
    C --> D[ApiKeyManager]
    D --> E[ApiKeyRepository]
    E --> F[(SQLite DB)]
    F --> G{Key Valid?}

    G -->|Yes| H[Continue to Controller]
    G -->|No| I[Return 401 Unauthorized]

    J[Generate API Key] --> K[ApiKeyManager]
    K --> L[Generate Random Key]
    L --> M[Hash with Salt]
    M --> N[Store in Repository]

    O[Rotate Keys] --> P[KeyRotationService]
    P --> Q[Generate New Key]
    Q --> R[Update Repository]
    R --> S[Disable Old Key]
```

## 5. Encryption Service Flow

```mermaid
graph TB
    A[Connection String] --> B[EncryptionService]
    B --> C[Generate Salt]
    C --> D[Derive Key from Password]
    D --> E[AES Encryption]
    E --> F[Base64 Encode]
    F --> G[Encrypted String]

    H[Encrypted String] --> I[EncryptionService]
    I --> J[Base64 Decode]
    J --> K[Extract Salt]
    K --> L[Derive Key]
    L --> M[AES Decryption]
    M --> N[Original Connection String]

    O[Key Rotation Request] --> P[KeyRotationService]
    P --> Q[Generate New Key]
    Q --> R[Re-encrypt All Connections]
    R --> S[Update Storage]
    S --> T[Remove Old Key]
```

## 6. SSIS Integration Flow

```mermaid
graph LR
    A[SSIS Catalog Request] --> B[DatabaseMetadataProvider.Ssis]
    B --> C[Connect to SSISDB]
    C --> D[Query catalog.folders]
    D --> E[Query catalog.projects]
    E --> F[Query catalog.packages]
    F --> G[Query catalog.operations]

    G --> H[Build SsisCatalogInfo]
    H --> I[Include Execution History]
    I --> J[Include Environment Variables]
    J --> K[Return SSIS Data]

    L[Execute SSIS Package] --> M[SSIS Runtime]
    M --> N[Package Validation]
    N --> O[Parameter Binding]
    O --> P[Package Execution]
    P --> Q[Log to catalog.operations]
```

## 7. Data Models Relationship

```mermaid
erDiagram
    ConnectionEntry {
        string Name
        string EncryptedConnectionString
        string Description
        DateTime CreatedAt
        DateTime UpdatedAt
    }

    TableInfo {
        string SchemaName
        string TableName
        string TableType
        List-ColumnInfo Columns
        List-ForeignKeyInfo ForeignKeys
    }

    ColumnInfo {
        string ColumnName
        string DataType
        bool IsNullable
        bool IsIdentity
        bool IsPrimaryKey
        int MaxLength
    }

    ApiKeyEntry {
        string KeyId
        string HashedKey
        string Salt
        DateTime CreatedAt
        DateTime ExpiresAt
        bool IsActive
    }

    SsisCatalogInfo {
        List-SsisFolder Folders
        List-SsisProject Projects
        List-SsisPackage Packages
        List-SsisExecution Executions
    }

    AzureDevOpsInfo {
        List-Project Projects
        List-Repository Repositories
        List-WorkItem WorkItems
        List-Build Builds
    }

    ConnectionEntry ||--o{ TableInfo : "provides access to"
    TableInfo ||--o{ ColumnInfo : "contains"
    TableInfo ||--o{ ForeignKeyInfo : "has"
    ConnectionEntry ||--o{ SsisCatalogInfo : "can access"
    ConnectionEntry ||--o{ AzureDevOpsInfo : "can query"
```

## Key Components Summary

1. **ConnectionManager**: Handles database connection lifecycle
2. **DatabaseMetadataProvider**: Core service for retrieving database schema information
3. **EncryptionService**: Secures connection strings using AES encryption
4. **ApiKeyManager**: Handles API authentication and authorization
5. **SSIS Integration**: Provides access to SQL Server Integration Services catalog
6. **Azure DevOps Integration**: Connects to Azure DevOps for project information

The application follows a clean architecture pattern with clear separation of concerns, proper error handling, and comprehensive logging throughout the data flow.
