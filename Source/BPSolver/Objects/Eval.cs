using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    public class Eval
    {
        public int PieceSize ;
        public int PieceSizeW = 1;

        public int Preference ;
        public int PreferenceW = 1;

        public int Neighbors ;
        public int NeighborsW = 1;

        public int CompleteRoC ;
        public int CompleteRoCW = 1;


        public int Total
        {
            get
            {
                return (PieceSize * PieceSizeW) +
                       (Preference * PreferenceW) +
                       (Neighbors * NeighborsW) +
                       (CompleteRoC * CompleteRoCW);
            }  
        }

        public override string ToString()
        {
            return string.Format("PSize: {0} Pref: {1} Neighb: {2} ComplRoC: {3}\n  Total: {4}",
                PieceSize, Preference, Neighbors, CompleteRoC, Total);
        }
    }
}
