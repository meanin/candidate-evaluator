using System.Collections.Generic;

namespace CandidateEvaluator.Contract.Configuration
{
    public class MailOptions
    {
        // TODO: user configuration :o each user should be able to configure its own recipient list
        public Dictionary<string, string> Recipients { get; set; }
        public string ApiKey { get; set; }
    }
}
