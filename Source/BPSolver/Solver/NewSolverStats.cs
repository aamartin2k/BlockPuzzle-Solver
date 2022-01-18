using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Solver
{
    internal partial class NewSolver
    {

        public void UpdateGameStats(GameSimpleStatus status)
        {

            status.CellsCount = CellsCount(status.Cells);
            status.FreeCells = FreeCellsCount(status.Cells); 
            status.OccupiedCells = OccupiedCellsCount(status.Cells);
            //status.CompletedRows = CompletedRowsCount;
            //status.CompletedColumns = CompletedColumnsCount;

        }


        // Metodos privados
        private  int CellsCount(List<Cell> Cells)
        {
             return Cells.Count(); 
        }

        private int FreeCellsCount(List<Cell> Cells)
        {
             return Cells.Count(x => x.IsFree); 
        }

        private int OccupiedCellsCount(List<Cell> Cells)
        {
            return Cells.Count(x => !x.IsFree); 
        }


    }
}
