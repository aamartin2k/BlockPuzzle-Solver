using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver.Solver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class Controller : IBPServer
    {

        static private GameStatus CreateRootStatus()
        {
            return CreateChildStatus(0, "Root");
        }

        static private GameStatus CreateChildStatus(int id, string name)
        {
            GameStatus onew = new GameStatus(id);

            onew.Nombre = name;
            onew.NextPieces = new Dictionary<int, PieceName>();
            onew.Cells = new List<Cell>();

            // llenar con nada
            onew.NextPieces.Add(0, PieceName.None);
            onew.NextPieces.Add(1, PieceName.None);
            onew.NextPieces.Add(2, PieceName.None);

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
