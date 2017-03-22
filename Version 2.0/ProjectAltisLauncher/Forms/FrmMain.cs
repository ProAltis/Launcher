using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjectAltisLauncher.Core;
using System.Threading;

namespace ProjectAltisLauncher.Forms
{
    // The Main Form should only have designer events.
    // All tasks must be put into separate classes.
    // ===============================================
    // Buttons:
    // When a button is created, it must mouse enter, mouse leave, mouse down, mouse up, and click events.
    // This is important for visual effects.
    // ===============================================
    // Bugs:
    //      None at the moment
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            this.Icon = Properties.Resources.pieicon;
            InitializeComponent();
            try
            {
                wbNews.Navigate(new Uri("https://projectaltis.com/launcher"));
            }
            catch (Exception)
            {
                Console.WriteLine("[FrmMain] Error al navegar por Internet");
            }        
        }

        #region FrmMain

        private void FrmMain_Load(object sender, EventArgs e)
        {
            #region Set background image

            if (!Properties.Settings.Default.RandomBackgrounds)
            {
                this.BackgroundImage = Graphics.ReturnBackground(Properties.Settings.Default.Background);
            }
            else
            {
                this.BackgroundImage = Graphics.ReturnRandomBackground();
            }

            #endregion

            #region Apply Cursor
            if (Properties.Settings.Default.WantsToontownCursor)
            {
                System.IO.MemoryStream cursorMemoryStream = new System.IO.MemoryStream(Properties.Resources.toonmono);
                Cursor = new Cursor(cursorMemoryStream);
            }
            #endregion
        }

        private System.Drawing.Point mouseDownPoint = System.Drawing.Point.Empty;

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPoint = new System.Drawing.Point(e.X, e.Y);
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = System.Drawing.Point.Empty;
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new System.Drawing.Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }

        #endregion

        private void btnWebsite_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://www.projectaltis.com/");
            this.ActiveControl = null;
        }

        private void btnDiscord_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://discord.me/ttprojectaltis");
            this.ActiveControl = null;
        }

        private void btnContentPacks_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmContentPacks contentPack = new FrmContentPacks();
            this.ActiveControl = null;
            contentPack.ShowDialog(this);
            this.ActiveControl = null;

        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmBackgroundChoices bg = new FrmBackgroundChoices(this);
            this.ActiveControl = null;
            bg.ShowDialog(this);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmOptions options = new FrmOptions(this);
            this.ActiveControl = null;
            options.ShowDialog(this);
        }

        private void btnCredits_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmCredits credits = new FrmCredits();
            this.ActiveControl = null;
            credits.ShowDialog();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            if (cbSaveLogin.Checked)
            {
                if (!string.IsNullOrEmpty(txtUser.Text))
                {
                    Properties.Settings.Default.Username = txtUser.Text;
                    Properties.Settings.Default.Save();
                }
            }
            Updater updater = new Updater(this);
            Thread update = new Thread(() =>
            {
                updater.DoWork();
            });
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
            
            this.ActiveControl = null;
        }

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

        #region News Events
        private void wbNews_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!(e.Url.ToString().Contains("https://projectaltis.com/launcher")))
            {
                e.Cancel = true;
                Process.Start(e.Url.ToString());
            }
        }
        #endregion

        #region Mouse Enter
        private void btnWebsite_MouseEnter(object sender, EventArgs e)
        {
            btnWebsite.BackgroundImage = Properties.Resources.website_h;
        }

        private void btnDiscord_MouseEnter(object sender, EventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord_h;
        }

        private void btnContentPacks_MouseEnter(object sender, EventArgs e)
        {
            btnContentPacks.BackgroundImage = Properties.Resources.contentpacks_h;
        }

        private void btnTheme_MouseEnter(object sender, EventArgs e)
        {
            btnTheme.BackgroundImage = Properties.Resources.theme_h;
        }

        private void btnOptions_MouseEnter(object sender, EventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options_h;
        }

        private void btnCredits_MouseEnter(object sender, EventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits_h;
        }

        private void btnPlay_MouseEnter(object sender, EventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play_h;
        }

        #endregion

        #region Mouse Leave

        private void btnWebsite_MouseLeave(object sender, EventArgs e)
        {
            btnWebsite.BackgroundImage = Properties.Resources.website;
        }

        private void btnDiscord_MouseLeave(object sender, EventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord;
        }

        private void btnContentPacks_MouseLeave(object sender, EventArgs e)
        {
            btnContentPacks.BackgroundImage = Properties.Resources.contentpacks;
        }

        private void btnTheme_MouseLeave(object sender, EventArgs e)
        {
            btnTheme.BackgroundImage = Properties.Resources.theme;
        }

        private void btnOptions_MouseLeave(object sender, EventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options;
        }

        private void btnCredits_MouseLeave(object sender, EventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits;
        }

        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play;
        }

        #endregion

        #region Mouse Down

        private void btnWebsite_MouseDown(object sender, MouseEventArgs e)
        {
            btnWebsite.BackgroundImage = Properties.Resources.website_d;
        }

        private void btnDiscord_MouseDown(object sender, MouseEventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord_d;
        }

        private void btnContentPacks_MouseDown(object sender, MouseEventArgs e)
        {
            btnContentPacks.BackgroundImage = Properties.Resources.contentpacks_d;
        }

        private void btnTheme_MouseDown(object sender, MouseEventArgs e)
        {
            btnTheme.BackgroundImage = Properties.Resources.theme_d;
        }

        private void btnOptions_MouseDown(object sender, MouseEventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options_d;
        }

        private void btnCredits_MouseDown(object sender, MouseEventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits_d;
        }

        private void btnPlay_MouseDown(object sender, MouseEventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play_d;
        }

        #endregion

        #region Mouse Up

        private void btnWebsite_MouseUp(object sender, MouseEventArgs e)
        {
            btnWebsite.BackgroundImage = Properties.Resources.website;
        }

        private void btnDiscord_MouseUp(object sender, MouseEventArgs e)
        {
            btnDiscord.BackgroundImage = Properties.Resources.discord;
        }

        private void btnContentPacks_MouseUp(object sender, MouseEventArgs e)
        {
            btnContentPacks.BackgroundImage = Properties.Resources.contentpacks;
        }

        private void btnTheme_MouseUp(object sender, MouseEventArgs e)
        {
            btnTheme.BackgroundImage = Properties.Resources.theme;
        }

        private void btnOptions_MouseUp(object sender, MouseEventArgs e)
        {
            btnOptions.BackgroundImage = Properties.Resources.options;
        }

        private void btnCredits_MouseUp(object sender, MouseEventArgs e)
        {
            btnCredits.BackgroundImage = Properties.Resources.credits;
        }

        private void btnPlay_MouseUp(object sender, MouseEventArgs e)
        {
            btnPlay.BackgroundImage = Properties.Resources.play;
        }

        #endregion
    }
}
