using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {
        #region Solver Recursivo

       
        // Anidadas
        

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


        // Calcula valor de Movimiento aplicado
        public Eval EvaluateMove(Movement move, GameStatus game)
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
            eval.Neighbors = GetNeighborsCount(piece, move.InsertPoint, game);

            // Completion
            bool ret;
            int ccount = 0;

            ret = IsAnyCompleted(game);
            if (ret)
                ccount = CompletedCount(game);

            eval.CompleteRoC = ccount;

            return eval;
        }


        private void DeleteMovedPiece(Movement move, GameStatus game)
        {
            // Borrar pieza de Dict
            // El comando DeleteNextPieceCommand asigna PieceName.None
            // aqui se borra realmente.
            game.NextPieces.Remove(move.Index);
        }

        // Aplicar Movimiento a GameStatus
        // Se repite codigo de comandos para agilidad
        public Movement MakeMove(Movement move, GameStatus game)
        {
            // Comprobar que coincide el nombre de pieza
            bool ret = move.Name == game.NextPieces[move.Index];

            if (!ret)
                throw new Exception("No coincide el nombre de pieza de Move con Dictionary");
            // "Dibujar" pieza en board
            // Get reference to piece
            Piece piece = GetPiece(move.Name);

            // Obtener Real Coords
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);

            // ejecutar
            var ex = realCoords.Select(c => game[c].Color = piece.Color).ToList();

            return move;
        }

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
