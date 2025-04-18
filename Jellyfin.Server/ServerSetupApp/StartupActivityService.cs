#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System.Collections.Generic;
using System.Linq;

namespace Jellyfin.Server.ServerSetupApp;

/// <summary>
/// Contains a structured list of activities that are preformed during startup that can be displayed on the Startup UI.
/// </summary>
public class StartupActivityService
{
    public StartupActivityGroup CurrentActivity { get; set; } = MainStartupActivity;

    public static MainStartupActivity MainStartupActivity { get; } = new MainStartupActivity();

    public void RefreshActivity(string key, ActivityState state)
    {
        var activity = FindActivity(key);
        if (activity is null)
        {
            return;
        }

        activity.State = state;
    }

    private StartupActivity? FindActivity(string key)
    {
        var activity = CurrentActivity;
        var activityKeyStack = new Queue<string>(key.Split("."));
        while (activity != null && activity.Key != key && activityKeyStack.TryDequeue(out var nextKey))
        {
            activity = activity.Children.FirstOrDefault(e => e.Key == nextKey);
        }

        return activity;
    }
}

public record MainStartupActivity : StartupActivityGroup
{
    public MainStartupActivity() : base("main", "Jellyfin Server startup")
    {
        Children = [
            Startup,
            Logging,
            WebUI,
            PreStartupCodeMigration,
            BuildWebHost,
            InitServices,
            RunDbMigration,
            StartupCodeMigration
        ];
    }

    public StartupActivityGroup Startup { get; set; } = new("startup", "Initialise Temporary Startup service.");

    public StartupActivityGroup Logging { get; set; } = new("logging", "Initialise Logging");

    public StartupActivityGroup WebUI { get; set; } = new("webUI", "Setup webUI");

    public StartupActivityGroup PreStartupCodeMigration { get; set; } = new("preStartupCodeMigration", "Run Pre-Startup Migrations");

    public WebHostActivity BuildWebHost { get; set; } = new WebHostActivity();

    public StartupActivityGroup InitServices { get; set; } = new("initServices", "Intitialse Services");

    public StartupActivityGroup RunDbMigration { get; set; } = new("runDbMigration", "Run Database Migrations");

    public StartupActivityGroup StartupCodeMigration { get; set; } = new("startupCodeMigration", "Run Startup Migrations");
}

public record WebHostActivity : StartupActivityGroup
{
    public WebHostActivity() : base("buildWebHost", "Build Jellyfin Webservice")
    {
        Children = [
            ConfigureServices,
            ConfigureNetwork,
            ConfigureApp
        ];
    }

    public StartupActivityGroup ConfigureServices { get; set; } = new("configServices", "Configure Services");

    public StartupActivityGroup ConfigureNetwork { get; set; } = new("configNetwork", "Configure Network settings");

    public StartupActivityGroup ConfigureApp { get; set; } = new("configApp", "Configure Application settings");
}

public record StartupActivityGroup(string Key, string Title) : StartupActivity(Key, Title)
{
    public IList<StartupActivityGroup> Children { get; set; } = [];
}

public record StartupActivity
{
    public StartupActivity(string key, string title)
    {
        Key = key;
        Title = title;
    }

    public ActivityState State { get; set; }
    public string Title { get; set; }
    public string Key { get; set; }
}

public enum ActivityState
{
    Unknown,
    InProgress,
    Done,
    Failed,
}
