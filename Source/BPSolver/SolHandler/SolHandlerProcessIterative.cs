using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver
{
    /// <summary>
    /// Implement finding solutions.
    /// Find solution iteratively.
    /// </summary>
    internal partial class SolHandler : ISolver
    {
        private GameTreeNode CreateSolutionTreeIterative(GameStatus game)
        {
            GameTreeNode treeRoot;

            GameStatus GStIni = Factory.CloneGameStatus(0, RootName, game);
 
            // Solution Tree Root
            treeRoot = new GameTreeNode(GStIni);

            List<GameTreeNode> ramas = treeRoot.SelectLeaves().ToList();

            //Llamada a proceso basico por paso
            // Cada paso se ejecuta en un nivel inferior del arbol
            // sobre los hijos del nivel superior
            for (int i = 0; i < Constants.NexPieces; i++)
            {
                ramas = CreateNewNodes(ramas, treeRoot);
                ProcessNewNodesP(ramas, treeRoot);
            }

            return treeRoot;
        }

        private void ProcessNewNodesP(List<GameTreeNode> padres, GameTreeNode root)
        {
            GameStatus cloned;

            foreach (var parent in padres)
            {
                cloned = parent.Item;

                // Aplicar Movimiento
                MakeMove(cloned);

                // Borrar pieza de la lista
                DeleteMovedPiece(cloned);
                // Evaluar Movimiento
                cloned.Evaluation = EvaluateMove(cloned);
                // Chequear por Completamiento
                //CheckCompleteAndDeleteNuevo(cloned);
                Utils.DeleteCompletedRoC(cloned);
            }

        }

        private void MakeMove( GameStatus game)
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

        private void DeleteMovedPiece(GameStatus game)
        {
            Movement move = game.Movement;

            // Borrar pieza de Dictionary
            game.NextPieces.Remove(move.Index);

        }

        private Eval EvaluateMove( GameStatus game)
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
            eval.Neighbors = GetNeighborsCount(piece, move.InsertPoint, game);

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

      
        private int GetPreference(List<Coord> coords)
        {
            var ssum = coords.Select(c => Preferences[c.Row, c.Col]).Sum();
            return ssum;
        }

        private int GetNeighborsCount(Piece piece, Coord point, GameStatus game)
        {
            //// Matriz Coord vecinos
            //List<Coord> ngbMatrix;
            //ngbMatrix = Piece.GetNeighborsMatrix(piece);

            // Coord Reales vecinos
            List<Coord> realCoords;
            realCoords = Piece.GetNeighborsRealCoords(piece, point);

            // contar celdas vecinas ocupadas (cell.IsFree == false)
            var ex = realCoords.Select(c => game.Cells[c].IsFree).Where(x => x == false).Count();

            return ex;
        }

        private List<GameTreeNode> CreateNewNodes(List<GameTreeNode> padres, GameTreeNode root)
        {
            List<GameTreeNode> hijos = new List<GameTreeNode>();
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
                    lmm.AddRange(CreateMovements(index, piece, gstatus));
                }

                foreach (var move in lmm)
                {
                    // Obtener gameStatus de Parent y clonar
                    GameTreeNode node = CreateCloneChild(root, parent, parent.Item);

                    //SimpleGameStatus cloned = node.Item;
                    //cloned.Movement = move;
                    node.Item.Movement = move;

                    hijos.Add(node);
                }
            }

            return hijos;
        }

        private GameTreeNode CreateCloneChild(GameTreeNode root, GameTreeNode parent, GameStatus game)
        {
            int id = root.Count();
            string nombre = string.Format("Cloned {0}", id);

            GameStatus clonedGame = Factory.CloneGameStatus(id, nombre, game);

            //crear y retornar Nodo hijo
            return parent.AddChild(clonedGame);
        }


        private List<Movement> CreateMovements(int index, PieceName pname, GameStatus game)
        {
            List<Movement> mlist = new List<Movement>();

            List<Coord> clist = CreateValidPositionList(game, pname);
            Movement mov;

            foreach (var item in clist)
            {
                mov = new Movement(index, item, pname);
                mlist.Add(mov);
            }

            return mlist;
        }


        private List<Coord> CreateValidPositionList(GameStatus game, PieceName pname)
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
