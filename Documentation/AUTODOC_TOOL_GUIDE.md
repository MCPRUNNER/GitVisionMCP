# GitVisionMCP Auto-Documentation Tool Guide

## Overview

The `gv_generate_autodoc` tool is a powerful MCP (Model Context Protocol) tool that automatically generates comprehensive documentation for C# projects by analyzing source code structure and generating JSON documentation files. This tool is particularly useful for maintaining up-to-date architectural documentation and code analysis reports.

## Tool Signature

```
gv_generate_autodoc(
    configFilePath?: string = ".gitvision/autodocument.json",
    jsonPath?: string = "$.Documentation",
    templateFilePath?: string = ".gitvision/templates/autodoc.template.sbn",
    templateOutputPath?: string = "Documentation/autodoc.md"
)
```

## Parameters

| Parameter            | Type   | Default                                     | Description                                                        |
| -------------------- | ------ | ------------------------------------------- | ------------------------------------------------------------------ |
| `configFilePath`     | string | `.gitvision/autodocument.json`              | Path to the configuration file that defines which files to analyze |
| `jsonPath`           | string | `$.Documentation`                           | JSONPath expression to locate the file mappings in the config      |
| `templateFilePath`   | string | `.gitvision/templates/autodoc.template.sbn` | Path to the Scriban template file for output formatting            |
| `templateOutputPath` | string | `Documentation/autodoc.md`                  | Path where the final documentation will be generated               |

## Configuration File Structure

### Basic Configuration (`autodocument.json`)

The configuration file uses JSON format to define documentation metadata and specify which source files to analyze:

```json
{
  "Documentation": {
    "Title": "Your Project Documentation",
    "Summary": "Brief description of your project",
    "Description": "Detailed description of the project's purpose",
    "Commands": [
      {
        "Command": "#gv_get_recent_commits",
        "Post": "{maxCommits=300}",
        "Output": "commits"
      }
    ],
    "Files": [
      {
        "Name": "ServiceName",
        "Type": "Service|Repository|Tools|Controller",
        "Description": "Brief description of the component",
        "RelativePath": "path/to/source/file.cs",
        "OutputPath": ".temp/output-file.json"
      }
    ]
  }
}
```

### Configuration Properties

#### Documentation Section

| Property      | Type   | Required | Description                                   |
| ------------- | ------ | -------- | --------------------------------------------- |
| `Title`       | string | Yes      | Main title for the generated documentation    |
| `Summary`     | string | Yes      | Brief one-line summary of the project         |
| `Description` | string | Yes      | Detailed description of the project           |
| `Commands`    | array  | No       | Optional commands to include in documentation |
| `Files`       | array  | Yes      | List of source files to analyze               |

#### File Entry Properties

| Property       | Type   | Required | Description                                                      |
| -------------- | ------ | -------- | ---------------------------------------------------------------- |
| `Name`         | string | Yes      | Logical name for the component                                   |
| `Type`         | string | Yes      | Architecture type (Service, Repository, Tools, Controller, etc.) |
| `Description`  | string | Yes      | Brief description of the component's purpose                     |
| `RelativePath` | string | Yes      | Path to the source file relative to workspace root               |
| `OutputPath`   | string | Yes      | Path where the JSON analysis will be saved                       |

## Template File (Scriban)

The tool uses Scriban template engine to format the output. The default template structure:

```scriban
# {{Title}}
*{{Description}}*
{{Summary}}

## Repositories and Services
| Name | Architecture Model | Description | Path |
|------------|-------------------|-----------|----------------|
{{- for item in Files }}
| `{{ item.Name }}` | {{ item.Type }} | {{ item.Description }} |   {{ item.RelativePath }} |
{{- end }}

## Components and Public Methods with Arguments
| Class Name | Architecture Model | Namespace | Public Methods with Arguments |
|------------|--------------------|-----------|------------------------------:|
{{- for item in deconstructed }}
| `{{ item.ClassName }}` | {{ item.ArchitectureModel }} | {{ item.Namespace }} |
{{- for method in item.Actions }}
  {{- if method.Accessibility == "public" && method.Parameters.size > 0 }}
    `{{ method.Name }}` :
    {{- for param in method.Parameters }}
      {{param.Name}} {{param.Type}}<br>
    {{- end }}<br>
  {{- end }}
{{- end }}|
{{- end }}
```

### Available Template Variables

| Variable        | Type   | Description                     |
| --------------- | ------ | ------------------------------- |
| `Title`         | string | Project title from config       |
| `Description`   | string | Project description from config |
| `Summary`       | string | Project summary from config     |
| `Files`         | array  | List of file configurations     |
| `deconstructed` | array  | Analyzed source code structures |

#### Deconstructed Object Properties

Each `deconstructed` item contains:

- `ClassName`: The name of the C# class
- `ArchitectureModel`: The architecture type (from config)
- `Namespace`: The C# namespace
- `Actions`: Array of methods/actions
  - `Name`: Method name
  - `Accessibility`: public, private, protected, etc.
  - `Parameters`: Array of parameter objects
    - `Name`: Parameter name
    - `Type`: Parameter type

## Usage Examples

### Basic Usage

```bash
# Generate documentation with default settings
mcp://gv_generate_autodoc
```

### Custom Configuration File

```bash
# Use a custom configuration file
mcp://gv_generate_autodoc?configFilePath=.config/my-autodoc.json
```

### Custom JSONPath

```bash
# Use a different JSONPath to locate file mappings
mcp://gv_generate_autodoc?jsonPath=$.ProjectFiles
```

### Custom Template and Output

```bash
# Use custom template and output location
mcp://gv_generate_autodoc?templateFilePath=templates/custom.sbn&templateOutputPath=docs/api.md
```

## Workflow

1. **Configuration Reading**: The tool reads the specified configuration file
2. **JSONPath Evaluation**: Extracts file mappings using the provided JSONPath
3. **Source Code Analysis**: Each source file is analyzed using the deconstruction service
4. **JSON Generation**: Individual JSON files are created for each analyzed source file
5. **Template Processing**: The Scriban template is processed with the collected data
6. **Documentation Output**: Final markdown documentation is generated

## Output Files

The tool generates several types of output:

### Temporary JSON Files

- Individual JSON files for each analyzed source file
- Contain detailed structural analysis of C# classes
- Stored in paths specified by `OutputPath` in configuration

### Final Documentation

- Markdown file generated from the template
- Contains aggregated information from all analyzed files
- Includes tables of components and their public methods

## Directory Structure

```
project-root/
├── .gitvision/
│   ├── autodocument.json          # Configuration file
│   └── templates/
│       └── autodoc.template.sbn   # Template file
├── .temp/                         # Temporary JSON outputs
│   ├── Service1.cs.json
│   ├── Repository1.cs.json
│   └── ...
└── Documentation/
    └── autodoc.md                 # Final generated documentation
```

## Error Handling

The tool provides comprehensive error handling:

- **Configuration File Not Found**: Returns null with warning log
- **Invalid JSONPath**: Logs error and throws `InvalidOperationException`
- **Source File Analysis Failure**: Continues with other files, logs warnings
- **Template Processing Error**: Throws `InvalidOperationException` with details

## Best Practices

### Configuration Management

1. **Organize by Architecture**: Group files by their architectural role (Services, Repositories, etc.)
2. **Descriptive Names**: Use clear, descriptive names for components
3. **Consistent Paths**: Use consistent relative path patterns
4. **Version Control**: Keep configuration files in version control

### Template Customization

1. **Project-Specific Templates**: Create custom templates for different project types
2. **Conditional Sections**: Use Scriban conditionals for optional content
3. **Formatting**: Maintain consistent markdown formatting
4. **Responsive Design**: Consider how tables will render in different viewers

### File Organization

1. **Temporary Files**: Use `.temp/` or similar for temporary JSON outputs
2. **Documentation Location**: Place final documentation in a logical location
3. **Template Versioning**: Version control template files for consistency

## Troubleshooting

### Common Issues

**Issue**: "Failed to generate auto-documentation JSON files"

- **Solution**: Check that all source files exist and are readable
- **Check**: Verify configuration file JSON syntax

**Issue**: "Template processing failed"

- **Solution**: Validate Scriban template syntax
- **Check**: Ensure all referenced variables exist in template data

**Issue**: "No files found to process"

- **Solution**: Verify JSONPath expression matches configuration structure
- **Check**: Confirm `Files` array exists at specified JSONPath

### Debugging

Enable detailed logging to troubleshoot issues:

```bash
# Check logs for detailed error information
tail -f logs/gitvisionmcp-*.log
```

## Integration with CI/CD

The tool can be integrated into build pipelines to automatically update documentation:

```yaml
# Example GitHub Actions step
- name: Generate Documentation
  run: |
    # Call the MCP tool to generate updated documentation
    mcp://gv_generate_autodoc
    git add Documentation/autodoc.md
    git commit -m "Auto-update documentation"
```

## Related Tools

- `gv_deconstruct_to_file`: Analyze individual C# files
- `gv_deconstruct_to_json`: Generate JSON analysis for single files
- `gv_run_sbn_template`: Process Scriban templates with custom data
- `gv_search_json_file`: Query configuration files using JSONPath

## Version Compatibility

This tool is compatible with:

- GitVisionMCP 1.0.9.1+
- .NET 6.0+
- Scriban template engine
- JSONPath.NET library

---

_This documentation was generated using GitVisionMCP's auto-documentation capabilities._
