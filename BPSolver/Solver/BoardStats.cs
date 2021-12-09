using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Solver
{
    public class BoardStats
    {
        //lbFree.Text = st.Board.FreeCells.ToString();
        //    lbOcupp.Text = st.Board.OccupiedCells.ToString();
        //    lbCount.Text = st.Board.CellsCount.ToString();

        public int FreeCells { get; set; }
        public int OccupiedCells { get; set; }
        public int CellsCount { get; set; }
        public int CompletedRows { get; set; }
        public int CompletedColumns { get; set; }
    }
}
