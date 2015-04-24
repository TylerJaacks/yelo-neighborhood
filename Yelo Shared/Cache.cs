using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Yelo.Shared
{
    public static class Cache
    {
        public static int Version { get { return 11; } }

        public static XBoxLocator XBoxLocator { get { return _xboxLocator; } }
        static XBoxLocator _xboxLocator = new XBoxLocator();

        const string DownloadServerURL = "http://acemods.org/remnant/archive/applications/Halo%202%20Xbox%20Apps/Yelo%20Sauce/";

        public static void CheckForUpdate()
        {
            Updater.UpdatingTasks.VersionDownloadDirectory = new Uri(DownloadServerURL);
            Updater.UpdatingTasks.UpdateDownloadDirectory = new Uri(DownloadServerURL);
            Updater.UpdatingTasks.ProgramLocation = Assembly.GetEntryAssembly().Location;
            Updater.UpdatingTasks.CurrentVersion = Cache.Version;
            Updater.UpdatingTasks.CheckForUpdates();
        }
    }
}
