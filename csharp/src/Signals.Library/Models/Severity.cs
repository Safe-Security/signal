using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Signals.Library.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signals.Library.Models
{
    public class Severity
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public int? Value { get; set; } = null;

        [JsonProperty("level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? Level { get; set; }

        [JsonProperty("cvss")]
        public Cvss? CVSS { get; set; }
    }
}
