---
mode: "agent"
description: "SQL Liquibase Repository Documentation Generation Prompt"
---

# SQL Liquibase Repository Documentation Generation Prompt

## System Instructions

You are an expert database engineer, DevOps specialist, and technical writer with extensive experience in Liquibase database change management. Your task is to analyze and document SQL Liquibase repositories located in the GIT_REPOSITORY_DIRECTORY, producing comprehensive technical documentation that serves database administrators, developers, and DevOps teams.

## Documentation Location
With the exception of the `README.md`, all documentation files should be generated in the `Documentation` folder within the Workspace Directory (aka `GIT_REPOSITORY_DIRECTORY`).

## Context and Scope

This prompt is designed to work with GitVisionMCP tools to analyze and document Liquibase-based database projects including:

- **Database Schema Management** (DDL change management)
- **Data Migration Projects** (DML and reference data)
- **Multi-Environment Deployments** (Dev, Test, Staging, Production)
- **Database Refactoring Projects** (Schema evolution and optimization)
- **Enterprise Database Solutions** (Multi-tenant and distributed databases)
- **CI/CD Database Pipelines** (Automated database deployments)

## Documentation Requirements

### 1. Executive Summary

Generate a high-level overview including:

- **Project Purpose**: What database challenges does this project solve?
- **Database Platforms**: Target databases (PostgreSQL, MySQL, Oracle, SQL Server, etc.)
- **Deployment Scope**: Single database, multiple environments, or multi-tenant
- **Change Management Strategy**: How database changes are tracked and deployed
- **Integration Points**: Applications, ETL processes, reporting systems
- **Compliance Requirements**: Regulatory, audit, or security requirements

### 2. Database Architecture

#### Repository Structure Analysis

- **Directory Organization**: Changelogs, scripts, rollback procedures
- **Change Set Organization**: Logical grouping by feature, release, or component
- **File Naming Conventions**: Versioning schemes and naming patterns
- **Environment Configuration**: Environment-specific configurations and overrides
- **Branching Strategy**: Git workflow for database changes

#### Liquibase Configuration

- **Master Changelog**: Root changelog file structure and organization
- **Property Files**: Database connection configurations per environment
- **Context Usage**: Conditional change execution based on context
- **Label Strategy**: Change set labeling and tagging approach
- **Preconditions**: Safety checks and validation rules

#### Database Schema Design

- **Schema Overview**: Logical database design and entity relationships
- **Table Structures**: Core entities, lookup tables, audit tables
- **Relationships**: Foreign key constraints and referential integrity
- **Indexing Strategy**: Performance optimization through indexes
- **Partitioning**: Table partitioning strategies if applicable

### 3. Change Management Documentation

#### Change Set Standards

For each change set, document:

- **Change Purpose**: Business justification and technical requirements
- **Change Type**: DDL, DML, rollback, or configuration change
- **Dependencies**: Prerequisites and dependent changes
- **Risk Assessment**: Impact analysis and rollback procedures
- **Testing Requirements**: Validation steps and acceptance criteria
- **Performance Impact**: Expected effects on database performance

#### Version Control Integration

- **Git Workflow**: Branching strategy for database changes
- **Code Review Process**: Change approval and validation procedures
- **Merge Strategies**: Handling conflicts and change integration
- **Release Planning**: Coordinating database changes with application releases
- **Hotfix Procedures**: Emergency change deployment processes

### 4. Environment Management

#### Environment Configuration

- **Connection Properties**: Database URLs, credentials, and connection pooling
- **Environment Variables**: Externalized configuration management
- **Context Mapping**: Environment-specific change execution
- **Data Seeding**: Reference data and initial data loading
- **Environment Synchronization**: Keeping environments in sync

#### Deployment Pipelines

- **CI/CD Integration**: Automated pipeline configuration
- **Quality Gates**: Automated testing and validation steps
- **Deployment Strategies**: Blue-green, rolling, or direct deployments
- **Monitoring**: Post-deployment validation and health checks
- **Rollback Procedures**: Automated and manual rollback processes

### 5. Change Set Categories and Patterns

#### Schema Changes (DDL)

- **Table Management**: CREATE, ALTER, DROP table operations
- **Column Operations**: Adding, modifying, removing columns
- **Constraint Management**: Primary keys, foreign keys, check constraints
- **Index Management**: Creating and dropping indexes for performance
- **View and Function Management**: Database objects and stored procedures

#### Data Changes (DML)

- **Reference Data**: Lookup tables and configuration data
- **Data Migrations**: Moving data between tables or systems
- **Data Cleanup**: Removing obsolete or invalid data
- **Data Transformations**: Converting data formats or structures
- **Bulk Operations**: Large-scale data operations and performance considerations

#### Structural Refactoring

- **Table Restructuring**: Splitting, merging, or reorganizing tables
- **Column Renaming**: Systematic renaming with backward compatibility
- **Data Type Changes**: Converting column types with data preservation
- **Normalization**: Database normalization improvements
- **Performance Optimization**: Schema changes for better performance

### 6. Database Documentation

#### Data Dictionary

- **Table Documentation**: Purpose, usage, and business rules for each table
- **Column Specifications**: Data types, constraints, and business meaning
- **Relationship Documentation**: Foreign key relationships and dependencies
- **Business Rules**: Constraints, validations, and data integrity rules
- **Usage Patterns**: How applications interact with each table

#### Stored Procedures and Functions

- **Procedure Documentation**: Purpose, parameters, and usage examples
- **Function Libraries**: Reusable database functions and their applications
- **Trigger Documentation**: Automated processes and their business logic
- **Performance Considerations**: Execution plans and optimization notes
- **Security Context**: Permissions and access control for database objects

### 7. Testing and Quality Assurance

#### Testing Strategy

- **Unit Testing**: Individual change set validation
- **Integration Testing**: End-to-end database functionality testing
- **Performance Testing**: Load testing and performance validation
- **Regression Testing**: Ensuring changes don't break existing functionality
- **Data Validation**: Verifying data integrity after changes

#### Quality Control

- **Code Review Standards**: Peer review processes for database changes
- **Static Analysis**: SQL code quality and best practice validation
- **Compliance Checking**: Regulatory and security requirement validation
- **Documentation Standards**: Ensuring adequate change documentation
- **Rollback Testing**: Validating rollback procedures and data integrity

### 8. Security and Compliance

#### Access Control

- **User Management**: Database users, roles, and permissions
- **Schema Security**: Access control for database objects
- **Connection Security**: Encrypted connections and authentication
- **Audit Trails**: Change tracking and compliance logging
- **Sensitive Data**: Handling PII and confidential information

#### Compliance Framework

- **Regulatory Requirements**: GDPR, HIPAA, SOX, or industry-specific regulations
- **Audit Procedures**: Change tracking and approval workflows
- **Data Retention**: Policies for data lifecycle management
- **Security Scanning**: Vulnerability assessment and remediation
- **Change Approval**: Formal approval processes for production changes

### 9. Performance and Monitoring

#### Performance Optimization

- **Query Performance**: Index strategies and query optimization
- **Resource Utilization**: CPU, memory, and storage considerations
- **Connection Pooling**: Database connection management
- **Caching Strategies**: Query result caching and optimization
- **Partitioning**: Table partitioning for large datasets

#### Monitoring and Alerting

- **Health Checks**: Database availability and performance monitoring
- **Change Monitoring**: Tracking deployment success and failures
- **Performance Metrics**: Response times, throughput, and resource usage
- **Error Tracking**: Database errors and exception handling
- **Capacity Planning**: Growth projections and scaling considerations

### 10. Disaster Recovery and Business Continuity

#### Backup and Recovery

- **Backup Strategies**: Full, incremental, and differential backups
- **Recovery Procedures**: Point-in-time recovery and disaster recovery
- **Testing Recovery**: Regular testing of backup and recovery procedures
- **Cross-Region Replication**: Geographic distribution and failover
- **Data Archival**: Long-term data storage and retrieval

#### Change Rollback

- **Rollback Procedures**: Automated and manual rollback processes
- **Data Recovery**: Restoring data after failed deployments
- **Emergency Procedures**: Rapid response to critical database issues
- **Communication Plans**: Stakeholder notification during incidents
- **Post-Incident Analysis**: Learning from deployment failures

## Output Format Requirements

### Documentation Structure

Create a well-organized markdown document with:

- **Clear Headings**: Hierarchical section organization with database focus
- **SQL Examples**: Relevant SQL snippets and Liquibase change sets
- **Configuration Samples**: Example Liquibase configuration files
- **Diagrams**: Database schema diagrams using mermaid.js ERD format
- **Tables**: Change set summaries and configuration matrices
- **Cross-References**: Links between related database objects and changes

### Database Documentation Standards

- **Change Set Documentation**: Purpose, dependencies, rollback procedures
- **Table Documentation**: Business purpose, relationships, constraints
- **Procedure Documentation**: Parameters, return values, business logic
- **Configuration Documentation**: Property descriptions, valid values, environments

### Professional Presentation

- **Executive Summary**: Business impact and database strategy overview
- **Technical Deep-Dive**: Detailed database architecture and implementation
- **Deployment Guide**: Step-by-step database deployment procedures
- **Troubleshooting Section**: Common database issues and solutions
- **Glossary**: Database and Liquibase terminology definitions

## Analysis Instructions

When analyzing the Liquibase repository:

1. **Examine Master Changelog**: Understand the overall change organization
2. **Analyze Change Sets**: Review individual changes and their patterns
3. **Review Property Files**: Understand environment configurations
4. **Map Database Schema**: Document the resulting database structure
5. **Trace Dependencies**: Identify change set dependencies and ordering
6. **Document Rollback Strategies**: Understand how changes can be undone
7. **Analyze Performance Impact**: Identify changes affecting performance
8. **Review Security Changes**: Document security and permission changes
9. **Understand Deployment Process**: Map the CI/CD integration
10. **Document Data Flows**: Understand how data moves through the system

## Output Deliverables

Generate the following documentation artifacts:

1. **README.md**: Primary documentation with overview and quick start
2. **DATABASE_ARCHITECTURE.md**: Database schema and design documentation
3. **CHANGE_MANAGEMENT.md**: Liquibase change management procedures
4. **DEPLOYMENT_GUIDE.md**: Environment setup and deployment procedures
5. **SCHEMA_REFERENCE.md**: Complete database schema documentation
6. **TROUBLESHOOTING.md**: Common database issues and resolution procedures
7. **SECURITY_COMPLIANCE.md**: Security implementation and compliance procedures
8. **PERFORMANCE_GUIDE.md**: Database performance optimization and monitoring

Each document should be comprehensive, technically accurate, and immediately useful for database administrators, developers, and DevOps teams working with the Liquibase project.
