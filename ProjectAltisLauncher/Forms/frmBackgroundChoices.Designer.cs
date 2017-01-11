namespace ProjectAltisLauncher.Forms
{
    partial class frmBackgroundChoices
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.radDD = new System.Windows.Forms.RadioButton();
            this.radTTC = new System.Windows.Forms.RadioButton();
            this.radDDL = new System.Windows.Forms.RadioButton();
            this.radBrrrgh = new System.Windows.Forms.RadioButton();
            this.radDG = new System.Windows.Forms.RadioButton();
            this.radMML = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblRandBG = new System.Windows.Forms.Label();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.radDD);
            this.grpMain.Controls.Add(this.radTTC);
            this.grpMain.Controls.Add(this.radDDL);
            this.grpMain.Controls.Add(this.radBrrrgh);
            this.grpMain.Controls.Add(this.radDG);
            this.grpMain.Controls.Add(this.radMML);
            this.grpMain.Controls.Add(this.lblRandBG);
            this.grpMain.ForeColor = System.Drawing.Color.White;
            this.grpMain.Location = new System.Drawing.Point(12, 12);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(304, 156);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Choices";
            // 
            // radDD
            // 
            this.radDD.AutoSize = true;
            this.radDD.Location = new System.Drawing.Point(6, 41);
            this.radDD.Name = "radDD";
            this.radDD.Size = new System.Drawing.Size(95, 17);
            this.radDD.TabIndex = 5;
            this.radDD.TabStop = true;
            this.radDD.Text = "Donald\'s Dock";
            this.radDD.UseVisualStyleBackColor = true;
            // 
            // radTTC
            // 
            this.radTTC.AutoSize = true;
            this.radTTC.Location = new System.Drawing.Point(6, 18);
            this.radTTC.Name = "radTTC";
            this.radTTC.Size = new System.Drawing.Size(109, 17);
            this.radTTC.TabIndex = 4;
            this.radTTC.TabStop = true;
            this.radTTC.Text = "Toontown Central";
            this.radTTC.UseVisualStyleBackColor = true;
            // 
            // radDDL
            // 
            this.radDDL.AutoSize = true;
            this.radDDL.Location = new System.Drawing.Point(6, 133);
            this.radDDL.Name = "radDDL";
            this.radDDL.Size = new System.Drawing.Size(120, 17);
            this.radDDL.TabIndex = 3;
            this.radDDL.TabStop = true;
            this.radDDL.Text = "Donald\'s Dreamland";
            this.radDDL.UseVisualStyleBackColor = true;
            // 
            // radBrrrgh
            // 
            this.radBrrrgh.AutoSize = true;
            this.radBrrrgh.Location = new System.Drawing.Point(6, 110);
            this.radBrrrgh.Name = "radBrrrgh";
            this.radBrrrgh.Size = new System.Drawing.Size(75, 17);
            this.radBrrrgh.TabIndex = 2;
            this.radBrrrgh.TabStop = true;
            this.radBrrrgh.Text = "The Brrrgh";
            this.radBrrrgh.UseVisualStyleBackColor = true;
            // 
            // radDG
            // 
            this.radDG.AutoSize = true;
            this.radDG.Location = new System.Drawing.Point(6, 64);
            this.radDG.Name = "radDG";
            this.radDG.Size = new System.Drawing.Size(101, 17);
            this.radDG.TabIndex = 1;
            this.radDG.TabStop = true;
            this.radDG.Text = "Daisy\'s Gardens";
            this.radDG.UseVisualStyleBackColor = true;
            // 
            // radMML
            // 
            this.radMML.AutoSize = true;
            this.radMML.Location = new System.Drawing.Point(6, 87);
            this.radMML.Name = "radMML";
            this.radMML.Size = new System.Drawing.Size(125, 17);
            this.radMML.TabIndex = 0;
            this.radMML.TabStop = true;
            this.radMML.Text = "Minnies Melody Land";
            this.radMML.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(12, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.LightGray;
            this.btnConfirm.Location = new System.Drawing.Point(221, 179);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(95, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblRandBG
            // 
            this.lblRandBG.AutoSize = true;
            this.lblRandBG.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblRandBG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblRandBG.Location = new System.Drawing.Point(26, 84);
            this.lblRandBG.Name = "lblRandBG";
            this.lblRandBG.Size = new System.Drawing.Size(258, 20);
            this.lblRandBG.TabIndex = 6;
            this.lblRandBG.Text = "Random backgrounds are enabled!";
            this.lblRandBG.Visible = false;
            // 
            // BackgroundChoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(328, 214);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BackgroundChoices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Background Choices";
            this.Load += new System.EventHandler(this.BackgroundChoices_Load);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.RadioButton radDD;
        private System.Windows.Forms.RadioButton radTTC;
        private System.Windows.Forms.RadioButton radDDL;
        private System.Windows.Forms.RadioButton radBrrrgh;
        private System.Windows.Forms.RadioButton radDG;
        private System.Windows.Forms.RadioButton radMML;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblRandBG;
    }
}