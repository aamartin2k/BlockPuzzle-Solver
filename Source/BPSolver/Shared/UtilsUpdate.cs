using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver
{
    public static partial class Utils
    {
        #region Updates after piece played

        static internal void DeleteCompletedRoC(GameStatus status)
        {
            // Testing for Column or Row completed.
            if (IsAnyCompleted(status))
            {
                ClearCompleted(status);
            }
        }

        #region Public

        static internal int CompletedCount(GameStatus game)
        {
            return CompletedRowsCount(game) +
                   CompletedColumnsCount(game);
        }

        #endregion

        #region Private
        #region Check for Completion 
        static internal bool IsAnyCompleted(GameStatus game)
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

        static private List<Coord> GetRowCoord(GameStatus game, int row)
        {
            var list = game.Cells.Where(z => z.Row == row);

            Coord coord;
            List<Coord> Coords = new List<Coord>();

            foreach (var cell in list)
            {
                coord = new Coord(cell.Row, cell.Col);
                Coords.Add(coord);
            }
            return Coords;
        }

        static private List<Coord> GetColumnCoord(GameStatus game, int col)
        {
            var list = game.Cells.Where(z => z.Col == col);

            Coord coord;
            List<Coord> Coords = new List<Coord>();
            
            foreach (var cell in list)
            {
                coord = new Coord(cell.Row, cell.Col);
                Coords.Add(coord);
            }
            return Coords;
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
                count = CompletedRowsCount(status);
                status.CompletedRows += count;

                listRow = GetListRowsCompleted(status);
            }

            // buscar columnas
            ret = IsAnyColumnCompleted(status);
            if (ret)
            {
                count = CompletedColumnsCount(status);
                status.CompletedColumns += count;

                // guardar indices en lista
                listCol = GetListColumnsCompleted(status);
            }

            // eliminar filas y columnas con foreach segun indice
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
            var list = GetColumnCoord(game, index);
            ClearCells(game, list);
        }

        static private void ClearRow(GameStatus game, int index)
        {
            var list = GetRowCoord(game, index);
            ClearCells(game, list);
        }

        static private void ClearCells(GameStatus game, List<Coord> list)
        {
            foreach (var coord in list)
            {
                game.Cells[coord].Color = PieceColor.None;
            }
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

        #region Updates after piece inserted

        #region Public
        static internal void UpdateGameStatsAfterDraw(GameStatus status)
        {
            status.CellsCount = CellsCount(status.Cells.ToList());
            status.FreeCells = FreeCellsCount(status.Cells.ToList());
            status.OccupiedCells = OccupiedCellsCount(status.Cells.ToList());
        }

        #endregion

        #region Private
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

        #endregion

        #endregion

    }
}
