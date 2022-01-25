using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    [Serializable]
    public class CellCollection : IEnumerable<Cell>
    {

        private List<Cell> _cells { get; set; }

        public CellCollection()
        {
            _cells = new List<Cell>();
        }

        public void Add(Cell cell)
        {
            _cells.Add(cell);
        }

        #region Indexer
        // Implementacion standard
        public Cell this[int row, int column]
        {   
            get
            {
                return _cells.First(z => z.Row == row && z.Col == column);
            }
        }

        public Cell this[Coord coord]
        {
            get
            {
                return _cells.First(z => z.Row == coord.Row && z.Col == coord.Col);
            }
        }
        #endregion

        #region IEnumerable implementation

        public IEnumerator<Cell> GetEnumerator()
        {
            return ((IEnumerable<Cell>)_cells).GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        #endregion
    }
}
