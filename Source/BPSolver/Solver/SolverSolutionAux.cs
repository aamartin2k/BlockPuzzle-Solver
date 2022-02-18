using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {
        private void DMsg(string msg)
        {
            Console.WriteLine(msg);
        }

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
