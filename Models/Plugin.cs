namespace GitVisionMCP.Models;

public class Plugin
{
    public string Name { get; set; } = string.Empty;
    public string WorkingDirectory { get; set; } = string.Empty;
    public string Executable { get; set; } = string.Empty;
    public Dictionary<string, string> Environment { get; set; } = new Dictionary<string, string>();
    public bool Enabled { get; set; } = false;
    public int TimeoutMilliseconds { get; set; } = 60000;
    public Dictionary<string, string> Arguments { get; set; } = new Dictionary<string, string>();
}
