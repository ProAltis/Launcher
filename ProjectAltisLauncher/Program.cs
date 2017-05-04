using System;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using ProjectAltis.Enums;
using ProjectAltis.Forms;

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
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string filesDir = Path.Combine(appDataPath, "Project Altis");
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
                
            }
            catch (Exception e)
            {
                Log.Error(e);
                Log.TryOpenUrl(Path.Combine(Directory.GetCurrentDirectory(), "Logs"));
                Environment.Exit(1);
            }
        }
    }
}