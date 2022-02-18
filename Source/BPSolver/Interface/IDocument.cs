using System;
using BPSolver.Objects;
using BPSolver.Enums;


namespace BPSolver
{
    /// <summary>
    /// Defines behavior of document handling component (DocHandler)
    /// </summary>
    public interface IDocument
    {
        #region Properties
      
        Document CurrentDocument { get; }

        #endregion

        #region Inputs
        // Manejo Documento
        void In_NewFile();
        void In_CloseFile();

        // Operaciones IO
        void In_LoadFile(string file);
        void In_SaveFile();
        void In_SaveFileAs(string file);
        #endregion

        #region Salidas

        Action<bool> Out_UserEnable { get; set; }

        Action<bool, string> Out_NewFileResult { get; set; }
        Action<bool, string> Out_CloseFileResult { get; set; }

        Action<bool, string> Out_LoadFileResult { get; set; }

        Action<bool, string> Out_SaveFileResult { get; set; }
        #endregion
    }
}
