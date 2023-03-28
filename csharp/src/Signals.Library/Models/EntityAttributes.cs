using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    /// <summary>
    /// Attributes that describe the asset
    /// </summary>
    public class EntityAttributes
    {
        /// <value>Property <c>IpAddresses</c> One or more IP address interfaces the asset has. A machine can have multiple interfaces as seen from the following commands in Windows/Unix
        /// ifconfig
        /// ipconfig /all
        /// </value>
        [JsonProperty("ipAddresses")]
        public List<IpAddress> IpAddresses { get; set; }

        /// <value>Property <c>Type</c> The type of machine, file or identity.
        /// For a machine this will be the operating system name or the service name
        ///<example>
        /// Windows 10, AWS DynamoDb
        /// </example>
        /// For a file, it will be the file type
        /// <example>
        /// .pdf, .exe
        /// </example>
        /// </value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>Property<c>Criticality</c> 
        ///The criticality of the asset from the business perspective.
        /// </value>
        [JsonProperty("criticality")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? Criticality { get; set; }

        /// <value>Property <c>ConfidentialityRequirement</c> The CVSS score from 0-10.
        /// In information security, confidentiality "is the property, that information is not made available or disclosed to
        /// unauthorized individuals, entities, or processes.
        /// This field captures the confidentiality requirement of the entity for which the signal is referring to.
        /// </value>
        [JsonProperty("confidentialityRequirement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? ConfidentialityRequirement { get; set; }

        /// <value>Property <c>IntegrityRequirement</c> The CVSS score from 0-10.
        /// In IT security, data integrity means maintaining and assuring the accuracy and completeness of data over its entire lifecycle.
        /// This field captures the integrity requirement of the entity for which the signal is referring to
        /// </value>
        [JsonProperty("integrityRequirement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? IntegrityRequirement { get; set; }

        /// <value>Property <c>AvailabilityRequirement</c> The CVSS score from 0-10.
        /// For any information system to serve its purpose, the information must be available when it is needed.
        /// This field captures the integrity requirement of the entity for which the signal is referring to.
        /// </value>
        [JsonProperty("availabilityRequirement")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SeverityLevel? AvailabilityRequirement { get; set; }

        /// <value>Property <c>Tags</c> A place holder to add name value pairs as tags/labels.</value>
        [JsonProperty("tags")]
        public Dictionary<string, string[]> Tags { get; set; }

    }
}
