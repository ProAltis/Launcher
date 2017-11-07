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
            Settings.Instance.WantCursor = chkCursor.Checked;
            Settings.Instance.WantClickSound = chkClickSounds.Checked;
            Settings.Instance.WantRandomBackgrounds = chkRandomBackgrounds.Checked;
            Settings.Instance.WantSavePassword = chkSavePassword.Checked;
            this.Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            chkCursor.Checked = Settings.Instance.WantCursor;
            chkClickSounds.Checked = Settings.Instance.WantClickSound;
            chkRandomBackgrounds.Checked = Settings.Instance.WantRandomBackgrounds;
            chkSavePassword.Checked = Settings.Instance.WantSavePassword;
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
