using BPSolver.Objects;
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
        // estado del juegp
        private GameSimpleStatus _gameStatus;
        //ref a resolvedor
        // crear mediante prop
        // eliminar ref de Form  

        //private GameSolver _gameSolver;
        private NewSolver _gameSolver;


        // Constructor Explicito NewSolver
        public Controller()
        {
            _gameSolver = new NewSolver();
            //_gameSolver = new GameSolver();
           
        }


      
    }
}
