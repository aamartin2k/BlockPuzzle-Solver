using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Comunicacion con el Componente GameHandler

        #region Salidas
        #region Declaracion de Delegates
        internal Action Out_Undo { get; set; }

        // Insertar Pieza en Tablero
        internal Action<Coord, PieceName> Out_DrawPiece { get; set; }

        // Establecer Proxima Pieza
        internal Action<int, PieceName> Out_DrawNextPiece { get; set; }

        // Borrar celda en Grid
        internal Action<Coord> Out_DeleteGridCell { get; set; }

        // Borrar Nextpiece
        internal Action<int> Out_DeleteNextPiece { get; set; }

        // Insertar Pieza desde NextPiece en Tablero
        // Jugada
        internal Action<Coord, PieceName, int> Out_DrawGridPlay { get; set; }

        #endregion
        #region Invocacion de Delegates
        private void OnOut_Undo()
        {
            Out_Undo?.Invoke();
        }

        private void OnOut_DrawPiece(Coord coord, PieceName name)
        {
            Out_DrawPiece?.Invoke(coord, name);
        }
        private void OnOut_DrawNextPiece(int index, PieceName name)
        {
            Out_DrawNextPiece?.Invoke(index, name);
        }
        private void OnOut_DeleteGridCell(Coord coord)
        {
            Out_DeleteGridCell?.Invoke(coord);
        }
        private void OnOut_DeleteNextPiece(int index)
        {
            Out_DeleteNextPiece?.Invoke(index);
        }
        private void OnOut_DrawGridPlay(Coord coord, PieceName name, int index)
        {
            Out_DrawGridPlay?.Invoke(coord, name, index);
        }

        #endregion
        #endregion

        #region Entradas de Componente

        internal void In_DrawGridPlay_Result(bool result)
        {
            OnOut_DrawGridPlay_Result(result);
            OnPlay_UpdateGameBoard(result);
        }

        internal void In_Undo_Result(bool result)
        {
            OnOut_Undo_Result(result);
            OnDraw_UpdateGameBoard(result);
        }
        internal void In_DrawPiece_Result(bool result)
        {
            OnOut_DrawPiece_Result(result);
            OnDraw_UpdateGameBoard(result);
        }
        internal void In_DrawNextPiece_Result(bool result)
        {
            OnOut_DrawNextPiece_Result(result);
            OnOut_UpdateGameBoard();
        }
        internal void In_DeleteGridCell_Result(bool result)
        {
            OnOut_DeleteGridCell_Result(result);
            OnDraw_UpdateGameBoard(result);
        }
        internal void In_DeleteNextPiece_Result(bool result)
        {
            OnOut_DeleteNextPiece_Result(result);
            OnOut_UpdateGameBoard();
        }

        //Out_EmptyCommandStack es Entrada aunque no es Notificacion deResultado
        internal void In_EmptyCommandStack(bool result)
        {
            OnOut_EmptyCommandStack(result);
        }

        private void OnDraw_UpdateGameBoard(bool ret)
        {
            if (ret)
            {
                // Update GameStatus stats
                SolHandler.UpdateGameStatsAfterDraw(_GameHandler.CurrentStatus);
                OnOut_UpdateGameBoard();
            }
        }
        private void OnPlay_UpdateGameBoard(bool ret)
        {
            if (ret)
            {
                // TreeHandler
                _TreeHandler.CreateChildNode(_GameHandler.CurrentStatus);

                // Update GameStatus stats
                // Important. Order matter here.
                // StatsAfterPlay must be called BEFORE StatsAfterDraw to account for
                // deleted cells as free cells.
                SolHandler.UpdateGameStatsAfterPlay(_GameHandler.CurrentStatus);
                SolHandler.UpdateGameStatsAfterDraw(_GameHandler.CurrentStatus);

                OnOut_UpdateGameBoard();
            }
        }
        #endregion

        #region  GameStatus Factory
        private GameStatus CreateRootStatus()
        {
            return CreateChildStatus(0, "Root");
        }

        private GameStatus CreateChildStatus(int id, string name)
        {
            GameStatus onew = new GameStatus(id);

            onew.Nombre = name;
            onew.NextPieces = new Dictionary<int, PieceName>();
            onew.Cells = new List<Cell>();

            // llenar con nada
            onew.NextPieces.Add(0, PieceName.None);
            onew.NextPieces.Add(1, PieceName.None);
            onew.NextPieces.Add(2, PieceName.None);

            for (int i = 0; i < Constants.Rank; i++)
            {
                for (int j = 0; j < Constants.Rank; j++)
                {
                    onew.Cells.Add(new Cell(j, i));
                }
            }

            return onew;
        }


        #endregion
    }
}
