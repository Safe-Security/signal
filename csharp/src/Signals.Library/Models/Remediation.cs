using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    ///  If the signal that has relevant remediation steps. They are to be described here
    /// </summary>
    public class Remediation
    {

        ///<value>
        /// The description of the remediation.
        /// </value>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <value>
        /// Any any references to web sites or documents
        /// </value>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <value>
        /// Describes the impact of remediation
        /// </value>
        [JsonProperty("impact")]
        public string? Impact { get; set; }
    }
}
