using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class NextPieceDeletionState : BaseState, IGuiState
    {

        public Action<int> Out_DeleteNextPiece { get; set; }

        public NextPieceDeletionState(StContext context) : base(context)
        {
            Console.WriteLine("NextPieceDeletionState created");
        }



        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            base.NextPieceImageClicked(index);

            // OUTPUT Delete Piece on Board[position]
            Out_DeleteNextPiece(index);
        }

       
    }
}
