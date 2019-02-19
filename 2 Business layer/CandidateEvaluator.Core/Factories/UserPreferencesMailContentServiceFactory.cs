using System;
using CandidateEvaluator.Contract.Factories;
using CandidateEvaluator.Contract.Services;
using CandidateEvaluator.Core.Services;
using CandidateEvaluator.Core.Razor.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CandidateEvaluator.Core.Factories
{
    public class UserPreferencesMailContentServiceFactory : IMailContentServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UserPreferencesMailContentServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IInterviewResultMailContentService GetService(Guid userId)
        {
            return new HtmlMailContentService(
                (IRazorViewEngine)_serviceProvider.GetService(typeof(IRazorViewEngine)),
                (ITempDataProvider)_serviceProvider.GetService(typeof(ITempDataProvider)),
                _serviceProvider);
            return new YamlMailContentService();
        }
    }
}
