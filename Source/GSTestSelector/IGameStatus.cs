using BPSolver.Enums;
using System.Collections.Generic;

namespace GSTestSelector
{
    interface IGameStatus
    {
        Dictionary<int, PieceName> NextPieces { get; set; }
        IEnumerable<ICell> Cells { get; set; }


    }
}
