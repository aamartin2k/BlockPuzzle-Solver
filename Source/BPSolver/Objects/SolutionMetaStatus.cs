using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    public class SolutionMetaStatus
    {
        public List<Solution> Solutions { get; private set; }

        public SolutionMetaStatus(List<Solution> solutions)
        {
            Solutions = solutions;
        }

    }
}
