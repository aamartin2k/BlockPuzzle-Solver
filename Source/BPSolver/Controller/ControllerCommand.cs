using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;

namespace BPSolver
{
    public partial class Controller : IBPServer
    {
      
        private Stack<ICommand> _commandStack;

        private void ResetStack()
        {
            _commandStack = new Stack<ICommand>();
            Out_EmptyCommandStack(true);
        }

        // Command Execution and Storage
        private void ExecuteCommandDo(ICommand command)
        {
            _commandStack.Push(command);
            command.Do();
            Out_EmptyCommandStack(false);
        }

        private void ExecuteCommandUndo()
        {
            if (_commandStack.Count == 0)
            {
                Out_EmptyCommandStack(true);
                return;
            }

            ICommand command = _commandStack.Pop();
            command.Undo();
        }

        
     
        
    }
}
