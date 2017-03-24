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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using ProjectAltisLauncher.Core.Manifests;
using ProjectAltisLauncher.Forms;
using ProjectAltisLauncher.Properties;

namespace ProjectAltisLauncher.Core
{
    public class Updater
    {
        private readonly string _currentDir = Directory.GetCurrentDirectory();
        private readonly SortedList<string, string> _downloadList = new SortedList<string, string>(); // Filename, URL
        private readonly FrmMain instance;
        private readonly string pass;

        private readonly string user;
        private string _nowDownloading = "";

        private int _verifyCount;


        public Updater(FrmMain instance)
        {
            this.instance = instance;
            this.user = instance.txtUser.Text;
            this.pass = instance.txtPass.Text;
        }

        public void DoWork()
        {
            this.instance.BeginInvoke((MethodInvoker) delegate { this.instance.btnPlay.Enabled = false; });

            LoginAPIResponse resp = GetLoginAPIResponse(this.instance.txtUser.Text, this.instance.txtPass.Text);

            if (resp == null) return;

            switch (resp.status)
            {
                case "true":
                {
                    this.instance.BeginInvoke((MethodInvoker) delegate
                    {
                        this.instance.lblInfo.ForeColor = Color.Green;
                        this.instance.lblInfo.Text = resp.reason;
                    });
                    UpdateFilesAndPlay();
                    break;
                }
                case "false":
                {
                    this.instance.BeginInvoke((MethodInvoker) delegate
                    {
                        this.instance.lblInfo.ForeColor = Color.Red;
                        this.instance.lblInfo.Text = resp.reason;
                        this.instance.btnPlay.Enabled = true;
                    });
                    break;
                }
                case "critical":
                {
                    this.instance.BeginInvoke((MethodInvoker) delegate
                    {
                        this.instance.lblInfo.ForeColor = Color.Red;
                        this.instance.lblInfo.Text = resp.additional;
                        this.instance.btnPlay.Enabled = true;
                    });
                    break;
                }
                case "info":
                {
                    this.instance.BeginInvoke((MethodInvoker) delegate
                    {
                        this.instance.lblInfo.ForeColor = Color.Orange;
                        this.instance.lblInfo.Text = resp.reason;
                        this.instance.btnPlay.Enabled = true;
                    });
                    break;
                }
                default:
                {
                    this.instance.BeginInvoke((MethodInvoker) delegate
                    {
                        MessageBox.Show(this.instance, "There was an error logging you in!", "Oops!");
                        this.instance.lblInfo.ForeColor = Color.Red;
                        this.instance.lblInfo.Text = "Error";
                    });
                    break;
                }
            }
            this.instance.BeginInvoke((MethodInvoker) delegate
            {
                this.instance.lblInfo.Visible = true;
                this.instance.ActiveControl = null;
            });
        }

        /// <summary>
        ///     Contacts Project Altis's login API to check for valid credentials.
        /// </summary>
        /// <param name="user">Username.</param>
        /// <param name="pass">Password.</param>
        /// <param name="response">Web response.</param>
        /// <returns>True if credentials are valid; otherwise false.</returns>
        private LoginAPIResponse GetLoginAPIResponse(string user, string pass)
        {
            try
            {
                this.instance.BeginInvoke((MethodInvoker) delegate
                {
                    this.instance.lblNowDownloading.Visible = true;
                    this.instance.lblInfo.ForeColor = Color.Black;
                    this.instance.lblInfo.Text = "Contacting login server...";
                });
                HttpWebRequest httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("https://www.projectaltis.com/api/login");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"u\":\"" + user + "\"," +
                                  "\"p\":\"" + pass + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();

                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    this.instance.BeginInvoke((MethodInvoker) delegate { this.instance.lblNowDownloading.Text = ""; });

                    return JsonConvert.DeserializeObject<LoginAPIResponse>(result);
                }
            }
            catch (Exception)
            {
                this.instance.BeginInvoke(
                    (MethodInvoker)
                    delegate
                    {
                        MessageBox.Show(this.instance,
                            Resources.Updater_GetLoginAPIResponse_Failed_to_contact_the_login_server_);
                    });

                return null;
            }
        }

        /// <summary>
        ///     Updates the game files and runs Project Altis.
        /// </summary>
        private void UpdateFilesAndPlay()
        {
            this.instance.BeginInvoke((MethodInvoker) delegate { this.instance.lblNowDownloading.Visible = true; });

            Thread main = new Thread(() =>
            {
                if (!CreateGameDirectorys())
                {
                    this.instance.BeginInvoke(
                        (MethodInvoker)
                        delegate
                        {
                            MessageBox.Show(this.instance, "Unable to create game directories. Exiting update process.");
                        });

                    return; // Not able to create directorys, exit out of thread.
                }

                string rawManifest = RetrieveManifest();
                if (rawManifest == null)
                {
                    this.instance.BeginInvoke(
                        (MethodInvoker)
                        delegate
                        {
                            MessageBox.Show(this.instance,
                                "Unable to retrieve the latest file manifest. Exiting update process.");
                        });
                    return;
                }


                string[] rawManifestArray = rawManifest.Split('#');
                this.instance.BeginInvoke(
                    (MethodInvoker) delegate { this.instance.lblNowDownloading.Text = "Verifying game files..."; });

                foreach (string item in rawManifestArray)
                {
                    // Make sure the item is not an empty value.
                    // An empty value is added when the raw manifest is split
                    if (item == "") continue;
                    Thread fileThread = new Thread(() =>
                    {
                        string workingDir;
                        string path;
                        ManifestJson manifest = JsonConvert.DeserializeObject<ManifestJson>(item.Replace("#", ""));
                        this.instance.BeginInvoke(
                            (MethodInvoker)
                            delegate
                            {
                                this.instance.lblNowDownloading.Text = "Verified files: " + this._verifyCount + "/" +
                                                                       (rawManifestArray.Length - 1);
                            });

                        #region Determine the File Type and Set Working Directory

                        if (manifest.filename.Contains("phase"))
                            workingDir = this._currentDir + @"\resources\default\";
                        else if (manifest.filename.Equals("toon.dc"))
                            workingDir = this._currentDir + @"\config\";
                        else
                            workingDir = this._currentDir;

                        #endregion

                        path = Path.Combine(workingDir, manifest.filename);
                        if (File.Exists(path) && Crypto.CalculateSHA256(path) == manifest.sha256)
                        {
                        }
                        else // The file probably does not exist or does not have the same hash
                        {
                            this._downloadList.Add(manifest.filename, manifest.url);
                        }
                        this._verifyCount++;
                    });
                    fileThread.Start();
                } // Add items to list

                while (!(this._verifyCount == rawManifestArray.Length - 1))
                    Thread.Sleep(30);
                DownloadItemsFromList(this._downloadList);
            });
            main.Start();
        }

        /// <summary>
        ///     Downloads items from the download list.
        /// </summary>
        /// <param name="list">Download List.</param>
        private void DownloadItemsFromList(SortedList<string, string> list)
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;

            string Filename = string.Empty;
            string URL = string.Empty;
            Thread aThread = new Thread(() =>
            {
                if (this._downloadList.Count > 0)
                {
                    this.instance.BeginInvoke((MethodInvoker) delegate
                    {
                        this.instance.pbDownload.Visible = true;
                        this.instance.lblNowDownloading.Visible = true;
                    });
                    foreach (KeyValuePair<string, string> kvp in this._downloadList)
                        // Let it iterate here, we'll just take the last in the list
                    {
                        Filename = kvp.Key;
                        URL = kvp.Value;
                    }
                    this._nowDownloading = Filename;
                    if (Filename.Contains("phase"))
                        client.DownloadFileAsync(new Uri(URL), this._currentDir + @"\resources\default\" + Filename);
                    else if (Filename.Contains("toon"))
                        client.DownloadFileAsync(new Uri(URL), this._currentDir + @"\config\" + Filename);
                    else
                        client.DownloadFileAsync(new Uri(URL), this._currentDir + @"\" + Filename);
                }
                else
                {
                    this.instance.BeginInvoke((MethodInvoker) delegate
                    {
                        this.instance.lblNowDownloading.Text = "Have fun!";
                        this.instance.pbDownload.Visible = false;
                        this.instance.btnPlay.Enabled = true;
                    });
                    Thread t = new Thread(() => Play.LaunchGame(this.user, this.pass, this.instance));
                    t.Start();
                }
            });
            aThread.Start();
        }

        /// <summary>
        ///     Download event fired when progress has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.instance.BeginInvoke((MethodInvoker) delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                this.instance.lblNowDownloading.Text = this._nowDownloading + ": " + "Downloaded " +
                                                       Data.ConvertToNetworkDataType(e.BytesReceived) + " of " +
                                                       Data.ConvertToNetworkDataType(e.TotalBytesToReceive);
                this.instance.pbDownload.Value = Convert.ToInt32(percentage);
            });
        }

        /// <summary>
        ///     Download event fired when the download has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.instance.BeginInvoke((MethodInvoker) delegate
            {
                this._downloadList.Remove(this._nowDownloading);
                this.instance.lblNowDownloading.Text = "Completed";
                DownloadItemsFromList(this._downloadList);
            });
        }


        /// <summary>
        ///     Creates the game directories necessary for file updating.
        /// </summary>
        /// <returns>True if directories were created successfully, otherwise false.</returns>
        private static bool CreateGameDirectorys()
        {
            try
            {
                Directory.CreateDirectory(@"resources");
                Directory.CreateDirectory(@"resources\default\");
                Directory.CreateDirectory(@"config");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Retrieves the file manifest from the Project Altis manifest API.
        /// </summary>
        /// <returns></returns>
        private static string RetrieveManifest()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString("http://projectaltis.com/api/manifest");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}