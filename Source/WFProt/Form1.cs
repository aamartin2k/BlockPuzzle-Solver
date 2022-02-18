using System;
using System.Drawing;
using System.Windows.Forms;
using Cells = SourceGrid.Cells;
using Views = SourceGrid.Cells.Views;
using BPSolver;
using BPSolver.Enums;


namespace WFProt
{
    public partial class Form1 : Form
    {
        // Declarations
        private const string extension = "Modelos | *.bmd";

        public Form1()
        {
            InitializeComponent();

            #region Piece Id


            // Setting action id on buttons tag
            tsbMoveFirst.Tag = SequenceAction.First;
            tsbMovePrevious.Tag = SequenceAction.Previous;
            tsbMoveNext.Tag = SequenceAction.Next;
            tsbMoveLast.Tag = SequenceAction.Last;

            tsbArrow.Tag = CommandAction.Select;
            tsbDelete.Tag = CommandAction.Delete;
            tsbUndo.Tag = CommandAction.Undo;

            tsbOne.Tag = PieceName.One;
            tsbTwoHor.Tag = PieceName.TwoHor;
            tsbTwoVert.Tag = PieceName.TwoVert;
            //
            tsbThreeHor.Tag = PieceName.ThreeHor;
            tsbThreeVert.Tag = PieceName.ThreeVert;
            //
            tsbThreeLOne.Tag = PieceName.ThreeLOne;
            tsbThreeLTwo.Tag = PieceName.ThreeLTwo;
            tsbThreeLThree.Tag = PieceName.ThreeLThree;
            tsbThreeLFour.Tag = PieceName.ThreeLFour;
            //
            tsbFour.Tag = PieceName.Four;
            //
            tsbFourT1.Tag = PieceName.FourT1;
            tsbFourT2.Tag = PieceName.FourT2;
            tsbFourT3.Tag = PieceName.FourT3;
            tsbFourT4.Tag = PieceName.FourT4;
            //
            tsbFourJ1.Tag = PieceName.FourJ1;
            tsbFourJ2.Tag = PieceName.FourJ2;
            tsbFourJ3.Tag = PieceName.FourJ3;
            tsbFourJ4.Tag = PieceName.FourJ4;
            //
            tsbFourL1.Tag = PieceName.FourL1;
            tsbFourL2.Tag = PieceName.FourL2;
            tsbFourL3.Tag = PieceName.FourL3;
            tsbFourL4.Tag = PieceName.FourL4;
            //
            tsbFourS1.Tag = PieceName.FourS1;
            tsbFourS2.Tag = PieceName.FourS2;
            //
            tsbFourZ1.Tag = PieceName.FourZ1;
            tsbFourZ2.Tag = PieceName.FourZ2;
            //
            tsbFourHor.Tag = PieceName.FourHor;
            tsbFourVert.Tag = PieceName.FourVert;
            //
            tsbFiveHor.Tag = PieceName.FiveHor;
            tsbFiveVert.Tag = PieceName.FiveVert;
            //
            tsbFiveLOne.Tag = PieceName.FiveL1;
            tsbFiveLTwo.Tag = PieceName.FiveL2;
            tsbFiveLThree.Tag = PieceName.FiveL3;
            tsbFiveLFour.Tag = PieceName.FiveL4;
            //
            tsbNine.Tag = PieceName.Nine;

            // Wire up event handling methods
            // Action buttons click handler
            tsbMoveFirst.Click += TsbSequence_Click;
            tsbMovePrevious.Click += TsbSequence_Click;
            tsbMoveNext.Click += TsbSequence_Click;
            tsbMoveLast.Click += TsbSequence_Click;


            tsbArrow.Click += TsbArrow_Click;
            //tsbArrow.Click += TsbShowAction_Click;

            tsbDelete.Click += TsbDelete_Click;
            //tsbDelete.Click += TsbShowAction_Click;

            tsbUndo.Click += tsbUndo_Click;
            tsbUndo.Click += TsbShowAction_Click;
            //
            // Piece buttons click handler
            tsbOne.Click += TsbSetPiece_Click;
            //
            tsbTwoHor.Click += TsbSetPiece_Click;
            tsbTwoVert.Click += TsbSetPiece_Click;
            //
            tsbThreeHor.Click += TsbSetPiece_Click;
            tsbThreeVert.Click += TsbSetPiece_Click;
            //
            tsbThreeLOne.Click += TsbSetPiece_Click;
            tsbThreeLTwo.Click += TsbSetPiece_Click;
            tsbThreeLThree.Click += TsbSetPiece_Click;
            tsbThreeLFour.Click += TsbSetPiece_Click;
            //
            tsbFour.Click += TsbSetPiece_Click;
            //
            tsbFourT1.Click += TsbSetPiece_Click;
            tsbFourT2.Click += TsbSetPiece_Click;
            tsbFourT3.Click += TsbSetPiece_Click;
            tsbFourT4.Click += TsbSetPiece_Click;
            //
            tsbFourJ1.Click += TsbSetPiece_Click;
            tsbFourJ2.Click += TsbSetPiece_Click;
            tsbFourJ3.Click += TsbSetPiece_Click;
            tsbFourJ4.Click += TsbSetPiece_Click;
            //
            tsbFourL1.Click += TsbSetPiece_Click;
            tsbFourL2.Click += TsbSetPiece_Click;
            tsbFourL3.Click += TsbSetPiece_Click;
            tsbFourL4.Click += TsbSetPiece_Click;
            //
            tsbFourS1.Click += TsbSetPiece_Click;
            tsbFourS2.Click += TsbSetPiece_Click;
            //
            tsbFourZ1.Click += TsbSetPiece_Click;
            tsbFourZ2.Click += TsbSetPiece_Click;
            //
            tsbFourHor.Click += TsbSetPiece_Click;
            tsbFourVert.Click += TsbSetPiece_Click;
            //
            tsbFiveHor.Click += TsbSetPiece_Click;
            tsbFiveVert.Click += TsbSetPiece_Click;
            //
            tsbFiveLOne.Click += TsbSetPiece_Click;
            tsbFiveLTwo.Click += TsbSetPiece_Click;
            tsbFiveLThree.Click += TsbSetPiece_Click;
            tsbFiveLFour.Click += TsbSetPiece_Click;
            //
            tsbNine.Click += TsbSetPiece_Click;

            // Next Piece image
            pbNextPiece1.MouseDown += PbNextPiece_Click;
            pbNextPiece2.MouseDown += PbNextPiece_Click;
            pbNextPiece3.MouseDown += PbNextPiece_Click;

            pbNextPiece1.Tag = PieceName.None;
            pbNextPiece2.Tag = PieceName.None;
            pbNextPiece3.Tag = PieceName.None;
            #endregion

            #region Grid Juego

            //  Grid Views
            vBackColor = new Views.Cell();
            vBackColor.BackColor = Color.DarkSlateBlue;

            vTransColor = new Views.Cell();
            vTransColor.BackColor = Color.FromArgb(75, Color.DarkSlateBlue); ;
             

            // Grid Config
            int dim = (sgBoard.Width / 10);


            sgBoard.Selection.EnableMultiSelection = false;
            sgBoard.BorderStyle = BorderStyle.None;
            
            sgBoard.Redim(Constants.BoardSize, Constants.BoardSize);

            // Grid Controller
            // Local handler passed as argument on the constructor
            ClickController cellController = new ClickController(ClickOnCell, MouseEnterCell, MouseLeaveCell);
            Cells.Cell xCell;

            for (int row = 0; row < Constants.BoardSize; row++)
            {
                for (int col = 0; col < Constants.BoardSize; col++)
                {
                    xCell = new Cells.Cell();
                    xCell.View = vBackColor;

                    sgBoard[row, col] = xCell;
                    sgBoard[row, col].AddController(cellController);
                }

                sgBoard.Columns[row].AutoSizeMode = SourceGrid.AutoSizeMode.None;
                sgBoard.Columns[row].Width = dim;

                sgBoard.Rows[row].AutoSizeMode = SourceGrid.AutoSizeMode.None;
                sgBoard.Rows[row].Height = dim;
            }

            sgBoard.AutoStretchColumnsToFitWidth = false;
            sgBoard.AutoStretchRowsToFitHeight = false;
            sgBoard.VerticalScroll.Visible = false;
            sgBoard.HorizontalScroll.Visible = false;

            sgBoard.Invalidate();

           
            #endregion

            #region Grid Soluciones
            //  Grid Views // sgSolution
            vSolutionColor = new Views.Cell();
            vSolutionColor.BackColor = Color.DarkGreen;

            // Grid Config
            dim = (sgSolution.Width / 10);
            sgSolution.BorderStyle = BorderStyle.None;
            sgSolution.Redim(Constants.BoardSize, Constants.BoardSize);

            for (int row = 0; row < Constants.BoardSize; row++)
            {
                for (int col = 0; col < Constants.BoardSize; col++)
                {
                    xCell = new Cells.Cell();
                    xCell.View = vSolutionColor;

                    sgSolution[row, col] = xCell;
                    sgSolution[row, col].AddController(cellController);
                }

                sgSolution.Columns[row].AutoSizeMode = SourceGrid.AutoSizeMode.None;
                sgSolution.Columns[row].Width = dim;

                sgSolution.Rows[row].AutoSizeMode = SourceGrid.AutoSizeMode.None;
                sgSolution.Rows[row].Height = dim;
            }
            sgSolution.AutoStretchColumnsToFitWidth = false;
            sgSolution.AutoStretchRowsToFitHeight = false;
            sgSolution.VerticalScroll.Visible = false;
            sgSolution.HorizontalScroll.Visible = false;

            sgSolution.Invalidate();


            #endregion

            // TreeView
            cTreeStatus.DrawStyle = ControlTreeView.CTreeViewDrawStyle.HorizontalDiagram;
            //cTreeStatus.DrawStyle = ControlTreeView.CTreeViewDrawStyle.LinearTree;
            //cTreeStatus.DrawStyle = ControlTreeView.CTreeViewDrawStyle.VerticalDiagram;
            cTreeStatus.SelectionMode = ControlTreeView.CTreeViewSelectionMode.Single;

            //establecer handler para actualizar default item en ToolStripButtons
            tssbTwo.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbThreeLine.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbThreeL.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbT.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbFourJ.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbFourL.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbFourS.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbFourZ.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbFourLine.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbFiveLine.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);
            tssbFiveL.DropDownItemClicked += new ToolStripItemClickedEventHandler(this.TssbDefault_DropDownItemClicked);

            // ejecutando handler para poner como default el primer elemento 
            TssbFirstDefault(tssbTwo);
            TssbFirstDefault(tssbThreeLine);
            TssbFirstDefault(tssbThreeL);
            TssbFirstDefault(tssbT);
            TssbFirstDefault(tssbFourJ);
            TssbFirstDefault(tssbFourL);
            TssbFirstDefault(tssbFourS);
            TssbFirstDefault(tssbFourZ);
            TssbFirstDefault(tssbFourLine);
            TssbFirstDefault(tssbFiveLine);
            TssbFirstDefault(tssbFiveL);

            newToolStripButton.Click += newToolStripButton_Click;
            saveToolStripButton.Click += saveToolStripButton_Click;
            openToolStripButton.Click += openToolStripButton_Click;
            closeToolStripButton.Click += closeToolStripButton_Click;
            // deshabilitar controles
            UserEnable = false;

            // creando FSM
            CreateStateMachine();
        }

        private bool UserEnable
        {
            set
            {
                toolStripControls.Enabled = value;
                toolStripTask.Enabled = value;
                sgBoard.Enabled = value;
                sgSolution.Enabled = value;
            }
        }

        private bool MissingNextPieces()
        {
            bool ret = false;

            // Check if all three next pieces are present.
            ret = ret | (PieceName.None == (PieceName)pbNextPiece1.Tag);
            ret = ret | (PieceName.None == (PieceName)pbNextPiece2.Tag);
            ret = ret | (PieceName.None == (PieceName)pbNextPiece3.Tag);

            return ret;
        }
       
        private void TsbShowAction_Click(object sender, EventArgs e)
        {
            // Set selected piece
            // Casting sender back to button
            ToolStripItem itm = (ToolStripItem)sender;

            // new impl
            ShowCurrentAction((CommandAction)itm.Tag);
        }

        private void tsbUndo_Click(object sender, EventArgs e)
        {
            OnUndo();
        }

        private void tsbSoluciones_Click(object sender, EventArgs e)
        {
            // All three next pieces must be present to make find solutions.
            bool ret = MissingNextPieces();

            if (ret)
            {
                string text = "All three next pieces must be present to find solutions";

                MessageBox.Show(this, text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                OnSolution();
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
             SaveFile();
        }
        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = extension;
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                tlsbModelText.Text = saveFileDialog1.FileName;
                tlsbActionImage.Image = saveToolStripButton.Image;
                SaveFileAs(saveFileDialog1.FileName);

            }

        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = extension;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                tlsbModelText.Text = openFileDialog1.FileName;
                tlsbActionImage.Image = openToolStripButton.Image;
                LoadFile(openFileDialog1.FileName);
            }

        }

        //closeToolStripButton_Click
        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void lvSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lvMoves.Items.Clear();

            //Solution sol = (Solution) lvSolutions.SelectedItem;

            //foreach (var move in sol.Moves)
            //{
            //    lvMoves.Items.Add(move);
            //}

        }
        private void TssbFirstDefault(ToolStripSplitButton parent)
        {
            ToolStripItem item = parent.DropDownItems[0];
            var eh = new ToolStripItemClickedEventArgs(item);

            TssbDefault_DropDownItemClicked(parent, eh);
        }

        private void TssbDefault_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            ToolStripSplitButton tsb;
            tsb = (ToolStripSplitButton)sender;

            tsb.DefaultItem = e.ClickedItem;
            tsb.Text= e.ClickedItem.Text;
            tsb.Image = e.ClickedItem.Image;

            
        }

       
    }
}
