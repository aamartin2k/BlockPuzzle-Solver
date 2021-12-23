using System;
using BPSolver.Enums;
using System.Collections.Generic;

namespace BPSolver.Command
{
    internal class DeleteNextPieceCommand : ICommand
    {
        int Index;
        PieceName OldName, NewName;
        Dictionary<int, PieceName> NextPieces;


        public DeleteNextPieceCommand(int index, Dictionary<int, PieceName> nextPieces)
        {
            Index = index;
            NewName = PieceName.None;
            NextPieces = nextPieces;
        }




        public void Do()
        {
            // guardar estado previo
            OldName = NextPieces[Index];

            // aplicar estado nuevo
            NextPieces[Index] = NewName;
        }

        public void Undo()
        {
            // restaurar estado previo
            NextPieces[Index] = OldName;
        }
    }
}
