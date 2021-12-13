using BPSolver.Enums;
using System.Collections.Generic;

namespace BPSolver.Command
{
    internal class SetNextPieceCommand : ICommand
    {
        int Index;
        PieceName OldName, NewName;
        List<PieceName> NextPieces;

        //In_SetNextPiece(int index, PieceName name)

        public SetNextPieceCommand(int index, List<PieceName> nextPieces, PieceName name)
        {
            Index = index;
            NewName = name;
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
