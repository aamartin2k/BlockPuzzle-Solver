using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Objects;
using BPSolver.Enums;


namespace BPSolver.Solver
{
    public partial class GameSolver
    {
        // Declarations
        private GameStatus RootStatus;
        

        private void CreateAllMoves()
        {
            GameStatus current;
            
            Queue<GameStatus> qStatus = new Queue<GameStatus>();

            qStatus.Enqueue(RootStatus);

            while (qStatus.Count > 0)
            {
                current = qStatus.Dequeue();

                if (current.CantMoves > 0)
                {
                    // crear todas las posibles movidas para esta combinacion mientras haya piezas en lista
                    if (current.NextPieces.Count > 0)
                    {
                        Piece piece;

                        //foreach (var piece in NextPieces)
                        while (current.NextPieces.Count > 0)
                        {
                            Move move;
                            piece = current.NextPieces.Dequeue();

                            foreach (Cell cell in GetFreeCells(current).OrderBy(cell => cell.Row).ThenBy(cell => cell.Col))
                            {
                                // crear lista coord absolutas para la pieza
                                Coord insertCoord = new Coord(cell.Row, cell.Col);
                                List<Coord> realCoords = GetRealCoords(insertCoord, piece.Matrix);

                                // comprobar si todas las coordenadas estan dentro del board
                                bool ret = TestRealCoords(realCoords);
                                if (!ret)
                                    continue;

                                // comprobar si todas las coordenadas están libres
                                ret = TestFreeCells(current, realCoords);
                                if (!ret)
                                    continue;

                                // La pieza se puede insertar en la posicion: crear movida y estado
                                move = new Move(cell.Row, cell.Col, piece);

                                // Completar parametros de valor de posicion
                                move.Preference = GetPreference(realCoords);
                                // Cant Vecinos
                                List<Coord> neighborsRealCoords = GetRealCoordsNeighbors(insertCoord, piece.NeighborsMatrix);
                                move.Neighbors = GetNeighbors(current, neighborsRealCoords);

                                // Crear nuevo estado producto de esta movida
                                GameStatus newSt = CreateChild(current, move, realCoords);

                                

                                // Pasando referencia a nuevo estado
                                // a la cola de trabajo
                                qStatus.Enqueue(newSt);
                              
                            }

                        }


                    }
                }
                else
                {
                    // no hay mas posibles combinaciones, generar solucion
                    // recorriendo de abajo hasta el root
                    Solution sol = new Solution();

                    if (current.Causa != null)
                        sol.Moves.Add(current.Causa);

                    while (current.Parent != null)
                    {
                        current = current.Parent;
                        if (current.Causa != null)
                            sol.Moves.Add(current.Causa);
                    }

                    var rev = sol.Moves.Select( c => c).Reverse();
                    sol.Moves = rev.ToList();

                    Solutions.Add(sol);
                }


            } // while



        }

        private GameStatus CreateChild(GameStatus current, Move move, List<Coord> realCoords)
        {
            GameStatus newSt = new GameStatus(Rank);

            newSt.Parent = current;
            newSt.Causa = move;
            newSt.CantMoves = --current.CantMoves ;

            // Copiar  contenido lista de celdas
            foreach (var xcell in current.Cells)
            {
                newSt[xcell.Row, xcell.Col].Color = xcell.Color;
            }

            // Copiar  contenido lista NextPieces
            newSt.NextPieces = new Queue<Piece>(current.NextPieces.ToArray());

            // insertando pieza
            var ex = realCoords.Select(c => newSt[c, 0].Color = move.Piece.Color).ToList();

            // Comprobando si hay filas o columnas completas
            move.CompleteRoC = IsAnyCompleted(newSt);

            // Eliminando filas o columnas completas
            return newSt;
        }
       
    }
}
