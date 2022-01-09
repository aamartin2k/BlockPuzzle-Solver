using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Command
{
    internal class DrawPieceCommand : BaseCommand
    {

        private List<Coord> Coords;
        private PieceColor NewColor, OldColor;
    
        public DrawPieceCommand(GameStatus game,
                               List<Coord> coords, 
                               PieceColor color) : base (game)
        {
            Coords = coords;
            NewColor = color;
            OldColor = PieceColor.None;
        }

        public override void Do()
        {
            // Inserting Piece on board
            var ex = Coords.Select(c => Context[c].Color = NewColor).ToList();
        }

        public override void Undo()
        {
            // restaurar estado previo, celdas vacias
            var ex = Coords.Select(c => Context[c].Color = OldColor).ToList();
        }
    }
}
