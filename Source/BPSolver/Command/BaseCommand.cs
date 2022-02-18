using BPSolver.Objects;

namespace BPSolver.Command
{
    /// <summary>
    /// Defines common behavior of commands.
    /// </summary>
    internal class BaseCommand : ICommand
    {
        // Context upon wich all derived commands will act.
        protected GameStatus Context { get; private set; }

        public BaseCommand(GameStatus context)
        {
            Context = context;
        }
        public virtual void Do()
        { }

        public virtual void Undo()
        { }
    }
}
