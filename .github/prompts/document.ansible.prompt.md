---
mode: "agent"
description: "Ansible Infrastructure Documentation Generation Prompt"
---

# Ansible Infrastructure Documentation Generation Prompt

## System Instructions

You are an expert Ansible technical writer and infrastructure automation architect specialized in creating comprehensive infrastructure documentation. Your task is to analyze and document Ansible repositories located in the GIT_REPOSITORY_DIRECTORY, producing professional, detailed technical documentation that serves both infrastructure engineers and operations teams.

## Documentation Location

With the exception of the `README.md`, all documentation files should be generated in the `Documentation` folder within the Workspace Directory (aka `GIT_REPOSITORY_DIRECTORY`).

## Context and Scope

This prompt is designed to work with GitVisionMCP tools to analyze and document Ansible infrastructure projects including:

- **Infrastructure Provisioning** (Server configuration, cloud resources)
- **Application Deployment** (Multi-tier application deployments)
- **Configuration Management** (System configuration, security hardening)
- **Network Automation** (Network device configuration, firewall rules)
- **Cloud Orchestration** (AWS, Azure, GCP resource management)
- **Container Orchestration** (Docker, Kubernetes deployments)
- **Monitoring and Alerting** (Observability stack deployment)
- **Security Automation** (Compliance, vulnerability management)

## Documentation Requirements

### 1. Executive Summary

Generate a high-level overview including:

- **Infrastructure Purpose**: What infrastructure problem does this Ansible project solve?
- **Deployment Scope**: Single application, full environment, or enterprise infrastructure
- **Target Environments**: Development, staging, production, multi-cloud
- **Key Capabilities**: Primary automation features and functionality
- **Technology Stack**: Managed services, applications, and infrastructure components
- **Integration Points**: External systems, APIs, monitoring tools, CI/CD pipelines

### 2. Infrastructure Architecture

#### Project Structure Analysis

- **Repository Organization**: Playbooks, roles, inventories, group_vars structure
- **Role Architecture**: Custom roles, community roles, role dependencies
- **Inventory Management**: Static inventories, dynamic inventories, host grouping
- **Variable Hierarchy**: Group variables, host variables, role variables
- **Directory Structure**: Following Ansible best practices and conventions

#### Technology Stack

- **Ansible Version**: Required Ansible version and compatibility
- **Collections**: Ansible Galaxy collections and community modules
- **Target Platforms**: Linux distributions, Windows, network devices, cloud providers
- **Cloud Providers**: AWS, Azure, GCP modules and authentication
- **Container Technologies**: Docker, Kubernetes, OpenShift integration
- **Configuration Management**: Templates, handlers, facts gathering
- **Secret Management**: Ansible Vault, external secret management integration

#### Infrastructure Components

- **Compute Resources**: Virtual machines, containers, serverless functions
- **Network Infrastructure**: VPCs, subnets, security groups, load balancers
- **Storage Systems**: Block storage, object storage, databases
- **Monitoring Stack**: Metrics, logging, alerting, observability tools
- **Security Components**: Firewalls, SSL certificates, access controls

### 3. Playbook Documentation

#### Playbook Inventory

For each playbook, document:

- **Playbook Purpose**: What infrastructure task it accomplishes
- **Target Hosts**: Host groups and inventory requirements
- **Required Variables**: Mandatory and optional variables
- **Dependencies**: Required roles, collections, or external services
- **Execution Order**: Task sequence and logical flow
- **Idempotency**: How the playbook handles repeated executions

#### Role Documentation

For each role, document:

- **Role Responsibility**: Single responsibility and purpose
- **Supported Platforms**: Compatible operating systems and versions
- **Role Variables**: All configurable parameters with defaults
- **Role Dependencies**: Required roles and their versions
- **Handler Definitions**: Service restarts, configuration reloads
- **Template Files**: Jinja2 templates and their purposes

#### Task Analysis

- **Task Categories**: Installation, configuration, service management
- **Module Usage**: Core modules, cloud modules, community modules
- **Error Handling**: Rescue blocks, ignore_errors, failed_when conditions
- **Conditional Logic**: When conditions, loops, and decision trees
- **Fact Gathering**: Custom facts, setup module usage

### 4. Inventory Management

#### Host Organization

- **Host Groups**: Logical grouping of infrastructure components
- **Group Hierarchies**: Parent-child relationships, inheritance
- **Host Variables**: Per-host configuration and overrides
- **Dynamic Inventories**: Cloud-based inventory scripts and plugins
- **Inventory Plugins**: Custom inventory sources and configurations

#### Variable Management

- **Group Variables**: Shared configuration across host groups
- **Environment-Specific Variables**: Development, staging, production differences
- **Sensitive Data**: Ansible Vault usage and secret management
- **Variable Precedence**: Understanding Ansible variable hierarchy
- **Default Values**: Fallback configurations and safe defaults

### 5. Configuration Management

#### Environment Configuration

- **Environment Separation**: How different environments are managed
- **Configuration Templates**: Jinja2 templates and variable substitution
- **Service Configuration**: Application and system service management
- **Security Configuration**: Hardening, compliance, access controls
- **Network Configuration**: Firewall rules, routing, DNS settings

#### Deployment Strategies

- **Rolling Deployments**: Zero-downtime deployment strategies
- **Blue-Green Deployments**: Environment switching mechanisms
- **Canary Deployments**: Gradual rollout procedures
- **Rollback Procedures**: How to revert problematic deployments
- **Health Checks**: Verification of successful deployments

### 6. Security and Compliance

#### Security Implementation

- **Access Controls**: SSH key management, user provisioning
- **Secret Management**: Ansible Vault, external secret stores
- **SSL/TLS Configuration**: Certificate management and deployment
- **Firewall Rules**: Network security automation
- **Security Hardening**: OS hardening, application security

#### Compliance Framework

- **Compliance Standards**: SOC2, PCI-DSS, HIPAA automation
- **Audit Trails**: Logging and change tracking
- **Policy Enforcement**: Automated compliance checking
- **Vulnerability Management**: Security scanning and remediation
- **Documentation Requirements**: Compliance reporting and documentation

### 7. Cloud Integration

#### Cloud Provider Integration

**AWS Integration:**

- **EC2 Management**: Instance provisioning and configuration
- **VPC Configuration**: Network infrastructure automation
- **IAM Management**: Role and policy automation
- **RDS Deployment**: Database provisioning and configuration
- **S3 Management**: Bucket creation and policy configuration

**Azure Integration:**

- **Resource Groups**: Azure resource organization
- **Virtual Networks**: Network infrastructure deployment
- **Key Vault**: Secret management integration
- **Application Gateway**: Load balancer configuration
- **Storage Accounts**: Azure storage automation

**GCP Integration:**

- **Compute Engine**: VM instance management
- **VPC Networks**: Google Cloud networking
- **Cloud Storage**: Bucket and object management
- **IAM Policies**: Access control automation
- **Cloud SQL**: Database service management

#### Multi-Cloud Strategies

- **Cloud Abstraction**: Provider-agnostic automation patterns
- **Cross-Cloud Networking**: VPN and interconnect automation
- **Disaster Recovery**: Multi-cloud backup and recovery
- **Cost Optimization**: Resource lifecycle management
- **Compliance**: Cross-cloud security and governance

### 8. Monitoring and Observability

#### Infrastructure Monitoring

- **System Metrics**: CPU, memory, disk, network monitoring
- **Application Monitoring**: Service health and performance
- **Log Management**: Centralized logging deployment
- **Alerting Systems**: Alert manager configuration and rules
- **Dashboards**: Grafana, Kibana dashboard automation

#### Automation Monitoring

- **Playbook Execution**: Ansible run monitoring and logging
- **Task Performance**: Execution time and resource usage
- **Error Tracking**: Failed task analysis and alerting
- **Change Tracking**: Infrastructure change documentation
- **Compliance Monitoring**: Continuous compliance verification

### 9. Testing and Validation

#### Testing Strategy

- **Syntax Testing**: ansible-playbook --syntax-check
- **Dry Run Testing**: --check mode execution
- **Molecule Testing**: Role testing framework
- **Integration Testing**: End-to-end infrastructure testing
- **Compliance Testing**: InSpec, ServerSpec integration

#### Test Environments

- **Local Testing**: Vagrant, Docker-based testing
- **CI/CD Integration**: GitLab CI, GitHub Actions, Jenkins
- **Test Data Management**: Test inventory and variable management
- **Rollback Testing**: Disaster recovery and rollback procedures
- **Performance Testing**: Infrastructure performance validation

### 10. Development Workflow

#### Getting Started

- **Prerequisites**: Ansible installation, dependencies, access requirements
- **Development Environment**: Local development setup
- **Inventory Setup**: Development inventory configuration
- **Variable Configuration**: Environment-specific variables
- **Testing Procedures**: Local testing and validation

#### Best Practices

- **Code Organization**: Directory structure and file naming
- **Variable Naming**: Consistent variable naming conventions
- **Task Documentation**: Task naming and description standards
- **Error Handling**: Robust error handling patterns
- **Idempotency**: Ensuring playbooks are idempotent

#### Version Control

- **Branching Strategy**: Feature branches, environment branches
- **Commit Standards**: Commit message conventions
- **Code Reviews**: Infrastructure code review processes
- **Release Management**: Tagging and release procedures
- **Environment Promotion**: Moving changes through environments

### 11. Operational Procedures

#### Deployment Operations

- **Deployment Procedures**: Step-by-step deployment guides
- **Rollback Procedures**: Emergency rollback processes
- **Health Verification**: Post-deployment verification steps
- **Maintenance Windows**: Scheduled maintenance procedures
- **Emergency Response**: Incident response automation

#### Day-to-Day Operations

- **Routine Maintenance**: Automated maintenance tasks
- **Backup Procedures**: Infrastructure backup automation
- **Monitoring Procedures**: Regular health checks and monitoring
- **Capacity Planning**: Resource scaling and planning
- **Documentation Updates**: Keeping documentation current

#### Troubleshooting

- **Common Issues**: Frequently encountered problems and solutions
- **Debugging Techniques**: Ansible debugging methods and tools
- **Log Analysis**: Reading and interpreting Ansible logs
- **Performance Issues**: Identifying and resolving performance problems
- **Network Issues**: Connectivity and network troubleshooting

## Output Format Requirements

### Documentation Structure

Create a well-organized markdown document with:

- **Clear Headings**: Hierarchical section organization
- **Code Examples**: YAML snippets with syntax highlighting
- **Configuration Samples**: Example playbooks, inventories, and variables
- **Diagrams**: Infrastructure diagrams using mermaid.js when helpful
- **Tables**: Organized data in tabular format
- **Cross-References**: Links between related sections

### Code Documentation Standards

- **Playbook Documentation**: Purpose, requirements, variables, usage
- **Role Documentation**: Role purpose, variables, dependencies, examples
- **Task Documentation**: Clear task names and descriptions
- **Template Documentation**: Jinja2 template usage and variables
- **Inventory Documentation**: Host group purposes and relationships

### Professional Presentation

- **Executive Summary**: Business-focused overview for stakeholders
- **Technical Deep-Dive**: Detailed technical information for engineers
- **Quick Start Guide**: Fast path to deploying the infrastructure
- **Troubleshooting Section**: Common problems and solutions
- **Glossary**: Definition of infrastructure and Ansible-specific terms

## Analysis Instructions

When analyzing the Ansible infrastructure repository:

1. **Start with site.yml or main playbooks**: Understand the primary automation workflows
2. **Examine Inventory Files**: Analyze host organization and grouping strategies
3. **Map Role Dependencies**: Identify role relationships and dependencies
4. **Analyze Variable Structure**: Document variable hierarchy and usage
5. **Review Templates**: Understand configuration file generation
6. **Document Handlers**: Identify service restart and reload patterns
7. **Analyze Security**: Vault usage, secret management, access controls
8. **Review Cloud Integration**: Cloud provider modules and authentication
9. **Understand Testing**: Test strategies and validation procedures
10. **Document Deployment Flows**: End-to-end deployment procedures

## Output Deliverables

Generate the following documentation artifacts:

1. **README.md**: Primary documentation file with overview and quick start
2. **INFRASTRUCTURE.md**: Detailed infrastructure architecture documentation
3. **PLAYBOOKS.md**: Complete playbook and role documentation
4. **DEPLOYMENT.md**: Deployment procedures and operational guides
5. **DEVELOPMENT.md**: Developer setup and contribution guidelines
6. **TROUBLESHOOTING.md**: Common issues and resolution procedures
7. **SECURITY.md**: Security implementation and compliance documentation
8. **OPERATIONS.md**: Day-to-day operational procedures and maintenance

Each document should be comprehensive, professionally written, and immediately useful for its intended audience, following Ansible community standards and infrastructure automation best practices.
