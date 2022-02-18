using System;
using System.Collections.Generic;
using BPSolver.Objects;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public interface IBPSolver
    { 
        // Entradas
        // Operaciones IO
        void In_LoadFile(string file);
        void In_SaveFile(string file);

        // Salidas
        Action<GameSimpleStatus> Out_UpdateBoard { get; set; }
    }
}
