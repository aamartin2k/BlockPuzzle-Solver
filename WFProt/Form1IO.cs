using System.Windows.Forms;
using System;
using BPSolver.Solver;
using BPSolver.Objects;


namespace WFProt
{

    public partial class Form1 : Form
    {

        private void SaveFile(string file)
        {
            GameSimpleStatus st = CreateGameStatus();

            Serializer.Serialize<GameSimpleStatus>(st, file);

        }

        private void LoadFile(string file)
        {
            ICommand command;

            GameSimpleStatus st = Serializer.Deserialize<GameSimpleStatus>(file);

            // Drawing NextPieces
            PictureBox[] pbl = { pbNextPiece1, pbNextPiece2, pbNextPiece3 };

            for (int i = 0; i < GameSolver.NexPieces; i++)
            {
                command = new DrawNextPiece(pbl[i], GetImage(st.NextPieces[i]), st.NextPieces[i]);
                command.Do(); // See * below
            }

            // Drawing Board
            for (int row = 0; row < GameSolver.Rank; row++)
            {
                for (int col = 0; col < GameSolver.Rank; col++)
                {
                    command = new DrawBlockOnBoard(sgBoard,
                                                   GetBlockImage(st[row, col].Color),
                                                   new Coord(row, col),
                                                   st[row, col].Color);
                    //  *
                    // As loading model from file is not subject to Undo, method ExecuteCommandDo is not used
                    command.Do();
                }
            }

            nUpdMoves.Value = st.CantMoves;

            //gGStrat.SetGameStatus(CreateGameStatus(), true);

        }

       
    }
}