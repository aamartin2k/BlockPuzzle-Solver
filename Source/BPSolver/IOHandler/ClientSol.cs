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
        #region Entradas de Cliente
        public void In_Solution()
        {
            OnOut_Solution();
        }
        #endregion

        #region Salidas al Cliente
        #region Declaracion de Delegates
        public Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }

        #endregion
        #region Invocacion de Delegates
        public void OnOut_UpdateSolutionBoard(SolutionMetaStatus status)
        {
            Out_UpdateSolutionBoard?.Invoke(status);
        }

        #endregion
        #endregion
    }
}
