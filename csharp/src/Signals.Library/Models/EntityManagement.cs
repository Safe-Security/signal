using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    /// Points to an asset management system where more details about the entity could be found.
    /// <example>
    /// An ldap server, an ADFS server, an employee management system or an Identity management system
    /// </example>
    /// </summary>
    public class EntityManagement
    {
        /// <value>Property <c>Type</c> Type of the management system.</value>
        /// <example><
        /// ldap
        /// /example>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>Property <c>ConnectionString</c> If it can be referenced by a connection string.</value>
        [JsonProperty("connectionString")]
        public string? ConnectionString { get; set; }

        /// <value>Property <c>LookupAttributes</c> A set of name value pairs that can be used to infer the connection details to the system.</value>
        [JsonProperty("lookupAttributes")]
        public Dictionary<string, string> LookupAttributes { get; set; }
    }
}
