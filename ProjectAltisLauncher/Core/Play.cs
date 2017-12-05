using System;
using System.Diagnostics;
using System.Windows.Forms;
using ProjectAltis.Forms;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace ProjectAltis.Core
{
    public static class Play
    {
        public static string AltisProcessName = "ProjectAltis";


        public static bool VerifyAltisSignatureShouldOpenExe()
        {
            var isTrusted = AuthenticodeTools.IsTrusted(AltisProcessName + ".exe");
            var isSignedByAltis = AuthenticodeTools.IsSignedByAltis(AltisProcessName + ".exe");
            if (!isTrusted)
            {
                MessageBox.Show($"The Project Altis file is not signed with a valid Code signing certificate!\n\nThis usually means your download was corrupt or someone is trying to trick you into running a malicious file!\n\n In most cases, pressing the download button again should work.", "Invalid file signature!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!isSignedByAltis)
            {
                MessageBox.Show($"The file {AltisProcessName + ".exe"} is authenticode-verified, however wasn't signed with the alis certificate.\n\nIf you are the new launcher/game distributor, the signature fingerprint can be changed in Core\\AuthenticodeTools.", "Invalid file signature!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

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
                if (!VerifyAltisSignatureShouldOpenExe())
                    return;
                Process altis = StartAltis(username, password);
                HideAllForms();

                frmInstance?.Invoke((MethodInvoker)delegate
                {
                    if (altis == null) // Returned null process because AV removed it.
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

        public static string GetGameserver()
        {
            return new WebClient().DownloadString("https://projectaltis.com/api/gameserver");
        }

        public static Process StartAltis(string username, string password)
        {
            Log.Info("Starting Toontown: " + username);
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            Environment.SetEnvironmentVariable("TT_GAMESERVER", GetGameserver());
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
