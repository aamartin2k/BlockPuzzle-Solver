using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;

namespace BPSolver.Objects
{
    public class Piece
    {
        public string Name { get; set; }
        public PieceColor Color { get; set; }
        // Block Count for the piece, implemented
        public int Count
        {   get  { return Matrix.Count ; }  }
        public PieceAttitude Attitude { get; set; }
        // Geometric description, relative coords
        public List<Coord> Matrix { get;  set; }


        // Constructor
        public Piece(string name, PieceColor color, PieceAttitude att)
        {
            Name = name;
            Color = color;
            Attitude = att;
            Matrix = new List<Coord>();
        }

        //
        public List<Coord> NeighborsMatrix
        {
            get
            {
                
                List<Coord>  allItems = new List<Coord>();

                foreach (var item in Matrix)
                {
                    allItems.Add(new Coord(item.Row -1, item.Col));
                    allItems.Add(new Coord(item.Row, item.Col + 1));
                    allItems.Add(new Coord(item.Row + 1, item.Col));
                    allItems.Add(new Coord(item.Row, item.Col - 1 ));
                }

                return allItems.Except(Matrix).ToList();
            }
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
