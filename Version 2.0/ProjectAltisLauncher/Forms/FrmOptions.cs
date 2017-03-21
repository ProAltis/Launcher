using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Forms
{
    public partial class FrmOptions : Form
    {
        private Form instance;

        public FrmOptions(Form instance)
        {
            this.Icon = Properties.Resources.pieicon;
            InitializeComponent();
            this.instance = instance;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WantsToontownCursor = chkCursor.Checked;
            Properties.Settings.Default.WantsDebugWindow = chkDebugWindow.Checked;
            Properties.Settings.Default.WantsClickSounds = chkClickSounds.Checked;
            Properties.Settings.Default.RandomBackgrounds = chkRandomBackgrounds.Checked;
            Properties.Settings.Default.Save();

            // Apply the cursor if checked.

            if (Properties.Settings.Default.WantsToontownCursor)
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Properties.Resources.toonmono);
                instance.Cursor = new Cursor(cursorMemoryStream);
            }
            else
            {
                instance.Cursor = Cursors.Default;
            }
            this.Close();
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            chkCursor.Checked = Properties.Settings.Default.WantsToontownCursor;
            chkDebugWindow.Checked = Properties.Settings.Default.WantsDebugWindow;
            chkClickSounds.Checked = Properties.Settings.Default.WantsClickSounds;
            chkRandomBackgrounds.Checked = Properties.Settings.Default.RandomBackgrounds;
        }
    }
}
