using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class PieceSettingState : BaseState, IGuiState
    {
  
        public PieceSettingState(StContext context) : base(context)
        {
            Console.WriteLine("PieceSettingState created");
        }

        public override void PieceButtonClicked(PieceName piece)
        {
            // Debug
            base.PieceButtonClicked(piece);

            // Set Current Piece
            Context.CurrentPiece = piece;
            // State changes
            //Context.CurrentState = Context.PieceSettingState;

        }

        public override void NextPieceImageClicked( int index, PieceName piece = PieceName.None)
        {
            base.NextPieceImageClicked(index, piece);

            Context.CurrentState = Context.NextPieceDrawingState;
            Context.CurrentState.NextPieceImageClicked(index);
        }

       

        public override void GridCellClicked(Coord position)
        {
            base.GridCellClicked(position);

            Context.CurrentState = Context.GridCellDrawingState;
            Context.CurrentState.GridCellClicked(position);
        }
    }

}
