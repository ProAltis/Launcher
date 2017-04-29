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
using System.Windows.Forms;
using ProjectAltisLauncher.Core;
using ProjectAltisLauncher.Properties;

namespace ProjectAltisLauncher.Forms
{
    public partial class FrmBackgroundChoices : Form
    {
        private readonly Form instance;

        public FrmBackgroundChoices(Form form)
        {
            this.Icon = Resources.pieicon;
            InitializeComponent();
            this.instance = form;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.radTTC.Checked)
                Settings.Default.Background = "TTC";
            else if (this.radDD.Checked)
                Settings.Default.Background = "DD";
            else if (this.radDG.Checked)
                Settings.Default.Background = "DG";
            else if (this.radMML.Checked)
                Settings.Default.Background = "MML";
            else if (this.radBrrrgh.Checked)
                Settings.Default.Background = "Brrrgh";
            else if (this.radDDL.Checked)
                Settings.Default.Background = "DDL";
            Settings.Default.Save();
            if (!Settings.Default.RandomBackgrounds)
                this.instance.BackgroundImage = Graphics.ReturnBackground(Settings.Default.Background);

            Close();
        }

        private void BackgroundChoices_Load(object sender, EventArgs e)
        {
            if (Settings.Default.RandomBackgrounds)
            {
                this.radBrrrgh.Visible = false;
                this.radDD.Visible = false;
                this.radTTC.Visible = false;
                this.radDG.Visible = false;
                this.radDDL.Visible = false;
                this.radMML.Visible = false;
                this.lblRandBG.Visible = true;
            }
            else
            {
                switch (Settings.Default.Background)
                {
                    case "TTC":
                        this.radTTC.Checked = true;
                        break;
                    case "DD":
                        this.radDD.Checked = true;
                        break;
                    case "DG":
                        this.radDG.Checked = true;
                        break;
                    case "MML":
                        this.radMML.Checked = true;
                        break;
                    case "Brrrgh":
                        this.radBrrrgh.Checked = true;
                        break;
                    case "DDL":
                        this.radDDL.Checked = true;
                        break;
                }
            }
        }
    }
}