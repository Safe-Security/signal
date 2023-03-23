using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class SignalSource
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nextSubmissionIntervalInMins")]
        public string? NextSubmissionIntervalInMins { get; set; }
    }
}
