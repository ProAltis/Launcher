using System;
using System.Windows.Forms;

namespace ProjectAltis.Forms
{
    public partial class frmReleaseNotes : Form
    {
        public frmReleaseNotes()
        {
            InitializeComponent();
            webBrowser1.Url = new Uri("https://proaltis.github.io/Launcher-Releases/");
        }

        public static bool ShouldShowReleaseNotes()
        {
            return string.IsNullOrEmpty(Properties.Settings.Default.ReleaseNotesVersion) ||
                   Properties.Settings.Default.ReleaseNotesVersion != typeof(Program).Assembly.GetName().Version.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
