
using System;

namespace BPSolver.Objects
{
    [Serializable]
    public struct Coord
    {
        public int Row;
        public int Col;

        // Constructor
        public Coord(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public Coord(Coord coord)
        {
            Row = coord.Row;
            Col = coord.Col;
        }

        static public Coord operator +(Coord one, Coord two)
        {
            Coord result = new Coord(one);

            result.Row += two.Row;
            result.Col += two.Col;

            return result;
        }
    }

}