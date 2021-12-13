using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Objects;
using BPSolver.Enums;

namespace WFProt
{
    public partial class Form1 : Form
    {
        // Acciones 
        // Deshacer ultimo comando
        public Action Out_Undo { get; set; }

        // Insert Pieza en board
        public Action<Coord, PieceName> Out_PutPiece { get; set; }

        
        // Poner NextPiece en Image
        public Action<int, PieceName> Out_SetNextPiece { get; set; }

        public Action<Coord> Out_DeleteCell { get; set; }

        // Metodos Concretos
        private void Undo()
        {
            Out_Undo();
        }

        private void PutPiece(Coord coord, PieceName name)
        {
            Out_PutPiece(coord, name);
        }

        private void SetNextPiece(int index, PieceName name)
        {
            Out_SetNextPiece(index, name);
        }

        private void DeleteCell(Coord coord)
        {
            Out_DeleteCell(coord);
        }
    }
}
