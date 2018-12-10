using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;
using System.Threading.Tasks;

namespace CandidateEvaluator.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _modelRepository;
        private IUserRecentActivityRepository _activityRepository;

        private static EntityType ModelType = EntityType.Question;

        public QuestionService(IQuestionRepository questionRepository,
            IUserRecentActivityRepository activityRepository)
        {
            _modelRepository = questionRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Question> Add(Question model)
        {
            var result = await _modelRepository.Create(model);
            await _activityRepository.Upsert(model.OwnerId, new RecentActivity
            {
                Type = ModelType,
                EntityId = result.Id
            });
            return result;
        }
    }
}
