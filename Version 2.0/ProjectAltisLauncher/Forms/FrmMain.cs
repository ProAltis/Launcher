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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ProjectAltisLauncher.Core;
using ProjectAltisLauncher.Properties;
using Graphics = ProjectAltisLauncher.Core.Graphics;

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
            this.Icon = Resources.pieicon;
            InitializeComponent();
            try
            {
                this.wbNews.Navigate(new Uri("https://projectaltis.com/launcher"));
            }
            catch (Exception)
            {
                Console.WriteLine("[FrmMain] Error al navegar por Internet");
            }
        }

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
            if (this.cbSaveLogin.Checked)
                if (!string.IsNullOrEmpty(this.txtUser.Text))
                {
                    Settings.Default.Username = this.txtUser.Text;
                    Settings.Default.Save();
                }
            Updater updater = new Updater(this);
            Thread update = new Thread(() => { updater.DoWork(); });
            try
            {
                update.Start();
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(
                    Resources
                        .FrmMain_btnPlay_Click_Unable_to_start_the_updating_process__It_appears_your_computer_is_out_of_memory_);
            }
            catch (ThreadStateException)
            {
                MessageBox.Show(
                    Resources
                        .FrmMain_btnPlay_Click_The_updater_thread_could_not_be_started__Try_and_restarting_the_launcher_);
            }

            this.ActiveControl = null;
        }

        private void txtPassAndUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.btnPlay.Enabled)
                    this.btnPlay.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #region News Events

        private void wbNews_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!e.Url.ToString().Contains("https://projectaltis.com/launcher"))
            {
                e.Cancel = true;
                Process.Start(e.Url.ToString());
            }
        }

        #endregion

        #region FrmMain

        private void FrmMain_Load(object sender, EventArgs e)
        {
            #region Set background image

            if (!Settings.Default.RandomBackgrounds)
                this.BackgroundImage = Graphics.ReturnBackground(Settings.Default.Background);
            else
                this.BackgroundImage = Graphics.ReturnRandomBackground();

            #endregion

            #region Apply Cursor

            if (Settings.Default.WantsToontownCursor)
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Resources.toonmono);
                this.Cursor = new Cursor(cursorMemoryStream);
            }

            #endregion
        }

        private Point mouseDownPoint = Point.Empty;

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDownPoint = new Point(e.X, e.Y);
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDownPoint = Point.Empty;
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - this.mouseDownPoint.X),
                f.Location.Y + (e.Y - this.mouseDownPoint.Y));
        }

        #endregion

        #region Mouse Enter

        private void btnWebsite_MouseEnter(object sender, EventArgs e)
        {
            this.btnWebsite.BackgroundImage = Resources.website_h;
        }

        private void btnDiscord_MouseEnter(object sender, EventArgs e)
        {
            this.btnDiscord.BackgroundImage = Resources.discord_h;
        }

        private void btnContentPacks_MouseEnter(object sender, EventArgs e)
        {
            this.btnContentPacks.BackgroundImage = Resources.contentpacks_h;
        }

        private void btnTheme_MouseEnter(object sender, EventArgs e)
        {
            this.btnTheme.BackgroundImage = Resources.theme_h;
        }

        private void btnOptions_MouseEnter(object sender, EventArgs e)
        {
            this.btnOptions.BackgroundImage = Resources.options_h;
        }

        private void btnCredits_MouseEnter(object sender, EventArgs e)
        {
            this.btnCredits.BackgroundImage = Resources.credits_h;
        }

        private void btnPlay_MouseEnter(object sender, EventArgs e)
        {
            this.btnPlay.BackgroundImage = Resources.play_h;
        }

        #endregion

        #region Mouse Leave

        private void btnWebsite_MouseLeave(object sender, EventArgs e)
        {
            this.btnWebsite.BackgroundImage = Resources.website;
        }

        private void btnDiscord_MouseLeave(object sender, EventArgs e)
        {
            this.btnDiscord.BackgroundImage = Resources.discord;
        }

        private void btnContentPacks_MouseLeave(object sender, EventArgs e)
        {
            this.btnContentPacks.BackgroundImage = Resources.contentpacks;
        }

        private void btnTheme_MouseLeave(object sender, EventArgs e)
        {
            this.btnTheme.BackgroundImage = Resources.theme;
        }

        private void btnOptions_MouseLeave(object sender, EventArgs e)
        {
            this.btnOptions.BackgroundImage = Resources.options;
        }

        private void btnCredits_MouseLeave(object sender, EventArgs e)
        {
            this.btnCredits.BackgroundImage = Resources.credits;
        }

        private void btnPlay_MouseLeave(object sender, EventArgs e)
        {
            this.btnPlay.BackgroundImage = Resources.play;
        }

        #endregion

        #region Mouse Down

        private void btnWebsite_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnWebsite.BackgroundImage = Resources.website_d;
        }

        private void btnDiscord_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnDiscord.BackgroundImage = Resources.discord_d;
        }

        private void btnContentPacks_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnContentPacks.BackgroundImage = Resources.contentpacks_d;
        }

        private void btnTheme_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnTheme.BackgroundImage = Resources.theme_d;
        }

        private void btnOptions_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnOptions.BackgroundImage = Resources.options_d;
        }

        private void btnCredits_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnCredits.BackgroundImage = Resources.credits_d;
        }

        private void btnPlay_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnPlay.BackgroundImage = Resources.play_d;
        }

        #endregion

        #region Mouse Up

        private void btnWebsite_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnWebsite.BackgroundImage = Resources.website;
        }

        private void btnDiscord_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnDiscord.BackgroundImage = Resources.discord;
        }

        private void btnContentPacks_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnContentPacks.BackgroundImage = Resources.contentpacks;
        }

        private void btnTheme_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnTheme.BackgroundImage = Resources.theme;
        }

        private void btnOptions_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnOptions.BackgroundImage = Resources.options;
        }

        private void btnCredits_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnCredits.BackgroundImage = Resources.credits;
        }

        private void btnPlay_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnPlay.BackgroundImage = Resources.play;
        }

        #endregion
    }
}