namespace CandidateEvaluator.Contract.Models
{
    public class AuthTokens
    {
        public string Username { get; set; }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
