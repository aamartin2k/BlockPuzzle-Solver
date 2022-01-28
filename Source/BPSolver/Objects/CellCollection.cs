using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Objects
{
    /// <summary>
    /// Implement a collection of Cell.
    /// </summary>
    /// <remarks>The traditional approach for a board (2D matrix) is
    /// replaced with an IEnumerable collection to favor LINQ query.</remarks>
    /// 
    [Serializable]
    public class Board : IEnumerable<Cell>
    {

        //private List<Cell> _cells;

        public List<Cell> Cells { get; private set; }

        public Board()
        {
            Cells = new List<Cell>();
        }
        public Board(List<Cell> cells)
        {
            Cells = new List<Cell>();

            foreach (var cell in cells)
            {
                Cells.Add(new Cell(cell));
            }
        }

        public void Add(Cell cell)
        {
            Cells.Add(cell);
        }

        #region Indexer
        public Cell this[int row, int column]
        {   
            get
            {
                return Cells.First(z => z.Row == row && z.Col == column);
            }
        }

        public Cell this[Coord coord]
        {
            get
            {
                return Cells.First(z => z.Row == coord.Row && z.Col == coord.Col);
            }
        }
        #endregion

        #region IEnumerable implementation

        public IEnumerator<Cell> GetEnumerator()
        {
            return ((IEnumerable<Cell>)Cells).GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Cells.GetEnumerator();
        }

        #endregion
    }
}
