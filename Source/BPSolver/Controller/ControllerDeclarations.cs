﻿using BPSolver.Objects;
using BPSolver.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class Controller : IBPServer
    {
        // Declaraciones
        
        //private GameSolver _gameSolver;
        private NewSolver _gameSolver;


        // Constructor Explicito NewSolver
        public Controller()
        {
            // Crear deps
            // Solver
            _gameSolver = new NewSolver();
           
           
        }


      
    }
}
