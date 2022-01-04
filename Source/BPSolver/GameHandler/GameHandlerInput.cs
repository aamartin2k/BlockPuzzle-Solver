using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
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
                    ICommand command = new DrawPieceCommand(RealCoords, piece.Color, CurrentStatus);
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
        }

        //Insertar nueva pieza jugada
       
        public void In_DrawGridPlay(Coord coord, PieceName name, int index)
        {
            bool ret;

            try
            {
                // Chequear si hay espacio para la pieza y que este dentro del board
                ret = SolHandler.TestPiece(coord, name, CurrentStatus);

                if (ret)
                {
                    // Crear clon de estado actual
                    GameStatus cloned = SolHandler.CloneGameStatus(CurrentStatus);

                    List<Coord> RealCoords;
                    // Get reference to piece
                    Piece piece = SolHandler.GetPiece(name);
                    // Create absolute coords list.
                    RealCoords = Piece.GetRealCoords(piece, coord);

                    ICommand command = new DrawPieceCommand(RealCoords, piece.Color, cloned);
                    // execute command directly ,No Stack, Play have no Undo
                    command.Do();

                    command = new DeleteNextPieceCommand(index, cloned.NextPieces);
                    command.Do();

                    CurrentStatus = cloned;
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
                ICommand command = new DrawNextPieceCommand(index,
                                                          CurrentStatus.NextPieces,
                                                          name);
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
                ICommand command = new DeleteCellCommand(coord, CurrentStatus);
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
            try
            {
                // create command
                ICommand command = new DeleteNextPieceCommand(index,
                                                          CurrentStatus.NextPieces);
                ExecuteCommandDo(command);
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
                //throw;
            }

            ////notificar
            OnOut_DeleteNextPiece_Result(ret);
        }


        #endregion
    }
}
