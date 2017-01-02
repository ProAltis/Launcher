using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;
/// <summary>
/// TODO:
///     Work on background art.
///     Improve Buttons Look
///     Add changelog
///     Add group tracker
///     Add invasion tracker
/// </summary>
namespace ProjectAltisLauncher
{
    public partial class Form1 : Form
    {
        #region Main Form Events
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                txtUser.Text = Properties.Settings.Default.username;
                txtPass.Text = Properties.Settings.Default.password;
            }
            catch { }
            // Load last saved user background choice
            SetBackground(Properties.Settings.Default.background);
            // This prevents other controls from being focused
            this.Select();
            this.ActiveControl = null;
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Select();
            this.ActiveControl = null;
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            this.Select();
            this.ActiveControl = null;
        }
        #endregion
        #region Global Variables
        private string currentDir = Directory.GetCurrentDirectory() + "\\";
        private double totalFiles = 0;
        private double totalProgress;
        private double currentFile = 0;
        private string nowDownloading = "";
        #endregion
        #region Borderless Form Code
        Point mouseDownPoint = Point.Empty;


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPoint = new Point(e.X, e.Y);
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
                return;
            Form f = sender as Form;
            f.Location = new Point(f.Location.X + (e.X - mouseDownPoint.X), f.Location.Y + (e.Y - mouseDownPoint.Y));
        }
        #endregion
        #region Button Behaviors
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (cbSaveLogin.Checked == true)
            {
                Console.WriteLine("Save checked");
                if (txtUser.Text != null || txtPass.Text != null)
                {
                    Properties.Settings.Default.username = txtUser.Text;
                    Properties.Settings.Default.password = txtPass.Text;
                    Properties.Settings.Default.Save();
                }

            }
            string finalURL = "https://www.projectaltis.com/api/?u=" + txtUser.Text + "&p=" + txtPass.Text;
            string APIResponse = RequestData(finalURL, "GET"); // Send request to login API, store the response as string
            loginAPIResponse resp = JsonConvert.DeserializeObject<loginAPIResponse>(APIResponse);

            switch (resp.status)
            {
                case "true":
                    lblInfo.Text = resp.reason;
                    btnPlay.Enabled = false;
                    Updater.RunWorkerAsync();
                    break;
                case "false":
                    lblInfo.Text = resp.reason;
                    break;
                case "critical":
                    lblInfo.Text = resp.additional;
                    break;
                case "info":
                    lblInfo.Text = resp.reason;
                    break;
            }
            lblInfo.Visible = true;
            this.ActiveControl = null;
        }
        private void btnOfficialSite_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.projectaltis.com/");
            this.ActiveControl = null;
        }
        private void btnDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/szEPYtV");
        }
        private void btnGroupTracker_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Group Tracker will be implemented soon!", "Oops!");
        }
        private void btnCredits_Click(object sender, EventArgs e)
        {
            Credits f = new Credits();
            f.ShowDialog();
        }
        private void btnChangeBg_Click(object sender, EventArgs e)
        {
            BackgroundChoices bg = new BackgroundChoices();
            bg.ShowDialog();
        }
        private void btnOptions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Options will be implemented soon!", "Oops!");
        }
        #endregion
        #region Hashing Functions
        private bool CompareFileSize(string filePath, int size)
        {
            try
            {
                FileInfo myFile = new FileInfo(filePath);
                long sizeInBytes = myFile.Length;
                return sizeInBytes == size; // returns true or false

            }
            catch (Exception)
            {
                // The file doesn't exist
                Console.WriteLine("{0} does not exist!", filePath);
                return false;
            }
        }
        private bool CompareSHA256(string filePath, string hash)
        {
            try
            {
                SHA256 mySHA256 = SHA256.Create();

                FileInfo myFile = new FileInfo(filePath);
                using (FileStream fileStream = myFile.Open(FileMode.Open))
                {
                    byte[] hashValue = mySHA256.ComputeHash(fileStream);
                    string strHashValue = "";
                    foreach (byte x in hashValue)
                    {
                        strHashValue += x.ToString("x2");
                    }
                    // Comparing the hash now
                    Console.WriteLine("The SHA256 of {0} is: {1}", filePath, strHashValue);

                    return strHashValue == hash;
                }

            }
            catch (Exception)
            {
                // File doesn't exist
                Console.WriteLine("{0} does not exist!", filePath);
                return false;
            }
        }
        #endregion
        private string RequestData(string URL, string Method)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Method = Method;
                WebResponse response = request.GetResponse();
                Stream dataStream = default(Stream);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Thrown: " + "\n  Type:    " + ex.GetType().Name + "\n  Message: " + ex.Message);
                this.Close();
            }
            return "";
        }
        private void Updater_DoWork(object sender, DoWorkEventArgs e)
        {
            currentFile = 0; // Reset the value so every time user plays totalProg
            string responseFromServer = RequestData("https://www.projectaltis.com/api/manifest", "GET");
            string[] array = responseFromServer.Split('#'); // Seperate each json value into an index


            Console.WriteLine("The length of array is {0}", array.Length);
            Directory.CreateDirectory(currentDir + "config\\");
            Directory.CreateDirectory(currentDir + "resources\\");
            Directory.CreateDirectory(currentDir + "resources\\default\\");
            totalFiles = array.Length - 1;
            for (int i = 0; i < array.Length - 1; i++)
            {
                currentFile += 1;
                manifest patchManifest = JsonConvert.DeserializeObject<manifest>(array[i]);
                WebClient client = new WebClient();
                if (patchManifest.filename != null || patchManifest.filename != "")
                {
                    if (patchManifest.filename.Contains("phase"))
                    {
                        if (CompareFileSize(currentDir + "resources\\default\\" + patchManifest.filename, Convert.ToInt32(patchManifest.size)))
                        {
                            Console.WriteLine("Phase file: {0} is up to date!", patchManifest.filename);
                        }
                        else
                        {
                            nowDownloading = patchManifest.filename;
                            Updater.ReportProgress(0); // Fire the progress changed event
                            Console.WriteLine("Starting download for phase file: {0}", patchManifest.filename);
                            client.DownloadFile(new Uri(patchManifest.url), currentDir + "resources\\default\\" + patchManifest.filename);
                            Console.WriteLine("Finished!");
                        }
                    }
                    else if (patchManifest.filename.Contains("ProjectAltis"))
                    {
                        if (CompareSHA256(currentDir + "config\\" + patchManifest.filename, patchManifest.sha256))
                        {

                        }
                        else
                        {
                            nowDownloading = patchManifest.filename;
                            Updater.ReportProgress(0); // Fire the progress changed event
                            client.DownloadFile(new Uri(patchManifest.url), currentDir + "config\\" + patchManifest.filename);
                        }

                    }
                    else if (CompareSHA256(currentDir + patchManifest.filename, patchManifest.sha256)) // If the hashes are the same skip the update
                    {
                        Console.WriteLine("{0} is up to date!", patchManifest.filename);
                    }
                    else
                    {
                        nowDownloading = patchManifest.filename;
                        Updater.ReportProgress(0); // Fire the progress changed event
                        Console.WriteLine("Starting download for file: {0}", patchManifest.filename);
                        client.DownloadFile(new Uri(patchManifest.url), currentDir + patchManifest.filename);
                        Console.WriteLine("Finished!");
                    }
                }



                totalProgress = ((currentFile / totalFiles) * 100);
                Console.WriteLine("Total progress is {0}", totalProgress);
                Updater.ReportProgress(Convert.ToInt32(totalProgress));

            }
        }
        private void Updater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblNowDownloading.Visible = true;
            lblNowDownloading.Text = "Downloading " + nowDownloading;
            if (totalProgress == 100)
            {
                btnPlay.Enabled = true;
                lblNowDownloading.Text = "";
                lblNowDownloading.Visible = false;
                // Launch game once all files are download
                Play.LaunchGame(txtUser.Text, txtPass.Text);
            }

        }
        private void SetBackground(string bg)
        {
            switch (bg)
            {
                case "TTC":
                    BackgroundImage = Properties.Resources.TTC;
                    break;
                case "DD":
                    BackgroundImage = Properties.Resources.DD;
                    break;
                case "DG":
                    BackgroundImage = Properties.Resources.DG;
                    break;
                case "MML":
                    BackgroundImage = Properties.Resources.MML;
                    break;
                case "Brrrgh":
                    BackgroundImage = Properties.Resources.Brrrgh;
                    break;
                case "DDL":
                    BackgroundImage = Properties.Resources.DDL;
                    break;
            }
        }
    }
}
