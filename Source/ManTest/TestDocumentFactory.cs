using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTest
{
    class TestDocumentFactory
    {
        // Output
        internal Action Out_NewFile { get; set; }
        internal Action<string> Out_SaveFileAs { get; set; }
        internal Action Out_CloseFile { get; set; }
        internal Action<string> Out_LoadFile { get; set; }
        internal Action<List<Coord>, PieceColor> Out_Draw { get; set; }
        internal Action<int, PieceName> Out_DrawNextPiece { get; set; }
        internal Action<Coord> Out_DeleteGridCell  { get; set; }
      
        // Input
        internal void In_NewFileResult(bool result, string text)
        {
            Console.WriteLine(string.Format("New file {0} result: {1}", text, result));
        }
        internal void In_CloseFileResult(bool result, string text)
        {
            Console.WriteLine(string.Format("Close file {0} result: {1}", text, result));
        }

        internal void In_SaveFileResult(bool result, string text)
        {
            Console.WriteLine(string.Format("Save file {0} result: {1}", text, result));
        }

        internal void In_Draw_Result(bool result)
        {
            Console.WriteLine(string.Format("Draw operation result: {0}", result));
        }

        internal void In_DrawNextPiece_Result(bool result)
        {
            Console.WriteLine(string.Format("Draw Next piece operation result: {0}", result));
        }

        public void Start()
        {
            // create new file
            // draw coords
            // save as 
            // close file
            Piece piece;
            PieceColor color;
            List<Coord> coords, points;

            #region  tgame01.bmd
            Out_NewFile();
            // File tgame01.bmd
            // Piece PieceName.Nine
            // Coords: (0, 0) (0, 7) (3, 1) (3, 5) (6, 0) (6, 4) (6, 7)       
            piece = PieceSet.GetPiece(PieceName.Nine);
            color = piece.Color;
           
            points = new List<Coord>()
            {
                new Coord(0, 0), new Coord(0, 7), new Coord(3, 1),
                new Coord (3, 5), new Coord (6, 0), new Coord (6, 4),
                new Coord (6, 7)
            };

            coords = new List<Coord>();
            foreach (var point in points)
            {
                coords.AddRange(Piece.GetRealCoords(piece, point));
            }

            Out_Draw(coords, color);

            // Piece PieceName.One
            // Coords: (3, 9) (5,0) (5, 4) (9,9)
            color = PieceColor.One;

            points = new List<Coord>()
            {
                new Coord(3, 9),new Coord(5,0), new Coord(5, 4),new Coord(9,9)
            };
            Out_Draw(points, color);

            // Next Pieces
            // Piece PieceName.Nine  Index 0
            // Piece PieceName.Four  Index 1
            // Piece PieceName.FourVert  Index 2
            Out_DrawNextPiece(0, PieceName.Nine);
            Out_DrawNextPiece(1, PieceName.Four);
            Out_DrawNextPiece(2, PieceName.FourVert);

            Out_SaveFileAs("tgame01.bmd");
            Out_CloseFile();
            #endregion

            #region  tgame02.bmd
            Out_NewFile();
            // File tgame02.bmd
            // Piece PieceName.Nine
            // Coords: (7, 4) (7, 7) 
            piece = PieceSet.GetPiece(PieceName.Nine);
            color = piece.Color;
            points = new List<Coord>()
            {
                new Coord(7, 4), new Coord(7, 7)
            };

            coords = new List<Coord>();
            foreach (var point in points)
            {
                coords.AddRange(Piece.GetRealCoords(piece, point));
            }
            Out_Draw(coords, color);

            // Piece PieceName.Four
            // Coords: (0,2) (0, 8) (2, 5) (4, 1) 
            piece = PieceSet.GetPiece(PieceName.Four);
            color = piece.Color;
            points = new List<Coord>()
            {
                new Coord(0,2), new Coord(0, 8), new Coord(2, 5), new Coord(4, 1)
            };

            coords = new List<Coord>();
            foreach (var point in points)
            {
                coords.AddRange(Piece.GetRealCoords(piece, point));
            }
            Out_Draw(coords, color);

            // Piece PieceName.TwoHor
            // Coords: (4,4) (6,6)
            piece = PieceSet.GetPiece(PieceName.TwoHor);
            color = piece.Color;
            points = new List<Coord>()
            {
                new Coord(4, 4), new Coord(6, 6)
            };

            coords = new List<Coord>();
            foreach (var point in points)
            {
                coords.AddRange(Piece.GetRealCoords(piece, point));
            }
            Out_Draw(coords, color);

            // Piece PieceName.ThreeHor
            // Coords: (4, 7) 
            piece = PieceSet.GetPiece(PieceName.ThreeHor);
            color = piece.Color;
            points = new List<Coord>()
            {
                new Coord(4, 7)
            };

            coords = new List<Coord>();
            foreach (var point in points)
            {
                coords.AddRange(Piece.GetRealCoords(piece, point));
            }
            Out_Draw(coords, color);

            // Piece PieceName.One
            // Coords: (0,1) (0,4) (0,6) (1,1) (2,9) (4,0) (6,0) (6,9) (8,1) (9,0) (9,1)
            color = PieceColor.One;

            points = new List<Coord>()
            {
                new Coord(0, 1), new Coord(0, 4), new Coord(0,6), new Coord(1,1),
                new Coord(2, 9), new Coord(4, 0), new Coord(6,0), new Coord(6,9),
                new Coord(8, 1), new Coord(9, 0), new Coord(9, 1)
            };
            Out_Draw(points, color);

            // Next Pieces
            // Piece PieceName.FiveL4  Index 0
            // Piece PieceName.Four  Index 1
            // Piece PieceName.FourVert  Index 2
            Out_DrawNextPiece(0, PieceName.FiveL4);
            Out_DrawNextPiece(1, PieceName.ThreeHor);
            Out_DrawNextPiece(2, PieceName.FourHor);

            Out_SaveFileAs("tgame02.bmd");
            Out_CloseFile();
            #endregion

            #region  tgame03.bmd
            // File tgame02.bmd
            // Delete(0,4)(1,1)(2,9)(9,1)
            Out_LoadFile("tgame02.bmd");

            points = new List<Coord>()
            {
                new Coord(0, 4), new Coord (1, 1),
                new Coord(2, 9), new Coord(9, 1)
            };

            foreach (var point in points)
            {
                Out_DeleteGridCell(point);
            }
            Out_SaveFileAs("tgame03.bmd");
            Out_CloseFile();
            #endregion

            #region  tgame04.bmd
            // tgame03.bmd
            // Delete (7,4) (7,5) (8,4) (8,5) (9,4) (9,5)(9, 6)
            Out_LoadFile("tgame03.bmd");

            points = new List<Coord>()
            {
                new Coord(7, 4), new Coord (7, 5),
                new Coord(8, 4), new Coord(8, 5),
                new Coord(9, 4), new Coord(9, 5), new Coord(9, 6)
            };

            foreach (var point in points)
            {
                Out_DeleteGridCell(point);
            }
            Out_SaveFileAs("tgame04.bmd");
            Out_CloseFile();
            #endregion

            #region  tgame05.bmd
            // tgame04.bmd
            // Delete  (3, 6)
            Out_LoadFile("tgame04.bmd");

            Out_DeleteGridCell(new Coord(3, 6));
    
            Out_SaveFileAs("tgame05.bmd");
            Out_CloseFile();
            #endregion

            #region  tgame06.bmd
            // tgame05.bmd
            // Delete (0, 7) (1, 7) (8,6) 
            Out_LoadFile("tgame05.bmd");

            points = new List<Coord>()
            {
                new Coord(0, 7), new Coord (1, 7), new Coord(8, 6)
            };

            foreach (var point in points)
            {
                Out_DeleteGridCell(point);
            }
            Out_SaveFileAs("tgame06.bmd");
            Out_CloseFile();
            #endregion

            #region  tgame07.bmd
            // tgame06.bmd
            // Delete (1,3)(5,2)(8,7)
            Out_LoadFile("tgame06.bmd");

            points = new List<Coord>()
            {
                new Coord(1, 3), new Coord (5, 2), new Coord(8, 7)
            };

            foreach (var point in points)
            {
                Out_DeleteGridCell(point);
            }
            Out_SaveFileAs("tgame07.bmd");
            Out_CloseFile();
            #endregion
        }

    }
}
