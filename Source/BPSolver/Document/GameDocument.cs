using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
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
