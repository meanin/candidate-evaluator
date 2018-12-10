using CandidateEvaluator.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Queries
{
    public class GetCategory : QueryBase, IQuery<Category>
    {
        public Guid Id { get; set; }
    }
}
