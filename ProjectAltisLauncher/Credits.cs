using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAltisLauncher
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
        }

        private void Credits_Load(object sender, EventArgs e)
        {
            string string1 = @"{\rtf1\ansi \b Developers\b0\par     • Lead Programmer & Designer: Ben\par     • Programmer: Roast Duck}";

            rtfCredits.Rtf = string1;
        }
    }
}
