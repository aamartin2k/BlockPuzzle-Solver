using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class SelectionState : BaseState, IGuiState
    {
        

        public SelectionState(StMachContext context, CommandAction action) : base(context, action)
        {
            //Console.WriteLine("SelectionState created");
        }


        public override void PieceButtonClicked(PieceName piece)
        {
            // Debug
            //base.PieceButtonClicked(piece);

            // Set Current Piece
            Context.CurrentPiece = piece;
            // State changes
            Context.CurrentState = Context.PieceSettingState;

        }

        public override void NextPieceImageClicked(int index, PieceName piece)
        {
            // All three pieces must be present
            

            // State changes
            Context.CurrentState = Context.NextPiecePlayState;
            // CurrentPiece changes
            Context.CurrentPiece = piece;
            // Guardar indice 
            Context.NextPieceIndex = index;
        }

       
 
       

       
    }
}
