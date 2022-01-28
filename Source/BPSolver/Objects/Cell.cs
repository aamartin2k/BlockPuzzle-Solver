using BPSolver.Enums;
using System;


namespace BPSolver.Objects
{
    /// <summary>
    /// Implement a cell in the board.
    /// </summary>
    [Serializable]
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public bool IsFree
        {
            get
            {   return (Color == PieceColor.None);  }
        }

        public PieceColor Color
        {   get; set;   }

        // Constructors
        public Cell(int row, int col, PieceColor color)
        {
            Row = row;
            Col = col;
            Color = color;
        }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
            Color = PieceColor.None;
        }

        public Cell(Cell cell)
        {
            Row = cell.Row;
            Col = cell.Col;
            Color = cell.Color;
        }


    }
}
