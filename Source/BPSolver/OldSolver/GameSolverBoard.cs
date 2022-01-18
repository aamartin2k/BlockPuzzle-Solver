using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Objects;
using BPSolver.Enums;


namespace BPSolver.Solver
{
    public partial class GameSolver
    {


        #region Properties
        // Properties

        // Numeric
        public int CellsCount
        {
            get { return RootStatus.Cells.Count(); }
        }

        public int FreeCellsCount
        {
            get { return RootStatus.Cells.Count(x => x.IsFree); }
        }

        public int OccupiedCellsCount
        {
            get { return RootStatus.Cells.Count(x => !x.IsFree); }
        }

        public int CompletedRowsCount
        {
            get
            {
                int count = 0;

                if (IsAnyRowCompleted())
                {
                    // Calculate how many rows
                    for (int i = 0; i < Rank; i++)
                    {
                        if (IsRowCompleted(i))
                            count++;
                    }
                }

                return count;
            }
        }

        public int CompletedColumnsCount
        {
            get
            {
                int count = 0;

                if (IsAnyColumnCompleted())
                {
                    // Calculate how many rows
                    for (int i = 0; i < Rank; i++)
                    {
                        if (IsColumnCompleted(i))
                            count++;
                    }
                }

                return count;
            }
        }

       

     

        public List<Cell> GetFreeCells(GameStatus status)
        {
                 
            var list = status.Cells.Where(z => z.IsFree == true);
            return list.ToList();
     
        }
        #endregion

        #region Public Methods

        // Retrieving Rows and Columns
        public List<Cell> GetRow(GameStatus game, int row)
        {
            var list = game.Cells.Where(z => z.Row == row);
            return list.ToList();
        }

        public bool IsColumnCompleted( int col)
        { return IsColumnCompleted(RootStatus, col); }

        public bool IsColumnCompleted(GameStatus game, int col)
        {
            var list = GetColumn(game, col);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }


        public List<Cell> GetColumn(GameStatus game, int col)
        {
            var list = game.Cells.Where(z => z.Col == col);
            return list.ToList();
        }

        public bool IsRowCompleted(int row)
        { return IsRowCompleted(RootStatus, row); }

        public bool IsRowCompleted(GameStatus game, int row)
        {
            var list = GetRow(game, row);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
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

        #endregion




        // Crear coord absolutasde celdas vecinas a partir del pto de insercion y la lista de coord relativas de la pieza
        // Descarta las coord fuera del tablero.
        private List<Coord> GetRealCoordsNeighbors(Coord insertCoord, List<Coord> matrix)
        {
            Coord newCoord;
            List<Coord> realCoords = new List<Coord>();

            foreach (Coord coord in matrix)
            {
                newCoord = insertCoord + coord;
                realCoords.Add(newCoord);
            }

            var outBoard = realCoords.Where(c => (c.Row < 0) || (c.Row > Rank - 1) ||
                                                 (c.Col < 0) || (c.Col > Rank - 1));

            return realCoords.Except(outBoard).ToList();
        }

        // Comprobar que las coordenadas absolutas de la pieza están dentro del tablero
        private bool TestRealCoords(List<Coord> matrix)
        {
            var outRange = matrix.Where(newCoord => (newCoord.Row < 0) || (newCoord.Row > Rank - 1) ||
                                                    (newCoord.Col < 0) || (newCoord.Col > Rank - 1)).Count();
            return outRange == 0;

        }

        // Comprobar que las celdas de destino están libres
        private bool TestFreeCells(List<Coord> realCoords)
        {
            return TestFreeCells(RootStatus, realCoords);
        }

        private bool TestFreeCells(GameStatus game,  List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game[c, 0].IsFree).Where(x => x != true).Count();
            return ex == 0;
        }

        //GameSimpleStatus
        private bool TestFreeCells(GameSimpleStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game[c].IsFree).Where(x => x != true).Count();
            return ex == 0;
        }


        #region Find  Completed Methods

        private void ListRowsCompleted(List<int> list)
        {
            ListRowsCompleted(RootStatus, list);
        }
        private void ListRowsCompleted(GameStatus game, List<int> list)
        {
            for (int i = 0; i < Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    list.Add(i);
            }
        }

        private void ListColumnsCompleted(List<int> list)
        {
            ListColumnsCompleted(RootStatus, list);
        }

        private void ListColumnsCompleted(GameStatus game, List<int> list)
        {
            for (int i = 0; i < Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    list.Add(i);
            }
        }

        private bool IsAnyCompleted()
        { return IsAnyCompleted(RootStatus); }

        private bool IsAnyCompleted(GameStatus game)
        {
            return IsAnyRowCompleted(game) | IsAnyColumnCompleted(game);
        }

        private bool IsAnyRowCompleted()
        { return IsAnyRowCompleted(RootStatus); }

        private bool IsAnyRowCompleted(GameStatus game)
        {

            for (int i = 0; i < Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    return true;
            }

            return false;
        }

        private bool IsAnyColumnCompleted()
        { return IsAnyColumnCompleted(RootStatus); }

        private bool IsAnyColumnCompleted(GameStatus game)
        {

            for (int i = 0; i < Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    return true;
            }

            return false;

        }

        #endregion


        #region Clear Completed Methods

        private void ClearAnyCompleted()
        {   ClearAnyCompleted(RootStatus); }

        private void ClearAnyCompleted(GameStatus game)
        {
            //ClearAnyRowCompleted(game);
            //ClearAnyColumnCompleted(game);
            // make list
            List<int> Rows = new List<int>();
            List<int> Columns = new List<int>();

            ListRowsCompleted(game, Rows);
            ListColumnsCompleted(game, Columns);

            foreach (var row in Rows)
            {
                ClearRow(game, row);
            }

            foreach (var col in Columns)
            {
                ClearColumn(game, col);
            }

        }

        private void ClearAnyRowCompleted(GameStatus game)
        {
            for (int i = 0; i < Rank; i++)
            {
                if (IsRowCompleted(game, i))
                    ClearRow(game, i);
            }
        }

        private void ClearAnyColumnCompleted(GameStatus game)
        {
            for (int i = 0; i < Rank; i++)
            {
                if (IsColumnCompleted(game, i))
                    ClearColumn(game, i);
            }
        }
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

       



        #endregion

    }
}