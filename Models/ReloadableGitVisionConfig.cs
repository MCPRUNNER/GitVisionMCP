using System;
using GitVisionMCP.Configuration;

namespace GitVisionMCP.Models;

/// <summary>
/// A thread-safe, reloadable wrapper around <see cref="GitVisionConfig"/> that implements
/// <see cref="IGitVisionConfig"/> and can be updated at runtime when the config file changes.
/// </summary>
public class ReloadableGitVisionConfig : IGitVisionConfig
{
    private readonly object _lock = new();
    private GitVisionConfig? _current;

    public ReloadableGitVisionConfig(GitVisionConfig? initial)
    {
        _current = initial;
    }

    public void Set(GitVisionConfig? cfg)
    {
        lock (_lock)
        {
            _current = cfg;
        }
    }

    public Git? Git
    {
        get
        {
            lock (_lock)
            {
                return _current?.Git;
            }
        }
        set
        {
            lock (_lock)
            {
                if (_current == null)
                {
                    _current = new GitVisionConfig();
                }

                _current.Git = value;
            }
        }
    }

    public Kestrel? Kestrel
    {
        get
        {
            lock (_lock)
            {
                return _current?.Kestrel;
            }
        }
        set
        {
            lock (_lock)
            {
                if (_current == null)
                {
                    _current = new GitVisionConfig();
                }

                _current.Kestrel = value;
            }
        }
    }

    public Project? Project
    {
        get
        {
            lock (_lock)
            {
                return _current?.Project;
            }
        }
        set
        {
            lock (_lock)
            {
                if (_current == null)
                {
                    _current = new GitVisionConfig();
                }

                _current.Project = value;
            }
        }
    }

    public Settings? Settings
    {
        get
        {
            lock (_lock)
            {
                return _current?.Settings;
            }
        }
        set
        {
            lock (_lock)
            {
                if (_current == null)
                {
                    _current = new GitVisionConfig();
                }

                _current.Settings = value;
            }
        }
    }
}
