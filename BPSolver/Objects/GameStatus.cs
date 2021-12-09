using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;


namespace BPSolver.Objects
{ 

    [Serializable]
    public class GameSimpleStatus
    {
        // Propiedades del juego que se van a guardar el archivo o enviar a Solver  
        //
        public int CantMoves;

        public bool AnyCompleted;
        public List<int> RowsCompleted;
        public List<int> ColumnsCompleted;
        public List<PieceName> NextPieces;

        // Cells collection
        public List<Cell> Cells;
        // Cells Indexer
        public Cell this[int row, int col]
        {
            get { return Cells.First(z => z.Row == row && z.Col == col); }
        }


        public GameSimpleStatus(byte rank )
         {
            
            
            NextPieces = new List<PieceName>();
            RowsCompleted = new List<int>();
            ColumnsCompleted = new List<int>();


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

       
    }

}
