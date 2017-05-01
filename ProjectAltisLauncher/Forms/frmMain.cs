using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ProjectAltisLauncher.Core;
using ProjectAltisLauncher.Manifests;
using System.Threading;
using System.Collections.Generic;
using ProjectAltisLauncher.Enums;
using System.ComponentModel;
/*
* TODO:
*    Catch Exceptions
*    Enable uploading of logs to pastebin - Idea suggested by Judge2020
*/
namespace ProjectAltisLauncher.Forms
{
    public partial class frmMain : Form
    {
        #region Fields
        private readonly SortedList<string, string> _downloadList = new SortedList<string, string>(); // Filename, URL
        private readonly string _currentDir = Directory.GetCurrentDirectory();
        private string _nowDownloading = "";
        #endregion
        #region Main Form Events
        public frmMain()
        {
            InitializeComponent();
            
            _currentDir = Directory.GetCurrentDirectory() + @"\";
            _nowDownloading = "";

            Properties.Settings.Default.password = "Deprecated";

            if (!IsWriteable())
            {
                MessageBox.Show("It appears you do not have permission to write the the current directory: " + _currentDir + "\n" +
                                "The launcher may not work correctly without permissions. \n" +
                                "Try running the launcher with administrator rights or installing in a different location.");
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show("This is a debug build, do not put this into production",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            #region Loading Settings
            try
            {
                txtUser.Text = Properties.Settings.Default.username;
            }
            catch { }
            // Read user settings
            if (Properties.Settings.Default.wantsCursor == true) // Cursor
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Properties.Resources.toonmono);
                this.Cursor = new Cursor(cursorMemoryStream);
            }
            // Load last saved user background choice
            this.BackgroundImage.Dispose();
            if (Properties.Settings.Default.wantsRandomBg)
            {
                BackgroundImage = Background.ReturnRandomBackground();
            }
            else
            {
                BackgroundImage = Background.ReturnBackground(Properties.Settings.Default.background);
            }
            #endregion
            // This prevents other controls from being focused
            this.Select();
            this.ActiveControl = null;
            Button_MouseLeave(btnPlay, EventArgs.Empty);
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        #endregion
        #region Borderless Form Code

        private Point mouseDownPoint = Point.Empty;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPoint = new Point(e.X, e.Y);
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }
        #endregion
        #region Button Behaviors
        #region Exit Button
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region Minimize Button
        private void btnMin_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            this.WindowState = FormWindowState.Minimized;
            this.ActiveControl = null;
        }
        #endregion
        #region Play Button
        private void btnPlay_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                Log.TryOpenUrl("https://projectaltis.com/register");
                return;
            }
            btnPlay.Enabled = false;
            #region Save credentials if necessary
            if (cbSaveLogin.Checked == true)
            {
                Console.WriteLine("Save checked");
                if (txtUser.Text != null || txtPass.Text != null)
                {
                    Properties.Settings.Default.username = txtUser.Text;
                    Properties.Settings.Default.Save();
                }
            }
            #endregion
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.projectaltis.com/api/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            LoginApiResponse resp;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"u\":\"" + txtUser.Text + "\"," +
                              "\"p\":\"" + txtPass.Text + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<LoginApiResponse>(result);
            }

            lblInfo.ForeColor = Color.Black; // Reset the label color
            switch (resp.status)
            {
                case "true":
                    lblInfo.ForeColor = Color.Green;
                    lblInfo.Text = resp.reason;
                    UpdateFilesAndPlay();
                    break;
                case "false":
                    lblInfo.ForeColor = Color.Red;
                    lblInfo.Text = resp.reason;
                    btnPlay.Enabled = true;
                    break;
                case "critical":
                    lblInfo.ForeColor = Color.Red;
                    lblInfo.Text = resp.additional;
                    btnPlay.Enabled = true;
                    break;
                case "info":
                    lblInfo.ForeColor = Color.Orange;
                    lblInfo.Text = resp.reason;
                    btnPlay.Enabled = true;
                    break;
                default:
                    MessageBox.Show("There was an error logging you in!", "Oops!");
                    lblInfo.Text = "Error";
                    break;
            }
            lblInfo.Visible = true;
            this.ActiveControl = null;
        }
        #endregion
        #region Site Button
        private void btnOfficialSite_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://www.projectaltis.com/");
            this.ActiveControl = null;
        }
        #endregion
        #region Discord Button
        private void btnDiscord_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://discord.me/ttprojectaltis");
            this.ActiveControl = null;
        }
        #endregion
        #region Content Packs
        private void btnContentPacks_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            btnContentPacks.BackgroundImage = Properties.Resources.contentpacks_d;
            frmContentPacks contentPack = new frmContentPacks();
            contentPack.ShowDialog(this);
            this.ActiveControl = null;
        }
        #endregion
        #region Change Theme
        private void btnChangeBg_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            frmBackgroundChoices bg = new frmBackgroundChoices();
            bg.ShowDialog();
            /// Applying the background after the user closes the Change Theme form
            if (!Properties.Settings.Default.wantsRandomBg)
            {
                BackgroundImage.Dispose();
                BackgroundImage = Background.ReturnBackground(Properties.Settings.Default.background);
            }

            this.ActiveControl = null;
        }
        #endregion
        #region Options Button
        private void btnOptions_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            frmOptions op = new frmOptions();
            op.ShowDialog();
            // Apply user settings
            if (Properties.Settings.Default.wantsCursor) // Cursor
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Properties.Resources.toonmono);
                this.Cursor = new Cursor(cursorMemoryStream);
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
            this.ActiveControl = null;
        }
        #endregion
        #region Credits
        private void btnCredits_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            frmCredits cred = new frmCredits();
            cred.ShowDialog();
            this.ActiveControl = null;
        }
        #endregion
        #region Main Button Events
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if(string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseEnter");
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseLeave");
        }
        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseDown");
        }
        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseUp");
        }
        #endregion
        #region Play on Enter
        private void txtPassAndUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnPlay.Enabled)
                {
                    btnPlay.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        #endregion
        #endregion
        #region File Downloader / Verifier
        private void UpdateFilesAndPlay()
        {
            lblNowDownloading.Visible = true;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var myThread = new Thread(() =>
            {
                Directory.CreateDirectory(@"resources");
                Directory.CreateDirectory(@"resources\default\");
                Directory.CreateDirectory(@"config");
                string rawManifest = RetrieveManifest();
                string[] rawManifestArray = rawManifest.Split('#');
                foreach (string item in rawManifestArray)
                {
                    string workingDir;
                    string path;
                    // Make sure the item is not an empty value.
                    // An empty value is added when the raw manifest is split
                    if (item == "") continue;

                    FileTypes type;

                    ManifestJson manifest = JsonConvert.DeserializeObject<ManifestJson>(item.Replace("#", ""));

                    #region Determine the File Type and Set Working Directory

                    if (manifest.filename.Contains("phase"))
                    {
                        type = FileTypes.Phase;
                        workingDir = _currentDir + @"\resources\default\";
                    }
                    else if (manifest.filename.Equals("toon.dc"))
                    {
                        type = FileTypes.Config;
                        workingDir = _currentDir + @"\config\";
                    }
                    else
                    {
                        type = FileTypes.Default;
                        workingDir = _currentDir;
                    }

                    #endregion

                    path = Path.Combine(workingDir, manifest.filename);

                    BeginInvoke((MethodInvoker)delegate
                    {
                        lblNowDownloading.Text = "Verifying " + manifest.filename;
                    });

                    if (type == FileTypes.Phase && File.Exists(path) && Hashing.CalculateSHA256(path) == manifest.sha256)
                    {
                        Console.WriteLine("Phase file: {0} is up to date", manifest.filename);
                    }
                    else if (type == FileTypes.Config && File.Exists(path) && Hashing.CalculateSHA256(path) == manifest.sha256)
                    {
                        Console.WriteLine("File {0} exists and it has the latest checksum", path);
                    }
                    else if (type == FileTypes.Default && File.Exists(path) && Hashing.CalculateSHA256(path) == manifest.sha256)
                    {
                        Console.WriteLine("File {0} exists and it has the latest checksum", path);
                    }
                    else // The file probably does not exist or does not have the same hash
                    {
                        AddFileToDownloadList(manifest.filename, manifest.url);
                        Console.WriteLine("Added {0} to the download list! Total Items: {1}", manifest.filename, _downloadList.Count);
                    }
                } // Add items to list
                Console.WriteLine("Added all items to the list, starting download...");
                sw.Stop();
                Console.WriteLine("Total time elapsed (seconds): " + sw.Elapsed.TotalSeconds);
                DownloadItemsFromList(_downloadList);
            });
            myThread.Start();
        }
        private void AddFileToDownloadList(string filename, string url)
        {
            _downloadList.Add(filename, url);
        }
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
                    BeginInvoke((MethodInvoker)delegate
                    {
                        pbDownload.Visible = true;
                        lblNowDownloading.Visible = true;
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
                    BeginInvoke((MethodInvoker)delegate
                    {
                        lblNowDownloading.Text = "Have fun!";
                        pbDownload.Visible = false;
                        btnPlay.Enabled = true;
                    });
                    Thread t = new Thread(() => Play.LaunchGame(txtUser.Text, txtPass.Text, this));
                    t.Start();

                }
            });
            myThread.Start();
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                lblNowDownloading.Text = _nowDownloading + ": " + "Downloaded " + Data.ConvertToNetworkDataType(e.BytesReceived) + " of " + Data.ConvertToNetworkDataType(e.TotalBytesToReceive);
                pbDownload.Value = Convert.ToInt32(percentage);
            });
        }
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                _downloadList.Remove(_nowDownloading);
                lblNowDownloading.Text = "Completed";
                DownloadItemsFromList(_downloadList);
            });
        }
        private static string RetrieveManifest()
        {
            string rawManifest;
            using (WebClient client = new WebClient())
            {
                rawManifest = client.DownloadString("http://projectaltis.com/api/manifest");
            }
            return rawManifest;
        }
        #endregion
        #region Web Browser / News
        // Place any web browser events inside here
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString().Contains("https://projectaltis.com/launcher"))
            {
                return;
            }
            e.Cancel = true;
            Process.Start(e.Url.ToString());
        }



        #endregion


        #region Directory Things
        /// <summary>
        /// Determines whether this instance is administrator.
        /// </summary>
        /// <returns><c>true</c> if this instance is administrator; otherwise, <c>false</c>.</returns>
        private static bool IsAdministrator()
        {
            Console.WriteLine("Is Administrator called");
            return (new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()))
                    .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }


        /// <summary>
        /// Determines whether the current directory can be written to.
        /// </summary>
        /// <returns><c>true</c> if the directory is writeable; otherwise, <c>false</c>.</returns>
        private bool IsWriteable()
        {
            Console.WriteLine("Is writable called");
            try
            {
                using (FileStream fs = File.Create(_currentDir + "writeText")) { }
                File.Delete(_currentDir + "writeText");
                Log.Info("Directory is writeable");
            }
            catch (Exception ex)
            {
                                Console.WriteLine("Exception thrown, not writeable");
                Log.Error(ex);
                return false;
            }
            return true;
        }
        #endregion

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            Button_MouseLeave(btnPlay, EventArgs.Empty);
        }
    }
}