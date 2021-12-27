using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {
        #region Solver Basico

        // Realiza iteracion por lista de NextPieces y no emplea Queue
        private void ProccessNodeB(GameTreeNode parent, GameTreeNode root)
        {
            GameStatus gstatus = parent.Item;

            //PrintMsg(string.Format("Procesando {0}.", parent.Item.Nombre));
            foreach (var dkv in gstatus.NextPieces)
            {
                int index = dkv.Key;
                PieceName piece = dkv.Value;

                //Generar lista de  movidas
                List<Movement> lmm = CreateMovements(index, piece, gstatus);

                //DMsg(string.Format("Procesando {0} movidas.", lmm.Count));
                foreach (var move in lmm)
                {
                    ProcessMove(move, parent, root);
                }
            }

            //Para cada hijo de NodoParent
            //PrintMsg(string.Format("{0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));
            var idList = parent.Children.Select(n => n.Id).ToArray();
            foreach (var id in idList)
            {
                ProccessNodeB(root[id], root);
            }


        }

        // Anidadas
        private void DMsg(string msg)
        {
            Console.WriteLine(msg);
        }

        // Reducir movidas
        // aplicar "heuristicas"  para reducir movidas 
        private void ReduceMoves(GameTreeNode parent)
        {
            var children = parent.Children;
            //PrintMsg(string.Format("RM: Nodo {0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));

            var toPrune = children.Where(n => n.Item.Evaluation.Neighbors < 1).ToList();
            if (toPrune.Count > 0)
            {
                var idList = toPrune.Select(n => n.Item.Id).ToArray();
                GameStatus game;

                foreach (int id in idList)
                {
                    game = parent[id].Item;
                    DMsg(string.Format("Nodo: {0} Vecinos: {1} Eval: {2} ", game.Nombre, game.Evaluation.Neighbors, game.Evaluation));
                    //parent[id].Detach();
                }
            }
            //PrintMsg(string.Format(" RM: Nodo {0} reducido a {1} hijos", parent.Item.Nombre, parent.Children.Count));
        }

        // Reducir hijos
        // Reducir nodos hijo en base al valor de evaluación
        private void ReduceChildren(GameTreeNode parent)
        {
            var children = parent.Children;
            DMsg(string.Format("RC: Nodo {0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));

            int maxEval = children.Max(n => n.Item.Evaluation.Total);
            int limt = Convert.ToInt32(maxEval * 0.7);
            var toPrune = children.Where(n => (n.Item.Evaluation.CompleteRoC == 0 ) && (n.Item.Evaluation.Total < limt)   );

            var idList = toPrune.Select(n => n.Id).ToArray();

            // Si la reduccion baja de  limit
            //if (2 < (parent.Children.Count - idList.Length))
            //{
            foreach (var id in idList)
            {
                DMsg(string.Format(" Eliminando: Nodo {0} Eval {1}", parent[id].Item.Nombre, parent[id].Item.Evaluation));
                parent[id].Detach();
            }

            DMsg(string.Format(" RC: Nodo {0} reducido a {1} hijos", parent.Item.Nombre, parent.Children.Count));
            //}


        }


        private void ProcessMove(Movement move, GameTreeNode parent, GameTreeNode root)
        {
            // Obtener gameStatus de Parent y clonar
            GameStatus cloned = CreateCloneChild(root, parent, parent.Item);
            // Aplicar Movimiento
            cloned.Movement = MakeMove(move, cloned);
            // Borrar pieza de la lista
            DeleteMovedPiece(move, cloned);
            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMove(move, cloned);
            // Chequear por Completamiento
            CheckCompleteAndDelete(cloned);
        }

        //  Auxiliares

        //  Chequear completamiento y eliminar completas
        private void CheckCompleteAndDelete(GameStatus game)
        {
            if (IsAnyCompleted(game))
            {
                ClearCompleted(game);
            }
        }

        // Crear clon de Nodetree
        private GameStatus CreateCloneChild(GameTreeNode root, GameTreeNode parent, GameStatus game)
        {
            GameStatus clonedGame = CloneGameStatus(game);

            // Reset Id
            clonedGame.Id = root.Count();

            // Reset Name
            clonedGame.Nombre = string.Format("Cloned {0}", clonedGame.Id);

            //crear
            parent.AddChild(clonedGame);

            return clonedGame;
        }

        #endregion
    }
}
