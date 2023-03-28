

using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    /// Mapping to MITRE ATT&CK matrix.
    /// Note: STIX spec does not have any easy way to
    /// mention this mapping.This is how Signal Spec differs from STIX.
    /// </summary>
    public class Mapping
    {
        /// <value>
        /// The name of the technique.
        /// </value>
        /// <example>
        /// File and Directory Permissions Modification
        /// </example>
        [JsonProperty("techniqueName")]
        public string TechniqueName { get; set; }


        /// <value>
        /// The technique id of TTP.
        /// </value>
        /// <example>
        /// T1222
        /// </example>
        [JsonProperty("techniqueId")]
        public string TechniqueId { get; set; }
    }
}
