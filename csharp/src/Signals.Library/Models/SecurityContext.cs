using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    public class SecurityContext
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public SecurityType Type { get; set; }

        [JsonProperty("subType")]
        public string? SubType { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("evidence")]
        public Evidence? Evidence { get; set; }

        [JsonProperty("severity")]
        public Severity Severity { get; set; }

        [JsonProperty("standardsMapping")]
        public List<StandardMapping>? StandardsMapping { get; set; }

        [JsonProperty("killChainPhases")]
        public List<KillChainPhase>? KillChainPhases { get; set; }

        [JsonProperty("attackPattern")]
        public List<AttackPattern>? AttackPattern { get; set; }

        [JsonProperty("campaign")]
        public List<Campaign>? Campaign { get; set; }

        [JsonProperty("degreeOfImpact")]
        public int DegreeOfImpact { get; set; }

        [JsonProperty("effect", ItemConverterType = typeof(StringEnumConverter))]
        // [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public List<Effect>? Effect { get; set; }

        [JsonProperty("controlType")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public ControlType? ControlType { get; set; }

        [JsonProperty("remediation")]
        public Remediation? Remediation { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, string[]>? Tags { get; set; }
    }
}
