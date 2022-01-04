using System;
using BPSolver.Objects;
using BPSolver.Enums;

namespace BPSolver
{
    public interface IController
    {
        // IController se define como copia de los metodos IN y Out 
        // de las interfaces de componentes, SIN emplear las propiedades
        // que permiten acceso desde IController a GameStatus, CurrentNode etc

        #region General
        Action<GameMetaStatus> Out_UpdateGameBoard { get; set; }
        #endregion

        #region IDocument
        #region Entradas
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
        #endregion

        #region IGame
        #region Entradas
        
        void In_Undo();
        void In_DrawPiece(Coord coord, PieceName name);
        void In_DrawNextPiece(int index, PieceName name);
        void In_DeleteGridCell(Coord coord);
        void In_DeleteNextPiece(int index);
        void In_DrawGridPlay(Coord coord, PieceName name, int index);
        #endregion

        #region Salidas
        Action<bool> Out_Undo_Result { get; set; }
        Action<bool> Out_DrawPiece_Result { get; set; }
        Action<bool> Out_DrawNextPiece_Result { get; set; }
        Action<bool> Out_DeleteGridCell_Result { get; set; }
        Action<bool> Out_DeleteNextPiece_Result { get; set; }
        Action<bool> Out_DrawGridPlay_Result { get; set; }
        Action<bool> Out_EmptyCommandStack { get; set; }
        #endregion
        #endregion

        #region ITree
        #region Entradas
        // Manejo de Secuencia
        void In_MoveFirst();
        void In_MovePrevious();
        void In_MoveNext();
        void In_MoveLast();
        void In_MoveToChild(int id);
        void In_Rename(int id, string name);

        #endregion

        #region Salidas
        // Resultado de Movimentos
        Action<bool> Out_MoveFirst_Result { get; set; }
        Action<bool> Out_MovePrevious_Result { get; set; }
        Action<bool> Out_MoveNext_Result { get; set; }
        Action<bool> Out_MoveLast_Result { get; set; }
        Action<bool> Out_MoveToChild_Result { get; set; }
        Action<bool> Out_Rename_Result { get; set; }

        
        #endregion
        #endregion

        #region ISolver
        #region Entradas
        // Analizar soluciones a partir de estado actual
        void In_Solution();
        #endregion

        #region Salidas
        Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }
        #endregion
        #endregion
    }
}
