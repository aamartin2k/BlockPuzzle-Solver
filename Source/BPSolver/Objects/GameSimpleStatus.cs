using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;


namespace BPSolver.Objects
{ 

    [Serializable]
    public class GameSimpleStatus
    {
        // Propiedades del juego que se van a guardar el archivo o enviar a Solver  
        //
        public int CantMoves;

        public bool AnyCompleted;
        //public List<int> RowsCompletedIndex;
        //public List<int> ColumnsCompletedIndex;
        public List<PieceName> NextPieces;

        // Stats
        public int FreeCells;
        public int OccupiedCells;
        public int CellsCount;
        public int CompletedRows;
        public int CompletedColumns;

        // Cells collection
        public List<Cell> Cells;

        // Cells Indexer
        // Recupera Cell por fila y columna
        public Cell this[int row, int col]
        {
            get { return Cells.First(z => z.Row == row && z.Col == col); }
        }

        public Cell this[Coord coord]
        {
            get { return Cells.First(z => z.Row == coord.Row && z.Col == coord.Col); }
        }




    }

}
