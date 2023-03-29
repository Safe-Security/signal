using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Signals.Library.Constants
{
    /// <summary>
    /// A coarse severity level for signal.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum SeverityLevel
    {
        Critical,
        High,
        Medium,
        Low,
        Info
    };


    /// <summary>
    /// A signal can have different types of impact on the security posture.
    ///  This is used in CRQ.
    /// 
    ///  For CA, VA the degree of impact on scoring is well understood using the severity (CVSS score).
    ///  For EDR, the degree of impact on scoring is well understood using the severity (Critical, High, Medium, Low, Info)
    /// 
    ///  For generic signals, when the degree of impact is not well defined or not as per a standard, this field allows the
    ///  submitter to define the degree of impact.
    /// 
    ///  The signal Db has the provision to auto-determine this value for well known CSPs.
    /// </summary>

    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum ControlType
    {
        /// <summary>
        /// Indicates that the signal represents a detection control.
        /// </summary>
        Detection,
        /// <summary>
        /// Indicates that the signal represents a resilience control.
        /// </summary>
        Mitigation,
        /// <summary>
        /// Indicates that the signal represents a resilience control.
        /// </summary>
        Resilience,
        /// <summary>
        /// Indicates that the signal represents a recovery Control.
        /// </summary>
        Recovery
    };

    /// <summary>
    ///  These are the list of different types of Cyber Security types of signals supported by this specification.
    ///  The enum type is specifically string and not an auto-increment number. This is because dealing with numbers
    ///  makes data-lake queries and reports hard to understand. Numeric values are useful for machines and highly
    ///  responsive applications.
    /// </summary>

    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum SecurityType
    {
        Finding,
        Ca,
        Va,
        Edr,
        Log,
        Backup,
        Network,
        Dlp,
        Email,
        Uba,
        Waf,
        Others
    };
    /// <summary>
    /// A signal should always refer to an object on which the security threat applies to.
    /// 
    ///  It can be a machine, machines in the form of hostname(s), IP addresses (IPv4 and IPv6)
    ///  or a network CIDR or even a set of diverse resources like a cloud account (an ARN as an example)
    /// 
    ///  A file which is generally the object when reporting a malware.
    /// 
    ///  A user which is not necessarily a human user. This is typically characterized by an email id or a system user id.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum EntityType
    {
        Machine,
        File,
        Identity,
        Organization
    }
    /// <summary>
    /// 
    /// The signal specification allows submitting a complete signal(with entity and security context) which
    /// is the default use case.

    /// For the use case of daily assessments, there are information which are duplicate. Signal specification
    /// allows the flexibility to the signal submitter to choose to post asset details alone which can later be
    /// used as a reference in full signal submission.
    ///
    /// Example: A submitter would need to post a few 100 signals
    /// for an asset on a daily basis. It is not needed to submit full details about the asset each time.This
    /// approach is motivated by Azure and GCP security center's approach where they have the concept of resources
    /// and findings as separate objects.
    ///
    /// A signal of type = entityOnly is a resource with an unique id. A signal of type = securityContext is a Finding with a unique id
    /// and referencing an entity.
	/// </summary>
    
	[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum SignalType
    {
        Default,
        EntityOnly,
        SecurityContextOnly
    }

    /// <summary>
    /// Possible effect of the signal. The values are motivated by AWS ASFF spec
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum Effect
    {
        DataExposure,
        DataExfiltration,
        DataDestruction,
        DenialOfService,
        ResourceConsumption
    }
    /// <summary>
    /// Status applicable to compliance signals.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum ComplianceStatus
    {
        Pass,
        Fail,
        Unknown
    }
    /// <summary>
    /// Status applicable to capture workflow state from the source of signal.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum WorkFlowStatus
    {
        New,
        Resolved
    }
}