using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;

namespace GSTestSelector
{
    class GameStatusListClass : BaseGStatus, IGameStatus
    {
        private List<ICell> listCells;
         
        
        public  ICell this[Coord coord]
        {
            get { return  listCells.First(z => z.Row == coord.Row && z.Col == coord.Col); }
        }

        public ICell this[int row, int col]
        {
            get { return listCells.First(z => z.Row == row && z.Col == col); }
        }


        public Dictionary<int, PieceName> NextPieces { get; set; }


        public IEnumerable<ICell> Cells
        {
            get { return listCells; }
            set { listCells = value as List<ICell> ; }
        }


    }
}
