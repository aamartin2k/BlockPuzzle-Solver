using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {

        private SolutionMetaStatus CreateMetaSolutionX(GameStatus game)
        {
            GameTreeNode treeRoot;

            // crear arbol de games
            treeRoot = CreateSolutionTreePBasico(game);
    
            // crear resumen de soluciones
            var ramas = treeRoot.SelectLeaves();

            List<Solution> solutions = new List<Solution>();

            foreach (GameTreeNode item in ramas)
            {
                // Seleccionar todos hacia arriba e invertir
                var invSol = item.SelectPathUpward().Reverse();

                solutions.Add(CreateSolution(invSol));
            }

            // crear inf de retorno
            SolutionMetaStatus meta = new SolutionMetaStatus(solutions);
            Console.WriteLine("*** Cant Nodos: {0} ***", treeRoot.Count());

            return meta;
        }

        private Solution CreateSolution(IEnumerable<GameTreeNode> seqNodes)
        {
            GameStatus game;

            // crear total eval y dictionary
            Eval TotalEval = Eval.GetTotalEval();
            Dictionary<int, GameStatus> StatusList = new Dictionary<int, GameStatus>();

            foreach (GameTreeNode nod in seqNodes)
            {
                game = nod.Item;
                StatusList.Add(game.Id, game);

                // saltando GameStatus inicial, que no tiene Eval
                if (game.Nombre != RootName)
                {
                    TotalEval.PieceSize += game.Evaluation.PieceSizeTotal;
                    TotalEval.Preference += game.Evaluation.PreferenceTotal;
                    TotalEval.Neighbors += game.Evaluation.NeighborsTotal;
                    TotalEval.CompleteRoC += game.Evaluation.CompleteRoCTotal;
                }
            }

            Solution sol = new Solution(TotalEval, StatusList);
            return sol;
        }

        private GameTreeNode CreateSolutionTreePBasico(GameStatus game)
        {
            //Crear SolTree y RootNode con copia de GStIni
            GameTreeNode treeRoot;

            // Clon de Estado inicial para no modificarlo
            GameStatus GStIni = CloneGameStatus(game);
            GStIni.Nombre = RootName;

            // Solution Tree Root
            treeRoot = new GameTreeNode(GStIni);

            //DMsg("Iniciando proceso");

            //Llamada a proceso basico recursivo
            ProccessNodeB(treeRoot, treeRoot);

            return treeRoot;
        }

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

    }
}
