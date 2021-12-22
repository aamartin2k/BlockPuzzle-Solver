using System;
using System.Windows.Forms;
using BPSolver.Objects;
using BPSolver.Enums;

namespace WFProt
{
    public partial class Form1 : Form
    {
        // Acciones 
        // Deshacer ultimo comando
        public Action Out_Undo { get; set; }

        // Dibujar Pieza Seleccionada en board
        public Action<Coord, PieceName> Out_DrawPiece { get; set; }

        // Poner Pieza seleccionada en Image NextPiece
        public Action<int, PieceName> Out_DrawNextPiece { get; set; }

        // Borrar celda en Grid StmOut_DeleteGridCell
        public Action<Coord> Out_DeleteGridCell { get; set; }

        // Borrar Pieza  en Image NextPiece
        public Action<int> Out_DeleteNextPiece { get; set; }

        // Poner Pieza seleccionada desde Image NextPiece en Board
        // Jugada 
        public Action<Coord, PieceName, int> Out_DrawGridPlay { get; set; }

        // Control de Secuencia
        // Browsing
        public Action Out_MoveFirst { get; set; }
        public Action Out_MovePrevious { get; set; }
        public Action Out_MoveNext { get; set; }
        public Action Out_MoveLast { get; set; }
        public Action<int> Out_MoveToChild { get; set; }
        // Childs
        public Action<GameStatus> Out_AddChild { get; set; }
        public Action<GameStatus> Out_AddChildStay { get; set; }
        public Action <int, string> Out_Rename { get; set; }
        // 


        // Metodos Concretos
        private void Undo()
        {
            Out_Undo();
        }

       
    }
}
