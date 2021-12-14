using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class GridCellDeletionState : BaseState, IGuiState

    {
        public Action<Coord> Out_DeleteGridCell { get; set; }
        

        public GridCellDeletionState(StContext context) : base(context)
        {
            Console.WriteLine("GridCellDeletionState created");
        }

       

        public override void GridCellClicked(Coord position)
        {
            base.GridCellClicked(position);

            // OUTPUT Delete Piece on Board[position]
            //Console.WriteLine(string.Format("GridCell Borrar Cell Row: {0} Col: {1}", position.Row, position.Column));
            Out_DeleteGridCell(position);
        }
    }

}
