using BPSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    public class SimpleCell
    {
        // Declaraciones

        public int Row {get; set;}
        public int Col { get; set; }

        public bool IsFree
        {   get
            {
                return (Color == PieceColor.None);
            }
        }

        public PieceColor Color { get; set; }

        // Constructors
        // Copy from SimpleCell
        public SimpleCell(SimpleCell cell)
        {
            Row = cell.Row;
            Col = cell.Col;
            Color = cell.Color;
        }

        // Copy from Cell
        public SimpleCell(Cell cell)
        {
            Row = cell.Row;
            Col = cell.Col;
            Color = cell.Color;
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
