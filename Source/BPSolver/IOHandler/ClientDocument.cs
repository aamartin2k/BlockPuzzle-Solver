using System;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Comunicacion con el cliente
        // para funciones de DocHandler
        //
        #region Inputs from DocHandler
        public void In_NewFile()
        {
            // Llamada a Document
            OnOut_NewFile();
        }
        public void In_CloseFile()
        {
            // Llamada a Document
            OnOut_CloseFile();
        }
        public void In_LoadFile(string file)
        {
            // Llamada a Document
            OnOut_LoadFile(file);

        }
      
        public void In_SaveFile()
        {
            UpdateDocumentFromGameTree();
            OnOut_SaveFile();
        }
        public void In_SaveFileAs(string file)
        {
            UpdateDocumentFromGameTree();
            OnOut_SaveFileAs(file);
        }

        #endregion

        #region Salidas a Cliente
        #region Declaration of Delegates
        public Action<bool> Out_UserEnable { get; set; }
        public Action<bool, string> Out_NewFileResult { get; set; }
        public Action<bool, string> Out_CloseFileResult { get; set; }
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
        #endregion

       

    }


}
