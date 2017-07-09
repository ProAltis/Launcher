namespace ProjectAltis.Forms
{
    partial class FrmContentPacks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContentPacks));
            this.grpContentPacks = new System.Windows.Forms.GroupBox();
            this.lstPacks = new System.Windows.Forms.ListBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnMoveItemUp = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnMoveItemDown = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.grpContentPacks.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpContentPacks
            // 
            this.grpContentPacks.Controls.Add(this.lstPacks);
            this.grpContentPacks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpContentPacks.ForeColor = System.Drawing.Color.White;
            this.grpContentPacks.Location = new System.Drawing.Point(12, 14);
            this.grpContentPacks.Name = "grpContentPacks";
            this.grpContentPacks.Size = new System.Drawing.Size(394, 185);
            this.grpContentPacks.TabIndex = 0;
            this.grpContentPacks.TabStop = false;
            this.grpContentPacks.Text = "Content Packs";
            // 
            // lstPacks
            // 
            this.lstPacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPacks.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPacks.FormattingEnabled = true;
            this.lstPacks.ItemHeight = 17;
            this.lstPacks.Location = new System.Drawing.Point(3, 18);
            this.lstPacks.Name = "lstPacks";
            this.lstPacks.Size = new System.Drawing.Size(388, 164);
            this.lstPacks.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(13, 205);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(119, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnMoveItemUp
            // 
            this.btnMoveItemUp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveItemUp.Location = new System.Drawing.Point(150, 205);
            this.btnMoveItemUp.Name = "btnMoveItemUp";
            this.btnMoveItemUp.Size = new System.Drawing.Size(119, 23);
            this.btnMoveItemUp.TabIndex = 2;
            this.btnMoveItemUp.Text = "Move Item Up";
            this.btnMoveItemUp.UseVisualStyleBackColor = true;
            this.btnMoveItemUp.Click += new System.EventHandler(this.btnMoveItemUp_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(287, 205);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(119, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnMoveItemDown
            // 
            this.btnMoveItemDown.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveItemDown.Location = new System.Drawing.Point(150, 234);
            this.btnMoveItemDown.Name = "btnMoveItemDown";
            this.btnMoveItemDown.Size = new System.Drawing.Size(119, 23);
            this.btnMoveItemDown.TabIndex = 4;
            this.btnMoveItemDown.Text = "Move Item Down";
            this.btnMoveItemDown.UseVisualStyleBackColor = true;
            this.btnMoveItemDown.Click += new System.EventHandler(this.btnMoveItemDown_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(13, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(119, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(287, 234);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(119, 23);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // frmContentPacks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(418, 278);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnMoveItemDown);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnMoveItemUp);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.grpContentPacks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmContentPacks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Content Pack Chooser";
            this.Load += new System.EventHandler(this.frmContentPacks_Load);
            this.grpContentPacks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpContentPacks;
        private System.Windows.Forms.ListBox lstPacks;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnMoveItemUp;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnMoveItemDown;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
    }
}