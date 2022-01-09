using BPSolver.Objects;
using System;
using TreeCollections;

namespace BPSolver
{
    // Clase para serializar arbol de estados a disco
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
