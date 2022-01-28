using System.Windows.Forms;
using System.Collections.Generic;

using BPSolver.Objects;
using BPSolver.Enums;
using System.Drawing;
using System;
using BPSolver;

namespace WFProt
{
    public partial class Form1 : Form
	{
        // Actualiza form con estado del juego
        public void In_UpdateBoard(GameMetaStatus meta)
        {
            GameStatus status = meta.Status;
            GameSimpleNode dataRoot = meta.SimpleTree;

            // Copiando Cells del board 
            _previewCells = new Board(status.Cells.Cells);

            // Actualizando Control Tree
            UpdateTreeView(dataRoot);

            PieceName piece;
            Bitmap bitMap;

            // Drawing NextPieces
            PictureBox[] pbl = { pbNextPiece1, pbNextPiece2, pbNextPiece3 };

            for (int i = 0; i < Constants.NexPieces; i++)
            {
                piece = status.NextPieces[i];
                bitMap = GetImage(piece);

                // Actualizar imagen en control y
                pbl[i].Image = bitMap;
                // almacenar Piece en Tag
                pbl[i].Tag = piece;
            }

            // Drawing Board
            PieceColor color;

            for (int row = 0; row < Constants.BoardSize; row++)
            {
                for (int col = 0; col < Constants.BoardSize; col++)
                {
                    color = status.Cells[row, col].Color;
                    bitMap = GetBlockImage(color);

                    // simplificar al implementar command pattern
                    //sgBoard[row, col].View = vBackColor;
                    sgBoard[row, col].Image = bitMap;
                    //TODO implement draw temp piece on grid
                    // posible uso
                }
            }
            sgBoard.VerticalScroll.Visible = false;
            sgBoard.HorizontalScroll.Visible = false;
            sgBoard.Invalidate();

            // Update labels
            lbFree.Text = status.FreeCells.ToString();
            lbOcupp.Text = status.OccupiedCells.ToString();
            lbCount.Text = status.CellsCount.ToString();
            lbColComp.Text = status.CompletedColumns.ToString();
            lbRowComp.Text = status.CompletedRows.ToString();

            lbId.Text = status.Id.ToString();
            lbName.Text = status.Nombre;

            //actualizando lista de hijos en tsdbChildren
            // analizar extraer proc
            tsdbChildren.DropDownItems.Clear();

            foreach (var item in meta.Childs)
            {
                // Construir nombre con Id - Nombre
                // Asignar Click Handler
                ToolStripMenuItem mItem = new ToolStripMenuItem(item.Nombre);
                mItem.Tag = item.Id;

                // registrando controlador para Click
                mItem.Click += MItem_Click;    
                tsdbChildren.DropDownItems.Add(mItem);
            }

            // actualizando permitir edicion 
            //UserEnable = meta.IsLeaf;
        }

        private void MItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item;
            item = (ToolStripMenuItem)sender;
            int index = Convert.ToInt32( item.Tag);

            Out_MoveToChild(index);
        }


        //// Cambia color de filas/cols completas antes de borrarlas
        //public void In_SelectRows(int[] indexList)
        //{
        //    foreach (var row in indexList)
        //    {
        //        for (int col = 0; col < Constants.BoardSize; col++)
        //        {
        //            sgBoard[row, col].View = vSelectColor;
        //            //sgBoard.InvalidateCell(sgBoard[row, col]);                                     
        //        }
        //    }

        //    sgBoard.Invalidate();
        //    Application.DoEvents();
        //    System.Threading.Thread.Sleep(500);
        //}

        //public void In_SelectColumns(int[] indexList)
        //{
        //    foreach (var col in indexList)
        //    {
        //        for (int row = 0; row < Constants.BoardSize; row++)
        //        {
        //            sgBoard[row, col].View = vSelectColor;
        //        }
        //    }
        //    sgBoard.Invalidate();
        //    Application.DoEvents();
        //    System.Threading.Thread.Sleep(500);
        //}

        // Notifica resultado de operacion IO
        public void In_NewFileResult(bool status, string text)
        {
            if (!status)
                ShowErrorImage();


            tlsbModelText.Text = text;
        }

        public void In_LoadFileResult(bool status, string file)
        {
            string text = "Error: " + file;

            if (status)
                text = file;
            else
                ShowErrorImage();

            tlsbModelText.Text = text;
        }
        public void In_SaveFileResult(bool status, string file)
        {
            string text = "Error salvando " + file;

            if (status)
                text = file;
            else
                ShowErrorImage();

            tlsbModelText.Text = text;
        }

        // Notifica carga de modelo par habilitar controles
        public void In_UserEnable(bool status)
        {
            UserEnable = status;
        }

        public void In_EmptyCommandStack(bool status)
        {
            tsbUndo.Enabled = !status;
        }

        // Mostrar imagen  de error en statusbar
        private void ShowErrorImage()
        {
            tlsbActionImage.Image = WFProt.Properties.Resources.Error;
        }

        // Resultado de movimientos
        public void In_MoveFirst_Result(bool status)
        {
            Console.WriteLine(string.Format("MoveFirst: {0}", status ));
        }
        public void In_MovePrevious_Result(bool status)
        {
            Console.WriteLine(string.Format("MovePrevious: {0}", status));
        }
        public void In_MoveNext_Result(bool status)
        {
            Console.WriteLine(string.Format("MoveNext: {0}", status));
        }
        public void In_MoveLast_Result(bool status)
        {
            Console.WriteLine(string.Format("MoveLast: {0}", status));
        }
        public void In_MoveToChild_Result(bool status)
        {
            Console.WriteLine(string.Format("MoveToChild: {0}", status));
        }
    }
}
