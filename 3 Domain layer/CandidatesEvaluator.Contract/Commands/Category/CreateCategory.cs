using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateEvaluator.Contract.Commands.Category
{
    public class CreateCategory : CommandBase
    {
        public string Name { get; set; }
    }
}
