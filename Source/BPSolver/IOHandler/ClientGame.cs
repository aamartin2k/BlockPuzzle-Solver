﻿using BPSolver.Enums;
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
        #region Entradas de Cliente

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

        public void In_DrawPiece(Coord coord, PieceName name)
        {
            OnOut_DrawPiece(coord, name);
        }

        public void In_Undo()
        {
            OnOut_Undo();
        }
        #endregion

        #region Salidas a Cliente
        #region Declaracion de Delegates
        public Action<bool> Out_Undo_Result { get; set; }
        public Action<bool> Out_DrawGridPlay_Result { get; set; }
        public Action<bool> Out_DrawPiece_Result { get; set; }
        public Action<bool> Out_DrawNextPiece_Result { get; set; }
        public Action<bool> Out_DeleteGridCell_Result { get; set; }
        public Action<bool> Out_DeleteNextPiece_Result { get; set; }

        #endregion

        #region Invocacion de Delegates

       
        public void OnOut_DrawGridPlay_Result(bool result)
        {
            Out_DrawGridPlay_Result?.Invoke(result);
        }

        public void OnOut_Undo_Result(bool result)
        {
            Out_Undo_Result?.Invoke(result);
        }
        public void OnOut_DrawPiece_Result(bool result)
        {
            Out_DrawPiece_Result?.Invoke(result);
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