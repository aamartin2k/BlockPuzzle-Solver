
using BPSolver;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTest
{
    class MultiSolTest
    {
        // Output
        internal Action Out_CloseFile { get; set; }
        internal Action<string> Out_LoadFile { get; set; }
        internal Action Out_Solution { get; set; }
        internal Action Out_SelectRecursiveProcess { get; set; }
        internal Action Out_SelectIterativeProcess { get; set; }

        static Queue<string> Files;

        // Start point
        public void Start()
        {
            // list of files to solve in batch
            string[] files = new string[]
               {
                    @"TestDocs\tgame01.bmd",
                    @"TestDocs\tgame02.bmd",
                    @"TestDocs\tgame03.bmd",
                    @"TestDocs\tgame04.bmd",
                    @"TestDocs\tgame05.bmd",
                    @"TestDocs\tgame06.bmd",
                    @"TestDocs\tgame07.bmd"
               };

            Console.WriteLine("-> Recursive Process" );
            Files = new Queue<string>(files);
            // Switching procedure
            Out_SelectRecursiveProcess();

            LoadNextFile();

            Console.WriteLine("-> Iterative Process");
            Files = new Queue<string>(files);
            // Switching procedure
            Out_SelectIterativeProcess();

            LoadNextFile();
        }

        private void LoadNextFile()
        {
            if (Files.Count > 0)
            {
                string file = Files.Peek();
                Console.WriteLine("Loading " + file);
                Out_LoadFile(file);
            }
            else
                Console.WriteLine("Done!");
        }

        // Input
        public void In_CloseFileResult(bool status, string message)
        {
            Console.WriteLine("Close File Result: " + message);
        }

        public void In_LoadFileResult(bool status, string message)
        {
            
            if (status)
            {
                Console.WriteLine("  File loaded OK");

                if (Files.Count > 0)
                {
                    string file = Files.Dequeue();
                    // Running solution on loaded file
                    Out_Solution();
                }

            }
            else
            {
                Console.WriteLine("  Error loading file.");
                Console.WriteLine(message);
                Files.Dequeue();

                LoadNextFile();
                
            }

        }
        

        public void In_UpdateSolution(SolutionMetaStatus meta)
        {
            Console.WriteLine(string.Format("Elapsed Time: {0}", meta.ProcTime));
            Console.WriteLine(string.Format("Node Count: {0}", meta.NodeCount));
            Console.WriteLine(string.Format("Solutions Found: {0}", meta.Solutions.Count));
            Console.WriteLine();

            LoadNextFile();
        }

       
       


       

      

    }
}
