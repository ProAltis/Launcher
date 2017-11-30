using System;

namespace LauncherLib.Login
{
    /// <summary>
    ///     A login configuration specifying how to send a webrequest
    /// </summary>
    public class LoginConfig
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginConfig"/> class.
        /// </summary>
        /// <param name="loginAPI">The login api that web requests will be sent to</param>
        /// <param name="userParameter">The username parameter for sending a web request to the login api</param>
        /// <param name="passParameter">The password parameter for sending a web request to the login api</param>
        /// <param name="shouldPost"></param>
        public LoginConfig(Uri loginAPI, string userParameter, string passParameter, bool shouldPost = true)
        {
            UserParameter = userParameter ?? throw new ArgumentNullException(nameof(userParameter));
            PassParameter = passParameter ?? throw new ArgumentNullException(nameof(passParameter));
            LoginAPI = loginAPI ?? throw new ArgumentNullException(nameof(loginAPI));
            ShouldPost = shouldPost;
        }

        /// <summary>
        ///     A default config for Project Altis
        /// </summary>
        public static readonly LoginConfig ProjectAltis = new LoginConfig(new Uri("https://projectaltis.com/api/login"), "u", "p");

        /// <summary>
        ///     The username parameter for sending a web request to the login api
        /// </summary>
        internal string UserParameter { get; }

        /// <summary>
        ///     The password parameter for sending a web request to the login api
        /// </summary>
        internal string PassParameter { get; }

        /// <summary>
        ///     The login api that web requests will be sent to
        /// </summary>
        internal Uri LoginAPI { get; }

        /// <summary>
        ///     Determines whether a POST request should be sent
        ///     instead of a GET request.
        ///     True if POST
        ///     False if GET
        /// </summary>
        internal bool ShouldPost { get; }
    }
}
