using System;
using System.Collections.Generic;
using System.Linq;
using BPSolver;
using BPSolver.Enums;
using BPSolver.Objects;

namespace GSTestSelector
{
    class Factory
    {
        static public GameStatusListClass CreateGameStatusListClass(int id, string name)
        {
            GameStatusListClass onew = new GameStatusListClass();

            onew.Id = id;
            onew.Name = name;

            onew.NextPieces = new Dictionary<int, PieceName>();

            // llenar con nada
            onew.NextPieces.Add(0, PieceName.None);
            onew.NextPieces.Add(1, PieceName.None);
            onew.NextPieces.Add(2, PieceName.None);

            CCell cell;
            List<CCell> _Cells = new List<CCell>();


            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    cell = new CCell(i, j);
                    _Cells.Add(cell);
                }
            }

            onew.Cells = _Cells;

            return onew;
        }


        static public GameStatusArrayStruct CreateGameStatusArrayStruct(int id, string name)
        {
            GameStatusArrayStruct onew = new GameStatusArrayStruct(Constants.BoardSize);

            onew.Id = id;
            onew.Name = name;

            onew.NextPieces = new Dictionary<int, PieceName>();

            // llenar con nada
            onew.NextPieces.Add(0, PieceName.None);
            onew.NextPieces.Add(1, PieceName.None);
            onew.NextPieces.Add(2, PieceName.None);

            onew.Cells = new ICell[Constants.BoardSize * Constants.BoardSize];

            SCell cell;

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    cell = new SCell(i, j);
                    onew[i, j] = cell;
                }
            }

            return onew;
        }

    }
}
