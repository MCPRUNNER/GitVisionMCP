
using System;
using System.Collections.Generic;
using System.Linq;
namespace GitVisionMCP.Models;
public class Files
{
    public string Filename { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

public class ConflictResult
{
    public string Filename { get; set; } = string.Empty;
    public int LineNumber { get; set; }
    public string ConflictContent { get; set; } = string.Empty;
}
