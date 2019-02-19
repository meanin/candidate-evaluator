using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Services;
using SendGrid;

namespace CandidateEvaluator.Core.Services
{
    public class YamlMailContentService : IInterviewResultMailContentService
    {
        public Task<(string Type, string Content)> ToMailContent(InterviewResult result)
        {
            return Task.FromResult((MimeType.Text, new YamlDotNet.Serialization.Serializer().Serialize(result.Content)));
        }
    }
}
