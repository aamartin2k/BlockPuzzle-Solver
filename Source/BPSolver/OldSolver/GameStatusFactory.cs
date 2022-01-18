using BPSolver.Enums;
using BPSolver.Solver;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Objects
{
    // No Compile
    public class GameStatusFactory
    {
        static public GameStatus Create()
        {
            GameStatus onew = new GameStatus
            {
                

                NextPieces = new List<PieceName>(),

                Cells = new  List<Cell>(),
                
            };

            
            // llenar con nada
            onew.NextPieces.Add(PieceName.None);
            onew.NextPieces.Add(PieceName.None);
            onew.NextPieces.Add(PieceName.None);

            for (int i = 0; i < Constants.Rank; i++)
            {
                for (int j = 0; j < Constants.Rank; j++)
                {
                    onew.Cells.Add(new Cell(j, i));
                }
            }

            return onew;
        }

        static public GameStatus Clone(GameStatus item)
        {
            // create Memory Stream

            GameStatus cloned;

            using (MemoryStream tempStream = new MemoryStream())
            {
                Serializer.Serialize<GameStatus>(item, tempStream);
                cloned = Serializer.Deserialize<GameStatus>(tempStream);
            }

            return cloned;
        }
    }
}
