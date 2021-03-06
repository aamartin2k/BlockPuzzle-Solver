using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Salidas a Cliente que no son Notificacion de Resultado concreto
        #region Declaration of Delegates

        public Action<GameMetaStatus> Out_UpdateGameBoard { get; set; }
        
       
        public Action<bool> Out_EmptyCommandStack { get; set; }
 
        #endregion

        #region Invocation of Delegates
        public void OnOut_UpdateGameBoard()
        {
            // Crear internamente GameMetaStatus antes de invocar
            GameSimpleNode sern;
            sern = CreateSimpleTreeFromRoot(_TreeHandler.TreeRoot);
            GameStatus status;
            status = _GameHandler.CurrentStatus;

            GameMetaStatus meta;
            meta = new GameMetaStatus(status,
                                      _TreeHandler.CurrentChilds,
                                      _TreeHandler.CurrentIsIsLeaf,
                                      sern);

            Out_UpdateGameBoard?.Invoke(meta);
        }
        
  
       
        public void OnOut_EmptyCommandStack(bool status)
        {
            Out_EmptyCommandStack?.Invoke(status);
        }

        // Analizar Out_SelectRows Out_SelectColumns
        #endregion

    }
}
