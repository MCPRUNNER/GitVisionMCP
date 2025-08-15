using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using GitVisionMCP.Repositories;

namespace GitVisionMCP.Tests.Repositories;

/// <summary>
/// Unit tests for FileRepository focusing on cross-platform path handling and spaces in names
/// </summary>
public class FileRepositoryTests
{
    private readonly Mock<ILogger<FileRepository>> _mockLogger;
    private readonly FileRepository _fileRepository;

    public FileRepositoryTests()
    {
        _mockLogger = new Mock<ILogger<FileRepository>>();
        _fileRepository = new FileRepository(_mockLogger.Object);
    }

    [Theory]
    [InlineData("test/path/file.txt", "test\\path\\file.txt")]
    [InlineData("test\\path\\file.txt", "test/path/file.txt")]
    [InlineData("test path/file name.txt", "test path\\file name.txt")]
    [InlineData("test\\space path\\file with spaces.txt", "test/space path/file with spaces.txt")]
    public void IsPathMatchingPattern_ShouldHandleCrossPlatformPaths(string path, string pattern)
    {
        // Act
        var result1 = _fileRepository.IsPathMatchingPattern(path, pattern);
        var result2 = _fileRepository.IsPathMatchingPattern(pattern, path);

        // Assert
        Assert.True(result1, $"Path '{path}' should match pattern '{pattern}'");
        Assert.True(result2, $"Pattern '{pattern}' should match path '{path}'");
    }

    [Theory]
    [InlineData("test/*/file.txt", "test/subfolder/file.txt")]
    [InlineData("test\\*\\file.txt", "test/subfolder/file.txt")]
    [InlineData("test/**/file.txt", "test/sub/folder/file.txt")]
    [InlineData("test\\**\\file.txt", "test\\sub\\folder\\file.txt")]
    [InlineData("**/file with spaces.txt", "deep/nested/folder/file with spaces.txt")]
    [InlineData("folder with spaces/*", "folder with spaces/some file.txt")]
    public void IsPathMatchingPattern_ShouldHandleGlobPatterns(string pattern, string path)
    {
        // Act
        var result = _fileRepository.IsPathMatchingPattern(path, pattern);

        // Assert
        Assert.True(result, $"Path '{path}' should match pattern '{pattern}'");
    }

    [Theory]
    [InlineData("test/file.txt", "other/file.txt")]
    [InlineData("test/file.txt", "test/other.txt")]
    [InlineData("test/**/file.txt", "different/file.txt")]
    [InlineData("folder with spaces/*", "other folder/file.txt")]
    public void IsPathMatchingPattern_ShouldNotMatchDifferentPaths(string pattern, string path)
    {
        // Act
        var result = _fileRepository.IsPathMatchingPattern(path, pattern);

        // Assert
        Assert.False(result, $"Path '{path}' should not match pattern '{pattern}'");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void IsPathMatchingPattern_ShouldHandleInvalidInputs(string input)
    {
        // Act & Assert
        Assert.False(_fileRepository.IsPathMatchingPattern(input, "test"));
        Assert.False(_fileRepository.IsPathMatchingPattern("test", input));
    }

    [Fact]
    public void IsPathMatchingPattern_ShouldHandleNullInputs()
    {
        // Act & Assert
        Assert.False(_fileRepository.IsPathMatchingPattern(null!, "test"));
        Assert.False(_fileRepository.IsPathMatchingPattern("test", null!));
    }

    [Fact]
    public void DetermineWorkspaceRoot_ShouldReturnNormalizedPath()
    {
        // Act
        var workspaceRoot = _fileRepository.DetermineWorkspaceRoot();

        // Assert
        Assert.NotNull(workspaceRoot);
        Assert.NotEmpty(workspaceRoot);
        // Should be a valid absolute path
        Assert.True(Path.IsPathRooted(workspaceRoot));
    }

    [Fact]
    public void GetWorkspaceRoot_ShouldReturnConsistentPath()
    {
        // Act
        var root1 = _fileRepository.GetWorkspaceRoot();
        var root2 = _fileRepository.GetWorkspaceRoot();

        // Assert
        Assert.Equal(root1, root2);
        Assert.True(Path.IsPathRooted(root1));
    }
}
