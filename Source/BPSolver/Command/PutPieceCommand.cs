using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Command
{
    internal class DrawPieceCommand : ICommand
    {

        List<Coord> RealCoords;
        PieceColor NewColor, OldColor;
        GameStatus Cells;

        public DrawPieceCommand(List<Coord> realCoords, PieceColor color, GameStatus cells)
        {
            RealCoords = realCoords;
            NewColor = color;
            Cells = cells;

            OldColor = PieceColor.None;
        }

        public void Do()
        {
            // Inserting Piece on board
            var ex = RealCoords.Select(c => Cells[c].Color = NewColor).ToList();
        }

        public void Undo()
        {
            // restaurar estado previo, celdas vacias
            var ex = RealCoords.Select(c => Cells[c].Color = OldColor).ToList();
        }
    }
}
