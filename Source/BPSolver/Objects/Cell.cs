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
       
        // Declaraciones
       
        // Row
        public int Row { get; set; }
        // Col
        public int Col { get; set; }
        public bool IsFree
        {
            get
            {
                return (Color == PieceColor.None);
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

        //Retorna una Coord con la posicion de la Cell
        public Coord Coord
        {
            get
            {
                return new Coord(this.Row, this.Col);
            }
        }
    }
}
