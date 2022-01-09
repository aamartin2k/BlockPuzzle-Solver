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
        public void In_Solution(GameStatus game) 
        {
            GameTreeSimple treeRoot;

            // Timing
            System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
            _stopWatch.Start();

            // create tree of possible moves
            // iterative procedure
            treeRoot = CreateSolutionTree(game);
            // recursive procedure
            //treeRoot = CreateSolutionTreeRecursive(game);

            _stopWatch.Stop();

            // crear resumen de soluciones
            var leaves = treeRoot.SelectLeaves();

            List<Solution> solutions = new List<Solution>();

            foreach (var node in leaves)
            {
                // Seleccionar todos hacia arriba e invertir
                var invSol = node.SelectPathUpward().Reverse();

                // Crear estados de solucion a partir del original game
                solutions.Add(CreateSolution(invSol, game));
            }

            // crear inf de retorno
            SolutionMetaStatus meta = new SolutionMetaStatus(solutions);
            meta.ProcTime = _stopWatch.Elapsed.ToString();
            meta.NodeCount = treeRoot.Count();

            OnOut_UpdateSolutionBoard(meta);
        }

        private Solution CreateSolution(IEnumerable<GameTreeSimple> seqNodes, GameStatus initial)
        {

            //  Crear objetos GameStatus para solucion
            // crear total eval y dictionary
            Eval TotalEval = Eval.GetTotalEval();

            Dictionary<int, GameStatus> StatusList;

            StatusList = CreateGameStatusSolution(seqNodes, initial);

            foreach (var dkv in StatusList)
            {
                GameStatus game = dkv.Value;

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

        private Dictionary<int, GameStatus> CreateGameStatusSolution(IEnumerable<GameTreeSimple> seqNodes, GameStatus initial)
        {
            
            Dictionary<int, GameStatus> StatusList = new Dictionary<int, GameStatus>();

            GameStatus child = null;

            foreach (GameTreeSimple nod in seqNodes)
            {
                child = GetGameFromSimple(nod.Item);
                StatusList.Add(child.Id, child);
            }
            return StatusList;
        }
    }
}
