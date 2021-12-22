using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;
using TreeCollections;

namespace BPSolver
{
    public partial class Controller : IBPServer
    {
        // Secuencia de estados del juego: GameStatus

        // private 
        //private GameTreeNode _currentNode;
        private GameTreeNode _treeRoot;

        // Public 
        private GameTreeNode TreeRoot
        {
            get { return _treeRoot; }
            set
            {
                _treeRoot = value;
                CurrentNode = value;
            }
        }
        // Current
        private GameTreeNode CurrentNode { get; set; }
        //{
        //    get { return _currentNode; }
        //    set { _currentNode = value; }
        //}

        private GameStatus CurrentStatus
        {
            get { return CurrentNode.Item; }
        }

        private List<GameStatus> CurrentChilds
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

        private bool CurrentIsIsLeaf
        {
            get { return CurrentNode.IsLeaf; }
        }
        // Creation 
        private void CreateRootNode(GameStatus item)
        {
            // GameStatus para Root debe tener default Id = 0 
            // los demas hijos se les asigna Id igual a Count
            _treeRoot = new GameTreeNode(item);
            CurrentNode = _treeRoot;

        }

        // Browse Sequence

        private bool MoveNext()
        {
            if (CurrentNode.Children.Count == 1)
            {
                CurrentNode = CurrentNode.Children[0];
                return true;
            }
            else
                return false;
        }
        //private GameStatus MoveNext()
        //{
        //    GameStatus st = null;

        //    // chequear condiciones
        //    // Si tiene 0 o mas de un hijo no se puede avanzar
        //    if (_currentNode.Children.Count == 1)
        //    {
        //        _currentNode = _currentNode.Children[0];
        //        st = _currentNode.Item;
        //    }


        //    return st;
        //}

        private bool MovePrevious()
        {
            if (!CurrentNode.IsRoot)
            {
                CurrentNode = CurrentNode.Parent;
                return true;
            }
            else
                return false;
        }

        //private GameStatus MovePrevious()
        //{
        //    GameStatus st = null;

        //    // chequear condiciones
        //    // Si es padre no se puede retroceder
        //    if (!_currentNode.IsRoot)
        //    {
        //        _currentNode = _currentNode.Parent;
        //        st = _currentNode.Item;
        //    }
        //    return st;
        //}

        private bool MoveLast()
        {
            bool ret = false;

            while (CurrentNode.Children.Count == 1)
            {
                CurrentNode = CurrentNode.Children[0];
                ret = true;
            }

            return ret;
        }

        //private GameStatus MoveLast()
        //{
        //    GameStatus st = null;

        //    while (_currentNode.Children.Count == 1)
        //    {
        //        _currentNode = _currentNode.Children[0];
        //        st = _currentNode.Item;
        //    }

        //    return st;
        //}

        private bool MoveFirst()
        {
            if (!CurrentNode.IsRoot)
            {
                CurrentNode = _treeRoot;
                return true;
            }

            return false;
        }

        private GameStatus MoveToChild(int id)
        {
            GameStatus st;

            CurrentNode = _treeRoot[id];
            st = CurrentNode.Item;

            return st;
        }

        // Add childs
        // Crea Child de Current y lo hace Current
        // comprobar si requiere retornar GameStatus
        private void AddChild(GameStatus child)
        {
            GameTreeNode newNode;
            //crear
            newNode = CurrentNode.AddChild(child);

            // hacer current
            CurrentNode = newNode;
        }

        // Crea Child de Current y lo hace Current
        // comprobar si requiere retornar GameStatus
        private void AddChildStay(GameStatus child)
        {
            CurrentNode.AddChild(child);
        }


        //Misc Test
        // Count
        private int Count()
        {
            return _treeRoot.Count();
        }

        // Rename
        private void Rename(int id, string name)
        {
            _treeRoot[id].Item.Nombre = name;
        }

        // SelectPathUpward
        private IEnumerable<GameTreeNode> SelectPathUpward()
        {
            return CurrentNode.SelectPathUpward();
        }
        

        // Operations
        // Crear clon de estado actual, incrementar Id y hace  current
        private void CreateCloneChild()
        {
            GameStatus cloned = Clone(CurrentStatus);

            // Reset Id
            cloned.SetId(Count());

            // Reset Name
            cloned.Nombre = string.Format("Cloned {0}", Count());

            AddChild(cloned);
        }

        // Serialize to Disk
        private void BinSerialize(string file, GameTreeNode tree)
        {
            // Creando arbol ligero serializable
            GameSerialNode dataRoot = new GameSerialNode(_treeRoot.Item);

            // Copiando Tree
            tree.CopyTo(dataRoot, (n, d) =>
                {
                    d.Status = n.Item;
                });

            // Serializando con Binary Formatter
            Serializer.Serialize(dataRoot, file);

        }

        private GameTreeNode BinDeserialize(string file)
        {
            GameSerialNode dataRoot = Serializer.Deserialize<GameSerialNode>(file);

            GameTreeNode newTree = new GameTreeNode(dataRoot.Status);

            newTree.Build(dataRoot, n => n.Status);

            return newTree;
        }
    }
}
