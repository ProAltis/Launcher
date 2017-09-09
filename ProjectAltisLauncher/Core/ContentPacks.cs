using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ProjectAltis.Manifests;

namespace ProjectAltis.Core
{
    public class ContentPacks
    {
        private static ContentPacks _instance;
        public static ContentPacks Instance = _instance ?? (_instance = new ContentPacks());
        
        public ContentPackResponse cachedPacks = null;

        public ContentPacks()
        {
            cachedPacks = GetPacks();
            MessageBox.Show(cachedPacks.data.First().versions.First().displayversion);
        }

        private static ContentPackResponse GetPacks()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string response = wc.DownloadString("https://raw.githubusercontent.com/ProAltis/Launcher-Releases/master/cp.json");

                    ContentPackResponse contentPackResponse = JsonConvert.DeserializeObject<ContentPackResponse>(response);
                    return contentPackResponse;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return new ContentPackResponse
                {
                    data = new List<ContentPackDetail>
                    {
                        new ContentPackDetail
                        {
                            author = "",
                            description = "",
                            name = "INVALID",
                            versions = new List<ContentPackVersion>
                            {
                                new ContentPackVersion
                                {
                                    displayversion = "",
                                    download = "",
                                    size = "",
                                    updates = "",
                                    version = 1
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}