namespace ProjectAltis.Forms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cbSaveLogin = new System.Windows.Forms.CheckBox();
            this.lblNowDownloading = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnWebsite = new System.Windows.Forms.Button();
            this.btnTheme = new System.Windows.Forms.Button();
            this.btnCredits = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnDiscord = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.btnContentPacks = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(622, 307);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(201, 20);
            this.txtUser.TabIndex = 0;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassAndUser_KeyDown);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(622, 333);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(201, 20);
            this.txtPass.TabIndex = 1;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassAndUser_KeyDown);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.BackgroundImage = global::ProjectAltis.Properties.Resources.play;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(826, 302);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(90, 41);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnPlay.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnPlay.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnPlay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(565, 356);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(341, 34);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "Info is displayed here";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfo.Visible = false;
            // 
            // cbSaveLogin
            // 
            this.cbSaveLogin.BackColor = System.Drawing.Color.Transparent;
            this.cbSaveLogin.Location = new System.Drawing.Point(829, 343);
            this.cbSaveLogin.Name = "cbSaveLogin";
            this.cbSaveLogin.Size = new System.Drawing.Size(90, 17);
            this.cbSaveLogin.TabIndex = 5;
            this.cbSaveLogin.Text = "Save Login";
            this.cbSaveLogin.UseVisualStyleBackColor = false;
            // 
            // lblNowDownloading
            // 
            this.lblNowDownloading.BackColor = System.Drawing.Color.Transparent;
            this.lblNowDownloading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblNowDownloading.ForeColor = System.Drawing.Color.Black;
            this.lblNowDownloading.Location = new System.Drawing.Point(566, 280);
            this.lblNowDownloading.Name = "lblNowDownloading";
            this.lblNowDownloading.Size = new System.Drawing.Size(340, 24);
            this.lblNowDownloading.TabIndex = 6;
            this.lblNowDownloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNowDownloading.Visible = false;
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.ForeColor = System.Drawing.Color.Black;
            this.lblUsername.Location = new System.Drawing.Point(557, 310);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 7;
            this.lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(557, 336);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            // 
            // btnWebsite
            // 
            this.btnWebsite.BackColor = System.Drawing.Color.Transparent;
            this.btnWebsite.BackgroundImage = global::ProjectAltis.Properties.Resources.website;
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
            this.btnWebsite.TabIndex = 9;
            this.btnWebsite.TabStop = false;
            this.btnWebsite.UseVisualStyleBackColor = false;
            this.btnWebsite.Click += new System.EventHandler(this.btnOfficialSite_Click);
            this.btnWebsite.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnWebsite.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnWebsite.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnWebsite.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // btnTheme
            // 
            this.btnTheme.BackColor = System.Drawing.Color.Transparent;
            this.btnTheme.BackgroundImage = global::ProjectAltis.Properties.Resources.theme;
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
            this.btnTheme.TabIndex = 12;
            this.btnTheme.TabStop = false;
            this.btnTheme.UseVisualStyleBackColor = false;
            this.btnTheme.Click += new System.EventHandler(this.btnChangeBg_Click);
            this.btnTheme.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnTheme.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnTheme.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnTheme.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // btnCredits
            // 
            this.btnCredits.BackColor = System.Drawing.Color.Transparent;
            this.btnCredits.BackgroundImage = global::ProjectAltis.Properties.Resources.credits;
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
            this.btnCredits.TabIndex = 13;
            this.btnCredits.TabStop = false;
            this.btnCredits.UseVisualStyleBackColor = false;
            this.btnCredits.Click += new System.EventHandler(this.btnCredits_Click);
            this.btnCredits.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnCredits.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnCredits.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnCredits.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // btnOptions
            // 
            this.btnOptions.BackColor = System.Drawing.Color.Transparent;
            this.btnOptions.BackgroundImage = global::ProjectAltis.Properties.Resources.options;
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
            this.btnOptions.TabIndex = 15;
            this.btnOptions.TabStop = false;
            this.btnOptions.UseVisualStyleBackColor = false;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            this.btnOptions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnOptions.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnOptions.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnOptions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // btnDiscord
            // 
            this.btnDiscord.BackColor = System.Drawing.Color.Transparent;
            this.btnDiscord.BackgroundImage = global::ProjectAltis.Properties.Resources.discord;
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
            this.btnDiscord.TabIndex = 16;
            this.btnDiscord.TabStop = false;
            this.btnDiscord.UseVisualStyleBackColor = false;
            this.btnDiscord.Click += new System.EventHandler(this.btnDiscord_Click);
            this.btnDiscord.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnDiscord.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnDiscord.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.btnDiscord.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::ProjectAltis.Properties.Resources.cancel;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Transparent;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(974, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(24, 24);
            this.btnExit.TabIndex = 18;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnExit.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::ProjectAltis.Properties.Resources.minus;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.ForeColor = System.Drawing.Color.Transparent;
            this.btnMin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMin.Location = new System.Drawing.Point(944, 1);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(24, 24);
            this.btnMin.TabIndex = 19;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            this.btnMin.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnMin.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(38, 76);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(435, 525);
            this.webBrowser1.TabIndex = 20;
            this.webBrowser1.TabStop = false;
            this.webBrowser1.Url = new System.Uri("https://projectaltis.com/launcher", System.UriKind.Absolute);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(566, 359);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(340, 24);
            this.pbDownload.TabIndex = 21;
            this.pbDownload.Visible = false;
            // 
            // btnContentPacks
            // 
            this.btnContentPacks.BackColor = System.Drawing.Color.Transparent;
            this.btnContentPacks.BackgroundImage = global::ProjectAltis.Properties.Resources.contentpacks;
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
            this.btnContentPacks.TabIndex = 23;
            this.btnContentPacks.TabStop = false;
            this.btnContentPacks.UseVisualStyleBackColor = false;
            this.btnContentPacks.Click += new System.EventHandler(this.btnContentPacks_Click);
            this.btnContentPacks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.btnContentPacks.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.btnContentPacks.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::ProjectAltis.Properties.Resources.TTC;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1000, 666);
            this.Controls.Add(this.btnContentPacks);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDiscord);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnCredits);
            this.Controls.Add(this.btnTheme);
            this.Controls.Add(this.btnWebsite);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblNowDownloading);
            this.Controls.Add(this.cbSaveLogin);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 666);
            this.MinimumSize = new System.Drawing.Size(1000, 666);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Altis - Launcher";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.CheckBox cbSaveLogin;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnWebsite;
        private System.Windows.Forms.Button btnTheme;
        private System.Windows.Forms.Button btnCredits;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnDiscord;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.Button btnContentPacks;
        public System.Windows.Forms.Label lblNowDownloading;
    }
}

