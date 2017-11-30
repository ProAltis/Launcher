using System;
using System.Threading.Tasks;
using LauncherLib.Net;

namespace LauncherLib.Login
{
    /// <summary>
    ///     Represents a generic account with a username and password.
    /// </summary>
    public class Account : IAccount
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="user">Username</param>
        /// <param name="pass">Password</param>
        /// <param name="config">The login configuration</param>
        public Account(string user, string pass, LoginConfig config)
        {
            Username = user ?? throw new ArgumentNullException(nameof(user));
            Password = pass ?? throw new ArgumentNullException(nameof(pass));
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        ///     The login configuration
        /// </summary>
        internal LoginConfig Config { get; }

        /// <summary>
        ///     Account username
        /// </summary>
        public string Username { get; }

        /// <summary>
        ///     Account password
        /// </summary>
        public string Password { get; }

        /// <summary>
        ///     Logs in this instance of <see cref="Account"/> to the login configs web server.
        /// </summary>
        /// <returns>A valid <see cref="LoginAPIResponse"/> if successful, otherwise an empty <see cref="LoginAPIResponse"/></returns>
        public async Task<LoginAPIResponse> Login()
        {
            // Get the login api response
            LoginAPIResponse response = await Http.GetLoginAPIResponse(this, Config);

            // Return LoginAPIResponse.Empty if response is null
            return response ?? LoginAPIResponse.Empty;
        }
    }
}
