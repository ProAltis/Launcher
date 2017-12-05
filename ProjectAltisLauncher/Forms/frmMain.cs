using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectAltis.Core;
using ProjectAltis.Forms.ContentPacks;
using CefSharp;
using CefSharp.WinForms;
using LauncherLib.Login;
using LauncherLib.Play;
using LauncherLib.Update;

namespace ProjectAltis.Forms
{
    public partial class FrmMain : Form
    {
        #region Fields
        private readonly string _currentDir;
        private readonly Uri _fileApi = new Uri("https://projectaltis.com/api/manifest");
        public ChromiumWebBrowser Browser;
        #endregion
        #region Main Form Events
        public FrmMain()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            _currentDir = Directory.GetCurrentDirectory() + @"\";
            if (!IsWriteable())
            {
                MessageBox.Show(@"It appears you do not have permission to write the the current directory: " + _currentDir + @"" +
                                @"The launcher may not work correctly without permissions." +
                                @"Try running the launcher with administrator rights. If you have problems, message ModMail bot on the Altis discord.");
            }

            versionLabel.Text = "Launcher v" + typeof(Program).Assembly.GetName().Version.ToString();
            ValidatePrefJson();
            //webBrowser1.Navigate("https://projectaltis.com/launcher", null, null, "User-Agent: Altis Launcher\r\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadUserSettings();
            ActiveControl = string.IsNullOrEmpty(txtUser.Text) ? txtUser : txtPass;
            // Hack to change the button text to "create account"
            Button_MouseLeave(btnPlay, EventArgs.Empty);
            InitBrowser();
        }

        #region Chromium Browser
        public void InitBrowser()
        {
            var cefsettings = new CefSettings();
            cefsettings.UserAgent = "Altis Launcher " + typeof(Program).Assembly.GetName().Version;
            Cef.Initialize(cefsettings);
            Browser = new ChromiumWebBrowser("https://projectaltis.com/launcher");
            this.Controls.Add(Browser);
            // Here, we use webbrowser1 (old internet explorer) to define the bounds of CEF browser
            Browser.Size = new Size(435, 525);
            Browser.Dock = DockStyle.None;
            Browser.Location = new Point(38, 76);
            Browser.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            Browser.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Browser.Width, Browser.Height, 20, 20));
            Browser.ConsoleMessage += BrowserOnConsoleMessage;
            Browser.LifeSpanHandler = new BrowserLifeSpanHandler();
        }

        private void BrowserOnConsoleMessage(object sender, ConsoleMessageEventArgs consoleMessageEventArgs)
        {
            Log.Info($"Line: {consoleMessageEventArgs.Line}, Source: {consoleMessageEventArgs.Source}, Message: {consoleMessageEventArgs.Message}");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
        #endregion


        private void Form1_Activated(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            new RedistCheck(this).CheckRedistHandler();
        }
        #endregion
        #region Borderless Form Code

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // X-coordinate of upper-left corner
            int nTopRect, // Y-coordinate of upper-left corner
            int nRightRect, // X-coordinate of lower-right corner
            int nBottomRect, // Y-coordinate of lower-right corner
            int nWidthEllipse, // Height of ellipse
            int nHeightEllipse // Width of ellipse
        );

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
            {
                return;
            }
            if (sender is Form)
            {
                Form form = sender as Form;
                form.Location = new Point(form.Location.X + (e.X - mouseDownPoint.X),
                                form.Location.Y + (e.Y - mouseDownPoint.Y));
            }
        }

        #endregion
        #region Button Behaviors
        #region Exit Button
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Cef.Shutdown();
            Environment.Exit(0);
        }
        #endregion
        #region Minimize Button
        private void BtnMin_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            WindowState = FormWindowState.Minimized;
            ActiveControl = null;
        }
        #endregion
        #region Play Button
        private async void BtnPlay_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                Log.TryOpenUrl("https://projectaltis.com/register");
                return;
            }
            btnPlay.Enabled = false;

            ErrorReporter.Instance.Username = txtUser.Text;
            SaveCredentials(); // Save credentials if necessary


            IAccount account = new Account(txtUser.Text, txtPass.Text, LoginConfig.ProjectAltis);

            bool isGoodLogin = await Login(account);

            if (isGoodLogin)
            {
                // Don't want to block UI Thread
                // Run updater
                bool finished = false;

                // Enable progress bar for updating
                pbDownload.Visible = true;
                new Task(async () =>
                {
                    await PatchFiles();
                    finished = true;
                }).Start();

                while (!finished)
                {
                    await Task.Delay(200);
                }

                pbDownload.Visible = false;
                lblNowDownloading.Text = "";

                // Files are updated
                // Time to launch the game

                bool gameFinished = false;
                new Task(async () =>
                {
                    // NOTE: we need to still use Play.LaunchGame for security and code signing certificate purposes.
                    Play.LaunchGame(txtUser.Text, txtPass.Text, this);
                    gameFinished = true;
                }).Start();

                while (!gameFinished)
                {
                    await Task.Delay(200);
                }

                btnPlay.Enabled = true;

            }
            else
            {
                btnPlay.Enabled = true;
            }


            ActiveControl = null;
        }

        public async Task LaunchGame(IAccount account)
        {
            string processName = Path.Combine(Directory.GetCurrentDirectory(), "ProjectAltis");

            IGame game = new Game(account, "Project Altis", processName,
                "https://projectaltis.com/api/gameserver");

            bool success = await game.Run();
        }

        public async Task<bool> Login(IAccount account)
        {
            var response = await account.Login();

            // Make sure a bad response has not been given
            if (response == LoginAPIResponse.Empty)
            {
                lblInfo.ForeColor = Color.Red;
                lblInfo.Text = "Failed to contact the login servers.";
                return LoginAPIResponse.Bad;
            }

            switch (response.Status)
            {
                case LoginAPIResponse.Good:
                    {
                        lblInfo.ForeColor = Color.Green;
                        lblInfo.Text = response.Reason;

                        return LoginAPIResponse.Good;
                    }
                case LoginAPIResponse.Bad:
                    {
                        lblInfo.ForeColor = Color.Red;
                        lblInfo.Text = response.Reason;

                        return LoginAPIResponse.Bad;
                    }
                default:
                    {
                        return LoginAPIResponse.Bad;
                    }
            }
        }

        public async Task PatchFiles()
        {
            using (var updater = new LauncherLib.Update.Updater(_fileApi, Directory.GetCurrentDirectory()))
            {

                // Subscribe
                updater.FileStartDownload += OnFileStartDownload;
                updater.FileDownloadProgressChanged += OnFileDownloadProgressChanged;
                updater.FileDownloaded += OnFileDownloaded;

                // Patch
                await updater.PatchFiles();

                // Unsubscribe
                updater.FileStartDownload -= OnFileStartDownload;
                updater.FileDownloadProgressChanged -= OnFileDownloadProgressChanged;
                updater.FileDownloaded -= OnFileDownloaded;
            }
        }

        private void OnFileDownloaded(object sender, FileManifestEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    lblNowDownloading.Text = "Finished downloading " + e.File.Filename;
                }));
            }
            else
            {
                lblNowDownloading.Text = "Finished downloading " + e.File.Filename;
            }
        }

        private void OnFileDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage % 2 != 0)
            {
                // Only update progress every positive intervals
                // 2%, 4%, 6%...
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    pbDownload.Value = e.ProgressPercentage;
                }));
            }
            else
            {
                pbDownload.Value = e.ProgressPercentage;
            }
        }

        private void OnFileStartDownload(object sender, FileManifestEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    lblNowDownloading.Text = "Downloading " + e.File.Filename;
                }));
            }
            else
            {
                lblNowDownloading.Text = "Downloading " + e.File.Filename;
            }
        }

        #endregion
        #region Site Button
        private void BtnOfficialSite_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://www.projectaltis.com/");
            ActiveControl = null;
        }
        #endregion
        #region Discord Button
        private void BtnDiscord_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Log.TryOpenUrl("https://discord.me/ttprojectaltis");
            ActiveControl = null;
        }
        #endregion
        #region Content Packs
        private void BtnContentPacks_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            btnContentPacks.BackgroundImage = Properties.Resources.contentpacks_d;
            FrmContentPacks contentPack = new FrmContentPacks();
            contentPack.ShowDialog(this);
            ActiveControl = null;
        }
        #endregion
        #region Change Theme
        private void BtnChangeBg_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmBackgroundChoices bg = new FrmBackgroundChoices();
            bg.ShowDialog();
            if (!Settings.Instance.WantRandomBackgrounds)
            {
                BackgroundImage.Dispose();
                BackgroundImage = Background.ReturnBackground(Settings.Instance.Background);
            }

            ActiveControl = null;
        }
        #endregion
        #region Options Button
        private void BtnOptions_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmOptions op = new FrmOptions();
            op.ShowDialog();
            // Apply user settings
            if (Settings.Instance.WantCursor) // Cursor
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Properties.Resources.toonmono);
                Cursor = new Cursor(cursorMemoryStream);
            }
            else
            {
                Cursor = Cursors.Default;
            }
            ActiveControl = null;
        }
        #endregion
        #region Credits
        private void BtnCredits_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmCredits cred = new FrmCredits();
            cred.ShowDialog();
            ActiveControl = null;
        }
        #endregion
        #region Main Button Events
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
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
        private void TxtPassAndUser_KeyDown(object sender, KeyEventArgs e)
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
        #region Web Browser / News
        // Place any web browser events inside here
        private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString().Contains("https://projectaltis.com/launcher"))
            {
                return;
            }
            e.Cancel = true;
            Log.TryOpenUrl(e.Url.ToString());
        }

        #endregion

        #region Directory Things

        /// <summary>
        /// Determines whether the current directory can be written to.
        /// </summary>
        /// <returns><c>true</c> if the directory is writeable; otherwise, <c>false</c>.</returns>
        private bool IsWriteable()
        {
            try
            {
                Log.Info("Checking if directory is writeable.");
                if (!File.Exists(_currentDir + "writeTest"))
                {
                    using (File.Create(_currentDir + "writeTest"))
                    {
                    }
                    File.Delete(_currentDir + "writeTest");
                }
                Log.Info("Directory is writeable.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception thrown, not writeable");
                Log.Error(ex);
                return false;
            }
        }

        private void ValidatePrefJson()
        {
            if (!File.Exists("preferences.json"))
            {
                // Will be automatically created by the game
                return;
            }
            string prefFile = File.ReadAllText("preferences.json");
            try
            {
                JObject contents = JObject.Parse(prefFile);
            }
            catch (JsonReaderException ex)
            {
                File.Delete("preferences.json");
                Log.Info("DELETED Preferences.json due to it being corrupted");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }
        #endregion

        private void LoadUserSettings()
        {
            try
            {
                txtUser.Text = Settings.Instance.Username;
                if (Settings.Instance.WantSavePassword)
                {
                    txtPass.Text = UwpHelper.GetPassword();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            // Read user settings
            if (Settings.Instance.WantCursor) // Old Toontown Cursors
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Properties.Resources.toonmono);
                Cursor = new Cursor(cursorMemoryStream);
            }
            // Load last saved user background choice
            BackgroundImage.Dispose();
            if (Settings.Instance.WantRandomBackgrounds)
            {
                BackgroundImage = Background.ReturnRandomBackground();
            }
            else
            {
                BackgroundImage = Background.ReturnBackground(Settings.Instance.Background);
            }
        }

        public void SaveCredentials()
        {
            Settings.Instance.Username = txtUser.Text;
            if (Settings.Instance.WantSavePassword)
            {
                Log.Info("Trying to save password via Windows Credential Manager...");
                try
                {
                    UwpHelper.SetCredentials(txtUser.Text, txtPass.Text);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    Log.Error("Error when saving password via Credential Manager. Continuing...");
                }
            }
        }

        private void TxtUser_TextChanged(object sender, EventArgs e)
        {
            Button_MouseLeave(btnPlay, EventArgs.Empty);
        }
    }
}