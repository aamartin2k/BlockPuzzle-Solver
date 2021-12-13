using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Command
{
    internal class DeleteCellCommand : ICommand
    {

        Coord Coord;
        GameSimpleStatus Cells;
        PieceColor OldColor;

        public DeleteCellCommand(Coord coord, GameSimpleStatus cells)
        {
            Coord = coord;
            Cells = cells;
        }


        public void Do()
        {
            OldColor = Cells[Coord].Color;
            Cells[Coord].Color = PieceColor.None;
        }

        public void Undo()
        {
            Cells[Coord].Color = OldColor;
        }
    }
}
