# GitVisionMCP Plugin Runner Tool Guide

## Overview

The `gv_run_plugin` tool is a powerful MCP (Model Context Protocol) utility tool that enables the execution of external plugins and processes within the GitVisionMCP framework. This tool allows you to configure and run custom executables, scripts, and applications as part of your workflow, with full environment variable support, timeout controls, and output capture.

## Tool Signature

```
gv_run_plugin(
    pluginName: string
)
```

## Parameters

| Parameter    | Type   | Required | Description                                                                   |
| ------------ | ------ | -------- | ----------------------------------------------------------------------------- |
| `pluginName` | string | Yes      | Name of the plugin to execute (must match a configured plugin in config.json) |

## Return Value

The tool returns a dictionary containing:

```json
{
  "success": boolean,
  "output": string,
  "error": string
}
```

| Property  | Type    | Description                                                                             |
| --------- | ------- | --------------------------------------------------------------------------------------- |
| `success` | boolean | `true` if the plugin executed successfully (exit code 0), `false` otherwise             |
| `output`  | string  | Standard output (stdout) from the plugin execution                                      |
| `error`   | string  | Standard error (stderr) from the plugin execution, or error message if execution failed |

## Configuration

### Plugin Configuration File

Plugins are configured in the `.gitvision/config.json` file under the `Plugins` array. Each plugin must be properly configured before it can be executed.

### Configuration File Location

```
.gitvision/config.json
```

### Plugin Configuration Structure

```json
{
  "Plugins": [
    {
      "Name": "pluginName",
      "WorkingDirectory": "path/to/working/directory",
      "Executable": "path/to/executable",
      "Arguments": {
        "/arg1": "value1",
        "/arg2": "value2",
        "/arg3": ""
      },
      "Enabled": true,
      "timeoutMilliseconds": 60000,
      "Environment": {
        "ENV_VAR1": "value1",
        "ENV_VAR2": "value2"
      }
    }
  ]
}
```

### Plugin Properties

| Property              | Type    | Required | Default | Description                                                                                    |
| --------------------- | ------- | -------- | ------- | ---------------------------------------------------------------------------------------------- |
| `Name`                | string  | Yes      | -       | Unique identifier for the plugin (used in `gv_run_plugin` calls)                               |
| `WorkingDirectory`    | string  | Yes      | -       | Directory where the plugin will be executed                                                    |
| `Executable`          | string  | Yes      | -       | Path to the executable file (relative or absolute)                                             |
| `Arguments`           | object  | No       | `{}`    | Key-value pairs of command line arguments                                                      |
| `Enabled`             | boolean | No       | `false` | Whether the plugin is enabled for execution                                                    |
| `timeoutMilliseconds` | number  | No       | `60000` | Maximum execution time in milliseconds (60 seconds default)                                    |
| `Environment`         | object  | No       | `{}`    | Key-value pairs of environment variables to set for the plugin (e.g., `{"VAR_NAME": "value"}`) |

## Usage Examples

### Basic Plugin Execution

```bash
# Execute a plugin named "pluginDemo"
mcp://gv_run_plugin?pluginName=pluginDemo
```

### Chat-Based Plugin Execution

When using GitVisionMCP through chat interfaces (like VS Code Copilot), you can execute plugins using natural language commands. Here are practical examples:

#### Example 1: Running a Data Processing Plugin

**Chat Command:**

```
Run the dataProcessor plugin
```

**What happens:**

- GitVisionMCP executes `gv_run_plugin` with `pluginName=dataProcessor`
- The plugin processes data according to its configuration
- Results are returned to the chat interface

#### Example 2: Executing a Deployment Script

**Chat Command:**

```
Execute the deployScript plugin to deploy to staging
```

**Alternative Commands:**

```
Run plugin deployScript
Please execute the deployment plugin
Start the deployScript plugin
```

#### Example 3: Running Analysis Tools

**Chat Command:**

```
Run the analysisScript plugin and show me the results
```

**Expected Response:**

```json
{
  "success": true,
  "output": "Analysis completed successfully. Found 15 issues, generated report.json",
  "error": ""
}
```

#### Example 4: Multiple Plugin Execution

**Chat Command:**

```
Run the buildPlugin first, then execute the testRunner plugin
```

**Chat Command for Error Handling:**

```
Try running the documentation plugin, and if it fails, show me the error details
```

#### Example 5: Plugin Status Check

**Chat Command:**

```
What plugins are available to run?
```

**Expected Response:**
The assistant would query the `.gitvision/config.json` file and list all enabled plugins with their descriptions.

#### Example 6: Plugin Troubleshooting

**Chat Command:**

```
The buildPlugin failed last time, can you run it again and tell me what went wrong?
```

**Expected Response:**

```json
{
  "success": false,
  "output": "",
  "error": "Build failed: MSBuild error MSB3073 - The command 'dotnet build' exited with code 1"
}
```

### Chat Integration Tips

1. **Natural Language**: Use conversational language - the assistant understands context
2. **Plugin Names**: You can refer to plugins by their exact name or descriptive terms
3. **Error Handling**: Ask for error details when plugins fail
4. **Sequential Execution**: Request multiple plugins in sequence
5. **Output Interpretation**: Ask the assistant to explain plugin output or errors

### Chat Command Patterns

| Pattern                   | Example                                  | Description                       |
| ------------------------- | ---------------------------------------- | --------------------------------- |
| **Direct Execution**      | "Run pluginName"                         | Execute a specific plugin         |
| **Conditional Execution** | "Run plugin X if Y condition"            | Execute with conditions           |
| **Sequential Execution**  | "Run plugin A, then plugin B"            | Execute multiple plugins in order |
| **Error Recovery**        | "Try plugin X, if it fails show details" | Execute with error handling       |
| **Status Inquiry**        | "What plugins are available?"            | Query plugin configuration        |
| **Output Analysis**       | "Run plugin X and explain the results"   | Execute and interpret output      |

### Example Plugin Configurations

#### 1. Simple Console Application

```json
{
  "Name": "dataProcessor",
  "WorkingDirectory": ".gitvision/plugins/dataProcessor",
  "Executable": "DataProcessor.exe",
  "Arguments": {
    "--input": "data.json",
    "--output": "processed.json",
    "--verbose": ""
  },
  "Enabled": true,
  "timeoutMilliseconds": 30000,
  "Environment": {
    "LOG_LEVEL": "INFO",
    "CONFIG_PATH": "./config"
  }
}
```

#### 2. PowerShell Script

```json
{
  "Name": "deployScript",
  "WorkingDirectory": ".gitvision/scripts",
  "Executable": "powershell.exe",
  "Arguments": {
    "-ExecutionPolicy": "Bypass",
    "-File": "deploy.ps1",
    "-Environment": "staging"
  },
  "Enabled": true,
  "timeoutMilliseconds": 300000,
  "Environment": {
    "DEPLOY_ENV": "staging",
    "API_KEY": "your-api-key"
  }
}
```

#### 3. Python Script

```json
{
  "Name": "analysisScript",
  "WorkingDirectory": ".gitvision/python",
  "Executable": "python",
  "Arguments": {
    "": "analysis.py",
    "--config": "config.yaml",
    "--output-format": "json"
  },
  "Enabled": true,
  "timeoutMilliseconds": 120000,
  "Environment": {
    "PYTHONPATH": "./lib",
    "DATA_DIR": "./data"
  }
}
```

#### 4. Node.js Application

```json
{
  "Name": "nodeProcessor",
  "WorkingDirectory": ".gitvision/node",
  "Executable": "node",
  "Arguments": {
    "": "processor.js",
    "--input": "input.json",
    "--format": "pretty"
  },
  "Enabled": true,
  "timeoutMilliseconds": 60000,
  "Environment": {
    "NODE_ENV": "production",
    "DEBUG": "app:*"
  }
}
```

## Plugin Development

### Creating a Custom Plugin

1. **Create Plugin Directory**

   ```bash
   mkdir .gitvision/plugins/myPlugin
   cd .gitvision/plugins/myPlugin
   ```

2. **Create Plugin Executable**

   - Console application (C#, Go, Rust, etc.)
   - Script (PowerShell, Python, Node.js, etc.)
   - Batch file or shell script

3. **Configure in config.json**
   Add plugin configuration to the `Plugins` array

4. **Test Plugin**
   ```bash
   mcp://gv_run_plugin?pluginName=myPlugin
   ```

### Plugin Best Practices

#### Exit Codes

- Return `0` for success
- Return non-zero for errors
- Use consistent exit codes for different error types

#### Output Handling

- Write normal output to stdout
- Write error messages to stderr
- Use structured output (JSON, XML) when possible

#### Environment Variables

- Use environment variables for configuration
- Document required environment variables
- Provide sensible defaults
- Environment variables are defined as key-value pairs in the `Environment` object:
  ```json
  "Environment": {
    "VARIABLE_NAME": "variable_value",
    "CONFIG_PATH": "./config",
    "DEBUG": "true"
  }
  ```

#### Error Handling

- Handle all expected error conditions
- Provide meaningful error messages
- Log important information to stderr

### Example Plugin (C# Console App)

```csharp
using System;
using System.IO;
using System.Text.Json;

class Program
{
    static int Main(string[] args)
    {
        try
        {
            var config = Environment.GetEnvironmentVariable("CONFIG_PATH");
            var inputFile = GetArgument(args, "--input");
            var outputFile = GetArgument(args, "--output");

            if (string.IsNullOrEmpty(inputFile))
            {
                Console.Error.WriteLine("Error: --input parameter is required");
                return 1;
            }

            // Process the input file
            var data = File.ReadAllText(inputFile);
            var processed = ProcessData(data);

            if (!string.IsNullOrEmpty(outputFile))
            {
                File.WriteAllText(outputFile, processed);
                Console.WriteLine($"Successfully processed data to {outputFile}");
            }
            else
            {
                Console.WriteLine(processed);
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    static string GetArgument(string[] args, string name)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == name)
                return args[i + 1];
        }
        return string.Empty;
    }

    static string ProcessData(string input)
    {
        // Your processing logic here
        return input.ToUpper();
    }
}
```

## Security Considerations

### Plugin Security

1. **Executable Validation**

   - Only run trusted executables
   - Validate plugin paths
   - Use relative paths when possible

2. **Environment Variables**

   - Avoid storing sensitive data in environment variables
   - Use secure configuration management
   - Sanitize environment variable values
   - Environment variables are specified as JSON key-value pairs: `{"KEY": "value"}`

3. **Working Directory**

   - Use sandboxed directories
   - Avoid system directories
   - Validate directory paths

4. **Timeout Protection**
   - Always set reasonable timeouts
   - Monitor resource usage
   - Implement cleanup procedures

### Access Control

- Plugins run with the same permissions as GitVisionMCP
- Consider running plugins in containers or with restricted permissions
- Validate all input parameters
- Implement logging and auditing

## Troubleshooting

### Common Issues

#### "Plugin not found"

- **Cause**: Plugin name doesn't match configuration or plugin is disabled
- **Solution**: Check plugin name spelling and ensure `Enabled: true`

#### "Failed to parse plugin configuration"

- **Cause**: Invalid JSON in config.json
- **Solution**: Validate JSON syntax and plugin structure

#### Plugin execution timeout

- **Cause**: Plugin takes longer than configured timeout
- **Solution**: Increase `timeoutMilliseconds` or optimize plugin performance

#### Permission denied

- **Cause**: Executable lacks proper permissions
- **Solution**: Set executable permissions or run GitVisionMCP with appropriate privileges

#### Environment variable issues

- **Cause**: Missing or incorrect environment variables
- **Solution**: Verify environment variable names and values in configuration

### Debugging

#### Enable Detailed Logging

Check GitVisionMCP logs for detailed execution information:

```bash
# View logs
tail -f logs/gitvisionmcp-*.log
```

#### Test Plugin Manually

Run the plugin manually to verify it works:

```bash
cd .gitvision/plugins/pluginName
./plugin.exe --arg1 value1 --arg2 value2
```

#### Validate Configuration

Use JSON validation tools to ensure config.json is valid:

```bash
# Example using Node.js
node -e "console.log('Valid JSON')" < .gitvision/config.json
```

## Advanced Configuration

### Dynamic Arguments

Use placeholder values in arguments that can be replaced at runtime:

```json
{
  "Arguments": {
    "/input": "<INPUT_FILE>",
    "/output": "<OUTPUT_FILE>",
    "/timestamp": "<TIMESTAMP>"
  }
}
```

### Conditional Execution

Configure plugins to run only under certain conditions:

```json
{
  "Name": "conditionalPlugin",
  "Enabled": true,
  "Environment": {
    "RUN_CONDITION": "development"
  }
}
```

### Plugin Chaining

Configure multiple plugins to work together:

1. First plugin outputs to a known location
2. Second plugin reads from that location
3. Use environment variables to coordinate

## Integration with CI/CD

### GitHub Actions Example

```yaml
name: Run Custom Plugins
on: [push]

jobs:
  run-plugins:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2

      - name: Setup GitVisionMCP
        run: |
          # Setup GitVisionMCP

      - name: Run Data Processing Plugin
        run: |
          mcp://gv_run_plugin?pluginName=dataProcessor

      - name: Run Analysis Plugin
        run: |
          mcp://gv_run_plugin?pluginName=analysisScript
```

### Azure DevOps Example

```yaml
trigger:
  - main

pool:
  vmImage: "windows-latest"

steps:
  - task: PowerShell@2
    displayName: "Run Deployment Plugin"
    inputs:
      targetType: "inline"
      script: |
        # Execute plugin via GitVisionMCP
        mcp://gv_run_plugin?pluginName=deployScript
```

## Performance Considerations

### Resource Management

- Set appropriate timeouts for long-running processes
- Monitor memory usage in plugins
- Clean up temporary files created by plugins
- Use async execution patterns when possible

### Optimization Tips

1. **Batch Processing**: Group related operations in single plugin calls
2. **Caching**: Cache plugin results when appropriate
3. **Parallel Execution**: Run independent plugins in parallel
4. **Resource Limits**: Set memory and CPU limits for plugins

## Related Tools

- `gv_run_process`: Execute arbitrary processes with full control
- `gv_get_environment_variable`: Retrieve environment variable values
- `gv_set_environment_variable`: Set environment variables
- `gv_search_json_file`: Query configuration files

## Version Compatibility

This tool is compatible with:

- GitVisionMCP 1.0.9.1+
- .NET 6.0+
- Windows, Linux, macOS
- PowerShell 5.1+, PowerShell Core 6.0+

## Complete Configuration Example

```json
{
  "Project": {
    "Name": "MyProject",
    "Version": "1.0.0"
  },
  "Plugins": [
    {
      "Name": "buildPlugin",
      "WorkingDirectory": ".gitvision/plugins/build",
      "Executable": "dotnet",
      "Arguments": {
        "": "build",
        "--configuration": "Release",
        "--output": "./dist"
      },
      "Enabled": true,
      "timeoutMilliseconds": 300000,
      "Environment": {
        "DOTNET_CLI_TELEMETRY_OPTOUT": "1",
        "BUILD_ENV": "production"
      }
    },
    {
      "Name": "testRunner",
      "WorkingDirectory": ".gitvision/plugins/test",
      "Executable": "npm",
      "Arguments": {
        "": "test",
        "--": "--coverage"
      },
      "Enabled": true,
      "timeoutMilliseconds": 180000,
      "Environment": {
        "NODE_ENV": "test",
        "CI": "true"
      }
    },
    {
      "Name": "documentation",
      "WorkingDirectory": ".gitvision/plugins/docs",
      "Executable": "python",
      "Arguments": {
        "": "generate_docs.py",
        "--format": "markdown",
        "--output": "docs/"
      },
      "Enabled": false,
      "timeoutMilliseconds": 120000,
      "Environment": {
        "DOCS_VERSION": "latest"
      }
    }
  ]
}
```

---

_This documentation was generated using GitVisionMCP's documentation capabilities._
