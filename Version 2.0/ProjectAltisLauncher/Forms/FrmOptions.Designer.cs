namespace ProjectAltisLauncher.Forms
{
    partial class FrmOptions
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkRandomBackgrounds = new System.Windows.Forms.CheckBox();
            this.chkClickSounds = new System.Windows.Forms.CheckBox();
            this.chkDebugWindow = new System.Windows.Forms.CheckBox();
            this.chkCursor = new System.Windows.Forms.CheckBox();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(18, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.LightGray;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(231, 180);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkRandomBackgrounds);
            this.grpOptions.Controls.Add(this.chkClickSounds);
            this.grpOptions.Controls.Add(this.chkDebugWindow);
            this.grpOptions.Controls.Add(this.chkCursor);
            this.grpOptions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOptions.ForeColor = System.Drawing.Color.White;
            this.grpOptions.Location = new System.Drawing.Point(13, 12);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(303, 161);
            this.grpOptions.TabIndex = 6;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // chkRandomBackgrounds
            // 
            this.chkRandomBackgrounds.AutoSize = true;
            this.chkRandomBackgrounds.Location = new System.Drawing.Point(7, 91);
            this.chkRandomBackgrounds.Name = "chkRandomBackgrounds";
            this.chkRandomBackgrounds.Size = new System.Drawing.Size(175, 17);
            this.chkRandomBackgrounds.TabIndex = 3;
            this.chkRandomBackgrounds.Text = "Enable random backgrounds";
            this.chkRandomBackgrounds.UseVisualStyleBackColor = true;
            // 
            // chkClickSounds
            // 
            this.chkClickSounds.AutoSize = true;
            this.chkClickSounds.Location = new System.Drawing.Point(7, 68);
            this.chkClickSounds.Name = "chkClickSounds";
            this.chkClickSounds.Size = new System.Drawing.Size(127, 17);
            this.chkClickSounds.TabIndex = 2;
            this.chkClickSounds.Text = "Enable click sounds";
            this.chkClickSounds.UseVisualStyleBackColor = true;
            // 
            // chkDebugWindow
            // 
            this.chkDebugWindow.AutoSize = true;
            this.chkDebugWindow.Location = new System.Drawing.Point(7, 45);
            this.chkDebugWindow.Name = "chkDebugWindow";
            this.chkDebugWindow.Size = new System.Drawing.Size(202, 17);
            this.chkDebugWindow.TabIndex = 1;
            this.chkDebugWindow.Text = "Launch game with debug window";
            this.chkDebugWindow.UseVisualStyleBackColor = true;
            // 
            // chkCursor
            // 
            this.chkCursor.AutoSize = true;
            this.chkCursor.Location = new System.Drawing.Point(7, 22);
            this.chkCursor.Name = "chkCursor";
            this.chkCursor.Size = new System.Drawing.Size(153, 17);
            this.chkCursor.TabIndex = 0;
            this.chkCursor.Text = "Enable Toontown Cursor";
            this.chkCursor.UseVisualStyleBackColor = true;
            // 
            // FrmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(328, 214);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkCursor;
        private System.Windows.Forms.CheckBox chkDebugWindow;
        private System.Windows.Forms.CheckBox chkClickSounds;
        private System.Windows.Forms.CheckBox chkRandomBackgrounds;
    }
}