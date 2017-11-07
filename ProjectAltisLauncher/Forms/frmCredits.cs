using System;
using System.Windows.Forms;

namespace ProjectAltis.Forms
{
    public partial class FrmCredits : Form
    {
        public FrmCredits()
        {
            InitializeComponent();
        }

        private void Credits_Load(object sender, EventArgs e)
        {
            Log.TryOpenUrl("https://github.com/ProAltis/Launcher/graphs/contributors");
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
