using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    // Clase para enviar informacion simple del arbol a la GUI
    public class GameSimpleNode : SerialTreeNode<GameSimpleNode>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public GameSimpleNode() { }

        public GameSimpleNode(int id,
                              string nombre,
                              params GameSimpleNode[] children)
            : base(children)
        {
            Id = id;
            Nombre = nombre;
        }
    }

}
