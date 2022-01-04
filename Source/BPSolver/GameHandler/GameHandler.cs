using BPSolver.Command;
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
              
        public GameStatus CurrentStatus { get; set; }
        
        private List<GameStatus> CurrentChilds
        {
            get
            {
                List<GameStatus> children = new List<GameStatus>();

                //foreach (var node in CurrentNode.Children)
                //{
                //    children.Add(node.Item);
                //}
                return children;
            }
        }

        private bool CurrentIsIsLeaf
        {
            //get { return CurrentNode.IsLeaf; }
            get { return true; }
        }

        public GameHandler()
        {
            _commandStack = new Stack<ICommand>();
        }
       







    }
}
