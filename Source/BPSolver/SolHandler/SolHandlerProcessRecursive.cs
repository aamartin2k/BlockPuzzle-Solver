using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver
{
    internal partial class SolHandler : ISolver
    {
        private GameTreeSimple CreateSolutionTreeRecursive(GameStatus game)
        {
            GameTreeSimple treeRoot;

            // Convertir Estado inicial GameStatus en GameTreeSimple por optimizacion
            SimpleGameStatus GStIni = GetSimpleFromGameStatus(game);
            GStIni.Nombre = RootName;
            // Solution Tree Root
            treeRoot = new GameTreeSimple(GStIni);

            //Llamada a proceso basico recursivo
            ProccessNodeRecursive(treeRoot, treeRoot);

            return treeRoot;
        }

        // Realiza iteracion por lista de NextPieces y no emplea Queue
        private void ProccessNodeRecursive(GameTreeSimple parent, GameTreeSimple root)
        {
            SimpleGameStatus gstatus = parent.Item;

            foreach (var dkv in gstatus.NextPiecesA)
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
            SimpleGameStatus cloned = CreateCloneChildNuevo(root, parent, parent.Item);
            // Aplicar Movimiento
            cloned.Movement = MakeMoveNuevo(move, cloned);
            // Borrar pieza de la lista
            DeleteMovedPieceNuevo(move, cloned);
            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMoveNuevo(move, cloned);
            // Chequear por Completamiento
            CheckCompleteAndDeleteNuevo(cloned);
        }

        private SimpleGameStatus CreateCloneChildNuevo(GameTreeSimple root, GameTreeSimple parent, SimpleGameStatus game)
        {
            SimpleGameStatus clonedGame = CloneSimpleGameStatus(game);

            // Reset Id
            clonedGame.Id = root.Count();

            // Reset Name
            clonedGame.Nombre = string.Format("Cloned {0}", clonedGame.Id);

            //crear
            parent.AddChild(clonedGame);

            return clonedGame;
        }



    }
}
