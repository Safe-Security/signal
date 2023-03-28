using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    /// To accommodate a way to capture evidence of the security context.
    /// <example>
    /// A reference to a screenshot or windows registry content that proves a missing configuration
    /// </example>
    /// </summary>
    public class Evidence
    {
        /// <value>
        /// Property<c>Evidence</c> 
        /// To accommodate evidence in the form of text, comments, etc
        /// </value>
        /// <example>
        ///  Registry or config file values or Software versions list.
        /// </example>
        [JsonProperty("observationText")]
        public string ObservationText { get; set; }

        /// <value>
        /// Property<c>Path</c> 
        /// To accommodate references to file based evidences
        /// </value>
        /// <example>
        /// An PDF document or a screenshot stored elsewhere.
        /// </example>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
