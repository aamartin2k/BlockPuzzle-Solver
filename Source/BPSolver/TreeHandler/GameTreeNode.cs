using BPSolver.Objects;
using System.Collections.Generic;
using TreeCollections;

namespace BPSolver
{
    /// <summary>
    /// Implement how to access the entity id from the core entity object. 
    /// </summary>
    /// <typeparam name="TValue">Type of payload entity.</typeparam>
    /// <remarks>Specifically, property GameStatus.Id of type int will be used as nodes id. 
    ///  The other two EqualityComparers are set to the default equality comparers 
    /// for types int and object. Further and better explanation in: https://github.com/davidwest/TreeCollections/wiki/Entity-Definitions 
    /// </remarks>
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

   
    /// <summary>
    /// Implement a mutable tree node with a payload of type GameStatus, accessible through
    /// the Item property.
    /// </summary>
    public class GameTreeNode : MutableEntityTreeNode<GameTreeNode, int, GameStatus>
    {

        // Constructor.
        // For Root Node, requires entity definition for base constructor.
        public GameTreeNode(GameStatus item)
             : base(GameStatusEntityDefinition<GameStatus>.Instance, item, ErrorCheckOptions.All)
        { }

        // For Child Node, requires payload entity and parent node.
        public GameTreeNode(GameStatus item, GameTreeNode parent)
            : base(item, parent)
        { }

        protected override GameTreeNode Create(GameStatus item, GameTreeNode parent)
        {
            return new GameTreeNode(item, parent);
        }
    }

    
}

