using BPSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    /// <summary>
    /// Describe el movimiento de una pieza
    /// </summary>
    [Serializable]
    public class Movement
    {
        /// <summary>
        /// Clave de la pieza en el diccionario.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Coordenadas de Cell donde se inserta la pieza
        /// </summary>
        public Coord InsertPoint { get; set; } 

        /// <summary>
        /// Nombre de pieza a mover
        /// </summary>
        public PieceName Name { get; set; }


        // Constructor
        public Movement(int index, Coord point, PieceName name)
        {
            Index = index;
            InsertPoint = point;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("Mover pieza: {0} index: {1} a celda R: {2} C: {3}",
                                    Name, Index, InsertPoint.Row, InsertPoint.Col);
        }
    }
}
