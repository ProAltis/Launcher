using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using ProjectAltisLauncher.Core.Manifests;
using ProjectAltisLauncher.Forms;
using System.Threading;
using ProjectAltisLauncher.Core.Enums;
using System.ComponentModel;

namespace ProjectAltisLauncher.Core
{
    public class Updater
    {
        private FrmMain instance;
        private readonly SortedList<string, string> _downloadList = new SortedList<string, string>(); // Filename, URL
        private readonly string _currentDir = Directory.GetCurrentDirectory();
        private string _nowDownloading = "";

        private int _verifyCount;

        private string user;
        private string pass;


        public Updater(FrmMain instance)
        {
            this.instance = instance;
            user = instance.txtUser.Text;
            pass = instance.txtPass.Text;
        }

        public void DoWork()
        {
            instance.btnPlay.Enabled = false;

            var resp = GetLoginAPIResponse(instance.txtUser.Text, instance.txtPass.Text);

            switch (resp.status)
            {
                case "true":
                    {
                        instance.lblInfo.ForeColor = Color.Green;
                        instance.lblInfo.Text = resp.reason;
                        UpdateFilesAndPlay();
                        break;
                    }
                case "false":
                    {
                        instance.lblInfo.ForeColor = Color.Red;
                        instance.lblInfo.Text = resp.reason;
                        instance.btnPlay.Enabled = true;
                        break;
                    }
                case "critical":
                    {
                        instance.lblInfo.ForeColor = Color.Red;
                        instance.lblInfo.Text = resp.additional;
                        instance.btnPlay.Enabled = true;
                        break;
                    }
                case "info":
                    {
                        instance.lblInfo.ForeColor = Color.Orange;
                        instance.lblInfo.Text = resp.reason;
                        instance.btnPlay.Enabled = true;
                        break;
                    }
                default:
                    {
                        MessageBox.Show(instance, "There was an error logging you in!", "Oops!");
                        instance.lblInfo.ForeColor = Color.Red;
                        instance.lblInfo.Text = "Error";
                        break;
                    }
            }

            instance.lblInfo.Visible = true;
            instance.ActiveControl = null;
        }

        /// <summary>
        /// Contacts Project Altis's login API to check for valid credentials.
        /// </summary>
        /// <param name="user">Username.</param>
        /// <param name="pass">Password.</param>
        /// <param name="response">Web response.</param>
        /// <returns>True if credentials are valid; otherwise false.</returns>
        private LoginAPIResponse GetLoginAPIResponse(string user, string pass)
        {
            instance.lblInfo.ForeColor = Color.Black;
            instance.lblInfo.Text = "Contacting login server...";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.projectaltis.com/api/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"u\":\"" + user + "\"," +
                              "\"p\":\"" + pass + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                instance.lblNowDownloading.Text = "";
                return JsonConvert.DeserializeObject<LoginAPIResponse>(result);
            }
        }

        /// <summary>
        /// Updates the game files and runs Project Altis.
        /// </summary>
        private void UpdateFilesAndPlay()
        {
            instance.lblNowDownloading.Visible = true;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var main = new Thread(() =>
            {
                CreateGameDirectorys();

                string rawManifest = RetrieveManifest();
                string[] rawManifestArray = rawManifest.Split('#');
                instance.BeginInvoke((MethodInvoker)delegate
                {
                    instance.lblNowDownloading.Text = "Verifying game files...";
                });

                foreach (string item in rawManifestArray)
                {
                    // Make sure the item is not an empty value.
                    // An empty value is added when the raw manifest is split
                    if (item == "") continue;
                    var fileThread = new Thread(() =>
                    {
                        string workingDir;
                        string path;                        
                        ManifestJson manifest = JsonConvert.DeserializeObject<ManifestJson>(item.Replace("#", ""));
                        instance.BeginInvoke((MethodInvoker)delegate
                        {
                            instance.lblNowDownloading.Text = "Verified files: " + _verifyCount + "/" + (rawManifestArray.Length - 1);
                        });
                        
                        #region Determine the File Type and Set Working Directory

                        if (manifest.filename.Contains("phase"))
                        {
                            workingDir = _currentDir + @"\resources\default\";
                        }
                        else if (manifest.filename.Equals("toon.dc"))
                        {
                            workingDir = _currentDir + @"\config\";
                        }
                        else
                        {
                            workingDir = _currentDir;
                        }

                        #endregion

                        path = Path.Combine(workingDir, manifest.filename);
                        if (File.Exists(path) && Crypto.CalculateSHA256(path) == manifest.sha256)
                        {
                        }
                        else // The file probably does not exist or does not have the same hash
                        {
                            _downloadList.Add(manifest.filename, manifest.url);
                        }
                        _verifyCount++;
                    });
                    fileThread.Start();
                } // Add items to list

                while (!(_verifyCount == rawManifestArray.Length -1))
                {
                    Thread.Sleep(30);
                }
                DownloadItemsFromList(_downloadList);
            });
            main.Start();
        }

        /// <summary>
        /// Downloads items from the download list.
        /// </summary>
        /// <param name="list">Download List.</param>
        private void DownloadItemsFromList(SortedList<string, string> list)
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            string Filename = String.Empty;
            string URL = String.Empty;
            var myThread = new Thread(() =>
            {
                if (_downloadList.Count > 0)
                {
                    instance.BeginInvoke((MethodInvoker)delegate
                    {
                        instance.pbDownload.Visible = true;
                        instance.lblNowDownloading.Visible = true;
                    });
                    foreach (KeyValuePair<string, string> kvp in _downloadList) // Let it iterate here, we'll just take the last in the list
                    {
                        Filename = kvp.Key;
                        URL = kvp.Value;
                    }
                    _nowDownloading = Filename;
                    if (Filename.Contains("phase"))
                    {
                        client.DownloadFileAsync(new Uri(URL), _currentDir + @"\resources\default\" + Filename);
                    }
                    else if (Filename.Contains("toon"))
                    {
                        client.DownloadFileAsync(new Uri(URL), _currentDir + @"\config\" + Filename);
                    }
                    else
                    {
                        client.DownloadFileAsync(new Uri(URL), _currentDir + @"\" + Filename);
                    }

                }
                else
                {
                    instance.BeginInvoke((MethodInvoker)delegate
                    {
                        instance.lblNowDownloading.Text = "Have fun!";
                        instance.pbDownload.Visible = false;
                        instance.btnPlay.Enabled = true;
                    });
                    Thread t = new Thread(() => Play.LaunchGame(user, pass, instance));
                    t.Start();

                }
            });
            myThread.Start();
        }

        /// <summary>
        /// Download event fired when progress has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            instance.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                instance.lblNowDownloading.Text = _nowDownloading + ": " + "Downloaded " + Data.ConvertToNetworkDataType(e.BytesReceived) + " of " + Data.ConvertToNetworkDataType(e.TotalBytesToReceive);
                instance.pbDownload.Value = Convert.ToInt32(percentage);
            });
        }

        /// <summary>
        /// Download event fired when the download has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            instance.BeginInvoke((MethodInvoker)delegate
            {
                _downloadList.Remove(_nowDownloading);
                instance.lblNowDownloading.Text = "Completed";
                DownloadItemsFromList(_downloadList);
            });
        }
 
        /// <summary>
        /// Creates the game directories necessary for file updating.
        /// </summary>
        private static void CreateGameDirectorys()
        {
            Directory.CreateDirectory(@"resources");
            Directory.CreateDirectory(@"resources\default\");
            Directory.CreateDirectory(@"config");
        }

        /// <summary>
        /// Retrieves the file manifest from the Project Altis manifest API.
        /// </summary>
        /// <returns></returns>
        private static string RetrieveManifest()
        {
            string rawManifest;
            using (WebClient client = new WebClient())
            {
                rawManifest = client.DownloadString("http://projectaltis.com/api/manifest");
            }
            return rawManifest;
        }

    }
}
