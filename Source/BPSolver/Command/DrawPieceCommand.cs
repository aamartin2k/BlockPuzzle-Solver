using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;

namespace BPSolver.Command
{
    /// <summary>
    /// Draw cells based on its location (Coord list) and PieceColor.
    /// </summary>
    internal class DrawCommand : BaseCommand
    {

        private List<Coord> Coords;
        private PieceColor NewColor, OldColor;
    
        public DrawCommand(GameStatus game,
                               List<Coord> coords, 
                               PieceColor color) : base (game)
        {
            Coords = coords;
            NewColor = color;
            OldColor = PieceColor.None;
        }

        public override void Do()
        {
            foreach (var coord in Coords)
            {
                Context.Cells[coord].Color = NewColor;
            }
        }

        public override void Undo()
        {
            foreach (var coord in Coords)
            {
                Context.Cells[coord].Color = OldColor;
            }
        }
    }
}
