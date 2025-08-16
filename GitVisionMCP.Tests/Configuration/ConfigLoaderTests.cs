using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using GitVisionMCP.Configuration;
using GitVisionMCP.Services;
using GitVisionMCP.Models;

namespace GitVisionMCP.Tests.Configuration;

/// <summary>
/// Unit tests for ConfigLoader focusing on loading the main config.json
/// </summary>
public class ConfigLoaderTests
{
    private readonly Mock<ILogger<ConfigLoader>> _mockLogger;
    private readonly Mock<IFileService> _mockFileService;
    private readonly ConfigLoader _configLoader;

    public ConfigLoaderTests()
    {
        _mockLogger = new Mock<ILogger<ConfigLoader>>();
        _mockFileService = new Mock<IFileService>();
        _configLoader = new ConfigLoader(_mockLogger.Object, _mockFileService.Object);
    }

    [Fact]
    public void LoadConfig_ShouldReturnValidConfig_WhenFileExists()
    {
        // Arrange
        var configJson = @"{
            ""Project"": {
                ""Name"": ""GitVisionMCP"",
                ""RelativePath"": ""GitVisionMCP.csproj""
            },
            ""Settings"": {
                ""maxCommits"": 200,
                ""maxFiles"": 300,
                ""maxFileSize"": 1024
            },
            ""Git"": {
                ""Release"": ""origin/master""
            }
        }";

        _mockFileService.Setup(x => x.GetFullPath(".gitvision/config.json"))
            .Returns(".gitvision/config.json");
        _mockFileService.Setup(x => x.ReadFile(".gitvision/config.json"))
            .Returns(configJson);

        // Act
        var result = _configLoader.LoadConfig();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Project);
        Assert.Equal("GitVisionMCP", result.Project.Name);
        Assert.Equal("GitVisionMCP.csproj", result.Project.RelativePath);

        Assert.NotNull(result.Settings);
        Assert.Equal(200, result.Settings.MaxCommits);
        Assert.Equal(300, result.Settings.MaxFiles);
        Assert.Equal(1024, result.Settings.MaxFileSize);

        Assert.NotNull(result.Git);
        Assert.Equal("origin/master", result.Git.Release);
    }

    [Fact]
    public void LoadConfig_ShouldReturnEmptyConfig_WhenFileNotFound()
    {
        // Arrange
        _mockFileService.Setup(x => x.GetFullPath(".gitvision/config.json"))
            .Returns((string?)null);

        // Act
        var result = _configLoader.LoadConfig();

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Project);
        Assert.Null(result.Settings);
        Assert.Null(result.Git);
    }

    [Fact]
    public void LoadConfig_ShouldReturnEmptyConfig_WhenFileEmpty()
    {
        // Arrange
        _mockFileService.Setup(x => x.GetFullPath(".gitvision/config.json"))
            .Returns(".gitvision/config.json");
        _mockFileService.Setup(x => x.ReadFile(".gitvision/config.json"))
            .Returns("");

        // Act
        var result = _configLoader.LoadConfig();

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Project);
        Assert.Null(result.Settings);
        Assert.Null(result.Git);
    }

    [Fact]
    public void LoadConfig_ShouldReturnEmptyConfig_WhenJsonInvalid()
    {
        // Arrange
        _mockFileService.Setup(x => x.GetFullPath(".gitvision/config.json"))
            .Returns(".gitvision/config.json");
        _mockFileService.Setup(x => x.ReadFile(".gitvision/config.json"))
            .Returns("{ invalid json }");

        // Act
        var result = _configLoader.LoadConfig();

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Project);
        Assert.Null(result.Settings);
        Assert.Null(result.Git);
    }
}
