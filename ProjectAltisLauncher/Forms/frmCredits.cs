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
            const string string1 = @"{\rtf1\ansi \b Credits\b0\par"+ 
                                   @"     • Lead Programmer & Designer: Ben\par" +
                                   @"     • Designer: Ask Alice\par" + 
                                   @"     • Programmer: Dubito\par" + 
                                   @"     • Programmer: Judge2020}";

            rtfCredits.Rtf = string1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
