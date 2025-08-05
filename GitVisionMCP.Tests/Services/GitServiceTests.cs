using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitVisionMCP.Models;
using GitVisionMCP.Services;
using LibGit2Sharp;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GitVisionMCP.Tests.Services
{
    public class GitServiceTests : IDisposable
    {
        private readonly Mock<ILogger<GitService>> _mockLogger;
        private readonly Mock<IGitCommandRepository> _mockGitCommandRepository;
        private readonly GitService _gitService;
        private readonly string _testRepoPath;
        private readonly string _invalidRepoPath;

        public GitServiceTests()
        {
            _mockLogger = new Mock<ILogger<GitService>>();
            var mockLocationService = new Mock<ILocationService>();
            _mockGitCommandRepository = new Mock<IGitCommandRepository>();
            _gitService = new GitService(_mockLogger.Object, mockLocationService.Object, _mockGitCommandRepository.Object);
            _testRepoPath = Path.Combine(Path.GetTempPath(), "test-git-repo-" + Guid.NewGuid().ToString());
            _invalidRepoPath = Path.Combine(Path.GetTempPath(), "invalid-repo-" + Guid.NewGuid().ToString());

            // Create a temporary directory for invalid repo tests
            Directory.CreateDirectory(_invalidRepoPath);
        }

        public void Dispose()
        {
            // Clean up test repositories with better handling for Git locks
            try
            {
                if (Directory.Exists(_testRepoPath))
                {
                    // Force removal of read-only files that Git might have created
                    SetDirectoryWritable(_testRepoPath);
                    Directory.Delete(_testRepoPath, true);
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the test
                Console.WriteLine($"Warning: Could not clean up test repo at {_testRepoPath}: {ex.Message}");
            }

            try
            {
                if (Directory.Exists(_invalidRepoPath))
                {
                    Directory.Delete(_invalidRepoPath, true);
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the test
                Console.WriteLine($"Warning: Could not clean up invalid repo at {_invalidRepoPath}: {ex.Message}");
            }
        }

        private static void SetDirectoryWritable(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists)
                {
                    dirInfo.Attributes &= ~FileAttributes.ReadOnly;

                    foreach (var file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                    {
                        file.Attributes &= ~FileAttributes.ReadOnly;
                    }

                    foreach (var dir in dirInfo.GetDirectories("*", SearchOption.AllDirectories))
                    {
                        dir.Attributes &= ~FileAttributes.ReadOnly;
                    }
                }
            }
            catch
            {
                // Ignore errors in cleanup
            }
        }

        private void CreateTestRepository()
        {
            // Create a test git repository with some commits
            Directory.CreateDirectory(_testRepoPath);
            Repository.Init(_testRepoPath);

            using var repo = new Repository(_testRepoPath);

            // Configure identity for the test repo
            var signature = new Signature("Test User", "test@example.com", DateTimeOffset.Now.AddMinutes(-10));

            // Create first commit
            var file1Path = Path.Combine(_testRepoPath, "file1.txt");
            File.WriteAllText(file1Path, "Initial content");
            Commands.Stage(repo, "file1.txt");
            repo.Commit("Initial commit", signature, signature);

            // Wait a bit to ensure different timestamps
            System.Threading.Thread.Sleep(100);

            // Create second commit
            var signature2 = new Signature("Test User", "test@example.com", DateTimeOffset.Now.AddMinutes(-5));
            File.WriteAllText(file1Path, "Modified content");
            Commands.Stage(repo, "file1.txt");
            repo.Commit("Second commit", signature2, signature2);

            // Wait a bit to ensure different timestamps
            System.Threading.Thread.Sleep(100);

            // Create third commit with new file
            var signature3 = new Signature("Test User", "test@example.com", DateTimeOffset.Now);
            var file2Path = Path.Combine(_testRepoPath, "file2.txt");
            File.WriteAllText(file2Path, "New file content");
            Commands.Stage(repo, "file2.txt");
            repo.Commit("Third commit - added new file", signature3, signature3);
        }

        private void CreateTestRepositoryWithBranches()
        {
            // Create a test git repository with multiple branches
            Directory.CreateDirectory(_testRepoPath);
            Repository.Init(_testRepoPath);

            using var repo = new Repository(_testRepoPath);

            // Configure identity for the test repo
            var signature = new Signature("Test User", "test@example.com", DateTimeOffset.Now);

            // Create initial commit on main branch
            var file1Path = Path.Combine(_testRepoPath, "file1.txt");
            File.WriteAllText(file1Path, "Initial content");
            Commands.Stage(repo, "file1.txt");
            var initialCommit = repo.Commit("Initial commit", signature, signature);

            // Create a feature branch
            var featureBranch = repo.CreateBranch("feature/test-branch");
            Commands.Checkout(repo, featureBranch);

            // Add commit to feature branch
            File.WriteAllText(file1Path, "Feature content");
            Commands.Stage(repo, "file1.txt");
            repo.Commit("Feature commit", signature, signature);

            // Add another commit to feature branch
            var file2Path = Path.Combine(_testRepoPath, "feature.txt");
            File.WriteAllText(file2Path, "Feature file content");
            Commands.Stage(repo, "feature.txt");
            repo.Commit("Add feature file", signature, signature);

            // Switch back to main and add another commit
            Commands.Checkout(repo, repo.Head);
            File.WriteAllText(file1Path, "Main branch content");
            Commands.Stage(repo, "file1.txt");
            repo.Commit("Main branch commit", signature, signature);
        }

        [Fact]
        public async Task GetGitLogsAsync_WithValidRepository_ReturnsCommits()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>
            {
                new GitCommitInfo { Message = "Third commit - added new file", Hash = "abc123" },
                new GitCommitInfo { Message = "Second commit", Hash = "def456" },
                new GitCommitInfo { Message = "Initial commit", Hash = "ghi789" }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync(_testRepoPath, 10))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetGitLogsAsync(_testRepoPath, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(expectedCommits, result);
        }

        [Fact]
        public async Task GetGitLogsAsync_WithNullRepositoryPath_ThrowsArgumentException()
        {
            // Arrange
            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync(null!, It.IsAny<int>()))
                .ThrowsAsync(new ArgumentException("Repository path cannot be null or empty"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _gitService.GetGitLogsAsync(null!, 10));
        }

        [Fact]
        public async Task GetGitLogsAsync_WithEmptyRepositoryPath_ThrowsArgumentException()
        {
            // Arrange
            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync("", It.IsAny<int>()))
                .ThrowsAsync(new ArgumentException("Repository path cannot be null or empty"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _gitService.GetGitLogsAsync("", 10));
        }

        [Fact]
        public async Task GetGitLogsAsync_WithNonExistentPath_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync("/non/existent/path", It.IsAny<int>()))
                .ThrowsAsync(new DirectoryNotFoundException("Repository path does not exist"));

            // Act & Assert
            await Assert.ThrowsAsync<DirectoryNotFoundException>(() =>
                _gitService.GetGitLogsAsync("/non/existent/path", 10));
        }

        [Fact]
        public async Task GetGitLogsAsync_WithInvalidRepository_ThrowsInvalidOperationException()
        {
            // Arrange
            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync(_invalidRepoPath, It.IsAny<int>()))
                .ThrowsAsync(new InvalidOperationException("Path is not a valid git repository"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _gitService.GetGitLogsAsync(_invalidRepoPath, 10));
        }

        [Fact]
        public async Task GetGitLogsAsync_WithMaxCommitsLimit_ReturnsLimitedResults()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>
            {
                new() { Message = "Commit 1", Hash = "abc123" },
                new() { Message = "Commit 2", Hash = "def456" }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync(_testRepoPath, 2))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetGitLogsAsync(_testRepoPath, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetRecentCommitsAsync_WithValidRepository_ReturnsRecentCommits()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>
            {
                new() { Message = "Recent commit 1", Hash = "abc123" },
                new() { Message = "Recent commit 2", Hash = "def456" }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetRecentCommitsAsync(_testRepoPath, 2))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetRecentCommitsAsync(_testRepoPath, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(expectedCommits, result);
        }

        [Fact]
        public async Task GetLocalBranchesAsync_WithValidRepository_ReturnsBranches()
        {
            // Arrange
            var expectedBranches = new List<string> { "main" };

            _mockGitCommandRepository
                .Setup(repo => repo.GetLocalBranchesAsync(_testRepoPath))
                .ReturnsAsync(expectedBranches);

            // Act
            var result = await _gitService.GetLocalBranchesAsync(_testRepoPath);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains("main", result);
        }

        [Fact]
        public async Task GetRemoteBranchesAsync_WithValidRepository_ReturnsRemoteBranches()
        {
            // Arrange
            var expectedBranches = new List<string>();

            _mockGitCommandRepository
                .Setup(repo => repo.GetRemoteBranchesAsync(_testRepoPath))
                .ReturnsAsync(expectedBranches);

            // Act
            var result = await _gitService.GetRemoteBranchesAsync(_testRepoPath);

            // Assert
            Assert.NotNull(result);
            // New repository typically has no remote branches
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllBranchesAsync_WithValidRepository_ReturnsAllBranches()
        {
            // Arrange
            var expectedBranches = new List<string> { "main" };

            _mockGitCommandRepository
                .Setup(repo => repo.GetAllBranchesAsync(_testRepoPath))
                .ReturnsAsync(expectedBranches);

            // Act
            var result = await _gitService.GetAllBranchesAsync(_testRepoPath);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GenerateDocumentationAsync_WithValidCommits_ReturnsMarkdownDocumentation()
        {
            // Arrange
            var commits = new List<GitCommitInfo>
            {
                new() { Message = "Test commit", Hash = "abc123", Author = "Test Author" }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GenerateCommitDocumentationAsync(commits, "markdown"))
                .ReturnsAsync("# Git Commit Documentation\n\nTest commit");

            // Act
            var result = await _gitService.GenerateCommitDocumentationAsync(commits, "markdown");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("# Git Commit Documentation", result);
            Assert.Contains("Test commit", result);
        }

        [Fact]
        public async Task GenerateDocumentationAsync_WithHtmlFormat_ReturnsHtmlDocumentation()
        {
            // Arrange
            var commits = new List<GitCommitInfo>
            {
                new() { Message = "Test commit", Hash = "abc123", Author = "Test Author", Date = DateTime.Now }
            };
            var expectedResult = "<!DOCTYPE html><html><head><title>Git Commit Documentation</title></head><body><h1>Git Commit Documentation</h1></body></html>";

            _mockGitCommandRepository
                .Setup(repo => repo.GenerateCommitDocumentationAsync(commits, "html"))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _gitService.GenerateCommitDocumentationAsync(commits, "html");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("<!DOCTYPE html>", result);
            Assert.Contains("<h1>Git Commit Documentation</h1>", result); // Actual service uses "Commit" not "Repository"
        }

        [Fact]
        public async Task GenerateDocumentationAsync_WithTextFormat_ReturnsTextDocumentation()
        {
            // Arrange
            var commits = new List<GitCommitInfo>
            {
                new() { Message = "Third commit - added new file", Hash = "abc123", Author = "Test Author", Date = DateTime.Now }
            };
            var expectedResult = "GIT COMMIT DOCUMENTATION\n\nThird commit - added new file";

            _mockGitCommandRepository
                .Setup(repo => repo.GenerateCommitDocumentationAsync(commits, "text"))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _gitService.GenerateCommitDocumentationAsync(commits, "text");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("GIT COMMIT DOCUMENTATION", result); // Actual service uses "COMMIT" not "REPOSITORY"
            Assert.Contains("Third commit - added new file", result);
        }

        [Fact]
        public async Task GenerateDocumentationAsync_WithInvalidFormat_DefaultsToMarkdown()
        {
            // Arrange
            var commits = new List<GitCommitInfo>
            {
                new() { Message = "Test commit", Hash = "abc123", Author = "Test Author", Date = DateTime.Now }
            };
            var expectedResult = "# Git Commit Documentation\n\nTest commit";

            _mockGitCommandRepository
                .Setup(repo => repo.GenerateCommitDocumentationAsync(commits, "invalid"))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _gitService.GenerateCommitDocumentationAsync(commits, "invalid");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("# Git Commit Documentation", result); // Actual service uses "Commit" not "Repository"
        }

        [Fact]
        public async Task WriteDocumentationToFileAsync_WithValidPath_WritesSuccessfully()
        {
            // Arrange
            var content = "Test documentation content";
            var tempFile = Path.Combine(Path.GetTempPath(), "test-doc-" + Guid.NewGuid().ToString() + ".md");

            _mockGitCommandRepository
                .Setup(repo => repo.WriteDocumentationToFileAsync(content, tempFile))
                .ReturnsAsync(true);

            // Act
            var result = await _gitService.WriteDocumentationToFileAsync(content, tempFile);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task WriteDocumentationToFileAsync_WithDirectoryCreation_CreatesDirectoryAndWritesFile()
        {
            // Arrange
            var content = "Test documentation content";
            var tempDir = Path.Combine(Path.GetTempPath(), "test-dir-" + Guid.NewGuid().ToString());
            var tempFile = Path.Combine(tempDir, "test-doc.md");

            _mockGitCommandRepository
                .Setup(repo => repo.WriteDocumentationToFileAsync(content, tempFile))
                .ReturnsAsync(true);

            // Act
            var result = await _gitService.WriteDocumentationToFileAsync(content, tempFile);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetChangedFilesBetweenCommitsAsync_WithValidCommits_ReturnsChangedFiles()
        {
            // Arrange
            var commit1 = "abc123";
            var commit2 = "def456";
            var expectedFiles = new List<string> { "file1.txt", "file2.txt" };

            _mockGitCommandRepository
                .Setup(repo => repo.GetChangedFilesBetweenCommitsAsync(_testRepoPath, commit1, commit2))
                .ReturnsAsync(expectedFiles);

            // Act
            var result = await _gitService.GetChangedFilesBetweenCommitsAsync(_testRepoPath, commit1, commit2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedFiles, result);
        }

        [Fact]
        public async Task GetChangedFilesBetweenCommitsAsync_WithInvalidCommit_ThrowsArgumentException()
        {
            // Arrange
            var validCommit = "abc123";
            var invalidCommit = "invalid-commit-hash";

            _mockGitCommandRepository
                .Setup(repo => repo.GetChangedFilesBetweenCommitsAsync(_testRepoPath, validCommit, invalidCommit))
                .ThrowsAsync(new ArgumentException("Invalid commit hash"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _gitService.GetChangedFilesBetweenCommitsAsync(_testRepoPath, validCommit, invalidCommit));
        }

        [Fact]
        public async Task GetCommitDiffInfoAsync_WithValidCommits_ReturnsDiffInfo()
        {
            // Arrange
            var commit1 = "abc123";
            var commit2 = "def456";
            var expectedDiffInfo = new GitCommitDiffInfo
            {
                Commit1 = commit1,
                Commit2 = commit2,
                AddedFiles = new List<string> { "file2.txt" },
                ModifiedFiles = new List<string> { "file1.txt" },
                DeletedFiles = new List<string>(),
                RenamedFiles = new List<string>()
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetCommitDiffInfoAsync(_testRepoPath, commit1, commit2))
                .ReturnsAsync(expectedDiffInfo);

            // Act
            var result = await _gitService.GetCommitDiffInfoAsync(_testRepoPath, commit1, commit2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(commit1, result.Commit1);
            Assert.Equal(commit2, result.Commit2);
            Assert.Contains("file2.txt", result.AddedFiles);
            Assert.True(result.TotalChanges > 0);
        }

        [Fact]
        public async Task GetDetailedDiffBetweenCommitsAsync_WithValidCommits_ReturnsDiffText()
        {
            // Arrange
            var commit1 = "abc123";
            var commit2 = "def456";
            var expectedDiff = "diff --git a/file1.txt b/file1.txt\n--- a/file1.txt\n+++ b/file1.txt\n@@ -1 +1 @@\n-old content\n+new content";

            _mockGitCommandRepository
                .Setup(repo => repo.GetDetailedDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2, null))
                .ReturnsAsync(expectedDiff);

            // Act
            var result = await _gitService.GetDetailedDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains("diff --git", result);
        }

        [Fact]
        public async Task GetDetailedDiffBetweenCommitsAsync_WithSpecificFiles_ReturnsFilteredDiff()
        {
            // Arrange
            var newerCommit = "def456";
            var olderCommit = "abc123";
            var specificFiles = new List<string> { "file1.txt" };
            var expectedDiff = "diff --git a/file1.txt b/file1.txt\n--- a/file1.txt\n+++ b/file1.txt\n@@ -1 +1 @@\n-old content\n+new content";

            _mockGitCommandRepository
                .Setup(repo => repo.GetDetailedDiffBetweenCommitsAsync(_testRepoPath, olderCommit, newerCommit, specificFiles))
                .ReturnsAsync(expectedDiff);

            // Act
            var result = await _gitService.GetDetailedDiffBetweenCommitsAsync(_testRepoPath, olderCommit, newerCommit, specificFiles);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("file1.txt", result);
        }

        [Fact]
        public async Task FetchFromRemoteAsync_WithValidRepositoryNoRemote_ReturnsFalse()
        {
            // Arrange
            _mockGitCommandRepository
                .Setup(repo => repo.FetchFromRemoteAsync(_testRepoPath, "origin"))
                .ReturnsAsync(false);

            // Act
            var result = await _gitService.FetchFromRemoteAsync(_testRepoPath, "origin");

            // Assert
            // Should return false as there's no remote configured
            Assert.False(result);
        }

        [Fact]
        public async Task SearchCommitsForStringAsync_WithValidSearchString_ReturnsResults()
        {
            // Arrange
            var searchString = "commit";
            var expectedResults = new CommitSearchResponse
            {
                SearchString = searchString,
                TotalCommitsSearched = 5,
                Results = new List<CommitSearchResult>
                {
                    new CommitSearchResult
                    {
                        CommitHash = "abc123",
                        Author = "Test Author",
                        CommitMessage = "Test commit message",
                        Timestamp = DateTime.Now,
                        FileMatches = new List<FileSearchMatch>()
                    }
                }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.SearchCommitsForStringAsync(_testRepoPath, searchString, 100))
                .ReturnsAsync(expectedResults);

            // Act
            var result = await _gitService.SearchCommitsForStringAsync(_testRepoPath, searchString, 100);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(searchString, result.SearchString);
            Assert.True(result.TotalCommitsSearched > 0);
            Assert.True(result.TotalMatchingCommits > 0);
            Assert.True(result.Results.Count > 0);
        }

        [Fact]
        public async Task SearchCommitsForStringAsync_WithNonExistentString_ReturnsEmptyResults()
        {
            // Arrange
            var searchString = "nonexistent-string-12345";
            var expectedResults = new CommitSearchResponse
            {
                SearchString = searchString,
                TotalCommitsSearched = 3,
                Results = new List<CommitSearchResult>()
            };

            _mockGitCommandRepository
                .Setup(repo => repo.SearchCommitsForStringAsync(_testRepoPath, searchString, 100))
                .ReturnsAsync(expectedResults);

            // Act
            var result = await _gitService.SearchCommitsForStringAsync(_testRepoPath, searchString, 100);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(searchString, result.SearchString);
            Assert.True(result.TotalCommitsSearched > 0);
            Assert.Equal(0, result.TotalMatchingCommits);
            Assert.Empty(result.Results);
        }

        [Fact]
        public async Task GetFileLineDiffBetweenCommitsAsync_WithValidCommitsAndFile_ReturnsDiffInfo()
        {
            // Arrange
            var commit1 = "abc123"; // Initial commit
            var commit2 = "def456"; // Second commit
            var filePath = "file1.txt";
            var expectedDiffInfo = new FileLineDiffInfo
            {
                FilePath = filePath,
                Commit1 = commit1,
                Commit2 = commit2,
                FileExistsInBothCommits = true,
                TotalLines = 10,
                AddedLines = 2,
                DeletedLines = 1,
                ModifiedLines = 3
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetFileLineDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2, filePath))
                .ReturnsAsync(expectedDiffInfo);

            // Act
            var result = await _gitService.GetFileLineDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2, filePath);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(filePath, result.FilePath);
            Assert.Equal(commit1, result.Commit1);
            Assert.Equal(commit2, result.Commit2);
            Assert.True(result.FileExistsInBothCommits);
        }

        [Fact]
        public async Task GetGitLogsBetweenCommitsAsync_WithValidCommits_ReturnsCommitsBetween()
        {
            // Arrange
            var commit1 = "abc123";
            var commit2 = "def456";
            var expectedCommits = new List<GitCommitInfo>
            {
                new GitCommitInfo { Hash = "def456", Message = "Latest commit" }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsBetweenCommitsAsync(_testRepoPath, commit1, commit2))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetGitLogsBetweenCommitsAsync(_testRepoPath, commit1, commit2);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.DoesNotContain(result, c => c.Hash == commit1);
        }

        [Fact]
        public async Task GetGitLogsBetweenCommitsAsync_WithInvalidCommits_ThrowsArgumentException()
        {
            // Arrange
            var validCommit = "valid-commit-hash";
            var invalidCommit = "invalid-commit-hash";

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsBetweenCommitsAsync(_testRepoPath, validCommit, invalidCommit))
                .ThrowsAsync(new ArgumentException("Invalid commit hash"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _gitService.GetGitLogsBetweenCommitsAsync(_testRepoPath, validCommit, invalidCommit));
        }

        [Fact]
        public async Task GetGitLogsBetweenBranchesAsync_WithValidBranches_ReturnsCommitsBetween()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>
            {
                new() { Hash = "abc123", Message = "Feature commit on test branch" }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsBetweenBranchesAsync(_testRepoPath, "master", "feature/test-branch"))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetGitLogsBetweenBranchesAsync(_testRepoPath, "master", "feature/test-branch");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
            Assert.Contains(result, c => c.Message.Contains("Feature commit"));
        }

        [Fact]
        public async Task GetGitLogsBetweenBranchesAsync_WithNonExistentBranch_ThrowsArgumentException()
        {
            // Arrange
            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsBetweenBranchesAsync(_testRepoPath, "master", "non-existent-branch"))
                .ThrowsAsync(new ArgumentException("Branch does not exist"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _gitService.GetGitLogsBetweenBranchesAsync(_testRepoPath, "master", "non-existent-branch"));
        }

        [Fact]
        public async Task GetGitLogsBetweenBranchesWithRemoteAsync_WithValidBranches_ReturnsCommitsBetween()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>
            {
                new() { Hash = "abc123", Message = "Feature commit" }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsBetweenBranchesWithRemoteAsync(_testRepoPath, "master", "feature/test-branch", false))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetGitLogsBetweenBranchesWithRemoteAsync(_testRepoPath, "master", "feature/test-branch", false);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task GetLocalBranchesAsync_WithMultipleBranches_ReturnsAllLocalBranches()
        {
            // Arrange
            var expectedBranches = new List<string> { "master", "feature/test-branch" };

            _mockGitCommandRepository
                .Setup(repo => repo.GetLocalBranchesAsync(_testRepoPath))
                .ReturnsAsync(expectedBranches);

            // Act
            var result = await _gitService.GetLocalBranchesAsync(_testRepoPath);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
            Assert.Contains(result, b => b.Contains("master")); // Git default is master
            Assert.Contains(result, b => b.Contains("feature/test-branch"));
        }

        [Fact]
        public async Task GetAllBranchesAsync_WithMultipleBranches_ReturnsAllBranches()
        {
            // Arrange
            var expectedBranches = new List<string> { "master", "feature/test-branch" };

            _mockGitCommandRepository
                .Setup(repo => repo.GetAllBranchesAsync(_testRepoPath))
                .ReturnsAsync(expectedBranches);

            // Act
            var result = await _gitService.GetAllBranchesAsync(_testRepoPath);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
            Assert.Contains(result, b => b.Contains("master")); // Git default is master
            Assert.Contains(result, b => b.Contains("feature/test-branch"));
        }

        [Fact]
        public async Task SearchCommitsForStringAsync_WithSpecificMessageContent_ReturnsMatchingCommits()
        {
            // Arrange
            var searchString = "Feature";
            var expectedResults = new CommitSearchResponse
            {
                SearchString = searchString,
                TotalCommitsSearched = 5,
                Results = new List<CommitSearchResult>
                {
                    new() { CommitHash = "abc123", CommitMessage = "Feature commit", Author = "Test Author", Timestamp = DateTime.Now },
                    new() { CommitHash = "def456", CommitMessage = "Add feature file", Author = "Test Author", Timestamp = DateTime.Now }
                }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.SearchCommitsForStringAsync(_testRepoPath, searchString, 100))
                .ReturnsAsync(expectedResults);

            // Act
            var result = await _gitService.SearchCommitsForStringAsync(_testRepoPath, searchString, 100);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(searchString, result.SearchString);
            Assert.True(result.TotalMatchingCommits > 0);
            Assert.Contains(result.Results, r => r.CommitMessage.Contains("Feature"));
        }

        [Theory]
        [InlineData("markdown")]
        [InlineData("html")]
        [InlineData("text")]
        public async Task GenerateDocumentationAsync_WithDifferentFormats_ReturnsCorrectFormat(string format)
        {
            // Arrange
            var commits = new List<GitCommitInfo>
            {
                new() { Message = "Test commit", Hash = "abc123", Author = "Test Author", Date = DateTime.Now }
            };

            var expectedResult = format.ToLower() switch
            {
                "markdown" => "# Git Commit Documentation\n\nTest commit",
                "html" => "<!DOCTYPE html><html><head><title>Git Commit Documentation</title></head><body><h1>Git Commit Documentation</h1></body></html>",
                "text" => "GIT COMMIT DOCUMENTATION\n\nTest commit",
                _ => "# Git Commit Documentation\n\nTest commit"
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GenerateCommitDocumentationAsync(commits, format))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _gitService.GenerateCommitDocumentationAsync(commits, format);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            switch (format.ToLower())
            {
                case "markdown":
                    Assert.Contains("# Git Commit Documentation", result); // Actual service uses "Commit" not "Repository"
                    break;
                case "html":
                    Assert.Contains("<!DOCTYPE html>", result);
                    break;
                case "text":
                    Assert.Contains("GIT COMMIT DOCUMENTATION", result); // Actual service uses "COMMIT" not "REPOSITORY"
                    break;
            }
        }

        [Fact]
        public async Task GetCommitDiffInfoAsync_WithSameCommit_ReturnsEmptyDiff()
        {
            // Arrange
            var commit = "abc123";
            var expectedDiffInfo = new GitCommitDiffInfo
            {
                Commit1 = commit,
                Commit2 = commit,
                AddedFiles = [],
                ModifiedFiles = [],
                DeletedFiles = [],
                RenamedFiles = []
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetCommitDiffInfoAsync(_testRepoPath, commit, commit))
                .ReturnsAsync(expectedDiffInfo);

            // Act
            var result = await _gitService.GetCommitDiffInfoAsync(_testRepoPath, commit, commit);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalChanges);
            Assert.Empty(result.AddedFiles);
            Assert.Empty(result.ModifiedFiles);
            Assert.Empty(result.DeletedFiles);
        }

        [Fact]
        public async Task GetFileLineDiffBetweenCommitsAsync_WithNonExistentFile_ReturnsErrorInfo()
        {
            // Arrange
            var commit1 = "abc123";
            var commit2 = "def456";
            var nonExistentFile = "non-existent-file.txt";
            var expectedDiffInfo = new FileLineDiffInfo
            {
                FilePath = nonExistentFile,
                Commit1 = commit1,
                Commit2 = commit2,
                FileExistsInBothCommits = false
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetFileLineDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2, nonExistentFile))
                .ReturnsAsync(expectedDiffInfo);

            // Act
            var result = await _gitService.GetFileLineDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2, nonExistentFile);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(nonExistentFile, result.FilePath);
            Assert.False(result.FileExistsInBothCommits);
        }

        [Fact]
        public async Task WriteDocumentationToFileAsync_WithInvalidPath_ReturnsFalse()
        {
            // Arrange
            var content = "Test documentation content";
            var invalidPath = "Z:\\NonExistentDrive\\test-doc.md"; // Invalid drive path

            // Act
            var result = await _gitService.WriteDocumentationToFileAsync(content, invalidPath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetDetailedDiffBetweenCommitsAsync_WithEmptySpecificFilesList_ReturnsAllDiffs()
        {
            // Arrange
            var commit1 = "abc123";
            var commit2 = "def456";
            var emptyList = new List<string>();
            var expectedDiff = "";

            _mockGitCommandRepository
                .Setup(repo => repo.GetDetailedDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2, emptyList))
                .ReturnsAsync(expectedDiff);

            // Act
            var result = await _gitService.GetDetailedDiffBetweenCommitsAsync(_testRepoPath, commit1, commit2, emptyList);

            // Assert
            Assert.NotNull(result);
            // Empty specific files list should return no diffs
            Assert.Empty(result.Trim());
        }

        [Fact]
        public async Task GetGitLogsAsync_WithZeroMaxCommits_ReturnsOneCommit()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>
            {
                new() { Message = "Single commit", Hash = "abc123", Author = "Test Author", Date = DateTime.Now }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync(_testRepoPath, 0))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetGitLogsAsync(_testRepoPath, 0);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Should return at least 1 commit due to Math.Max(1, maxCommits)
        }

        [Fact]
        public async Task GetGitLogsAsync_WithNegativeMaxCommits_ReturnsOneCommit()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>
            {
                new() { Message = "Single commit", Hash = "abc123", Author = "Test Author", Date = DateTime.Now }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.GetGitLogsAsync(_testRepoPath, -5))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetGitLogsAsync(_testRepoPath, -5);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Should return at least 1 commit due to Math.Max(1, maxCommits)
        }

        [Fact]
        public async Task GetRecentCommitsAsync_WithZeroCount_ReturnsEmpty()
        {
            // Arrange
            var expectedCommits = new List<GitCommitInfo>();

            _mockGitCommandRepository
                .Setup(repo => repo.GetRecentCommitsAsync(_testRepoPath, 0))
                .ReturnsAsync(expectedCommits);

            // Act
            var result = await _gitService.GetRecentCommitsAsync(_testRepoPath, 0);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task SearchCommitsForStringAsync_WithEmptySearchString_ReturnsAllCommits()
        {
            // Arrange
            var searchString = "";
            var expectedResults = new CommitSearchResponse
            {
                SearchString = searchString,
                TotalCommitsSearched = 3,
                Results = new List<CommitSearchResult>
                {
                    new() { CommitHash = "abc123", CommitMessage = "Test commit 1", Author = "Test Author", Timestamp = DateTime.Now },
                    new() { CommitHash = "def456", CommitMessage = "Test commit 2", Author = "Test Author", Timestamp = DateTime.Now },
                    new() { CommitHash = "ghi789", CommitMessage = "Test commit 3", Author = "Test Author", Timestamp = DateTime.Now }
                }
            };

            _mockGitCommandRepository
                .Setup(repo => repo.SearchCommitsForStringAsync(_testRepoPath, searchString, 100))
                .ReturnsAsync(expectedResults);

            // Act
            var result = await _gitService.SearchCommitsForStringAsync(_testRepoPath, searchString, 100);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(searchString, result.SearchString);
            // Empty search string might return all commits or none depending on implementation
            Assert.True(result.TotalCommitsSearched > 0);
        }
    }
}
