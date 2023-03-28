using Newtonsoft.Json;

namespace Signals.Library.Models
{

/// <summary>
///  A Campaign is a grouping of adversarial behaviors that describes a set of malicious activities or attacks (sometimes called waves)
/// that occur over a period of time against a specific set of targets.Campaigns usually have well defined
/// objectives and may be part of an Intrusion Set.
/// Campaigns are often attributed to an intrusion set and threat actors.The threat actors may reuse known infrastructure
/// from the intrusion set or may set up new infrastructure specific for conducting that campaign.
/// Campaigns can be characterized by their objectives and the incidents they cause, people or resources they target, and the resources(infrastructure, intelligence, Malware, Tools, etc.) they use.
/// For example, a Campaign could be used to describe a crime syndicate's attack using a specific variant of malware and new
/// C2 servers against the executives of ACME Bank during the summer of 2016 in order to gain secret information about an upcoming merger with another bank.
/// </summary>
    public class Campaign
    {
        /// <value>Property <c>Name</c>  A name used to identify the Campaign.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <value>Property <c>Aliases</c> Alternative names used to identify this Campaign.</value>
        [JsonProperty("aliases")]
        public List<string> Aliases { get; set; }

        /// <value>Property <c>Objective</c> The Campaign’s primary goal, objective, desired outcome, or intended effect — what the Threat Actor or Intrusion Set hopes to accomplish with this Campaign.</value>
        [JsonProperty("objective")]
        public string Objective { get; set; }

        /// <value>Property <c>Description</c> A description that provides more details and context about the Campaign, potentially including its purpose and its key characteristics.</value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <value>Property <c>FirstSeen</c> The time that this Campaign was first seen.</value>
        [JsonProperty("firstSeen")]
        public DateTime FirstSeen { get; set; }

        /// <value>Property <c>LastSeen</c> The time that this Campaign was last seen.</value>
        [JsonProperty("lastSeen")]
        public DateTime LastSeen { get; set; }
    }
}
