using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SignalSource
    {
        /// <value>
        ///A unique name identifying the submitter.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the next submission interval in mins.
        /// </summary>
        /// <value>
        ///  If the submitter guarantees that it will re-submit a new state of the finding again, it should be mentioned here.
        /// Note: A signal may be marked a different {@link Signal.expiresAt} in which case, the expiresAt would be given preference.
        /// </value>
        /// <example>
        /// If the source is a daily scanner, then this will be 24*60
        /// </example>
        [JsonProperty("nextSubmissionIntervalInMins")]
        public string? NextSubmissionIntervalInMins { get; set; }
    }
}
