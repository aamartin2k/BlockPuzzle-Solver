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
        private GameTreeSimple CreateSolutionTreeRecursive(GameStatus game)
        {
            GameTreeSimple treeRoot;

            GameStatus GStIni = Factory.CloneGameStatus(0, RootName, game);
            // Solution Tree Root
            treeRoot = new GameTreeSimple(GStIni);

            //Llamada a proceso basico recursivo
            ProccessNodeRecursive(treeRoot, treeRoot);

            return treeRoot;
        }

        // Realiza iteracion por lista de NextPieces y no emplea Queue
        private void ProccessNodeRecursive(GameTreeSimple parent, GameTreeSimple root)
        {
            GameStatus gstatus = parent.Item;

            foreach (var dkv in gstatus.NextPieces)
            {
                int index = dkv.Key;
                PieceName piece = dkv.Value;

                //Generar lista de  movidas
                List<Movement> lmm = CreateMovementsNuevo(index, piece, gstatus);

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

        private void ProcessMoveNuevo(Movement move, GameTreeSimple parent, GameTreeSimple root)
        {
            // Obtener gameStatus de Parent y clonar
            GameStatus cloned = CreateCloneChildNuevo(root, parent, parent.Item);

            // Aplicar Movimiento
            cloned.Movement = move;
            MakeMoveNuevo(cloned);

            // Borrar pieza de la lista
            DeleteMovedPieceNuevo(cloned);

            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMoveNuevo(cloned);

            // Chequear por Completamiento
            Utils.DeleteCompletedRoC(cloned);
        }

        private GameStatus CreateCloneChildNuevo(GameTreeSimple root, GameTreeSimple parent, GameStatus game)
        {      
            int id = root.Count();
            string nombre = string.Format("Cloned {0}", id);

            GameStatus clonedGame = Factory.CloneGameStatus(id, nombre, game);

            parent.AddChild(clonedGame);
            return clonedGame;
        }



    }
}
