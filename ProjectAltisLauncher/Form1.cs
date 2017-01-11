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
/// <summary>
/// TODO:
///     Add group tracker
/// </summary>
namespace ProjectAltisLauncher
{
    public partial class Form1 : Form
    {
        #region Main Form Events
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                txtUser.Text = Properties.Settings.Default.username;
                txtPass.Text = Properties.Settings.Default.password;
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
                SetRandomBackground();
            }
            else
            {
                SetBackground(Properties.Settings.Default.background);
            }

            new System.Threading.Thread(() =>
            {
                CheckForUpdate();
            }).Start(); 
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
        #region Global Variables
        private string currentDir = Directory.GetCurrentDirectory() + "\\";
        private double totalFiles = 0;
        private double totalProgress;
        private double currentFile = 0;
        private string nowDownloading = "";
        private Random _rand = new Random(); // Pretty random
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
            this.Close();
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
            PlaySoundFile("sndclick");
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
            PlaySoundFile("sndclick");
            if (cbSaveLogin.Checked == true)
            {
                Console.WriteLine("Save checked");
                if (txtUser.Text != null || txtPass.Text != null)
                {
                    Properties.Settings.Default.username = txtUser.Text;
                    Properties.Settings.Default.password = txtPass.Text;
                    Properties.Settings.Default.Save();
                }

            }
            string finalURL = "https://www.projectaltis.com/api/?u=" + txtUser.Text + "&p=" + txtPass.Text;
            string APIResponse = RequestData(finalURL, "GET"); // Send request to login API, store the response as string
            loginAPIResponse resp = JsonConvert.DeserializeObject<loginAPIResponse>(APIResponse);

            switch (resp.status)
            {
                case "true":
                    lblInfo.Text = resp.reason;
                    Updater.RunWorkerAsync();
                    break;
                case "false":
                    lblInfo.Text = resp.reason;
                    btnPlay.Enabled = true;
                    break;
                case "critical":
                    lblInfo.Text = resp.additional;
                    btnPlay.Enabled = true;
                    break;
                case "info":
                    lblInfo.Text = resp.reason;
                    btnPlay.Enabled = true;
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
        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play;
        }
        #endregion
        #region Site Button
        private void btnOfficialSite_Click(object sender, EventArgs e)
        {
            PlaySoundFile("sndclick");
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
            PlaySoundFile("sndclick");
            Process.Start("https://discord.gg/szEPYtV");
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
            PlaySoundFile("sndclick");
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
            PlaySoundFile("sndclick");
            BackgroundChoices bg = new BackgroundChoices();
            bg.ShowDialog();
            if (!Properties.Settings.Default.wantsRandomBg)
            {
                this.BackgroundImage.Dispose();
                SetBackground(Properties.Settings.Default.background);
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
            PlaySoundFile("sndclick");
            Options.Options op = new Options.Options();
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
            PlaySoundFile("sndclick");
            Credits.Credits f = new Credits.Credits();
            f.ShowDialog();
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
                btnPlay.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        #endregion
        #endregion
        private static void PlaySoundFile(string filename)
        {
            if (Properties.Settings.Default.wantsClickSounds)
            {
                System.Media.SoundPlayer player;
                switch (filename.ToLower())
                {
                    case "sndclick":
                        player = new System.Media.SoundPlayer(Properties.Resources.sndclick);
                        player.Load();
                        player.Play();
                        break;
                }
            }

        }
        private string RequestData(string URL, string Method)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Method = Method;
                WebResponse response = request.GetResponse();
                Stream dataStream = default(Stream);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Thrown: " + "\n  Type:    " + ex.GetType().Name + "\n  Message: " + ex.Message + "\n Further errors may occur");
            }
            return "";
        }
        #region File Updater
        private void Updater_DoWork(object sender, DoWorkEventArgs e)
        {
            currentFile = 0; // Reset the value so every time user plays totalProg
            string responseFromServer = RequestData("https://www.projectaltis.com/api/manifest", "GET");
            string[] array = responseFromServer.Split('#'); // Seperate each json value into an index


            Console.WriteLine("The length of array is {0}", array.Length);
            Directory.CreateDirectory(currentDir + "config\\");
            Directory.CreateDirectory(currentDir + "resources\\");
            Directory.CreateDirectory(currentDir + "resources\\default\\");
            totalFiles = array.Length - 1;
            for (int i = 0; i < array.Length - 1; i++)
            {
                currentFile += 1;
                manifest patchManifest = JsonConvert.DeserializeObject<manifest>(array[i]);
                WebClient client = new WebClient();
                if (patchManifest.filename != null || patchManifest.filename != "")
                {
                    if (patchManifest.filename.Contains("phase"))
                    {
                        if (Hashing.CompareFileSize(currentDir + "resources\\default\\" + patchManifest.filename, Convert.ToInt32(patchManifest.size)))
                        {
                            Console.WriteLine("Phase file: {0} is up to date!", patchManifest.filename);
                        }
                        else
                        {
                            nowDownloading = patchManifest.filename;
                            Updater.ReportProgress(0); // Fire the progress changed event
                            Console.WriteLine("Starting download for phase file: {0}", patchManifest.filename);
                            client.DownloadFile(new Uri(patchManifest.url), currentDir + "resources\\default\\" + patchManifest.filename);
                            Console.WriteLine("Finished!");
                        }
                    }
                    else if (patchManifest.filename.Contains("ProjectAltis"))
                    {
                        if (Hashing.CompareSHA256(currentDir + "config\\" + patchManifest.filename, patchManifest.sha256))
                        {

                        }
                        else
                        {
                            nowDownloading = patchManifest.filename;
                            Updater.ReportProgress(0); // Fire the progress changed event
                            client.DownloadFile(new Uri(patchManifest.url), currentDir + "config\\" + patchManifest.filename);
                        }

                    }
                    else if (Hashing.CompareSHA256(currentDir + patchManifest.filename, patchManifest.sha256)) // If the hashes are the same skip the update
                    {
                        Console.WriteLine("{0} is up to date!", patchManifest.filename);
                    }
                    else
                    {
                        nowDownloading = patchManifest.filename;
                        Updater.ReportProgress(0); // Fire the progress changed event
                        Console.WriteLine("Starting download for file: {0}", patchManifest.filename);
                        client.DownloadFile(new Uri(patchManifest.url), currentDir + patchManifest.filename);
                        Console.WriteLine("Finished!");
                    }
                }



                totalProgress = ((currentFile / totalFiles) * 100);
                Console.WriteLine("Total progress is {0}", totalProgress);
                Updater.ReportProgress(Convert.ToInt32(totalProgress));

            }
        }
        private void Updater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblNowDownloading.Visible = true;
            lblNowDownloading.Text = "Downloading " + nowDownloading;
            if (totalProgress == 100)
            {
                btnPlay.Enabled = true;
                lblNowDownloading.Text = "";
                lblNowDownloading.Visible = false;
                // Launch game once all files are download
                Play.LaunchGame(txtUser.Text, txtPass.Text);
            }

        }
        #endregion
        /// <summary>
        /// Checks for the latest update of the launcher manifest
        /// </summary>
        private void CheckForUpdate()
        {
            string responseFromServer = RequestData(@"https://projectaltis.com/api/launcherManifest", "GET");
            string[] array = responseFromServer.Split('#'); // Seperate each json value into an index
            bool restartRequired = false;
            for (int i = 0; i < array.Length - 1; i++) // - 1 Because string split creates one extra null line D:
            {
                // First compare the file against current file and see if it needs to be updated
                manifest patchManifest = JsonConvert.DeserializeObject<manifest>(array[i]);

                WebClient client = new WebClient();
                if (Hashing.CompareSHA256(currentDir + patchManifest.filename, patchManifest.sha256))
                {
                    // File is already up to date(Possibly Launcher)
                }
                #region Launcher Update
                else if (patchManifest.filename.ToLower() == "project altis launcher.exe")
                {
                    client.DownloadFile(patchManifest.url, "Launcher_New.exe");
                    try
                    {
                        File.Delete(Path.GetTempPath() + @"\updater.vbs");
                    }
                    catch (Exception){ }
                    
                    using (StreamWriter file = new StreamWriter(Path.GetTempPath() + @"\updater.vbs", true))
                    {
                        #region Write Update Script
                        file.WriteLine("WScript.Sleep 250"); // Wait .25 ms for main launcher to exit
                        file.WriteLine("Dim f");
                        file.WriteLine("Set f = WScript.CreateObject(\"Scripting.FileSystemObject\")");
                        file.WriteLine("Set obj = CreateObject(\"Scripting.FileSystemObject\")");
                        file.WriteLine("obj.DeleteFile(\"{0}\")", currentDir + "Project Altis Launcher.exe"); // Deletes current launcher to prevent IO Errors
                        file.WriteLine("f.MoveFile " + "\"" + currentDir + "Launcher_New.exe" + "\", " + "\"" + currentDir + "Project Altis Launcher.exe" + "\"");
                        file.WriteLine("Set objShell = WScript.CreateObject(\"WScript.Shell\")");
                        file.WriteLine("objShell.Run(\"\"\"{0}\"\"\")", currentDir + "Project Altis Launcher");
                        #endregion
                        restartRequired = true;
                    }
                }
                #endregion
                else
                {
                    client.DownloadFile(patchManifest.url, patchManifest.filename);
                }
            }

            if (restartRequired) // Checks if restart is required to update
            {
                Process.Start(Path.GetTempPath() + @"\updater.vbs");
                Application.Exit();
            }
        } 
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.IsWebBrowserContextMenuEnabled = false;

        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString().Contains("https://projectaltis.com/launcher"))
            {
                return;
            }
            e.Cancel = true;
            Process.Start(e.Url.ToString());
        }
        #region Background Methods
        private void SetRandomBackground()
        {
            int val = _rand.Next(1, 7); // Generates a random number 1-6
            switch (val)
            {
                case 1:
                    BackgroundImage = Properties.Resources.TTC;
                    break;
                case 2:
                    BackgroundImage = Properties.Resources.DD;
                    break;
                case 3:
                    BackgroundImage = Properties.Resources.DG;
                    break;
                case 4:
                    BackgroundImage = Properties.Resources.MML;
                    break;
                case 5:
                    BackgroundImage = Properties.Resources.Brrrgh;
                    break;
                case 6:
                    BackgroundImage = Properties.Resources.DDL;
                    break;
            }
        }
        private void SetBackground(string bg)
        {
            switch (bg)
            {
                case "TTC":
                    BackgroundImage = Properties.Resources.TTC;
                    break;
                case "DD":
                    BackgroundImage = Properties.Resources.DD;
                    break;
                case "DG":
                    BackgroundImage = Properties.Resources.DG;
                    break;
                case "MML":
                    BackgroundImage = Properties.Resources.MML;
                    break;
                case "Brrrgh":
                    BackgroundImage = Properties.Resources.Brrrgh;
                    break;
                case "DDL":
                    BackgroundImage = Properties.Resources.DDL;
                    break;
            }
        }
        #endregion

    }
}
