using System;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.CQRS.Handlers;
using CandidateEvaluator.Contract.Interview.Commands.InterviewResult;
using CandidateEvaluator.Contract.Interview.Repositories;

namespace CandidateEvaluator.Core.Interview.CommandHandlers.InterviewResult
{
    public class DeleteInterviewResultHandler : ICommandHandler<DeleteInterviewResultCommand>
    {
        private readonly IInterviewResultRepository _modelRepository;

        public DeleteInterviewResultHandler(IInterviewResultRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<Guid> Handle(DeleteInterviewResultCommand command)
        {
            await _modelRepository.Delete(command.OwnerId, command.Id);
            return Guid.Empty;
        }
    }
}
