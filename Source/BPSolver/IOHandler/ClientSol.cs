using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Comunicacion con el cliente
        // para funciones de SolHandler
        //
        #region Inputs de Cliente
        public void In_Solution()
        {
            OnOut_Solution();
        }

        // Switch on Solution process
        public void In_SelectRecursive()
        {
            OnOut_SelectRecursive();
        }
        public void In_SelectIterative()
        {
            OnOut_SelectIterative();
        }

        #endregion

        #region Salidas al Cliente
        #region Declaration of Delegates
        public Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }

        #endregion
        #region Invocation of Delegates
        public void OnOut_UpdateSolutionBoard(SolutionMetaStatus status)
        {
            Out_UpdateSolutionBoard?.Invoke(status);
        }

        #endregion
        #endregion
    }
}
