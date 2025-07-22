using GitVisionMCP.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GitVisionMCP.Tests.Services;

public class LocationServiceTests
{
    [Fact]
    public void ReadPromptFile_WithValidFilename_ReturnsContent()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LocationService>>();

        // Set the GIT_REPOSITORY_DIRECTORY environment variable to point to the main project directory
        var originalDir = Environment.GetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY");
        var projectDir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..");
        Environment.SetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY", projectDir);

        try
        {
            var locationService = new LocationService(mockLogger.Object);
            var promptFilename = "ReleaseNotesPrompt.md";

            // Act
            var content = locationService.GetGitHubPromptFileContent(promptFilename);

            // Assert
            Assert.NotNull(content);
            Assert.NotEmpty(content);
            Assert.Contains("System Prompt for Generating Release Notes", content);
        }
        finally
        {
            // Restore original environment variable
            Environment.SetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY", originalDir);
        }
    }

    [Fact]
    public void ReadPromptFile_WithInvalidFilename_ReturnsNull()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LocationService>>();
        var locationService = new LocationService(mockLogger.Object);
        var promptFilename = "NonExistentFile.md";

        // Act
        var content = locationService.GetGitHubPromptFileContent(promptFilename);

        // Assert
        Assert.Null(content);
    }

    [Fact]
    public void ReadPromptFile_WithEmptyFilename_ReturnsNull()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LocationService>>();
        var locationService = new LocationService(mockLogger.Object);

        // Act
        var content = locationService.GetGitHubPromptFileContent("");

        // Assert
        Assert.Null(content);
    }

    [Fact]
    public void ReadPromptFile_WithNullFilename_ReturnsNull()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LocationService>>();
        var locationService = new LocationService(mockLogger.Object);

        // Act
        var content = locationService.GetGitHubPromptFileContent(null!);

        // Assert
        Assert.Null(content);
    }

    [Fact]
    public void SearchYamlFile_WithValidYamlContent_ReturnsSearchResult()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LocationService>>();

        // Set up environment to point to test directory
        var originalDir = Environment.GetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY");
        var testDir = Path.Combine(Path.GetTempPath(), "yaml-test");
        Directory.CreateDirectory(testDir);

        var yamlFilePath = Path.Combine(testDir, "test-config.yaml");
        var yamlContent = @"
application:
  name: ""TestApp""
  version: ""1.0.0""
database:
  host: ""localhost""
  port: 5432
users:
  - name: ""John""
    role: ""admin""
  - name: ""Jane""
    role: ""user""
";
        File.WriteAllText(yamlFilePath, yamlContent);

        Environment.SetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY", testDir);

        try
        {
            var locationService = new LocationService(mockLogger.Object);

            // Act
            var result = locationService.SearchYamlFile("test-config.yaml", "$.application.name");

            // Assert
            Assert.NotNull(result);
            Assert.Contains("TestApp", result);
        }
        finally
        {
            // Cleanup
            Environment.SetEnvironmentVariable("GIT_REPOSITORY_DIRECTORY", originalDir);
            if (Directory.Exists(testDir))
            {
                Directory.Delete(testDir, true);
            }
        }
    }
}