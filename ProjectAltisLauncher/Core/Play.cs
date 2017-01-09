using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ProjectAltisLauncher.Core
{
    class Play
    {
        public static void LaunchGame(string username, string password)
        {
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (Properties.Settings.Default.wantsGameDebug == false)
            {
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }         
            startInfo.FileName = "Altis";
            Process.Start(startInfo);
        }

    }
}
