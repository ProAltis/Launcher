using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectAltis.Enums;
using ProjectAltis.Core;
using SharpRaven;
using SharpRaven.Data;

namespace ProjectAltis
{
    [DebuggerStepThrough]
    public class Log
    {
        private const int MaxLogFileAge = 2;
        private const int KeepOldLogs = 10;
        private static readonly Queue<string> LogQueue = new Queue<string>();
        public static bool Initialized { get; private set; }

        private static LogType CurrenLogType;

        internal static void Initialize(LogType logType)
        {
            CurrenLogType = logType;
            if(Initialized)
                return;
            Trace.AutoFlush = true;
            var logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            var logFile = Path.Combine(logDir, "launcher_log.txt");
            if(!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            else
            {
                try
                {
                    var fileInfo = new FileInfo(logFile);
                    if(fileInfo.Exists)
                    {
                        using(var fs = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            //can access log file => no other instance of same installation running
                        }
                        File.Move(logFile, logFile.Replace(".txt", "_" + DateTime.Now.ToUnixTime() + ".txt"));
                        //keep logs from the last 2 days plus 25 before that
                        foreach(var file in
                            new DirectoryInfo(logDir).GetFiles("launcher_log*")
                                .Where(x => x.LastWriteTime < DateTime.Now.AddDays(-MaxLogFileAge))
                                .OrderByDescending(x => x.LastWriteTime)
                                .Skip(KeepOldLogs))
                        {
                            try
                            {
                                File.Delete(file.FullName);
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                        File.Create(logFile).Dispose();
                }
                catch(Exception)
                {
                    return;
                }
            }
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener(new StreamWriter(logFile, false)));
            }
            catch(Exception ex)
            {

            }
            Initialized = true;
            foreach(var line in LogQueue)
                Trace.WriteLine(line);
        }

        public static void WriteLine(string msg, LogType type, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "")
        {

            var file = sourceFilePath?.Split('/', '\\').LastOrDefault()?.Split('.').FirstOrDefault();
            var line = $"{DateTime.Now.ToLongTimeString()}|{type}|{file}.{memberName} >> {msg}";
            if(Initialized)
                Trace.WriteLine(line);
            else
                LogQueue.Enqueue(line);
        }

        public static void Debug(string msg, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
            => WriteLine(msg, LogType.Debug, memberName, sourceFilePath);

        public static void Info(string msg, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
            => WriteLine(msg, LogType.Info, memberName, sourceFilePath);

        public static void Warn(string msg, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
            => WriteLine(msg, LogType.Warning, memberName, sourceFilePath);

        public static void Error(string msg, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
            => WriteLine(msg, LogType.Error, memberName, sourceFilePath);

        public static void Error(Exception ex, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "")
        {
            WriteLine(ex.ToString(), LogType.Error, memberName, sourceFilePath);
            var ravenClient = new RavenClient("https://4efd8a20d8a94bfe9fbd4ec828c43dfe:614ae4d4f90c4205ad1c1b1f2c494088@sentry.io/186402");
            ravenClient.Capture(new SentryEvent(ex));
        }

        public static bool TryOpenUrl(string url, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            try
            {
                Log.Info("[Utility.Helper.TryOpenUrl] " + url, memberName, sourceFilePath);
                Process.Start(url);
                return true;
            }
            catch(Exception e)
            {
                Log.Error("[Utility.Helper.TryOpenUrl] " + e, memberName, sourceFilePath);
                return false;
            }
        }
    }
}
