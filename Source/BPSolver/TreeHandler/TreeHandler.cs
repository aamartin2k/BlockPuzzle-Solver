using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    /// <summary>
    /// Implement editing/navigating functions for the game's tree structure of gamestatus.
    /// Declarations.
    /// </summary>
    internal partial class TreeHandler : ITree
    {

        private GameTreeNode _treeRoot;
        public GameTreeNode TreeRoot
        {
            get { return _treeRoot; }
            set
            {
                _treeRoot = value;
                CurrentNode = value;
            }
        }

        public GameTreeNode CurrentNode { get; set; }
        // For Debug
        //private GameTreeNode _currentNode;
        //public GameTreeNode CurrentNode
        //{
        //    get { return _currentNode; }
        //    set
        //    {
        //        _currentNode = value;
        //        Console.WriteLine(string.Format("Current Node Id: {0} Status Id: {1} Status Nombre: {2}", _currentNode.Id, _currentNode.Item.Id, _currentNode.Item.Nombre));
        //    }
        //}

        public int TreeCount
        {
            get { return TreeRoot.Count(); }
        }

        public List<GameStatus> CurrentChilds
        {
            get
            {
                List<GameStatus> children = new List<GameStatus>();

                foreach (var node in CurrentNode.Children)
                {
                    children.Add(node.Item);
                }
                return children;
            }
        }

        public bool CurrentIsIsLeaf
        {
            get { return CurrentNode.IsLeaf; }
        }

        public void CreateRootNode(GameStatus item)
        {
            TreeRoot = new GameTreeNode(item);
        }

        public void CreateChildNode(GameStatus item)
        {
            GameTreeNode newNode = CurrentNode.AddChild(item);
            CurrentNode = newNode;
        }
    }
}
