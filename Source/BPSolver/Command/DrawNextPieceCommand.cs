using BPSolver.Enums;
using BPSolver.Objects;

namespace BPSolver.Command
{
    /// <summary>
    /// Update a playable piece based on its index and name
    /// </summary>
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
            OldName = Context.NextPieces[Index];
            Context.NextPieces[Index] = NewName;
        }

        public override void Undo()
        {
            Context.NextPieces[Index] = OldName;
        }


    }
}
