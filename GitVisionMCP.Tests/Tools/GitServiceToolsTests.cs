using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitVisionMCP.Models;
using GitVisionMCP.Services;
using GitVisionMCP.Tools;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GitVisionMCP.Tests.Tools
{
    public class GitServiceToolsTests
    {
        private readonly Mock<ILogger<GitServiceTools>> _mockLogger;
        private readonly Mock<IGitService> _mockGitService;
        private readonly Mock<ILocationService> _mockLocationService;
        private readonly Mock<IDeconstructionService> _mockDeconstructionService;
        private readonly GitServiceTools _gitServiceTools;
        private readonly List<WorkspaceFileInfo> _mockFiles;

        public GitServiceToolsTests()
        {
            _mockLogger = new Mock<ILogger<GitServiceTools>>();
            _mockGitService = new Mock<IGitService>();
            _mockLocationService = new Mock<ILocationService>();
            _mockDeconstructionService = new Mock<IDeconstructionService>();
            _gitServiceTools = new GitServiceTools(_mockGitService.Object, _mockLocationService.Object, _mockDeconstructionService.Object, _mockLogger.Object);            // Setup mock workspace root
            var mockWorkspaceRoot = Path.Combine(Path.GetTempPath(), "mock-workspace");
            _mockLocationService.Setup(m => m.GetWorkspaceRoot()).Returns(mockWorkspaceRoot);

            // Create mock file list
            _mockFiles = new List<WorkspaceFileInfo>
            {
                new()
                {
                    RelativePath = "file1.cs",
                    FileType = "cs",
                    FullPath = Path.Combine(mockWorkspaceRoot, "file1.cs"),
                    Size = 1024,
                    LastModified = DateTime.Now.AddDays(-1)
                },
                new()
                {
                    RelativePath = "file2.txt",
                    FileType = "txt",
                    FullPath = Path.Combine(mockWorkspaceRoot, "file2.txt"),
                    Size = 2048,
                    LastModified = DateTime.Now.AddDays(-2)
                },
                new()
                {
                    RelativePath = "subfolder/file3.cs",
                    FileType = "cs",
                    FullPath = Path.Combine(mockWorkspaceRoot, "subfolder/file3.cs"),
                    Size = 4096,
                    LastModified = DateTime.Now.AddDays(-3)
                },
                new()
                {
                    RelativePath = "subfolder/file4.json",
                    FileType = "json",
                    FullPath = Path.Combine(mockWorkspaceRoot, "subfolder/file4.json"),
                    Size = 512,
                    LastModified = DateTime.Now.AddDays(-4)
                }
            };

            // Setup mock location service
            _mockLocationService.Setup(m => m.GetAllFiles()).Returns(_mockFiles);
            _mockLocationService.Setup(m => m.GetAllFilesAsync()).ReturnsAsync(_mockFiles);
        }

        #region ListWorkspaceFilesAsync Tests

        [Fact]
        public async Task ListWorkspaceFilesAsync_ReturnsAllFiles_WhenNoFiltersProvided()
        {
            // Arrange

            // Act
            var result = await _gitServiceTools.ListWorkspaceFilesAsync();

            // Assert
            Assert.Equal(_mockFiles.Count, result.Count);
            Assert.Equal(_mockFiles, result);
        }
        [Fact]
        public async Task ListWorkspaceFilesAsync_FiltersFilesByType()
        {
            // Arrange
            var fileType = "cs";

            // Act
            var result = await _gitServiceTools.ListWorkspaceFilesAsync(fileType: fileType);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, file => Assert.Equal(fileType, file.FileType));
        }

        [Fact]
        public async Task ListWorkspaceFilesAsync_FiltersFilesByRelativePath()
        {
            // Arrange
            var relativePath = "subfolder";

            // Act
            var result = await _gitServiceTools.ListWorkspaceFilesAsync(relativePath: relativePath);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, file => Assert.Contains(relativePath, file.RelativePath));
        }

        [Fact]
        public async Task ListWorkspaceFilesAsync_FiltersFilesByFullPath()
        {
            // Arrange
            var fullPath = "subfolder";

            // Act
            var result = await _gitServiceTools.ListWorkspaceFilesAsync(fullPath: fullPath);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, file => Assert.Contains(fullPath, file.FullPath));
        }
        [Fact]
        public async Task ListWorkspaceFilesAsync_FiltersFilesByLastModifiedAfter()
        {
            // Arrange
            // We're providing a date string format (yyyy-MM-dd) without time component, 
            // which means time will default to midnight. This changes the behavior when comparing dates.
            var cutoffDate = DateTime.Now.AddDays(-2.5).Date;
            var lastModifiedAfter = cutoffDate.ToString("yyyy-MM-dd");

            // Act
            var result = await _gitServiceTools.ListWorkspaceFilesAsync(lastModifiedAfter: lastModifiedAfter);

            // Assert
            var expectedFiles = _mockFiles.Where(f => f.LastModified.Date >= cutoffDate).ToList();
            Assert.Equal(expectedFiles.Count, result.Count);

            // Verify each expected file is in the result
            foreach (var expectedFile in expectedFiles)
            {
                Assert.Contains(result, file => file.RelativePath == expectedFile.RelativePath);
            }
        }

        [Fact]
        public async Task ListWorkspaceFilesAsync_FiltersFilesByLastModifiedBefore()
        {
            // Arrange
            // Use a fixed date to avoid any timing issues
            var baseDate = new DateTime(2025, 7, 19); // Today's date fixed
            var cutoffDate = baseDate.AddDays(-2.5); // Should be July 16 12:00:00
            var lastModifiedBefore = cutoffDate.ToString("yyyy-MM-dd"); // "2025-07-16"

            // Update mock files to have specific dates for this test
            _mockFiles[0].LastModified = baseDate.AddDays(-1); // July 18 - should NOT match (newer)
            _mockFiles[1].LastModified = baseDate.AddDays(-2); // July 17 - should NOT match (newer)  
            _mockFiles[2].LastModified = baseDate.AddDays(-3); // July 16 - should NOT match (same date)
            _mockFiles[3].LastModified = baseDate.AddDays(-4); // July 15 - should match (older)

            // Act
            var result = await _gitServiceTools.ListWorkspaceFilesAsync(lastModifiedBefore: lastModifiedBefore);

            // Debug output
            foreach (var file in _mockFiles)
            {
                System.Console.WriteLine($"Mock file: {file.RelativePath}, LastModified: {file.LastModified:yyyy-MM-dd}, Before filter: {lastModifiedBefore}");
            }
            foreach (var file in result)
            {
                System.Console.WriteLine($"Result file: {file.RelativePath}, LastModified: {file.LastModified:yyyy-MM-dd}");
            }

            // Assert - only file4.json (July 15) should match as it's before July 16
            Assert.Single(result);
            Assert.Equal("subfolder/file4.json", result.First().RelativePath);
        }

        [Fact]
        public async Task ListWorkspaceFilesAsync_CombinesMultipleFilters()
        {
            // Arrange
            var fileType = "cs";
            var relativePath = "subfolder";

            // Act
            var result = await _gitServiceTools.ListWorkspaceFilesAsync(fileType: fileType, relativePath: relativePath);

            // Assert
            Assert.Single(result);
            Assert.Equal("subfolder/file3.cs", result.First().RelativePath);
        }

        [Fact]
        public async Task ListWorkspaceFilesAsync_HandlesExceptions()
        {
            // Arrange
            _mockLocationService.Setup(m => m.GetAllFilesAsync()).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _gitServiceTools.ListWorkspaceFilesAsync());
        }

        #endregion

        #region ReadFilteredWorkspaceFilesAsync Tests

        [Fact]
        public async Task ReadFilteredWorkspaceFilesAsync_ReturnsFileContents()
        {
            // Arrange
            // Setup mock file content
            var fileContent = "Test file content";
            var mockWorkspaceRoot = _mockLocationService.Object.GetWorkspaceRoot();

            // Mock directory and file
            Directory.CreateDirectory(mockWorkspaceRoot);
            Directory.CreateDirectory(Path.Combine(mockWorkspaceRoot, "subfolder"));

            foreach (var file in _mockFiles)
            {
                File.WriteAllText(file.FullPath, fileContent);
                // Setup mock for ReadFile to return the content
                _mockLocationService.Setup(m => m.ReadFile(file.FullPath)).Returns(fileContent);
            }

            try
            {
                // Act
                var result = await _gitServiceTools.ReadFilteredWorkspaceFilesAsync();

                // Assert
                Assert.Equal(_mockFiles.Count, result.Count);
                Assert.All(result, file => Assert.Equal(fileContent, file.Content));
                Assert.All(result, file => Assert.False(file.IsError));
            }
            finally
            {
                // Cleanup
                if (Directory.Exists(mockWorkspaceRoot))
                {
                    Directory.Delete(mockWorkspaceRoot, true);
                }
            }
        }

        [Fact]
        public async Task ReadFilteredWorkspaceFilesAsync_LimitsNumberOfFiles()
        {
            // Arrange
            var maxFiles = 2;

            // Act
            var result = await _gitServiceTools.ReadFilteredWorkspaceFilesAsync(maxFiles: maxFiles);

            // Assert
            Assert.Equal(maxFiles, result.Count);
        }

        [Fact]
        public async Task ReadFilteredWorkspaceFilesAsync_HandlesNonExistentFiles()
        {
            // Arrange
            // Ensure files do not exist on disk
            var mockWorkspaceRoot = _mockLocationService.Object.GetWorkspaceRoot();
            if (Directory.Exists(mockWorkspaceRoot))
            {
                Directory.Delete(mockWorkspaceRoot, true);
            }

            // Act
            var result = await _gitServiceTools.ReadFilteredWorkspaceFilesAsync();

            // Assert
            Assert.Equal(_mockFiles.Count, result.Count);
            Assert.All(result, file => Assert.True(file.IsError));
            Assert.All(result, file => Assert.Equal("File not found", file.ErrorMessage));
        }

        [Fact]
        public async Task ReadFilteredWorkspaceFilesAsync_SkipsFilesExceedingSizeLimit()
        {
            // Arrange
            // Setup mock file size limit to be smaller than all files
            long maxFileSize = 100;

            // Create mock directory but no files - we'll just test the size check
            var mockWorkspaceRoot = _mockLocationService.Object.GetWorkspaceRoot();
            Directory.CreateDirectory(mockWorkspaceRoot);

            try
            {
                // Act
                var result = await _gitServiceTools.ReadFilteredWorkspaceFilesAsync(maxFileSize: maxFileSize);

                // Assert
                Assert.Equal(_mockFiles.Count, result.Count);
                Assert.All(result, file => Assert.True(file.IsError));
                Assert.All(result, file => Assert.Contains("exceeds maximum allowed size", file.ErrorMessage));
            }
            finally
            {
                // Cleanup
                if (Directory.Exists(mockWorkspaceRoot))
                {
                    Directory.Delete(mockWorkspaceRoot, true);
                }
            }
        }
        [Fact]
        public async Task ReadFilteredWorkspaceFilesAsync_FiltersFilesByType()
        {
            // Arrange
            var fileType = "cs";

            // Act
            var result = await _gitServiceTools.ReadFilteredWorkspaceFilesAsync(fileType: fileType);

            // Assert
            Assert.Equal(2, result.Count); // Should have 2 CS files
            Assert.All(result, file => Assert.Equal(fileType, file.FileType));
        }

        [Fact]
        public async Task ReadFilteredWorkspaceFilesAsync_CombinesMultipleFilters()
        {
            // Arrange
            var fileType = "cs";
            var relativePath = "subfolder";

            // Act
            var result = await _gitServiceTools.ReadFilteredWorkspaceFilesAsync(
                fileType: fileType, relativePath: relativePath);

            // Assert
            Assert.Single(result);
            Assert.Equal("subfolder/file3.cs", result.First().RelativePath);
        }

        [Fact]
        public async Task ReadFilteredWorkspaceFilesAsync_HandlesExceptions()
        {
            // Arrange
            _mockLocationService.Setup(m => m.GetAllFilesAsync()).ThrowsAsync(new Exception("Test exception"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _gitServiceTools.ReadFilteredWorkspaceFilesAsync());
        }

        [Fact]
        public async Task SearchJsonFileAsync_ValidJsonFile_ReturnsResult()
        {
            // Arrange
            var jsonFilePath = "test.json";
            var jsonPath = "$.name";
            var expectedResult = "\"John Doe\"";

            _mockLocationService.Setup(m => m.SearchJsonFile(jsonFilePath, jsonPath, true, false))
                .Returns(expectedResult);

            // Act
            var result = await _gitServiceTools.SearchJsonFileAsync(jsonFilePath, jsonPath);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockLocationService.Verify(m => m.SearchJsonFile(jsonFilePath, jsonPath, true, false), Times.Once);
        }

        [Fact]
        public async Task SearchJsonFileAsync_EmptyFilePath_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _gitServiceTools.SearchJsonFileAsync("", "$.name"));
        }

        [Fact]
        public async Task SearchJsonFileAsync_EmptyJsonPath_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _gitServiceTools.SearchJsonFileAsync("test.json", ""));
        }

        [Fact]
        public async Task SearchJsonFileAsync_NoResults_ReturnsNoMatchesMessage()
        {
            // Arrange
            var jsonFilePath = "test.json";
            var jsonPath = "$.nonexistent";

            _mockLocationService.Setup(m => m.SearchJsonFile(jsonFilePath, jsonPath, true, true))
                .Returns(string.Empty);

            // Act
            var result = await _gitServiceTools.SearchJsonFileAsync(jsonFilePath, jsonPath);

            // Assert
            Assert.Equal("No matches found", result);
        }

        #endregion

        #region SearchYamlFileAsync Tests

        [Fact]
        public async Task SearchYamlFileAsync_ValidYamlFile_ReturnsResult()
        {
            // Arrange
            var yamlFilePath = "test.yaml";
            var jsonPath = "$.application.name";
            var expectedResult = "\"GitVisionMCP\"";

            _mockLocationService.Setup(m => m.SearchYamlFile(yamlFilePath, jsonPath, true, false))
                .Returns(expectedResult);

            // Act
            var result = await _gitServiceTools.SearchYamlFileAsync(yamlFilePath, jsonPath);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockLocationService.Verify(m => m.SearchYamlFile(yamlFilePath, jsonPath, true, false), Times.Once);
        }

        [Fact]
        public async Task SearchYamlFileAsync_EmptyFilePath_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _gitServiceTools.SearchYamlFileAsync("", "$.name"));
        }

        [Fact]
        public async Task SearchYamlFileAsync_EmptyJsonPath_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _gitServiceTools.SearchYamlFileAsync("test.yaml", ""));
        }

        [Fact]
        public async Task SearchYamlFileAsync_NoResults_ReturnsNoMatchesMessage()
        {
            // Arrange
            var yamlFilePath = "test.yaml";
            var jsonPath = "$.nonexistent";

            _mockLocationService.Setup(m => m.SearchYamlFile(yamlFilePath, jsonPath, true, false))
                .Returns(string.Empty);

            // Act
            var result = await _gitServiceTools.SearchYamlFileAsync(yamlFilePath, jsonPath);

            // Assert
            Assert.Equal("No matches found", result);
        }

        #endregion

        #region TransformXmlWithXsltAsync Tests

        [Fact]
        public async Task TransformXmlWithXsltAsync_ValidFiles_ReturnsTransformedResult()
        {
            // Arrange
            var xmlFilePath = "test-data.xml";
            var xsltFilePath = "transform-to-html.xslt";
            var expectedResult = "<html><head><title>Book Catalog</title>";
            _mockLocationService.Setup(x => x.TransformXmlWithXslt(xmlFilePath, xsltFilePath))
                               .Returns(expectedResult);

            // Act
            var result = await _gitServiceTools.TransformXmlWithXsltAsync(xmlFilePath, xsltFilePath);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockLocationService.Verify(x => x.TransformXmlWithXslt(xmlFilePath, xsltFilePath), Times.Once);
        }

        [Fact]
        public async Task TransformXmlWithXsltAsync_EmptyXmlFilePath_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _gitServiceTools.TransformXmlWithXsltAsync("", "transform.xslt"));
        }

        [Fact]
        public async Task TransformXmlWithXsltAsync_EmptyXsltFilePath_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                async () => await _gitServiceTools.TransformXmlWithXsltAsync("test.xml", ""));
        }

        [Fact]
        public async Task TransformXmlWithXsltAsync_TransformationFails_ReturnsFailureMessage()
        {
            // Arrange
            var xmlFilePath = "test.xml";
            var xsltFilePath = "transform.xslt";
            _mockLocationService.Setup(x => x.TransformXmlWithXslt(xmlFilePath, xsltFilePath))
                               .Returns((string?)null);

            // Act
            var result = await _gitServiceTools.TransformXmlWithXsltAsync(xmlFilePath, xsltFilePath);

            // Assert
            Assert.Equal("XSLT transformation failed", result);
        }

        [Fact]
        public async Task TransformXmlWithXsltAsync_LocationServiceThrowsFileNotFoundException_RethrowsFileNotFoundException()
        {
            // Arrange
            var xmlFilePath = "nonexistent.xml";
            var xsltFilePath = "transform.xslt";
            _mockLocationService.Setup(x => x.TransformXmlWithXslt(xmlFilePath, xsltFilePath))
                               .Throws(new FileNotFoundException("File not found"));

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(
                async () => await _gitServiceTools.TransformXmlWithXsltAsync(xmlFilePath, xsltFilePath));
        }

        #endregion
    }
}
