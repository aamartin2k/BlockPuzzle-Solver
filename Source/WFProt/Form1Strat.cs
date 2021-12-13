using System.Windows.Forms;
using System;
using BPSolver.Objects;
using BPSolver.Solver;
using BPSolver.Enums;


namespace WFProt
{

    public partial class Form1 : Form
    {
        

        // Create GameStatus for Solver
        private GameSimpleStatus CreateGameStatus()
        {
            //GameSimpleStatus st = new GameSimpleStatus(GameSolver.Rank);
            GameSimpleStatus st = null;

            // Copying NextPieces
            PictureBox[] pbl = { pbNextPiece1, pbNextPiece2, pbNextPiece3 };

            for (int i = 0; i < Constants.NexPieces; i++)
            {
                st.NextPieces.Add((pbl[i].Tag != null) ? (PieceName)pbl[i].Tag : PieceName.None);
            }

            // Copying Cells color
            for (int row = 0; row < Constants.Rank; row++)
            {
                for (int col = 0; col < Constants.Rank; col++)
                {
                    st[row, col].Color = (PieceColor)sgBoard[row, col].Tag;
                }
            }

            st.CantMoves = (int) nUpdMoves.Value;
            return st;
        }

        // Call Solver AnalizeGame
        // Continuar refactor
        private void AnalizeGame()
        {
            //gGStrat.AnalizeGame(CreateGameStatus());
        }

    }
}