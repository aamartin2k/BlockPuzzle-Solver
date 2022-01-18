using BPSolver.Objects;

namespace BPSolver.Solver
{
    public partial class GameSolver
    {

        public const int Rank = 10;

        public const int NexPieces = 3;

        private PieceSet Pieces;

        private int[,] Preferences = new int[10, 10]
    {
        {
            8,
            7,
            6,
            5,
            4,
            4,
            5,
            6,
            7,
            8
        },
        {
            7,
            6,
            5,
            4,
            3,
            3,
            4,
            5,
            6,
            7
        },
        {
            6,
            5,
            4,
            3,
            2,
            2,
            3,
            4,
            5,
            6
        },
        {
            5,
            4,
            3,
            2,
            1,
            1,
            2,
            3,
            4,
            5
        },
        {
            4,
            3,
            2,
            1,
            0,
            0,
            1,
            2,
            3,
            4
        },
        {
            4,
            3,
            2,
            1,
            0,
            0,
            1,
            2,
            3,
            4
        },
        {
            5,
            4,
            3,
            2,
            1,
            1,
            2,
            3,
            4,
            5
        },
        {
            6,
            5,
            4,
            3,
            2,
            2,
            3,
            4,
            5,
            6
        },
        {
            7,
            6,
            5,
            4,
            3,
            3,
            4,
            5,
            6,
            7
        },
        {
            8,
            7,
            6,
            5,
            4,
            4,
            5,
            6,
            7,
            8
        }
    };



    }
}
