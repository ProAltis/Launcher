namespace ProjectAltisLauncher
{
    partial class Credits
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
            this.rtfCredits = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtfCredits
            // 
            this.rtfCredits.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rtfCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfCredits.ForeColor = System.Drawing.Color.White;
            this.rtfCredits.Location = new System.Drawing.Point(12, 12);
            this.rtfCredits.Name = "rtfCredits";
            this.rtfCredits.ReadOnly = true;
            this.rtfCredits.Size = new System.Drawing.Size(388, 174);
            this.rtfCredits.TabIndex = 1;
            this.rtfCredits.Text = "";
            // 
            // Credits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(412, 198);
            this.Controls.Add(this.rtfCredits);
            this.MaximumSize = new System.Drawing.Size(428, 237);
            this.MinimumSize = new System.Drawing.Size(428, 237);
            this.Name = "Credits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Credits";
            this.Load += new System.EventHandler(this.Credits_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtfCredits;
    }
}