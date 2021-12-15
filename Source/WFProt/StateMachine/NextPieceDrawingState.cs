﻿using System;
using BPSolver.Enums;
using BPSolver.Objects;


namespace WFProt
{
    class NextPieceDrawingState : BaseState, IGuiState
    {

        public Action<int, PieceName> Out_DrawNextPiece;

        public NextPieceDrawingState(StContext context, CommandAction action) : base(context, action)
        {
            Console.WriteLine("NextPieceDrawingState created");
        }

        public override void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            base.NextPieceImageClicked(index, piece);

            Out_DrawNextPiece(index, Context.CurrentPiece); 
        }

        public override void PieceButtonClicked(PieceName piece)
        {
            base.PieceButtonClicked(piece);

            // Set Current Piece
            Context.CurrentPiece = piece;
        }

        public override void GridCellClicked(Coord position)
        {
            Context.CurrentState = Context.GridCellDrawingState;
            Context.CurrentState.GridCellClicked(position);

        }
    }

      
}
