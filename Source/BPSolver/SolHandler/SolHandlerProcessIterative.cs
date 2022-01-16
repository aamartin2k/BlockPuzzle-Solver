using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class SolHandler : ISolver
    {
        private GameTreeSimple CreateSolutionTreeIterative(GameStatus game)
        {
            GameTreeSimple treeRoot;

            // Convertir Estado inicial GameStatus en GameTreeSimple por optimizacion
            SimpleGameStatus GStIni = GetSimpleFromGameStatus(game);
            GStIni.Nombre = RootName;

            // Solution Tree Root
            treeRoot = new GameTreeSimple(GStIni);
            // sobre las hojas del arbol
            List<GameTreeSimple> ramas = treeRoot.SelectLeaves().ToList();

            //Llamada a proceso basico por paso
            // Cada paso se ejecuta en un nivel inferior del arbol
            // sobre los hijos del nivel superior
            for (int i = 0; i < Constants.NexPieces; i++)
            {
                ramas = CreateNewNodesP(ramas, treeRoot);
                ProcessNewNodesP(ramas, treeRoot);
            }

            return treeRoot;
        }

        private void ProcessNewNodesP(List<GameTreeSimple> padres, GameTreeSimple root)
        {
            SimpleGameStatus cloned;

            foreach (var parent in padres)
            {
                cloned = parent.Item;

                // Aplicar Movimiento
                MakeMoveNuevo(cloned.Movement, cloned);

                // Borrar pieza de la lista
                DeleteMovedPieceNuevo(cloned.Movement, cloned);
                // Evaluar Movimiento
                cloned.Evaluation = EvaluateMoveNuevo(cloned.Movement, cloned);
                // Chequear por Completamiento
                CheckCompleteAndDeleteNuevo(cloned);
            }

        }

        private Movement MakeMoveNuevo(Movement move, SimpleGameStatus game)
        {
            // Comprobar que coincide el nombre de pieza
            bool ret = move.Name == game.NextPiecesA[move.Index];

            if (!ret)
                throw new Exception("No coincide el nombre de pieza de Move con Dictionary");
            // "Dibujar" pieza en board
            // Get reference to piece
            Piece piece = GetPiece(move.Name);

            // Obtener Real Coords
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);

            // ejecutar Select y asignar color a seleccion
            var ex = realCoords.Select(c => game[c] = piece.Color).ToList();


            return move;
        }

        private void DeleteMovedPieceNuevo(Movement move, SimpleGameStatus game)
        {
            // Borrar pieza de Dictionary
            game.NextPiecesA.Remove(move.Index);

        }

        private Eval EvaluateMoveNuevo(Movement move, SimpleGameStatus game)
        {
            Eval eval = Eval.GetNewEval();

            Piece piece = GetPiece(move.Name);
            // Tamanno de pieza
            eval.PieceSize = piece.Count;

            // Preference
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);
            eval.Preference = GetPreference(realCoords);

            // Neighbors
            eval.Neighbors = GetNeighborsCountNuevo(piece, move.InsertPoint, game);

            // Completion
            bool ret;
            int ccount = 0;

            ret = IsAnyCompletedNuevo(game);
            if (ret)
                ccount = CompletedCountNuevo(game);

            eval.CompleteRoC = ccount;

            return eval;

        }

        private int CompletedCountNuevo(SimpleGameStatus game)
        {
            return CompletedRowsCount(game) +
                   CompletedColumnsCount(game);
        }

        // Contar Filas completas
        private int CompletedRowsCount(SimpleGameStatus game)
        {
            int count = 0;

            // Count how many rows
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsRowCompletedNuevo(game, i))
                    count++;
            }

            return count;
        }

        // Contar Columnas completas
        private int CompletedColumnsCount(SimpleGameStatus game)
        {
            int count = 0;

            // Calculate how many rows
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsColumnCompletedNuevo(game, i))
                    count++;
            }
            return count;
        }

        private void CheckCompleteAndDeleteNuevo(SimpleGameStatus game)
        {
            if (IsAnyCompletedNuevo(game))
            {
                ClearCompletedNuevo(game);
            }
        }

        private void ClearCompletedNuevo(SimpleGameStatus status)
        {
            int count;
            bool ret;
            int[] listRow = new int[] { };
            int[] listCol = new int[] { };

            // buscar filas
            ret = IsAnyRowCompletedNuevo(status);
            if (ret)
            {
                // contar ANTES de Borrar
                count = CompletedRowsCount(status);
                status.CompletedRows += count;

                listRow = GetListRowsCompleted(status);
            }

            // buscar columnas
            ret = IsAnyColumnCompletedNuevo(status);
            if (ret)
            {
                // contar
                count = CompletedColumnsCount(status);
                status.CompletedColumns += count;

                // guardar indices en lista
                listCol = GetListColumnsCompleted(status);
            }

            // eliminar filas y columnas con foreach
            foreach (var row in listRow)
            {
                ClearRow(status, row);
            }

            foreach (var col in listCol)
            {
                ClearColumn(status, col);
            }
        }


        // Eliminar
        private void ClearColumn(SimpleGameStatus game, int index)
        {
            //var list = GetColumn(game, index);
            //list.Select(c => c.Color = PieceColor.None).ToList();
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                game[i, index] = PieceColor.None;
            }
        }

        private void ClearRow(SimpleGameStatus game, int index)
        {
            //var list = GetRow(game, index);
            //list.Select(c => c.Color = PieceColor.None).ToList();
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                game[index, i] = PieceColor.None;
            }
        }


        // Listar Indice de Filas completas
        private int[] GetListRowsCompleted(SimpleGameStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsRowCompletedNuevo(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }

        private int[] GetListColumnsCompleted(SimpleGameStatus game)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsColumnCompletedNuevo(game, i))
                    list.Add(i);
            }

            return list.ToArray();
        }



        private bool IsAnyCompletedNuevo(SimpleGameStatus game)
        {
            return IsAnyRowCompletedNuevo(game) | IsAnyColumnCompletedNuevo(game);
        }

        private bool IsAnyRowCompletedNuevo(SimpleGameStatus game)
        {

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsRowCompletedNuevo(game, i))
                    return true;
            }

            return false;
        }

        private bool IsAnyColumnCompletedNuevo(SimpleGameStatus game)
        {

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (IsColumnCompletedNuevo(game, i))
                    return true;
            }

            return false;

        }

        private bool IsColumnCompletedNuevo(SimpleGameStatus game, int col)
        {
    
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                //if (!game.CellsA[i, col])
                if (game[i, col] == PieceColor.None)
                    return false;
            }

            return true;
        }

        private bool IsRowCompletedNuevo(SimpleGameStatus game, int row)
        {

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                if (game[row, i] == PieceColor.None)
                    return false;
            }

            return true;
        }


        private int GetPreference(List<Coord> coords)
        {
            var ssum = coords.Select(c => Preferences[c.Row, c.Col]).Sum();
            return ssum;
        }

        private int GetNeighborsCountNuevo(Piece piece, Coord point, SimpleGameStatus game)
        {
            // Matriz Coord vecinos
            List<Coord> ngbMatrix;
            ngbMatrix = Piece.GetNeighborsMatrix(piece);
            // Coord Reales vecinos
            List<Coord> realCoords;
            realCoords = Piece.GetNeighborsRealCoords(point, ngbMatrix);

            var ex = realCoords.Select(c => PieceColor.None == game[c]).Where(x => x != true).Count();
            return ex;
        }

        private List<GameTreeSimple> CreateNewNodesP(List<GameTreeSimple> padres, GameTreeSimple root)
        {
            List<GameTreeSimple> hijos = new List<GameTreeSimple>();
            SimpleGameStatus gstatus;

            foreach (var parent in padres)
            {
                gstatus = parent.Item;
                List<Movement> lmm = new List<Movement>();

                
                foreach (var dkv in gstatus.NextPiecesA)
                {
                    int index = dkv.Key;
                    PieceName piece = dkv.Value;

                    //Generar lista de  movidas
                    lmm.AddRange(CreateMovementsNuevo(index, piece, gstatus));
                }

                //DMsg(string.Format("Procesando {0} movidas.", lmm.Count));
                foreach (var move in lmm)
                {
                    // Obtener gameStatus de Parent y clonar
                    GameTreeSimple node = CloneSimpleStatus(root, parent, parent.Item);
                    SimpleGameStatus cloned = node.Item;
                    cloned.Movement = move;
                    hijos.Add(node);
                }
            }

            return hijos;
        }

        private GameStatus GetGameFromSimple(SimpleGameStatus sgame)
        {
            GameStatus game;
            game = Factory.CreateGameStatus(sgame.Id, sgame.Nombre);

            game.Evaluation = sgame.Evaluation;
            game.Movement = sgame.Movement;
            game.CompletedRows = sgame.CompletedRows;
            game.CompletedColumns = sgame.CompletedColumns;

            game.NextPieces = new Dictionary<int, PieceName>();

            foreach (var dkv in sgame.NextPiecesA)
            {
                game.NextPieces.Add(dkv.Key, dkv.Value);
            }

            game.Cells = new List<Cell>();
            
            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    game.Cells.Add(new Cell(i, j, sgame[i, j]   ));
                }
            }

            return game;
        }

        private SimpleGameStatus GetSimpleFromGameStatus(GameStatus game)
        {
            SimpleGameStatus sg = new SimpleGameStatus();

            // Copia de propiedades necesarias 

            sg.Id = game.Id;
            sg.Nombre = game.Nombre;

            sg.NextPiecesA = new Dictionary<int, PieceName>();

            sg.NextPiecesA.Add(0, game.NextPieces[0]);
            sg.NextPiecesA.Add(1, game.NextPieces[1]);
            sg.NextPiecesA.Add(2, game.NextPieces[2]);

            // Convertir List<Cell> en arreglo bidimensional de PieceColor
            // Se eliminan 100 referencias a class Cell

            sg.CellsA = new PieceColor[Constants.BoardSize, Constants.BoardSize];

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    sg[i, j] = game[i, j].Color;
                }
            }
            return sg;
        }

        private GameTreeSimple CloneSimpleStatus(GameTreeSimple root, GameTreeSimple parent, SimpleGameStatus game)
        {
            SimpleGameStatus clonedGame = CloneSimpleGameStatus(game);

            // Reset Id
            clonedGame.Id = root.Count();

            // Reset Name
            clonedGame.Nombre = string.Format("Cloned {0}", clonedGame.Id);

            //crear
            return parent.AddChild(clonedGame);


        }

        private SimpleGameStatus CloneSimpleGameStatus(SimpleGameStatus game)
        {
            SimpleGameStatus sg = new SimpleGameStatus();

            sg.Id = game.Id;
            sg.Nombre = game.Nombre;

            sg.NextPiecesA = new Dictionary<int, PieceName>();

            foreach (var dkv in game.NextPiecesA)
            {
                sg.NextPiecesA.Add(dkv.Key, dkv.Value);
            }

            sg.CellsA = new PieceColor[Constants.BoardSize, Constants.BoardSize];


            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    sg[i, j] = game[i, j];
                }
            }

            return sg;

        }


        private List<Movement> CreateMovementsNuevo(int index, PieceName pname, SimpleGameStatus game)
        {
            List<Movement> mlist = new List<Movement>();

            List<Coord> clist = CreateValidPositionListNuevo(game, pname);
            Movement mov;

            foreach (var item in clist)
            {
                mov = new Movement(index, item, pname);
                mlist.Add(mov);
            }

            return mlist;
        }


        private List<Coord> CreateValidPositionListNuevo(SimpleGameStatus game, PieceName pname)
        {

            // obtener lista de coordenadas de celdas libres
            var freeCoord = GetFreeCells(game);

            // obtener lista de coordenadas donde es posible insertar la pieza
            //  where TestPiece retorna True;
            var qry = from cord in freeCoord
                      where TestPieceNuevo(cord, pname, game)
                      select cord;

            int count = qry.Count();
            //List<Coord> list = qry.ToList();
            //return list;

            return qry.ToList();
        }

        private List<Coord> GetFreeCells(SimpleGameStatus game)
        {
            List<Coord> freec = new List<Coord>();

            for (int i = 0; i < Constants.BoardSize; i++)
            {
                for (int j = 0; j < Constants.BoardSize; j++)
                {
                    if (game.CellsA[i, j] == PieceColor.None)
                        freec.Add(new Coord(i, j));
                }
            }
            return freec;
        }

        public bool TestPieceNuevo(Coord insertCoord, PieceName name, SimpleGameStatus gstat)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = GetPiece(name);

            // Create absolute coords list.
            realCoords = Piece.GetRealCoords(piece, insertCoord);

            // Test if all coords are within limits.
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // Test if all coords are free
            ret = TestFreeCellsNuevo(gstat, realCoords);

            return ret;
        }

        private bool TestFreeCellsNuevo(SimpleGameStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => PieceColor.None == game[c]).Where(x => x == false).Count();
            return ex == 0;
        }


    }
}
