using BPSolver.Enums;

namespace GSTestSelector
{

    interface ICell
    {
        int Row { get; set; }
        int Col { get; set; }
        bool IsFree { get; }
        PieceColor Color { get; set; }
    }
    
}
