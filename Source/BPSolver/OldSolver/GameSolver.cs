using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Objects;
using BPSolver.Enums;
using System.Diagnostics;

namespace BPSolver.Solver
{
    public partial class GameSolver
    {  
      


        // Constructor
        public GameSolver()
        {
            CreatePieceSet();
            Solutions = new List<Solution>();
        }


        // Properties
        public BoardStats Stats
        {
            get; set;
        }

       
        public List<Solution> Solutions
        {
            get; set;
        }
        #region Public API Event
        public delegate void AnalysisReadyEventHandler(object sender, EventArgs e);
        public event AnalysisReadyEventHandler AnalysisReady;

        private void OnAnalysisReady()
        {
            if (AnalysisReady != null)
            {
                AnalysisReady(this, new EventArgs());
            }
        }


        #endregion

        #region Public API
        public bool TestPiece(int row, int col, PieceName name, ref GameSimpleStatus gstat)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = GetPiece(name);

            // Create absolute coords list.
            Coord insertCoord = new Coord(row, col);
            realCoords = GetRealCoords(insertCoord, piece.Matrix);

            // Test if all coords are within limits.
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // Copying game properties. 
            ApplyGameStatus(gstat);

            // Test if all coords are free
            ret = TestFreeCells(realCoords);
            if (!ret)
                return false;

            // Inserting Piece on board
            var ex = realCoords.Select(c => RootStatus[c, 0].Color = piece.Color).ToList();

            // Testing for Column or Row completion
            ret = IsAnyCompleted();
            gstat.AnyCompleted = ret;

            if (ret)
            {
                // Find completed CoR, pass index to list and clear cells
                if (IsAnyRowCompleted())
                    ListRowsCompleted(gstat.RowsCompleted);

                if (IsAnyColumnCompleted())
                    ListColumnsCompleted(gstat.ColumnsCompleted);

                ClearAnyCompleted();
            }

            // Cells from RootStatus to gstat
            foreach (var cell in RootStatus.Cells)
            {
                gstat[cell.Row, cell.Col].Color = cell.Color;
            }

            return true;
        }

        public bool TestPiece(Coord insertCoord, PieceName name,  GameSimpleStatus gstat)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = GetPiece(name);

            // Create absolute coords list.
            realCoords = GetRealCoords(insertCoord, piece.Matrix);

            // Test if all coords are within limits.
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // Test if all coords are free
            ret = TestFreeCells(gstat, realCoords);
          
            return ret;
        }

        public Piece GetPiece(PieceName name)
        {
            return Pieces.GetPiece(name);
        }

        public void AnalizeGame(GameSimpleStatus gstat)
        {

            // Copying game properties. 
            ApplyGameStatus(gstat);

            CreateAllMoves();

            // Calculate Stats.
            UpdateGameStats();

            // Notify form.
            OnAnalysisReady();
        }

        #endregion

        #region Game Strat Board

        private void ApplyGameStatus(GameSimpleStatus gstat)
        {
            // Create new RootStatus.
            RootStatus = new GameStatus(Rank);

            // Copying game properties. 
            RootStatus.CantMoves = gstat.CantMoves;
            // Next Pieces to queue.
            foreach (var item in gstat.NextPieces)
            {
                if (item != PieceName.None)
                    RootStatus.NextPieces.Enqueue(Pieces.GetPiece(item));
            }
            // Cells to cells.
            foreach (var cell in gstat.Cells)
            {
                RootStatus[cell.Row, cell.Col].Color = cell.Color;
            }

        }


        /// <summary>
        /// Main input
        /// </summary>
        /// <param name="gstat"></param>
        private void SetGameStatus(GameSimpleStatus gstat, bool skipRules = false)
        {
            // Create new RootStatus
            RootStatus = new GameStatus(Rank);

            
            foreach (var item in gstat.NextPieces)
            {
                if (item != PieceName.None)
                    RootStatus.NextPieces.Enqueue(Pieces.GetPiece(item));
            }

            // Put game status on analysis board
            foreach (var cell in gstat.Cells)
            {
                RootStatus[cell.Row, cell.Col].Color = cell.Color;
            }

            // Put Cant Moves to combine
            RootStatus.CantMoves = gstat.CantMoves;

            if (!skipRules)
            {
                // Apply rules, calculate next moves
                //TryAllPiecesFreeCells(RootStatus.NextPieces);

                // nueva implementacion
                CreateAllMoves();

            }

            // Calculate Stats
            UpdateGameStats();

            // test
            //IEnumerable<Cell> cells = gstat.Board.Matrix.Cast<Cell>();
            //int total = cells.Where(p => p.Free.Equals(true)).Count();
            // Inform Client
            if (!skipRules)
            {
                OnAnalysisReady();
            }
        }

       

        private bool TryOnePieceOutRealCoord(int row, int col, Piece piece, out List<Coord> realCoords)
        {
            // crear lista coord absolutas
            Coord insertCoord = new Coord(row, col);
            List<Coord> realm = GetRealCoords(insertCoord, piece.Matrix);
            // asignar argumento de salida
            realCoords = realm;

            // comprobar si todas las coordenadas estan dentro del board
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // comprobar si todas las coordenadas están libres
            ret = TestFreeCells(realCoords);

            return ret;
        }

        private void CreatePieceSet()
        {
            Pieces = new PieceSet();
            Pieces.Create();

        }


        private void UpdateGameStats()
        {
            BoardStats stats = new BoardStats();

            stats.CellsCount = CellsCount;
            stats.FreeCells = FreeCellsCount;
            stats.OccupiedCells = OccupiedCellsCount;
            stats.CompletedRows = CompletedRowsCount;
            stats.CompletedColumns = CompletedColumnsCount;

            Stats = stats;
        }

        #endregion


        #region Calculating moves

       

        //private bool TryOnePieceOneCoord(GameStatus game, int row, int col, Piece piece, Move move)
        //{
        //    // crear lista coord absolutas
        //    Coord insertCoord = new Coord(row, col);
        //    List<Coord> realCoords = GetRealCoords(insertCoord, piece.Matrix);

        //    // comprobar si todas las coordenadas estan dentro del board
        //    bool ret = TestRealCoords(realCoords);
        //    if (!ret)
        //        return false;

        //    // comprobar si todas las coordenadas están libres
        //    ret = TestFreeCells(game, realCoords);
        //    if (!ret)
        //        return false;
        //    // Extraer methods
        //    // comprobar si se completa fila o columna
        //    bool complet = IsPieceComplete(realCoords);

        //    if (complet)
        //        move.CompleteRoC = complet;

        //    // Calcular valor de la posicion
        //    // Preferencia
        //    move.Preference = GetPreference(realCoords);
        //    // Cant Vecinos
        //    //
        //    List<Coord> neighborsRealCoords = GetRealCoordsNeighbors(insertCoord, piece.NeighborsMatrix);
        //    move.Neighbors = GetNeighbors(game, neighborsRealCoords);

            

        //    return ret;
        //}

        // Obtiene cantidad de vecinos
        private int GetNeighbors(GameStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => game[c, 0].IsFree).Where(x => x != true).Count();

            return ex;
        }
        // Suma valores de preferencia de las celdas ocupadas por la pieza
        private int GetPreference(List<Coord> realCoords)
        {
            
            var ssum = realCoords.Select(c => Preferences[c.Row, c.Col]).Sum();
            return ssum;
        }

        //private bool IsPieceComplete(List<Coord> realCoords)
        //{
        //    //  Poner pieza
        //    var ex = realCoords.Select(c => RootStatus[c, 0].Color = PieceColor.One).ToList();
        //    //foreach (var item in realCoords)
        //    //{
        //    //    Board[item, 0].Color = PieceColor.One;
        //    //}

        //    // Comprobar
        //    bool ret = IsAnyCompleted;

        //    // quitar pieza
        //    ex = realCoords.Select(c => RootStatus[c, 0].Color = PieceColor.None).ToList();
        //    //foreach (var item in realCoords)
        //    //{
        //    //    Board[item, 0].Color = PieceColor.None;
        //    //}

        //    return ret;
        //}

        // Eliminar
        //private void TryOnePieceFreeCells(Piece piece)
        //{
        //    bool ret;
        //    Move move ;

        //    foreach (Cell cell in FreeCells.OrderBy(cell => cell.Row).ThenBy(cell => cell.Col))
        //    {
        //        move = new Move();

        //        ret = TryOnePieceOneCoord(cell.Row, cell.Col, piece, move);

        //        if (ret)
        //        {
        //            // Completar parametros de valor de posicion
        //            move.Row = cell.Row;
        //            move.Col = cell.Col;
        //            move.Piece = piece;
                
        //            Moves.Add(move);
        //        }
                
        //    }
        //}

        // Eliminar
        //private void TryAllPiecesFreeCells(List<Piece> nextPieces)
        //{
        //    // new moves list
        //    Moves = new List<Move>();

        //    // lista de piezas ordenada por tamaño de pieza, mayores primero
        //    foreach (var piece in nextPieces.OrderBy(c => c.Count).Reverse())
        //    {
        //      TryOnePieceFreeCells(piece);
        //    }
        //}

        #endregion



    }
}
