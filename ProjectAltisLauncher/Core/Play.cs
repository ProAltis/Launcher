using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Core
{
    public static class Play
    {
        /// <summary>
        /// Launches the game.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public static void LaunchGame(string username, string password)
        {
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            Environment.SetEnvironmentVariable("TT_GAMESERVER", "gs1.projectaltis.com");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (!Properties.Settings.Default.wantsGameDebug)
            {
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }         
            startInfo.FileName = "ProjectAltis";
            Process.Start(startInfo);
        }

    }
}
