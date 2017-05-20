using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProjectAltis.Core
{
    public class CpBrowser
    {
        private static CpBrowser _instance;
        public static CpBrowser Instance => _instance ?? (_instance = new CpBrowser());

        public List<RemoteContentPack> CpList;

        public const string ContentPackUrl = "https://github.com/ProAltis/Launcher-Releases/raw/master/cp.json";

        private CpBrowser()
        {
            GetRemoteContentPacks();
        }

        private List<RemoteContentPack> GetRemoteContentPacks()
        {
            CpList = new List<RemoteContentPack>();
            using (WebClient wc = new WebClient())
            {
                string rawJson = wc.DownloadString(ContentPackUrl);
                JObject json = JObject.Parse(rawJson);
                foreach (JToken pack in json["data"])
                {
                    CpList.Add(new RemoteContentPack
                    {
                        Name = pack["name"].ToString(),
                        Author = pack["author"].ToString(),
                        DownloadUrl = pack["downloadurl"].ToString(),
                        Description = pack["description"].ToString()
                    });
                }
            }
            return CpList;
        }
    }

    public class RemoteContentPack
    {
        public string Name;
        public string Author;
        public string DownloadUrl;
        public string Description;
    }
}