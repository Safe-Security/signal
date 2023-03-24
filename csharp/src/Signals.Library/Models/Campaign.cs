using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class Campaign
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("aliases")]
        public List<string> Aliases { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("firstSeen")]
        public DateTime FirstSeen { get; set; }

        [JsonProperty("lastSeen")]
        public DateTime LastSeen { get; set; }
    }
}
