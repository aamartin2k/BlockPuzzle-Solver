using System;
using System.Collections.Generic;
using System.Linq;
using BPSolver.Solver;
using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver;

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
            //Program.status

            Test_Solution(Program.status);
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
            Test_OneMove(0, PieceName.Nine, status);
            Test_OneMove(1, PieceName.Four, status);
            Test_OneMove(2, PieceName.FourVert, status);
        }

        static void Test_OneMove(int index, PieceName pname, GameStatus status)
        {
            Console.WriteLine("Movimientos posibles: " + pname);

            List<Movement> lmm = _solver.CreateMovements(index, pname, status);

            foreach (var item in lmm)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("Aplicar Movimientos posibles");

            //GameStatus cloneg;
            //Eval eval;

            foreach (var item in lmm)
            {
                // clonar status
                //cloneg = _solver.CloneGameStatus(status);

                //// aplicar move
                //_solver.MakeMove(item, cloneg);

                //// evaluar
                //eval = _solver.EvaluateMove(item, cloneg);
                //Console.WriteLine(eval);
                //Console.WriteLine();
            }

        }

        static void Test_Solution(GameStatus status)
        {
            //Console.WriteLine("Soluciones posibles");

            //GameTreeNode treeRoot;
            //treeRoot = _solver.CreateSolutionTreePBasico(status);

            //Console.WriteLine(" Nodos: " + treeRoot.Count());

            //// obtener ramas
            //var ramas = treeRoot.SelectLeaves();
            //Console.WriteLine(" Soluciones: " + ramas.Count());

            //foreach (GameTreeNode item in ramas)
            //{
            //    //Console.WriteLine(item.Item.Movement);

            //    // Seleccionar todos hacia arriba e invertir
            //    var invSol = item.SelectPathUpward().Reverse();

            //    DisplaySolution(invSol);
            //}
        }

        static void DisplaySolution(IEnumerable<GameTreeNode> seq)
        {

            foreach (GameTreeNode nod in seq)
            {
                GameStatus gm = nod.Item;

                //Console.WriteLine(string.Format(""));
                Console.WriteLine(string.Format("Nombre: {0} Id: {1}", gm.Nombre, gm.Id));

                foreach (var dkv in gm.NextPieces)
                {
                    int index = dkv.Key;
                    PieceName piece = dkv.Value;
                    Console.WriteLine(string.Format(" Piece {0} Index: {1}", piece, index));
                }

                Console.WriteLine(gm.Movement);
                Console.WriteLine(gm.Evaluation);
                Console.WriteLine();
            }

            // Sumar evaluaciones Parciales
        }
    }
}
