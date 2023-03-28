using Newtonsoft.Json;

namespace Signals.Library.Models
{
    /// <summary>
    ///   The kill-chain-phase represents a phase in a kill chain, which describes the various phases an attacker may undertake
    ///  in order to achieve their objectives.
    /// </summary>
    public class KillChainPhase
    {

        /// <value>
        /// The name of the chain.
        /// </value>
        /// <example>
        /// lockheed-martin-cyber-kill-chain, mitre-attack
        /// </example>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <value>
        /// The phase.
        /// </value>
        /// <example>
        /// reconnaissance, credential-access
        /// </example>
        [JsonProperty("phase")]
        public string Phase { get; set; }
    }
}
