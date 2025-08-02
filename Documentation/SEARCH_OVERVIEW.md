# Search Tools Documentation

> **Comprehensive guide to GitVisionMCP search tools for querying JSON, YAML, XML, CSV, and Excel files**

## Table of Contents

1. [Quick Start](#quick-start)
2. [Available Search Tools](#available-search-tools)
3. [Getting Started Examples](#getting-started-examples)
4. [Query Language Reference](#query-language-reference)
5. [Tool Features Comparison](#tool-features-comparison)
6. [Advanced Usage](#advanced-usage)
7. [Practical Use Cases](#practical-use-cases)
8. [Parameter Reference](#parameter-reference)

---

## Quick Start

GitVisionMCP provides powerful search tools for different file formats:

- **JSON/YAML/CSV/Excel**: Use JSONPath queries
- **XML**: Use XPath queries
- **All tools** support filtering, wildcards, and structured output

**Basic syntax examples:**

```bash
# Find all users in JSON
$.users[*]

# Get XML attributes
//user/@name

# Filter by condition
$[?(@.active == true)]
```

---

## Available Search Tools

### 🗂️ `search_json_file`

**Query JSON files using JSONPath**

- **File Support**: `.json`
- **Query Language**: JSONPath 2.0
- **Key Features**: Wildcards, filters, structured output
- **Library**: Newtonsoft.Json

### 📄 `search_yaml_file`

**Query YAML files using JSONPath (after JSON conversion)**

- **File Support**: `.yaml`, `.yml`
- **Query Language**: JSONPath 2.0
- **Key Features**: YAML-native parsing, full JSONPath support
- **Library**: YamlDotNet + Newtonsoft.Json

### 🌐 `search_xml_file`

**Query XML files using XPath**

- **File Support**: `.xml`
- **Query Language**: XPath 1.0
- **Key Features**: Element/attribute access, namespace-aware
- **Library**: System.Xml.XPath (.NET Core)

### 📊 `search_csv_file`

**Query CSV files using JSONPath (after JSON conversion)**

- **File Support**: `.csv`
- **Query Language**: JSONPath 2.0
- **Key Features**: Header detection, blank line handling
- **Library**: CsvHelper + Newtonsoft.Json

### 📈 `search_excel_file`

**Query Excel files using JSONPath (after JSON conversion)**

- **File Support**: `.xlsx`
- **Query Language**: JSONPath 2.0
- **Key Features**: Multi-sheet processing, auto header detection
- **Library**: ClosedXML + Newtonsoft.Json

---

## Getting Started Examples

### JSON Configuration Search

```bash
# Find all API endpoints
$.endpoints[*].url

# Get database settings
$.database.connectionString
```

**Copilot Chat:**

> "Find all API endpoints in config.json"

### YAML Docker Compose Analysis

```bash
# List all service images
$.services[*].image

# Find services with specific ports
$.services[?(@.ports)]
```

**Copilot Chat:**

> "List all container images in docker-compose.yml"

### XML Configuration Extraction

```bash
# Get connection strings
//connectionStrings/add/@connectionString

# Find user permissions
//user[@role='admin']/@permissions
```

**Copilot Chat:**

> "Extract database connection strings from web.config"

### CSV Data Analysis

```bash
# Get all server names
$[*].ServerName

# Find active servers
$[?(@.Status == 'Active')]
```

**Copilot Chat:**

> "Find all active servers in inventory.csv"

### Excel Data Extraction

```bash
# Extract budget data
$[?(@.Department == 'IT')].Budget

# Get all employee names
$[*].EmployeeName
```

**Copilot Chat:**

> "Get IT department budget from budget.xlsx"

---

---

## Query Language Reference

### 📖 JSONPath Syntax Guide

JSONPath is used for JSON, YAML, CSV, and Excel files. Here are the most common patterns:

| Pattern            | Description        | Example          | Result            |
| ------------------ | ------------------ | ---------------- | ----------------- |
| `$`                | Root element       | `$`              | Entire document   |
| `$.name`           | Property access    | `$.user.name`    | User's name       |
| `$[0]`             | Array index        | `$.users[0]`     | First user        |
| `$[*]`             | All array elements | `$.users[*]`     | All users         |
| `$..email`         | Recursive descent  | `$..email`       | All email fields  |
| `$[?(@.age > 18)]` | Filter condition   | `$[?(@.active)]` | Active items only |

#### 🔍 JSONPath Advanced Features

| Feature            | Syntax              | Example                                          | Description         |
| ------------------ | ------------------- | ------------------------------------------------ | ------------------- |
| **Array slicing**  | `[start:end]`       | `$.users[1:3]`                                   | Elements 1-2        |
| **Multiple props** | `['prop1','prop2']` | `$['name','email']`                              | Name and email      |
| **Logical AND**    | `&&`                | `$[?(@.age > 18 && @.active)]`                   | Multiple conditions |
| **Logical OR**     | `\|\|`              | `$[?(@.role == 'admin' \|\| @.role == 'owner')]` | Either condition    |
| **Contains**       | `contains`          | `$[?(@.name contains 'John')]`                   | String matching     |
| **Length**         | `length()`          | `$[?(@.items.length > 5)]`                       | Array length check  |

### 🌐 XPath Syntax Guide

XPath is used exclusively for XML files:

| Pattern           | Description       | Example                 | Result                |
| ----------------- | ----------------- | ----------------------- | --------------------- |
| `/`               | Root element      | `/root`                 | Root node             |
| `//`              | Descendant search | `//user`                | All user elements     |
| `/@attr`          | Attribute access  | `//user/@id`            | User ID attributes    |
| `/text()`         | Text content      | `//name/text()`         | Text inside name tags |
| `[@attr='value']` | Attribute filter  | `//user[@role='admin']` | Admin users           |
| `[position()]`    | Position filter   | `//user[1]`             | First user            |

#### 🔍 XPath Advanced Features

| Feature                 | Syntax                 | Example                              | Description       |
| ----------------------- | ---------------------- | ------------------------------------ | ----------------- |
| **Multiple conditions** | `and`, `or`            | `//user[@age>18 and @active='true']` | Multiple criteria |
| **Contains function**   | `contains()`           | `//user[contains(@name,'John')]`     | String matching   |
| **Position functions**  | `last()`, `position()` | `//user[last()]`                     | Last element      |
| **Parent axis**         | `parent::`             | `//name/parent::user`                | Parent elements   |
| **Following axis**      | `following::`          | `//user/following::comment`          | Following nodes   |

---

## Tool Features Comparison

### 🔧 Feature Matrix

| Feature               | 📄 JSON  | 📄 YAML         | 🌐 XML | 📊 CSV    | 📈 Excel |
| --------------------- | -------- | --------------- | ------ | --------- | -------- |
| **File Extensions**   | `.json`  | `.yaml`, `.yml` | `.xml` | `.csv`    | `.xlsx`  |
| **Query Language**    | JSONPath | JSONPath        | XPath  | JSONPath  | JSONPath |
| **Structured Output** | ✅       | ✅              | ✅     | ✅        | ✅       |
| **Path Information**  | ✅       | ✅              | ✅     | ❌        | ✅       |
| **Pretty Printing**   | ✅       | ✅              | ✅     | ❌        | ❌       |
| **Wildcards**         | ✅       | ✅              | ✅     | ✅        | ✅       |
| **Filtering**         | ✅       | ✅              | ✅     | ✅        | ✅       |
| **Multi-sheet**       | ❌       | ❌              | ❌     | ❌        | ✅       |
| **Auto Headers**      | ❌       | ❌              | ❌     | 🔧 Config | ✅       |
| **Attributes**        | ❌       | ❌              | ✅     | ❌        | ❌       |
| **Text Nodes**        | ❌       | ❌              | ✅     | ❌        | ❌       |

### 📋 Capability Comparison

#### JSONPath Support Matrix

| JSONPath Feature     | JSON | YAML | CSV | Excel | Example           |
| -------------------- | ---- | ---- | --- | ----- | ----------------- |
| **Root Access**      | ✅   | ✅   | ✅  | ✅    | `$`               |
| **Property Access**  | ✅   | ✅   | ✅  | ✅    | `$.name`          |
| **Array Indexing**   | ✅   | ✅   | ✅  | ✅    | `$.users[0]`      |
| **Wildcards**        | ✅   | ✅   | ✅  | ✅    | `$.users[*]`      |
| **Recursive Search** | ✅   | ✅   | ✅  | ✅    | `$..email`        |
| **Filters**          | ✅   | ✅   | ✅  | ✅    | `$[?(@.active)]`  |
| **Array Slicing**    | ✅   | ✅   | ✅  | ✅    | `$.users[1:3]`    |
| **Union Queries**    | ✅   | ✅   | ✅  | ✅    | `$['name','age']` |

#### XPath Support (XML Only)

| XPath Feature        | Support | Example                  | Description           |
| -------------------- | ------- | ------------------------ | --------------------- |
| **Absolute Paths**   | ✅      | `/root/users/user`       | From document root    |
| **Relative Paths**   | ✅      | `users/user`             | From current context  |
| **Descendant Axis**  | ✅      | `//user`                 | All descendants       |
| **Attribute Access** | ✅      | `//user/@name`           | Element attributes    |
| **Text Nodes**       | ✅      | `//name/text()`          | Text content          |
| **Predicates**       | ✅      | `//user[@id='1']`        | Conditional selection |
| **Functions**        | ✅      | `contains(@name,'John')` | Built-in functions    |
| **Axes**             | ✅      | `parent::users`          | Navigation axes       |

---

## Advanced Usage

### 🚀 Complex Query Examples

#### Multi-Level Filtering

```jsonpath
# Find active admin users with recent login
$.users[?(@.role == 'admin' && @.active == true && @.lastLogin > '2024-01-01')]
```

#### Nested Object Navigation

```jsonpath
# Get all database connection strings
$.environments[*].databases[*].connectionString
```

#### Conditional Excel Analysis

```jsonpath
# Find high-budget IT projects
$[?(@.Department == 'IT' && @.Budget > 100000)]
```

#### XML Attribute Combinations

```xpath
# Find config entries with specific attributes
//config[@environment='prod' and @enabled='true']/@value
```

### 🔧 Output Formatting Options

#### Default Output

Returns direct values or arrays.

#### Structured Output (`showKeyPaths: true`)

Returns objects with path information:

```json
{
  "path": "$.users[0].email",
  "value": "john@example.com",
  "key": "email"
}
```

#### Pretty Printing (`indented: true`)

Formats JSON output with proper indentation for readability.

### ⚙️ Tool-Specific Configuration

#### CSV Options

- `hasHeaderRecord`: Whether first row contains headers (default: true)
- `ignoreBlankLines`: Skip empty rows (default: true)

#### Excel Behavior

- Automatically processes all worksheets
- Headers detected from first row of each sheet
- Results organized by sheet name

---

## Practical Use Cases

### 🔍 Configuration Management

#### Extract API Settings

```bash
# JSON config
$.api.endpoints[*].url

# YAML config
$.services[*].endpoints[*]

# XML config
//endpoints/endpoint/@url
```

**Use Cases:**

- Extract API keys from configuration files
- Find database connection strings
- Analyze service configurations

### 🐳 DevOps & Infrastructure

#### Docker Compose Analysis

```bash
# Find all container images
$.services[*].image

# Get environment variables
$.services[*].environment[*]

# Find exposed ports
$.services[*].ports[*]
```

**Use Cases:**

- Audit container dependencies
- Extract environment configurations
- Analyze service networking

### 📊 Data Analysis

#### Excel/CSV Data Mining

```bash
# Filter by department
$[?(@.Department == 'Engineering')]

# Find high-value items
$[?(@.Value > 1000)]

# Get specific columns
$[*].['Name', 'Email', 'Department']
```

**Use Cases:**

- HR data analysis
- Financial reporting
- Inventory management

### 🔒 Security Auditing

#### Configuration Security

```bash
# Find SSL settings
$[?(@.ssl == true)]

# Check encryption settings
//security[@encryption='enabled']

# Audit user permissions
$.users[?(@.permissions)]
```

**Use Cases:**

- Security configuration audits
- Compliance checking
- Permission analysis

---

## Parameter Reference

### 📋 Tool Signatures

| Tool                  | Signature                                                      | Required                    | Optional                                            |
| --------------------- | -------------------------------------------------------------- | --------------------------- | --------------------------------------------------- |
| **search_json_file**  | `(jsonFilePath, jsonPath, indented?, showKeyPaths?)`           | `jsonFilePath`, `jsonPath`  | `indented` (true), `showKeyPaths` (false)           |
| **search_yaml_file**  | `(yamlFilePath, jsonPath, indented?, showKeyPaths?)`           | `yamlFilePath`, `jsonPath`  | `indented` (true), `showKeyPaths` (false)           |
| **search_xml_file**   | `(xmlFilePath, xPath, indented?, showKeyPaths?)`               | `xmlFilePath`, `xPath`      | `indented` (true), `showKeyPaths` (false)           |
| **search_csv_file**   | `(csvFilePath, jsonPath, hasHeaderRecord?, ignoreBlankLines?)` | `csvFilePath`, `jsonPath`   | `hasHeaderRecord` (true), `ignoreBlankLines` (true) |
| **search_excel_file** | `(excelFilePath, jsonPath)`                                    | `excelFilePath`, `jsonPath` | None                                                |

### 🔧 Parameter Details

#### Common Parameters

| Parameter        | Type      | Description                         | Example                            |
| ---------------- | --------- | ----------------------------------- | ---------------------------------- |
| **filePath**     | `string`  | Path relative to workspace root     | `"config/app.json"`, `"data.xlsx"` |
| **jsonPath**     | `string`  | JSONPath query (JSONPath 2.0 spec)  | `"$.users[*]"`, `"$[?(@.active)]"` |
| **xPath**        | `string`  | XPath query (XPath 1.0 spec)        | `"//user[@id='1']"`, `"//@name"`   |
| **indented**     | `boolean` | Pretty-print output (default: true) | `true`, `false`                    |
| **showKeyPaths** | `boolean` | Include path info (default: false)  | `true`, `false`                    |

#### CSV-Specific Parameters

| Parameter            | Type      | Default | Description                       |
| -------------------- | --------- | ------- | --------------------------------- |
| **hasHeaderRecord**  | `boolean` | `true`  | First row contains column headers |
| **ignoreBlankLines** | `boolean` | `true`  | Skip empty rows during processing |

#### Excel-Specific Behavior

- **Multi-sheet processing**: Automatically processes all worksheets
- **Header detection**: Headers automatically detected from first row
- **Result organization**: Results organized by sheet name in JSON structure

### 💬 Copilot Chat Examples

#### Simple Commands

```bash
@copilot Use search_json_file to find all users in config.json with path $.users[*]

@copilot Search docker-compose.yml for all service images using search_yaml_file with $.services[*].image

@copilot Extract server names from servers.xlsx using search_excel_file with $[*].ServerName
```

#### Detailed Commands with Parameters

```bash
@copilot Use search_json_file with these parameters:
- jsonFilePath: "appsettings.json"
- jsonPath: "$.Database.ConnectionStrings[*]"
- indented: true
- showKeyPaths: true

@copilot Use search_xml_file to find user permissions:
- xmlFilePath: "security.xml"
- xPath: "//user[@role='admin']/@permissions"
- showKeyPaths: true

@copilot Use search_csv_file to analyze inventory:
- csvFilePath: "inventory.csv"
- jsonPath: "$[*].ServerName"
- hasHeaderRecord: true
```

#### Natural Language Requests

```bash
"Search for all email addresses in users.json"
"Find Docker container images in my compose file"
"Extract database settings from web.config"
"Get IT department budget from Excel file"
"Find all active servers in the CSV inventory"
```

---

## Quick Reference

### 🎯 Common Query Patterns

#### JSONPath Quick Reference

```bash
$                    # Root element
$.property           # Direct property
$[0]                 # First array element
$[*]                 # All array elements
$..property          # Recursive search
$[?(@.prop)]        # Filter by property existence
$[?(@.prop == val)] # Filter by value
$['prop1','prop2']  # Multiple properties
```

#### XPath Quick Reference

```bash
/                    # Root
//element           # All elements
//@attr             # All attributes
//elem[@attr]       # Elements with attribute
//elem[@attr='val'] # Elements with specific value
//elem[1]           # First element
//elem[last()]      # Last element
```

### 📊 Output Format Examples

#### Default Output

```json
["john@example.com", "jane@example.com"]
```

#### Structured Output (showKeyPaths: true)

```json
[
  {
    "path": "$.users[0].email",
    "value": "john@example.com",
    "key": "email"
  },
  {
    "path": "$.users[1].email",
    "value": "jane@example.com",
    "key": "email"
  }
]
```

---

## Best Practices

### 🎯 Query Optimization

- Use specific paths instead of recursive descent when possible
- Filter early in the query to reduce processing
- Use wildcards judiciously to avoid large result sets

### 🔍 Debugging Tips

- Start with simple queries and build complexity gradually
- Use `showKeyPaths` to understand result structure
- Test queries on small data sets first

### 📁 File Organization

- Keep file paths relative to workspace root
- Use consistent naming conventions
- Organize configuration files logically

---

_For more examples and advanced usage, see the individual tool documentation or use Copilot chat for interactive assistance._
