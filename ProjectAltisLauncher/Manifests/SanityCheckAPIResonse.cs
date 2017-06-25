namespace ProjectAltis.Manifests
{
    public class SanityCheckApiResponse
    {
        /// <summary>
        /// if there's an error
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// "true" or "false" for if they get pre-beta
        /// </summary>
        public string statuscheck { get; set; }

        /// <summary>
        /// "true" or "false"
        /// </summary>
        public string isbanned { get; set; }

        /// <summary>
        /// Reason for ban
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// integer in form of string. Int.Parse for int. 150 is default and has no privileges
        /// </summary>
        public string powerlevel { get; set; }
    }
}
