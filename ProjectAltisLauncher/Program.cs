using System;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using ProjectAltis.Enums;
using ProjectAltis.Forms;
using Squirrel;

namespace ProjectAltis
{
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var filesDir = Path.Combine(appDataPath, "Project Altis");
                if(!Directory.Exists(filesDir))
                    Directory.CreateDirectory(filesDir);
                Directory.SetCurrentDirectory(filesDir);
                Log.Initialize(LogType.Info);
                Log.Info("Loaded altis launcher v" + typeof(Program).Assembly.GetName().Version);
                Log.Info("Current time: " + DateTime.Now.ToLongDateString() + "|" + DateTime.Now.TimeOfDay);
                Log.Info("Working directory: " + Directory.GetCurrentDirectory());
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
                //It'll update in the background while it's running. The update is in effect the next time they restart app.
                MainAsync().Wait();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private static async Task MainAsync()
        {
            await UpdateManager.GitHubUpdateManager("https://github.com/Jakebooy/altis-releases");
        }
    }
}