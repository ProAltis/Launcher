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
using System.IO;
using System.Windows.Forms;
using ProjectAltisLauncher.Properties;

namespace ProjectAltisLauncher.Forms
{
    public partial class FrmOptions : Form
    {
        private readonly Form instance;

        public FrmOptions(Form instance)
        {
            this.Icon = Resources.pieicon;
            InitializeComponent();
            this.instance = instance;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Settings.Default.WantsToontownCursor = this.chkCursor.Checked;
            Settings.Default.WantsDebugWindow = this.chkDebugWindow.Checked;
            Settings.Default.WantsClickSounds = this.chkClickSounds.Checked;
            Settings.Default.RandomBackgrounds = this.chkRandomBackgrounds.Checked;
            Settings.Default.Save();

            // Apply the cursor if checked.

            if (Settings.Default.WantsToontownCursor)
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Resources.toonmono);
                this.instance.Cursor = new Cursor(cursorMemoryStream);
            }
            else
            {
                this.instance.Cursor = Cursors.Default;
            }
            Close();
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            this.chkCursor.Checked = Settings.Default.WantsToontownCursor;
            this.chkDebugWindow.Checked = Settings.Default.WantsDebugWindow;
            this.chkClickSounds.Checked = Settings.Default.WantsClickSounds;
            this.chkRandomBackgrounds.Checked = Settings.Default.RandomBackgrounds;
        }
    }
}