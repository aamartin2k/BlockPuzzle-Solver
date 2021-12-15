using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver.Solver;
using System;
using System.Collections.Generic;

namespace BPSolver
{
    public partial class Controller : IBPServer
    {
        

  
        // Salidas
        public Action<GameStatus> Out_UpdateBoard { get; set; }
        public Action<bool> Out_UserEnable { get; set; }
        public Action<bool> Out_NewFileResult { get; set; }
        public Action<bool, string> Out_LoadFileResult { get; set; }
        public Action<bool, string> Out_SaveFileResult { get; set; }
        public Action<bool> Out_EmptyCommandStack { get; set; }
   
        public Action<int[]> Out_SelectRows { get; set; }
        public Action<int[]> Out_SelectColumns { get; set; }



    }
}
