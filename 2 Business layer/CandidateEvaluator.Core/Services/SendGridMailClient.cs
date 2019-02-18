using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;

namespace CandidateEvaluator.Core.Services
{
    public class SendGridMailClient : IMailClient
    {
        private readonly SendGridClient _client;
        private readonly List<EmailAddress> _recipients;

        public SendGridMailClient(MailOptions options)
        {
            _client = new SendGridClient(options.ApiKey);
            _recipients = options.Recipients.Select(r => new EmailAddress(r.Key, r.Value)).ToList();
        }

        public Task SendInterviewResultReport(InterviewResult result)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress("candidateevaluator@noreply.com", "Candidate Evaluator"),
                Subject = $"Interview result with {result.CandidateName} from {result.InterviewDate}",
                Contents = new List<Content>
                {
                    new PlainTextContent(new YamlDotNet.Serialization.Serializer().Serialize(result.Content))
                }
            };
            msg.AddTos(_recipients);

            return _client.SendEmailAsync(msg);
        }
    }
}
