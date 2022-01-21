using BPSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    [Serializable]
    public struct SCell
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public bool IsFree
        {
            get
            { return (Color == PieceColor.None); }
        }

        public PieceColor Color
        { get; set; }

        // Constructors
        public SCell(int row, int col, PieceColor color)
        {
            Row = row;
            Col = col;
            Color = color;
        }

        public SCell(int row, int col)
        {
            Row = row;
            Col = col;
            Color = PieceColor.None;
        }

        public SCell(SCell cell)
        {
            Row = cell.Row;
            Col = cell.Col;
            Color = cell.Color;
        }


    }
}
