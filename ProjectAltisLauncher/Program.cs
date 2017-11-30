using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
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
            ServicePointManager.Expect100Continue = true;
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Initialize();
        }

        private static async void Initialize()
        {
            try
            {
                Log.Initialize(LogType.Info);
#if (!DEBUG)
                Updater.CheckForUpdates();
#endif
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string filesDir = Path.Combine(appDataPath, "Project Altis");
                if (!Directory.Exists(filesDir))
                    Directory.CreateDirectory(filesDir);

                Directory.SetCurrentDirectory(filesDir);

                // First run
                // This may be used in the future for first-run messages or configuration
                Settings.Instance.FirstRun = false;

                Log.Info("Loaded altis launcher v" + typeof(Program).Assembly.GetName().Version);
                Log.Info("Current time: " + DateTime.Now.ToLongDateString() + "|" + DateTime.Now.TimeOfDay);
                Log.Info("Working directory: " + Directory.GetCurrentDirectory());
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());

            }
            catch (Exception e)
            {
                Log.Error(e);
                Log.TryOpenUrl(Path.Combine(Directory.GetCurrentDirectory(), "Logs"));
                Environment.Exit(1);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Error(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(e.ExceptionObject as Exception);
        }
    }
}
