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
        void In_PutPiece(Coord coord, PieceName name);

        // Establecer Proxima Pieza
        void In_SetNextPiece(int index, PieceName name);

        // Borrar celda Out_DeleteCell
        void In_DeleteCell(Coord coord);

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
