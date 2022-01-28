using BPSolver.Objects;
using System;
using System.Collections.Generic;

namespace BPSolver
{
    /// <summary>
    /// Defines behavior of data tree handling component (TreeHandler).
    /// </summary>
    public interface ITree
    {
       

        #region Properties
        GameTreeNode TreeRoot { get; set; }
        GameTreeNode CurrentNode { get; set; }
        List<GameStatus> CurrentChilds { get;  }
        bool CurrentIsIsLeaf { get; }
        int TreeCount { get; }
        #endregion

        #region Inputs
        // Manejo de Secuencia
        void In_MoveFirst();
        void In_MovePrevious();
        void In_MoveNext();
        void In_MoveLast();
        void In_MoveToChild(int id);
        void In_Rename(int id, string name);

        void CreateRootNode(GameStatus item);
        void CreateChildNode(GameStatus item);
        #endregion

        #region Outputs
        // Resultado de Movimentos
        Action<bool> Out_MoveFirst_Result { get; set; }
        Action<bool> Out_MovePrevious_Result { get; set; }
        Action<bool> Out_MoveNext_Result { get; set; }
        Action<bool> Out_MoveLast_Result { get; set; }
        Action<bool> Out_MoveToChild_Result { get; set; }
        Action<bool> Out_Rename_Result { get; set; }

        #endregion
    }
}
