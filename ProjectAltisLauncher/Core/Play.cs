using System;
using System.Diagnostics;
using System.Windows.Forms;
using ProjectAltis.Forms;
using System.ComponentModel;

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
                if (!Properties.Settings.Default.wantsGameDebug)
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                startInfo.FileName = "ProjectAltis";
                Process altis = Process.Start(startInfo);

                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    int i1 = i; // Modified Closure for delegates. Can't directly access the index variable.

                    if (Application.OpenForms[i] is FrmMain)
                    {
                        Application.OpenForms[i]?.BeginInvoke((MethodInvoker)delegate
                        {
                            Application.OpenForms[i1]?.Hide();
                        });
                    }
                    else
                    {
                        Application.OpenForms[i]?.BeginInvoke((MethodInvoker)delegate
                        {
                            Application.OpenForms[i1]?.Dispose();
                        });
                    }
                }

                frmInstance.BeginInvoke((MethodInvoker)delegate
                {
                    altis?.WaitForExit();
                    Log.Info("Game process ended.");
                    frmInstance.lblNowDownloading.Text = "Thanks for playing!";
                    frmInstance.Show();
                });
            }
            catch (Win32Exception ex)
            {
                Log.Error("Win32Exception thrown. Possibly older os?");
                Log.Error(ex);
                MessageBox.Show("An error has occured starting the game!\nIt is possible that you are running a legacy system.\nIt has been logged in the launcher's log.");
            }
            catch (Exception ex)
            {
                Log.Error("Error when starting game!");
                Log.Error(ex);
                MessageBox.Show("An unknown error occured when starting the game!\n It has been logged in the launcher's log.");
            }

        }
    }
}
