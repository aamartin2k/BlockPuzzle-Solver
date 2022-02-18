using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public class Factory
    {
        #region  GameStatus Factory

        // Create GameStatus for Root,  Id = 0,  name = "Root"
        static public GameStatus CreateRootGameStatus()
        {
            return CreateGameStatus(0, "Root");
        }

        // Call as  Factory.CreateGameStatus(id, name);
        static public GameStatus CreateGameStatus(int id, string name)
        {
            GameStatus onew = new GameStatus(id);

            onew.Nombre = name;
            onew.NextPieces = new Dictionary<int, PieceName>();
            
            // llenar con nada
            onew.NextPieces.Add(0, PieceName.None);
            onew.NextPieces.Add(1, PieceName.None);
            onew.NextPieces.Add(2, PieceName.None);

            SCell cell;
            DenseSCellArray<SCell> _Cells;
            _Cells = new DenseSCellArray<SCell>(Constants.BoardSize, Constants.BoardSize);

            onew.Cells = _Cells;

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    cell = new SCell(i, j);
                    _Cells[i, j] = cell;
                }
            }

            return onew;
        }

        // Create GameStatus with deep copy of another
        static public GameStatus CloneGameStatus(int id, GameStatus game)
        {
            string name = string.Format("Cloned {0}", id);

            return CloneGameStatus(id, name, game);
        }

        static public GameStatus CloneGameStatus(int id, string name, GameStatus game)
        {
            GameStatus onew = new GameStatus(id);

            onew.Nombre = name;
            onew.NextPieces = new Dictionary<int, PieceName>();

            foreach (var dkv in game.NextPieces)
            {
                onew.NextPieces.Add(dkv.Key, dkv.Value);
            }

            DenseSCellArray<SCell> _Cells;
            _Cells = new DenseSCellArray<SCell>(Constants.BoardSize, Constants.BoardSize);

            onew.Cells = _Cells;

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    _Cells[i, j] = new SCell(game.Cells[i, j]) ;
                }
            }

            return onew;
        }

        #endregion
        /*
        #region  GameStatus Factory

        // Create GameStatus for Root,  Id = 0,  name = "Root"
        static public GameStatus CreateRootGameStatus()
        {
            return CreateGameStatus(0, "Root");
        }

        // Call as  Factory.CreateGameStatus(id, name);
        static public GameStatus CreateGameStatus(int id, string name)
        {
            GameStatus onew = new GameStatus(id);

            onew.Nombre = name;
            onew.NextPieces = new Dictionary<int, PieceName>();
            onew.Cells = new List<Cell>();

            // llenar con nada
            onew.NextPieces.Add(0, PieceName.None);
            onew.NextPieces.Add(1, PieceName.None);
            onew.NextPieces.Add(2, PieceName.None);

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    onew.Cells.Add(new Cell(i, j));
                }
            }

            return onew;
        }

        // Create GameStatus with deep copy of another
        static public GameStatus CloneGameStatus(int id, GameStatus game)
        {
            string name = string.Format("Cloned {0}", id);

            return CloneGameStatus(id, name, game);
        }

        static public GameStatus CloneGameStatus(int id, string name, GameStatus game)
        {
            GameStatus onew = new GameStatus(id);

            onew.Nombre = name;
            onew.NextPieces = new Dictionary<int, PieceName>();
            onew.Cells = new List<Cell>();

            onew.NextPieces.Add(0, game.NextPieces[0]);
            onew.NextPieces.Add(1, game.NextPieces[1]);
            onew.NextPieces.Add(2, game.NextPieces[2]);

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    onew.Cells.Add(new Cell(game[i, j]));
                }
            }


            return onew;
        }

        #endregion
        */
    }
}
