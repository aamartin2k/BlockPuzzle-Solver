using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Solver
{
    public partial class Solver
    {

        public SolutionMetaStatus CreateMetaSolution(GameStatus game)
        {
            GameTreeSimple treeRoot;

            // crear arbol de games
            treeRoot = CreateSolutionTreeNuevo(game);

            // crear resumen de soluciones
            var ramas = treeRoot.SelectLeaves();

            List<Solution> solutions = new List<Solution>();

            foreach (var item in ramas)
            {
                // Seleccionar todos hacia arriba e invertir
                var invSol = item.SelectPathUpward().Reverse();

                solutions.Add(CreateSolutionX(invSol));
            }

            // crear inf de retorno
            SolutionMetaStatus meta = new SolutionMetaStatus(solutions);

            Console.WriteLine("*** Cant Nodos: {0} ***", treeRoot.Count() );
            return meta;
        }

        private Solution CreateSolutionX(IEnumerable<GameTreeSimple> seqNodes)
        {
            GameStatus game;
            //  Crear objetos GameStatus para solucion
            // crear total eval y dictionary
            Eval TotalEval = Eval.GetTotalEval();
            Dictionary<int, GameStatus> StatusList = new Dictionary<int, GameStatus>();

            foreach (GameTreeSimple nod in seqNodes)
            {
                game = GetGameStatus( nod.Item) ;

                StatusList.Add(game.Id, game);

                // saltando GameStatus inicial, que no tiene Eval
                if (game.Nombre != RootName)
                {
                    TotalEval.PieceSize += game.Evaluation.PieceSizeTotal;
                    TotalEval.Preference += game.Evaluation.PreferenceTotal;
                    TotalEval.Neighbors += game.Evaluation.NeighborsTotal;
                    TotalEval.CompleteRoC += game.Evaluation.CompleteRoCTotal;
                }
            }

            Solution sol = new Solution(TotalEval, StatusList);
            return sol;
        }

        private GameTreeSimple CreateSolutionTreeNuevo(GameStatus game)
        {
            GameTreeSimple treeRoot;

            // Clon de Estado inicial para no modificarlo
            SimpleGameStatus GStIni = GetSimpleGameStatus(game);
            GStIni.Nombre = RootName;

            // Solution Tree Root
            treeRoot = new GameTreeSimple(GStIni);

            //DMsg("Iniciando proceso");

            //Llamada a proceso basico recursivo
            ProccessNodeNuevo(treeRoot, treeRoot);

            //DMsg(string.Format("{0} tiene {1} hijos", treeRoot.Item.Nombre, treeRoot.Count()));

            return treeRoot;
        }


        private void ProccessNodeNuevo(GameTreeSimple parent, GameTreeSimple root)
        {
            SimpleGameStatus gstatus = parent.Item;

            //PrintMsg(string.Format("Procesando {0}.", parent.Item.Nombre));
            foreach (var dkv in gstatus.NextPiecesA)
            {
                int index = dkv.Key;
                PieceName piece = dkv.Value;

                //Generar lista de  movidas
                List<Movement> lmm = CreateMovementsNuevo(index, piece, gstatus);
                
                //DMsg(string.Format("Procesando {0} movidas.", lmm.Count));
                foreach (var move in lmm)
                {
                    ProcessMoveNuevo(move, parent, root);
                }
            }

            //if (parent.Children.Count > 50)
            //    ReduceChildrenNuevo(parent);

            //Para cada hijo de NodoParent
            //PrintMsg(string.Format("{0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));
            var idList = parent.Children.Select(n => n.Id).ToArray();
            foreach (var id in idList)
            {
                ProccessNodeNuevo(root[id], root);
            }

            
        }

        private void ReduceChildrenNuevo(GameTreeSimple parent)
        {
            var children = parent.Children;
            //DMsg(string.Format("RC: Nodo {0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));

            int maxEval = children.Max(n => n.Item.Evaluation.Total);
            int limt = Convert.ToInt32(maxEval * 0.7);
            var toPrune = children.Where(n => (n.Item.Evaluation.CompleteRoC == 0) && (n.Item.Evaluation.Total < limt));

            var idList = toPrune.Select(n => n.Id).ToArray();

            // Si la reduccion baja de  limit
            //if (2 < (parent.Children.Count - idList.Length))
            //{
            foreach (var id in idList)
            {
                //DMsg(string.Format(" Eliminando: Nodo {0} Eval {1}", parent[id].Item.Nombre, parent[id].Item.Evaluation));
                parent[id].Detach();
            }

            //DMsg(string.Format(" RC: Nodo {0} reducido a {1} hijos", parent.Item.Nombre, parent.Children.Count));
            //}


        }


        private void ProcessMoveNuevo(Movement move, GameTreeSimple parent, GameTreeSimple root)
        {
            // Obtener gameStatus de Parent y clonar
            SimpleGameStatus cloned = CreateCloneChildNuevo(root, parent, parent.Item);
            // Aplicar Movimiento
            cloned.Movement = MakeMoveNuevo(move, cloned);
            // Borrar pieza de la lista
            DeleteMovedPieceNuevo(move, cloned);
            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMoveNuevo(move, cloned);
            // Chequear por Completamiento
            CheckCompleteAndDeleteNuevo(cloned);
        }


        private void CheckCompleteAndDeleteNuevo(SimpleGameStatus game)
        {
            if (IsAnyCompletedNuevo(game))
            {
                ClearCompletedNuevo(game);
            }
        }

        private Eval EvaluateMoveNuevo(Movement move, SimpleGameStatus game)
        {
            Eval eval = Eval.GetNewEval();

            Piece piece = GetPiece(move.Name);
            // Tamanno de pieza
            eval.PieceSize = piece.Count;

            // Preference
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);
            eval.Preference = GetPreference(realCoords);

            // Neighbors
            eval.Neighbors = GetNeighborsCountNuevo(piece, move.InsertPoint, game);

            // Completion
            bool ret;
            int ccount = 0;

            ret = IsAnyCompletedNuevo(game);
            if (ret)
                ccount = CompletedCountNuevo(game);

            eval.CompleteRoC = ccount;

            return eval;

        }

        private int GetNeighborsCountNuevo(Piece piece, Coord point, SimpleGameStatus game)
        {
            // Matriz Coord vecinos
            List<Coord> ngbMatrix;
            ngbMatrix = Piece.GetNeighborsMatrix(piece);
            // Coord Reales vecinos
            List<Coord> realCoords;
            realCoords = Piece.GetNeighborsRealCoords(point, ngbMatrix);

            var ex = realCoords.Select(c => true == game[c]).Where(x => x != true).Count();
            return ex;
        }

        private void DeleteMovedPieceNuevo(Movement move, SimpleGameStatus game)
        {
            // Borrar pieza de Dictionary
            game.NextPiecesA.Remove(move.Index);
            
        }

        private Movement MakeMoveNuevo(Movement move, SimpleGameStatus game)
        {
            // Comprobar que coincide el nombre de pieza
            bool ret = move.Name == game.NextPiecesA[move.Index];

            if (!ret)
                throw new Exception("No coincide el nombre de pieza de Move con Dictionary");
            // "Dibujar" pieza en board
            // Get reference to piece
            Piece piece = GetPiece(move.Name);

            // Obtener Real Coords
            List<Coord> realCoords;
            realCoords = Piece.GetRealCoords(piece, move.InsertPoint);

            // ejecutar para class SimpleCell
            var ex = realCoords.Select(c => game[c] = true).ToList();

            
            return move;
        }

        private SimpleGameStatus CreateCloneChildNuevo(GameTreeSimple root, GameTreeSimple parent, SimpleGameStatus game)
        {
            SimpleGameStatus clonedGame = CloneSimpleGameStatus(game);

            // Reset Id
            clonedGame.Id = root.Count();

            // Reset Name
            clonedGame.Nombre = string.Format("Cloned {0}", clonedGame.Id);

            //crear
            parent.AddChild(clonedGame);

            return clonedGame;
        }


        private List<Movement> CreateMovementsNuevo(int index, PieceName pname, SimpleGameStatus game)
        {
            List<Movement> mlist = new List<Movement>();

            List<Coord> clist = CreateValidPositionListNuevo(game, pname);
            Movement mov;

            foreach (var item in clist)
            {
                mov = new Movement(index, item, pname);
                mlist.Add(mov);
            }

            return mlist;
        }


        private List<Coord> CreateValidPositionListNuevo(SimpleGameStatus game, PieceName pname)
        {
          
            // obtener lista de coordenadas de celdas libres
            var freeCoord = GetFreeCells(game);
           
            // obtener lista de coordenadas donde es posible insertar la pieza
            //  where TestPiece retorna True;
            var qry = from cord in freeCoord
                      where TestPieceNuevo(cord, pname, game)
                      select cord;

            int count = qry.Count();
            //List<Coord> list = qry.ToList();
            //return list;

            return qry.ToList();
        }

        public bool TestPieceNuevo(Coord insertCoord, PieceName name, SimpleGameStatus gstat)
        {
            List<Coord> realCoords;
            // Get reference to piece
            Piece piece = GetPiece(name);

            // Create absolute coords list.
            realCoords = Piece.GetRealCoords(piece, insertCoord);

            // Test if all coords are within limits.
            bool ret = TestRealCoords(realCoords);
            if (!ret)
                return false;

            // Test if all coords are free
            ret = TestFreeCellsNuevo(gstat, realCoords);

            return ret;
        }

        private bool TestFreeCellsNuevo(SimpleGameStatus game, List<Coord> realCoords)
        {
            var ex = realCoords.Select(c => true == game[c]).Where(x => x == true).Count();
            return ex == 0;
        }

        // Lista de Coords de celdas libres
        private List<Coord> GetFreeCells(SimpleGameStatus game)
        {
            List<Coord> freec = new List<Coord>();
           
            for (int i = 0; i < Constants.Rank; i++)
            {
                for (int j = 0; j < Constants.Rank; j++)
                {
                    if (!game.CellsA[i, j])
                        freec.Add(new Coord(i, j));
                }
            }
            return freec;
        }

        // Metodo Factory para optimizar Solver
        // Susutituye a Clone
        private GameStatus  GetGameStatus(SimpleGameStatus game)
        {
            GameStatus sg = Controller.CreateChildStatus(game.Id, game.Nombre);

            sg.Movement = game.Movement;
            sg.Evaluation = game.Evaluation;

            sg.CompletedRows = game.CompletedRows;
            sg.CompletedColumns = game.CompletedColumns;

            foreach (var dkv in game.NextPiecesA)
            {
                //sg.NextPieces.Add(dkv.Key, dkv.Value);
                sg.NextPieces[dkv.Key] = dkv.Value;
            }

      
            for (int i = 0; i < Constants.Rank; i++)
            {
                for (int j = 0; j < Constants.Rank; j++)
                {
                    sg[i, j].Color = game.CellsA[i, j] ? PieceColor.One : PieceColor.None    ;
                }
            }

            return sg;
        }

        private SimpleGameStatus GetSimpleGameStatus(GameStatus game)
        {
            SimpleGameStatus sg = new SimpleGameStatus();

            //// Propiedades para Solver
            // Movement y Evaluation no se clonan

            sg.Id = game.Id;
            sg.Nombre = game.Nombre;

            //sg.NextPiecesA = new PieceName[Constants.NexPieces];
            sg.NextPiecesA = new Dictionary<int, PieceName>();

            sg.NextPiecesA.Add(0, game.NextPieces[0]);
            sg.NextPiecesA.Add(1, game.NextPieces[1]);
            sg.NextPiecesA.Add(2, game.NextPieces[2]);

            sg.CellsA = new bool[Constants.Rank,  Constants.Rank];
         
            for (int i = 0; i < Constants.Rank; i++)
            {
                for (int j = 0; j < Constants.Rank; j++)
                {
                    sg.CellsA[i, j] = !game[i, j].IsFree ;
                }
            }
            return sg;
        }

        private SimpleGameStatus CloneSimpleGameStatus(SimpleGameStatus game)
        {
            SimpleGameStatus sg = new SimpleGameStatus();

            sg.Id = game.Id;
            sg.Nombre = game.Nombre;

            sg.NextPiecesA = new Dictionary<int, PieceName>();
         
            foreach (var dkv in game.NextPiecesA)
            {
                sg.NextPiecesA.Add(dkv.Key, dkv.Value);
            }

            sg.CellsA = new bool[Constants.Rank, Constants.Rank];

            
            for (int i = 0; i < Constants.Rank; i++)
            {
                for (int j = 0; j < Constants.Rank; j++)
                {
                    sg.CellsA[i, j] = game.CellsA[i, j];
                }
            }

            return sg;

        }

    }
}
