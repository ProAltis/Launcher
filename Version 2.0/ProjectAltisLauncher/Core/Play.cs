using ProjectAltisLauncher.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static void LaunchGame(string username, string password, FrmMain frmInstance)
        {
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            Environment.SetEnvironmentVariable("TT_GAMESERVER", "gs1.projectaltis.com");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (!Properties.Settings.Default.WantsDebugWindow)
            {
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }
            startInfo.FileName = "ProjectAltis";
            Process altis = Process.Start(startInfo);

            frmInstance.BeginInvoke((MethodInvoker)delegate
            {
                List<Form> openForms = new List<Form>();

                foreach (Form f in Application.OpenForms)
                {
                    openForms.Add(f);
                }                 

                foreach (Form f in openForms)
                {
                    if (!f.Name.Contains("Launcher"))
                    {
                        f.Close();
                    }                 
                }
                frmInstance.Hide();
                altis.WaitForExit();
                frmInstance.lblNowDownloading.Text = "Thanks for playing!";
                frmInstance.Show();
            });

        }
    }
}
