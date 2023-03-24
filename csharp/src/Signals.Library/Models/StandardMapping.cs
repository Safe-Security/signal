using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class StandardMapping
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string, string[]> Properties { get; set; }
    }
}
