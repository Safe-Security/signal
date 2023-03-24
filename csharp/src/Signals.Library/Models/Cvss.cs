using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    ///  The Common Vulnerability Scoring System (CVSS) provides a way to capture the principal characteristics of a vulnerability
    /// and produce a numerical score reflecting its severity.This object is also used to represent CCSS
    /// See [CVSS specification](https://www.first.org/cvss/)
    /// </summary>
    public class Cvss
    {
        /// <value>Property <c>Version</c> The version of CVSS used here.</value>
        /// <example>
        /// CVSS 3.1
        /// </example>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <value>Property <c>Vector</c>  This represents the CVSS string of all metric and its value.</value>
        /// <example>
        /// AV:A/AC:H/PR:L/UI:R/S:C/C:L/I:L/A:L
        /// </example>
        [JsonProperty("vector")]
        public string Vector { get; set; }

        /// <value>Property <c>BaseScore</c> The CVSS score from 0-10.</value>
        [JsonProperty("baseScore")]
        public double BaseScore { get; set; }

        /// <value>Property <c>TemporalScore</c> </value>
        [JsonProperty("temporalScore")]
        public double TemporalScore { get; set; }

        /// <value>Property <c>EnvironmentalScore</c> </value>
        [JsonProperty("environmentalScore")]
        public double EnvironmentalScore { get; set; }
    }
}
