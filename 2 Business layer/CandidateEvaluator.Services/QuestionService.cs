using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Repositories;
using CandidateEvaluator.Contract.Services;
using System;
using System.Collections.Generic;
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

        public async Task<List<Question>> GetAll(Guid ownerId)
        {
            return await _modelRepository.GetAll(ownerId);
        }

        public async Task<Question> Get(Guid ownerId, Guid categoryId, Guid questionId)
        {
            return await _modelRepository.Get(ownerId, categoryId, questionId);
        }

        public async Task<List<Question>> GetAllFromCategory(Guid ownerId, Guid categoryId)
        {
            return await _modelRepository.GetAllFromPartition(ownerId, categoryId);
        }
    }
}
