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
        static public Action<string> Out_LoadFile { get; set; }

        static internal GameStatus status;

        static void Main(string[] args)
        {
            Console.WriteLine("Creando Controller");
            IBPServer server = new Controller();

            // outputs
            Out_LoadFile = server.In_LoadFile;

            // inputs
            server.Out_UpdateBoard = In_UpdateBoard;
            server.Out_UserEnable = In_UserEnable;
            server.Out_LoadFileResult = In_LoadFileResult;
            server.Out_EmptyCommandStack = In_UserEnable;

            Console.WriteLine("Controller creado y enlazado");
            Console.WriteLine("Cargando file...");

            string file = "solver.bmd";
            Out_LoadFile(file);



            Console.WriteLine("Iniciando SolverTest");
            SolverTest.Test_Solver();

            Console.WriteLine("SolverTest Terminado");

            Console.WriteLine("ENTER to exit...");
            //Console.ReadLine();
        }

        static void In_UpdateBoard(GameMetaStatus meta) 
        {
            GameStatus st = meta.Status;
            Console.WriteLine("GameStatus recibido: " + st.Nombre);

            status = st;
        }

        static public void In_UserEnable(bool status)
        {
            
        }

        static public void In_LoadFileResult(bool status, string file)
        {
            string text = "Error cargando " + file;

            if (status)
                text = file;

            Console.WriteLine( text);
        }
    }
}
