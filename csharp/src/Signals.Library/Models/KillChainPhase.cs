using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class KillChainPhase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phase")]
        public string Phase { get; set; }
    }
}
