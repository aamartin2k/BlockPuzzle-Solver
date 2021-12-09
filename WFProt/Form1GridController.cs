using System;
using System.Drawing;
using System.Windows.Forms;
using SourceGrid;
using Cells = SourceGrid.Cells;
using Views = SourceGrid.Cells.Views;
using BPSolver.Enums;


namespace WFProt
{
    public partial class Form1 : Form
    {
        // Declarations
        // Cell Views, one for each piece color.
        Views.Cell vBackColor, vColorOne, vColorTwo, vColorThree, vColorFour,
                   vColorFive, vColorSix, vColorSeven, vColorEight, vColorNine;

        // Grid controllers, nested private classes
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

        private void ClickOnCell(CellContext sender)
        {
            Position pos = sender.Position;
            label1.Text = string.Format("Row: {0} Col:{1}", pos.Row, pos.Column);
            tslbCoordText.Text = string.Format("Row: {0} Col:{1}", pos.Row, pos.Column);


            if (SelectedPieceName == PieceName.None) 
                return;

            // crear inst de comando DrawPieceOnBoard
            HelpDrawPieceOnBoard(sender);
        }

    }
}