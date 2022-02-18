using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    /// <summary>
    /// Implement finding solutions.
    /// Outputs for notifying IOHandler.
    /// </summary>
    internal partial class SolHandler : ISolver
    {
        #region Declaration of Delegates

        public Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }
        #endregion

        #region Invocation of Delegates

        private void OnOut_UpdateSolutionBoard(SolutionMetaStatus result)
        {
            Out_UpdateSolutionBoard?.Invoke(result);
        }
        #endregion

    }
}
