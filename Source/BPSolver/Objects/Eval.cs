using BPSolver.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    [Serializable]
    public class Eval
    {
        public int PieceSize { get; set; }
        public int PieceSizeW { get; private set; }

        public int Preference { get; set; }
        public int PreferenceW { get; private set; }

        public int Neighbors { get; set; }
        public int NeighborsW { get; private set; }

        public int CompleteRoC { get; set; }
        public int CompleteRoCW { get; private set; }

        // Constructor privado
        private Eval(int psw, int prfw, int ngbw, int crcw)
        {
            PieceSizeW = psw;
            PreferenceW = prfw;
            NeighborsW = ngbw;
            CompleteRoCW = crcw;
        }

        // Metodo Factory
        static public Eval GetNewEval()
        {
            Eval newEval = new Eval(Constants.PieceSizeW,
                                    Constants.PreferenceW,
                                    Constants.NeighborsW,
                                    Constants.CompleteRoCW);
            return newEval;
        }


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
