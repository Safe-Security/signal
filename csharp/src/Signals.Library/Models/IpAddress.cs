using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    /// Represent an IP address interface
    /// </summary>
    public class IpAddress
    {
        /// <value>Property<c>Name</c> interface name, Example: eth0, Wifi </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <value>Property<c>Ipv4</c> An IP v4 in string format. It does not need to be an int32 for convenience </value>
        [JsonProperty("ipv4")]
        public string? Ipv4 { get; set; }

        /// <value>Property<c>Ipv6</c>  An IP v6 string.</value>`
        [JsonProperty("ipv6")]
        public string? Ipv6 { get; set; }
    }
}
