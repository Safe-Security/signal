using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class Remediation
    {

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("reference")]
        public string? Reference { get; set; }

        [JsonProperty("impact")]
        public string? Impact { get; set; }
    }
}
