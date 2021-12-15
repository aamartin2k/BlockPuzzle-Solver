using System;
using System.Collections.Generic;
using BPSolver.Objects;
using System.Text;
using System.Threading.Tasks;
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
        void In_DrawGridPlay(Coord coord, PieceName name);

        // Salidas
        Action<GameSimpleStatus> Out_UpdateBoard { get; set; }

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

    }
}
