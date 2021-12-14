using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class NextPiecePlayState : BaseState, IGuiState
    {
        
        public Action<Coord, PieceName> Out_DrawGridPlay;

        public NextPiecePlayState(StContext context) : base(context)
        {
            Console.WriteLine("NextPiecePlayState created");
        }

       
        public override void GridCellClicked(Coord position)
        {
            base.GridCellClicked(position);

            
            Out_DrawGridPlay(position, Context.CurrentPiece);

            // reset CurrentPiece
            Context.CurrentPiece = PieceName.None;
            // State changes
            Context.CurrentState = Context.SelectionState;
        }



    }

}
