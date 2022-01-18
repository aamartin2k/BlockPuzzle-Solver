using BPSolver.Enums;
using BPSolver.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    public class GameSimpleStatusFactory
    {
        static public GameSimpleStatus Create()
        {
            GameSimpleStatus onew = new GameSimpleStatus
            {
                NextPieces = new List<PieceName>(),
                //RowsCompletedIndex = new List<int>(),
                //ColumnsCompletedIndex = new List<int>(),
                Cells = new  List<Cell>(),
                CantMoves = 3
            };

            
            // llenar con nada
            onew.NextPieces.Add(PieceName.None);
            onew.NextPieces.Add(PieceName.None);
            onew.NextPieces.Add(PieceName.None);

            for (int i = 0; i < Constants.Rank; i++)
            {
                for (int j = 0; j < Constants.Rank; j++)
                {
                    onew.Cells.Add(new Cell(j, i));
                }
            }

            return onew;
        }
    }
}
