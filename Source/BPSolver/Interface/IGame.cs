using System;
using BPSolver.Objects;
using BPSolver.Enums;
using System.Collections.Generic;

namespace BPSolver
{
    /// <summary>
    /// Defines behavior of game status handling component (GameHandler).
    /// </summary>
    public interface IGame
    {
        #region Properties
        GameStatus CurrentStatus { get; set; }

        #endregion

        #region Inputs
        // Deshacer ultimo
        void In_Undo();

        // Insertar Pieza en Tablero
        void In_Draw(List<Coord> coords, PieceColor color);

        void In_DrawPiece(Coord coord, PieceName name);

        // Establecer Proxima Pieza
        void In_DrawNextPiece(int index, PieceName name);

        // Borrar celda en Grid
        void In_DeleteGridCell(Coord coord);

        // Borrar Nextpiece
        void In_DeleteNextPiece(int index);

        // Insertar Pieza desde NextPiece en Tablero
        // Jugada
        void In_DrawGridPlay(Coord coord, PieceName name, int index, int id);
        #endregion

        #region Salidas
        // Se incorporan notificacion de resultados para cada accion 
        Action<bool> Out_Undo_Result { get; set; }
        Action<bool> Out_Draw_Result { get; set; }
        Action<bool> Out_DrawNextPiece_Result { get; set; }
        Action<bool> Out_DeleteGridCell_Result { get; set; }
        Action<bool> Out_DeleteNextPiece_Result { get; set; }
        Action<bool> Out_DrawGridPlay_Result { get; set; }
        Action<bool> Out_EmptyCommandStack { get; set; }
        #endregion
    }
}
