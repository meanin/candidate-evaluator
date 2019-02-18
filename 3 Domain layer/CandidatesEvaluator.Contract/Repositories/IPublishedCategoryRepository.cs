using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateEvaluator.Contract.Models;

namespace CandidateEvaluator.Contract.Repositories
{
    public interface IPublishedCategoryRepository
    {
        Task<Guid> Add(PublishedCategory model);
        Task<IEnumerable<PublishedCategory>> GetAll();
        Task<PublishedCategory> Get(Guid id);
        Task<Guid> Update(PublishedCategory model);
        Task Delete(Guid id);
    }
}
