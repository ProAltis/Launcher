using Newtonsoft.Json;

namespace LauncherLib.Login
{
    /// <summary>
    ///     Represents a login server response
    /// </summary>
    public class LoginAPIResponse
    {
        /// <summary>
        ///     A constant response for a "Good" login
        /// </summary>
        [JsonIgnore]
        public const bool Good = true;

        /// <summary>
        ///     A constant response for a "Bad" login
        /// </summary>
        [JsonIgnore]
        public const bool Bad = false;

        /// <summary>
        ///     An empty response
        /// </summary>
        [JsonIgnore]
        public static readonly LoginAPIResponse Empty = new LoginAPIResponse(false, "Empty", "Empty");

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginAPIResponse"/> class.
        /// </summary>
        /// <param name="status">The response status</param>
        /// <param name="reason">The response reason</param>
        /// <param name="additional">The response additional information</param>
        public LoginAPIResponse(bool status, string reason, string additional)
        {
            Status = status;
            Reason = reason;
            Additional = additional;
        }

        /// <summary>
        ///     The login status
        /// </summary>
        [JsonProperty]
        public bool Status { get; }

        /// <summary>
        ///     The reason
        /// </summary>
        [JsonProperty]
        public string Reason { get; }

        /// <summary>
        ///     The additional information
        /// </summary>
        [JsonProperty]
        public string Additional { get; }
    }
}
