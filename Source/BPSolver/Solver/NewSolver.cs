using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Solver
{
    internal partial class NewSolver
    {
        // sustituir GameSolver

        public bool TestPiece(Coord insertCoord, PieceName name, GameStatus gstat)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = GetPiece(name);

            // Create absolute coords list.
            //realCoords = GetRealCoords(insertCoord, piece.Matrix);
            realCoords = Piece.GetRealMatrix(piece, insertCoord);

            // Test if all coords are within limits.
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // Test if all coords are free
            ret = TestFreeCells(gstat, realCoords);

            return ret;
        }


        
        // Crear coord absolutas a partir del pto de insercion y la lista de coord relativas de la pieza
        public List<Coord> GetRealCoords(Coord insertCoord, List<Coord> matrix)
        {
            Coord newCoord;
            List<Coord> realCoords = new List<Coord>();

            foreach (Coord coord in matrix)
            {
                newCoord = insertCoord + coord;
                realCoords.Add(newCoord);
            }

            return realCoords;
        }


        // Metodos Privados

        // Comprobar que las coordenadas absolutas de la pieza están dentro del tablero
        private bool TestRealCoords(List<Coord> matrix)
        {
            var outRange = matrix.Where(newCoord => (newCoord.Row < 0) || (newCoord.Row > Constants.Rank - 1) ||
                                                    (newCoord.Col < 0) || (newCoord.Col > Constants.Rank - 1)).Count();
            return outRange == 0;

        }

        // Comprobar que las celdas a ocupar por la pieza
        // están vacías.
        private bool TestFreeCells(GameStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game[c].IsFree).Where(x => x != true).Count();
            return ex == 0;
        }


    }
}
