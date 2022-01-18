using BPSolver.Objects;
using System.Collections.Generic;
using TreeCollections;

namespace BPSolver
{
    #region Implementacion con GameStatus
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

    #endregion

    #region Implementacion con SimpleGameStatus

    public class SimpleGameStatusEntityDefinition<TValue> : EntityDefinition<int, TValue>
      where TValue : SimpleGameStatus
    {
        public static readonly SimpleGameStatusEntityDefinition<TValue> Instance = new SimpleGameStatusEntityDefinition<TValue>();

        private SimpleGameStatusEntityDefinition()
            : base(c => c.Id,
                   EqualityComparer<int>.Default,
                   EqualityComparer<SimpleGameStatus>.Default)
        { }
    }

    // Definicion de Nodo Mutable para el arbol
    public class GameTreeSimple : MutableEntityTreeNode<GameTreeSimple, int, SimpleGameStatus>
    {

        // Constructors
        // Root Node, requiere definicion de entidad
        public GameTreeSimple(SimpleGameStatus item)
             : base(SimpleGameStatusEntityDefinition<SimpleGameStatus>.Instance, item, ErrorCheckOptions.All)
        { }

        //Child Node
        public GameTreeSimple(SimpleGameStatus item, GameTreeSimple parent)
            : base(item, parent)
        { }

        protected override GameTreeSimple Create(SimpleGameStatus item, GameTreeSimple parent)
        {
            return new GameTreeSimple(item, parent);
        }
    }

    #endregion
}

