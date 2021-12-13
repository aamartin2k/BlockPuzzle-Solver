namespace BPSolver.Command
{
    interface ICommand
    {
        void Do();
        void Undo();
    }
}
