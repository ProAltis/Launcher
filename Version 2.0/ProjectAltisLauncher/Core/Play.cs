#region License

// The MIT License
// 
// Copyright (c) 2017 Project Altis
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ProjectAltisLauncher.Forms;
using ProjectAltisLauncher.Properties;

namespace ProjectAltisLauncher.Core
{
    public static class Play
    {
        /// <summary>
        ///     Launches the game.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="frmInstance">Form instance.</param>
        public static void LaunchGame(string username, string password, FrmMain frmInstance)
        {
            Environment.SetEnvironmentVariable("TT_USERNAME", username);
            Environment.SetEnvironmentVariable("TT_PASSWORD", password);
            Environment.SetEnvironmentVariable("TT_GAMESERVER", "gs1.projectaltis.com");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (!Settings.Default.WantsDebugWindow)
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "ProjectAltis";
            Process altis;

            try
            {
                altis = Process.Start(startInfo);
            }
            catch (Exception)
            {
                frmInstance.BeginInvoke(
                    (MethodInvoker) delegate { MessageBox.Show(frmInstance, Resources.Play_LaunchGame_Unable_to_start_Project_Altis_); });
                return;
            }

            frmInstance.BeginInvoke((MethodInvoker) delegate
            {
                List<Form> openForms = new List<Form>();

                foreach (Form f in Application.OpenForms)
                    openForms.Add(f);

                foreach (Form f in openForms)
                    if (!f.Name.Contains("FrmMain"))
                        f.Close();

                frmInstance.Hide();
            });

            altis?.WaitForExit(int.MaxValue);

            frmInstance.BeginInvoke((MethodInvoker) delegate
            {
                frmInstance.lblNowDownloading.Text = Resources.Play_LaunchGame_Thanks_for_playing_;
                frmInstance.Show();
            });
        }
    }
}