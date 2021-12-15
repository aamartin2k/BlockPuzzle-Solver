using BPSolver.Enums;
using System.Collections.Generic;

namespace BPSolver.Command
{
    internal class DrawNextPieceCommand : ICommand
    {
        int Index;  //DrawNextPieceCommand
        PieceName OldName, NewName;
        List<PieceName> NextPieces;

        
        public DrawNextPieceCommand(int index, List<PieceName> nextPieces, PieceName name)
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
