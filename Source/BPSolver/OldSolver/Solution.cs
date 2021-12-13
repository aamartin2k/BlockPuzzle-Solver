using BPSolver.Solver;
using System.Collections.Generic;
using System.Linq;



namespace BPSolver.Solver
{
    public class Solution
    {
        public List<Move> Moves
        {
            get;
            set;
        }

        public bool CompleteRoC
        {
            get
            {
                int count = 0;
                foreach (Move item in Moves)
                {
                    if (item.CompleteRoC)
                    {
                        count++;
                    }
                }
                return count > 0;
            }
        }

        public int Value => Moves.Sum((Move z) => z.Value);

        public int Preference => Moves.Sum((Move z) => z.Preference);

        public Solution()
        {
            Moves = new List<Move>();
        }
    }

}
