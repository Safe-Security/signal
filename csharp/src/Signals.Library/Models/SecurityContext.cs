using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    /// <summary>
    /// The most important aspect of a single is the type of security context. It can be any of the following types or something that the
    /// signal consumer can interpret.
    /// </summary>
    public class SecurityContext
    {
       
        /// <value>
        ///    The type.
        ///    Some common types of signals that impact scoring, are, but not limited to:
        ///  - Vulnerability (NVD CVEs and custom)
        ///  - Mis-Configuration info (CIS and custom)
        ///  - Endpoint Detection and Response (malware, virus, etc)
        ///  - Backup Success/Failure events from Backup and Recovery Software
        ///  - IDP/IPS/NAC/Firewall events
        ///  - DLP events
        ///  - Email Security events
        ///  - SIEM event
        ///  So, the type of the event is a mandatory security context.
        /// </value>
        [JsonProperty("type")]
        public SecurityType Type { get; set; }

        /// <value>        
        /// To provide additional context, subType is an optional field.
        /// </value>
        /// <example>
        /// "UserAccess" can be a subType for type=uba
        /// "Blocked" can be a subType for type=firewall
        /// </example>
        
        [JsonProperty("subType")]
        public string? SubType { get; set; }

        /// <value>
        /// The status of the signal.
        /// </value>
        [JsonProperty("status")]
        public Status Status { get; set; }

        /// <value>
        /// To accommodate a way to capture evidence of the security context.
        /// </value>
        /// <example>
        ///A reference to a screenshot or windows registry content that proves a missing configuration
        /// </example>
        [JsonProperty("evidence")]
        public Evidence? Evidence { get; set; }

     
        /// <value>
        ///The severity of the signal is the most important aspect used in CRQ. Multiple vendors use their own severity system which
        /// make it very hard to consolidate various signals from various security tools. Common variations seen among popular security
        /// vendors include
        /// - 0 - 100 score
        /// - 0 - 10 score
        /// - 0 - no high value
        /// - Low, Medium, High, Critical.
        /// - 0 - 5
        ///
        /// To add to complexity, a high number in some systems indicate high severity, whereas in others its the reverse.
        ///
        /// The most popular standard widely used across the industry are
        /// - CVSS - to communicate characteristics and severity of software vulnerabilities
        /// - CCSS - based on CVSS, but to characterize software security configuration issues.
        /// Non VA and CA systems or for that matter, for reporting any security incidents, usage of CVSS to characterize the incident
        /// severity is commonly used in the industry.
        ///
        /// The signal specification allows full characterization using CVSS/CCSS and yet allows any custom score.
        /// </value>
        [JsonProperty("severity")]
        public Severity Severity { get; set; }

        /// <value>
        /// When a signal maps to certain standard, like CIS, NVD, OWASP, etc, the mapping can be provided
        /// </value>
        [JsonProperty("standardsMapping")]
        public List<StandardMapping>? StandardsMapping { get; set; }

        /// <value>
        /// A list of relevant kill chains this signal contributes to.
        /// </value>
        [JsonProperty("killChainPhases")]
        public List<KillChainPhase>? KillChainPhases { get; set; }

        /// <value>
        /// A list of relevant attack patterns this signal contributes to.
        /// </value>
        [JsonProperty("attackPattern")]
        public List<AttackPattern>? AttackPattern { get; set; }

        /// <summary>
        /// Gets or sets the campaign.
        /// </summary>
        /// <value>
        /// The campaign.
        /// </value>
        [JsonProperty("campaign")]
        public List<Campaign>? Campaign { get; set; }

        /// <value>
        /// The degree of impact.
        ///  To accommodate High Impact control values.
        ///  Range from -10 to +10
        ///  A negative impact translates to improvement in scoring
        ///  A positive impact translates to penalty of score.
        /// </value>
        /// <example>
        ///  0= neutral (use severity to determine impact)
        ///  1=Low
        ///  2=Low-Medium
        ///  3=Medium
        ///  3=Medium-High
        ///  4=High
        ///  5=High-Critical
        ///  6=Critical
        ///  7=Urgent
        ///  8=BreachCertain
        ///  9=BreachConfirmed
        /// </example>
        [JsonProperty("degreeOfImpact")]
        public int DegreeOfImpact { get; set; }

        /// <value>
        /// The possible effect of this signal.
        /// </value>
        [JsonProperty("effect", ItemConverterType = typeof(StringEnumConverter))]
        public List<Effect>? Effect { get; set; }

        /// <value>
        /// Description of the security details of this signal.
        /// </value>
        [JsonProperty("controlType")]
        public ControlType? ControlType { get; set; }

      
        /// <value>
        /// If a signal that has relevant remediation steps. They are to be described here
        /// </value>
        [JsonProperty("remediation")]
        public Remediation? Remediation { get; set; }

        /// <value>
        /// A place holder to add name value pairs as tags/labels.
        /// </value>
        [JsonProperty("tags")]
        public Dictionary<string, string[]>? Tags { get; set; }
    }
}
