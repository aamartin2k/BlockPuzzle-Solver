using System;

namespace BPSolver
{
    /// <summary>
    /// Supports serialization to disk of game information along with other irrelevant boilerplate YAGNI stuff.
    /// </summary>
    [Serializable]
    public class DocData
    {
        public string Description { get; set; }


        //default values
        public DocData()
        {
            Description = "Description";
        }
    }
}
