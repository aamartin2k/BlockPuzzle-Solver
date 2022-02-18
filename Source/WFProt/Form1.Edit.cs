using System;
using System.Windows.Forms;
using BPSolver.Objects;
using BPSolver.Enums;

namespace WFProt
{
    public partial class Form1 : Form
    {
        #region Outputs
        // Actions.
        #region Declaration of Delegates
        // Undo last command.
        public Action Out_Undo { get; set; }
        // Find solution.
        public Action Out_Solution { get; set; }
        // Draw selected piece on board.
        public Action<Coord, PieceName> Out_DrawPiece { get; set; }
        // Draw selected piece on Next piece picture box.
        public Action<int, PieceName> Out_DrawNextPiece { get; set; }
        // Delete grid cell.
        public Action<Coord> Out_DeleteGridCell { get; set; }
        // Delete piece on Next piece picture box.
        public Action<int> Out_DeleteNextPiece { get; set; }
        // Play selected piece.
        public Action<Coord, PieceName, int> Out_DrawGridPlay { get; set; }

        // Browsing.
        public Action Out_MoveFirst { get; set; }
        public Action Out_MovePrevious { get; set; }
        public Action Out_MoveNext { get; set; }
        public Action Out_MoveLast { get; set; }
        public Action<int> Out_MoveToChild { get; set; }
        // Childs
        public Action <int, string> Out_Rename { get; set; }

        #endregion

        #region Invocation of Delegates
        private void OnUndo()
        {
            Out_Undo?.Invoke();
        }

        private void OnSolution()
        {
            Out_Solution?.Invoke();
        }

        private void OnOut_DrawPiece(Coord coord, PieceName name)
        {
            Out_DrawPiece?.Invoke(coord, name);
        }

        private void OnOut_DrawNextPiece(int index, PieceName name)
        {
            Out_DrawNextPiece?.Invoke(index, name);
        }

        private void OnOut_DeleteGridCell(Coord coord)
        {
            Out_DeleteGridCell?.Invoke(coord);
        }

        private void OnOut_DeleteNextPiece(int index)
        {
            Out_DeleteNextPiece?.Invoke(index);
        }

        private void OnOut_DrawGridPlay(Coord coord, PieceName name, int index)
        {
            Out_DrawGridPlay?.Invoke(coord, name, index);
        }

        #endregion

        #endregion
    }
}
