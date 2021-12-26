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
    public partial class Controller : IBPServer
    {
        // Servicio a GUI
        // Entradas
        public void In_NewFile()
        {
            try
            {
                // crear nuevo _gameStatus
                CreateRootNode(CreateRootStatus());
            }
            catch (Exception)
            {
                Out_NewFileResult(false);
                throw;
            }

            // notificar form para visualizacion
            On_Out_UpdateBoard();

            Out_UserEnable(true);
            Out_NewFileResult(true);

            // new command stack
            ResetCommandStack();
        }

        public void In_CloseFile()
        {
            try
            {
               CreateRootNode(CreateRootStatus());
            }
            catch (Exception)
            {

                throw;
            }

            On_Out_UpdateBoard();
            Out_UserEnable(false);
        }

        // Operaciones IO
        public void In_LoadFile(string file)
        {

            try
            {
                // deserializar
                //_gameStatus = Serializer.Deserialize<GameStatus>(file);
                _treeRoot = BinDeserialize(file);
                CurrentNode = _treeRoot;
            }
            catch (Exception)
            {
                Out_LoadFileResult(false, file);
                throw;
            }

            // notificar form para visualizacion
            On_Out_UpdateBoard();
            Out_UserEnable(true);
            Out_LoadFileResult(true, file);

            // new command stack
            ResetCommandStack();
        }

        public void In_SaveFile(string file)
        {

            try
            {
                BinSerialize(file, _treeRoot);
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
            _gameSolver.UpdateGameStats(CurrentStatus);

            // notificar form para update
            On_Out_UpdateBoard();
        }

        //Insertar nueva pieza
        public void In_DrawPiece(Coord coord, PieceName name)
        {
            // Chequear si hay espacio para la pieza y que este dentro del board
            bool ret = _gameSolver.TestPiece(coord, name, CurrentStatus);

            if (ret)
            {
                //ejecutar
                List<Coord> RealCoords;
                // Get reference to piece
                Piece piece = _gameSolver.GetPiece(name);
                // Create absolute coords list.
                RealCoords = Piece.GetRealCoords(piece, coord);

                // create command
                ICommand command = new DrawPieceCommand(RealCoords, piece.Color, CurrentStatus);
                ExecuteCommandDo(command);

                // Notificar para actualizacion, ver pieza insertada
                On_Out_UpdateBoard();

            }

        }

        public void In_DrawGridPlay(Coord coord, PieceName name, int index)
        {
            // Chequear si hay espacio para la pieza y que este dentro del board
            bool ret = _gameSolver.TestPiece(coord, name, CurrentStatus);

            if (ret)
            {
                // Definir movimiento
                // Crear clon de estado actual
                CreateCloneChild();

                //ejecutar
                List<Coord> RealCoords;
                // Get reference to piece
                Piece piece = _gameSolver.GetPiece(name);
                // Create absolute coords list.
                RealCoords = Piece.GetRealCoords(piece, coord);

                // create command
                ICommand command = new DrawPieceCommand(RealCoords, piece.Color, CurrentStatus);
                //ExecuteCommandDo(command);
                command.Do();

                // Delete NextPiece
                command = new DeleteNextPieceCommand(index,
                                                      CurrentStatus.NextPieces);

                command.Do();

                // Actualizar Stats
                _gameSolver.UpdateGameStats(CurrentStatus);

                // Notificar para actualizacion, ver pieza insertada
                On_Out_UpdateBoard();

                // Testing for Column or Row completion
                if (_gameSolver.IsAnyCompleted(CurrentStatus))
                {
                    if (_gameSolver.IsAnyRowCompleted(CurrentStatus))
                    {
                        int[] list = _gameSolver.GetListRowsCompleted(CurrentStatus);
                        // Notificar para seleccionar fila completa
                        Out_SelectRows(list);
                    }

                    if (_gameSolver.IsAnyColumnCompleted(CurrentStatus))
                    {
                        int[] list = _gameSolver.GetListColumnsCompleted(CurrentStatus);
                        // Notificar para seleccionar fila completa
                        Out_SelectColumns(list);
                    }

                    // Delete
                    _gameSolver.ClearCompleted(CurrentStatus);

                    // Update Stats
                    _gameSolver.UpdateGameStats(CurrentStatus);

                    //notificar para mostrar eliminacion
                    On_Out_UpdateBoard();
                }
            }
        }
        // Establecer proxima pieza
        public void In_DrawNextPiece(int index, PieceName name)
        {
            ICommand command = new DrawNextPieceCommand(index,
                                                      CurrentStatus.NextPieces,
                                                      name);
            ExecuteCommandDo(command);

            // notificar form para update
            On_Out_UpdateBoard();
        }

        // Borrar celda en Grid
        public void In_DeleteGridCell(Coord coord)
        {
            // create command
            ICommand command = new DeleteCellCommand(coord, CurrentStatus);
            ExecuteCommandDo(command);

            // Update Stats
            _gameSolver.UpdateGameStats(CurrentStatus);

            //notificar
            On_Out_UpdateBoard();
        }

        // Borrar proxima pieza
        public void In_DeleteNextPiece(int index)
        {
            ICommand command = new DeleteNextPieceCommand(index,
                                                      CurrentStatus.NextPieces);
            ExecuteCommandDo(command);

            // notificar form para update
            On_Out_UpdateBoard();
        }

        // Control de Secuencia
        public  void In_MoveFirst()
        {
            bool ret = MoveFirst();

            if (ret)
                On_Out_UpdateBoard();

            Out_MoveFirst_Result(ret);
        }

        public void In_MoveLast()
        {
            bool ret = MoveLast();

            if (ret)
                On_Out_UpdateBoard();

            Out_MoveLast_Result(ret);
        }

        public void In_MoveNext()
        {
            bool ret = MoveNext();

            if (ret)
                On_Out_UpdateBoard();

            Out_MoveNext_Result(ret);
        }

        public void In_MovePrevious()
        {
            bool ret = MovePrevious();

            if (ret)
                On_Out_UpdateBoard();

            Out_MovePrevious_Result(ret);
        }

        public void In_MoveToChild(int id)
        {
            if (id != CurrentNode.Item.Id)
            {
                CurrentNode = TreeRoot[id];
                On_Out_UpdateBoard();
            }
        }

        public void In_AddChild(GameStatus child)
        {
            throw new NotImplementedException();
        }

        public void In_AddChildStay(GameStatus child)
        {
            throw new NotImplementedException();
        }

        public void In_Rename(int id, string name)
        {
            Rename(id, name);
            // No puede responderse con update, se genera excepcion
            // por coleccion en medio de iteracion
            //On_Out_UpdateBoard();
        }

        public void In_Solution()
        {
            // Ejecuta soluciones a partir de estado actual
            var ret = _gameSolver.CreateMetaSolution(CurrentStatus);


            // ejecut accion de modificacio de form similar a Out_UpdateBoard
            Out_UpdateSolutionBoard(ret);
        }
    }

}
