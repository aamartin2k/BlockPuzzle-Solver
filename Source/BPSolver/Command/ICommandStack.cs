namespace BPSolver.Command
{
    /// <summary>
    /// Defines behavior of command stack.
    /// </summary>
    internal interface ICommandStack
    {
        void ResetCommandStack();
        void ExecuteCommandDo(ICommand command);
        bool ExecuteCommandUndo();
    }
}
