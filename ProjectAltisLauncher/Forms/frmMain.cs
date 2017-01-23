using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ProjectAltisLauncher.Core;
using ProjectAltisLauncher.Manifests;
using System.Threading;
/// <summary>
/// TODO:
///     Clean up code
///     Add group tracker
/// </summary>
namespace ProjectAltisLauncher.Forms
{
    public partial class frmMain : Form
    {
        #region Fields
        private string _currentDir;
        private double _totalFiles;
        private double _totalProgress;
        private double _currentFile;
        private string _nowDownloading;
        private string _playcookie;
        #endregion
        #region Main Form Events
        public frmMain()
        {
            InitializeComponent();
            _currentDir = Directory.GetCurrentDirectory() + "\\";
            _totalFiles = 0;
            _currentFile = 0;
            _nowDownloading = "";

            Properties.Settings.Default.password = "Deprecated";
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
            var AutoUpdateThread = new Thread(AutoUpdater.CheckForUpdate);
            AutoUpdateThread.Start();
            // This prevents other controls from being focused
            this.Select();
            this.ActiveControl = null;
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
        Point mouseDownPoint = Point.Empty;
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
        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.BackgroundImage = Properties.Resources.cancel_h;
        }
        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackgroundImage = Properties.Resources.cancel;
        }
        #endregion
        #region Minimize Button
        private void btnMin_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            this.WindowState = FormWindowState.Minimized;
            this.ActiveControl = null;
        }
        private void btnMin_MouseEnter(object sender, EventArgs e)
        {
            btnMin.BackgroundImage = Properties.Resources.minus_h;
        }
        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackgroundImage = Properties.Resources.minus;
        }
        #endregion
        #region Play Button
        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Enabled = false;
            Audio.PlaySoundFile("sndclick");
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
            //string finalURL = "https://www.projectaltis.com/api/?u=" + txtUser.Text + "&p=" + txtPass.Text;
            //string APIResponse = "";
            //    try
            //    {
            //        APIResponse = Data.RequestData(finalURL, "GET"); // Send request to login API, store the response as string
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("An error contacting the login API occured");
            //    }
            //loginAPIResponse resp = JsonConvert.DeserializeObject<loginAPIResponse>(APIResponse); // Deserialize API response into vars


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.projectaltis.com/api/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            loginAPIResponse resp;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"u\":\""+ txtUser.Text +"\"," +
                              "\"p\":\"" + txtPass.Text + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                resp = JsonConvert.DeserializeObject<loginAPIResponse>(result);
            }
            

            lblInfo.ForeColor = Color.Black; // Reset the label color
            switch (resp.status)
            {
                case "true":
                    lblInfo.ForeColor = Color.Green;
                    lblInfo.Text = resp.reason;
                    _playcookie = resp.additional;
                    Updater.RunWorkerAsync();
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
        private void btnPlay_MouseDown(object sender, MouseEventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play_d;
        }
        private void btnPlay_MouseUp(object sender, MouseEventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play;
        }
        private void btnPlay_MouseEnter(object sender, EventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play_h;
        }
        private void btnPlay_MouseLeaveAndUp(object sender, EventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play;
        }
        #endregion
        #region Site Button
        private void btnOfficialSite_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://www.projectaltis.com/");
            this.ActiveControl = null;
        }
        private void btnOfficialSite_MouseLeave(object sender, EventArgs e)
        {
            btnOfficialSite.BackgroundImage = Properties.Resources.website;
        }
        private void btnOfficialSite_MouseDown(object sender, MouseEventArgs e)
        {
            btnOfficialSite.BackgroundImage = Properties.Resources.website_d;
        }
        private void btnOfficialSite_MouseEnter(object sender, EventArgs e)
        {
            btnOfficialSite.BackgroundImage = Properties.Resources.website_h;
        }
        private void btnOfficialSite_MouseUp(object sender, MouseEventArgs e)
        {
            btnOfficialSite.BackgroundImage = Properties.Resources.website;
        }
        #endregion
        #region Discord Button
        private void btnDiscord_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://discordapp.com/invite/qzJ3d");
            this.ActiveControl = null;
        }
        private void btnDiscord_MouseDown(object sender, MouseEventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord_d;
        }
        private void btnDiscord_MouseEnter(object sender, EventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord_h;
        }
        private void btnDiscord_MouseLeave(object sender, EventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord;
        }
        private void btnDiscord_MouseUp(object sender, MouseEventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord;
        }
        #endregion
        #region Group Tracker
        private void btnGroupTracker_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            MessageBox.Show("Group Tracker will be implemented soon!", "Oops!");
            this.ActiveControl = null;
        }
        private void btnGroupTracker_MouseDown(object sender, MouseEventArgs e)
        {
            btnGroupTracker.BackgroundImage = Properties.Resources.group_d;
        }
        private void btnGroupTracker_MouseEnter(object sender, EventArgs e)
        {
            btnGroupTracker.BackgroundImage = Properties.Resources.group_h;
        }
        private void btnGroupTracker_MouseLeave(object sender, EventArgs e)
        {
            btnGroupTracker.BackgroundImage = Properties.Resources.group;
        }
        private void btnGroupTracker_MouseUp(object sender, MouseEventArgs e)
        {
            btnGroupTracker.BackgroundImage = Properties.Resources.group;
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
        private void btnChangeBg_MouseDown(object sender, MouseEventArgs e)
        {
            btnChangeBg.BackgroundImage = Properties.Resources.theme_d;
        }
        private void btnChangeBg_MouseEnter(object sender, EventArgs e)
        {
            btnChangeBg.BackgroundImage = Properties.Resources.theme_h;
        }
        private void btnChangeBg_MouseLeave(object sender, EventArgs e)
        {
            btnChangeBg.BackgroundImage = Properties.Resources.theme;
        }
        private void btnChangeBg_MouseUp(object sender, MouseEventArgs e)
        {
            btnChangeBg.BackgroundImage = Properties.Resources.theme;
        }
        #endregion
        #region Options Button
        private void btnOptions_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            frmOptions op = new frmOptions();
            op.ShowDialog();
            // Apply user settings
            if (Properties.Settings.Default.wantsCursor == true) // Cursor
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
        private void btnOptions_MouseDown(object sender, MouseEventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options_d;
        }
        private void btnOptions_MouseEnter(object sender, EventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options_h;
        }
        private void btnOptions_MouseLeave(object sender, EventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options;
        }
        private void btnOptions_MouseUp(object sender, MouseEventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options;
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
        private void btnCredits_MouseDown(object sender, MouseEventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits_d;
        }
        private void btnCredits_MouseEnter(object sender, EventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits_h;
        }
        private void btnCredits_MouseLeave(object sender, EventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits;
        }
        private void btnCredits_MouseUp(object sender, MouseEventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits;
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
        #region File Updater
        private void Updater_DoWork(object sender, DoWorkEventArgs e)
        {
            _currentFile = 0; // Reset the value so every time user plays totalProg
            string responseFromServer = "";
            try
            {
                responseFromServer = Data.RequestData("https://www.projectaltis.com/api/manifest", "GET");
            }
            catch (Exception)
            {
                Console.WriteLine("An exception was generated while requesting data to the manifest.");
            }
            
            string[] array = responseFromServer.Split('#'); // Seperate each json value into an index


            Console.WriteLine("The length of array is {0}", array.Length);
            Directory.CreateDirectory(_currentDir + "config\\");
            Directory.CreateDirectory(_currentDir + "resources\\");
            Directory.CreateDirectory(_currentDir + "resources\\default\\");
            _totalFiles = array.Length - 1;
            for (int i = 0; i < array.Length - 1; i++)
            {
                _currentFile += 1;
                manifest patchManifest = JsonConvert.DeserializeObject<manifest>(array[i]);
                WebClient client = new WebClient();
                if (patchManifest.filename != null || patchManifest.filename != "")
                {
                    if (patchManifest.filename.Contains("phase"))
                    {
                        if (Hashing.CompareSHA256(_currentDir + "resources\\default\\" + patchManifest.filename, patchManifest.sha256))
                        {
                            Console.WriteLine("Phase file: {0} is up to date!", patchManifest.filename);
                        }
                        else
                        {
                            _nowDownloading = patchManifest.filename;
                            Updater.ReportProgress(0); // Fire the progress changed event
                            Console.WriteLine("Starting download for phase file: {0}", patchManifest.filename);
                            client.DownloadFile(new Uri(patchManifest.url), _currentDir + "resources\\default\\" + patchManifest.filename);
                            Console.WriteLine("Finished!");
                        }
                    }
                    else if (patchManifest.filename.Contains("toon"))
                    {
                        if (Hashing.CompareSHA256(_currentDir + "config\\" + patchManifest.filename, patchManifest.sha256))
                        {

                        }
                        else
                        {
                            _nowDownloading = patchManifest.filename;
                            Updater.ReportProgress(0); // Fire the progress changed event
                            client.DownloadFile(new Uri(patchManifest.url), _currentDir + "config\\" + patchManifest.filename);
                        }

                    }
                    else if (Hashing.CompareSHA256(_currentDir + patchManifest.filename, patchManifest.sha256)) // If the hashes are the same skip the update
                    {
                        Console.WriteLine("{0} is up to date!", patchManifest.filename);
                    }
                    else
                    {
                        _nowDownloading = patchManifest.filename;
                        Updater.ReportProgress(0); // Fire the progress changed event
                        Console.WriteLine("Starting download for file: {0}", patchManifest.filename);
                        client.DownloadFile(new Uri(patchManifest.url), _currentDir + patchManifest.filename);
                        Console.WriteLine("Finished!");
                    }
                }



                _totalProgress = ((_currentFile / _totalFiles) * 100);
                Console.WriteLine("Total progress is {0}", _totalProgress);
                Updater.ReportProgress(Convert.ToInt32(_totalProgress));

            }
        }
        private void Updater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblNowDownloading.Visible = true;
            lblNowDownloading.Text = "Downloading " + _nowDownloading;
            if (_totalProgress == 100)
            {
                
                lblNowDownloading.Text = "";
                lblNowDownloading.Visible = false;
                // Launch game once all files are download
                Play.LaunchGame(txtUser.Text, txtPass.Text);
            }

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
        private void Updater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPlay.Enabled = true; // Re-enable button
        }
    }
}
