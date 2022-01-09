using System;
using BPSolver.Objects;

namespace BPSolver
{
    /// <summary>
    /// Defines behavior of solution handling component (SolHandler)
    /// </summary>
    public interface ISolver
    {
        #region Entradas
        // Analizar soluciones a partir de estado actual
        void In_Solution(GameStatus game);
        #endregion

        #region Salidas
        Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }
        #endregion
    }
}
