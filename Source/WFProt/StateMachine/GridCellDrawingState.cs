﻿using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class GridCellDrawingState : BaseState, IGuiState
    {
        public Action<Coord, PieceName> Out_DrawGrid;
       
        public GridCellDrawingState(StContext context, CommandAction action) : base(context, action)
        {
            //Console.WriteLine("GridCellDrawingState created");
        }


        public override void GridCellClicked(Coord position)
        {
            //base.GridCellClicked(position);

            Out_DrawGrid(position, Context.CurrentPiece);
        }

        public override void PieceButtonClicked(PieceName piece)
        {
            // State changes
            Context.CurrentState = Context.PieceSettingState;
            Context.CurrentState.PieceButtonClicked(piece);

        }

        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            //base.NextPieceImageClicked(index, piece);

            // Temp State Switch
            //var xState = Context.CurrentState;
            //Context.CurrentState = Context.NextPieceDrawingState;
            //Context.CurrentState.NextPieceImageClicked(index, piece);
            //Context.CurrentState = xState;
        }
    }
}
