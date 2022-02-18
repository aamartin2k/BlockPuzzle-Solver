using BPSolver.Objects;

namespace BPSolver
{
    /// <summary>
    /// Implement finding solutions.
    /// Implement selection of solving procedure by client code.
    /// </summary>
    /// <remarks>A switch controller implemented according to 
    /// Ted Faison, 'Even-Based Programming' 2006 </remarks>
    internal partial class SolHandler : ISolver
    {
        private delegate GameTreeNode CreateSolutionTree(GameStatus game);

        private CreateSolutionTree OnCreateSolution;
        private CreateSolutionTree ProcRecursive;
        private CreateSolutionTree ProcIterative;

        // Create delegates for solving procedures.
        private void CreateDelegates()
        {
            ProcRecursive = new CreateSolutionTree(CreateSolutionTreeRecursive);
            ProcIterative = new CreateSolutionTree(CreateSolutionTreeIterative);
        }

        // Invoke solving procedure via stored delegate.
        private GameTreeNode FireCreateSolution(GameStatus game)
        {
            return OnCreateSolution?.Invoke(game);
        }

        // Do switch for recursive procedure.
        private void SelectRecursive()
        {
            // Break before make Mode.
            OnCreateSolution -= ProcIterative;
            OnCreateSolution += ProcRecursive;
        }

        // Do switch for iterative procedure.
        private void SelectIterative()
        {
            OnCreateSolution -= ProcRecursive;
            OnCreateSolution += ProcIterative;
        }


    }
}
