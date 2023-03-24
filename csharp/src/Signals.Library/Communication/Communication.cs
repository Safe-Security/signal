using Newtonsoft.Json;
using Signals.Library.Communication.Models;
using Signals.Library.Constants;
using Signals.Library.Models;
using Signals.Library.Utility;
using System.Security;
using System.Text;

namespace Signals.Library.Communication
{
    public class Communication : ICommunication
    {
        private string SafeUrl { get; }
        private string RestApiUsername { get; }
        private SecureString RestApiPassword { get; }

        private AuthResponse? AuthApiResponse { get; set; }

        private HttpClient Client { get; set; }

        public Communication(string safeUrl, string restApiUserName, SecureString restApiPassword)
        {
            SafeUrl = safeUrl;
            RestApiUsername = restApiUserName;
            RestApiPassword = restApiPassword;
            Client = new HttpClient();
        }


        public async Task<string> GetAccessToken()
        {
            try
            {
                if (AuthApiResponse?.AccessToken != null)
                {
                    return AuthApiResponse.AccessToken;
                }

                HttpRequestMessage authRequest =
                    new HttpRequestMessage(HttpMethod.Post, $"{SafeUrl}{ApiEndpoints.Auth}");
                var byteArray = Encoding.ASCII.GetBytes($"{RestApiUsername}:{Util.ToString(RestApiPassword)}");
                Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = await Client.SendAsync(authRequest);
                response.EnsureSuccessStatusCode();

                var rawAuthResponse = await response.Content.ReadAsStringAsync();
                AuthApiResponse = JsonConvert.DeserializeObject<AuthResponse>(rawAuthResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred-{ex.Message}");

                throw;
            }

            return AuthApiResponse?.AccessToken;
        }

        public async Task<SignalResponse> SubmitSignal(Signal signal)
        {
            SignalResponse? signalResponse = null;
            try
            {
                HttpRequestMessage signalRequest =
                    new HttpRequestMessage(HttpMethod.Post, $"{SafeUrl}{ApiEndpoints.Signals}");
                signalRequest.Headers.Add("Authorization", $"Bearer {await GetAccessToken()}");
                string serialized = JsonConvert.SerializeObject(signal);
                StringContent signalContent = new StringContent(serialized, null, "application/json");
                signalRequest.Content = signalContent;
                var response = await Client.SendAsync(signalRequest);

                response.EnsureSuccessStatusCode();

                var rawResponse = await response.Content.ReadAsStringAsync();
                signalResponse = JsonConvert.DeserializeObject<SignalResponse>(rawResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred-{ex.Message}");
                throw;
            }

            return signalResponse;
        }

        public Task SubmitSignalZip(string zipFilePath)
        {
            throw new NotImplementedException();
        }
    }
}