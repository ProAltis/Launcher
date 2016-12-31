using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
/// <summary>
/// There are a few things I still have to do before we can start designing the launcher.
/// The download code needs to be finished. I haven't been able to finish it yet because the API's are down.
/// Eventually we can add more features such as a group and invasion tracker. Also saving credentials.
/// </summary>
namespace ProjectAltisLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Global Variables
        private string currentDir = Directory.GetCurrentDirectory() + "\\";
        private double totalFiles = 0;
        private double totalProgress;
        private double currentFile = 0;
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
            string finalURL = "https://www.projectaltis.com/api/?u=" + txtUser.Text + "&p=" + txtPass.Text;
            string APIResponse = RequestData(finalURL, "GET"); // Send request to login API, store the response as string
            loginAPIResponse resp = JsonConvert.DeserializeObject<loginAPIResponse>(APIResponse);
            Console.WriteLine("[Info] Status: {0}", resp.status);
            Console.WriteLine("[Info] Reason: {0}", resp.reason);
            Console.WriteLine("[Info] Additional: {0}", resp.additional);
            switch (resp.status)
            {
                case "true":
                    lblInfo.Text = resp.reason;
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
            //  Play.LaunchGame(txtUser.Text, txtPass.Text);
            Updater.RunWorkerAsync();
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
            string responseFromServer = RequestData("https://www.projectaltis.com/api/manifest", "GET"); // We need to fix this API
            string[] array = responseFromServer.Split('#'); // Seperate each json value into an index


            Console.WriteLine("The length of array is {0}", array.Length);
            Directory.CreateDirectory(currentDir + "config\\");
            Directory.CreateDirectory(currentDir + "resources\\");
            Directory.CreateDirectory(currentDir + "resources\\default\\");
            totalFiles = array.Length - 1;
            for (int i = 0; i < array.Length -1; i++)
            {
                currentFile += 1;


                manifest patchManifest = JsonConvert.DeserializeObject<manifest>(array[i]);
                WebClient client = new WebClient();

                if (patchManifest.filename != null  || patchManifest.filename != "")
                {
                    if (patchManifest.filename.Contains("phase"))
                    {
                        Console.WriteLine("Starting download for phase file: {0}", patchManifest.filename);
                        client.DownloadFile(new Uri(patchManifest.url), currentDir + "resources\\default\\" + patchManifest.filename);
                        Console.WriteLine("Finished!");
                    }
                    else if (patchManifest.filename.Contains("ProjectAltis"))
                    {

                        Console.WriteLine("Starting download for config file: {0}", patchManifest.filename);
                        client.DownloadFile(new Uri(patchManifest.url), currentDir + "config\\" + patchManifest.filename);
                        Console.WriteLine("Finished!");
                    }
                    else
                    {
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
            
            if (totalProgress == 100)
            {
                // Launch game once all files are download
                Play.LaunchGame(txtUser.Text, txtPass.Text);
            }

        }
        


    }
}
