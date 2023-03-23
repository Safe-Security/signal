using Newtonsoft.Json;

namespace Signals.Library.Models
{
    public class Description
    {
        [JsonProperty("technical")]
        public string Technical { get; set; }

        [JsonProperty("business")]
        public string Business { get; set; }

        [JsonProperty("businessImpact")]
        public string BusinessImpact { get; set; }
    }
}
