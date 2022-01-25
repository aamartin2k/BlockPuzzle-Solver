using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Comunicacion con el Componente SolHandler

        //private  Stopwatch _stopWatch  ;

        #region Salidas
        #region Declaration of Delegates
        internal Action<GameStatus> Out_Solution { get; set; }
        internal Action Out_SelectRecursive { get; set; }
        internal Action Out_SelectIterative { get; set; }

        #endregion
        #region Invocation of Delegates
        internal void OnOut_Solution( )
        {
            Out_Solution?.Invoke(_GameHandler.CurrentStatus);
        }
        internal void OnOut_SelectRecursive()
        {
            Out_SelectRecursive?.Invoke();
        }
        internal void OnOut_SelectIterative()
        {
            Out_SelectIterative?.Invoke();
        }
        #endregion
        #endregion

        #region Inputs de Componente
        internal void In_UpdateSolutionBoard(SolutionMetaStatus result)
        {
            OnOut_UpdateSolutionBoard(result);
        }
        #endregion
    }
}
