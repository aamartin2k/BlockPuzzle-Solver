using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Command
{
    internal class DrawPieceCommand : ICommand
    {

        List<Coord> RealCoords;
        PieceColor NewColor, OldColor;
        GameStatus GameSt;

        public DrawPieceCommand(List<Coord> realCoords, PieceColor color, GameStatus gamest)
        {
            RealCoords = realCoords;
            NewColor = color;
            GameSt = gamest;

            OldColor = PieceColor.None;
        }

        public void Do()
        {
            // Inserting Piece on board
            var ex = RealCoords.Select(c => GameSt[c].Color = NewColor).ToList();
        }

        public void Undo()
        {
            // restaurar estado previo, celdas vacias
            var ex = RealCoords.Select(c => GameSt[c].Color = OldColor).ToList();
        }
    }
}
