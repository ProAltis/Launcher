using System.IO;
using System.Net;

namespace ProjectAltis.Core
{
    public static class Data
    {
        /// <summary>
        /// Requests the data.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="Method">The method.</param>
        /// <returns>System.String.</returns>
        public static string RequestData(string URL, string Method)
        {
            string responseFromServer;
            WebRequest request = WebRequest.Create(URL);
            request.Method = Method;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
            }
            return responseFromServer;
        }

        /// <summary>
        /// Converts the type of to network data.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string ConvertToNetworkDataType(long bytes)
        {
            // Determine whether it should be converted, to bytes, kb, mb, gb etc.

            if (bytes >= 1000000000)
            {
                return (bytes / 1000000000) + " GB";
            }
            if (bytes >= 1000000)
            {
                return (bytes / 1000000) + " MB";
            }
            if (bytes >= 1000)
            {
                return (bytes / 1000) + " KB";
            }
            return bytes + " bytes";
        }
    }
}
