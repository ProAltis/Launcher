using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AltisUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Altis Updater";
            Console.WriteLine("Downloading latest launcher...");
            try
            {
                string launcherName = Path.GetFileName(args[0]).Replace("%20", " ");
                using (WebClient client = new WebClient())
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\" + launcherName);
                    client.DownloadFile(args[0], launcherName);
                }

                Process.Start(launcherName);
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to update :(");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }

        }
    }
}
