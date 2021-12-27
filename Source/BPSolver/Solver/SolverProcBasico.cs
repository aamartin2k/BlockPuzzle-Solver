using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {

        public SolutionMetaStatus CreateMetaSolutionX(GameStatus game)
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

        public GameTreeNode CreateSolutionTreePBasico(GameStatus game)
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
    }
}
