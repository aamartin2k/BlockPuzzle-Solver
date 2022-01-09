using System;
using System.Collections.Generic;
using System.Linq;
using BPSolver.Enums;


namespace BPSolver.Objects
{

    [Serializable]
    public class GameStatus
    {
        // Propiedades para Solver
        public Movement Movement { get; set; }
        public Eval Evaluation { get; set; }

        // Propiedades para TreeHandler
        public int Id { get; private set; }
        public string Nombre { get; set; }

        // Constructor
        public GameStatus(int id)
        {
            Id = id;
        }

        // Lista de piezas para posibles movimientos
         public Dictionary<int, PieceName> NextPieces  { get; set; }

        // Cells collection
        public List<Cell> Cells { get; set; }

        // Cells Indexer
        // Return Cell by row and column
        public Cell this[int row, int col]
        {
            get { return Cells.First(z => z.Row == row && z.Col == col); }
        }
        // Return Cell by Coord
        public Cell this[Coord coord]
        {
            get { return Cells.First(z => z.Row == coord.Row && z.Col == coord.Col); }
        }


        // Stats
        public int FreeCells { get; set; }
        public int OccupiedCells { get; set; }
        public int CellsCount { get; set; }
        public int CompletedRows { get; set; }
        public int CompletedColumns { get; set; }




    }

}
