using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class IpAddress
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ipv4")]
        public string? Ipv4 { get; set; }

        [JsonProperty("ipv6")]
        public string? Ipv6 { get; set; }
    }
}
