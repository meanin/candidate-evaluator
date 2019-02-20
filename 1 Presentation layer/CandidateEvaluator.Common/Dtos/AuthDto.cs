namespace CandidateEvaluator.Common.Dtos
{
    public class AuthDto
    {
        public string Username { get; set; }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }
        public string Code { get; set; }
    }
}
