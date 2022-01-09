using BPSolver.Enums;
using BPSolver.Objects;

namespace BPSolver.Command
{
    internal class DrawNextPieceCommand : BaseCommand
    {
        private int Index;
        private PieceName OldName, NewName;
        

        public DrawNextPieceCommand(GameStatus cells, 
                                    int index, 
                                    PieceName name) : base (cells)
        {
            Index = index;
            NewName = name;
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
