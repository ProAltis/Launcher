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
    public partial class frmBackgroundChoices : Form
    {
        public frmBackgroundChoices()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (radTTC.Checked == true)
            {
                Properties.Settings.Default.background = "TTC";
            }
            else if (radDD.Checked == true)
            {
                Properties.Settings.Default.background = "DD";
            }
            else if (radDG.Checked == true)
            {
                Properties.Settings.Default.background = "DG";
            }
            else if (radMML.Checked == true)
            {
                Properties.Settings.Default.background = "MML";
            }
            else if (radBrrrgh.Checked == true)
            {
                Properties.Settings.Default.background = "Brrrgh";
            }
            else if(radDDL.Checked == true)
            {
                Properties.Settings.Default.background = "DDL";
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void BackgroundChoices_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.wantsRandomBg)
            {
                radBrrrgh.Visible = false;
                radDD.Visible = false;
                radTTC.Visible = false;
                radDG.Visible = false;
                radDDL.Visible = false;
                radMML.Visible = false;
                lblRandBG.Visible = true;
            }
            else
            {
                switch (Properties.Settings.Default.background)
                {
                    case "TTC":
                        radTTC.Checked = true;
                        break;
                    case "DD":
                        radDD.Checked = true;
                        break;
                    case "DG":
                        radDG.Checked = true;
                        break;
                    case "MML":
                        radMML.Checked = true;
                        break;
                    case "Brrrgh":
                        radBrrrgh.Checked = true;
                        break;
                    case "DDL":
                        radDDL.Checked = true;
                        break;
                }
            }

        }
    }
}
