using BPSolver.Enums;
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
            context.GridCellDeletionState.Out_DeleteGridCell = Out_DeleteGridCell;
            context.NextPieceDeletionState.Out_DeleteNextPiece = Out_DeleteNextPiece;

            context.NextPieceDrawingState.Out_DrawNextPiece = Out_DrawNextPiece;
            context.GridCellDrawingState.Out_DrawGrid = Out_DrawGrid;
            context.NextPiecePlayState.Out_DrawGridPlay = Out_DrawGridPlay;

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

            context.PieceButtonClicked(piece);
        }
        #endregion

        #region "Salidas"
        void Out_DeleteGridCell(Coord pos)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar Grid Cell Row: {0} Col: {1}", pos.Row, pos.Col));
        }

        void Out_DeleteNextPiece(int index)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar NextPieceImage index {0}", index));
        }

        void Out_DrawNextPiece(int index, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar NextPiece index: {0} Image: {1} ", index, piece));
        }

        void Out_DrawGrid(Coord pos, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Col, piece));
        }

        void Out_DrawGridPlay(Coord pos, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. PLAY. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Col, piece));
        }


        #endregion


        // Estados
        private enum States { Initial, Select, Delete, Piece, NextPiece }
        private States states = States.Initial;

       
       
       

        private void PbNextPiece_Click(object sender, MouseEventArgs e)
        {
            if (states == States.Select)
            {
                states = States.NextPiece;
            }

            if (states == States.Piece)
            {
                // Actualizar NextPiece
                // get target  picture box
                PictureBox pb = (PictureBox)sender;
                int index = 0;

                // establecer indice para lista en dependencia del nombre de PictureBox
                if (pb.Name == "pbNextPiece3")
                    index = 2;

                if (pb.Name == "pbNextPiece2")
                    index = 1;

                SetNextPiece(index, SelectedPieceName);
                // Estado Seleccion
                states = States.Select;
            }

        }
        private void ClickOnCell(CellContext sender)
        {
            Position pos = sender.Position;
            Coord coord = new Coord(pos.Row, pos.Column);

            // States.Delete
            if (states == States.Delete)
            {
                DeleteCell(coord);
            }

            if (states == States.Piece)
            {
                PutPiece(coord, SelectedPieceName);
            }

            if (states == States.NextPiece)
            {
                // PutPiece(coord, SelectedPieceName);
                // Considerar Jugada
                PutPiece(coord, SelectedPieceName);
            }
        }
    }
}
