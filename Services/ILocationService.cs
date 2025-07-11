using System.Threading;
using System.Threading.Tasks;

namespace GitVisionMCP.Services;
public interface ILocationService
{
    /// <summary>
    /// Gets the workspace root directory, checking for environment variable first
    /// </summary>
    /// <returns>The workspace root directory path</returns>
    string GetWorkspaceRoot();

    /// <summary>
    /// Gets all files under the workspace root directory with relative paths and file types
    /// </summary>
    /// <returns>A list of file information including relative path and file type</returns>
    List<WorkspaceFileInfo> GetAllFiles();

    /// <summary>
    /// Saves the output of GetAllFiles() to an XML file
    /// </summary>
    /// <param name="xmlFilePath">The path where the XML file will be saved</param>
    /// <returns>True if the file was saved successfully, false otherwise</returns>
    bool SaveAllFilesToXml(string xmlFilePath);
}