using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Objects;



namespace BPSolver.Solver
{
    
     class GameBoard
    {
        // Declarations
        private int _rank;


        private List<Cell> Cells { get; set; }

        // Constructor
        public GameBoard(int rank)
        {
            _rank = rank;

            Cells = new List<Cell>();


            int id = 1;
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < rank; j++)
                {
                    Cells.Add(new Cell(id, j, i));
                    id++;
                }
            }
        }

        // Cells Indexer
        public Cell this[Coord coord, int bar]
        {
            get { return Cells.First(z => z.Row == coord.Row && z.Col == coord.Col); }
        }

        public Cell this[int row, int col]
        {
            get { return Cells.First(z => z.Row == row && z.Col == col); }
        }


        #region Properties
        // Properties

        // Numeric
        public int CellsCount
        {
            get { return Cells.Count(); }
        }

        public int FreeCellsCount
        {
            get { return Cells.Count(x => x.IsFree); }
        }

        public int OccupiedCellsCount
        {
            get { return Cells.Count(x => !x.IsFree); }
        }

        public int CompletedRowsCount
        {
            get
            {
                int count = 0;

                if (IsAnyRowCompleted)
                {
                    // Calculate how many rows
                    for (int i = 0; i < _rank; i++)
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

                if (IsAnyColumnCompleted)
                {
                    // Calculate how many rows
                    for (int i = 0; i < _rank; i++)
                    {
                        if (IsColumnCompleted(i))
                            count++;
                    }
                }

                return count;
            }
        }

        public bool IsAnyRowCompleted
        {
            get
            {
            for (int i = 0; i < _rank; i++)
            {
                if (IsRowCompleted(i))
                    return true;
            }

            return false;
            }
        }

        public bool IsAnyColumnCompleted
        {
            get
            {
                for (int i = 0; i < _rank; i++)
                {
                    if (IsColumnCompleted(i))
                        return true;
                }

                return false;
            }
        }

        public bool IsAnyCompleted
        { get
            { return IsAnyRowCompleted | IsAnyColumnCompleted;   }
        }

        public List<Cell> FreeCells
        {
            get
            {
                var list = Cells.Where(z => z.IsFree == true);
                return list.ToList();
            }
        }
        #endregion

        #region Methods

        // Retrieving Rows and Columns
        public List<Cell> GetRow(int row)
        {
            var list = Cells.Where(z => z.Row == row);
            return list.ToList();
        }

        public bool IsColumnCompleted(int col)
        {
            var list = GetColumn(col);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }


        public List<Cell> GetColumn(int col)
        {
            var list = Cells.Where(z => z.Col == col);
            return list.ToList();
        }

        public bool IsRowCompleted(int row)
        {
            var list = GetRow(row);
            int cnt = list.Count(x => x.IsFree);

            return cnt == 0;
        }



        #endregion


    }
}
