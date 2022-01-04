using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{

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
