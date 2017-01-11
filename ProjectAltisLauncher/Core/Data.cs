using System.IO;
using System.Net;

namespace ProjectAltisLauncher.Core
{
    class Data
    {
        public static string RequestData(string URL, string Method)
        {
            string responseFromServer;
            WebRequest request = WebRequest.Create(URL);
            request.Method = Method;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = default(Stream))
                {
                    var myStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(myStream);
                    responseFromServer = reader.ReadToEnd();
                }
            }
                return responseFromServer;
        }
    }
}
