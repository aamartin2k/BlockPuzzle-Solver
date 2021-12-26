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

        // Totales
        public int PieceSizeTotal { get { return PieceSize * PieceSizeW; } }
        public int PreferenceTotal { get { return Preference * PreferenceW; } }
        public int NeighborsTotal { get { return Neighbors * NeighborsW; } }
        public int CompleteRoCTotal { get { return CompleteRoC * CompleteRoCW; } }

        // Constructor privado
        private Eval(int psw, int prfw, int ngbw, int crcw)
        {
            PieceSizeW = psw;
            PreferenceW = prfw;
            NeighborsW = ngbw;
            CompleteRoCW = crcw;
        }

       
        public int Total
        {
            get
            {
                return PieceSizeTotal +
                       PreferenceTotal +
                       NeighborsTotal +
                       CompleteRoCTotal;
            }  
        }

        public override string ToString()
        {
            return string.Format("PSize: {0} Pref: {1} Neighb: {2} ComplRoC: {3}  Total: {4}",
                PieceSize, Preference, Neighbors, CompleteRoC, Total);
        }

        // Factory
        // Metodo Factory para Eval de GameStatus
        static public Eval GetNewEval()
        {
            Eval newEval = new Eval(Constants.PieceSizeW,
                                    Constants.PreferenceW,
                                    Constants.NeighborsW,
                                    Constants.CompleteRoCW);
            return newEval;
        }

        // Metodo Factory para Eval de Solution
        // los coeficientes se hacen igual a 1 para no alterar
        // el resultado
        static public Eval GetTotalEval()
        {
            Eval newEval = new Eval(1, 1, 1, 1);
            return newEval;
        }
    }
}
