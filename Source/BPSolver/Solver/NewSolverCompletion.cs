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

        // Metodos publicos
        public void ClearCompleted(GameSimpleStatus status)
        {
            int count;

            // Si tiene alguno completado
            bool ret = IsAnyCompleted(status);

            if (ret)
            {
                // buscar filas
                ret = IsAnyRowCompleted(status);
                if (ret)
                {
                    // contar
                    count = CompletedRowsCount(status);
                    status.CompletedRows += count;

                    int[] list = GetListRowsCompleted(status);

                    // eliminar
                    foreach (var row in list)
                    {
                        ClearRow(status, row);
                    }
                }

                // buscar columnas
                ret = IsAnyColumnCompleted(status);
                if (ret)
                {
                    // contar
                    count = CompletedColumnsCount(status);
                    status.CompletedColumns += count;

                    // guardar indices en lista
                    int[] list = GetListColumnsCompleted(status);

                    // eliminar
                    foreach (var col in list)
                    {
                        ClearColumn(status, col);
                    }

                }
            }
            else
            {

            }
            // Actualizar board

            // Actualizar estadistica (done in situ)
        }



        // Metodos privados

        private bool IsAnyCompleted(GameSimpleStatus game)
        {
            return IsAnyRowCompleted(game) | IsAnyColumnCompleted(game);
        }

        public bool IsAnyRowCompleted(GameSimpleStatus game)
        {

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    return true;
            }

            return false;
        }

        public bool IsAnyColumnCompleted(GameSimpleStatus game)
        {

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    return true;
            }

            return false;

        }

        private bool IsColumnCompleted(GameSimpleStatus game, int col)
        {
            var list = GetColumn(game, col);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }

        private bool IsRowCompleted(GameSimpleStatus game, int row)
        {
            var list = GetRow(game, row);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }

        private List<Cell> GetRow(GameSimpleStatus game, int row)
        {
            var list = game.Cells.Where(z => z.Row == row);
            return list.ToList();
        }

        private List<Cell> GetColumn(GameSimpleStatus game, int col)
        {
            var list = game.Cells.Where(z => z.Col == col);
            return list.ToList();
        }


        // Contar Filas completas
        private int CompletedRowsCount(GameSimpleStatus game)
        {
            int count = 0;

            // Count how many rows
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    count++;
            }

            return count;
        }

        // Contar Columnas completas
        public int CompletedColumnsCount(GameSimpleStatus game)
        {
            int count = 0;

            // Calculate how many rows
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    count++;
            }
            return count;
        }

        // Listar Indice de Filas completas
        public int[] GetListRowsCompleted(GameSimpleStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }

        private void ListRowsCompleted(GameSimpleStatus game, List<int> list)
        {
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    list.Add(i);
            }
        }


        // Listar Indice de Columnas completas
        private void ListColumnsCompleted(GameSimpleStatus game, List<int> list)
        {
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    list.Add(i);
            }
        }

        public int[] GetListColumnsCompleted(GameSimpleStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }

        // Eliminar
        private void ClearColumn(GameSimpleStatus game, int index)
        {
            var list = GetColumn(game, index);
            list.Select(c => c.Color = PieceColor.None).ToList();

        }

        private void ClearRow(GameSimpleStatus game, int index)
        {
            var list = GetRow(game, index);
            list.Select(c => c.Color = PieceColor.None).ToList();

        }

    }
}
