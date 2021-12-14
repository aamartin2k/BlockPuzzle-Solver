﻿using BPSolver.Enums;
using BPSolver.Objects;
using SourceGrid;
using System;

using System.Windows.Forms;

namespace WFProt
{
    public partial class Form1 : Form
    {
        #region "State Machine Context"

        private StContext context;

        void CreateStateMachine()
        {
            context = new StContext();

            // creando todos los estados
            context.CreateStates();

            // asignando procedimientos de salida a delegates
            context.GridCellDeletionState.Out_DeleteGridCell = StmOut_DeleteGridCell;
            context.NextPieceDeletionState.Out_DeleteNextPiece = StmOut_DeleteNextPiece;

            context.NextPieceDrawingState.Out_DrawNextPiece = Out_DrawNextPiece;
            context.GridCellDrawingState.Out_DrawGrid = Out_DrawGrid;
            context.NextPiecePlayState.Out_DrawGridPlay = Out_DrawGridPlay;

            context.Out_ShowCurrentPiece = ShowCurrentPiece;
            context.ShowCurrentAction = ShowCurrentAction;
        }

        #endregion

        #region "Entradas"
        private void TsbArrow_Click(object sender, EventArgs e)
        {
            // Modo Seleccion
            context.ActionSelectClicked();
        }
        private void TsbDelete_Click(object sender, EventArgs e)
        {
            // Modo Borrar
            context.ActionDeleteClicked();
        }

        private void TsbSetPiece_Click(object sender, EventArgs e)
        {
            // Modo Seleccionar Pieza
            ToolStripItem itm = (ToolStripItem)sender;
            PieceName piece = (PieceName)itm.Tag;

            //ShowCurrentPiece(piece);

            context.PieceButtonClicked(piece);
        }

        private void PbNextPiece_Click(object sender, MouseEventArgs e)
        {
            // Modo NextPiece
            // get target  picture box
            PictureBox pbx = (PictureBox)sender;
            int index = 0;

            // establecer indice para lista en dependencia del nombre de PictureBox
            if (pbx.Name == "pbNextPiece3")
                index = 2;

            if (pbx.Name == "pbNextPiece2")
                index = 1;

            // get Piece on Control
            PieceName piece = (PieceName) pbx.Tag;

            context.NextPieceImageClicked(index, piece);

        }

        private void ClickOnCell(CellContext sender)
        {
            Position pos = sender.Position;
            Coord coord = new Coord(pos.Row, pos.Column);

            context.GridCellClicked(coord);

        }

        #endregion

        #region "Salidas"
        void StmOut_DeleteGridCell(Coord pos)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar Grid Cell Row: {0} Col: {1}", pos.Row, pos.Col));
            Out_DeleteCell(pos);
        }

        void StmOut_DeleteNextPiece(int index)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar NextPieceImage index {0}", index));
        }

        void Out_DrawNextPiece(int index, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar NextPiece index: {0} Image: {1} ", index, piece));
            Out_SetNextPiece(index, piece);
        }

        void Out_DrawGrid(Coord pos, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Col, piece));
            Out_DrawPiece(pos, piece);
        }

        void Out_DrawGridPlay(Coord pos, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. PLAY. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Col, piece));
        }


        #endregion


        

         
       
    }
}