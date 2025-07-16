using System.Text.Json;
using Microsoft.Extensions.Logging;
using ModelContextProtocol;
using ModelContextProtocol.Interfaces;
using GitVisionMCP.Services;

namespace GitVisionMCP.Tools;

/// <summary>
/// MCP tool for generating release documentation from git history
/// </summary>
public class DocumentationTool : IMcpTool
{
    private readonly ILogger<DocumentationTool> _logger;
    private readonly GitAnalysisService _gitAnalysisService;
    private readonly ReleaseDocumentGenerator _documentGenerator;
    private readonly MermaidDiagramGenerator _mermaidGenerator;

    public DocumentationTool(
        ILogger<DocumentationTool> logger,
        GitAnalysisService gitAnalysisService,
        ReleaseDocumentGenerator documentGenerator,
        MermaidDiagramGenerator mermaidGenerator)
    {
        _logger = logger;
        _gitAnalysisService = gitAnalysisService;
        _documentGenerator = documentGenerator;
        _mermaidGenerator = mermaidGenerator;
    }

    public string Name => "generate_release_documentation";

    public string Description => "Generate comprehensive release documentation from git history";

    public JsonElement Schema => JsonSerializer.SerializeToElement(new
    {
        type = "object",
        properties = new
        {
            outputFile = new
            {
                type = "string",
                description = "Output file path for the release document",
                @default = "RELEASE_DOCUMENT.md"
            },
            baseBranch = new
            {
                type = "string",
                description = "Base branch to compare against",
                @default = "master"
            },
            includeDiagrams = new
            {
                type = "boolean",
                description = "Include mermaid diagrams in the documentation",
                @default = true
            }
        }
    });

    public async Task<McpToolResult> ExecuteAsync(JsonElement arguments)
    {
        try
        {
            var outputFile = arguments.TryGetProperty("outputFile", out var outputProp)
                ? outputProp.GetString() ?? "RELEASE_DOCUMENT.md"
                : "RELEASE_DOCUMENT.md";

            var baseBranch = arguments.TryGetProperty("baseBranch", out var baseProp)
                ? baseProp.GetString() ?? "master"
                : "master";

            var includeDiagrams = arguments.TryGetProperty("includeDiagrams", out var diagProp)
                ? diagProp.GetBoolean()
                : true;

            _logger.LogInformation("Starting release documentation generation");

            // Get application name from repository
            var appName = await _gitAnalysisService.GetApplicationNameAsync();

            // Get current branch
            var currentBranch = await _gitAnalysisService.GetCurrentBranchAsync();

            // Compare branches to get changes
            var branchComparison = await _gitAnalysisService.CompareBranchesAsync(currentBranch, baseBranch);

            // Get git history for analysis
            var gitHistory = await _gitAnalysisService.GetGitHistoryAsync(baseBranch, currentBranch);

            // Generate mermaid diagrams if requested
            string? mermaidDiagrams = null;
            if (includeDiagrams)
            {
                var csFiles = await _gitAnalysisService.GetCSharpFilesAsync();
                mermaidDiagrams = await _mermaidGenerator.GenerateFlowchartAsync(csFiles);
            }

            // Generate the release document
            var releaseDocument = await _documentGenerator.GenerateDocumentAsync(
                appName,
                currentBranch,
                gitHistory,
                branchComparison,
                mermaidDiagrams);

            // Write to file
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), outputFile);
            await File.WriteAllTextAsync(fullPath, releaseDocument);

            _logger.LogInformation("Release documentation generated successfully: {OutputFile}", fullPath);

            return McpToolResult.Success(new
            {
                message = "Release documentation generated successfully",
                outputFile = fullPath,
                appName,
                currentBranch,
                baseBranch,
                changesAnalyzed = branchComparison.CommitCount,
                includedDiagrams = includeDiagrams
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating release documentation");
            return McpToolResult.Error($"Failed to generate release documentation: {ex.Message}");
        }
    }
}
