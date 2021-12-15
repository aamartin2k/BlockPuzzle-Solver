using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver.Solver;
using System;
using System.Collections.Generic;

namespace BPSolver
{
    public partial class Controller : IBPServer
    {
        

        // Servicio a GUI
        // Entradas
        public void In_NewFile()
        {
            try
            {
                // crear nuevo _gameStatus
                _gameStatus = GameSimpleStatusFactory.Create();
            }
            catch (Exception)
            {
                Out_NewFileResult(false);
                throw;
            }
            
            // notificar form para visualizacion
            Out_UpdateBoard(_gameStatus);
            Out_UserEnable(true);
            Out_NewFileResult(true);

            // new command stack
            ResetStack();
        }

        public void In_CloseFile()
        {
            try
            {
                _gameStatus = GameSimpleStatusFactory.Create();
    
            }
            catch (Exception)
            {

                throw;
            }



            Out_UpdateBoard(_gameStatus);
            Out_UserEnable(false);
        }

        // Operaciones IO
        public void In_LoadFile(string file)
        {

            try
            {
                // deserializar
                _gameStatus = Serializer.Deserialize<GameSimpleStatus>(file);
          
            }
            catch (Exception)
            {
                Out_LoadFileResult(false, file);
                throw;
            }
            

            // notificar form para visualizacion
            Out_UpdateBoard(_gameStatus);
            Out_UserEnable(true);
            Out_LoadFileResult(true, file);

            // new command stack
            ResetStack();
        }

        public void In_SaveFile(string file)
        {
            
            try
            {
                Serializer.Serialize(_gameStatus, file);
            }
            catch (Exception)
            {
                Out_SaveFileResult(false, file);
                throw;
            }

            Out_SaveFileResult(true, file);
        }

        // Edicion de Tablero
        public void In_Undo()
        {
            ExecuteCommandUndo();

            // Update Stats
            _gameSolver.UpdateGameStats(_gameStatus);

            // notificar form para update
            Out_UpdateBoard(_gameStatus);
        }

        //Insertar nueva pieza
        public void In_DrawPiece(Coord coord, PieceName name)
        {
            // Chequear si cabe
            bool ret = _gameSolver.TestPiece(coord, name, _gameStatus);

            if (ret)
            {
                //ejecutar
                List<Coord> RealCoords;
                // Get reference to piece
                Piece piece = _gameSolver.GetPiece(name);
                // Create absolute coords list.
                RealCoords = _gameSolver.GetRealCoords(coord, piece.Matrix);

                // Test
                List<Coord> TestRealCoords;
                TestRealCoords = Piece.GetRealMatrix(piece, coord);
                // Test

                // create command
                ICommand command = new DrawPieceCommand(RealCoords, piece.Color, _gameStatus);
                ExecuteCommandDo(command);

                // Notificar para actualizacion, ver pieza insertada
                Out_UpdateBoard(_gameStatus);

                // Testing for Column or Row completion
                if (_gameSolver.IsAnyRowCompleted(_gameStatus))
                {
                    int[] list = _gameSolver.GetListRowsCompleted(_gameStatus);
                    // Notificar para seleccionar fila completa
                    Out_SelectRows(list);
                }

                if (_gameSolver.IsAnyColumnCompleted(_gameStatus))
                {
                    int[] list = _gameSolver.GetListColumnsCompleted(_gameStatus);
                    // Notificar para seleccionar fila completa
                    Out_SelectColumns(list);
                }

                // Delete
                _gameSolver.ClearCompleted(_gameStatus);

                // Update Stats
                _gameSolver.UpdateGameStats(_gameStatus);

                //notificar para mostrar eliminacion
                Out_UpdateBoard(_gameStatus);
            }
        }

        // Establecer proxima pieza
        public void In_DrawNextPiece(int index, PieceName name)
        {
            ICommand command = new DrawNextPieceCommand(index, 
                                                      _gameStatus.NextPieces, 
                                                      name);
            ExecuteCommandDo(command);

            // notificar form para update
            Out_UpdateBoard(_gameStatus);
        }

        // Borrar celda en Grid
        public void In_DeleteGridCell(Coord coord)
        {
            // create command
            ICommand command = new DeleteCellCommand(coord, _gameStatus);
            ExecuteCommandDo(command);

            // Update Stats
            _gameSolver.UpdateGameStats(_gameStatus);

            //notificar
            Out_UpdateBoard(_gameStatus);
        }

        // Borrar proxima pieza
        public void In_DeleteNextPiece(int index)
        {
            ICommand command = new DeleteNextPieceCommand(index,
                                                      _gameStatus.NextPieces);
            ExecuteCommandDo(command);

            // notificar form para update
            Out_UpdateBoard(_gameStatus);
        }
        public void In_DrawGridPlay(Coord coord, PieceName name)
        {

        }

        // Salidas
        public Action<GameSimpleStatus> Out_UpdateBoard { get; set; }
        public Action<bool> Out_UserEnable { get; set; }
        public Action<bool> Out_NewFileResult { get; set; }
        public Action<bool, string> Out_LoadFileResult { get; set; }
        public Action<bool, string> Out_SaveFileResult { get; set; }
        public Action<bool> Out_EmptyCommandStack { get; set; }

        public Action<int[]> Out_SelectRows { get; set; }
        public Action<int[]> Out_SelectColumns { get; set; }



    }
}
