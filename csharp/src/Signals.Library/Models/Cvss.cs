using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class Cvss
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("vector")]
        public string Vector { get; set; }

        [JsonProperty("baseScore")]
        public double BaseScore { get; set; }

        [JsonProperty("temporalScore")]
        public double TemporalScore { get; set; }

        [JsonProperty("environmentalScore")]
        public double EnvironmentalScore { get; set; }
    }
}
