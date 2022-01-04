using BPSolver;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTest
{
    // El archivo solver.bmd contiene el modelo de prueba
    class Program
    {
        static public Action Out_NewFile { get; set; }
        static public Action Out_CloseFile { get; set; }
        static public Action<string> Out_LoadFile { get; set; }
        static public Action Out_SaveFile { get; set; }
        static public Action<string> Out_SaveFileAs { get; set; }
        static public Action Out_Solution { get; set; }

        static internal GameStatus status;

        static Queue<string> Files;

        static void Main(string[] args)
        {
            // pruebas nueva implementacion
           

            Console.WriteLine("Creando Controller");

            //IBPServer server = new Controller();
            IController server = IOHandler.CreateServer();

            // outputs
            Out_NewFile = server.In_NewFile;
            Out_CloseFile = server.In_CloseFile;
            Out_LoadFile = server.In_LoadFile;

            Out_Solution = server.In_Solution;
            Out_SaveFileAs = server.In_SaveFileAs;

            // inputs
            server.Out_NewFileResult = In_NewFileResult;
            server.Out_CloseFileResult = In_CloseFileResult;
            server.Out_LoadFileResult = In_LoadFileResult;


            server.Out_UpdateGameBoard = In_UpdateBoard;
            server.Out_UpdateSolutionBoard = In_UpdateSolution;
            server.Out_UserEnable = In_UserEnable;
            server.Out_EmptyCommandStack = In_UserEnable;
            server.Out_SaveFileResult = In_SaveFileResult;
            

            Console.WriteLine("Controller creado y enlazado");

            //TestLoadFile();
            //TestNewImpl();

            Console.WriteLine("Iniciando  MultipleLoadAndSave");
            MultipleLoadAndSave();


            //Console.WriteLine("Iniciando SolverTest");
            //SolverTest.Test_Solver();

            //Console.WriteLine("SolverTest Terminado");

            Console.WriteLine("ENTER to exit...");
            Console.ReadLine();
        }

        static void TestNewImpl()
        {
            Out_NewFile();
            Out_SaveFileAs(@"D:\Temp\Demos\savedAs.bmd");
            Out_CloseFile();

            Out_LoadFile(@"D:\Temp\Demos\savedAs.bmd");
            Out_CloseFile();
        }

        static void TestLoadFile()
        {
            //Out_LoadFile("sdfer.nofile");

            Out_LoadFile("solver.bmd");
            Out_SaveFile();




        }

        static void MultipleLoadAndSave()
        {
            
            // Lista de files a convertir
            string[] files = new string[]  
                {
                        "juego.bmd",
                        "juego1.bmd",
                        "juego2.bmd",
                        "juego3.bmd",
                        "juego4.bmd",
                        "juego5.bmd",
                        "juego6.bmd",
                        "juegoReal.bmd",
                        "juegoReal_Copia.bmd",
                        "newSer.bmd",
                        "solver.bmd"
                  };

            Files = new Queue<string>(files);

            //string file = Files.Dequeue();
            string file = Files.Peek();

            Console.WriteLine("Cargando " + file);

            Out_LoadFile(file);
            
        }
        static void MultipleSolution()
        {
            ////                               solver.bmd	   juego3		,juego1.bmd	   juego2.bmd	 juego4.bmd		juego5.bmd		juego6.bmd
            //string[] files = new string[]  { "solver.bmd", "juego3.bmd", "juego1.bmd", "juego2.bmd", "juego4.bmd", "juego5.bmd", "juego6.bmd" } ;

            //Files = new Queue<string>(files);

            //string file = Files.Dequeue();
            //Console.WriteLine("Cargando " + file);

            //Out_LoadFile(file);
            //Out_Solution();
        }

        #region Entradas
        static void In_UpdateSolution(SolutionMetaStatus meta)
        {
            //if (Files.Count > 0)
            //{
            //    string file = Files.Dequeue();
            //    Console.WriteLine("Cargando " + file);

            //    Out_LoadFile(file);
            //    Out_Solution();
            //}
            //else
            //    Console.WriteLine("Terminado " );
        }

        static void In_UpdateBoard(GameMetaStatus meta) 
        {
            GameStatus st = meta.Status;
            Console.WriteLine("GameStatus recibido: " + st.Nombre);
            //Console.WriteLine("Proceso terminado ");
            //Console.WriteLine(st.Movement);
            //Console.WriteLine(st.Evaluation);
            //Console.WriteLine();

            //status = st;
        }

        static public void In_NewFileResult(bool status, string message)
        {
            Console.WriteLine(message);
        }

        static public void In_CloseFileResult(bool status, string message)
        {
            Console.WriteLine(message);
        }


        static public void In_UserEnable(bool status)
        {
            Console.WriteLine("In_UserEnable: " + status);
        }

        static public void In_LoadFileResult(bool status, string message)
        {
            

            if (status)
            {
                Console.WriteLine(" File loaded OK. Saving as...");
            }
            else
            {
                Console.WriteLine("Error loading file. Moving on. ");
                Console.WriteLine(message);
            }

            if (Files.Count > 0)
            {
                string file = Files.Dequeue();
                // saving as
                string newFile = "Conv_" + file;
                Out_SaveFileAs(newFile);

                if (Files.Count > 0)
                {
                    // loading next in queue
                    file = Files.Peek();
                    Console.WriteLine("Cargando " + file);

                    Out_LoadFile(file);
                }
            }
            else
                Console.WriteLine("Terminado.");

        }

        static public void In_SaveFileResult(bool status, string message)
        {
            if (status)
                Console.WriteLine("File saved OK.  ");
            else
            {
                Console.WriteLine("File saved with Error!");
                Console.WriteLine(message);
            }
            

        }


        #endregion
    }
}
