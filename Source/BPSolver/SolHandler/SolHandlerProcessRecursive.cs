using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver
{
    /// <summary>
    /// Implement finding solutions.
    /// Find solution recursively.
    /// </summary>
    internal partial class SolHandler : ISolver
    {
        private GameTreeNode CreateSolutionTreeRecursive(GameStatus game)
        {
            GameTreeNode treeRoot;

            GameStatus GStIni = Factory.CloneGameStatus(0, RootName, game);
            // Solution Tree Root
            treeRoot = new GameTreeNode(GStIni);

            //Llamada a proceso basico recursivo
            ProccessNodeRecursive(treeRoot, treeRoot);

            return treeRoot;
        }

        // Realiza iteracion por lista de NextPieces y no emplea Queue
        private void ProccessNodeRecursive(GameTreeNode parent, GameTreeNode root)
        {
            GameStatus gstatus = parent.Item;

            foreach (var dkv in gstatus.NextPieces)
            {
                int index = dkv.Key;
                PieceName piece = dkv.Value;

                //Generar lista de  movidas
                List<Movement> lmm = CreateMovements(index, piece, gstatus);

                foreach (var move in lmm)
                {
                    ProcessMoveNuevo(move, parent, root);
                }
            }

            //Para cada hijo de NodoParent
           
            var idList = parent.Children.Select(n => n.Id).ToArray();
            foreach (var id in idList)
            {
                ProccessNodeRecursive(root[id], root);
            }


        }

        private void ProcessMoveNuevo(Movement move, GameTreeNode parent, GameTreeNode root)
        {
            // Obtener gameStatus de Parent y clonar
            GameStatus cloned = CreateCloneChildNuevo(root, parent, parent.Item);

            // Aplicar Movimiento
            cloned.Movement = move;
            MakeMove(cloned);

            // Borrar pieza de la lista
            DeleteMovedPiece(cloned);

            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMove(cloned);

            // Chequear por Completamiento
            Utils.DeleteCompletedRoC(cloned);
        }

        private GameStatus CreateCloneChildNuevo(GameTreeNode root, GameTreeNode parent, GameStatus game)
        {      
            int id = root.Count();
            string nombre = string.Format("Cloned {0}", id);

            GameStatus clonedGame = Factory.CloneGameStatus(id, nombre, game);

            parent.AddChild(clonedGame);
            return clonedGame;
        }



    }
}
