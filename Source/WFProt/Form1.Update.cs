using System.Windows.Forms;
using System.Collections.Generic;
using BPSolver.Solver;
using BPSolver.Objects;
using BPSolver.Enums;
using System.Drawing;

namespace WFProt
{
    public partial class Form1 : Form
	{
        // Actualiza form con estado del juego
        public void In_UpdateBoard(GameSimpleStatus status)
        {
            //ICommand command;
            PieceName name;
            Bitmap bitMap;

            // Drawing NextPieces
            PictureBox[] pbl = { pbNextPiece1, pbNextPiece2, pbNextPiece3 };

            for (int i = 0; i < Constants.NexPieces; i++)
            {
                name = status.NextPieces[i];
                bitMap = GetImage(name);

                // simplificar al implementar command pattern
                // en controller
                pbl[i].Image = bitMap;

            }

            // Drawing Board
            PieceColor color;

            for (int row = 0; row < Constants.Rank; row++)
            {
                for (int col = 0; col < Constants.Rank; col++)
                {
                    color = status[row, col].Color;
                    bitMap = GetBlockImage(color);

                    // simplificar al implementar command pattern
                    sgBoard[row, col].View = vBackColor;
                    sgBoard[row, col].Image = bitMap;

                }
            }
            sgBoard.Invalidate();

            // Update labels
            nUpdMoves.Value = status.CantMoves;
            lbFree.Text = status.FreeCells.ToString();
            lbOcupp.Text = status.OccupiedCells.ToString();
            lbCount.Text = status.CellsCount.ToString();
            lbColComp.Text = status.CompletedColumns.ToString();
            lbRowComp.Text = status.CompletedRows.ToString();

        }

        // Cambia color de filas/cols completas antes de borrarlas
        public void In_SelectRows(int[] indexList)
        {
            foreach (var row in indexList)
            {
                for (int col = 0; col < Constants.Rank; col++)
                {
                    sgBoard[row, col].View = vSelectColor;
                    //sgBoard.InvalidateCell(sgBoard[row, col]);                                     
                }
            }

            sgBoard.Invalidate();
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
        }

        public void In_SelectColumns(int[] indexList)
        {
            foreach (var col in indexList)
            {
                for (int row = 0; row < Constants.Rank; row++)
                {
                    sgBoard[row, col].View = vSelectColor;
                }
            }
            sgBoard.Invalidate();
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
        }

        // Notifica resultado de operacion IO
        public void In_NewFileResult(bool status)
        {
            string text ;

            if (status)
                text = "Nuevo archivo.";
            else
                text = "Error creando nuevo archivo. ";

            tlsbModelText.Text = text;
        }

        public void In_LoadFileResult(bool status, string file)
        {
            string text = "Error cargando " + file;

            if (status)
                text = file;

            tlsbModelText.Text = text;
        }
        public void In_SaveFileResult(bool status, string file)
        {
            string text = "Error salvando " + file;

            if (status)
                text= file;

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

    }
}
