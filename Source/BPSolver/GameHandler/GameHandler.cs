using BPSolver.Command;
using BPSolver.Objects;

namespace BPSolver.Game
{
    /// <summary>
    /// Implement editing functions for game status.
    /// Declarations.
    /// </summary>
    internal partial class GameHandler : IGame
    {
        private ICommandStack _CommandStack;

        public GameStatus CurrentStatus { get; set; }
        

        public GameHandler(ICommandStack commStack)
        {
            _CommandStack = commStack;
        }
       







    }
}
