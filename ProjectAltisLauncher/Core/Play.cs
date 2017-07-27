using System;
using System.Diagnostics;
using System.Windows.Forms;
using ProjectAltis.Forms;
using System.ComponentModel;
using System.IO;

namespace ProjectAltis.Core
{
    public static class Play
    {
        public static string AltisProcessName = "ProjectAltis";

        /// <summary>
        /// Launches the game.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="frmInstance">FrmMain instance.</param>
        public static void LaunchGame(string username, string password, FrmMain frmInstance)
        {
            try
            {
                Process altis = StartAltis(username, password);
                HideAllForms();

                frmInstance?.Invoke((MethodInvoker)delegate
                {
                    if(altis == null) // Returned null process because AV removed it.
                    {
                        frmInstance.Show();
                        MessageBox.Show(frmInstance, $"The executable {AltisProcessName} has been removed. " +
                            $"It is possible that it has been removed by your Anti-Virus. " +
                            $"If so, please add an exclusion to: {Directory.GetCurrentDirectory()}\\{AltisProcessName}");
                    }
                    else
                    {
                        altis.WaitForExit();
                        Log.Info("Game process ended.");
                        frmInstance.lblNowDownloading.Text = "Thanks for playing!";
                        frmInstance.Show();
                    }
                });
            }
            catch (Win32Exception ex)
            {
                Log.Error("Win32Exception thrown. Possibly older os or file removed by AV?");
                Log.Error(ex);
                MessageBox.Show("An error has occured starting the game!\nIt is possible that you are running a legacy system or the game has been removed by your Anti-Virus.\nIt has been logged in the launcher's log.");
            }
            catch (Exception ex)
            {
                Log.Error("Error when starting game!");
                Log.Error(ex);
                MessageBox.Show("An unknown error occured when starting the game!\n It has been logged in the launcher's log.");
            }

        }

        public static Process StartAltis(string username, string password)
        {
            Log.Info("Starting Toontown: " + username);
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            Environment.SetEnvironmentVariable("TT_GAMESERVER", "gs1.projectaltis.com");
            Log.Info("Successfully set Environment variables.");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden, // Hide the console window
                FileName = AltisProcessName
            };
            string path = Directory.GetCurrentDirectory() + "\\" + AltisProcessName + ".exe";

            if (!File.Exists(path)) // File removed by Anti-Virus
            {
                Log.Error($"{AltisProcessName} has been removed! It is most likely an Anti-Virus has detected" +
                    $"it as a false positive. Exiting");
                return null;
            }
            else
            {
                return Process.Start(startInfo);
            }
        }

        public static void HideAllForms()
        {
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
        }
    }
}
