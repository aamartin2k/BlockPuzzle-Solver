using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver;
using ControlTreeView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFProt
{
    public partial class Form1 : Form
    {
        //private List<Solution> _solutions;
        private Solution _solution;

        public void In_UpdateSolutionBoard(SolutionMetaStatus meta)
        {
            this.tabControl1.SelectedTab = this.tbpSolution;
            Console.WriteLine(" In_UpdateSolutionBoard");

            // Store 
            //_solutions = meta.Solutions;
            // Update List
            UpdateSolutionList(meta.Solutions);

            lbProcTime.Text = meta.ProcTime;
            lbNodeCount.Text = meta.NodeCount.ToString();

        }
        private void UpdateSolutionList(List<Solution>  solutions)
        {
            lbSoluciones.Items.Clear();

            var qry = from sol in solutions
                      where sol.TotalEval.CompleteRoCTotal >= 1000
                      orderby sol.TotalEval.Total descending
                      select sol;

            foreach (Solution sol in qry)
            {
                lbSoluciones.Items.Add(sol);
            }
        }

       
        private void lbSoluciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("lbSoluciones_SelectedIndexChanged " + lbSoluciones.SelectedItem);

            if (null != lbSoluciones.SelectedItem)
            {
                //Console.WriteLine("sol != null");
                Solution sol = (Solution)lbSoluciones.SelectedItem ;

                if (null != sol)
                {
                    UpdateSolutionTree(sol.StatusList);
                    _solution = sol;
                }

            }
           
        }
        private void UpdateSolutionTree(Dictionary<int, GameStatus> statusList)
        {
            
            cTreeSolution.Nodes.Clear();

            cTreeSolution.BeginUpdate();

            BPNode node;
            GameStatus game;
            int index;
            CTreeNode child = null;

            lbEvals.Items.Clear();

            foreach (var kvp in statusList)
            {
                game = kvp.Value;
                index = kvp.Key;

                if (null != game.Evaluation)
                    lbEvals.Items.Add(game.Evaluation);

                node = new BPNode(game.Nombre);
                node.Index = index;
                //node.BackColor = KnownColorList[colorIdx];
                node.MakeCurrent = ActionCurrentSol;

                CTreeNode ctlNodo;
                ctlNodo = new CTreeNode(index.ToString(), node);

                if (child == null)
                    cTreeSolution.Nodes.Add(ctlNodo);
                else
                    child.Nodes.Add(ctlNodo);

                child = ctlNodo;
            }

            cTreeSolution.EndUpdate();
        }

        private void ActionCurrentSol(int index)
        {
            Console.WriteLine(" Hacer actual id: " + index.ToString());

            PieceName piece;
            Bitmap bitMap;
            GameStatus status = _solution.StatusList[index];

            // Drawing NextPieces
            PictureBox[] pbl = { pbNextPieceSol1, pbNextPieceSol2, pbNextPieceSol3 };

            for (int i = 0; i < Constants.NexPieces; i++)
            {
                if (status.NextPieces.ContainsKey(i))
                    piece = status.NextPieces[i];
                else
                    piece = PieceName.None;

                bitMap = GetImage(piece);

                // Actualizar imagen en control y
                pbl[i].Image = bitMap;
                // almacenar Piece en Tag
                //pbl[i].Tag = piece;
            }

            // Drawing Board
            PieceColor color;

            for (int row = 0; row < Constants.BoardSize; row++)
            {
                for (int col = 0; col < Constants.BoardSize; col++)
                {
                    color = status.Cells[row, col].Color;
                    bitMap = GetBlockImage(color);

                    // simplificar al implementar command pattern
                    sgSolution[row, col].View = vSolutionColor;
                    sgSolution[row, col].Image = bitMap;
                   
                }
            }
            sgSolution.VerticalScroll.Visible = false;
            sgSolution.HorizontalScroll.Visible = false;
            sgSolution.Invalidate();

        }

    }
}
