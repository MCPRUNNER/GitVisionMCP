using System.ComponentModel;
using ModelContextProtocol.Server;

namespace GitVisionMCP.Prompts;

/// <summary>
/// Provides prompts for generating release documentation
/// </summary>
[McpServerPromptType]
public class ReleaseDocumentPrompts
{
    /// <summary>
    /// A prompt that guides the AI in creating a comprehensive release document
    /// </summary>
    [McpServerPrompt(Name = "release_document"), Description("Creates a comprehensive release document from git history")]
    public static string ReleaseDocumentPrompt() => @"
You are a professional technical writer creating release documentation.

Your task is to:
1. Get the Application Name from the git repository name (e.g., GitVisionMCP)
2. Analyze the git history provided to you
3. Organize changes into logical feature groups
4. Create a well-structured release document with the following sections:

   - Version and Release Date
   - Summary of Changes
   - New Features (detailed descriptions)
   - Enhancements (improvements to existing features)
   - Bug Fixes
   - Breaking Changes (if any)
   - Deprecated Features (if any)
   - Known Issues
   - Installation/Upgrade Instructions

Format the document in a clean, professional style suitable for technical users.
Use markdown formatting to improve readability.
Focus on providing valuable information that helps users understand the changes.
Be concise yet comprehensive.
";

    /// <summary>
    /// A prompt for creating a release document with version information
    /// </summary>
    [McpServerPrompt(Name = "release_document_with_version"), Description("Creates a release document with specific version information")]
    public static string ReleaseDocumentWithVersionPrompt(
        [Description("The version number of the release (e.g., 1.0.0)")] string version,
        [Description("The release date (e.g., 2025-07-06)")] string releaseDate) => $@"
You are a professional technical writer creating release documentation for version {version} to be released on {releaseDate}.

Your task is to:
1. Get the Application Name from the git repository name (e.g., GitVisionMCP)
2. Analyze the git history provided to you
3. Organize changes into logical feature groups
4. Create a well-structured release document with the following sections:
   - Version {version} ({releaseDate})
   - Summary of Changes
   - New Features (detailed descriptions)
   - Enhancements (improvements to existing features)
   - Bug Fixes
   - Breaking Changes (if any)
   - Deprecated Features (if any)
   - Known Issues
   - Installation/Upgrade Instructions

Format the document in a clean, professional style suitable for technical users.
Use markdown formatting to improve readability.
Focus on providing valuable information that helps users understand the changes.
Be concise yet comprehensive.
";
}
