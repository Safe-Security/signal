using Newtonsoft.Json;
using System.Collections.Generic;

namespace Signals.Library.Models
{
    public class EntityManagement
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("connectionString")]
        public string? ConnectionString { get; set; }

        [JsonProperty("lookupAttributes")]
        public Dictionary<string, string> LookupAttributes { get; set; }
    }
}
