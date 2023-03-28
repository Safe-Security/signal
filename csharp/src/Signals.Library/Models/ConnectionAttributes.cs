using Newtonsoft.Json;

namespace Signals.Library.Models
{   /// <summary>
    /// This is a SAFE specific section which is needed for supporting SAFE managed asset on-boarding and assessment from SAFE.
    /// This contains settings needed to remotely connect to the machine to perform a remote assessment.
    /// </summary>
    public class ConnectionAttributes
    {

        /// <value>Property <c>Type</c> Type of connection. Example: ssh, pim, db</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <value>Property <c>ConnectionString</c> The login username</value>
        [JsonProperty("connectionString")]
        public string? ConnectionString { get; set; }

        /// <value>Property <c>Username</c> The login username </value>
        [JsonProperty("username")]
        public string? Username { get; set; }

        /// <value>Property <c>Password</c> The login password </value>
        [JsonProperty("password")]
        public string? Password { get; set; }

        /// <value>Property <c>PrivilegedPassword</c> Some Unix like machines have a second password </value>
        [JsonProperty("privilegedPassword")]
        public string? PrivilegedPassword { get; set; }

        /// <value>Property <c>Port</c> The remote port to connect to.</value>
        [JsonProperty("port")]
        public int? Port { get; set; } = null;

        /// <value>Property <c>SshKey</c> SSH key, if applicable </value>
        [JsonProperty("sshKey")]
        public string? SshKey { get; set; }

        /// <value>Property <c>SshPassphrase</c> SSH passphrase for the ssh key, if applicable </value>
        [JsonProperty("sshPassphrase")]
        public string? SshPassphrase { get; set; }

        /// <value>Property <c>Attributes</c> Any additional properties needed to establish the connection </value>
        [JsonProperty("attributes")]
        public Dictionary<string, string>? Attributes { get; set; }

    }
}
