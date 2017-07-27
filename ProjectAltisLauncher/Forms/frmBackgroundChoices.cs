using System;
using System.Windows.Forms;
using ProjectAltis.Properties;

namespace ProjectAltis.Forms
{
    public partial class FrmBackgroundChoices : Form
    {
        public FrmBackgroundChoices()
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
                Settings.Default.Background = "TTC";
            }
            else if (radDD.Checked)
            {
                Settings.Default.Background = "DD";
            }
            else if (radDG.Checked)
            {
                Settings.Default.Background = "DG";
            }
            else if (radMML.Checked)
            {
                Settings.Default.Background = "MML";
            }
            else if (radBrrrgh.Checked)
            {
                Settings.Default.Background = "Brrrgh";
            }
            else if(radDDL.Checked)
            {
                Settings.Default.Background = "DDL";
            }
            Settings.Default.Save();
            Close();
        }

        private void BackgroundChoices_Load(object sender, EventArgs e)
        {
            if (Settings.Default.WantsRandomBg)
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
                switch (Settings.Default.Background)
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
