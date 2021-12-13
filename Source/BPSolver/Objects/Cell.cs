using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;


namespace BPSolver.Objects
{
    [Serializable]
    public class Cell
    {
       
        // Eliminar TEST
        //public int ID { get; set; }
        // Row
        public int Row { get; set; }
        // Col
        public int Col { get; set; }
        public bool IsFree
        {
            get
            {
                return (Color == PieceColor.None);
                //if (Color == PieceColor.None)
                //    return true;
                //else
                //    return false;
            }

        }

        public PieceColor Color
        {
            get; set;
        }

        // Constructor
        //public Cell(int id, int row, int col)
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;

            Color = PieceColor.None;
        }

    }
}
