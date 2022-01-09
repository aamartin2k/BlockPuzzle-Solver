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
        #region Declaracion de Delegates
        internal Action<GameStatus> Out_Solution { get; set; }
        #endregion
        #region Invocacion de Delegates
        internal void OnOut_Solution( )
        {
            //_stopWatch = new System.Diagnostics.Stopwatch();
            //_stopWatch.Start();

            Out_Solution?.Invoke(_GameHandler.CurrentStatus);
        }
        #endregion
        #endregion

        #region Entradas de Componente
        internal void In_UpdateSolutionBoard(SolutionMetaStatus result)
        {
            //_stopWatch.Stop();
            //result.TiempoSolucion = _stopWatch.Elapsed.ToString();

            OnOut_UpdateSolutionBoard(result);
        }
        #endregion
    }
}
