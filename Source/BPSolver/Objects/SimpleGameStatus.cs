using BPSolver.Enums;
using System.Collections.Generic;

namespace BPSolver.Objects
{
    public class SimpleGameStatus
    {
        // Properties  for Solver
        public Movement Movement;
        public Eval Evaluation;

        // Properties  for TreeHandler
        public int Id;
        public string Nombre;

        // List of pieces to play (make moves)
        public Dictionary<int, PieceName> NextPiecesA;

        // Cells collection
        public PieceColor[,] CellsA;

        // Cells Indexer
        // Return Cell by row and column
        public PieceColor this[int row, int col]
        {
            get
            {
                return CellsA[row, col];
            }
            set
            {
                CellsA[row, col] = value;
            }
        }
        // Return Cell by Coord
        public PieceColor this[Coord coord]
        {
            get
            {
                return CellsA[coord.Row, coord.Col];
            }
            set
            {
                CellsA[coord.Row, coord.Col] = value;
            }
        }


        // Stats
        public int CompletedRows;
        public int CompletedColumns;

        public SimpleGameStatus()
        { }

    }
}
