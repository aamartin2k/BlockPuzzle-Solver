using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class PieceSettingState : BaseState, IGuiState
    {

        // output
        public Action<Coord> Out_DrawPreview;
        public Action Out_DeletePreview;

        public PieceSettingState(StMachContext context, CommandAction action) : base(context, action)
        {
            //Console.WriteLine("PieceSettingState created");
        }

        public override void PieceButtonClicked(PieceName piece)
        {
            // Debug
            //base.PieceButtonClicked(piece);

            // Set Current Piece
            Context.CurrentPiece = piece;
        }

        public override void NextPieceImageClicked( int index, PieceName piece = PieceName.None)
        {
            //base.NextPieceImageClicked(index, piece);

            Context.CurrentState = Context.NextPieceDrawingState;
            Context.CurrentState.NextPieceImageClicked(index);
            // Test Delete
            Out_DeletePreview();
        }

        public override void GridCellClicked(Coord position)
        {
            //base.GridCellClicked(position);

            // Test Delete
            Out_DeletePreview();

            Context.CurrentState = Context.GridCellDrawingState;
            Context.CurrentState.GridCellClicked(position);
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
