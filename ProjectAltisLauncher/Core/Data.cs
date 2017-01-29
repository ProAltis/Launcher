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
        public static string ConvertToNetworkDataType(long bytes)
        {
            // Determine whether it should be converted, to bytes, kb, mb, gb etc.

            if (bytes >= 1000000000)
            {
                return (bytes / 1000000000).ToString() + " GB";
            }
            else if (bytes >= 1000000)
            {
                return (bytes / 1000000).ToString() + " MB";
            }
            else if (bytes >= 1000)
            {
                return (bytes / 1000).ToString() + " KB";
            }
            else
            {
                return bytes.ToString() + " bytes";
            }
        }
    }
}
