using BPSolver.Objects;
using System;
using TreeCollections;

namespace BPSolver
{
    /// <summary>
    /// Supports serialization to disk of the game's tree structure of gamestatus.
    /// </summary>
    [Serializable]
    public class GameSerialNode : SerialTreeNode<GameSerialNode>
    {
        public GameStatus Status { get; set; }

        public GameSerialNode() { }

        public GameSerialNode(GameStatus status,
                              params GameSerialNode[] children)
            : base(children)
        {
            Status = status;
        }

    }

    

}
