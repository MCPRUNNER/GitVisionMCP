# TODO

- [x] Update `GitServiceTools.cs` to ensure that the `maxFiles` parameter is set to a maximum of 1000 instead of 200.
- [x] Ensure that the `maxFiles` parameter in the `ReadFilteredWorkspaceFiles` method is set to 1000, and adjust the description accordingly.
- [x] Verify that the `maxFileSize` parameter is set to a maximum of 10MB in the `ReadFilteredWorkspaceFiles` method.
- [x] Add an exclude list for files under specific directories like `.git`, `node_modules`, and `bin`/`obj` directories in the `ReadFilteredWorkspaceFiles` and `ListWorkspaceFiles` methods. Save exclude list in a configuration file saved under `.gitvision/exclude.json`. Put the loading of exclude.json in the LocationService service.
  - [x] Created ExcludeConfiguration model with Newtonsoft.Json attributes
  - [x] Implemented `.gitvision/exclude.json` with default exclude patterns
  - [x] Added exclude functionality to LocationService with caching and pattern matching
  - [x] Updated GitServiceTools to use new GetAllFilesAsync() method with exclude filtering
  - [x] Added comprehensive documentation and examples
- [ ] Implement configuration file saved under `.gitvision/config.json` that allows changing the `maxFiles`, `maxFileSize`, and `applyExclusions` settings, and load it when needed.
- [x] Implement SearchYamlFile Tool with same capabilities as SearchJsonFile Tool.
  - [x] Added YamlDotNet package dependency for YAML parsing
  - [x] Implemented SearchYamlFile method in LocationService with YAML-to-JSON conversion
  - [x] Added SearchYamlFileAsync MCP tool in GitServiceTools with JSONPath support
  - [x] Created comprehensive unit tests for YAML search functionality
  - [x] Verified integration with existing JSON search infrastructure
