using System;
using Newtonsoft.Json;

namespace LauncherLib.Update
{
    /// <summary>
    ///     Stores metadata about a file
    /// </summary>
    public class FileManifest
    {
        /// <summary>
        ///     The file name
        /// </summary>
        [JsonProperty]
        public string Filename { get; set; }

        /// <summary>
        ///     The file download url
        /// </summary>
        [JsonProperty]
        public string Url { get; set; }

        /// <summary>
        ///     The Sha256 of the file
        /// </summary>
        [JsonProperty]
        public string Sha256 { get; set; }
    }
}
