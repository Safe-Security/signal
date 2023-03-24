using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    public class Status
    {
        [JsonProperty("complianceStatus")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public ComplianceStatus? ComplianceStatus { get; set; }

        [JsonProperty("workflowStatus")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public WorkFlowStatus? WorkflowStatus { get; set; }
    }
}
