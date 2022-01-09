using BPSolver;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTest
{
    
    class Program
    {


        static void Main(string[] args)
        {
            // pruebas nueva implementacion
          
            Console.WriteLine("Creating Controller.");
  
            IController server = CreateController();

            MultiSol client = new MultiSol();

            WireUpMultiSol(server, client);

            Console.WriteLine("Controller created and wired to client.");


            client.Start();



            Console.WriteLine("ENTER to exit...");
            Console.ReadLine();
        }


        static IController CreateController()
        {
            IController server = IOHandler.CreateServer();

            return server;
        }

        static void WireUpMultiSol(IController server, MultiSol client)
        {
            client.Out_CloseFile = server.In_CloseFile;
            client.Out_LoadFile = server.In_LoadFile;
            client.Out_Solution = server.In_Solution;

            server.Out_CloseFileResult = client.In_CloseFileResult;
            server.Out_LoadFileResult = client.In_LoadFileResult;
            server.Out_UpdateSolutionBoard = client.In_UpdateSolution;
        }



      
    }
}
