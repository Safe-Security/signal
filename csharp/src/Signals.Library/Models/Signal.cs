using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
     /// <summary>
     /// Signal
     /// Any kind of information about an asset that would be interesting from a security scoring point of view.
     /// This specification is motivated and uses references from STIX specification.You will notice several attribute name
     /// and description matching the[STIX 2.1] (https://docs.oasis-open.org/cti/stix/v2.1/os/stix-v2.1-os.html) specification.
     ///
     ///[STIX] (https://oasis-open.github.io/cti-documentation/stix/intro) is designed to improve many different capabilities, such as collaborative threat analysis, automated threat exchange,
     /// automated detection and response, and more.
     ///
     /// STIX has a far wider scope. While STIX is a well defined standard, its adoption within Security Products has been limited
     /// as most Security products deals with a small aspect of Cyber Security.Most security products therefore provide STIX
     /// interfaces to generate or consume STIX objects.
     ///
     /// Safe Signal focuses on understanding Signals which contribute to CRQ.This project will support importing and exporting to STIX format.
     ///
     /// Need for Signal spec when STIX is already present
     /// -------------------------------------------------
     /// STIX is a complex system, in the sense that it has a long learning curve.Its main goal is cross-collaboration between organization
     /// to exchange Cyber Threat Intelligence. STIX is a connected graph of nodes and edges.See STIX Spec document here.The SAFE Signal
     /// specification is motivated by
     /// - Those parts of STIX that can contribute to CQR
     /// - Aligned to existing SAFE architecture, which focuses on a layered scoring system for CISOs.
     ///
     /// SAFE Signal spec as the step towards adopting all aspects of CTI (Cyber Threat Intelligence).
     ///
     /// Background and Requirements
     /// ---------------------------
     /// SAFE(as of June, 2022) understands CA, VA and EDR signals only.These are not all the signals that an organization has.Therefore
     /// there is a need to ingest all interesting signals for a holistic scoring and risk calculation.
     ///
     /// String value convention for fields
     /// ----------------------------------
     /// There are several attributes of type
     /// ```
     /// string | SignalUrl
     /// ```
     /// SignalUrl is a reference to another signal where the details can be found.This allows non-repetition of some long strings.
     /// Example:
     /// The Signal.securityContext.remediation.description can be a bug paragraph. A submitter may be submitting the same signal for the
     /// asset to the Signal Db on a regular or daily basis. There is no need to submit large para to be submitted in every signal.
     /// A signal attribute can refer to another signal via the signal Url, via the signal.id field. So a submitter of a signal may references
     /// it via SignalUrl as
     /// ```
     /// remediation = "signalurl://<id of the original signal>"
     /// ```
     /// The above will refer to the remediation text of the referred signal.
     /// </summary>
    public class Signal
    {
        /// <value>Property <c>Version</c> The specification of this SAFE Signal version.</value>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <value>Property <c>Id</c>A signal must have an unique id, preferably a GUID. This needs to be globally unique as it will be referenced for relationship building.</value>
        [JsonProperty("id")] 
        public string Id { get; set; }

        /// <value>Property <c>Type</c> The type of signal.</value>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public SignalType? Type { get; set; }

        /// <value>Property <c>Name</c> Indicates the name of the signal.It is different from the @id field as @id represents an instance of this name.</value>
        /// <example>
        /// Consider a CA event which you want to submit multiple times. The name could be "Password length should be 8 chars long"
        /// Every time you submit, you must keep this name the same else it will be treated as a different CA event.
        /// In every re-submission, a different @id must be used, else it will over-write a previously submitted signal.
        /// </example>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <value>Property <c>Type</c> Describe the source of the signal..</value>
        [JsonProperty("source")] 
        public SignalSource Source { get; set; }

        /// <value>Property <c>Description</c>  A paragraph that provides a human readable explanation of this signal.</value>
        [JsonProperty("description")] 
        public string? Description { get; set; }

        /// <value>Property <c>Entity</c> The type of the entity the signal applies to. See {@link EntityType}. </value>
        [JsonProperty("entity")] 
        public Entity? Entity { get; set; }

        /// <value>Property <c>SecurityContext</c>  Every signal MUST contain at least one interesting security information that contributes to SAFE score. This information can be either a positive or a negative contributor to the score.</value>
        /// <example>
        /// An antivirus agent installed on en Endpoint is a positive security context
        /// A malware detection or a CA control failing is a negative security context.
        /// </example>
        [JsonProperty("securityContext")] 
        public SecurityContext? SecurityContext { get; set; }

        /// <value>Property <c>SecurityContexts</c> If the signal contains more than one security information,the information can be sent as array of security information.</value>
        [JsonProperty("securityContexts")] 
        public List<SecurityContext>? SecurityContexts { get; set; }

        /// <value>Property <c>BusinessContext</c>  Information which do not describe the technical nature of the signal, but necessary for understanding the impact on business
        /// are provided in this section.This information may vary from organization to organization.
        /// This is to be provided in the form of key-value pairs. The signal specification currently does not have any standard for naming them
        /// </value>
        /// <example>
        ///  Cost of remediation of a vulnerability signal
        ///  Potential impact on cyber-insurance due to this signal.
        /// </example>

        [JsonProperty("businessContext")] 
        public Dictionary<string, string[]>? BusinessContext { get; set; }

        /// <value>Property <c>FirstSeen</c> The time when this was last seen. Typically used by connector to represent last detection time.</value>
        [JsonProperty("firstSeen")] 
        public DateTime? FirstSeen { get; set; }

        /// <value>Property <c>lastSeen</c> The date when this was modified.</value>
        [JsonProperty("lastSeen")] 
        public DateTime? LastSeen { get; set; }

        /// <value>Property <c>Confidence</c> A value in the range of 0-100. The confidence that the creator has in the correctness of their data.</value>
        [JsonProperty("confidence")] 
        public int Confidence { get; set; }

        /// <value>Property <c>CreatedAt</c> This is the first seen time and not when the signal object is being created as a JSON.</value>
        [JsonProperty("createdAt")] 
        public DateTime CreatedAt { get; set; }

        /// <value>Property <c>ModifiedAt</c> The date when this was modified</value>
        [JsonProperty("modifiedAt")] 
        public DateTime? ModifiedAt { get; set; }

        /// <value>Property <c>ExpiresAt</c> The date when this signal is no longer relevant. Sources which guarantee that it will re-submit a new assessment
        /// should use this field.</value>
        /// <example>
        /// A daily assessment submitter would have a Date = 24 hours from the time of submission.
        /// </example>.
        [JsonProperty("expiresAt")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? ExpiresAt { get; set; }

        /// <value>Property <c>Revoked</c> If the signal is no longer valid, mark it revoked and update the modifiedAt Date. </value>
        [JsonProperty("revoked")] 
        public bool? Revoked { get; set; }

        /// <value>Property <c>Tags</c>  A place holder to add name value pairs as tags/labels.</value>
        [JsonProperty("tags")] 
        public Dictionary<string, string[]>? Tags { get; set; }

        /// <value>Property <c>Location</c> Some signals are relevant to only certain regions or countries. In such cases, mention the country or countries.
        /// Leave it blank if this signal is not region specific
        /// </value>
        [JsonProperty("location")] 
        public List<Location>? Location { get; set; }

        /// <value>Property <c>Comment</c> The signal MUST contain a reference to an entity. An entity can be a machine, file or user.</value>
        /// <example>
        /// Added ATT&CK mapping to the signal
        /// </example>
        [JsonProperty("comment")] 
        public string? Comment { get; set; }
    }
}