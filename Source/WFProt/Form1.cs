﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SourceGrid;
using Cells = SourceGrid.Cells;
using Views = SourceGrid.Cells.Views;
using BPSolver.Solver;
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


            // Setting piece id on buttons tag
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
            tsbFiveLOne.Tag = PieceName.FiveLOne;
            tsbFiveLTwo.Tag = PieceName.FiveLTwo;
            tsbFiveLThree.Tag = PieceName.FiveLThree;
            tsbFiveLFour.Tag = PieceName.FiveLFour;
            //
            tsbNine.Tag = PieceName.Nine;

            // Wire up event handling methods
            // Action buttons click handler

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

            #region Grid

            //  Grid Views
            vBackColor = new Views.Cell();
            vBackColor.BackColor = Color.DarkSlateBlue;

            vSelectColor = new Views.Cell();
            vSelectColor.BackColor = Color.AntiqueWhite;

            //vColorTwo = new Views.Cell();
            //vColorTwo.BackColor = ColorAdapter(PieceColor.Two);
            //vColorThree = new Views.Cell();
            //vColorThree.BackColor = ColorAdapter(PieceColor.Three);
            //vColorFour = new Views.Cell();
            //vColorFour.BackColor = ColorAdapter(PieceColor.Four);

            //vColorFive = new Views.Cell();
            //vColorFive.BackColor = ColorAdapter(PieceColor.Five);
            //vColorSix = new Views.Cell();
            //vColorSix.BackColor = ColorAdapter(PieceColor.Six);
            //vColorSeven = new Views.Cell();
            //vColorSeven.BackColor = ColorAdapter(PieceColor.Seven);

            //vColorEight = new Views.Cell();
            //vColorEight.BackColor = ColorAdapter(PieceColor.Eight);
            //vColorNine = new Views.Cell();
            //vColorNine.BackColor = ColorAdapter(PieceColor.Nine);

            // Grid Config
            int dim = (sgBoard.Width / 10);

            sgBoard.BorderStyle = BorderStyle.None;
            sgBoard.Redim(Constants.Rank, Constants.Rank);

            // Grid Controller
            // Local handler passed as argument on the constructor
            ClickController cCont = new ClickController(ClickOnCell, MouseEnterCell);
            Cells.Cell xCell;

            for (int row = 0; row < Constants.Rank; row++)
            {
                for (int col = 0; col < Constants.Rank; col++)
                {
                    xCell = new Cells.Cell();
                    xCell.View = vBackColor;

                    sgBoard[row, col] = xCell;
                    sgBoard[row, col].AddController(cCont);
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

            // hook event handler

            #endregion

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
                toolStripTask.Enabled = value;
                sgBoard.Enabled = value;
            }
        }
        private void Gs_AnalysisReady(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
             MessageBox.Show("Analysis Ready");
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            
            //lbFree.Text = gGStrat.Stats.FreeCells.ToString();
            //lbOcupp.Text = gGStrat.Stats.OccupiedCells.ToString();
            //lbCount.Text = gGStrat.Stats.CellsCount.ToString();
            //lbColComp.Text = gGStrat.Stats.CompletedColumns.ToString();
            //lbRowComp.Text = gGStrat.Stats.CompletedRows.ToString();

            lvSolutions.Items.Clear();
               
            //foreach (var item in gGStrat.Solutions.OrderByDescending(s => s.CompleteRoC).ThenByDescending(s => s.Value).ThenByDescending(s => s.Preference)    )
            //{
            //    lvSolutions.Items.Add(item);
            //}

            //lbMoves.Text = gGStrat.Solutions.Count.ToString();
        }

        // private event handlers
        private void button1_Click(object sender, EventArgs e)
        {
            AnalizeGame();
        }

       
        private void TsbShowAction_Click(object sender, EventArgs e)
        {
            // Set selected piece
            // Casting sender back to button
            ToolStripItem itm = (ToolStripItem)sender;

            // new impl
            ShowCurrentAction((CommandAction)itm.Tag);
        }

       

        // private methods

        //private Color ColorAdapter(PieceColor color)
        //{
        //    Color result;

        //    switch (color)
        //    {
        //        case PieceColor.None:
        //            result = Color.DarkSlateBlue;
        //            break;
        //        case PieceColor.One:
        //            result = Color.Green;
        //            break;
        //        case PieceColor.Two:
        //            result = Color.AliceBlue;
        //            break;
        //        case PieceColor.Three:
        //            result = Color.DarkRed;
        //            break;
        //        case PieceColor.Four:
        //            result = Color.LightBlue;
        //            break;
        //        case PieceColor.Five:
        //            result = Color.Orange;
        //            break;
        //        case PieceColor.Six:
        //            result = Color.PaleVioletRed;
        //            break;
        //        case PieceColor.Seven:
        //            result = Color.LightGoldenrodYellow;
        //            break;
        //        case PieceColor.Eight:
        //            result = Color.LawnGreen;
        //            break;
        //        case PieceColor.Nine:
        //            result = Color.SteelBlue;
        //            break;
        //        default:
        //            result = Color.DarkSlateBlue;
        //            break;
        //    }
        //    return result;
        //}

      
        private void tsbUndo_Click(object sender, EventArgs e)
        {
            // ExecuteCommandUndo();
            Undo();
        }

        //newToolStripButton_Click
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            
            saveFileDialog1.Filter = extension;
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                tlsbModelText.Text = saveFileDialog1.FileName;
                tlsbActionImage.Image = saveToolStripButton.Image;
                SaveFile(saveFileDialog1.FileName);
                label1.Text = "Salvar";
                tlsbActionImage.Image = saveToolStripButton.Image;
                
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
                label1.Text = "Abrir";
               
            }

        }

        //closeToolStripButton_Click
        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void lvSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvMoves.Items.Clear();

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
