using System;
using System.IO;
using System.Windows.Forms;
using ProjectAltis.Core;

namespace ProjectAltis.Forms
{
	public partial class FrmOptions : Form
    {
        public FrmOptions()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WantsCursor = chkCursor.Checked;
            Properties.Settings.Default.WantsClickSounds = chkClickSounds.Checked;
            Properties.Settings.Default.WantsRandomBg = chkRandomBackgrounds.Checked;
            Properties.Settings.Default.WantsPassword = chkSavePassword.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            chkCursor.Checked = Properties.Settings.Default.WantsCursor;
            chkClickSounds.Checked = Properties.Settings.Default.WantsClickSounds;
            chkRandomBackgrounds.Checked = Properties.Settings.Default.WantsRandomBg;
            chkSavePassword.Checked = Properties.Settings.Default.WantsPassword;
            if(!UwpHelper.IsWindows10())
            {
                chkSavePassword.Enabled = false;
                chkSavePassword.Checked = false;
            }
        }

		private void button1_Click(object sender, EventArgs e)
		{
			Log.TryOpenUrl(Path.Combine(Directory.GetCurrentDirectory(), "Logs"));
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
