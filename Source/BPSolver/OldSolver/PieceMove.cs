using BPSolver.Objects;


namespace BPSolver.Solver
{
    public class Move
    {
        // Constructor con parametros
        public Move(int row, int col, Piece piece)
        {
            Row = row;
            Col = col;
            Piece = piece;
        }

        // Pieza y ubicacion de la movida
        public Piece Piece { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        // Valor (Propiedades) de la movida
        public int Value
        {
            get
            {
                return PieceSize + Preference + Neighbors + (CompleteRoC ? 60 : 0) ;
            }

        }

        public bool CompleteRoC;
        public int Neighbors;
        public int Preference;

        public int PieceSize { get { return Piece.Count;  }  }

        public override string ToString()
        {
            return string.Format("Piece: {0} to Row: {1}  Col: {2} Value: {3} Preference: {4} Neighbors: {5} Complete: {6}", Piece.Name, Row, Col, Value, Preference, Neighbors,  CompleteRoC);
        }
    }
}
