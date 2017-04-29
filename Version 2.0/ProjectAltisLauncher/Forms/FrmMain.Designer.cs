using System;

namespace ProjectAltisLauncher.Forms
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbNews = new System.Windows.Forms.WebBrowser();
            this.btnWebsite = new System.Windows.Forms.Button();
            this.btnDiscord = new System.Windows.Forms.Button();
            this.btnContentPacks = new System.Windows.Forms.Button();
            this.btnTheme = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnCredits = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.cbSaveLogin = new System.Windows.Forms.CheckBox();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.lblNowDownloading = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wbNews
            // 
            this.wbNews.AllowWebBrowserDrop = false;
            this.wbNews.IsWebBrowserContextMenuEnabled = false;
            this.wbNews.Location = new System.Drawing.Point(38, 76);
            this.wbNews.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbNews.Name = "wbNews";
            this.wbNews.ScriptErrorsSuppressed = true;
            this.wbNews.Size = new System.Drawing.Size(435, 518);
            this.wbNews.TabIndex = 21;
            this.wbNews.TabStop = false;
            this.wbNews.Url = new System.Uri("", System.UriKind.Relative);
            this.wbNews.WebBrowserShortcutsEnabled = false;
            this.wbNews.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbNews_Navigating);
            // 
            // btnWebsite
            // 
            this.btnWebsite.BackColor = System.Drawing.Color.Transparent;
            this.btnWebsite.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.website;
            this.btnWebsite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnWebsite.FlatAppearance.BorderSize = 0;
            this.btnWebsite.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnWebsite.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnWebsite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnWebsite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWebsite.Location = new System.Drawing.Point(26, 622);
            this.btnWebsite.Margin = new System.Windows.Forms.Padding(0, 0, 75, 0);
            this.btnWebsite.Name = "btnWebsite";
            this.btnWebsite.Size = new System.Drawing.Size(90, 41);
            this.btnWebsite.TabIndex = 22;
            this.btnWebsite.TabStop = false;
            this.btnWebsite.UseVisualStyleBackColor = false;
            this.btnWebsite.Click += new System.EventHandler(this.btnWebsite_Click);
            this.btnWebsite.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnWebsite_MouseDown);
            this.btnWebsite.MouseEnter += new System.EventHandler(this.btnWebsite_MouseEnter);
            this.btnWebsite.MouseLeave += new System.EventHandler(this.btnWebsite_MouseLeave);
            this.btnWebsite.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnWebsite_MouseUp);
            // 
            // btnDiscord
            // 
            this.btnDiscord.BackColor = System.Drawing.Color.Transparent;
            this.btnDiscord.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.discord;
            this.btnDiscord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDiscord.FlatAppearance.BorderSize = 0;
            this.btnDiscord.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDiscord.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDiscord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDiscord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiscord.Location = new System.Drawing.Point(191, 622);
            this.btnDiscord.Margin = new System.Windows.Forms.Padding(0, 0, 75, 0);
            this.btnDiscord.Name = "btnDiscord";
            this.btnDiscord.Size = new System.Drawing.Size(90, 41);
            this.btnDiscord.TabIndex = 23;
            this.btnDiscord.TabStop = false;
            this.btnDiscord.UseVisualStyleBackColor = false;
            this.btnDiscord.Click += new System.EventHandler(this.btnDiscord_Click);
            this.btnDiscord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDiscord_MouseDown);
            this.btnDiscord.MouseEnter += new System.EventHandler(this.btnDiscord_MouseEnter);
            this.btnDiscord.MouseLeave += new System.EventHandler(this.btnDiscord_MouseLeave);
            this.btnDiscord.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDiscord_MouseUp);
            // 
            // btnContentPacks
            // 
            this.btnContentPacks.BackColor = System.Drawing.Color.Transparent;
            this.btnContentPacks.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.contentpacks;
            this.btnContentPacks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnContentPacks.FlatAppearance.BorderSize = 0;
            this.btnContentPacks.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnContentPacks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnContentPacks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnContentPacks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContentPacks.Location = new System.Drawing.Point(359, 622);
            this.btnContentPacks.Margin = new System.Windows.Forms.Padding(3, 3, 75, 3);
            this.btnContentPacks.Name = "btnContentPacks";
            this.btnContentPacks.Size = new System.Drawing.Size(90, 41);
            this.btnContentPacks.TabIndex = 24;
            this.btnContentPacks.TabStop = false;
            this.btnContentPacks.UseVisualStyleBackColor = false;
            this.btnContentPacks.Click += new System.EventHandler(this.btnContentPacks_Click);
            this.btnContentPacks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnContentPacks_MouseDown);
            this.btnContentPacks.MouseEnter += new System.EventHandler(this.btnContentPacks_MouseEnter);
            this.btnContentPacks.MouseLeave += new System.EventHandler(this.btnContentPacks_MouseLeave);
            this.btnContentPacks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnContentPacks_MouseUp);
            // 
            // btnTheme
            // 
            this.btnTheme.BackColor = System.Drawing.Color.Transparent;
            this.btnTheme.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.theme;
            this.btnTheme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTheme.FlatAppearance.BorderSize = 0;
            this.btnTheme.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnTheme.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTheme.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTheme.Location = new System.Drawing.Point(534, 622);
            this.btnTheme.Margin = new System.Windows.Forms.Padding(3, 3, 75, 3);
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.Size = new System.Drawing.Size(90, 41);
            this.btnTheme.TabIndex = 25;
            this.btnTheme.TabStop = false;
            this.btnTheme.UseVisualStyleBackColor = false;
            this.btnTheme.Click += new System.EventHandler(this.btnTheme_Click);
            this.btnTheme.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTheme_MouseDown);
            this.btnTheme.MouseEnter += new System.EventHandler(this.btnTheme_MouseEnter);
            this.btnTheme.MouseLeave += new System.EventHandler(this.btnTheme_MouseLeave);
            this.btnTheme.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTheme_MouseUp);
            // 
            // btnOptions
            // 
            this.btnOptions.BackColor = System.Drawing.Color.Transparent;
            this.btnOptions.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.options;
            this.btnOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOptions.FlatAppearance.BorderSize = 0;
            this.btnOptions.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptions.Location = new System.Drawing.Point(710, 622);
            this.btnOptions.Margin = new System.Windows.Forms.Padding(3, 3, 75, 3);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(90, 41);
            this.btnOptions.TabIndex = 26;
            this.btnOptions.TabStop = false;
            this.btnOptions.UseVisualStyleBackColor = false;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            this.btnOptions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnOptions_MouseDown);
            this.btnOptions.MouseEnter += new System.EventHandler(this.btnOptions_MouseEnter);
            this.btnOptions.MouseLeave += new System.EventHandler(this.btnOptions_MouseLeave);
            this.btnOptions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnOptions_MouseUp);
            // 
            // btnCredits
            // 
            this.btnCredits.BackColor = System.Drawing.Color.Transparent;
            this.btnCredits.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.credits;
            this.btnCredits.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCredits.FlatAppearance.BorderSize = 0;
            this.btnCredits.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCredits.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCredits.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCredits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCredits.Location = new System.Drawing.Point(878, 622);
            this.btnCredits.Margin = new System.Windows.Forms.Padding(3, 3, 100, 3);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.Size = new System.Drawing.Size(90, 41);
            this.btnCredits.TabIndex = 27;
            this.btnCredits.TabStop = false;
            this.btnCredits.UseVisualStyleBackColor = false;
            this.btnCredits.Click += new System.EventHandler(this.btnCredits_Click);
            this.btnCredits.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCredits_MouseDown);
            this.btnCredits.MouseEnter += new System.EventHandler(this.btnCredits_MouseEnter);
            this.btnCredits.MouseLeave += new System.EventHandler(this.btnCredits_MouseLeave);
            this.btnCredits.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCredits_MouseUp);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(554, 336);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 13);
            this.lblPassword.TabIndex = 32;
            this.lblPassword.Text = "Password:";
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.Black;
            this.lblUsername.Location = new System.Drawing.Point(554, 310);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(68, 13);
            this.lblUsername.TabIndex = 31;
            this.lblUsername.Text = "Username:";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(622, 333);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(201, 22);
            this.txtPass.TabIndex = 1;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassAndUser_KeyDown);
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(622, 307);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(201, 22);
            this.txtUser.TabIndex = 0;
            this.txtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassAndUser_KeyDown);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.play;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(826, 303);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(90, 41);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPlay_MouseDown);
            this.btnPlay.MouseEnter += new System.EventHandler(this.btnPlay_MouseEnter);
            this.btnPlay.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            this.btnPlay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPlay_MouseUp);
            // 
            // cbSaveLogin
            // 
            this.cbSaveLogin.BackColor = System.Drawing.Color.Transparent;
            this.cbSaveLogin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSaveLogin.Location = new System.Drawing.Point(829, 343);
            this.cbSaveLogin.Name = "cbSaveLogin";
            this.cbSaveLogin.Size = new System.Drawing.Size(87, 17);
            this.cbSaveLogin.TabIndex = 3;
            this.cbSaveLogin.Text = "Save Login";
            this.cbSaveLogin.UseVisualStyleBackColor = false;
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(560, 359);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(340, 24);
            this.pbDownload.TabIndex = 35;
            this.pbDownload.Visible = false;
            // 
            // lblNowDownloading
            // 
            this.lblNowDownloading.BackColor = System.Drawing.Color.Transparent;
            this.lblNowDownloading.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNowDownloading.ForeColor = System.Drawing.Color.Black;
            this.lblNowDownloading.Location = new System.Drawing.Point(566, 280);
            this.lblNowDownloading.Name = "lblNowDownloading";
            this.lblNowDownloading.Size = new System.Drawing.Size(340, 24);
            this.lblNowDownloading.TabIndex = 36;
            this.lblNowDownloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNowDownloading.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(565, 356);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(341, 34);
            this.lblInfo.TabIndex = 37;
            this.lblInfo.Text = "Info is displayed here";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfo.Visible = false;
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.minimize;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Location = new System.Drawing.Point(943, 3);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(3, 3, 75, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(25, 25);
            this.btnMinimize.TabIndex = 38;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            this.btnMinimize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMinimize_MouseDown);
            this.btnMinimize.MouseEnter += new System.EventHandler(this.btnMinimize_MouseEnter);
            this.btnMinimize.MouseLeave += new System.EventHandler(this.btnMinimize_MouseLeave);
            this.btnMinimize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMinimize_MouseUp);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.close;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(971, 3);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 3, 75, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(25, 25);
            this.btnExit.TabIndex = 39;
            this.btnExit.TabStop = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnExit_MouseDown);
            this.btnExit.MouseEnter += new System.EventHandler(this.btnExit_MouseEnter);
            this.btnExit.MouseLeave += new System.EventHandler(this.btnExit_MouseLeave);
            this.btnExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnExit_MouseUp);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.TTC;
            this.ClientSize = new System.Drawing.Size(1000, 666);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.lblNowDownloading);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.cbSaveLogin);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.btnCredits);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnTheme);
            this.Controls.Add(this.btnContentPacks);
            this.Controls.Add(this.btnDiscord);
            this.Controls.Add(this.btnWebsite);
            this.Controls.Add(this.wbNews);
            this.Controls.Add(this.lblInfo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1000, 666);
            this.MinimumSize = new System.Drawing.Size(1000, 666);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Altis - Launcher";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbNews;
        private System.Windows.Forms.Button btnWebsite;
        private System.Windows.Forms.Button btnDiscord;
        private System.Windows.Forms.Button btnContentPacks;
        private System.Windows.Forms.Button btnTheme;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnCredits;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        public System.Windows.Forms.TextBox txtPass;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.CheckBox cbSaveLogin;
        public System.Windows.Forms.ProgressBar pbDownload;
        public System.Windows.Forms.Label lblNowDownloading;
        public System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnExit;
    }
}

