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
        public string Name { get; private set; }
        public PieceColor Color { get; private set; }
        // Block Count for the piece, implemented
        public int Count
        {   get  { return Matrix.Count ; }  }
        public PieceAttitude Attitude { get; private set; }
        // Geometric description, relative coords
        public List<Coord> Matrix { get;  private set; }

        
        // Constructor
        public Piece(string name, PieceColor color, PieceAttitude att, List<Coord> matrix)
        {
            Name = name;
            Color = color;
            Attitude = att;
            Matrix = matrix;
            //Matrix = new List<Coord>();
        }

        //  Metodos publicos
        // Static, comunes a toda la clase
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

        // Coord Reales, combina Matrix y Pto de Insercion
        public static List<Coord> GetRealMatrix(Piece instance, Coord point)
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

       
        public override string ToString()
        {
            return Name;
        }

    }
}
