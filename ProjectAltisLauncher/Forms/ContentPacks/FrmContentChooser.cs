using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAltis.Forms.ContentPacks
{
    public partial class FrmContentChooser : Form
    {
        private readonly FrmContentPacks _parentInstance;

        /// <summary>
        /// The current directory
        /// </summary>
        private readonly string _currentDirectory = Directory.GetCurrentDirectory() + @"\";

        public FrmContentChooser(FrmContentPacks parentInstance)
        {
            InitializeComponent();
            Icon = Properties.Resources.favicon;
            _parentInstance = parentInstance;
        }

        private void BtnAddFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            int totalFilesAdded = 0;
            try
            {
                if (result == DialogResult.OK)
                {
                    string folder = folderDialog.SelectedPath;
                    foreach (var filePath in Directory.GetFiles(folder))
                    {
                        if (filePath.Contains(".mf"))
                        {
                            string fileName = Path.GetFileName(filePath);
                            AddFile(filePath, fileName);
                            totalFilesAdded++;
                        }
                    }
                    MessageBox.Show(this, $"Added {totalFilesAdded} content pack files.", "Success");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load that content pack.");
            }
        }

        private void BtnAddPhaseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog importDialog = new OpenFileDialog()
            {
                Title = "Choose a Content Pack",
                Filter = "Multi files|*.mf",
            };
            DialogResult result = importDialog.ShowDialog();
            try
            {

                string originalFilePath = importDialog.FileName;
                string fileName = Path.GetFileName(originalFilePath);
                if (result == DialogResult.OK)
                {
                    AddFile(originalFilePath, fileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load that content pack.");
            }
        }

        private void AddFile(string originalFilePath, string fileName)
        {
            string contentPackPath = _currentDirectory + @"resources\contentpacks\";
            if (!File.Exists(contentPackPath + fileName))
            {
                File.Copy(originalFilePath, contentPackPath + fileName);
                _parentInstance.lstPacks.Items.Add(fileName);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
