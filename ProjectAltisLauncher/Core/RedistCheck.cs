using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.Win32;
using ProjectAltis.Forms;

namespace ProjectAltis.Core
{
    public class RedistCheck
    {
        private FrmMain _instance;
        public RedistCheck(FrmMain instance)
        {
            _instance = instance;
        }
        public static bool RedistInstalled()
        {
            Log.Info("Checking C++ 2010 x86 redistributable registry key");
            const string keyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Installer\Products\1D5E3C0FEDA1E123187686FED06E995A";
            const string valueName = "ProductName";
            bool result = Registry.GetValue(keyName, valueName, null) != null;
            Log.Info(result.ToString());
            return result;
        }

        public void CheckRedistHandler()
        {
            if(RedistInstalled()) return;
            DialogResult result = MessageBox.Show(
                "The Microsoft Visual C++ redistributable\n 2010 x86 was not found. It may be required to play Project Altis.\nDownload and install it now?", @"Project Altis", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(result != DialogResult.Yes) return;
            _instance.lblNowDownloading.Text = "Downloading Redist...";
            var finalDestination = Path.Combine(Path.GetTempPath(), "vcredist_x86.exe");
            using(WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += (sender, args) =>
                {
                    _instance.pbDownload.Visible = true;
                    _instance.pbDownload.Value = args.ProgressPercentage;
                };
                wc.DownloadFileCompleted += (sender, args) =>
                {
                    _instance.pbDownload.Visible = false;
                    Process.Start(finalDestination, "/passive /norestart");
                };
                _instance.pbDownload.Value = 0;
                wc.DownloadFileAsync(new Uri("https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x86.exe"), finalDestination);
                _instance.lblInfo.Text = "Downloading the C++ Redistributable...";

            }


            //Log.TryOpenUrl(
            //    "https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x86.exe");
        }

        public static bool GameDvrEnabled()
        {
            const string keyName = "HKEY_CURRENT_USER\\System\\GameConfigStore";
            const string valueName = "GameDVR_Enabled";
            return Registry.GetValue(keyName, valueName, RegistryValueKind.DWord).ToString() == "1"; 
        }
    }
}