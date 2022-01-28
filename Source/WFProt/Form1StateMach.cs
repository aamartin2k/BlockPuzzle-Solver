using BPSolver.Enums;
using BPSolver.Objects;
using SourceGrid;
using System;

using System.Windows.Forms;

namespace WFProt
{
    public partial class Form1 : Form
    {
        #region State Machine Context

        private StMachContext _stMContext;

        void CreateStateMachine()
        {
            _stMContext = new StMachContext();

            // creando todos los estados
            _stMContext.CreateStates();

            // asignando procedimientos de salida a delegates
            _stMContext.GridCellDeletionState.Out_DeleteGridCell = StmOut_DeleteGridCell;
            _stMContext.NextPieceDeletionState.Out_DeleteNextPiece = StmOut_DeleteNextPiece;

            _stMContext.NextPieceDrawingState.Out_DrawNextPiece = StmOut_DrawNextPiece;
            _stMContext.GridCellDrawingState.Out_DrawGrid = StmOut_DrawPiece;
            _stMContext.NextPiecePlayState.Out_DrawGridPlay = StmOut_DrawGridPlay;

            _stMContext.Out_ShowCurrentPiece = ShowCurrentPiece;
            _stMContext.Out_ShowCurrentAction = ShowCurrentAction;

            _stMContext.PieceSettingState.Out_DrawPreview = StmOut_DrawPreview;
            _stMContext.PieceSettingState.Out_DeletePreview = StmOut_DeletePreview;
        }

        #endregion

        #region Inputs
        private void TsbArrow_Click(object sender, EventArgs e)
        {
            // Modo Seleccion
            _stMContext.ActionSelectClicked();
        }
        private void TsbDelete_Click(object sender, EventArgs e)
        {
            // Modo Borrar
            _stMContext.ActionDeleteClicked();
        }

        // Ejecutar Accion de secuencia
        private void TsbSequence_Click(object sender, EventArgs e)
        {
            ToolStripItem itm = (ToolStripItem)sender;
            SequenceAction action = (SequenceAction)itm.Tag;

            switch (action)
            {
                case SequenceAction.First:
                    Out_MoveFirst();
                    break;
                case SequenceAction.Previous:
                    Out_MovePrevious();
                    break;
                case SequenceAction.Next:
                    Out_MoveNext();
                    break;
                case SequenceAction.Last:
                    Out_MoveLast();
                    break;
                default:
                    break;
            }
        }

        private void TsbSetPiece_Click(object sender, EventArgs e)
        {
            // Modo Seleccionar Pieza
            ToolStripItem itm = (ToolStripItem)sender;
            PieceName piece = (PieceName)itm.Tag;

            _stMContext.PieceButtonClicked(piece);
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

            _stMContext.NextPieceImageClicked(index, piece);

        }

       

        #endregion

        #region Outputs
        void StmOut_DrawPiece(Coord pos, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Col, piece));
            Out_DrawPiece(pos, piece);
        }

        void StmOut_DrawNextPiece(int index, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar NextPiece index: {0} Image: {1} ", index, piece));
            Out_DrawNextPiece(index, piece);
        }

        void StmOut_DeleteGridCell(Coord pos)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar Grid Cell Row: {0} Col: {1}", pos.Row, pos.Col));
            Out_DeleteGridCell(pos);
        }

        void StmOut_DeleteNextPiece(int index)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar NextPieceImage index {0}", index));
            Out_DeleteNextPiece(index);
        }

        void StmOut_DrawGridPlay(Coord pos, PieceName piece, int index)
        {
            // All three next pieces must be present to make a play
            bool ret = false;
            ret = ret | (PieceName.None == (PieceName)pbNextPiece1.Tag);
            ret = ret | (PieceName.None == (PieceName)pbNextPiece2.Tag);
            ret = ret | (PieceName.None == (PieceName)pbNextPiece3.Tag);

            if (ret)
            {
                string text = "All three next pieces must be present to make a play";
                
                MessageBox.Show(this, text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Console.WriteLine(string.Format("OUTPUT. PLAY. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Col, piece));
                Out_DrawGridPlay(pos, piece, index);
            }
        }

        //void StmOut_DrawPreview()
        //{

        //}

        //void StmOut_DeletePreview()
        //{

        //}
        #endregion






    }
}
