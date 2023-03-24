using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class Evidence
    {
        [JsonProperty("observationText")]
        public string ObservationText { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
