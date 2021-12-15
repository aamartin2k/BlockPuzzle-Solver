using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class NextPiecePlayState : BaseState, IGuiState
    {
        
        public Action<Coord, PieceName,int> Out_DrawGridPlay;

        public NextPiecePlayState(StContext context, CommandAction action) : base(context, action)
        {
            Console.WriteLine("NextPiecePlayState created");
        }

       
        public override void GridCellClicked(Coord position)
        {
            base.GridCellClicked(position);

            // Execute play
            Out_DrawGridPlay(position, Context.CurrentPiece, Context.NextPieceIndex);


            // reset CurrentPiece
            Context.CurrentPiece = PieceName.None;
            // State changes
            Context.CurrentState = Context.SelectionState;
            
        }

        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            // CurrentPiece changes
            Context.CurrentPiece = piece;
            // Guardar indice 
            Context.NextPieceIndex = index;
        }


    }

}
