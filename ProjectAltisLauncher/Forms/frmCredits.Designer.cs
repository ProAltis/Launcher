namespace ProjectAltis.Forms
{
    partial class FrmCredits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCredits));
            this.rtfCredits = new System.Windows.Forms.RichTextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtfCredits
            // 
            this.rtfCredits.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rtfCredits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfCredits.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtfCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfCredits.ForeColor = System.Drawing.Color.White;
            this.rtfCredits.Location = new System.Drawing.Point(12, 12);
            this.rtfCredits.Name = "rtfCredits";
            this.rtfCredits.ReadOnly = true;
            this.rtfCredits.Size = new System.Drawing.Size(388, 174);
            this.rtfCredits.TabIndex = 1;
            this.rtfCredits.Text = "";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightGray;
            this.btnExit.Location = new System.Drawing.Point(169, 163);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmCredits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(410, 197);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.rtfCredits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCredits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Credits";
            this.Load += new System.EventHandler(this.Credits_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtfCredits;
        private System.Windows.Forms.Button btnExit;
    }
}