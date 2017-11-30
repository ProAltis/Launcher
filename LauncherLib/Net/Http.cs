using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LauncherLib.Login;
using LauncherLib.Update;
using Newtonsoft.Json;

namespace LauncherLib.Net
{
    internal static class Http
    {
        /// <summary>
        ///     The HttpClient used for networking
        /// </summary>
        private static readonly HttpClient _client = new HttpClient();

        /// <summary>
        ///     Contacts the login api server and gets a login response
        /// </summary>
        /// <param name="account">The account credentials</param>
        /// <param name="config">The login configuration</param>
        /// <returns>A valid <see cref="LoginAPIResponse"/> if successful, otherwise an empty <see cref="LoginAPIResponse"/></returns>
        internal static async Task<LoginAPIResponse> GetLoginAPIResponse(IAccount account, LoginConfig config)
        {
            try
            {
                // Encode all content
                var values = new Dictionary<string, string>
                {
                    { config.UserParameter, account.Username },
                    { config.PassParameter, account.Password }
                };

                var content = new FormUrlEncodedContent(values);

                // Post to server
                var response = await _client.PostAsync(config.LoginAPI, content);

                // Get response
                var responseString = await response.Content.ReadAsStringAsync();

                // Return as new object
                return JsonConvert.DeserializeObject<LoginAPIResponse>(responseString);
            }
            catch (Exception)
            {
                return LoginAPIResponse.Empty;
            }
        }

        /// <summary>
        ///     Contacts the file api server gets the file manifest
        /// </summary>
        /// <returns>A <see cref="IFileManifestCollection"/> if successful, otherwise null</returns>
        internal static async Task<IFileManifestCollection> GetFileManifest(Uri fileApi)
        {
            try
            {
                // Get the raw manifest from the file api
                var rawManifest = await _client.GetStringAsync(fileApi);

                // Return the raw manifest as a collection
                return new FileManifestCollection(rawManifest);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static async Task<string> GetGameServer(Uri uri)
        {
            return await _client.GetStringAsync(uri);
        }
    }
}
