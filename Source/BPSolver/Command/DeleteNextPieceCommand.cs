﻿using BPSolver.Enums;
using BPSolver.Objects;

namespace BPSolver.Command
{
    internal class DeleteNextPieceCommand : BaseCommand
    {
        private int Index;
        private PieceName OldName, NewName;


        public DeleteNextPieceCommand(GameStatus cells, int index) : base(cells)
        {
            Index = index;
            NewName = PieceName.None;
        }


        public override void Do()
        {
            // guardar estado previo
            OldName = Context.NextPieces[Index];

            // aplicar estado nuevo
            Context.NextPieces[Index] = NewName;
        }

        public override void Undo()
        {
            // restaurar estado previo
            Context.NextPieces[Index] = OldName;
        }
    }
}
