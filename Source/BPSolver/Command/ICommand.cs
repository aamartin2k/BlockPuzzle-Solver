
namespace BPSolver.Command
{
    /// <summary>
    /// Defines behavior of commands
    /// </summary>
    interface ICommand
    {
        void Do();
        void Undo();
    }
}
