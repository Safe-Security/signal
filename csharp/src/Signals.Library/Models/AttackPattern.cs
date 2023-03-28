using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    ///  Attack Patterns are a type of TTP that describe ways that adversaries attempt to compromise targets. Attack Patterns are
    /// used to help categorize attacks, generalize specific attacks to the patterns that they follow, and provide detailed
    /// information about how attacks are performed.An example of an attack pattern is "spear phishing": a common type of attack
    /// where an attacker sends a carefully crafted e-mail message to a party with the intent of getting them to click a link or
    /// open an attachment to deliver malware.Attack Patterns can also be more specific; spear phishing as practiced by a
    /// particular threat actor(e.g., they might generally say that the target won a contest) can also be an Attack Pattern.
    /// References to externally-defined taxonomies of attacks such as [CAPEC] (http://capec.mitre.org/)
    /// </summary>
    public class AttackPattern
    {
        /// <value>Property <c>Name</c> The name, preferably using [CAPEC](http://capec.mitre.org/) terminology</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <value>Property <c>Mapping</c> Mapping to MITRE ATT&CK matrix.
        /// Note: STIX spec does not have any easy way to
        /// mention this mapping.This is how Signal Spec differs from STIX.
        /// </value>
        [JsonProperty("mapping")]
        public Mapping? Mapping { get; set; }

        /// <value>Property <c>SourceName</c> The source of this reference</value
        /// <example>
        ///  capec,cve,microsoft365defender
        /// </example>
        [JsonProperty("sourceName")]
        public string SourceName { get; set; }

        /// <value>Property <c>Description</c> A human readable description</value>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <value>Property <c>SourceId</c> The identifier within the source.</value>
        /// <example>
        /// capec-id
        /// </example>
        [JsonProperty("sourceId")]
        public string? SourceId { get; set; }
    }
}
