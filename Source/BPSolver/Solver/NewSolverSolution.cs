using BPSolver.Enums;
using BPSolver.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {

        // Funciones Auxiliares
        // Crear lista de Movimientos
        // Parte de un GStatus y PieceName
        // Recorre todas las celdas libres y comprueba si la pieza "cabe"
        //  var list = game.Cells.Where(c => c.IsFree);

        public List<Movement> CreateMovements(int index, PieceName pname, GameStatus game )
        {
            List<Movement> mlist = new List<Movement>();

            List<Coord> clist = CreateValidPositionList(game, pname);
            Movement mov;

            foreach (var item in clist)
            {
                mov = new Movement(index, item, pname);
                mlist.Add(mov);
            }

            return mlist;
        }

        // Crea lista de coordenadas donde es posible insertar la pieza
        public List<Coord> CreateValidPositionList(GameStatus game, PieceName pname)
        {
            //int count;

             // obtener celdas libres
            var freeCell = game.Cells.Where(c => c.IsFree);
            //count = freeCell.Count();

            // obtener lista de coordenadas de celdas libres
            var freeCoord = freeCell.Select(c => c.Coord).ToList();
            //count = freeCoord.Count();

            // obtener lista de coordenadas donde es posible insertar la pieza
            //  where TestPiece retorna True;
            var qry = from cord in freeCoord
                      where TestPiece(cord, pname, game)
                      select   cord ;

            //count = qry.Count();
            //List<Coord> list = qry.ToList();
            //return list;

            return qry.ToList();
        }

        // Aplicar Movimiento a GameStatus
        // Se repite codigo de comandos para agilidad
        public void MakeMove(Movement move, GameStatus game)
        {
            // Comprobar que coincide el nombre de pieza
            bool ret = move.Name == game.NextPieces[move.Index];

            // Acciones
            // Borrar pieza de Dict
            // El comando DeleteNextPieceCommand asigna PieceName.None
            // aqui se borra realmente.
            game.NextPieces.Remove(move.Index);

            // "Dibujar" pieza en board
            // Get reference to piece
            Piece piece = GetPiece(move.Name);

            // Obtener Real Coords
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);

            // ejecutar
            var ex = realCoords.Select(c => game[c].Color = piece.Color).ToList();
        }

        // Calcula valor de Movimiento aplicado
        public Eval EvaluateMove(Movement move, GameStatus game)
        {
            Eval eval = new Eval();

            Piece piece = GetPiece(move.Name);
            // Tamanno de pieza
            eval.PieceSize = piece.Count;

            // Preference
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);
            eval.Preference = GetPreference(realCoords);

            // Neighbors
            eval.Neighbors = GetNeighborsCount(piece, move.InsertPoint, game);

            // Completion
            bool ret;
            int ccount = 0;

            ret = IsAnyCompleted(game);
            if (ret)
                ccount = CompletedCount(game);

            eval.CompleteRoC = ccount;


            return eval;
        }


        public GameStatus CloneGameStatus(GameStatus item)
        {
            // create Memory Stream

            GameStatus cloned;

            using (MemoryStream tempStream = new MemoryStream())
            {
                Serializer.Serialize<GameStatus>(item, tempStream);
                tempStream.Position = 0;

                cloned = Serializer.Deserialize<GameStatus>(tempStream);
            }

            return cloned;
        }


        // Leer valor de preferencia
        private int GetPreference(List<Coord> coords)
        {
            var ssum = coords.Select(c => Preferences[c.Row, c.Col]).Sum();
            return ssum;
        }

        // Leer cantidad de Celdas vecina ocupadas
        private int GetNeighborsCount(Piece piece, Coord point, GameStatus game)
        {
            // Matriz Coord vecinos
            List<Coord> ngbMatrix;
            ngbMatrix = Piece.GetNeighborsMatrix(piece);
            // Coord Reales vecinos
            List<Coord> realCoords;
            realCoords = Piece.GetNeighborsRealCoords(point, ngbMatrix);

            var ex = realCoords.Select(c => game[c].IsFree).Where(x => x != true).Count();
            return ex;
        }
        



    }
}
