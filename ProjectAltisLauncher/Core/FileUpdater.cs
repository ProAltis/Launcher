using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using ProjectAltis.Forms;
using ProjectAltis.Manifests;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAltis.Core
{
    public class FileUpdater
    {
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private readonly SortedList<string, string> _downloadList = new SortedList<string, string>(); // Filename, URL
        private readonly FrmMain _instance;

        private volatile string _nowDownloading = string.Empty;

        private volatile int _filesChecked = 0;

        public event FilesUpdatedEventHandler FilesUpdated;

        public delegate void FilesUpdatedEventHandler();

        protected virtual void OnFilesUpdated()
        {
            FilesUpdated?.Invoke();
        }

        public FileUpdater(FrmMain instance)
        {
            _instance = instance;
        }

        public void RunUpdater()
        {
            _instance?.Invoke((MethodInvoker)delegate
            {
                _instance.btnPlay.Enabled = false;
            });

            LoginApiResponse resp = GetLoginAPIResponse(_instance?.txtUser.Text, _instance?.txtPass.Text);

            if (resp == null)
            {
                Log.Info("API response was null.");
                return;
            }

            Log.Info("Response Status info:");
            Log.Info("Status: " + resp.status);
            Log.Info("Reason: " + resp.reason);
            Log.Info("Additi: " + resp.additional);

            switch (resp.status)
            {
                case "true":
                    {
                        _instance?.Invoke((MethodInvoker)delegate
                        {
                            _instance.lblInfo.ForeColor = Color.Green;
                            _instance.lblInfo.Text = resp.reason;
                        });
                        UpdateFilesAndPlay();
                        break;
                    }
                case "false":
                    {
                        _instance?.Invoke((MethodInvoker)delegate
                        {
                            _instance.lblInfo.ForeColor = Color.Red;
                            _instance.lblInfo.Text = resp.reason;
                            _instance.btnPlay.Enabled = true;
                        });
                        break;
                    }
                case "critical":
                    {
                        _instance?.Invoke((MethodInvoker)delegate
                        {
                            _instance.lblInfo.ForeColor = Color.Red;
                            _instance.lblInfo.Text = resp.additional;
                            _instance.btnPlay.Enabled = true;
                        });
                        break;
                    }
                case "info":
                    {
                        _instance?.Invoke((MethodInvoker)delegate
                        {
                            _instance.lblInfo.ForeColor = Color.Orange;
                            _instance.lblInfo.Text = resp.reason;
                            _instance.btnPlay.Enabled = true;
                        });
                        break;
                    }
                default:
                    {
                        _instance.BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(_instance, "There was an error logging you in!", "Oops!");
                            _instance.lblInfo.ForeColor = Color.Red;
                            _instance.lblInfo.Text = "Error";
                        });
                        break;
                    }
            }

            _instance?.Invoke((MethodInvoker)delegate
            {
                _instance.lblInfo.Visible = true;
                _instance.ActiveControl = null;
            });
        }

        /// <summary>
        ///     Contacts Project Altis's login API to check for valid credentials.
        /// </summary>
        /// <param name="user">Username.</param>
        /// <param name="pass">Password.</param>
        /// <param name="response">Web response.</param>
        /// <returns>True if credentials are valid; otherwise false.</returns>
        public LoginApiResponse GetLoginAPIResponse(string user, string pass)
        {
            try
            {
                Log.Info("Querying API for response.");
                _instance?.Invoke((MethodInvoker)delegate
                {
                    _instance.lblNowDownloading.Visible = true;
                    _instance.lblInfo.ForeColor = Color.Black;
                    _instance.lblInfo.Text = "Contacting login server...";
                });
                return Data.GetLoginAPIResponse(user, pass);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _instance?.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(_instance, "Failed to contact the login server.");
                    _instance.btnPlay.Enabled = true;
                });
                return null;
            }
        }

        /// <summary>
        ///     Updates the game files and runs Project Altis.
        /// </summary>
        private void UpdateFilesAndPlay()
        {
            Log.Info("||||||||||");
            Log.Info("||||||||||");
            Log.Info("Started to update files and play!");
            Log.Info("||||||||||");
            Log.Info("||||||||||");
            _instance?.Invoke((MethodInvoker)delegate
            {
                _instance.lblNowDownloading.Visible = true; // Enable the downloading label to show what file is downloading
            });

            Thread mainThread = new Thread(() =>
            {
                Log.Info("Checking if directories exist.");
                if (!CreateGameDirectorys())
                {
                    _instance?.Invoke((MethodInvoker)delegate
                    {
                        Log.Error("Unable to check/create directories.");
                        MessageBox.Show(_instance, "Unable to create game directories. Exiting update process.");
                    });
                    return; // Not able to create directorys, exit out of thread.
                }

                Log.Info("Checking/creating directories succeeded.");
                Log.Info("Retrieving manifest.");
                string rawManifest = RetrieveManifest();
                if (rawManifest == null)
                {
                    Log.Info("Manifest was null, likely web server maintenance.");
                    Log.Info("Starting game anyways.");
                    OnFilesUpdated(); // This will start the game.
                    return;
                }
                Log.Info("Manifest wasn't null, verifying files.");
                Log.Info("---------");
                VerifyFiles(rawManifest);
                Log.Info("Downloading files that were not up to date.");

                new Thread(DownloadFiles).Start(); // Download any files that have not been verified
            });
            mainThread.Start();
        }

        private void VerifyFiles(string rawManifest)
        {
            string[] rawManifestArray = rawManifest.Split('#');

            // Remove the empty values in the array ( The last value will be removed, but just to future proof in case manifest changes )
            string[] manifestArray = rawManifestArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            _instance?.Invoke((MethodInvoker)delegate
            {
                _instance.lblNowDownloading.Text = "Verifying game files...";
            });

            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = (int)Math.Ceiling(Environment.ProcessorCount / 2d) // Cores / 2 50% cpu usage. Round up if we get dec vals
            };

            Parallel.ForEach(manifestArray, options, x =>
            {
                ManifestJson manifest = JsonConvert.DeserializeObject<ManifestJson>(x.Replace("#", ""));
                VerifyFile(manifest, manifestArray.Length);
            });
        }

        private void VerifyFile(ManifestJson manifest, int totalFiles)
        {

            Log.Info("Verifying file " + manifest.filename);

            _instance?.Invoke((MethodInvoker)delegate
            {
                _instance.lblNowDownloading.Text = "Verified files: " + _filesChecked + "/" + (totalFiles);
            });

            if (IsFileUpToDate(manifest))
            {
                Log.Info(manifest.filename + " : Up to date.");
            }
            else
            {
                Log.Info(manifest.filename + " : Outdated");
                _downloadList.Add(manifest.filename, manifest.url);
            }
            _filesChecked++;
        }

        /// <summary>
        /// Determines whether a file up to date.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns>
        ///   <c>true</c> if the file is up to date; otherwise, <c>false</c>.
        /// </returns>
        private bool IsFileUpToDate(ManifestJson manifest)
        {
            string workingDir = GetCorrectDownloadDirectory(manifest.filename);
            string filePath = workingDir + manifest.filename;
            return File.Exists(filePath) && Hashing.CalculateSha256(filePath) == manifest.sha256;
        }

        private void DownloadFiles()
        {
            using (WebClient client = new WebClient())
            {
                if (_downloadList.Count > 0)
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;

                    _instance?.Invoke((MethodInvoker)delegate
                    {
                        _instance.pbDownload.Visible = true;
                        _instance.lblNowDownloading.Visible = true;
                    });

                    string filename = string.Empty;
                    string url = string.Empty;

                    foreach (KeyValuePair<string, string> kvp in _downloadList)
                    // Let it iterate here, we'll just take the last in the list
                    {
                        filename = kvp.Key;
                        url = kvp.Value;
                    }

                    _nowDownloading = filename;

                    Log.Info("Downloading " + _nowDownloading);
                    client.DownloadFileAsync(new Uri(url), GetCorrectDownloadDirectory(filename) + filename);
                }
                else
                {
                    OnFilesUpdated(); // No files to be downloaded. All have verified as up to date
                }
            }
        }

        /// <summary>
        ///     Download event fired when progress has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _instance?.Invoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                _instance.lblNowDownloading.Text = _nowDownloading + ": " + "Downloaded " +
                                                       Data.ConvertToNetworkDataType(e.BytesReceived) + " of " +
                                                       Data.ConvertToNetworkDataType(e.TotalBytesToReceive);
                _instance.pbDownload.Value = Convert.ToInt32(percentage);
            });
        }

        /// <summary>
        ///     Download event fired when the download has completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log.Info("Finished downloading " + _nowDownloading);
            _instance.Invoke((MethodInvoker)delegate
            {
                _instance.lblNowDownloading.Text = "Completed";
            });
            _downloadList.Remove(_nowDownloading);
            new Thread(DownloadFiles).Start(); // Download the next item...
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
                Log.Info("Retrieving manifest");
                using (WebClient client = new WebClient())
                {
                    try
                    {

                        string manifest = client.DownloadString("http://projectaltis.com/api/manifest");
                        if (manifest.StartsWith("{"))
                        {
                            return manifest;
                        }
                        Log.Error("Manifest doesn't look like it's there or is good.");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Log.Error("I think the manifest host is down.");
                        Log.Error(ex);

                        return null;
                    }
                }
            }
            catch (Exception eex)
            {
                Log.Error("random Exception");
                Log.Error(eex);
                return null;
            }
        }

        /// <summary>
        /// Gets the correct download directory.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetCorrectDownloadDirectory(string fileName)
        {

            if (fileName.Contains("phase"))
            {
                return _currentDirectory + @"\resources\default\";
            }
            else if (fileName.Equals("toon.dc"))
            {
                return _currentDirectory + @"\config\";
            }
            else
            {
                return _currentDirectory + @"\";
            }
        }
    }
}