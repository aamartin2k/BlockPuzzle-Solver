using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;

namespace BPSolver
{
    // To Delete
    internal partial class SolHandler : ISolver
    {
        //static private Dictionary<PieceName, Piece> _pieceSet;

        //static internal Piece GetPiece(PieceName name)
        //{
        //    return _pieceSet[name];
        //}

        //private void CreatePieceSet()
        //{
        //    Piece piece;

        //    _pieceSet = new Dictionary<PieceName, Piece>();

        //    //One
        //    piece = new Piece(PieceName.One,
        //                      PieceColor.One,
        //                      new List<Coord>() { new Coord(0, 0) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // TwoHor
        //    piece = new Piece(PieceName.TwoHor,
        //                      PieceColor.Two,
        //                      new List<Coord>() { new Coord(0, 0), new Coord(0, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // TwoVert
        //    piece = new Piece(PieceName.TwoVert,
        //                      PieceColor.Two,
        //                      new List<Coord>() { new Coord(0, 0), new Coord(1, 0) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // ThreeHor
        //    piece = new Piece(PieceName.ThreeHor,
        //                      PieceColor.Three,
        //                      new List<Coord>() { new Coord(0, 0), new Coord(0, 1), new Coord(0, 2) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // ThreeVert
        //    piece = new Piece(PieceName.ThreeVert,
        //                      PieceColor.Three,
        //                      new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(2, 0) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // ThreeLOne
        //    piece = new Piece(PieceName.ThreeLOne,
        //                        PieceColor.Four,
        //                        new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(1, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // ThreeLTwo
        //    piece = new Piece(PieceName.ThreeLTwo,
        //                        PieceColor.Four,
        //                        new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(0, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // ThreeLThree
        //    piece = new Piece(PieceName.ThreeLThree,
        //                        PieceColor.Four,
        //                        new List<Coord>() { new Coord(0, 0), new Coord(0, 1), new Coord(1, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // ThreeLFour
        //    piece = new Piece(PieceName.ThreeLFour,
        //                        PieceColor.Four,
        //                        new List<Coord>() { new Coord(0, 0), new Coord(1, 0), new Coord(1, -1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // Four
        //    piece = new Piece(PieceName.Four,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(0, 1), new Coord(1, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourT1
        //    piece = new Piece(PieceName.FourT1,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourT2
        //    piece = new Piece(PieceName.FourT2,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(1, -1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourT3
        //    piece = new Piece(PieceName.FourT3,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(1, -1), new Coord(1, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourT4
        //    piece = new Piece(PieceName.FourT4,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(1, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourJ1
        //    piece = new Piece(PieceName.FourJ1,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(2, -1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourJ2
        //    piece = new Piece(PieceName.FourJ2,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(1, 1), new Coord(1, 2) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourJ3
        //    piece = new Piece(PieceName.FourJ3,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(0, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourJ4
        //    piece = new Piece(PieceName.FourJ4,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 2) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourL1
        //    piece = new Piece(PieceName.FourL1,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(2, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourL
        //    piece = new Piece(PieceName.FourL2,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 0) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourL3
        //    piece = new Piece(PieceName.FourL3,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(1, 1), new Coord(2, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourL4
        //    piece = new Piece(PieceName.FourL4,
        //                        PieceColor.Five,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(1, -1), new Coord(1, -2) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourS1
        //    piece = new Piece(PieceName.FourS1,
        //                        PieceColor.Nine,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(1, 0), new Coord(1, -1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourS2
        //    piece = new Piece(PieceName.FourS2,
        //                        PieceColor.Nine,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(1, 1), new Coord(2, 1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourZ1 
        //    piece = new Piece(PieceName.FourZ1,
        //                        PieceColor.Nine,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(1, 1), new Coord(1, 2) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourZ2
        //    piece = new Piece(PieceName.FourZ2,
        //                        PieceColor.Nine,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(1, -1), new Coord(2, -1) });
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourHor
        //    piece = new Piece(PieceName.FourHor,
        //                        PieceColor.Six,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(0, 3)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FourVert
        //    piece = new Piece(PieceName.FourVert,
        //                        PieceColor.Six,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(3, 0)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);

        //    // FiveHor
        //    piece = new Piece(PieceName.FiveHor,
        //                        PieceColor.Seven,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(0, 3), new Coord(0, 4)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FiveVert
        //    piece = new Piece(PieceName.FiveVert,
        //                        PieceColor.Seven,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(3, 0), new Coord(4, 0)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FiveL1
        //    piece = new Piece(PieceName.FiveL1,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(2, 1), new Coord(2, 2)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FiveL2
        //    piece = new Piece(PieceName.FiveL2,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(0, 1), new Coord(0, 2)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FiveL3
        //    piece = new Piece(PieceName.FiveL3,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(0, 1), new Coord(0, 2), new Coord(1, 2), new Coord(2, 2)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //    //
        //    // FiveL4
        //    piece = new Piece(PieceName.FiveL4,
        //                        PieceColor.Eight,
        //                        new List<Coord>() {
        //                        new Coord(0,0), new Coord(1, 0), new Coord(2, 0), new Coord(2, -1), new Coord(2, -2)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);

        //    //Nine
        //    piece = new Piece(PieceName.Nine,
        //                        PieceColor.Nine,
        //                        new List<Coord>() {
        //                        new Coord(0, 0), new Coord(1, 0), new Coord(2, 0),
        //                        new Coord(0, 1), new Coord(1, 1), new Coord(2, 1),
        //                        new Coord(0, 2), new Coord(1, 2), new Coord(2, 2)});
        //    // Storing Piece 
        //    _pieceSet.Add(piece.Name, piece);
        //}
    }
}
