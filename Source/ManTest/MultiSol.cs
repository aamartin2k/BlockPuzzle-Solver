
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
        public Action Out_CloseFile { get; set; }
        public Action<string> Out_LoadFile { get; set; }
        public Action Out_Solution { get; set; }
        public Action Out_SelectRecursiveProcess { get; set; }
        public Action Out_SelectIterativeProcess { get; set; }

        static Queue<string> Files;

        // Start point
        public void Start()
        {
            string[] files = new string[]
               {
                    "tgame01.bmd",
                    "tgame02.bmd",
                    "tgame03.bmd",
                    "tgame04.bmd",
                    "tgame05.bmd",
                    "tgame06.bmd",
                    "tgame07.bmd"
               };

            Console.WriteLine("-> Recursive Process" );
            Files = new Queue<string>(files);
            Out_SelectRecursiveProcess();

            LoadNextFile();

            Console.WriteLine("-> Iterative Process");
            Files = new Queue<string>(files);
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
