using System;
using System.Collections.Generic;
using System.Linq;
using BPSolver.Enums;


namespace BPSolver.Objects
{

    [Serializable]
    public class GameStatus
    {
        // Propiedades para Game Handler

        // Propiedades para Solver
        public Movement Movement { get; set; }
        public Eval Evaluation { get; set; }


        // Propiedades para TreeHandler
        public int Id { get; set; }
        
        public string Nombre { get; set; }

        // Constructor para asignar Id
        public GameStatus(int id)
        {
            Id = id;
        }

        // Propiedades del juego que se van a guardar el archivo o enviar a Solver  
        //
        // Lista de piezas para posibles movimientos
         public Dictionary<int, PieceName> NextPieces  { get; set; }


        // Stats
        public int FreeCells { get; set; }
        public int OccupiedCells { get; set; }
        public int CellsCount { get; set; }
        public int CompletedRows { get; set; }
        public int CompletedColumns { get; set; }

        // Cells collection
        public List<Cell> Cells { get; set; }

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
