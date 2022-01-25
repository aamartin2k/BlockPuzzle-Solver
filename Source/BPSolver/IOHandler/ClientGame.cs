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
        // Comunicacion con el cliente
        // para funciones de GameHandler
        //
        #region Inputs

        public void In_DeleteGridCell(Coord coord)
        {
            OnOut_DeleteGridCell(coord);
        }

        public void In_DeleteNextPiece(int index)
        {
            OnOut_DeleteNextPiece(index);
        }

        public void In_DrawGridPlay(Coord coord, PieceName name, int index)
        {
            int clonId = _TreeHandler.TreeCount ;
            OnOut_DrawGridPlay(coord, name, index, clonId);
        }

        public void In_DrawNextPiece(int index, PieceName name)
        {
            OnOut_DrawNextPiece(index, name);
        }

        public void In_Draw(List<Coord> coords, PieceColor color)
        {
            bool ret = Utils.TestCoords(coords, _GameHandler.CurrentStatus);

            if (ret)
            {
                OnOut_Draw(coords, color);
            }
            else
            {
                OnOut_Draw_Result(false);
            }
        }

        public void In_DrawPiece(Coord coord, PieceName name)
        {
            OnOut_DrawPiece(coord, name);
            // new impl
            // get real coords
            // test real coords
            // if proceed, draw

            // Get reference to piece
            Piece piece = PieceSet.GetPiece(name);
            // Create absolute coords list.
            List<Coord> RealCoords = Piece.GetRealCoords(piece, coord);

            bool ret = Utils.TestCoords( RealCoords, _GameHandler.CurrentStatus);

            if (ret)
            {
                OnOut_Draw(RealCoords, piece.Color);
            }
            else
            {
                OnOut_Draw_Result(false);
            }
        }

        public void In_Undo()
        {
            OnOut_Undo();
        }
        #endregion

        #region Outputs
        #region Declaration of Delegates
        public Action<bool> Out_Undo_Result { get; set; }
        public Action<bool> Out_DrawGridPlay_Result { get; set; }
        public Action<bool> Out_Draw_Result { get; set; }
        public Action<bool> Out_DrawNextPiece_Result { get; set; }
        public Action<bool> Out_DeleteGridCell_Result { get; set; }
        public Action<bool> Out_DeleteNextPiece_Result { get; set; }

        #endregion

        #region Invocation of Delegates

       
        public void OnOut_DrawGridPlay_Result(bool result)
        {
            Out_DrawGridPlay_Result?.Invoke(result);
        }

        public void OnOut_Undo_Result(bool result)
        {
            Out_Undo_Result?.Invoke(result);
        }
        public void OnOut_Draw_Result(bool result)
        {
            Out_Draw_Result?.Invoke(result);
        }
        public void OnOut_DrawNextPiece_Result(bool result)
        {
            Out_DrawNextPiece_Result?.Invoke(result);
        }
        public void OnOut_DeleteGridCell_Result(bool result)
        {
            Out_DeleteGridCell_Result?.Invoke(result);
        }
        public void OnOut_DeleteNextPiece_Result(bool result)
        {
            Out_DeleteNextPiece_Result?.Invoke(result);
        }
        #endregion
        #endregion

    }
}
