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
                Settings.Instance.Background = "TTC";
            }
            else if (radDD.Checked)
            {
                Settings.Instance.Background = "DD";
            }
            else if (radDG.Checked)
            {
                Settings.Instance.Background = "DG";
            }
            else if (radMML.Checked)
            {
                Settings.Instance.Background = "MML";
            }
            else if (radBrrrgh.Checked)
            {
                Settings.Instance.Background = "Brrrgh";
            }
            else if(radDDL.Checked)
            {
                Settings.Instance.Background = "DDL";
            }
            Close();
        }

        private void BackgroundChoices_Load(object sender, EventArgs e)
        {
            if (Settings.Instance.WantRandomBackgrounds)
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
                switch (Settings.Instance.Background)
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
