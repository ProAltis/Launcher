using Newtonsoft.Json;
using ProjectAltisLauncher.Manifests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Core
{
    class AutoUpdater
    {
        /// <summary>
        /// Checks for the latest update of the launcher manifest
        /// </summary>
        /// 
        public static void CheckForUpdate()
        {
            string currentDir = Directory.GetCurrentDirectory() + "\\";
            string responseFromServer = "";
            try
            {
#if DEBUG
                responseFromServer = Data.RequestData(@"https://raw.githubusercontent.com/SodiumFine/test/master/response.json", "GET");
#endif
#if !DEBUG
                responseFromServer = Data.RequestData(@"https://projectaltis.com/api/launcherManifest", "GET");
#endif
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to check for autoupdates!");
            }
            
            string[] array = responseFromServer.Split('#'); // Seperate each json value into an index
            bool restartRequired = false;
            for (int i = 0; i < array.Length - 1; i++) // - 1 Because string split creates one extra null line D:
            {
                // First compare the file against current file and see if it needs to be updated
                manifest patchManifest = JsonConvert.DeserializeObject<manifest>(array[i]);

                WebClient client = new WebClient();
                if (Hashing.CompareSHA256(currentDir + patchManifest.filename, patchManifest.sha256))
                {
                    // File is already up to date(Possibly Launcher)
                }
#region Launcher Update
                else if (patchManifest.filename.ToLower() == "project altis launcher.exe")
                {
                    client.DownloadFile(patchManifest.url, "Launcher_New.exe");
                    try
                    {
                        File.Delete(Path.GetTempPath() + @"\updater.vbs");
                    }
                    catch (Exception) { }

                    using (StreamWriter file = new StreamWriter(Path.GetTempPath() + @"\updater.vbs", true))
                    {
#region Write Update Script
                        file.WriteLine("WScript.Sleep 250"); // Wait 250 ms for main launcher to exit
                        file.WriteLine("Set f = WScript.CreateObject(\"Scripting.FileSystemObject\")");
                        file.WriteLine("Set obj = CreateObject(\"Scripting.FileSystemObject\")");
                        file.WriteLine("obj.DeleteFile(\"{0}\")", currentDir + "Project Altis Launcher.exe"); // Deletes current launcher to prevent IO Errors
                        file.WriteLine("f.MoveFile " + "\"" + currentDir + "Launcher_New.exe" + "\", " + "\"" + currentDir + "Project Altis Launcher.exe" + "\"");
                        file.WriteLine("Set objShell = WScript.CreateObject(\"WScript.Shell\")");
                        file.WriteLine("objShell.Run(\"\"\"{0}\"\"\")", currentDir + "Project Altis Launcher");
#endregion
                        restartRequired = true;
                    }
                }
#endregion
                else
                {
                    client.DownloadFile(patchManifest.url, patchManifest.filename);
                }
            }

            if (restartRequired) // Checks if restart is required to update
            {
                Process.Start(Path.GetTempPath() + @"\updater.vbs");
                Application.Exit();
            }
        }
    }
}
