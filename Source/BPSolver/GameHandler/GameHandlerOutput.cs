using System;

namespace BPSolver.Game
{
    /// <summary>
    /// Implement editing functions for game status.
    /// Outputs for notifying IOHandler.
    /// </summary>
    internal partial class GameHandler : IGame
    {
        #region Declaration of Delegates

        public Action<bool> Out_Undo_Result { get; set; }
        public Action<bool> Out_DrawGridPlay_Result { get; set; }
        public Action<bool> Out_Draw_Result { get; set; }
        public Action<bool> Out_DrawNextPiece_Result { get; set; }
        public Action<bool> Out_DeleteGridCell_Result { get; set; }
        public Action<bool> Out_DeleteNextPiece_Result { get; set; }
        public Action<bool> Out_EmptyCommandStack { get; set; }
        #endregion

        #region Invocation  of Delegates

        public void OnOut_Undo_Result(bool result)
        {
            Out_Undo_Result?.Invoke(result);
        }

        public void OnOut_DrawGridPlay_Result(bool result)
        {
            Out_DrawGridPlay_Result?.Invoke(result);
        }

        public void OnOut_Draw_Result(bool result)
        {
            Out_Draw_Result?.Invoke(result);
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
