using System;
using BPSolver.Objects;
using BPSolver.Enums;

namespace BPSolver
{
    public interface IBPServer
    {
        // Entradas
        // Manejo Documento
        void In_NewFile();
        void In_CloseFile();

        // Operaciones IO
        void In_LoadFile(string file);
        void In_SaveFile(string file);
        // Analizar soluciones a partir de estado actual
        void In_Solution();

        // Editar Tablero
        // Deshacer ultimo
        void In_Undo(); 

        // Insertar Pieza en Tablero
        void In_DrawPiece(Coord coord, PieceName name);

        // Establecer Proxima Pieza
        void In_DrawNextPiece(int index, PieceName name);

        // Borrar celda en Grid
        void In_DeleteGridCell(Coord coord);

        // Borrar Nextpiece
        void In_DeleteNextPiece(int index);

        // Insertar Pieza desde NextPiece en Tablero
        // Jugada
        void In_DrawGridPlay(Coord coord, PieceName name, int index);

        // Manejo de Secuencia
        void In_AddChild(GameStatus child);
        void In_AddChildStay(GameStatus child);
        void In_MoveFirst();
        void In_MovePrevious();
        void In_MoveNext();
        void In_MoveLast();
        void In_MoveToChild(int id);
        void In_Rename(int id, string name);


        // ***************************************************
        // Salidas
        Action<GameMetaStatus> Out_UpdateGameBoard { get; set; }

        Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }

        // Notificaciones
        Action<bool> Out_UserEnable { get; set; }

        // NewResult
        Action<bool> Out_NewFileResult { get; set; }

        // LoadResult
        Action<bool, string> Out_LoadFileResult { get; set; }

        // SaveResult
        Action<bool, string> Out_SaveFileResult { get; set; }

        // PutPieceResult

        // Empty Command Stack for Undo
        Action<bool> Out_EmptyCommandStack { get; set; }

        // Select Rows for deletion
        Action<int[]> Out_SelectRows { get; set; }

        // Select Columns for deletion
        Action<int[]> Out_SelectColumns { get; set; }

        // Resultado de Movimentos
        Action<bool> Out_MoveFirst_Result { get; set; }
        Action<bool> Out_MovePrevious_Result { get; set; }
        Action<bool> Out_MoveNext_Result { get; set; }
        Action<bool> Out_MoveLast_Result { get; set; }
    }
}
