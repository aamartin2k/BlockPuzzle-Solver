using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFProt
{
    class Program
    {
        static void Main(string[] args)
        {


            StContext context = new StContext();
            // creando todos los estados
            context.CreateStates();

            // asignando procedimientos de salida a delegates
            context.GridCellDeletionState.Out_DeleteGridCell = Out_DeleteGridCell;
            context.NextPieceDeletionState.Out_DeleteNextPiece = Out_DeleteNextPiece;

            context.NextPieceDrawingState.Out_DrawNextPiece = Out_DrawNextPiece;
            context.GridCellDrawingState.Out_DrawGrid = Out_DrawGrid;

            context.NextPiecePlayState.Out_DrawGridPlay = Out_DrawGridPlay;


            Coord pos = new Coord
            {
                Row = 2,
                Column = 4
            };

            // Acciones sin efecto por no estar en modo Select
            context.PieceButtonClicked(PieceName.Cuatro);
            context.NextPieceImageClicked(2);
            context.GridCellClicked(pos);
            Console.WriteLine("Acciones sin efecto por no estar en modo Select\n");

            // Seleccionar pieza 2 y 
            Console.WriteLine("Prueba 1 Seleccionar pieza 2 y dibujar en Grid 2,4 y 3,6");
            context.ActionSelectClicked();
            context.PieceButtonClicked(PieceName.Dos);
            // dibujarla en Grid
            context.GridCellClicked(pos);
            pos.Row = 3; pos.Column = 6;
            context.GridCellClicked(pos);
            Console.WriteLine("Fin Prueba 1\n");

            Console.WriteLine("Prueba 2 Seleccionar pieza 4 y dibujar en NP 1");
            context.ActionSelectClicked();
            context.PieceButtonClicked(PieceName.Cuatro);
            context.NextPieceImageClicked(1);
            Console.WriteLine("Fin Prueba 2\n");

            Console.WriteLine("Prueba 3 Seleccionar pieza 1 y dibujar en NP 0");
            context.ActionSelectClicked();
            context.PieceButtonClicked(PieceName.Uno);
            context.NextPieceImageClicked(0);
            Console.WriteLine("Fin Prueba 3\n");

            Console.WriteLine("Prueba 4 Borrar Grid 1,2 y 3,2");
            context.ActionDeleteClicked();
            pos.Row = 1; pos.Column = 2;
            context.GridCellClicked(pos);
            pos.Row = 3; pos.Column = 2;
            context.GridCellClicked(pos);
            Console.WriteLine("Fin Prueba 4\n");

            Console.WriteLine("Prueba 5 Borrar NP 0");
            context.ActionDeleteClicked();
            context.NextPieceImageClicked(0);
            Console.WriteLine("Fin Prueba 5\n");

            Console.WriteLine("Prueba 6 Play. Seleccionar NP 1 (contiene Pieza 4) y pasar a Grid 5,5");
            context.ActionSelectClicked();
            context.NextPieceImageClicked(0, PieceName.Cuatro);
            pos.Row = 5; pos.Column = 5;
            context.GridCellClicked(pos);
            Console.WriteLine("Fin Prueba 6\n");

            //Console.ReadLine();
        }

        static void Out_DeleteGridCell(Coord pos)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar Grid Cell Row: {0} Col: {1}", pos.Row, pos.Column));
        }

        static void Out_DeleteNextPiece(int index)
        {
            Console.WriteLine(string.Format("OUTPUT. Borrar NextPieceImage index {0}", index));
        }


        static void Out_DrawNextPiece(int index, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar NextPiece index: {0} Image: {1} ", index, piece));
        }

        static void Out_DrawGrid(Coord pos, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Column, piece));
        }

        static void Out_DrawGridPlay(Coord pos, PieceName piece)
        {
            Console.WriteLine(string.Format("OUTPUT. PLAY. Dibujar Grid Row: {0} Col: {1} Image: {2}", pos.Row, pos.Column, piece));
        }

    }
}
