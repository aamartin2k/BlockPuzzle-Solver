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

        // Propiedades del juego
        public int CompletedRows;
        public int CompletedColumns;

        public Dictionary<int, PieceName> NextPiecesA ;
        
        //public bool[,] CellsA;
        //PieceColor
        public PieceColor[,] CellsA;

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

    }
}
