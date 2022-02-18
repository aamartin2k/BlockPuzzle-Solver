using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;
using System.Diagnostics;

namespace BPSolver.Objects
{
    class PieceSet
    {
        
        private Dictionary<PieceName, Piece> _pieceSet;


        public void Create()
        {
            CreatePieceSet();

            /*
            // Test impresion de listas de coordenadas
            // test

            Debug.AutoFlush = true;
            Debug.Indent();
            Debug.WriteLine("Entering PieceSet");
            // Imprimiendo lista de coord de celdas vecinas
            foreach (var vkey in _pieceSet)
            {
                Piece piece = vkey.Value;
                Debug.WriteLine("Name: " + piece.Name);
                Debug.WriteLine("Neighbors Matrix: " );

                foreach (var coord in piece.NeighborsMatrix)
                {
                    Debug.Write(string.Format(" ({0}, {1})", coord.Row, coord.Col));
                }

                Debug.WriteLine("");
            }

            Debug.WriteLine("Exiting PieceSet");
            Debug.Unindent();
            // Test impresion de listas de coordenadas
            */
        }

        public Piece GetPiece(PieceName name)
        {
            return _pieceSet[name];
        }


        // Possible methods Load & Save, to store/retieve pieceset from/to disk

        private void CreatePieceSet()
        {
            Piece piece;

            _pieceSet = new Dictionary<PieceName, Piece>();

            //One
            piece = new Piece("One", PieceColor.One, PieceAttitude.None);
            // relative coord list, must match Count
            // (0, 0)
            piece.Matrix = new List<Coord>() { new Coord(0, 0) };
            // Storing Piece 
            _pieceSet.Add(PieceName.One, piece);

              
            // TwoHor
            piece = new Piece("TwoHor", PieceColor.Two, PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0, 0) (0, 1)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(0, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.TwoHor, piece);

            
            // TwoVert
            piece = new Piece("TwoVert", PieceColor.Two, PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0, 0) (1, 0)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(1, 0) };
            // Storing Piece 
            _pieceSet.Add(PieceName.TwoVert, piece);


            // ThreeHor
            piece = new Piece("ThreeHor", PieceColor.Three,  PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0, 0) (0, 1) (0, 2)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(0, 1), new Coord(0, 2) };
            // Storing Piece 
            _pieceSet.Add(PieceName.ThreeHor, piece);
            //
            // ThreeVert
            piece = new Piece("ThreeVert", PieceColor.Three,  PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (2, 0)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(2, 0) };
            // Storing Piece 
            _pieceSet.Add(PieceName.ThreeVert, piece);

            // ThreeLOne
            piece = new Piece("ThreeLOne", PieceColor.Four, PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (1, 1)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(1, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.ThreeLOne, piece);
            // ThreeLTwo
            piece = new Piece("ThreeLTwo", PieceColor.Four,  PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (0, 1)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(0, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.ThreeLTwo, piece);
            // ThreeLThree
            piece = new Piece("ThreeLThree", PieceColor.Four,  PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (0, 1) (1, 1)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(0, 1), new Coord(1, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.ThreeLThree, piece);
            // ThreeLFour
            piece = new Piece("ThreeLFour", PieceColor.Four,  PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (1, -1)
            piece.Matrix = new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(1, -1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.ThreeLFour, piece);

            // Four
            piece = new Piece("Four", PieceColor.Five,  PieceAttitude.None);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (0, 1) (1, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(0, 1), new Coord(1, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.Four, piece);

            // FourT
            piece = new Piece("FourT1", PieceColor.Eight,  PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0, 0) (0,1) (0, 2) (1, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourT1, piece);
            //
            piece = new Piece("FourT2", PieceColor.Eight, PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (1, 0) (2, 0) (1, -1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(1, -1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourT2, piece);
            //
            piece = new Piece("FourT3", PieceColor.Eight,  PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0,0) (1, 0) (1, -1) (1, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(1, -1), new Coord(1, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourT3, piece);
            //
            piece = new Piece("FourT4", PieceColor.Eight, PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (1, 0) (2, 0) (1, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(1, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourT4, piece);
            //
            // FourJ
            piece = new Piece("FourJ1", PieceColor.Five,  PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (1, 0) (2, 0) (2, -1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(2, -1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourJ1, piece);
            //
            piece = new Piece("FourJ2", PieceColor.Five, PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0,0) (1, 0) (1, 1) (1, 2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(1, 1), new Coord(1, 2) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourJ2, piece);
            //
            piece = new Piece("FourJ3", PieceColor.Five, PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (1, 0) (2, 0) (0, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(0, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourJ3, piece);
            //
            piece = new Piece("FourJ4", PieceColor.Five, PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0,0) (0, 1) (0, 2) (1, 2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 2) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourJ4, piece);

            // FourL
            piece = new Piece("FourL1", PieceColor.Five, PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (1, 0) (2, 0) (2, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(2, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourL1, piece);
            //
            piece = new Piece("FourL2", PieceColor.Five, PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0,0) (0, 1) (0, 2) (1, 0)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 0) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourL2, piece);
            //
            piece = new Piece("FourL3", PieceColor.Five,  PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (0, 1) (1, 1) (2, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(0, 1), new Coord(1, 1), new Coord(2, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourL3, piece);
            //
            piece = new Piece("FourL4", PieceColor.Five,  PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0,0) (1, 0) (1, -1) (1, -2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(1, -1), new Coord(1, -2) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourL4, piece);
            //
            // FourS
            //
            piece = new Piece("FourS1", PieceColor.Nine, PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0,0) (0, 1) (1, 0) (1, -1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(0, 1), new Coord(1, 0), new Coord(1, -1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourS1, piece);
            //
            piece = new Piece("FourS2", PieceColor.Nine,  PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (1, 0) (1, 1) (2, 1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(1, 1), new Coord(2, 1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourS2, piece);
            //
            // FourZ 
            piece = new Piece("FourSZ1", PieceColor.Nine,  PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0, 0) (0, 1) (1, 1) (1, 2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(0, 1), new Coord(1, 1), new Coord(1, 2) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourZ1, piece);
            //
            piece = new Piece("FourZ2", PieceColor.Nine,  PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0,0) (1, 0) (1, -1) (2, -1)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(1, -1), new Coord(2, -1) };
            // Storing Piece 
            _pieceSet.Add(PieceName.FourZ2, piece);

            // FourHor
            piece = new Piece("FourHor", PieceColor.Six,  PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0, 0) (0, 1) (0, 2) (0, 3)
            piece.Matrix = new List<Coord>() {
                 new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(0, 3)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FourHor, piece);
            // FourVert
            piece = new Piece("FourVert", PieceColor.Six, PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (2, 0) (3, 0)
            piece.Matrix = new List<Coord>() {
                 new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(3, 0)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FourVert, piece);

            // FiveHor
            piece = new Piece("FiveHor", PieceColor.Seven,  PieceAttitude.Horizontal);
            // relative coord list, must match Count
            // (0, 0) (0, 1) (0, 2) (0, 3) (0, 4)
            piece.Matrix = new List<Coord>() {
                 new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(0, 3), new Coord(0, 4)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FiveHor, piece);
            // FiveVert
            piece = new Piece("FiveVert", PieceColor.Seven,  PieceAttitude.Vertical);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (2, 0) (3, 0) (4, 0)
            piece.Matrix = new List<Coord>() {
                 new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(3, 0), new Coord(4, 0)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FiveVert, piece);

            // FiveLOne
            piece = new Piece("FiveLOne", PieceColor.Eight, PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (2, 0) (2, 1) (2, 2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(2, 1), new Coord(2, 2)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FiveLOne, piece);
            // FiveLTwo
            piece = new Piece("FiveLTwo", PieceColor.Eight, PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (2, 0) (0, 1) (0, 2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(0, 1), new Coord(0, 2)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FiveLTwo, piece);
            // FiveLThree
            piece = new Piece("FiveLThree", PieceColor.Eight, PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (0, 1) (0, 2) (1, 2) (2, 2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 2), new Coord(2, 2)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FiveLThree, piece);
            // FiveLFour
            piece = new Piece("FiveLFour", PieceColor.Eight, PieceAttitude.Both);
            // relative coord list, must match Count
            // (2, 0) (2, 1) (0, 2) (1, 2) (2, 2)
            // (0,0) (1, 0) (2, 0) (2, -1) (2, -2)
            piece.Matrix = new List<Coord>() {
                new Coord(0,0), new Coord(1, 0), new Coord(2, 0), new Coord(2, -1), new Coord(2, -2)};
            // Storing Piece 
            _pieceSet.Add(PieceName.FiveLFour, piece);

            //Nine
            piece = new Piece("Nine", PieceColor.Nine, PieceAttitude.Both);
            // relative coord list, must match Count
            // (0, 0) (1, 0) (2, 0) (0, 1) (1, 1) (2, 1) (0, 2) (1, 2) (2, 2)
            piece.Matrix = new List<Coord>() {
                new Coord(0, 0), new Coord(1, 0), new Coord(2, 0),
                new Coord(0, 1), new Coord(1, 1), new Coord(2, 1),
                new Coord(0, 2), new Coord(1, 2), new Coord(2, 2)};
            // Storing Piece 
            _pieceSet.Add(PieceName.Nine, piece);



        }

       
    }
}
