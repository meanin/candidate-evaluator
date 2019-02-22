using System.Threading.Tasks;
using CandidateEvaluator.Contract.Interview.Models;
using CandidateEvaluator.Contract.Interview.Services;
using SendGrid;

namespace CandidateEvaluator.Core.Interview.Services
{
    public class YamlMailContentService : IInterviewResultMailContentService
    {
        public Task<(string Type, string Content)> ToMailContent(InterviewResult result)
        {
            return Task.FromResult((MimeType.Text, new YamlDotNet.Serialization.Serializer().Serialize(result.Content)));
        }
    }
}
