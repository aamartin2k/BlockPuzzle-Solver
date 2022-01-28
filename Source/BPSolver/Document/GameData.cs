using System;

namespace BPSolver
{
    /// <summary>
    /// Supports serialization to disk of game information along with other irrelevant boilerplate YAGNI stuff.
    /// </summary>
    [Serializable]
    public class GameData
    {
        public int TotalMoves { get; set; }


        public GameData()
        {
            TotalMoves = 0;

        }
    }
}
