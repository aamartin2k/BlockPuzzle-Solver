using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        public bool InOut_TestPiece(Coord insertCoord, PieceName name, Board cells)
        {
            return Utils.TestPiece(insertCoord, name, cells); 
        }

        public PieceColor InOut_GetPieceColor(PieceName name)
        {
            Piece instance = PieceSet.GetPiece(name);
            return instance.Color;
        }

        public List<Coord> InOut_GetRealCoords(PieceName name, Coord point)
        {
            Piece instance = PieceSet.GetPiece(name);
            return Piece.GetRealCoords(instance, point);
        }

    }
}
