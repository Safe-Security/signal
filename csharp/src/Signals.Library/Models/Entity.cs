using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Signals.Library.Constants;

namespace Signals.Library.Models
{
    /// <summary>
    /// The signal MUST contain a reference to an entity. An entity can be a machine, file or user.
    /// </summary>
    public class Entity
    {
        /// <value>Property <c>Type</c> The type of the entity the signal applies to. See {EntityType} </value>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public EntityType Type { get; set; }

        /// <value>Property <c>Name</c> The name of the entity. Typically this should be a fully qualified name.</value>
        /// <example>
        /// FQDN
        /// IP address
        /// Email Id
        /// Filename
        /// An Arn
        /// A CIDR block
        /// </example>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <value>Property <c>EntityAttributes</c> 
        ///  A reference to an asset management software may not always be feasible. A machine/identify/file has many attributes
        /// which are useful for business workflows. Therefore there should be an option to specify additional attributes.
        /// Some of these attributes hold significant business values (even though optional).
        /// So, the following are recommended but optional attributes of an asset
        /// </value>
        [JsonProperty("entityAttributes")]
        public EntityAttributes EntityAttributes { get; set; }

        /// <value>Property <c>EntityManagement</c>  Identity/Asset/File Management is a well known concept and all organization have a centralized
        /// asset/identity/file management software. This is true for servers and endpoints.However from ephemeral, temporary and dynamic cloud assets, they
        /// are sometimes not tracked by a centralized Asset Management product.Popular asset management products include, but not limited to:
        /// - CMDB
        /// - LDAP
        /// - MS Active Directory
        /// - Azure Active Directory
        /// - ServiceNow ITSM
        ///  -IBM Maximo
        /// - Infor EAM
        ///The signal specification should allow submitters to add a reference to the Asset Management product.
        ///So, there is a need to specify
        /// - a type of CMDB
        /// - a connectionString
        /// - an optional list of key value that can be used to format the connection string.
        /// - connectionRef : Access to AssetManagement software is a sensitive information.Therefore, the signal
        /// submitter may supply only a reference to the asset management software as part of signal submission.
        /// </value>
        [JsonProperty("entityManagement")]
        public EntityManagement EntityManagement { get; set; }


        /// <value>Property <c>ConnectionAttributes</c> 
        /// A special scenario where the signal submitter does not have any security context to share, but still submits the asset
        ///  into the DB, with the expectation that the consumer of this signal has the ability to connect to the asset and perform
        /// native assessment.
        /// </value>
        /// <example>
        /// A submitter posting a Windows or a Ubuntu or a cloud connection details, expect SAFE to on-board and retrieve
        /// security context.
        /// </example>
        [JsonProperty("connectionAttributes")]
        public ConnectionAttributes ConnectionAttributes { get; set; }
    }
}
