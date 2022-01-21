using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    [Serializable]
    public class DenseSCellArray<SCell> : IEnumerable<SCell>
    {
        private SCell[] _dataArray;
        
        #region Constructor 

        /// <summary>
        /// Creates a new instance of DenseArray with no data
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        public DenseSCellArray(int rows, int columns)
        {
            RowCount = rows;
            ColumnCount = columns;

            _dataArray = new SCell[rows * columns];
        }

        /// <summary>
        /// Creates a new instance of DenseArray with data from a 2D array.
        /// </summary>
        /// <param name="array">Source 2d array</param>
        public DenseSCellArray(SCell[,] array)
        {
            RowCount = array.GetLength(0);
            ColumnCount = array.GetLength(1);

            _dataArray = new SCell[RowCount * ColumnCount];

            int realIndex;

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    realIndex = GetRealIndex(i, j);
                    _dataArray[realIndex] = array[i, j];
                }
            }
        }

        #endregion

        #region Indexer
        // Implementacion standard
        //public SCell this[int row, int column]
        //{
        //    get
        //    {
        //        int realIndex = GetRealIndex(row, column);
        //        return _dataArray[realIndex];
        //    }
        //    set
        //    {
        //        int realIndex = GetRealIndex(row, column);
        //        _dataArray[realIndex] = value;
        //    }
        //}

        // public ref SCell this[int row, int col] => ref _Cells[row, col];

        public ref SCell this[int row, int col]
        {
            get
            {
                int realIndex = GetRealIndex(row, col);
                return ref _dataArray[realIndex]; ;
            }
        }

        public ref SCell this[Coord coord]
        {
            get
            {
                int realIndex = GetRealIndex(coord.Row, coord.Col);
                return ref _dataArray[realIndex]; ;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of columns
        /// </summary>
        public int ColumnCount { get; private set; }

        /// <summary>
        /// Gets the number of rows
        /// </summary>
        public int RowCount { get; private set; }
        #endregion

        #region Private Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetRealIndex(int row, int col)
        {
            return (row * ColumnCount) + col;
        }

      
        #endregion

        #region IEnumerable implementation
        /// <summary>
        /// IEnumerable implementation.
        /// </summary>
        /// <returns>internal array enumerator</returns>
        public IEnumerator<SCell> GetEnumerator()
        {
            return ((IEnumerable<SCell>)_dataArray).GetEnumerator();
        }

        /// <summary>
        /// IEnumerable Implementation
        /// </summary>
        /// <returns>internal array enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dataArray.GetEnumerator();
        }

        #endregion
    }

}
