using CandidateEvaluator.Contract.Models;
using CandidateEvaluator.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Queries
{
    public class GetAllCategories : QueryBase, IQuery<List<Category>>
    {
    }
}
