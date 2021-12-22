using System;
using System.Collections.Generic;
using System.Linq;
using BPSolver.Objects;
using TreeCollections;

namespace BPSolver
{

    // Definicion de Entidad Item
    // Define como recuperar Id de la entidad Payload
    //  y como comparar Id. 
    public class GameStatusEntityDefinition<TValue> : EntityDefinition<int, TValue>
       where TValue : GameStatus
    {
        public static readonly GameStatusEntityDefinition<TValue> Instance = new GameStatusEntityDefinition<TValue>();

        private GameStatusEntityDefinition()
            : base(c => c.Id,
                   EqualityComparer<int>.Default,
                   EqualityComparer<GameStatus>.Default)
        { }
    }

    // Definicion de Nodo Mutable para el arbol
    public class GameTreeNode : MutableEntityTreeNode<GameTreeNode, int, GameStatus>
    {

        // Constructors
        // Root Node, requiere definicion de entidad
        public GameTreeNode(GameStatus item)
             : base(GameStatusEntityDefinition<GameStatus>.Instance, item, ErrorCheckOptions.All)
        { }

        //Child Node
        public GameTreeNode(GameStatus item, GameTreeNode parent)
            : base(item, parent)
        { }

        protected override GameTreeNode Create(GameStatus item, GameTreeNode parent)
        {
            return new GameTreeNode(item, parent);
        }
    }
}

