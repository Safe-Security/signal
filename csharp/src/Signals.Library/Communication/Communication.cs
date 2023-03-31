using Newtonsoft.Json;
using Signals.Library.Communication.Models;
using Signals.Library.Constants;
using Signals.Library.Models;
using Signals.Library.Utility;
using System.Security;
using System.Text;

namespace Signals.Library.Communication
{
    /// <summary>
    /// Responsible for making connection with SAFE API and submit signals
    /// </summary>
    /// <seealso cref="Signals.Library.Communication.ICommunication" />
    public class Communication : ICommunication
    {
        /// <summary>
        /// Gets the safe URL.
        /// </summary>
        /// <value>
        /// The safe URL.
        /// </value>
        private string SafeUrl { get; }
        /// <summary>
        /// Gets the rest API username.
        /// </summary>
        /// <value>
        /// The rest API username.
        /// </value>
        private string RestApiUsername { get; }
        /// <summary>
        /// Gets the rest API password.
        /// </summary>
        /// <value>
        /// The rest API password.
        /// </value>
        private SecureString RestApiPassword { get; }

        /// <summary>
        /// Gets or sets the authentication API response.
        /// </summary>
        /// <value>
        /// The authentication API response.
        /// </value>
        private AuthResponse? AuthApiResponse { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        private HttpClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Communication"/> class.
        /// </summary>
        /// <param name="safeUrl">The safe URL.</param>
        /// <param name="restApiUserName">Name of the rest API user.</param>
        /// <param name="restApiPassword">The rest API password.</param>
        public Communication(string safeUrl, string restApiUserName, SecureString restApiPassword)
        {
            SafeUrl = safeUrl;
            RestApiUsername = restApiUserName;
            RestApiPassword = restApiPassword;
            Client = new HttpClient();
        }


        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Submits the signal.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns></returns>
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

        public async Task<SignalResponse> SubmitSignalZip(string zipFilePath)
        {
            SignalResponse? signalResponse = null;
            try
            {
                var stream = File.OpenRead(zipFilePath);

                HttpRequestMessage signalRequest =
                    new HttpRequestMessage(HttpMethod.Post, $"{SafeUrl}{ApiEndpoints.ZipSignals}");
                signalRequest.Headers.Add("Authorization", $"Bearer {await GetAccessToken()}");
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(stream),"file",zipFilePath);
                signalRequest.Content = content;

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
    }
}