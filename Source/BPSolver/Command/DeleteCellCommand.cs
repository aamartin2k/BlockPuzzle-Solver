using BPSolver.Enums;
using BPSolver.Objects;

namespace BPSolver.Command
{
    internal class DeleteCellCommand : BaseCommand
    {

        private Coord Coord;
        private PieceColor OldColor;

        public DeleteCellCommand(GameStatus cells, Coord coord) : base ( cells)
        {
            Coord = coord;
        }


        public override void Do()
        {
            OldColor = Context.Cells[Coord].Color;
            Context.Cells[Coord].Color = PieceColor.None;
        }

        public override void Undo()
        {
            Context.Cells[Coord].Color = OldColor;
        }
    }
}
