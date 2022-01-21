﻿using BPSolver.Enums;
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
            //var ex = Coords.Select(c => Context.Cells[c].Color = NewColor).ToList();
            // new imp
            foreach (var coord in Coords)
            {
                Context.Cells[coord].Color = NewColor;
            }
        }

        public override void Undo()
        {
            // restaurar estado previo, celdas vacias
            //var ex = Coords.Select(c => Context.Cells[c].Color = OldColor).ToList();
            foreach (var coord in Coords)
            {
                Context.Cells[coord].Color = OldColor;
            }
        }
    }
}
