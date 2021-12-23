using System;
using System.Collections.Generic;
using System.Linq;
using BPSolver.Solver;
using BPSolver.Enums;
using BPSolver.Objects;

namespace ManTest
{
    // Se emplea archivo de prueba para celdas libres y movimientos
    // Pieza: PieceName.Four
    // Index: 1
    //
    //  Tiene 7 posibles posiciones/movimientos
    //  El ultimo mov completa 1 fila y 1 columna


    class SolverTest
    {
        static Solver _solver;

        internal static void Test_Solver()
        {
            // crear 
            Create_Solver();

            Test_FreeCells(Program.status);

            Test_Moves(Program.status);
        }

        // utileria
        static void Create_Solver()
        {
            _solver = new Solver();
        }

        static void Test_FreeCells(GameStatus status)
        {
            int fc = _solver.FreeCellsCount(status.Cells);
            Console.WriteLine(string.Format("Game {0} free cells: {1}", status.Nombre, fc));

            var test = _solver.CreateValidPositionList(status, PieceName.Four);

            Console.WriteLine("Coordenadas posibles");
            foreach (var item in test)
            {
                Console.WriteLine(string.Format(" Row: {0} Col: {1}", item.Row, item.Col));
            }

            

        }

        static void Test_Moves(GameStatus status)
        {
            Console.WriteLine("Movimientos posibles: PieceName.Four");
            List<Movement> lmm = _solver.CreateMovements(1, PieceName.Four, status);

            foreach (var item in lmm)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("Aplicar Movimientos posibles");

            GameStatus cloneg;
            Eval eval;

            foreach (var item in lmm)
            {
                // clonar status
                cloneg = _solver.CloneGameStatus(status);

                // aplicar move
                _solver.MakeMove(item, cloneg);

                // evaluar
                eval = _solver.EvaluateMove(item, cloneg);
                Console.WriteLine(eval);
                Console.WriteLine();
            }

        }
    }
}
