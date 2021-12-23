using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;
using BPSolver.Solver;

namespace BPSolver.Objects
{
    public struct Piece
    {
        public string Name { get; private set; }
        public PieceColor Color { get; private set; }
        // Block Count for the piece, implemented
        public int Count
        {   get  { return Matrix.Count ; }  }
        public PieceAttitude Attitude { get; private set; }
        // Geometric description, relative coords of cells
        public List<Coord> Matrix { get;  private set; }

        
        // Constructor
        public Piece(string name, PieceColor color, PieceAttitude att, List<Coord> matrix)
        {
            Name = name;
            Color = color;
            Attitude = att;
            Matrix = matrix;
        }

        //  Metodos publicos
        // Static, comunes a toda la clase
        // Coord Reales de la pieza insertada en board
        //  combina (suma) Matrix y Pto de Insercion
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


        //  Matriz de coord relativas de celdas vecinas
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

        // Crear coord absolutasde celdas vecinas a partir del pto de insercion y la lista de coord relativas de la pieza
        // Descarta las coord fuera del tablero.
        // Combina NeighborsMatrix y Pto de Insercion
        public static List<Coord> GetNeighborsRealCoords(Coord insertCoord, List<Coord> matrix)
        {
            Coord newCoord;
            List<Coord> realCoords = new List<Coord>();

            foreach (Coord coord in matrix)
            {
                newCoord = insertCoord + coord;
                realCoords.Add(newCoord);
            }

            var outBoard = realCoords.Where(c => (c.Row < 0) || (c.Row > Constants.Rank - 1) ||
                                                 (c.Col < 0) || (c.Col > Constants.Rank - 1));

            return realCoords.Except(outBoard).ToList();
        }

        

       
        public override string ToString()
        {
            return Name;
        }

    }
}
