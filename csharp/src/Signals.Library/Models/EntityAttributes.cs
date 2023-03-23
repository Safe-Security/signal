using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;
using System.Collections.Generic;

namespace Signals.Library.Models
{
    public class EntityAttributes
    {
        [JsonProperty("ipAddresses")]
        public List<IpAddress> IpAddresses { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("criticality")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? Criticality { get; set; }

        [JsonProperty("confidentialityRequirement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? ConfidentialityRequirement { get; set; }

        [JsonProperty("integrityRequirement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? IntegrityRequirement { get; set; }

        [JsonProperty("availabilityRequirement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? AvailabilityRequirement { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string,string[]> Tags { get; set; }

    }
}
