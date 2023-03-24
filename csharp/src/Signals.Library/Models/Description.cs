using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    /// Description of the security details of this signal.
    /// </summary>
    public class Description
    {
        /// <value>Property <c>Technical</c> A paragraph describing the signal where the target is the technical user </value>
        [JsonProperty("technical")]
        public string Technical { get; set; }

        /// <value>Property <c>Business</c> A paragraph describing the signal where the target is the business user </value>
        [JsonProperty("business")]
        public string Business { get; set; }

        /// <value>Property <c>BusinessImpact</c> A paragraph describing the business impact of this signal.</value>
        [JsonProperty("businessImpact")]
        public string BusinessImpact { get; set; }
    }
}
