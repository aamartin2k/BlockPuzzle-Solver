using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFProt
{
    public partial class Form1 : Form
    {

        public void In_UpdateSolutionBoard(SolutionMetaStatus meta)
        {
            Console.WriteLine(" In_UpdateSolutionBoard");

            // Store 

            // Update List
            this.bdsSolutions.DataSource = meta.Solutions;




        }
        private void UpdateSolutionList()
        {
            
        }

        private void UpdateSolutionTree(Dictionary<int, GameStatus> gameStList)
        {

        }
    }
}
