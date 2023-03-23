using Newtonsoft.Json;

namespace Signals.Library.Communication.Models
{
    public class AuthResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
       
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("expiresIn")]
        public string ExpiresIn { get; set; }
    }
}
