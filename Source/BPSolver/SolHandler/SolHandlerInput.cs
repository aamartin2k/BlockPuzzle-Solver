using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    /// <summary>
    /// Implement finding solutions.
    /// Services inputs from IOHandler.
    /// </summary>
    internal partial class SolHandler : ISolver
    {
        // Switch on Solution process
        public void In_SelectRecursive()
        {
            SelectRecursive();
        }
        public void In_SelectIterative()
        {
            SelectIterative();
        }


        public void In_Solution(GameStatus game) 
        {
            GameTreeNode treeRoot;

            // Timing.
            System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
            _stopWatch.Start();

            // Create tree of possible moves.
            // Using iterative procedure directly (before implement the switch).
            //treeRoot = CreateSolutionTreeIterative(game);
            // Using recursive procedure.
            //treeRoot = CreateSolutionTreeRecursive(game);

            // Switching procedures with dynamic late binding
            // accessible by external client code
            treeRoot = FireCreateSolution(game);

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

        private Solution CreateSolution(IEnumerable<GameTreeNode> seqNodes, GameStatus initial)
        {

            //  Crear objetos GameStatus para solucion
            // crear total eval y dictionary
            Eval TotalEval = Eval.GetTotalEval();

            Dictionary<int, GameStatus> StatusList;

            StatusList = CreateGameStatusSolution(seqNodes);

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

        private Dictionary<int, GameStatus> CreateGameStatusSolution(IEnumerable<GameTreeNode> seqNodes)
        {
            
            Dictionary<int, GameStatus> StatusList = new Dictionary<int, GameStatus>();

            GameStatus child = null;

            foreach (GameTreeNode nod in seqNodes)
            {
                child = nod.Item;
                StatusList.Add(child.Id, child);
            }
            return StatusList;
        }
    }
}
