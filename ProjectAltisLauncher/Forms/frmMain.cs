using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ProjectAltis.Core;
using ProjectAltis.Forms.ContentPacks;
using ProjectAltis.Properties;

namespace ProjectAltis.Forms
{
    public partial class FrmMain : Form
    {
        #region Fields
        private readonly string _currentDir;
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
                MessageBox.Show(@"It appears you do not have permission to write the the current directory: " + _currentDir + @"
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
                if (Settings.Default.WantPassword)
                    txtPass.Text = UwpHelper.GetPassword();
            }
            catch (Exception ex)
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


        }

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
            RedistCheck.CheckRedistHandler();
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
            {
                return;
            }
            if (sender is Form)
            {
                Form f = (Form)sender;
                f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X),
                                f.Location.Y + (e.Y - mouseDownPoint.Y));
            }
        }

        #endregion
        #region Button Behaviors
        #region Exit Button
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                Log.TryOpenUrl("https://projectaltis.com/register");
                return;
            }
            btnPlay.Enabled = false;
            ErrorReporter.Instance.Username = txtUser.Text;
            #region Save credentials if necessary
            Settings.Default.username = txtUser.Text;
            Settings.Default.Save();
            if (Settings.Default.WantPassword)
            {
                Log.Info("Trying to save password securely...");
                try
                {
                    UwpHelper.SetCredentials(txtUser.Text, txtPass.Text);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    Log.Error("Don't want to break after trying to save pass so continuing");
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
            btnContentPacks.BackgroundImage = Resources.contentpacks_d;
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
            if (!Settings.Default.wantsRandomBg)
            {
                BackgroundImage.Dispose();
                BackgroundImage = Background.ReturnBackground(Settings.Default.background);
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
        #endregion

        private void TxtUser_TextChanged(object sender, EventArgs e)
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