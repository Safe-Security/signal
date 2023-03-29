using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    /// <summary>
    /// The status of the signal signifies the state of the security information. It can vary depending on the type of signal
    /// </summary>
    /// <example>
    /// A Compliance signal must be either Pass, Fail or NotAssessed
    /// Customer often mark status based on their business workflow. These are captured under workflow status like "AcceptedFailed, Archived"
    /// </example>
    public class Status
    {
        /// <value>
        /// The compliance status.
        /// </value>
        [JsonProperty("complianceStatus")]
        public ComplianceStatus? ComplianceStatus { get; set; }

        /// <value>
        /// The workflow status.
        /// </value>
        [JsonProperty("workflowStatus")]
        public WorkFlowStatus? WorkflowStatus { get; set; }
    }
}
