
using System;
using BPSolver.Enums;
using System.ComponentModel;

namespace WFProt
{
    [Serializable]
    struct Cell
    {
        
        [DefaultValue(true)]
        public bool Free
        {
            get
            {
                if (Color == PieceColor.None)
                    return true;
                else
                    return false;
            }
            
        }
        [DefaultValue(PieceColor.None)]
        public PieceColor Color
        {
            get;    set;
        }
    }

    [Serializable]
    struct Board
    {
        public Cell[,] Matrix;

        public Board(byte rank)
        {
            Matrix = new Cell[ rank, rank];

            
            for (int i = 0; i < rank; i++)
            {
                for (int j = 0; j < rank; j++)
                {
                    Matrix[i, j] = new Cell();
                }
            }
        }
    }
}