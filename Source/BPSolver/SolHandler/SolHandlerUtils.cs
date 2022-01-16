using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class SolHandler : ISolver
    {
        #region Funciones Utileria Compartidas
        #region Publicas
        
        static internal bool TestPiece(Coord insertCoord, PieceName name, GameStatus gstat)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = GetPiece(name);

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
        static internal void UpdateGameStatsAfterDraw(GameStatus status)
        {
            status.CellsCount = CellsCount(status.Cells);
            status.FreeCells = FreeCellsCount(status.Cells);
            status.OccupiedCells = OccupiedCellsCount(status.Cells);
        }

        static internal void UpdateGameStatsAfterPlay(GameStatus status)
        {
            // check for completion, update stats and delete
            // Testing for Column or Row completion
            if (IsAnyCompleted(status))
            {
                // Delete
                ClearCompleted(status);
            }

    }

        #endregion
        #region Privadas
        #region Completion
        #region Completion Check
        static private bool IsAnyCompleted(GameStatus game)
        {
            return IsAnyRowCompleted(game) | IsAnyColumnCompleted(game);
        }

        static private bool IsAnyRowCompleted(GameStatus game)
        {

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsRowCompleted(game, i))
                    return true;
            }

            return false;
        }

        static private bool IsAnyColumnCompleted(GameStatus game)
        {

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsColumnCompleted(game, i))
                    return true;
            }

            return false;

        }

        static private bool IsColumnCompleted(GameStatus game, int col)
        {
            var list = GetColumn(game, col);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }

        static private bool IsRowCompleted(GameStatus game, int row)
        {
            var list = GetRow(game, row);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }

        static private List<Cell> GetRow(GameStatus game, int row)
        {
            var list = game.Cells.Where(z => z.Row == row);
            return list.ToList();
        }

        static private List<Cell> GetColumn(GameStatus game, int col)
        {
            var list = game.Cells.Where(z => z.Col == col);
            return list.ToList();
        }

        #endregion
        #region Completion Delete
        static private void ClearCompleted(GameStatus status)
        {
            int count;
            bool ret;
            int[] listRow = new int[] { };
            int[] listCol = new int[] { };

            // buscar filas
            ret = IsAnyRowCompleted(status);
            if (ret)
            {
                // contar ANTES de Borrar
                count = CompletedRowsCount(status);
                status.CompletedRows += count;

                listRow = GetListRowsCompleted(status);
            }

            // buscar columnas
            ret = IsAnyColumnCompleted(status);
            if (ret)
            {
                // contar
                count = CompletedColumnsCount(status);
                status.CompletedColumns += count;

                // guardar indices en lista
                listCol = GetListColumnsCompleted(status);
            }

            // eliminar filas y columnas con foreach
            foreach (var row in listRow)
            {
                ClearRow(status, row);
            }

            foreach (var col in listCol)
            {
                ClearColumn(status, col);
            }
        }

        static private void ClearColumn(GameStatus game, int index)
        {
            var list = GetColumn(game, index);
            list.Select(c => c.Color = PieceColor.None).ToList();

        }

        static private void ClearRow(GameStatus game, int index)
        {
            var list = GetRow(game, index);
            list.Select(c => c.Color = PieceColor.None).ToList();

        }
        static private int[] GetListColumnsCompleted(GameStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsColumnCompleted(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }

        static private int[] GetListRowsCompleted(GameStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsRowCompleted(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }

        


        // Contar Filas completas
        static private int CompletedRowsCount(GameStatus game)
        {
            int count = 0;

            // Count how many rows
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsRowCompleted(game, i))
                    count++;
            }

            return count;
        }

        // Contar Columnas completas
        static private int CompletedColumnsCount(GameStatus game)
        {
            int count = 0;

            // Calculate how many rows
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsColumnCompleted(game, i))
                    count++;
            }
            return count;
        }

        #endregion
        #endregion
        #region Stats
        static private int CellsCount(List<Cell> Cells)
        {
            return Cells.Count();
        }

        static private int FreeCellsCount(List<Cell> Cells)
        {
            return Cells.Count(x => x.IsFree);
        }

        static private int OccupiedCellsCount(List<Cell> Cells)
        {
            return Cells.Count(x => !x.IsFree);
        }
        
        static private bool TestFreeCells(GameStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game[c].IsFree).Where(x => x == false).Count();
            return ex == 0;
        }

        static private bool TestRealCoords(List<Coord> matrix)
        {
            var outRange = matrix.Where(newCoord => (newCoord.Row < 0) || (newCoord.Row > Constants.BoardSize - 1) ||
                                                    (newCoord.Col < 0) || (newCoord.Col > Constants.BoardSize - 1)).Count();
            return outRange == 0;

        }
        #endregion

        #endregion
        #endregion
    }
}
