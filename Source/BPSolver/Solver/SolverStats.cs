using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Solver
{
    public partial class Solver
    {

        public void UpdateGameStats(GameStatus status)
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

        public int FreeCellsCount(List<Cell> Cells)
        {
             return Cells.Count(x => x.IsFree); 
        }

        private int OccupiedCellsCount(List<Cell> Cells)
        {
            return Cells.Count(x => !x.IsFree); 
        }


    }
}
