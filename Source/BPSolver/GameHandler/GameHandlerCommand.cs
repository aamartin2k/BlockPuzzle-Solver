using BPSolver.Command;

namespace BPSolver.Game
{
    /// <summary>
    /// Implement editing functions for game status.
    /// Command Stack features.
    /// </summary>
    internal partial class GameHandler : IGame
    {

        private void ResetCommandStack()
        {
            _CommandStack.ResetCommandStack();
            OnOut_EmptyCommandStack(true);
        }

        private void ExecuteCommandDo(ICommand command)
        {
            _CommandStack.ExecuteCommandDo(command);
            OnOut_EmptyCommandStack(false);
        }

        private void ExecuteCommandUndo()
        {
            bool ret;
            ret = _CommandStack.ExecuteCommandUndo();

            OnOut_EmptyCommandStack(ret);
        }

    }
}
