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
        public bool IsAnyCompletedNuevo(SimpleGameStatus game)
        {
            return IsAnyRowCompletedNuevo(game) | IsAnyColumnCompletedNuevo(game);
        }

        public bool IsAnyRowCompletedNuevo(SimpleGameStatus game)
        {

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompletedNuevo(game, i))
                    return true;
            }

            return false;
        }

        public bool IsAnyColumnCompletedNuevo(SimpleGameStatus game)
        {

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompletedNuevo(game, i))
                    return true;
            }

            return false;

        }

        private bool IsColumnCompletedNuevo(SimpleGameStatus game, int col)
        {
            //var list = GetColumn(game, col);
            //int cnt = list.Count(x => x.IsFree);
            //return cnt == 0;
            for (int i = 0; i < Constants.Rank; i++)
            {
                //if (!game.CellsA[i, col])
                if (game[i, col] == PieceColor.None)
                        return false;  
            }

            return true;
        }

        private bool IsRowCompletedNuevo(SimpleGameStatus game, int row)
        {
            //var list = GetRow(game, row);
            //int cnt = list.Count(x => x.IsFree);
            //return cnt == 0;
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (game[row, i] == PieceColor.None)
                    return false ;
            }

            return true;
        }

        //private List<SimpleCell> GetRow(SimpleGameStatus game, int row)
        //{
        //    var list = game.CellsA.Where(z => z.Row == row);
        //    return list.ToList();
        //}

        //private List<SimpleCell> GetColumn(SimpleGameStatus game, int col)
        //{
        //    var list = game.CellsA.Where(z => z.Col == col);
        //    return list.ToList();
        //}


        // Contar completas
        private int CompletedCountNuevo(SimpleGameStatus game)
        {
            return CompletedRowsCount(game) +
                   CompletedColumnsCount(game);
        }

        // Contar Filas completas
        private int CompletedRowsCount(SimpleGameStatus game)
        {
            int count = 0;

            // Count how many rows
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompletedNuevo(game, i))
                    count++;
            }

            return count;
        }

        // Contar Columnas completas
        private int CompletedColumnsCount(SimpleGameStatus game)
        {
            int count = 0;

            // Calculate how many rows
            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompletedNuevo(game, i))
                    count++;
            }
            return count;
        }


        public void ClearCompletedNuevo(SimpleGameStatus status)
        {
            int count;
            bool ret;
            int[] listRow = new int[] { };
            int[] listCol = new int[] { };

            // buscar filas
            ret = IsAnyRowCompletedNuevo(status);
            if (ret)
            {
                // contar ANTES de Borrar
                count = CompletedRowsCount(status);
                status.CompletedRows += count;

                listRow = GetListRowsCompleted(status);
            }

            // buscar columnas
            ret = IsAnyColumnCompletedNuevo(status);
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


        // Eliminar
        private void ClearColumn(SimpleGameStatus game, int index)
        {
            //var list = GetColumn(game, index);
            //list.Select(c => c.Color = PieceColor.None).ToList();
            for (int i = 0; i < Constants.Rank; i++)
            {
                game[i, index] = PieceColor.None;
            }
        }

        private void ClearRow(SimpleGameStatus game, int index)
        {
            //var list = GetRow(game, index);
            //list.Select(c => c.Color = PieceColor.None).ToList();
            for (int i = 0; i < Constants.Rank; i++)
            {
                game[index, i] = PieceColor.None;
            }
        }



        // Listar Indice de Filas completas
        public int[] GetListRowsCompleted(SimpleGameStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsRowCompletedNuevo(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }


        public int[] GetListColumnsCompleted(SimpleGameStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.Rank; i++)
            {
                if (IsColumnCompletedNuevo(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }



    }
}
