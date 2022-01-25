using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;

namespace BPSolver.Game
{
    /// <summary>
    /// Implement editing functions for game status.
    /// Services inputs from IOHandler.
    /// </summary>
    internal partial class GameHandler : IGame
    {
       
        public void In_Draw(List<Coord> coords, PieceColor color)
        {
            bool ret;

            try
            {
                ICommand command = new DrawCommand(CurrentStatus, coords, color);
                ExecuteCommandDo(command);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }

            OnOut_Draw_Result(ret);
        }

        public void In_DrawPiece(Coord coord, PieceName name)
        {
            bool ret ;

            try
            {
                // Check if location is free and inside board limits.
                ret = Utils.TestPiece(coord, name, CurrentStatus);

                if (ret)
                {
                    List<Coord> RealCoords;
                    // Get reference to piece.
                    Piece piece = PieceSet.GetPiece(name);
                    // Create absolute coords list.
                    RealCoords = Piece.GetRealCoords(piece, coord);

                    ICommand command = new DrawCommand(CurrentStatus, RealCoords, piece.Color);
                    ExecuteCommandDo(command);
                }
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }
            // Notify result.
            OnOut_Draw_Result(ret);

        }

        public void In_Undo()
        {
            bool ret;
            try
            {
                ExecuteCommandUndo();
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
               // throw;
            }

            OnOut_Undo_Result(ret);
        }

        public void In_DrawGridPlay(Coord coord, PieceName name, int index, int id)
        {
            bool ret;

            try
            {
                // Chequear si hay espacio para la pieza y que este dentro del board
                ret = Utils.TestPiece(coord, name, CurrentStatus);

                if (ret)
                {
                    // Crear clon de estado actual
                    GameStatus cloned; 
                    cloned = Factory.CloneGameStatus(id, CurrentStatus);

                    List<Coord> RealCoords;
                    // Get reference to piece
                    Piece piece = PieceSet.GetPiece(name);
                    // Create absolute coords list.
                    RealCoords = Piece.GetRealCoords(piece, coord);

                    ICommand command = new DrawCommand(cloned, RealCoords, piece.Color);
                    // execute command directly, No Stack, Play have no Undo
                    command.Do();

                    command = new DeleteNextPieceCommand(cloned, index);
                    command.Do();

                    CurrentStatus = cloned;

                    // New Command Stack for new game status
                    ResetCommandStack();

                    ret = true;
                }
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }

            // Notificar
            OnOut_DrawGridPlay_Result(ret);
        }
        // Establecer proxima pieza
        public void In_DrawNextPiece(int index, PieceName name)
        {
            bool ret;
            try
            {
                ICommand command = new DrawNextPieceCommand(CurrentStatus, index, name);
                ExecuteCommandDo(command);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }
            // notificar 
            OnOut_DrawNextPiece_Result(ret);
        }

        // Borrar celda en Grid
        public void In_DeleteGridCell(Coord coord)
        {
            bool ret;
            try
            {
                // create command
                ICommand command = new DeleteCellCommand(CurrentStatus, coord);
                ExecuteCommandDo(command);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }
        
            ////notificar
            OnOut_DeleteGridCell_Result(ret) ;
        }

        // Borrar proxima pieza
        public void In_DeleteNextPiece(int index)
        {
            bool ret;

            if ((index < 0) || (index > Constants.NexPieces - 1))
            {
                ret = false;
                goto Notify;
            }

            
            try
            {
                // create command
                ICommand command = new DeleteNextPieceCommand(CurrentStatus, index);
                ExecuteCommandDo(command);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }
Notify:
            ////notificar
            OnOut_DeleteNextPiece_Result(ret);
        }

    }
}
