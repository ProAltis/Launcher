using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAltisLauncher.Forms
{
    public partial class FrmContentPacks : Form
    {

        private List<string> _filePaths = new List<string>();

        private readonly string _currentDirectory = Directory.GetCurrentDirectory() + @"\";

        private string _contentPackFile = Directory.GetCurrentDirectory() + @"\resources\contentpacks\pack-load-order.yaml";

        private bool _needsToBeClosed;

        public FrmContentPacks()
        {
            this.Icon = Properties.Resources.pieicon;
            InitializeComponent();
            try
            {
                Directory.CreateDirectory(_currentDirectory + "resources");
                Directory.CreateDirectory(_currentDirectory + @"resources\contentpacks");
                var myFile = File.Create(_contentPackFile);
                myFile.Close();
                Console.WriteLine("[FrmContentPacks] Created pack loader");
                string[] dirFiles = File.ReadAllLines(_currentDirectory + @"resources\contentpacks\pack-load-order.yaml");
                foreach (string item in dirFiles)
                {
                    lstPacks.Items.Add(Path.GetFileName(item.Replace("- ", "")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured with the content pack loader. Try and restart the launcher." +
                    "\nIf that does not work, show a developer the following stacktrace.\nType: " + ex.GetType() + "\nMessage: " + ex.Message + "\nStacktrace: " + ex.StackTrace);
                _needsToBeClosed = true;
            }
        }

        private void btnMoveItemUp_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
            this.ActiveControl = null;
        }

        private void btnMoveItemDown_Click(object sender, EventArgs e)
        {
            MoveItem(1);
            this.ActiveControl = null;
        }

        private void MoveItem(int direction)
        {
            // Checking selected item
            if (lstPacks.SelectedItem == null || lstPacks.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = lstPacks.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= lstPacks.Items.Count)
                return; // Index out of range - nothing to do

            object selected = lstPacks.SelectedItem;

            // Removing removable element
            lstPacks.Items.Remove(selected);
            // Insert it in new position
            lstPacks.Items.Insert(newIndex, selected);
            // Restore selection
            lstPacks.SetSelected(newIndex, true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

            List<string> items = new List<string>();
            foreach (var item in lstPacks.Items)
            {
                items.Add(item.ToString());
            }
            try
            {
                if (File.Exists(_contentPackFile))
                {
                    Console.WriteLine("[FrmContentPacks] File exists");
                    File.Delete(_contentPackFile);
                }
                else
                {
                    Console.WriteLine("[FrmContentPacks] Loader file doesn't exist.");
                    Directory.CreateDirectory(_currentDirectory + @"resources\contentpacks");
                    File.Create(_contentPackFile);
                    Console.WriteLine("[FrmContentPacks] Created pack loader");
                }
                using (StreamWriter writer = File.AppendText(_contentPackFile))
                {
                    foreach (var item in items)
                    {
                        writer.WriteLine("- " + item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FrmContentPacks] An exception with the packloader was thrown");
                Console.WriteLine("[FrmContentPacks] Type: {0}\n\tStacktrace: {1}", ex.GetType(), ex.StackTrace);
            }
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("[FrmContentPacks] Selected index: " + lstPacks.SelectedIndex);
                int selectedIndex = lstPacks.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    File.Delete(_currentDirectory + @"resources\contentpacks\" + lstPacks.Items[selectedIndex]);
                    lstPacks.Items.RemoveAt(selectedIndex);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to remove the file from the contentpacks folder.");
            }
            this.ActiveControl = null;
        }

        private void FrmContentPacks_Load(object sender, EventArgs e)
        {
            if (_needsToBeClosed)
            {
                this.Close();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Console.WriteLine("[FrmContentPacks] Opening import dialog");
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
                    if (!File.Exists(_currentDirectory + @"resources\contentpacks\" + fileName))
                    {
                        File.Copy(originalFilePath, _currentDirectory + @"resources\contentpacks\" + fileName, true);
                        lstPacks.Items.Add(fileName);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load that content pack.");
            }
            this.ActiveControl = null;
        }
    }
}
