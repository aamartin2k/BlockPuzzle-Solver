namespace BPSolver.Command
{
    internal interface ICommandStack
    {
        void ResetCommandStack();
        void ExecuteCommandDo(ICommand command);
        bool ExecuteCommandUndo();
    }
}
