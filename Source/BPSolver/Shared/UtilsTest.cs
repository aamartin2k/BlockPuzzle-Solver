using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver
{
    public static partial class Utils
    {

        #region Test before draw a piece

        #region Public
        static public bool TestPiece(Coord insertCoord, PieceName name, Board cells)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = PieceSet.GetPiece(name);

            // Create absolute coords list.
            realCoords = Piece.GetRealCoords(piece, insertCoord);

            // Test if all coords are within limits.
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // Test if all coords are free
            ret = TestFreeCells(cells, realCoords);

            return ret;
        }


        static internal bool TestCoords(List<Coord> coors,  GameStatus gstat)
        {
            // Test if all coords are within limits.
            bool ret = TestRealCoords(coors);
            if (!ret)
                return false;

            // Test if all coords are free
            ret = TestFreeCells(gstat, coors);

            return ret;
        }

        static internal bool TestPiece(Coord insertCoord, PieceName name, GameStatus gstat)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = PieceSet.GetPiece(name);

            // Create absolute coords list.
            realCoords = Piece.GetRealCoords(piece, insertCoord);

            // Test if all coords are within limits.
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // Test if all coords are free
            ret = TestFreeCells(gstat, realCoords);

            return ret;
        }



        #endregion

        #region Private

        // private
        static internal bool TestRealCoords(List<Coord> matrix)
        {
            var outRange = matrix.Where(coord => (coord.Row < 0) || (coord.Row > Constants.BoardSize - 1) ||
                                                 (coord.Col < 0) || (coord.Col > Constants.BoardSize - 1)).Count();
            return outRange == 0;

        }

        static private bool TestFreeCells(GameStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game.Cells[c].IsFree).Where(x => x == false).Count();
            return ex == 0;
        }

        static private bool TestFreeCells(Board game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game[c].IsFree).Where(x => x == false).Count();
            return ex == 0;
        }

        #endregion

        #endregion
    }
}
