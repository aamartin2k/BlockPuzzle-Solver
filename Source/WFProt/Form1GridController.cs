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
        private Views.Cell vBackColor, vSelectColor, vSolutionColor;
        //Views.Cell vBackColor, vColorOne, vColorTwo, vColorThree, vColorFour,
        //           vColorFive, vColorSix, vColorSeven, vColorEight, vColorNine;

        // Grid controllers, nested private classes
        // Controlador de evento click en Cell de SourceGrid
        // el metodo concreto ClickOnCell se pasa en  el constructor 
        // y se asigna a cada cell en su creacion
        private class ClickController : SourceGrid.Cells.Controllers.ControllerBase
        {
            private Action<CellContext> _clickHandler;
            private Action<CellContext> _mouseEnterHandler;

            // constructor
            public ClickController(Action<CellContext> chandler, Action<CellContext> mehandler)
            {
                _clickHandler = chandler;
                _mouseEnterHandler = mehandler;
            }

             // Reaccionar a click en cell
            public override void OnClick(CellContext sender, EventArgs e)
            {
                base.OnClick(sender, e);
                _clickHandler(sender);
            }

            // Reaccionar a Mouse Enter
            public override void OnMouseEnter(CellContext sender, EventArgs e)
            {
                base.OnMouseEnter(sender, e);
                _mouseEnterHandler(sender);
            }
        }

        // Se ejecuta cuando el mouse pasa sobre las celdas del grid,
        // se emplea un grid controller que ejecuta este método
        // Se comparte en los dos grids
        // En dependencia del Sender se ejecuta la ccion concreta
        private void MouseEnterCell(CellContext sender)
        {
            if (sender.Grid.Name == sgBoard.Name)
                UpdateGamePosition(sender.Position);
            else
                UpdateSolutionPosition(sender.Position);
        }

        private void UpdateGamePosition(Position pos)
        {
            lbRowPos.Text = string.Format("Row: {0}", pos.Row);
            lbColumnPos.Text = string.Format("Column: {0}", pos.Column);
        }

        private void UpdateSolutionPosition(Position pos)
        {
            lbSolutionRowPos.Text = string.Format("Row: {0}", pos.Row);
            lbSolutionColumnPos.Text = string.Format("Column: {0}", pos.Column);
        }

        // Se ejecuta cuando se hace click en una celda
        // Se comparte en los dos grids
        // En dependencia del Sender se ejecuta la accion concreta
        private void ClickOnCell(CellContext sender)
        {
            if (sender.Grid.Name == sgBoard.Name)
                ClickOnGameCell(sender.Position);
            else
                ClickOnSolutionCell(sender.Position);
        }

        private void ClickOnGameCell(Position pos)
        {
            Coord coord = new Coord(pos.Row, pos.Column);

            context.GridCellClicked(coord);
        }

        private void ClickOnSolutionCell(Position pos)
        {
            // TBI
        }

    }
}