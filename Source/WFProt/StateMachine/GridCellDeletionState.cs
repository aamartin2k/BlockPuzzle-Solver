using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class GridCellDeletionState : BaseState, IGuiState

    {
        public Action<Coord> Out_DeleteGridCell { get; set; }
        

        public GridCellDeletionState(StContext context, CommandAction action) : base(context, action)
        {
            //Console.WriteLine("GridCellDeletionState created");
        }

        public override void GridCellClicked(Coord position)
        {
            //base.GridCellClicked(position);

            // OUTPUT Delete Piece on Board[position]
            Out_DeleteGridCell(position);
        }

        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            Context.CurrentState = Context.NextPieceDeletionState;
            Context.CurrentState.NextPieceImageClicked(index, piece);
        }

        public override void PieceButtonClicked(PieceName piece)
        {
            // State changes
            Context.CurrentState = Context.PieceSettingState;
            Context.CurrentState.PieceButtonClicked(piece);
        }
    }

}
