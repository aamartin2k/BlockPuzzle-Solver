using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class GridCellDrawingState : BaseState, IGuiState
    {
        public Action<Coord, PieceName> Out_DrawGrid;
       
        public GridCellDrawingState(StContext context) : base(context)
        {
            Console.WriteLine("GridCellDrawingState created");
        }

        public override void GridCellClicked(Coord position)
        {
            base.GridCellClicked(position);

            Out_DrawGrid(position, Context.CurrentPiece);
        }
    }
}
