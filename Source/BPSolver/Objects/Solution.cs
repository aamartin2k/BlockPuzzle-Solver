using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    public class Solution
    {
        // Valores totales para la solucion
        public Eval TotalEval { get; private set; }

        public Dictionary<int, GameStatus> StatusList { get; private set; }

        public Solution(Eval eval, Dictionary<int, GameStatus> list)
        {
            TotalEval = eval;
            StatusList = list;
        }
        
    }
}
