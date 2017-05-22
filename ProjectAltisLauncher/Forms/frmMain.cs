using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using ProjectAltis.Core;
using ProjectAltis.Enums;
using ProjectAltis.Manifests;
using ProjectAltis.Properties;
using ProjectAltisLauncher.Core;

/*
* TODO:
*    Catch Exceptions
*    Enable uploading of logs to pastebin - Idea suggested by Judge2020
*/
namespace ProjectAltis.Forms
{
    public partial class FrmMain : Form
    {
        #region Fields
        private readonly SortedList<string, string> downloadList = new SortedList<string, string>(); // Filename, URL
        private readonly string currentDir;
        private string nowDownloading;
        #endregion
        #region Main Form Events
        public FrmMain()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            currentDir = Directory.GetCurrentDirectory() + @"\";
            nowDownloading = "";

            Settings.Default.password = "Deprecated";

            if (!IsWriteable())
            {
                MessageBox.Show(@"It appears you do not have permission to write the the current directory: " + currentDir + @"
" +
                                @"The launcher may not work correctly without permissions. 
" +
                                @"Try running the launcher with administrator rights or installing in a different location.");
            }

            versionLabel.Text = "Launcher v" + typeof(Program).Assembly.GetName().Version.ToString();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show(@"This is a debug build, do not put this into production",
                @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            #region Loading Settings
            try
            {
                txtUser.Text = Settings.Default.username;
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
            // Read user settings
            if (Settings.Default.wantsCursor) // Cursor
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Resources.toonmono);
                Cursor = new Cursor(cursorMemoryStream);
            }
            // Load last saved user background choice
            BackgroundImage.Dispose();
            if (Settings.Default.wantsRandomBg)
            {
                BackgroundImage = Background.ReturnRandomBackground();
            }
            else
            {
                BackgroundImage = Background.ReturnBackground(Settings.Default.background);
            }
            #endregion
            // This prevents other controls from being focused
            Select();
            ActiveControl = null;
            ActiveControl = string.IsNullOrEmpty(txtUser.Text) ? txtUser : txtPass;
            Button_MouseLeave(btnPlay, EventArgs.Empty);

            webBrowser1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, webBrowser1.Width, webBrowser1.Height, 20, 20));

            RedistCheck.CheckRedistHandler();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            ActiveControl = null;
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            ActiveControl = null;
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
            if (sender is Form f)
                f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X),
                    f.Location.Y + (e.Y - mouseDownPoint.Y));
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
            WindowState = FormWindowState.Minimized;
            ActiveControl = null;
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
            if (cbSaveLogin.Checked)
            {
                Console.WriteLine(@"Save checked");
                if (txtUser.Text != null || txtPass.Text != null)
                {
                    Settings.Default.username = txtUser.Text;
                    Settings.Default.Save();
                }
            }
            #endregion
            FileUpdater updater = new FileUpdater(this);
            Thread update = new Thread(() => { updater.DoWork(); });
            try
            {
                update.Start();
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Unable to start the updating process. It appears your computer is out of memory.");
            }
            catch (ThreadStateException)
            {
                MessageBox.Show("The updater thread could not be started. Try and restarting the launcher.");
            }
            ActiveControl = null;
        }
        #endregion
        #region Site Button
        private void btnOfficialSite_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://www.projectaltis.com/");
            ActiveControl = null;
        }
        #endregion
        #region Discord Button
        private void btnDiscord_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Log.TryOpenUrl("https://discord.me/ttprojectaltis");
            ActiveControl = null;
        }
        #endregion
        #region Content Packs
        private void btnContentPacks_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            btnContentPacks.BackgroundImage = Resources.contentpacks_d;
            frmContentPacks contentPack = new frmContentPacks();
            contentPack.ShowDialog(this);
            ActiveControl = null;
        }
        #endregion
        #region Change Theme
        private void btnChangeBg_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            frmBackgroundChoices bg = new frmBackgroundChoices();
            bg.ShowDialog();
            if (!Settings.Default.wantsRandomBg)
            {
                BackgroundImage.Dispose();
                BackgroundImage = Background.ReturnBackground(Settings.Default.background);
            }

            ActiveControl = null;
        }
        #endregion
        #region Options Button
        private void btnOptions_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            frmOptions op = new frmOptions();
            op.ShowDialog();
            // Apply user settings
            if (Settings.Default.wantsCursor) // Cursor
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Resources.toonmono);
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
        private void btnCredits_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            frmCredits cred = new frmCredits();
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
        #region Web Browser / News
        // Place any web browser events inside here
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
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
            Log.Info("Is writable called");
            try
            {
                using (File.Create(currentDir + "writeText"))
                { }
                File.Delete(currentDir + "writeText");
                Log.Info("Directory is writeable");
            }
            catch (Exception ex)
            {
                Log.Error("Exception thrown, not writeable");
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

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
    }
}