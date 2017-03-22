using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProjectAltisLauncher.Core;

namespace ProjectAltisLauncher.Forms
{
    public partial class FrmBackgroundChoices : Form
    {
        private Form instance;

        public FrmBackgroundChoices(Form form)
        {
            this.Icon = Properties.Resources.pieicon;
            InitializeComponent();
            instance = form;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (radTTC.Checked)
            {
                Properties.Settings.Default.Background = "TTC";
            }
            else if (radDD.Checked)
            {
                Properties.Settings.Default.Background = "DD";
            }
            else if (radDG.Checked)
            {
                Properties.Settings.Default.Background = "DG";
            }
            else if (radMML.Checked)
            {
                Properties.Settings.Default.Background = "MML";
            }
            else if (radBrrrgh.Checked)
            {
                Properties.Settings.Default.Background = "Brrrgh";
            }
            else if (radDDL.Checked)
            {
                Properties.Settings.Default.Background = "DDL";
            }
            Properties.Settings.Default.Save();
            if (!Properties.Settings.Default.RandomBackgrounds)
            {
                instance.BackgroundImage = Graphics.ReturnBackground(Properties.Settings.Default.Background);
            }

            this.Close();
        }

        private void BackgroundChoices_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RandomBackgrounds)
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
                switch (Properties.Settings.Default.Background)
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
