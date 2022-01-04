using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class DocHandler : IDocument
    {
        #region Declaracion de Delegates
        public Action<bool> Out_UserEnable { get; set; }

        public Action<bool, string> Out_CloseFileResult { get; set; }
        // NewResult
        public Action<bool, string> Out_NewFileResult { get; set; }

        // LoadResult
        public Action<bool, string> Out_LoadFileResult { get; set; }

        // SaveResult
        public Action<bool, string> Out_SaveFileResult { get; set; }
        #endregion

        #region Invocacion de Delegates
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
