using Newtonsoft.Json;
using ProjectAltisLauncher.Core.Manifests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Core
{
    public class AutoUpdater
    {
        private static string currentDir = Directory.GetCurrentDirectory() + @"\";

        public static void CheckForLauncherUpdate(Form instance)
        {
            bool isUpToDate;
            LauncherUpdateManifest resp = JsonConvert.DeserializeObject<LauncherUpdateManifest>(Data.RequestData("https://projectaltis.com/api/launcherversion", "GET"));

            string gameLocation = Process.GetCurrentProcess().MainModule.FileName;

            isUpToDate = (resp.version == Crypto.CalculateSHA256(gameLocation));

            if (!isUpToDate)
            {
                instance.BeginInvoke((MethodInvoker)delegate
                {
                    DialogResult result = MessageBox.Show(instance, "There is a launcher update available!\nWould you like to update?", "Update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start("AltisUpdater", resp.url);
                        Application.Exit();
                    }
                });


            }
        }
    }
}
