using BPSolver;
using BPSolver.Enums;
using BPSolver.Objects;
using SourceGrid;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WFProt
{
    public partial class Form1 : Form
    {
        private Board _previewCells;
        private List<Coord> _drawList;

        private void MouseEnterGameCell(Position pos)
        {
            Coord coord = new Coord(pos.Row, pos.Column);
            _stMContext.MouseEnterGameCell(coord);
        }

        private void MouseLeaveGameCell(Position pos)
        {
            _stMContext.MouseLeaveGameCell();
        }

        private void StmOut_DrawPreview(Coord coord)
        {
            
            bool ret = _previewCells[coord].IsFree;

            if (ret)
            {
                ret = Utils.TestPiece(coord, _stMContext.CurrentPiece, _previewCells);

                if (ret)
                {
                   
                    Piece instance = PieceSet.GetPiece(_stMContext.CurrentPiece);
                    List<Coord> drawList = Piece.GetRealCoords(instance, coord);

                    PieceColor color = instance.Color;
                    Bitmap bitmap = GetBlockImage(color);

                    foreach (var item in drawList)
                    {
                        sgBoard[item.Row, item.Col].View = vTransColor;
                        sgBoard[item.Row, item.Col].Image = bitmap;
                        _previewCells[item].Color = color;
                    }

                    sgBoard.Invalidate();
                    _drawList = drawList;

                }
            }
        }

        private void StmOut_DeletePreview()
        {
            
            if (_drawList != null)
            {
                foreach (var item in _drawList)
                {
                    sgBoard[item.Row, item.Col].View = vBackColor;
                    sgBoard[item.Row, item.Col].Image = null;
                    _previewCells[item].Color = PieceColor.None;
                }
                sgBoard.Invalidate();
                _drawList = null;
            }
            
       
                
        }
    }
}
