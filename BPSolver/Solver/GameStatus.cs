using System;
using System.Collections.Generic;
using System.Linq;
using BPSolver.Objects;
using System.Threading.Tasks;
using BPSolver.Enums;

namespace BPSolver.Solver
{
    public class GameStatus
    {
        public int CantMoves { get; set; }
        public Queue<Piece> NextPieces;


        // Estado padre
        public GameStatus Parent { get; set; }
       
        // Movida que genera este estado
        public Move Causa { get; set; }


        public List<Cell> Cells { get; set; }

        public GameStatus(int rank)
        {
            NextPieces = new Queue<Piece>();

            Cells = new List<Cell>();

            //int id = 1;
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < rank; j++)
                {
                    Cells.Add(new Cell( j, i));
                    //id++;
                }
            }
        }

        // Cells Indexer
        public Cell this[Coord coord, int bar]
        {
            get { return Cells.First(z => z.Row == coord.Row && z.Col == coord.Col); }
        }

        public Cell this[int row, int col]
        {
            get { return Cells.First(z => z.Row == row && z.Col == col); }
        }




    }
}
