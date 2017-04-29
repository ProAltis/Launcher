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

using System.IO;
using System.Net;

namespace ProjectAltisLauncher.Core
{
    public static class Data
    {
        /// <summary>
        ///     Requests the data.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="method">The method.</param>
        /// <returns>System.String.</returns>
        public static string RequestData(string URL, string method)
        {
            string responseFromServer;
            WebRequest request = WebRequest.Create(URL);
            request.Method = method;
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
        ///     Converts the type of to network data.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string ConvertToNetworkDataType(long bytes)
        {
            // Determine whether it should be converted, to bytes, kb, mb, gb etc.

            if (bytes >= 1000000000L)
                return bytes / 1000000000L + " GB";
            if (bytes >= 1000000L)
                return bytes / 1000000L + " MB";
            if (bytes >= 1000L)
                return bytes / 1000L + " KB";
            return bytes + " bytes";
        }
    }
}