using Signals.Library.Communication.Models;
using Signals.Library.Models;

namespace Signals.Library.Communication
{
    /// <summary>
    /// Abstraction layer for communication
    /// </summary>
    public interface ICommunication
    {
        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns></returns>
        public Task<string> GetAccessToken();
        /// <summary>
        /// Submits the signal.
        /// </summary>
        /// <param name="signal">The signal.</param>
        /// <returns></returns>
        public Task<SignalResponse> SubmitSignal(Signal signal);
        /// <summary>
        /// Submits multiple signals in zip.
        /// </summary>
        /// <param name="zipFilePath">The zip file path.</param>
        /// <returns></returns>
        public Task<SignalResponse> SubmitSignalZip(string zipFilePath);

    }
}
