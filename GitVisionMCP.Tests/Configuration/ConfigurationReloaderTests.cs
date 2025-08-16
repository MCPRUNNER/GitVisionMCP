using System;
using System.IO;
using GitVisionMCP.Configuration;
using GitVisionMCP.Models;
using Moq;
using Xunit;

namespace GitVisionMCP.Tests.Configuration;

public class ConfigurationReloaderTests
{
    [Fact]
    public void ReloadNow_ReplacesConfig_WhenLoaderReturnsNew()
    {
        var initial = new GitVisionConfig { Project = new Project { Name = "Initial" } };
        var updated = new GitVisionConfig { Project = new Project { Name = "Updated" } };

        var loader = new Mock<IConfigLoader>();
        loader.SetupSequence(l => l.LoadConfig())
            .Returns(initial)
            .Returns(updated);

        var reloadable = new ReloadableGitVisionConfig(initial);
        var reloader = new ConfigurationReloader(loader.Object, reloadable, null, startWatching: false);

        // trigger reload
        reloader.ReloadNow();

        Assert.Equal("Updated", reloadable.Project?.Name);
    }

    [Fact]
    public void ReloadNow_KeepsLastGood_WhenLoaderThrows()
    {
        var initial = new GitVisionConfig { Project = new Project { Name = "Initial" } };

        var loader = new Mock<IConfigLoader>();
        loader.SetupSequence(l => l.LoadConfig())
            .Returns(initial)
            .Throws(new InvalidDataException("bad config"));

        var reloadable = new ReloadableGitVisionConfig(initial);
        var reloader = new ConfigurationReloader(loader.Object, reloadable, null, startWatching: false);

        // trigger reload which will throw
        reloader.ReloadNow();

        // ensure the value remains the last good one
        Assert.Equal("Initial", reloadable.Project?.Name);
    }
}
