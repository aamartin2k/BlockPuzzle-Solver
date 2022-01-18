using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Solver 
{
    public partial class Solver
    {

        // Metodos publicos
        public void ClearCompleted(GameStatus status)
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



        // Metodos auxiliares

        public bool IsAnyCompleted(GameStatus game)
        {
            return IsAnyRowCompleted(game) | IsAnyColumnCompleted(game);
        }

        public bool IsAnyRowCompleted(GameStatus game)
        {

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    return true;
            }

            return false;
        }

        public bool IsAnyColumnCompleted(GameStatus game)
        {

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    return true;
            }

            return false;

        }

        private bool IsColumnCompleted(GameStatus game, int col)
        {
            var list = GetColumn(game, col);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }

        private bool IsRowCompleted(GameStatus game, int row)
        {
            var list = GetRow(game, row);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }

        private List<Cell> GetRow(GameStatus game, int row)
        {
            var list = game.Cells.Where(z => z.Row == row);
            return list.ToList();
        }

        private List<Cell> GetColumn(GameStatus game, int col)
        {
            var list = game.Cells.Where(z => z.Col == col);
            return list.ToList();
        }

        // Contar completas
        private int CompletedCount(GameStatus game)
        {
            return CompletedRowsCount(game) +
                   CompletedColumnsCount(game);
        }

        // Contar Filas completas
        private int CompletedRowsCount(GameStatus game)
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
        public int CompletedColumnsCount(GameStatus game)
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
        public int[] GetListRowsCompleted(GameStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }

        private void ListRowsCompleted(GameStatus game, List<int> list)
        {
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    list.Add(i);
            }
        }


        // Listar Indice de Columnas completas
        private void ListColumnsCompleted(GameStatus game, List<int> list)
        {
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    list.Add(i);
            }
        }

        public int[] GetListColumnsCompleted(GameStatus game)
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
        private void ClearColumn(GameStatus game, int index)
        {
            var list = GetColumn(game, index);
            list.Select(c => c.Color = PieceColor.None).ToList();

        }

        private void ClearRow(GameStatus game, int index)
        {
            var list = GetRow(game, index);
            list.Select(c => c.Color = PieceColor.None).ToList();

        }

    }
}
