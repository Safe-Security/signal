

using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class Mapping
    {
        [JsonProperty("techniqueName")]
        public string TechniqueName { get; set; }

        [JsonProperty("techniqueId")]
        public string TechniqueId { get; set; }
    }
}
