using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class Location
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}
