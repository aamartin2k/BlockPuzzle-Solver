using System.Collections.Generic;

namespace BPSolver.Objects
{
    // Clase para actualizar GUI, incluye GameStatus
    // y otros dato adicionales
    public class GameMetaStatus
    {
       
        // Meta Constructor
        public GameMetaStatus(GameStatus status, 
                              List<GameStatus> childs,
                              bool isLeaf,
                              GameSimpleNode simpleTree

            )
        {
            Status = status;
            Childs = childs;
            IsLeaf = isLeaf;
            SimpleTree = simpleTree;
        }

        // Estado a Actualizar
        public GameStatus Status { get; private set; }

        // Estados Hijos
        public List<GameStatus> Childs { get; private set; }

        // Es hoja
        public bool IsLeaf { get; private set; }

        // Informacion del arbol
        public GameSimpleNode SimpleTree { get; private set; }


        

    }
}
