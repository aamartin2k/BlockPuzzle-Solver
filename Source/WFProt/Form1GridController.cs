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
        private Views.Cell vBackColor, vTransColor, vSolutionColor;
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
            private Action<CellContext> _mouseLeaveHandler;

            // constructor
            public ClickController(Action<CellContext> chandler, 
                                   Action<CellContext> mehandler,
                                   Action<CellContext> mlhandler)
            {
                _clickHandler = chandler;
                _mouseEnterHandler = mehandler;
                _mouseLeaveHandler = mlhandler;
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

            public override void OnMouseLeave(CellContext sender, EventArgs e)
            {
                base.OnMouseLeave(sender, e);
                _mouseLeaveHandler(sender);
            }
        }
        private void MouseLeaveCell(CellContext sender)
        {
            if (sender.Grid.Name == sgBoard.Name)
                MouseLeaveGameGrid(sender.Position);
        }

        private void MouseLeaveGameGrid(Position pos)
        {
            MouseLeaveGameCell(pos);
        }

        // Se ejecuta cuando el mouse pasa sobre las celdas del grid,
        // se emplea un grid controller que ejecuta este método
        // Se comparte en los dos grids
        // En dependencia del Sender se ejecuta la accion concreta
        private void MouseEnterCell(CellContext sender)
        {
            if (sender.Grid.Name == sgBoard.Name)
                MouseEnterGameGrid(sender.Position);
            else
                MouseEnterSolutionGrid(sender.Position);
        }

        private void MouseEnterGameGrid(Position pos)
        {
            lbRowPos.Text = string.Format("Row: {0}", pos.Row);
            lbColumnPos.Text = string.Format("Column: {0}", pos.Column);

            MouseEnterGameCell(pos);
           
        }

        private void MouseEnterSolutionGrid(Position pos)
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
            //else
            //    ClickOnSolutionCell(sender.Position);
        }

        private void ClickOnGameCell(Position pos)
        {
            Coord coord = new Coord(pos.Row, pos.Column);

            _stMContext.GridCellClicked(coord);
        }

        //private void ClickOnSolutionCell(Position pos)
        //{
        //    // TBI
        //}

    }
}