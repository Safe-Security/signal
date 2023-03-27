using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    /// When a signal maps to certain standard, like CIS, stig, NVD, OWASP, etc, the mapping can be provided.
    /// </summary>
    /// <example>
    /// a Signal that maps to CIS standard Section 3.1 can have
    /// </example>
    /// <code>
    /// Name:"cisBenchMark",
    /// "Value":"9.14",
    /// "Properties":[
    /// { "cisBenchmarkVersion:"v1.0.0"
    /// }]
    /// </code>

    public class StandardMapping
    {
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <value>
        /// The value.
        /// </value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <value>
        /// The properties.
        /// </value>
        [JsonProperty("properties")]
        public Dictionary<string, string[]> Properties { get; set; }
    }
}
