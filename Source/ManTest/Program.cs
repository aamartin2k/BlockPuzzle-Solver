using BPSolver;
using System;

namespace ManTest
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Creating Controller.");
  
            IController server = CreateController();
            MultiSolTest client = new MultiSolTest();
            WireUpMultiSol(server, client);
      
            Console.WriteLine("Controller created and wired to client.");

            client.Start();

            Console.WriteLine("ENTER to exit...");
            //Console.ReadLine();
        }


        static IController CreateController()
        {
            IController server = IOHandler.CreateServer();

            return server;
        }

        static void WireUpMultiSol(IController server, MultiSolTest client)
        {
            client.Out_CloseFile = server.In_CloseFile;
            client.Out_LoadFile = server.In_LoadFile;
            client.Out_Solution = server.In_Solution;
            client.Out_SelectIterativeProcess = server.In_SelectIterative;
            client.Out_SelectRecursiveProcess = server.In_SelectRecursive;

            server.Out_CloseFileResult = client.In_CloseFileResult;
            server.Out_LoadFileResult = client.In_LoadFileResult;
            server.Out_UpdateSolutionBoard = client.In_UpdateSolution;
        }



      
    }
}
