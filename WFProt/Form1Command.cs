using SourceGrid;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver.Solver;
using SourceGrid.Selection;

namespace WFProt
{

    public partial class Form1 : Form
    {

        interface ICommand
        {
            void Do();
            void Undo();
        }

        // Enums
        enum CommandAction
        {  None,  Undo, Delete  }

        // Declarations
        private PieceName SelectedPieceName;
        private CommandAction SelectedAction;
        private Bitmap SelectedImage;
        private Bitmap SelectedBlock;
        

        private Stack<ICommand> CommandStack = new Stack<ICommand>();

        // Helper methods

        private void HelpDrawPieceOnBoard(CellContext sender)
        {
            bool ret;
           
            GameSimpleStatus gst = CreateGameStatus();

            ret = gGStrat.TestPiece(sender.Position.Row, sender.Position.Column, SelectedPieceName, ref gst);

            if (ret)
            {
                DrawBlockOnBoard command;
                CompositeDrawBlockOnBoard draw ;

                // If rows or columns are completed, notify before delete
                if (gst.AnyCompleted)
                {
                    // Make selection or animation
                   // Sequence: Draw piece, draw white block on  complete RoC, draw final board (with deleted completed)
                }

                // Draw all cells with undoable command
                // Creating composite command
                draw = new CompositeDrawBlockOnBoard();

                for (int row = 0; row < GameSolver.Rank; row++)
                {
                    for (int col = 0; col < GameSolver.Rank; col++)
                    {
                        command = new DrawBlockOnBoard(sgBoard,
                                                       GetBlockImage(gst[row, col].Color),
                                                       new Coord(row, col),
                                                       gst[row, col].Color);

                        draw.Add(command);
                    }
                }
                // executing with Undo enqueing
                ExecuteCommandDo(draw);

                // Actualizar stats
                //UpdateLabels();
            }
            /*
            gGStrat.SetGameStatus(CreateGameStatus(), true);

            //Coord gbase = new Coord(sender.Position.Row, sender.Position.Column);
            ////   Comprobar si las coordenadas reales de la pieza estan dentro del board
            List<Coord> matrix = new List<Coord>();
          
            bool ret = gGStrat.TryOnePieceOutRealCoord(sender.Position.Row, sender.Position.Column, SelectedPiece, out matrix);
            if (!ret)
                return;

           

            */
        }

        // Set current piece
        private void SetCurrentPiece(PieceName name)
        {
            SelectedPieceName = name;
            //SelectedPiece = GetPiece(SelectedPieceName);
            label2.Text = name.ToString();

            SelectedImage = GetImage(name);
            tslbPieceImage.Image = SelectedImage;

            SelectedBlock = GetBlockImage(name);
            label3.Text = (SelectedBlock != null) ? SelectedBlock.ToString() : string.Empty;
        }

        private void SetCurrentAction(CommandAction action)
        {
            SelectedAction = action;
            label1.Text = SelectedAction.ToString();

            SelectedPieceName = PieceName.None;

            Bitmap command;

            switch (action)
            {
                case CommandAction.None:
                    command = WFProt.Properties.Resources.Arrow;
                    break;
                case CommandAction.Delete:
                    command = WFProt.Properties.Resources.Delete;
                    break;
                case CommandAction.Undo:
                    command = WFProt.Properties.Resources.Undo;
                    break;
                default:
                    command = null;
                    break;
            }

            tlsbActionImage.Image = command;


        }


        // Return Piece by name
        //private Piece GetPiece(PieceName name)
        //{
        //    return  gGStrat.GetPiece(name);
        //}

        // Return image for piece by name
        private Bitmap GetImage(PieceName name)
        {
            // get selected bitmap
            Bitmap bmp;

            switch (name)
            {
                case PieceName.One:
                    bmp = WFProt.Properties.Resources.OneO;
                    break;
                case PieceName.TwoHor:
                    bmp = WFProt.Properties.Resources.TwoHor;
                    break;
                case PieceName.TwoVert:
                    bmp = WFProt.Properties.Resources.TwoVert;
                    break;
                case PieceName.ThreeHor:
                    bmp = WFProt.Properties.Resources.ThreeHor;
                    break;
                case PieceName.ThreeVert:
                    bmp = WFProt.Properties.Resources.ThreeVert;
                    break;
                case PieceName.ThreeLOne:
                    bmp = WFProt.Properties.Resources.ThreeLOne;
                    break;
                case PieceName.ThreeLTwo:
                    bmp = WFProt.Properties.Resources.ThreeLTwo;
                    break;
                case PieceName.ThreeLThree:
                    bmp = WFProt.Properties.Resources.ThreeLThree;
                    break;
                case PieceName.ThreeLFour:
                    bmp = WFProt.Properties.Resources.ThreeLFour;
                    break;
                case PieceName.Four:
                    bmp = WFProt.Properties.Resources.FourO;
                    break;
               
                    //
                case PieceName.FourT1:
                    bmp = WFProt.Properties.Resources.FourT1;
                    break;
                case PieceName.FourT2:
                    bmp = WFProt.Properties.Resources.FourT2;
                    break;
                case PieceName.FourT3:
                    bmp = WFProt.Properties.Resources.FourT3;
                    break;
                case PieceName.FourT4:
                    bmp = WFProt.Properties.Resources.FourT4;
                    break;
                //
                case PieceName.FourJ1:
                    bmp = WFProt.Properties.Resources.FourJ1;
                    break;
                case PieceName.FourJ2:
                    bmp = WFProt.Properties.Resources.FourJ2;
                    break;
                case PieceName.FourJ3:
                    bmp = WFProt.Properties.Resources.FourJ3;
                    break;
                case PieceName.FourJ4:
                    bmp = WFProt.Properties.Resources.FourJ4;
                    break;
                //
                case PieceName.FourL1:
                    bmp = WFProt.Properties.Resources.FourL1;
                    break;
                case PieceName.FourL2:
                    bmp = WFProt.Properties.Resources.FourL2;
                    break;
                case PieceName.FourL3:
                    bmp = WFProt.Properties.Resources.FourL3;
                    break;
                case PieceName.FourL4:
                    bmp = WFProt.Properties.Resources.FourL4;
                    break;
                //
                case PieceName.FourS1:
                    bmp = WFProt.Properties.Resources.FourS1;
                    break;
                case PieceName.FourS2:
                    bmp = WFProt.Properties.Resources.FourS2;
                    break;
                //
                case PieceName.FourZ1:
                    bmp = WFProt.Properties.Resources.FourZ1;
                    break;
                case PieceName.FourZ2:
                    bmp = WFProt.Properties.Resources.FourZ2;
                    break;
                //
                case PieceName.FourHor:
                    bmp = WFProt.Properties.Resources.FourHor;
                    break;
                case PieceName.FourVert:
                    bmp = WFProt.Properties.Resources.FourVert;
                    break;
                case PieceName.FiveHor:
                    bmp = WFProt.Properties.Resources.FiveHor;
                    break;
                case PieceName.FiveVert:
                    bmp = WFProt.Properties.Resources.FiveVert;
                    break;
                case PieceName.FiveLOne:
                    bmp = WFProt.Properties.Resources.FiveLOne;
                    break;
                case PieceName.FiveLTwo:
                    bmp = WFProt.Properties.Resources.FiveLTwo;
                    break;
                case PieceName.FiveLThree:
                    bmp = WFProt.Properties.Resources.FiveLThree;
                    break;
                case PieceName.FiveLFour:
                    bmp = WFProt.Properties.Resources.FiveLFour;
                    break;
                case PieceName.Nine:
                    bmp = WFProt.Properties.Resources.NineO;
                    break;

                default:
                    bmp = null;
                    break;
            }
            return bmp;
        }

        // Return image for block piece by name
        private Bitmap GetBlockImage(PieceName name)
        {
            // get selected bitmap
            Bitmap bmp;

            switch (name)
            {
                case PieceName.One:
                    bmp = WFProt.Properties.Resources.BlOne;
                    break;
                case PieceName.TwoHor:
                case PieceName.TwoVert:
                    bmp = WFProt.Properties.Resources.BlTwo;
                    break;
                
                case PieceName.ThreeHor:
                case PieceName.ThreeVert:
                    bmp = WFProt.Properties.Resources.BlThree;
                    break;
           
                case PieceName.ThreeLOne:
                case PieceName.ThreeLTwo:
                case PieceName.ThreeLThree:
                case PieceName.ThreeLFour:
                    bmp = WFProt.Properties.Resources.BlFour;
                    break;
                
                case PieceName.Four:
                case PieceName.FourL1:
                case PieceName.FourL2:
                case PieceName.FourL3:
                case PieceName.FourL4:
                case PieceName.FourJ1:
                case PieceName.FourJ2:
                case PieceName.FourJ3:
                case PieceName.FourJ4:
                    bmp = WFProt.Properties.Resources.BlFive;
                    break;

                case PieceName.FourT1:
                case PieceName.FourT2:
                case PieceName.FourT3:
                case PieceName.FourT4:
                    bmp = WFProt.Properties.Resources.BlEight;
                    break;

                case PieceName.FourHor:
                case PieceName.FourVert:
                    bmp = WFProt.Properties.Resources.BlSix;
                    break;

                case PieceName.FiveHor:
                case PieceName.FiveVert:
                    bmp = WFProt.Properties.Resources.BlSeven;
                    break;
                
                case PieceName.FiveLOne:
                case PieceName.FiveLTwo:
                case PieceName.FiveLThree:
                case PieceName.FiveLFour:
                    bmp = WFProt.Properties.Resources.BlEight;
                    break;

                case PieceName.FourS1:
                case PieceName.FourS2:
                case PieceName.FourZ1:
                case PieceName.FourZ2:
                case PieceName.Nine:
                    bmp = WFProt.Properties.Resources.BlNine;
                    break;

                default:
                    bmp = null;
                    break;
            }
            return bmp;
        }

        // Return image for block piece by color
        private Bitmap GetBlockImage(PieceColor name)
        {
            Bitmap image;

            switch (name)
            {
                case PieceColor.None:
                    image = null;
                    break;
                case PieceColor.One:
                    image =  WFProt.Properties.Resources.BlOne;
                    break;
                case PieceColor.Two:
                    image = WFProt.Properties.Resources.BlTwo;
                    break;
                case PieceColor.Three:
                    image = WFProt.Properties.Resources.BlThree;
                    break;
                case PieceColor.Four:
                    image = WFProt.Properties.Resources.BlFour;
                    break;
                case PieceColor.Five:
                    image = WFProt.Properties.Resources.BlFive;
                    break;
                case PieceColor.Six:
                    image = WFProt.Properties.Resources.BlSix;
                    break;
                case PieceColor.Seven:
                    image = WFProt.Properties.Resources.BlSeven;
                    break;
                case PieceColor.Eight:
                    image = WFProt.Properties.Resources.BlEight;
                    break;
                case PieceColor.Nine:
                    image = WFProt.Properties.Resources.BlNine;
                    break;
                default:
                    image = null;
                    break;
            }
            return image;

        }


        // Command Execution and Storage
        private void ExecuteCommandDo(ICommand command)
        {
            CommandStack.Push(command);
            command.Do();
            tsbUndo.Enabled = true;
        }

        private void ExecuteCommandUndo()
        {
            if (CommandStack.Count == 0)
            {
                tsbUndo.Enabled = false;
                return;
            }
                
            ICommand command = CommandStack.Pop();
            command.Undo();
        }

        // Concrete commands
        // Draw current Piece on NextPiece image frame
        // Undo Draw current Piece on NextPiece image frame
        private class DrawNextPiece : ICommand
        {
            PictureBox Canvas;
            Image OldBmp, NewBMP;
            PieceName OldName, NewName;

            public DrawNextPiece(PictureBox canvas, Bitmap bmp, PieceName name)
            {
                Canvas = canvas;
                NewBMP = bmp;
                NewName = name;
            }

            public void Do()
            {
                OldBmp = Canvas.Image;
                OldName = (Canvas.Tag != null) ? (PieceName)Canvas.Tag : PieceName.None;

                Canvas.Image = NewBMP;
                Canvas.Tag = NewName;
            }

            public void Undo()
            {
                Canvas.Image = OldBmp;
                Canvas.Tag = OldName;
            }
        }

        // Draw Piece on Board
        // Undo Draw Piece on Board
        // Se implementa un comando para dibujar un unico bloque. Se emplea directamente 
        //  al cargar modelo desde archivo y al dibujar modelo propuesto por analizador
        // Para dibujar una pieza de uno o varios bloques se emplea un comando compuesto
        //  implementado en la clase CompositeDrawBlockOnBoard

        private class DrawBlockOnBoard : ICommand
        {
            // Declarations
            SourceGrid.Grid Grid;
            Coord Coord;

            Image Block, OldBlock;
            PieceColor Color, OldColor;

            // Constructor
            public DrawBlockOnBoard(SourceGrid.Grid board, Bitmap bmp, Coord coord, PieceColor color)
            {
                Grid = board;
                Block = bmp;
                Coord = coord;
                Color = color;
            }

            public void Do()
            {
                // Backup
                OldBlock = Grid[Coord.Row, Coord.Col].Image;
                OldColor = (PieceColor)Grid[Coord.Row, Coord.Col].Tag;

                // Do
                Grid[Coord.Row, Coord.Col].Image = Block;
                Grid[Coord.Row, Coord.Col].Tag = Color;
               
               Grid.Invalidate();
            }

            public void Undo()
            {
                Grid[Coord.Row, Coord.Col].Image = OldBlock;
                Grid[Coord.Row, Coord.Col].Tag = OldColor;

                Grid.Invalidate();
            }
        }

        private class CompositeDrawBlockOnBoard : List<DrawBlockOnBoard>, ICommand
        {
            public void Do()
            {
                ForEach(cmd => cmd.Do());
            }

            public  void Undo()
            {
                ForEach(cmd => cmd.Undo());
            }
        }


    }


}