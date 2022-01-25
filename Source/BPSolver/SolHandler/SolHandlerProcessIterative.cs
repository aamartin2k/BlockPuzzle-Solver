using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver
{
    /// <summary>
    /// Implement finding solutions.
    /// Find solution  iteratively.
    /// </summary>
    internal partial class SolHandler : ISolver
    {
        private GameTreeSimple CreateSolutionTreeIterative(GameStatus game)
        {
            GameTreeSimple treeRoot;

            GameStatus GStIni = Factory.CloneGameStatus(0, RootName, game);
 
            // Solution Tree Root
            treeRoot = new GameTreeSimple(GStIni);

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
            GameStatus cloned;

            foreach (var parent in padres)
            {
                cloned = parent.Item;

                // Aplicar Movimiento
                MakeMoveNuevo(cloned);

                // Borrar pieza de la lista
                DeleteMovedPieceNuevo(cloned);
                // Evaluar Movimiento
                cloned.Evaluation = EvaluateMoveNuevo(cloned);
                // Chequear por Completamiento
                //CheckCompleteAndDeleteNuevo(cloned);
                Utils.DeleteCompletedRoC(cloned);
            }

        }

        private void MakeMoveNuevo( GameStatus game)
        {
            Movement move = game.Movement;
            // Comprobar que coincide el nombre de pieza
            bool ret = move.Name == game.NextPieces[move.Index];

            if (!ret)
                throw new Exception("No coincide el nombre de pieza de Move con Dictionary");
            // "Dibujar" pieza en board
            // Get reference to piece
            Piece piece = PieceSet.GetPiece(move.Name);

            // Obtener Real Coords
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);

            // ejecutar Select y asignar color a seleccion
            //var ex = realCoords.Select(c => game[c] = piece.Color).ToList();
            foreach (var coord in realCoords)
            {
                game.Cells[coord].Color = piece.Color;
            }

            //return move;
        }

        private void DeleteMovedPieceNuevo(GameStatus game)
        {
            Movement move = game.Movement;

            // Borrar pieza de Dictionary
            game.NextPieces.Remove(move.Index);

        }

        private Eval EvaluateMoveNuevo( GameStatus game)
        {
            Movement move = game.Movement;

            Eval eval = Eval.GetNewEval();

            Piece piece = PieceSet.GetPiece(move.Name);
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

            //ret = IsAnyCompletedNuevo(game);
            ret = Utils.IsAnyCompleted(game);
            if (ret)
                ccount = Utils.CompletedCount(game);

            eval.CompleteRoC = ccount;

            return eval;
        }

        //private int CompletedCountNuevo(SimpleGameStatus game)
        //{
        //    return CompletedRowsCount(game) +
        //           CompletedColumnsCount(game);
        //}

        //// Contar Filas completas
        //private int CompletedRowsCount(SimpleGameStatus game)
        //{
        //    int count = 0;

        //    // Count how many rows
        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        if (IsRowCompletedNuevo(game, i))
        //            count++;
        //    }

        //    return count;
        //}

        //// Contar Columnas completas
        //private int CompletedColumnsCount(SimpleGameStatus game)
        //{
        //    int count = 0;

        //    // Calculate how many rows
        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        if (IsColumnCompletedNuevo(game, i))
        //            count++;
        //    }
        //    return count;
        //}

        //private void CheckCompleteAndDeleteNuevo(SimpleGameStatus game)
        //{
        //    if (IsAnyCompletedNuevo(game))
        //    {
        //        ClearCompletedNuevo(game);
        //    }
        //}

        //private void ClearCompletedNuevo(SimpleGameStatus status)
        //{
        //    int count;
        //    bool ret;
        //    int[] listRow = new int[] { };
        //    int[] listCol = new int[] { };

        //    // buscar filas
        //    ret = IsAnyRowCompletedNuevo(status);
        //    if (ret)
        //    {
        //        // contar ANTES de Borrar
        //        count = CompletedRowsCount(status);
        //        status.CompletedRows += count;

        //        listRow = GetListRowsCompleted(status);
        //    }

        //    // buscar columnas
        //    ret = IsAnyColumnCompletedNuevo(status);
        //    if (ret)
        //    {
        //        // contar
        //        count = CompletedColumnsCount(status);
        //        status.CompletedColumns += count;

        //        // guardar indices en lista
        //        listCol = GetListColumnsCompleted(status);
        //    }

        //    // eliminar filas y columnas con foreach
        //    foreach (var row in listRow)
        //    {
        //        ClearRow(status, row);
        //    }

        //    foreach (var col in listCol)
        //    {
        //        ClearColumn(status, col);
        //    }
        //}


        //// Eliminar
        //private void ClearColumn(SimpleGameStatus game, int index)
        //{
        //    //var list = GetColumn(game, index);
        //    //list.Select(c => c.Color = PieceColor.None).ToList();
        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        game[i, index] = PieceColor.None;
        //    }
        //}

        //private void ClearRow(SimpleGameStatus game, int index)
        //{
        //    //var list = GetRow(game, index);
        //    //list.Select(c => c.Color = PieceColor.None).ToList();
        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        game[index, i] = PieceColor.None;
        //    }
        //}


        //// Listar Indice de Filas completas
        //private int[] GetListRowsCompleted(SimpleGameStatus game)
        //{
        //    List<int> list = new List<int>();

        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        if (IsRowCompletedNuevo(game, i))
        //            list.Add(i);
        //    }

        //    return list.ToArray();
        //}

        //private int[] GetListColumnsCompleted(SimpleGameStatus game)
        //{
        //    List<int> list = new List<int>();

        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        if (IsColumnCompletedNuevo(game, i))
        //            list.Add(i);
        //    }

        //    return list.ToArray();
        //}
        
        //private bool IsAnyCompletedNuevo(SimpleGameStatus game)
        //{
        //    return IsAnyRowCompletedNuevo(game) | IsAnyColumnCompletedNuevo(game);
        //}

        //private bool IsAnyRowCompletedNuevo(SimpleGameStatus game)
        //{

        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        if (IsRowCompletedNuevo(game, i))
        //            return true;
        //    }

        //    return false;
        //}

        //private bool IsAnyColumnCompletedNuevo(SimpleGameStatus game)
        //{

        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        if (IsColumnCompletedNuevo(game, i))
        //            return true;
        //    }

        //    return false;

        //}

        //private bool IsColumnCompletedNuevo(SimpleGameStatus game, int col)
        //{
    
        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        //if (!game.CellsA[i, col])
        //        if (game[i, col] == PieceColor.None)
        //            return false;
        //    }

        //    return true;
        //}

        //private bool IsRowCompletedNuevo(SimpleGameStatus game, int row)
        //{

        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        if (game[row, i] == PieceColor.None)
        //            return false;
        //    }

        //    return true;
        //}

        private int GetPreference(List<Coord> coords)
        {
            var ssum = coords.Select(c => Preferences[c.Row, c.Col]).Sum();
            return ssum;
        }

        private int GetNeighborsCountNuevo(Piece piece, Coord point, GameStatus game)
        {
            // Matriz Coord vecinos
            List<Coord> ngbMatrix;
            ngbMatrix = Piece.GetNeighborsMatrix(piece);

            // Coord Reales vecinos
            List<Coord> realCoords;
            realCoords = Piece.GetNeighborsRealCoords(point, ngbMatrix);

            //var ex = realCoords.Select(c => PieceColor.None == game.Cells[c].Color).Where(x => x != true).Count();
            var ex = realCoords.Select(c => game.Cells[c].IsFree).Where(x => x != true).Count();

            return ex;
        }

        private List<GameTreeSimple> CreateNewNodesP(List<GameTreeSimple> padres, GameTreeSimple root)
        {
            List<GameTreeSimple> hijos = new List<GameTreeSimple>();
            GameStatus gstatus;

            foreach (var parent in padres)
            {
                gstatus = parent.Item;
                List<Movement> lmm = new List<Movement>();

                
                foreach (var dkv in gstatus.NextPieces)
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
                    //SimpleGameStatus cloned = node.Item;
                    //cloned.Movement = move;
                    node.Item.Movement = move;

                    hijos.Add(node);
                }
            }

            return hijos;
        }

        //private GameStatus GetGameFromSimple(SimpleGameStatus sgame)
        //{
        //    GameStatus game;
        //    game = Factory.CreateGameStatus(sgame.Id, sgame.Nombre);

        //    game.Evaluation = sgame.Evaluation;
        //    game.Movement = sgame.Movement;
        //    game.CompletedRows = sgame.CompletedRows;
        //    game.CompletedColumns = sgame.CompletedColumns;

        //    game.NextPieces = new Dictionary<int, PieceName>();

        //    foreach (var dkv in sgame.NextPiecesA)
        //    {
        //        game.NextPieces.Add(dkv.Key, dkv.Value);
        //    }
            
        //    DenseSCellArray<SCell> _Cells;
        //    _Cells = new DenseSCellArray<SCell>(Constants.BoardSize, Constants.BoardSize);
        //    game.Cells = _Cells;

          
        //    SCell cell;
        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        for (int j = 0; j < Constants.BoardSize; j++)
        //        {
        //            cell = new SCell(i, j, sgame[i, j]);
        //            _Cells[i, j] = cell;
        //        }
        //    }

        //    return game;
        //}

        //private SimpleGameStatus GetSimpleFromGameStatus(GameStatus game)
        //{
        //    SimpleGameStatus sg = new SimpleGameStatus();

        //    // Copia de propiedades necesarias 

        //    sg.Id = game.Id;
        //    sg.Nombre = game.Nombre;

        //    sg.NextPiecesA = new Dictionary<int, PieceName>();

        //    sg.NextPiecesA.Add(0, game.NextPieces[0]);
        //    sg.NextPiecesA.Add(1, game.NextPieces[1]);
        //    sg.NextPiecesA.Add(2, game.NextPieces[2]);

        //    // Convertir List<Cell> en arreglo bidimensional de PieceColor
        //    // Se eliminan 100 referencias a class Cell

        //    sg.CellsA = new PieceColor[Constants.BoardSize, Constants.BoardSize];

        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        for (int j = 0; j < Constants.BoardSize; j++)
        //        {
        //            sg[i, j] = game.Cells[i, j].Color;
        //        }
        //    }
        //    return sg;
        //}

        private GameTreeSimple CloneSimpleStatus(GameTreeSimple root, GameTreeSimple parent, GameStatus game)
        {
            //SimpleGameStatus clonedGame = CloneSimpleGameStatus(game);

            int id = root.Count();
            string nombre = string.Format("Cloned {0}", id);

            GameStatus clonedGame = Factory.CloneGameStatus(id, nombre, game);
           
            //crear Nodo hijo
           return parent.AddChild(clonedGame);
        }

        //private SimpleGameStatus CloneSimpleGameStatus(SimpleGameStatus game)
        //{
        //    SimpleGameStatus sg = new SimpleGameStatus();

        //    sg.Id = game.Id;
        //    sg.Nombre = game.Nombre;

        //    sg.NextPiecesA = new Dictionary<int, PieceName>();

        //    foreach (var dkv in game.NextPiecesA)
        //    {
        //        sg.NextPiecesA.Add(dkv.Key, dkv.Value);
        //    }

        //    sg.CellsA = new PieceColor[Constants.BoardSize, Constants.BoardSize];


        //    for (int i = 0; i < Constants.BoardSize; i++)
        //    {
        //        for (int j = 0; j < Constants.BoardSize; j++)
        //        {
        //            sg[i, j] = game[i, j];
        //        }
        //    }

        //    return sg;

        //}


        private List<Movement> CreateMovementsNuevo(int index, PieceName pname, GameStatus game)
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


        private List<Coord> CreateValidPositionListNuevo(GameStatus game, PieceName pname)
        {

            // obtener lista de coordenadas de celdas libres
            var freeCoord = GetFreeCells(game);

            // obtener lista de coordenadas donde es posible insertar la pieza
            //  where TestPiece retorna True;
            var qry = from cord in freeCoord
                      where Utils.TestPiece(cord, pname, game)
                      select cord;

            //int count = qry.Count();
            //List<Coord> list = qry.ToList();
            //return list;

            return qry.ToList();
        }

        private List<Coord> GetFreeCells(GameStatus game)
        {
            List<Coord> freec = new List<Coord>();

            //for (int i = 0; i < Constants.BoardSize; i++)
            //{
            //    for (int j = 0; j < Constants.BoardSize; j++)
            //    {
            //        if (game.Cells[i, j].Color == PieceColor.None)
            //            freec.Add(new Coord(i, j));
            //    }
            //}
   
            var fcells = game.Cells.Where(x => x.IsFree);
            foreach (var item in fcells)
            {
                freec.Add(new Coord(item.Row, item.Col));
            }

            return freec;
        }

        //public bool TestPieceNuevo(Coord insertCoord, PieceName name, SimpleGameStatus gstat)
        //{
        //    List<Coord> realCoords;
        //    // Get reference to piece
        //    Piece piece = PieceSet.GetPiece(name);

        //    // Create absolute coords list.
        //    realCoords = Piece.GetRealCoords(piece, insertCoord);

        //    // Test if all coords are within limits.
        //    bool ret = Utils.TestRealCoords(realCoords);
        //    if (!ret)
        //        return false;

        //    // Test if all coords are free
        //    ret = TestFreeCellsNuevo(gstat, realCoords);

        //    return ret;
        //}

        //private bool TestFreeCellsNuevo(SimpleGameStatus game, List<Coord> realCoords)
        //{
        //    var ex = realCoords.Select(c => PieceColor.None == game[c]).Where(x => x == false).Count();
        //    return ex == 0;
        //}


    }
}
