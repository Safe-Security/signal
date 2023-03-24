using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    public class Signal
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public SignalType? Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("source")]
        public SignalSource Source { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("entity")]
        public Entity? Entity { get; set; }

        [JsonProperty("securityContext")]
        public SecurityContext? SecurityContext { get; set; }

        [JsonProperty("securityContexts")]
        public List<SecurityContext>? SecurityContexts { get; set; }

        [JsonProperty("businessContext")]
        public Dictionary<string, string[]>? BusinessContext { get; set; }

        [JsonProperty("firstSeen")]
        public DateTime? FirstSeen { get; set; }

        [JsonProperty("lastSeen")]
        public DateTime? LastSeen { get; set; }

        [JsonProperty("confidence")]
        public int Confidence { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        [JsonProperty("expiresAt")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? ExpiresAt { get; set; }

        [JsonProperty("revoked")]
        public bool? Revoked { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, string[]>? Tags { get; set; }

        [JsonProperty("location")]
        public List<Location>? Location { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }
    }
}
