using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class AttackPattern
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mapping")]
        public Mapping? Mapping { get; set; }

        [JsonProperty("sourceName")]
        public string SourceName { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("sourceId")]
        public string? SourceId { get; set; }
    }
}
