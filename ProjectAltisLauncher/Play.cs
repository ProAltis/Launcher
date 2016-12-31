using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ProjectAltisLauncher
{
    class Play
    {
        public static void LaunchGame(string username, string password)
        {
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "Altis";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(startInfo);
        }

    }
}
