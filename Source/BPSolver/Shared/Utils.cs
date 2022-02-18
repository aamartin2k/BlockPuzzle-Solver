using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public static class Utils
    {
        #region Funciones Utileria Compartidas
        #region Publicas
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

        #region Privadas

        static private bool TestRealCoords(List<Coord> matrix)
        {
            var outRange = matrix.Where(coord => (coord.Row < 0) || (coord.Row > Constants.BoardSize - 1) ||
                                                 (coord.Col < 0) || (coord.Col > Constants.BoardSize - 1)).Count();
            return outRange == 0;

        }

        static private bool TestFreeCells(GameStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game[c].IsFree).Where(x => x == false).Count();
            return ex == 0;
        }

        #endregion

        #endregion
    }
}
