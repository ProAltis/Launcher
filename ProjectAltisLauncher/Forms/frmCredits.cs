using System;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Forms
{
    public partial class frmCredits : Form
    {
        public frmCredits()
        {
            InitializeComponent();
        }

        private void Credits_Load(object sender, EventArgs e)
        {
            string string1 = @"{\rtf1\ansi \b Developers\b0\par     • Lead Programmer & Designer: Ben\par     • Programmer: Roast Duck}";

            rtfCredits.Rtf = string1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
