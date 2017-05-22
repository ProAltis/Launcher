using System;
using System.Diagnostics;
using System.Windows.Forms;
using ProjectAltis.Forms;

namespace ProjectAltis.Core
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
			try
			{
				Log.Info("Starting Toontown: " + username);
				Environment.SetEnvironmentVariable("TT_USERNAME", username);
				Environment.SetEnvironmentVariable("TT_PASSWORD", password);
				Environment.SetEnvironmentVariable("TT_GAMESERVER", "gs1.projectaltis.com");
				Log.Info("Successfully set Environment variables.");
				ProcessStartInfo startInfo = new ProcessStartInfo();
				if(!Properties.Settings.Default.wantsGameDebug)
				{
					startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				}
				startInfo.FileName = "ProjectAltis";
				Process altis = Process.Start(startInfo);

				frmInstance.BeginInvoke((MethodInvoker)delegate
				{
                    Log.Info("Game process ended.");
					frmInstance.Hide();
					altis.WaitForExit();
					frmInstance.lblNowDownloading.Text = "Thanks for playing!";
					frmInstance.Show();
				});
			}
			catch(Exception ex)
			{
				Log.Error("Error when starting game!");
				Log.Error(ex);
				MessageBox.Show("An unknown error occured when starting the game!\n It has been logged in the launcher's log.");
			}         

        }

    }
}
