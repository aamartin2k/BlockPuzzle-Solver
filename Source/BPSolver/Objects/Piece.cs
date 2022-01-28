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
        
        // Relative coords of squares forming the shape.
        public List<Coord> Matrix { get;  private set; }

        // Constructor
        public Piece(PieceName name, PieceColor color, List<Coord> matrix)
        {
            Name = name;
            Color = color;
            Matrix = matrix;
        }

        //  Returns a list or real locations.
        public static List<Coord> GetRealCoords(PieceName name, Coord point)
        {
            Piece instance = PieceSet.GetPiece(name);

            return GetRealCoords(instance, point);
        }

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


        // Relative coords of neighbor cells.
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

        // Real location of neighbor cells, removes coords outside board limits.
        public static List<Coord> GetNeighborsRealCoords(Piece instance, Coord point)
        {
            // Matriz Coord vecinos.
            List<Coord> ngbMatrix;
            ngbMatrix = Piece.GetNeighborsMatrix(instance);

            // Coord Reales vecinos.
            List<Coord> realCoords;
            realCoords = Piece.GetNeighborsRealCoords(point, ngbMatrix);
            return realCoords;
        }

        public static List<Coord> GetNeighborsRealCoords(Coord point, List<Coord> matrix)
        {
            Coord newCoord;
            List<Coord> realCoords = new List<Coord>();

            foreach (Coord coord in matrix)
            {
                // Create real coord list.
                newCoord = point + coord;
                realCoords.Add(newCoord);
            }

            // Create list of coords outside board limits.
            var outBoard = realCoords.Where(c => (c.Row < 0) || (c.Row > Constants.BoardSize - 1) ||
                                                 (c.Col < 0) || (c.Col > Constants.BoardSize - 1));
            // Remove invalid locations from list.
            return realCoords.Except(outBoard).ToList();
        }

        

       
        public override string ToString()
        {
            return Name.ToString();
        }

    }
}
