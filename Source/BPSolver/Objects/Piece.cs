using BPSolver.Enums;
using System.Collections.Generic;
using System.Linq;


namespace BPSolver.Objects
{
    public struct Piece
    {
        public PieceName Name { get; private set; }
        public PieceColor Color { get; private set; }
        public int Count
        {   get  { return Matrix.Count ; }  }
        
        // Relative coords of squares
        public List<Coord> Matrix { get;  private set; }

        // Constructor
        public Piece(PieceName name, PieceColor color, List<Coord> matrix)
        {
            Name = name;
            Color = color;
            Matrix = matrix;
        }

        //  Returns a list or real locations
        public static List<Coord> GetRealCoords(Piece instance, Coord point)
        {
            Coord newCoord;
            List<Coord> realCoords = new List<Coord>();

            foreach (var coord in instance.Matrix)
            {
                newCoord = point + coord;
                realCoords.Add(newCoord);
            }

            return realCoords;
        }


        // Relative coords of neighbor cells
        public static List<Coord>  GetNeighborsMatrix(Piece instance)
        {
            List<Coord> allItems = new List<Coord>();

            foreach (var item in instance.Matrix)
            {
                allItems.Add(new Coord(item.Row - 1, item.Col));
                allItems.Add(new Coord(item.Row, item.Col + 1));
                allItems.Add(new Coord(item.Row + 1, item.Col));
                allItems.Add(new Coord(item.Row, item.Col - 1));
            }

            return allItems.Except(instance.Matrix).ToList();
        }

        // Real location of neighbor cells, removes coords outside board limits
        public static List<Coord> GetNeighborsRealCoords(Coord insertCoord, List<Coord> matrix)
        {
            Coord newCoord;
            List<Coord> realCoords = new List<Coord>();

            foreach (Coord coord in matrix)
            {
                // create real coord list
                newCoord = insertCoord + coord;
                realCoords.Add(newCoord);
            }

            // create list of coords outside board limits
            var outBoard = realCoords.Where(c => (c.Row < 0) || (c.Row > Constants.BoardSize - 1) ||
                                                 (c.Col < 0) || (c.Col > Constants.BoardSize - 1));
            // remove invalid locations from list
            return realCoords.Except(outBoard).ToList();
        }

        

       
        public override string ToString()
        {
            return Name.ToString();
        }

    }
}
