using Newtonsoft.Json;
using System.Collections.Generic;

namespace Signals.Library.Models
{
    public class ConnectionAttributes
    {
       
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("connectionString")]
        public string? ConnectionString { get; set; }

        [JsonProperty("username")]
        public string? username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("privilegedPassword")]
        public string? PrivilegedPassword { get; set; }

        [JsonProperty("port")]
        public int? Port { get; set; } = null;

        [JsonProperty("sshKey")]
        public string? SshKey { get; set; }

        [JsonProperty("sshPassphrase")]
        public string? SshPassphrase { get; set; }

        [JsonProperty("attributes")]
        public Dictionary<string, string>? Attributes { get; set; }

    }
}
