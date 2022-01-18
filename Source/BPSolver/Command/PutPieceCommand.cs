using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Command
{
    internal class PutPieceCommand : ICommand
    {

        List<Coord> RealCoords;
        PieceColor Color;
        GameSimpleStatus Cells;

        public PutPieceCommand(List<Coord> realCoords, PieceColor color, GameSimpleStatus cells)
        {
            RealCoords = realCoords;
            Color = color;
            Cells = cells;
        }

        public void Do()
        {
            // Inserting Piece on board
            var ex = RealCoords.Select(c => Cells[c].Color = Color).ToList();
        }

        public void Undo()
        {
            // restaurar estado previo, celdas vacias
            var ex = RealCoords.Select(c => Cells[c].Color = PieceColor.None).ToList();
        }
    }
}
