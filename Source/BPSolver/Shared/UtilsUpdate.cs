using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public static partial class Utils
    {
        // Modificaciones despues de jugar pieza
        #region Updates after piece played
        static internal void DeleteCompletedRoC(GameStatus status)
        {
            // check for completion, update stats and delete
            // Testing for Column or Row completion
            if (IsAnyCompleted(status))
            {
                // Delete
                ClearCompleted(status);
            }
        }
        #region Public

        #endregion

        #region Private
        #region Check for Completion 
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

        static private List<SCell> GetRow(GameStatus game, int row)
        {
            var list = game.Cells.Where(z => z.Row == row);
            return list.ToList();
        }

        static private List<SCell> GetColumn(GameStatus game, int col)
        {
            var list = game.Cells.Where(z => z.Col == col);
            return list.ToList();
        }

        #endregion

        #region Delete Completed
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

        #endregion

        // Modificaciones despues de insertar pieza
        #region Updates after piece insertion

        #region Public
        static internal void UpdateGameStatsAfterDraw(GameStatus status)
        {
            status.CellsCount = CellsCount(status.Cells.ToList());
            status.FreeCells = FreeCellsCount(status.Cells.ToList());
            status.OccupiedCells = OccupiedCellsCount(status.Cells.ToList());
        }

        #endregion

        #region Private
        static private int CellsCount(List<SCell> Cells)
        {
            return Cells.Count();
        }

        static private int FreeCellsCount(List<SCell> Cells)
        {
            return Cells.Count(x => x.IsFree);
        }

        static private int OccupiedCellsCount(List<SCell> Cells)
        {
            return Cells.Count(x => !x.IsFree);
        }

        #endregion

        #endregion

    }
}
