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

            // uncomment to create test documents
            TestDocumentFactory docf = CreateTestDocument(server);
            Console.WriteLine("Controller created and wired to TestDocumentFactory client.");
            docf.Start();


            //MultiSolTest client = CreateMultiSol(server);
            //Console.WriteLine("Controller created and wired to MultiSolTest client.");
            //client.Start();


            // commented to save test output to file via pipe
            // mantest.exe > testLog.txt
            //Console.WriteLine("ENTER to exit...");
            //Console.ReadLine();
        }


        static IController CreateController()
        {
            IController server = IOHandler.CreateServer();

            return server;
        }

        static TestDocumentFactory CreateTestDocument(IController server)
        {
            TestDocumentFactory client = new TestDocumentFactory();

            client.Out_NewFile = server.In_NewFile;
            client.Out_SaveFileAs = server.In_SaveFileAs;
            client.Out_CloseFile = server.In_CloseFile;
            client.Out_LoadFile = server.In_LoadFile;
            client.Out_Draw = server.In_Draw;
            client.Out_DrawNextPiece = server.In_DrawNextPiece;
            client.Out_DeleteGridCell = server.In_DeleteGridCell;

            server.Out_NewFileResult = client.In_NewFileResult;
            server.Out_SaveFileResult = client.In_SaveFileResult;
            server.Out_CloseFileResult = client.In_CloseFileResult;
            server.Out_LoadFileResult = client.In_SaveFileResult;
            server.Out_Draw_Result = client.In_Draw_Result;
            server.Out_DeleteGridCell_Result = client.In_Draw_Result;
            server.Out_DrawNextPiece_Result = client.In_DrawNextPiece_Result;

            return client;
        }


        static MultiSolTest CreateMultiSol(IController server)
        {
            MultiSolTest client = new MultiSolTest();

            client.Out_CloseFile = server.In_CloseFile;
            client.Out_LoadFile = server.In_LoadFile;
            client.Out_Solution = server.In_Solution;
            client.Out_SelectIterativeProcess = server.In_SelectIterative;
            client.Out_SelectRecursiveProcess = server.In_SelectRecursive;

            server.Out_CloseFileResult = client.In_CloseFileResult;
            server.Out_LoadFileResult = client.In_LoadFileResult;
            server.Out_UpdateSolutionBoard = client.In_UpdateSolution;

            return client;

        }



      
    }
}
