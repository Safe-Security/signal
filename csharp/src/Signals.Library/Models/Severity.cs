using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    /// <summary>
    /// The severity of the signal is the most important aspect used in CRQ. Multiple vendors use their own severity system which
    /// make it very hard to consolidate various signals from various security tools. Common variations seen among popular security
    /// vendors include
    ///
    /// - 0-100 score
    /// - 0-10 score
    /// - 0-<no high value>
    /// - Low, Medium, High, Critical.
    /// - 0-5
    ///
    /// To add to complexity, a high number in some systems indicate high severity, whereas in others its the reverse.
    ///
    /// The most popular standard widely used across the industry are
    ///
    /// - CVSS - to communicate characteristics and severity of software vulnerabilities
    /// - CCSS - based on CVSS, but to characterize software security configuration issues.
    ///
    /// Non VA and CA systems or for that matter, for reporting any security incidents, usage of CVSS to characterize the incident
    /// severity is commonly used in the industry.
    ///
    /// The signal specification allows full characterization using CVSS/CCSS and yet allows any custom score.
    /// </summary>
    public class Severity
    {
        /// <value>
        /// Indicates the type for severity. Example -cvss, ccss, custom
        /// </value>
       
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>
        /// A numeric value that indicates the severity. Its range will vary based on the type.
        /// </value>
        /// <example>
        ///  if type=cvss, the range will be from 0-10
        /// </example>
        [JsonProperty("value")]
        public int? Value { get; set; } = null;

        /// <value>
        /// Typically in the absence of a CVSS score, a coarse value is used here.
        /// This may be present even when a specific score is present.
        /// </value>
        /// <example>
        /// A CVSS rating of 9.5 is considered to have a Severity Level = High
        /// </example>
        [JsonProperty("level")]
        public SeverityLevel? Level { get; set; }

        /// <value>
        /// The Common Vulnerability Scoring System based score.
        /// </value>
        [JsonProperty("cvss")]
        public Cvss? CVSS { get; set; }
    }
}
