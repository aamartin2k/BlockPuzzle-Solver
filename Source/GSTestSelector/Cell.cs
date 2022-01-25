using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;

namespace GSTestSelector
{
    class CCell : ICell
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public CCell(int row, int col)
        {
            Row = row;
            Col = col;
            Color = PieceColor.None;
        }

        public bool IsFree
        {
            get
            { return (Color == PieceColor.None); }
        }

        public PieceColor Color
        { get; set; }
    }

    struct SCell : ICell
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public SCell(int row, int col)
        {
            Row = row;
            Col = col;
            Color = PieceColor.None;
        }

        public bool IsFree
        {
            get
            { return (Color == PieceColor.None); }
        }

        public PieceColor Color
        { get; set; }
    }
}
