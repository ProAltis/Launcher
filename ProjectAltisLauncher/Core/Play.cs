using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Core
{
    class Play
    {
        public static void LaunchGame(string username, string password)
        {
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            Environment.SetEnvironmentVariable("TT_GAMESERVER", "gs1.projectaltis.com");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (Properties.Settings.Default.wantsGameDebug == false)
            {
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }         
            startInfo.FileName = "ProjectAltis";
            Process.Start(startInfo);
            Application.Exit();
        }

    }
}
