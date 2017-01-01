namespace ProjectAltisLauncher
{
    partial class Form1
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
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.Updater = new System.ComponentModel.BackgroundWorker();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cbSaveLogin = new System.Windows.Forms.CheckBox();
            this.lblNowDownloading = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnOfficialSite = new System.Windows.Forms.Button();
            this.btnGroupTracker = new System.Windows.Forms.Button();
            this.btnChangeBg = new System.Windows.Forms.Button();
            this.btnCredits = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnDiscord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(622, 307);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(201, 20);
            this.txtUser.TabIndex = 0;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(622, 333);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(201, 20);
            this.txtPass.TabIndex = 1;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(958, -1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(829, 306);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(90, 33);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Updater
            // 
            this.Updater.WorkerReportsProgress = true;
            this.Updater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Updater_DoWork);
            this.Updater.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Updater_ProgressChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Location = new System.Drawing.Point(565, 399);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(341, 54);
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
            this.lblNowDownloading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNowDownloading.ForeColor = System.Drawing.Color.Black;
            this.lblNowDownloading.Location = new System.Drawing.Point(566, 245);
            this.lblNowDownloading.Name = "lblNowDownloading";
            this.lblNowDownloading.Size = new System.Drawing.Size(340, 26);
            this.lblNowDownloading.TabIndex = 6;
            this.lblNowDownloading.Text = "Download info here";
            this.lblNowDownloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNowDownloading.Visible = false;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
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
            // btnOfficialSite
            // 
            this.btnOfficialSite.Location = new System.Drawing.Point(12, 616);
            this.btnOfficialSite.Margin = new System.Windows.Forms.Padding(0, 0, 75, 0);
            this.btnOfficialSite.Name = "btnOfficialSite";
            this.btnOfficialSite.Size = new System.Drawing.Size(95, 35);
            this.btnOfficialSite.TabIndex = 9;
            this.btnOfficialSite.Text = "Official Site";
            this.btnOfficialSite.UseVisualStyleBackColor = true;
            this.btnOfficialSite.Click += new System.EventHandler(this.btnOfficialSite_Click);
            // 
            // btnGroupTracker
            // 
            this.btnGroupTracker.Location = new System.Drawing.Point(352, 616);
            this.btnGroupTracker.Margin = new System.Windows.Forms.Padding(0, 0, 75, 0);
            this.btnGroupTracker.Name = "btnGroupTracker";
            this.btnGroupTracker.Size = new System.Drawing.Size(95, 35);
            this.btnGroupTracker.TabIndex = 11;
            this.btnGroupTracker.Text = "Group Tracker";
            this.btnGroupTracker.UseVisualStyleBackColor = true;
            this.btnGroupTracker.Click += new System.EventHandler(this.btnGroupTracker_Click);
            // 
            // btnChangeBg
            // 
            this.btnChangeBg.Location = new System.Drawing.Point(525, 616);
            this.btnChangeBg.Margin = new System.Windows.Forms.Padding(3, 3, 75, 3);
            this.btnChangeBg.Name = "btnChangeBg";
            this.btnChangeBg.Size = new System.Drawing.Size(95, 35);
            this.btnChangeBg.TabIndex = 12;
            this.btnChangeBg.Text = "Change Background";
            this.btnChangeBg.UseVisualStyleBackColor = true;
            this.btnChangeBg.Click += new System.EventHandler(this.btnChangeBg_Click);
            // 
            // btnCredits
            // 
            this.btnCredits.Location = new System.Drawing.Point(871, 617);
            this.btnCredits.Margin = new System.Windows.Forms.Padding(3, 3, 100, 3);
            this.btnCredits.Name = "btnCredits";
            this.btnCredits.Size = new System.Drawing.Size(95, 35);
            this.btnCredits.TabIndex = 13;
            this.btnCredits.Text = "Credits";
            this.btnCredits.UseVisualStyleBackColor = true;
            this.btnCredits.Click += new System.EventHandler(this.btnCredits_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(698, 616);
            this.btnOptions.Margin = new System.Windows.Forms.Padding(3, 3, 75, 3);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(95, 36);
            this.btnOptions.TabIndex = 15;
            this.btnOptions.Text = "Game Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnDiscord
            // 
            this.btnDiscord.Location = new System.Drawing.Point(182, 616);
            this.btnDiscord.Margin = new System.Windows.Forms.Padding(0, 0, 75, 0);
            this.btnDiscord.Name = "btnDiscord";
            this.btnDiscord.Size = new System.Drawing.Size(95, 35);
            this.btnDiscord.TabIndex = 16;
            this.btnDiscord.Text = "Discord";
            this.btnDiscord.UseVisualStyleBackColor = true;
            this.btnDiscord.Click += new System.EventHandler(this.btnDiscord_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.BackgroundImage = global::ProjectAltisLauncher.Properties.Resources.TTCLauncher;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.btnDiscord);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnCredits);
            this.Controls.Add(this.btnChangeBg);
            this.Controls.Add(this.btnGroupTracker);
            this.Controls.Add(this.btnOfficialSite);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblNowDownloading);
            this.Controls.Add(this.cbSaveLogin);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1000, 650);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "Form1";
            this.Text = "Project Altis Launcher";
            this.TransparencyKey = System.Drawing.Color.SandyBrown;
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
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPlay;
        private System.ComponentModel.BackgroundWorker Updater;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.CheckBox cbSaveLogin;
        private System.Windows.Forms.Label lblNowDownloading;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnOfficialSite;
        private System.Windows.Forms.Button btnGroupTracker;
        private System.Windows.Forms.Button btnChangeBg;
        private System.Windows.Forms.Button btnCredits;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnDiscord;
    }
}

