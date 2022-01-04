using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class SolHandler : ISolver
    {


        #region Salidas
        #region Declaracion de Delegates
        public Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }
        #endregion
        #region Invocacion de Delegates
        private void OnOut_UpdateSolutionBoard(SolutionMetaStatus result)
        {
            Out_UpdateSolutionBoard?.Invoke(result);
        }
        #endregion
        #endregion

    }
}
