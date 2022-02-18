using System.Linq;
using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GSTestSelector
{
    class GameStatusArrayStruct : BaseGStatus, IGameStatus
    {
        private ICell[] listCells;

        private int ColumnCount;

        public GameStatusArrayStruct(int size)
        {
            ColumnCount = size;
        }


        public ref ICell this[Coord coord]
        {
            get
            {
                int realIndex = GetRealIndex(coord.Row, coord.Col);
                return ref listCells[realIndex];
            }
        }
        public ref ICell this[int row, int col]
        {
            get
            {
                int realIndex = GetRealIndex(row, col);
                return ref listCells[realIndex]; ;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetRealIndex(int row, int col)
        {
            return (row * ColumnCount) + col;
        }


        public Dictionary<int, PieceName> NextPieces { get; set; }

        public IEnumerable<ICell> Cells
        {
            get { return listCells; }
            set
            {
                listCells = value as ICell[];
            }
        }

    }
}
