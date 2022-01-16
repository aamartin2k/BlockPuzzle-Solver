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
        private delegate GameTreeSimple CreateSolutionTree(GameStatus game);

        private CreateSolutionTree OnCreateSolution;
        private CreateSolutionTree ProcRecursive;
        private CreateSolutionTree ProcIterative;

        private void CreateDelegates()
        {
            ProcRecursive = new CreateSolutionTree(CreateSolutionTreeRecursive);
            ProcIterative = new CreateSolutionTree(CreateSolutionTreeIterative);
        }

        private GameTreeSimple FireCreateSolution(GameStatus game)
        {
            return OnCreateSolution?.Invoke(game);
        }

        private void SelectRecursive()
        {
            // Break before make Mode
            OnCreateSolution -= ProcIterative;
            OnCreateSolution += ProcRecursive;
        }

        private void SelectIterative()
        {
            OnCreateSolution -= ProcRecursive;
            OnCreateSolution += ProcIterative;
        }


    }
}
