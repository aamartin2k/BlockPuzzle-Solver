using System;
using System.Drawing;
using System.Windows.Forms;
using SourceGrid;
using Cells = SourceGrid.Cells;
using Views = SourceGrid.Cells.Views;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    public partial class Form1 : Form
    {
        // Declarations
        // Cell Views, background color.
        Views.Cell vBackColor, vSelectColor;
        //Views.Cell vBackColor, vColorOne, vColorTwo, vColorThree, vColorFour,
        //           vColorFive, vColorSix, vColorSeven, vColorEight, vColorNine;

        // Grid controllers, nested private classes
        // Controlador de evento click en Cell de SourceGrid
        // el metodo concreto ClickOnCell se pasa en  el constructor 
        // y se asigna a cada cell en su creacion
        private class ClickController : SourceGrid.Cells.Controllers.ControllerBase
        {
            private Action<CellContext> _handler;
            // constructor
            public ClickController(Action<CellContext> handler)
            { _handler = handler; }

            public override void OnClick(CellContext sender, EventArgs e)
            {
                base.OnClick(sender, e);
                _handler(sender);
            }
        }

        //
        private void ClickOnCell(CellContext sender)
        {
            Position pos = sender.Position;
            Coord coord = new Coord(pos.Row, pos.Column);

            label1.Text = string.Format("Row: {0} Col:{1}", pos.Row, pos.Column);
            tslbCoordText.Text = string.Format("Row: {0} Col:{1}", pos.Row, pos.Column);

            // new
            switch (SelectedAction)
            {
                case CommandAction.None:
                    if (SelectedPieceName != PieceName.None)
                        PutPiece(coord, SelectedPieceName);
                    break;

                case CommandAction.Undo:
                    break;

                case CommandAction.Delete:
                    DeleteCell(coord);
                    break;

                default:
                    break;
            }
            // end new

            if (SelectedPieceName == PieceName.None)
            {
                // Ejecutar Action Delete
                if (SelectedAction == CommandAction.Delete)
                return;
            }
               

           
            
        }

    }
}