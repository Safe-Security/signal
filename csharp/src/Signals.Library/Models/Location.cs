using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    ///  A geographical location. Use [ISO-3166 Alpha-2](https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes) codes
    /// </summary>
    public class Location
    {
        /// <value>
        /// The country code.
        /// </value>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}
