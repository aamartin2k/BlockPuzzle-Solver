using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class GridCellDrawingState : BaseState, IGuiState
    {
        
        // output
        public Action<Coord> Out_DrawPreview;
        public Action Out_DeletePreview;
        public Action<Coord, PieceName> Out_DrawGrid;


        public GridCellDrawingState(StMachContext context, CommandAction action) : base(context, action)
        {
            //Console.WriteLine("GridCellDrawingState created");
        }


        public override void GridCellClicked(Coord position)
        {
            //base.GridCellClicked(position);
            Out_DeletePreview();
            Out_DrawGrid(position, Context.CurrentPiece);
        }

        public override void PieceButtonClicked(PieceName piece)
        {
            // State changes
            Context.CurrentState = Context.PieceSettingState;
            Context.CurrentState.PieceButtonClicked(piece);
            
        }

        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            base.NextPieceImageClicked(index, piece);
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
