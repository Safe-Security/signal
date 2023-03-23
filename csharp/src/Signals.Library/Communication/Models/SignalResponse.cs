using Newtonsoft.Json;

namespace Signals.Library.Communication.Models
{
    public class SignalResponse
    {
        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("retryIn")]
        public string? RetryIn { get; set; }

    }
}
