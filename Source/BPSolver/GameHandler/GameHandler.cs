using BPSolver.Command;
using BPSolver.Objects;
using System.Collections.Generic;

namespace BPSolver.Game
{
    internal partial class GameHandler : IGame
    {
              
        public GameStatus CurrentStatus { get; set; }
        

        public GameHandler()
        {
            _commandStack = new Stack<ICommand>();
        }
       







    }
}
