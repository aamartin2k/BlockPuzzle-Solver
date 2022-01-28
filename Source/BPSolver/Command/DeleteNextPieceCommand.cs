using BPSolver.Enums;
using BPSolver.Objects;

namespace BPSolver.Command
{
    /// <summary>
    /// Delete a playable piece based on its index.
    /// </summary>
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
            OldName = Context.NextPieces[Index];
            Context.NextPieces[Index] = NewName;
        }

        public override void Undo()
        {
            Context.NextPieces[Index] = OldName;
        }
    }
}
