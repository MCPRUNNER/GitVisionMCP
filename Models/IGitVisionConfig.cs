namespace GitVisionMCP.Models;

public interface IGitVisionConfig
{
    Git? Git
    {
        get;
        set;
    }
    Kestrel? Kestrel
    {
        get;
        set;
    }
    Project? Project
    {
        get;
        set;
    }
    Settings? Settings
    {
        get;
        set;
    }
}