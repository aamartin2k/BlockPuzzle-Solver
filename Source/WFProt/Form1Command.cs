using SourceGrid;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver.Solver;

namespace WFProt
{

    public partial class Form1 : Form
    {

        private void ShowCurrentPiece(PieceName name)
        {
            label2.Text = name.ToString();
            Bitmap SelectedImage = GetImage(name);
            tslbPieceImage.Image = SelectedImage;
        }

        private void ShowCurrentAction(CommandAction action)
        {
            label1.Text = action.ToString();

            Bitmap command;

            switch (action)
            {
                case CommandAction.Select:
                    command = WFProt.Properties.Resources.Arrow;
                    this.Cursor = Cursors.Arrow;
                
                    break;

                case CommandAction.Delete:
                    command = WFProt.Properties.Resources.Delete;
                    this.Cursor = Cursors.Cross;
                    break;

                case CommandAction.Undo:
                    command = WFProt.Properties.Resources.Undo;
                    this.Cursor = Cursors.Arrow;
                    break;

                case CommandAction.Play:
                    command = WFProt.Properties.Resources.Arrow;
                    this.Cursor = Cursors.Hand;
                    break;

                default:
                    command = null;
                    break;
            }

            tlsbActionImage.Image = command;
        }

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

       
        // Return image for block piece by piece color
        private Bitmap GetBlockImage(PieceColor color)
        {
            Bitmap image;

            switch (color)
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


    }


}