using BPSolver.Command;
using System.Collections.Generic;

namespace BPSolver.Game
{
    internal partial class GameHandler : IGame
    {
        

        #region Gestion de Command Stack 
        private Stack<ICommand> _commandStack; 

        private void ResetCommandStack()
        {
            _commandStack = new Stack<ICommand>();
            OnOut_EmptyCommandStack(true);
        }

        // Command Execution and Storage
        private void ExecuteCommandDo(ICommand command)
        {
            _commandStack.Push(command);
            command.Do();
            OnOut_EmptyCommandStack(false);
        }

        // Command Retrieval and Execution
        private void ExecuteCommandUndo()
        {
            if (_commandStack.Count > 0)
            {
                ICommand command = _commandStack.Pop();
                command.Undo();
            }
        }

        #endregion

       
    }
}
