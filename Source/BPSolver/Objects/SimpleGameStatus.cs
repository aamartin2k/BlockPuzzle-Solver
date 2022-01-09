using BPSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    public class SimpleGameStatus
    {
        // Propiedades para Solver
        public Movement Movement;
        public Eval Evaluation;

        // Propiedades para TreeHandler
        public int Id;
        public string Nombre;

        // Lista de piezas para posibles movimientos
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
