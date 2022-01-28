using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class BaseState : IGuiState
    {
        protected StMachContext Context;

        public CommandAction Action { get; private set;  }

        public BaseState(StMachContext context, CommandAction action)
        {
            Action = action;
            Context = context;
            //Console.WriteLine("BaseState created, protected Context stored.");
        }

        
        // Non Virtual Members 
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
            Console.WriteLine(string.Format("Play: Piece: {0} on NPImage {1} move to Cell R: {2} C: {3}", Context.CurrentPiece, Context.NextPieceIndex, position.Row, position.Col));
        }

        public virtual void MouseEnterGameCell(Coord position)
        {
            Console.WriteLine(string.Format("Mouse Enter Game Cell. Row: {0} Column: {1}", position.Row, position.Col));
        }

        public virtual void MouseLeaveGameCell(Coord position)
        {
            Console.WriteLine(string.Format("Mouse Leave Game Cell. Row: {0} Column: {1}", position.Row, position.Col));
        }
    }


}

