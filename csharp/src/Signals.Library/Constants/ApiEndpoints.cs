namespace Signals.Library.Constants
{
    /// <summary>
    /// Stores information for various api endpoints
    /// </summary>
    static class ApiEndpoints
    {
        /// <summary>
        /// The authentication endpoint
        /// </summary>
        public readonly static string Auth = "/api/v3/auth";

        /// <summary>
        /// The signals endpoint
        /// </summary>
        public readonly static string Signals = "/api/v3/signals";
        
        /// <summary>
        /// The zip signals
        /// </summary>
        public readonly static string ZipSignals = "/api/v3/signals/zip";

    }
}
