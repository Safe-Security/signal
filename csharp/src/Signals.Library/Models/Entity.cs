using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    public class Entity
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public EntityType Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("entityAttributes")]
        public EntityAttributes EntityAttributes { get; set; }

        [JsonProperty("entityManagement")]
        public EntityManagement EntityManagement { get; set; }

        [JsonProperty("connectionAttributes")]
        public ConnectionAttributes ConnectionAttributes { get; set; }
    }
}
