using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class NextPiecePlayState : BaseState, IGuiState
    {
        // output
        public Action<Coord> Out_DrawPreview;
        public Action Out_DeletePreview;
        public Action<Coord, PieceName,int> Out_DrawGridPlay;

        public NextPiecePlayState(StMachContext context, CommandAction action) : base(context, action)
        {
            //Console.WriteLine("NextPiecePlayState created");
        }

       
        public override void GridCellClicked(Coord position)
        {
            //base.GridCellClicked(position);
            Out_DeletePreview();

            // Execute play.
            Out_DrawGridPlay(position, Context.CurrentPiece, Context.NextPieceIndex);

            // Reset CurrentPiece.
            Context.CurrentPiece = PieceName.None;
            // Change State.
            Context.CurrentState = Context.SelectionState;
            
        }

        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            // CurrentPiece changes
            Context.CurrentPiece = piece;
            // Guardar indice 
            Context.NextPieceIndex = index;
        }

        public override void MouseEnterGameCell(Coord position)
        {
            //base.MouseEnterGameCell(position);
            Out_DrawPreview(position);
        }

        public override void MouseLeaveGameCell()
        {
            //base.MouseLeaveGameCell(position);
            Out_DeletePreview();
        }

    }

}
