using System;
using BPSolver.Enums;
using BPSolver.Objects;
namespace WFProt
{
    class DeletionState : BaseState, IGuiState
    {
       
        public DeletionState(StContext context) : base(context)
        {
            Console.WriteLine("DeletionState created");
        }

        
        public override void GridCellClicked(Coord position)
        {
            //new, Change state and handle
            base.GridCellClicked(position);

            Context.CurrentState = Context.GridCellDeletionState;
            Context.CurrentState.GridCellClicked(position);
        }

        public override void NextPieceImageClicked( int index, PieceName piece)
        {
            base.NextPieceImageClicked(index, piece);

            // State changes
            Context.CurrentState = Context.NextPieceDeletionState;
            Context.CurrentState.NextPieceImageClicked(index, piece);

        }

        public override void PieceButtonClicked(PieceName piece)
        {  // override to do nothing 
            Console.WriteLine("PieceButton Clicked in DeletionState, do nothing ");
        }
    }

}
