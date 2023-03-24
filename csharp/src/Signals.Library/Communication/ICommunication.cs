using Signals.Library.Communication.Models;
using Signals.Library.Models;

namespace Signals.Library.Communication
{
    public interface ICommunication
    {
        public Task<string> GetAccessToken();
        public Task<SignalResponse> SubmitSignal(Signal signal);
        public Task SubmitSignalZip(string zipFilePath);

    }
}
