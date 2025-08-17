using System;
using System.IO;
using System.Timers;
using GitVisionMCP.Models;
using Serilog;

namespace GitVisionMCP.Configuration;

/// <summary>
/// Watches a configuration file and reloads it using IConfigLoader with debounce semantics.
/// Keeps the last-known-good configuration if a reload fails.
/// </summary>
public class ConfigurationReloader : IDisposable
{
    private readonly IConfigLoader _loader;
    private readonly ReloadableGitVisionConfig _target;
    private readonly System.Timers.Timer _debounceTimer;
    private readonly object _lock = new();
    private GitVisionConfig? _lastGood;
    private FileSystemWatcher? _watcher;

    /// <summary>
    /// Create a reloader. If <paramref name="watchDirectory"/> is provided and <paramref name="startWatching"/> is true,
    /// a FileSystemWatcher will be created to monitor the `config.json` filename.
    /// </summary>
    public ConfigurationReloader(IConfigLoader loader, ReloadableGitVisionConfig target, string? watchDirectory = null, bool startWatching = true, int debounceMilliseconds = 250)
    {
        _loader = loader ?? throw new ArgumentNullException(nameof(loader));
        _target = target ?? throw new ArgumentNullException(nameof(target));

        _debounceTimer = new System.Timers.Timer(debounceMilliseconds) { AutoReset = false };
        _debounceTimer.Elapsed += (_, __) => ReloadInternal();

        // Initialize from loader; keep last-good if available
        try
        {
            _lastGood = _loader.LoadConfig();
            _target.Set(_lastGood);
            Log.Information("Loaded initial configuration");
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Failed to load initial configuration");
            _lastGood = null;
        }

        if (startWatching && !string.IsNullOrEmpty(watchDirectory) && Directory.Exists(watchDirectory))
        {
            try
            {
                _watcher = new FileSystemWatcher(watchDirectory, "config.json")
                {
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
                };

                FileSystemEventHandler onChange = (_, __) => Debounce();

                _watcher.Changed += onChange;
                _watcher.Created += onChange;
                _watcher.Renamed += (_, __) => Debounce();
                _watcher.EnableRaisingEvents = true;

                AppDomain.CurrentDomain.ProcessExit += (_, _) => _watcher.Dispose();

                Log.Information("ConfigurationReloader file watcher started for {dir}", watchDirectory);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to start configuration file watcher for {dir}", watchDirectory);
            }
        }
    }

    private void Debounce()
    {
        try
        {
            // restart timer
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while scheduling config reload");
        }
    }

    private void ReloadInternal()
    {
        lock (_lock)
        {
            try
            {
                Log.Information("Attempting to reload configuration");
                var updated = _loader.LoadConfig();
                if (updated != null)
                {
                    _target.Set(updated);
                    _lastGood = updated;
                    Log.Information("Configuration reloaded successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to reload configuration; keeping last-known-good config");
            }
        }
    }

    /// <summary>
    /// For tests and manual triggers: attempt to reload configuration immediately.
    /// </summary>
    public void ReloadNow() => ReloadInternal();

    public void Dispose()
    {
        _debounceTimer.Dispose();
        _watcher?.Dispose();
    }
}
