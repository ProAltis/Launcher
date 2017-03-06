using System;
using System.Windows.Forms;
using ProjectAltisLauncher.Properties;

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
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (radTTC.Checked)
            {
                Settings.Default.background = "TTC";
            }
            else if (radDD.Checked)
            {
                Settings.Default.background = "DD";
            }
            else if (radDG.Checked)
            {
                Settings.Default.background = "DG";
            }
            else if (radMML.Checked)
            {
                Settings.Default.background = "MML";
            }
            else if (radBrrrgh.Checked)
            {
                Settings.Default.background = "Brrrgh";
            }
            else if(radDDL.Checked)
            {
                Settings.Default.background = "DDL";
            }
            Settings.Default.Save();
            Close();
        }

        private void BackgroundChoices_Load(object sender, EventArgs e)
        {
            if (Settings.Default.wantsRandomBg)
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
                switch (Settings.Default.background)
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
