using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class NextPieceDeletionState : BaseState, IGuiState
    {

        public Action<int> Out_DeleteNextPiece { get; set; }

        public NextPieceDeletionState(StMachContext context, CommandAction action) : base(context, action)
        { 
            //Console.WriteLine("NextPieceDeletionState created");
        }



        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            //base.NextPieceImageClicked(index);

            // OUTPUT Delete Piece on Board[position]
            Out_DeleteNextPiece(index);
        }

        public override void GridCellClicked(Coord position)
        {
            Context.CurrentState = Context.GridCellDeletionState;
            Context.CurrentState.GridCellClicked(position);
            
        }

        public override void PieceButtonClicked(PieceName piece)
        {
            // State changes
            Context.CurrentState = Context.PieceSettingState;
            Context.CurrentState.PieceButtonClicked(piece);
        }
    }
}
