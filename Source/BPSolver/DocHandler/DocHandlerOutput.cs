using System;

namespace BPSolver
{
    /// <summary>
    /// Implement handling game and solution data as a document.
    /// Outputs for notifying IOHandler.
    /// </summary>
    internal partial class DocHandler : IDocument
    {
        #region Declaration of Delegates

        public Action<bool> Out_UserEnable { get; set; }
        public Action<bool, string> Out_CloseFileResult { get; set; }
        public Action<bool, string> Out_NewFileResult { get; set; }
        public Action<bool, string> Out_LoadFileResult { get; set; }
        public Action<bool, string> Out_SaveFileResult { get; set; }
        #endregion

        #region Invocation of Delegates

        public void OnOut_UserEnable(bool result)
        {
            Out_UserEnable?.Invoke(result);
        }
        public void OnOut_NewFileResult(bool result, string text)
        {
            Out_NewFileResult?.Invoke(result, text);
        }
       
        public void OnOut_CloseFileResult(bool result, string text)
        {
            Out_CloseFileResult?.Invoke(result, text);
        }

        public void OnOut_LoadFileResult(bool result, string text)
        {
            Out_LoadFileResult?.Invoke(result, text);
        }
        public void OnOut_SaveFileResult(bool result, string text)
        {
            Out_SaveFileResult?.Invoke(result, text);
        }
       
        #endregion
    }
}
