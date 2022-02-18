using BPSolver;
using BPSolver.Enums;
using BPSolver.Objects;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WFProt
{
    public partial class Form1 : Form
    {
        // Functions
        public delegate bool TestPieceDelegate(Coord insertCoord, PieceName name, Board cells);
        public TestPieceDelegate InOut_TestPiece;

        public delegate PieceColor GetPieceColorDelegate(PieceName name);
        public GetPieceColorDelegate InOut_GetPieceColor;

        public delegate List<Coord> GetRealCoordsDelegate(PieceName name, Coord point);
        public GetRealCoordsDelegate InOut_GetRealCoords;

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
                ret = (bool) InOut_TestPiece?.Invoke(coord, _stMContext.CurrentPiece, _previewCells);

                if (ret)
                {
                    List<Coord> drawList = InOut_GetRealCoords(_stMContext.CurrentPiece, coord);
                    PieceColor color = InOut_GetPieceColor(_stMContext.CurrentPiece);
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
