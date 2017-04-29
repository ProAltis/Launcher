#region License

// The MIT License
// 
// Copyright (c) 2017 Project Altis
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ProjectAltisLauncher.Properties;

namespace ProjectAltisLauncher.Forms
{
    public partial class FrmContentPacks : Form
    {
        private readonly string _contentPackFile = Directory.GetCurrentDirectory() +
                                                   @"\resources\contentpacks\pack-load-order.yaml";

        private readonly string _currentDirectory = Directory.GetCurrentDirectory() + @"\";

        private readonly bool _needsToBeClosed;

        private List<string> _filePaths = new List<string>();

        public FrmContentPacks()
        {
            this.Icon = Resources.pieicon;
            InitializeComponent();
            try
            {
                Directory.CreateDirectory(this._currentDirectory + "resources");
                Directory.CreateDirectory(this._currentDirectory + @"resources\contentpacks");
                FileStream myFile = File.Create(this._contentPackFile);
                myFile.Close();
                Console.WriteLine("[FrmContentPacks] Created pack loader");
                string[] dirFiles =
                    File.ReadAllLines(this._currentDirectory + @"resources\contentpacks\pack-load-order.yaml");
                foreach (string item in dirFiles)
                    this.lstPacks.Items.Add(Path.GetFileName(item.Replace("- ", "")));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.FrmContentPacks_FrmContentPacks_An_error_has_occured_with_the_content_pack_loader__Try_and_restart_the_launcher_ +
                                Resources.FrmContentPacks_FrmContentPacks_If_that_does_not_work_show_a_developer_the_following_stacktrace +
                                ex.GetType() + Resources.FrmContentPacks_FrmContentPacks_Message + ex.Message + Resources.FrmContentPacks_FrmContentPacks_Stacktrace__ + ex.StackTrace);
                this._needsToBeClosed = true;
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
            if (this.lstPacks.SelectedItem == null || this.lstPacks.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = this.lstPacks.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= this.lstPacks.Items.Count)
                return; // Index out of range - nothing to do

            object selected = this.lstPacks.SelectedItem;

            // Removing removable element
            this.lstPacks.Items.Remove(selected);
            // Insert it in new position
            this.lstPacks.Items.Insert(newIndex, selected);
            // Restore selection
            this.lstPacks.SetSelected(newIndex, true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            foreach (object item in this.lstPacks.Items)
                items.Add(item.ToString());
            try
            {
                if (File.Exists(this._contentPackFile))
                {
                    Console.WriteLine("[FrmContentPacks] File exists");
                    File.Delete(this._contentPackFile);
                }
                else
                {
                    Console.WriteLine("[FrmContentPacks] Loader file doesn't exist.");
                    Directory.CreateDirectory(this._currentDirectory + @"resources\contentpacks");
                    File.Create(this._contentPackFile);
                    Console.WriteLine("[FrmContentPacks] Created pack loader");
                }
                using (StreamWriter writer = File.AppendText(this._contentPackFile))
                {
                    foreach (string item in items)
                        writer.WriteLine("- " + item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FrmContentPacks] An exception with the packloader was thrown");
                Console.WriteLine("[FrmContentPacks] Type: {0}\n\tStacktrace: {1}", ex.GetType(), ex.StackTrace);
            }
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("[FrmContentPacks] Selected index: " + this.lstPacks.SelectedIndex);
                int selectedIndex = this.lstPacks.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    File.Delete(this._currentDirectory + @"resources\contentpacks\" + this.lstPacks.Items[selectedIndex]);
                    this.lstPacks.Items.RemoveAt(selectedIndex);
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
            if (this._needsToBeClosed)
                Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Console.WriteLine("[FrmContentPacks] Opening import dialog");
            OpenFileDialog importDialog = new OpenFileDialog
            {
                Title = "Choose a Content Pack",
                Filter = "Multi files|*.mf"
            };
            DialogResult result = importDialog.ShowDialog();
            try
            {
                string originalFilePath = importDialog.FileName;
                string fileName = Path.GetFileName(originalFilePath);
                if (result == DialogResult.OK)
                    if (!File.Exists(this._currentDirectory + @"resources\contentpacks\" + fileName))
                    {
                        File.Copy(originalFilePath, this._currentDirectory + @"resources\contentpacks\" + fileName, true);
                        this.lstPacks.Items.Add(fileName);
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