using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;

namespace BPSolver.Game
{
    internal partial class GameHandler : IGame
    {
        #region Entradas de Controller

        public void In_DrawPiece(Coord coord, PieceName name)
        {
            bool ret ;

            try
            {
                // Chequear si hay espacio para la pieza y que este dentro del board
                ret = SolHandler.TestPiece(coord, name, CurrentStatus);

                if (ret)
                {
                    List<Coord> RealCoords;
                    // Get reference to piece
                    Piece piece = SolHandler.GetPiece(name);
                    // Create absolute coords list.
                    RealCoords = Piece.GetRealCoords(piece, coord);

                    // create command
                    ICommand command = new DrawPieceCommand(CurrentStatus, RealCoords, piece.Color);
                    ExecuteCommandDo(command);
                }
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }

            // Notificar para actualizacion, ver pieza insertada
            OnOut_DrawPiece_Result(ret);

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

            if (_commandStack.Count == 0)
            {
                OnOut_EmptyCommandStack(true);
            }

        }

        public void In_DrawGridPlay(Coord coord, PieceName name, int index, int id)
        {
            bool ret;

            try
            {
                // Chequear si hay espacio para la pieza y que este dentro del board
                ret = SolHandler.TestPiece(coord, name, CurrentStatus);

                if (ret)
                {
                    // Crear clon de estado actual
                    GameStatus cloned; //= SolHandler.CloneGameStatus(CurrentStatus);
                    cloned = Factory.CloneGameStatus(id, CurrentStatus);

                    List<Coord> RealCoords;
                    // Get reference to piece
                    Piece piece = SolHandler.GetPiece(name);
                    // Create absolute coords list.
                    RealCoords = Piece.GetRealCoords(piece, coord);

                    ICommand command = new DrawPieceCommand(cloned, RealCoords, piece.Color);
                    // execute command directly ,No Stack, Play have no Undo
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


        #endregion
    }
}
