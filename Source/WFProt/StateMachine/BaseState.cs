﻿using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class BaseState : IGuiState
    {
        protected StContext Context;

        public CommandAction Action { get; private set;  }

        public BaseState(StContext context, CommandAction action)
        {
            Action = action;
            Context = context;
            Console.WriteLine("BaseState created, protected Context stored.");
        }

        
        // Non Virtual Members (Overridables)
        public void ActionDeleteClicked()
        {
            Context.CurrentState = Context.DeletionState;
        }

        public void ActionSelectClicked()
        {
            Context.CurrentState = Context.SelectionState;
        }

       
       
        // Virtual Members
        public virtual void PieceButtonClicked(PieceName piece)
        {
            Console.WriteLine(string.Format("PieceButton Clicked: {0}", piece));
        }

        public virtual void NextPieceImageClicked(int index, PieceName piece = PieceName.None)
        {
            Console.WriteLine(string.Format("NextPieceImage index {0} Clicked. Piece Arg: {1} Current Piece: {2}", index, piece, Context.CurrentPiece));
        }

        public virtual void GridCellClicked(Coord position)
        {
            //Console.WriteLine(string.Format("Grid Cell R: {0} C: {1} Clicked.", position.Row, position.Col));
            //Context.NextPieceIndex
            Console.WriteLine(string.Format("Play: Piece: {0} on NPImage {1} move to Cell R: {2} C: {3}", Context.CurrentPiece, Context.NextPieceIndex, position.Row, position.Col));
        }

    }


}

