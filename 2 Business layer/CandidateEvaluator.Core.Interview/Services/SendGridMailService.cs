using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Configuration;
using CandidateEvaluator.Contract.Interview.Factories;
using CandidateEvaluator.Contract.Interview.Models;
using CandidateEvaluator.Contract.Interview.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CandidateEvaluator.Core.Interview.Services
{
    public class SendGridMailService : IMailService
    {
        private readonly SendGridClient _client;
        private readonly List<EmailAddress> _recipients;
        private readonly IMailContentServiceFactory _mailContentServiceFactory;

        public SendGridMailService(MailOptions options, IMailContentServiceFactory mailContentServiceFactory)
        {
            _mailContentServiceFactory = mailContentServiceFactory;
            _client = new SendGridClient(options.ApiKey);
            _recipients = options.Recipients.Select(r => new EmailAddress(r.Key, r.Value)).ToList();
        }

        public async Task SendInterviewResultReport(InterviewResult result, Guid userId)
        {
            var mailContent = await _mailContentServiceFactory.GetService(userId).ToMailContent(result);
            var msg = new SendGridMessage
            {
                From = new EmailAddress("candidateevaluator@noreply.com", "Candidate Evaluator"),
                Subject = $"Interview result with {result.CandidateName} from {result.InterviewDate}",
                Contents = new List<Content>
                {
                    new Content(mailContent.Type, mailContent.Content)
                }
            };
            msg.AddTos(_recipients);

            await _client.SendEmailAsync(msg);
        }
    }
}
