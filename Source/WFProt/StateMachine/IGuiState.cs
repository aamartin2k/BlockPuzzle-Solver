
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    interface IGuiState
    {
        
        void ActionSelectClicked();
        void ActionDeleteClicked();
        void PieceButtonClicked(PieceName piece);
        void NextPieceImageClicked(int index, PieceName piece = PieceName.None);
        void GridCellClicked(Coord position);

        void MouseEnterGameCell(Coord position);
        void MouseLeaveGameCell();
    }
}
