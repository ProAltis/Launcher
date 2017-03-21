using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Forms
{
    public partial class FrmCredits : Form
    {
        public FrmCredits()
        {
            this.Icon = Properties.Resources.pieicon;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCredits_Load(object sender, EventArgs e)
        {
            string cred = @"{\rtf1\ansi \b Credits\b0\par" +
                                   @"     • Lead Programmer & Designer: Ben\par" +
                                   @"     • Designer: Ask Alice\par" +
                                   @"     • Designer: Void Poro\par" +
                                   @"     • Programmer: Dubito\par" +
                                   @"     • Programmer: Judge2020}";

            rtfCredits.Rtf = cred;
        }
    }
}
