using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Command
{
    /// <summary>
    /// Implement functions of command stack.
    /// </summary>
    internal class CommandStack : ICommandStack
    {

        private Stack<ICommand> _commandStack;

        // Constructor
        public CommandStack()
        {
            ResetCommandStack();
        }


        public void ResetCommandStack()
        {
            _commandStack = new Stack<ICommand>();
        }


        public void ExecuteCommandDo(ICommand command)
        {
            _commandStack.Push(command);
            command.Do();
        }


        public bool ExecuteCommandUndo()
        {
            if (_commandStack.Count > 0)
            {
                ICommand command = _commandStack.Pop();
                command.Undo();

                return false;
            }
            else
                return true;
        }

    }
}
