using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class GameHandler : IGame
    {
        #region Gestion de Command Stack en cada GameStatus

        #endregion

        #region Gestion de Command Stack Unico
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

        private void ExecuteCommandUndo()
        {
            if (_commandStack.Count == 0)
            {
                OnOut_EmptyCommandStack(true);
                return;
            }

            ICommand command = _commandStack.Pop();
            command.Undo();
        }
        #endregion

       
    }
}
