using System;

namespace BPSolver
{
    /// <summary>
    /// Supports serialization to disk of game information along with oher irrelevant boilerplate YAGNI stuff
    /// </summary>
    [Serializable]
    public class Document
    {
        // Document Data
        public DocData DocumentData { get; set; }
        // Game Data
        public GameData GameData { get; set; }
        // Game Tree
        public GameSerialNode GameTree { get; set; }
    }
}
