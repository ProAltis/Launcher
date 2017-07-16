namespace ProjectAltis.Forms.ContentPacks
{
    partial class FrmContentChooser
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
            this.lblChoose = new System.Windows.Forms.Label();
            this.btnAddPhaseFile = new System.Windows.Forms.Button();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblChoose
            // 
            this.lblChoose.AutoSize = true;
            this.lblChoose.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoose.ForeColor = System.Drawing.Color.White;
            this.lblChoose.Location = new System.Drawing.Point(67, 9);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(248, 13);
            this.lblChoose.TabIndex = 0;
            this.lblChoose.Text = "How would you like to choose a content pack?";
            // 
            // btnAddPhaseFile
            // 
            this.btnAddPhaseFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddPhaseFile.Location = new System.Drawing.Point(12, 73);
            this.btnAddPhaseFile.Name = "btnAddPhaseFile";
            this.btnAddPhaseFile.Size = new System.Drawing.Size(174, 23);
            this.btnAddPhaseFile.TabIndex = 1;
            this.btnAddPhaseFile.Text = "Add Multifile (*.mf)";
            this.btnAddPhaseFile.UseVisualStyleBackColor = true;
            this.btnAddPhaseFile.Click += new System.EventHandler(this.BtnAddPhaseFile_Click);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddFolder.Location = new System.Drawing.Point(203, 73);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(173, 23);
            this.btnAddFolder.TabIndex = 2;
            this.btnAddFolder.Text = "Add Folder";
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.BtnAddFolder_Click);
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Location = new System.Drawing.Point(109, 129);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(173, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // FrmContentChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(388, 164);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddFolder);
            this.Controls.Add(this.btnAddPhaseFile);
            this.Controls.Add(this.lblChoose);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmContentChooser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Content Pack Chooser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChoose;
        private System.Windows.Forms.Button btnAddPhaseFile;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnClose;
    }
}