using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class GameHandler : IGame
    {
        #region Declaracion de Delegates
        
        public Action<bool> Out_Undo_Result { get; set; }
        public Action<bool> Out_DrawGridPlay_Result { get; set; }
        public Action<bool> Out_DrawPiece_Result { get; set; }
        public Action<bool> Out_DrawNextPiece_Result { get; set; }
        public Action<bool> Out_DeleteGridCell_Result { get; set; }
        public Action<bool> Out_DeleteNextPiece_Result { get; set; }

        public Action<bool> Out_EmptyCommandStack { get; set; }
        #endregion

        #region Invocacion de Delegates

        public void OnOut_Undo_Result(bool result)
        {
            Out_Undo_Result?.Invoke(result);
        }

        public void OnOut_DrawGridPlay_Result(bool result)
        {
            Out_DrawGridPlay_Result?.Invoke(result);
        }

        public void OnOut_DrawPiece_Result(bool result)
        {
            Out_DrawPiece_Result?.Invoke(result);
        }
        public void OnOut_DrawNextPiece_Result(bool result)
        {
            Out_DrawNextPiece_Result?.Invoke(result);
        }
        public void OnOut_DeleteGridCell_Result(bool result)
        {
            Out_DeleteGridCell_Result?.Invoke(result);
        }
        public void OnOut_DeleteNextPiece_Result(bool result)
        {
            Out_DeleteNextPiece_Result?.Invoke(result);
        }

        public void OnOut_EmptyCommandStack(bool result)
        {
            Out_EmptyCommandStack?.Invoke(result);
        }
        #endregion
    }
}
