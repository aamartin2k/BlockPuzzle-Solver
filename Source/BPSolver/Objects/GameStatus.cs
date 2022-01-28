using BPSolver.Enums;
using System;
using System.Collections.Generic;


namespace BPSolver.Objects
{
    /// <summary>
    /// Implement the storage of data describing a particular instance 
    /// in game sequence.
    /// </summary>
    [Serializable]
    public class GameStatus
    {
        // Properties  for Solver
        public Movement Movement { get; set; }
        public Eval Evaluation { get; set; }

        // Properties  for TreeHandler
        public int Id { get; private set; }
        public string Nombre { get; set; }

        // Constructor
        public GameStatus(int id)
        {   Id = id;    }

        // List of pieces to play (make moves)
         public Dictionary<int, PieceName> NextPieces  { get; set; }

        // Cell collection
        public Board Cells { get; set; }

        // Game Stats
        public int FreeCells { get; set; }
        public int OccupiedCells { get; set; }
        public int CellsCount { get; set; }
        public int CompletedRows { get; set; }
        public int CompletedColumns { get; set; }
    }

}
