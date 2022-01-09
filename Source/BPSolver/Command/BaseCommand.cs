using BPSolver.Objects;

namespace BPSolver.Command
{
    internal class BaseCommand : ICommand
    {
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
