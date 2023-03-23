using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Signals.Library.Constants
{
    public enum SeverityLevel
    {
        Critical,
        High,
        Medium,
        Low,
        Info
    };

    public enum ControlType
    {
        Detection,
        Mitigation,
        Resilience,
        Recovery
    };

    public enum SecurityType
    {   Finding,
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

    public enum EntityType
    {
        Machine,
        File,
        Identity,
        Organization
    }

    public enum SignalType
    {
        Default,
        EntityOnly,
        SecurityContextOnly
    }

    [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
    public enum Effect
    {
        DataExposure,
        DataExfiltration,
        DataDestruction,
        DenialOfService,
        ResourceConsumption
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ComplianceStatus
    {
        Pass,
        Fail,
        Unknown
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WorkFlowStatus
    {
        New,
        Resolved
    }
}