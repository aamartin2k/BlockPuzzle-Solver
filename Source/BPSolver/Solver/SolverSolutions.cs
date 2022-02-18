using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver.Solver
{
    //  Solver para SimpleGameStatus, da servicio a busqueda de Soluciones
    public partial class Solver
    {
        // Busqueda de soluciones con metodo iterativo  y
        // SimpleGameStatus.
        public SolutionMetaStatus CreateMetaSolution(GameStatus game)
        {
            GameTreeSimple treeRoot;

            // crear nodo root del arbol de posibles movimientos
            treeRoot = CreateSolutionTreePPaso(game);

            // crear resumen de soluciones
            var ramas = treeRoot.SelectLeaves();

            List<Solution> solutions = new List<Solution>();

            foreach (var item in ramas)
            {
                // Seleccionar todos hacia arriba e invertir
                var invSol = item.SelectPathUpward().Reverse();

                // Crear estados de solucion a partir del original game
                solutions.Add(CreateSolution(invSol, game));
            }

            // crear inf de retorno
            SolutionMetaStatus meta = new SolutionMetaStatus(solutions);

            return meta;
        }

        private Solution CreateSolution(IEnumerable<GameTreeSimple> seqNodes, GameStatus initial)
        {
            
            //  Crear objetos GameStatus para solucion
            // crear total eval y dictionary
            Eval TotalEval = Eval.GetTotalEval();

            Dictionary<int, GameStatus> StatusList;

            StatusList = CreateGameStatusSolution(seqNodes, initial);

            foreach (var dkv in StatusList)
            {
                GameStatus game = dkv.Value;

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

        private Dictionary<int, GameStatus> CreateGameStatusSolution(IEnumerable<GameTreeSimple> seqNodes, GameStatus initial)
        {
            // Clonar initial, copiar a parent y pasar al dict

            // Para el el resto nodos
            // Clonar parent, copiar a child 
            //  aplicar move {usr comandos drawpiece y deletenextp}
            //  y pasar al dict
            Dictionary<int, GameStatus> StatusList = new Dictionary<int, GameStatus>();

            GameStatus root = null;
            GameStatus parent = null;
            GameStatus child = null;

            foreach (GameTreeSimple nod in seqNodes)
            {
                if (nod.Item.Nombre == RootName)
                {
                    root = CloneGameStatus(initial);
                    root.Id = nod.Item.Id;
                    root.Nombre = nod.Item.Nombre;

                    parent = root;
                    StatusList.Add(root.Id, root);
                }
                else
                {
                    child = CloneGameStatus(parent);
                    child.Id = nod.Item.Id;
                    child.Nombre = nod.Item.Nombre;

                    child.Movement = nod.Item.Movement;
                    child.Evaluation = nod.Item.Evaluation;

                    child.CompletedRows = nod.Item.CompletedRows;
                    child.CompletedColumns = nod.Item.CompletedColumns;

                    parent = child;
                    StatusList.Add(child.Id, child);

                    // aplicar movimiento Draw y y Delete
                    Movement move = nod.Item.Movement;

                    Piece piece = GetPiece(move.Name);
                    // Create absolute coords list.
                    List<Coord> RealCoords = Piece.GetRealCoords(piece, move.InsertPoint);

                    // create command
                    ICommand command = new DrawPieceCommand(RealCoords, piece.Color, child);
                    command.Do();

                    command = new DeleteNextPieceCommand(move.Index, child.NextPieces);
                    command.Do();

                }
            }
            return StatusList;
        }

        #region Solver Paso a paso por niveles

        public GameTreeSimple CreateSolutionTreePPaso(GameStatus game)
        {
            GameTreeSimple treeRoot;

            // Convertir Estado inicial GameStatus en GameTreeSimple por optimizacion
            SimpleGameStatus GStIni = GetSimpleGameStatus(game);
            GStIni.Nombre = RootName;

            // Solution Tree Root
            treeRoot = new GameTreeSimple(GStIni);
            // sobre las hojas del arbol
            List<GameTreeSimple> ramas = treeRoot.SelectLeaves().ToList();

            DMsg("Iniciando proceso");

            //Llamada a proceso basico por paso
            for (int i = 0; i < Constants.NexPieces; i++)
            {
                //DMsg(string.Format("Hijos de Root: {0} Total Nodos: {1}.", treeRoot.Children.Count, treeRoot.Count()));
                ramas = CreateNewNodesP(ramas, treeRoot);

                ProcessNewNodesP(ramas, treeRoot);

            }

            return treeRoot;
        }

        private List<GameTreeSimple> CreateNewNodesP(List<GameTreeSimple> padres, GameTreeSimple root)
        {
            List<GameTreeSimple> hijos = new List<GameTreeSimple>();
            SimpleGameStatus gstatus;

            foreach (var parent in padres)
            {
                gstatus = parent.Item;
                List<Movement> lmm = new List<Movement>();

                //PrintMsg(string.Format("Procesando {0}.", parent.Item.Nombre));
                foreach (var dkv in gstatus.NextPiecesA)
                {
                    int index = dkv.Key;
                    PieceName piece = dkv.Value;

                    //Generar lista de  movidas
                    lmm.AddRange(CreateMovementsNuevo(index, piece, gstatus));
                }

                //DMsg(string.Format("Procesando {0} movidas.", lmm.Count));
                foreach (var move in lmm)
                {
                    // Obtener gameStatus de Parent y clonar
                    GameTreeSimple node = CreateCloneNodeNuevo(root, parent, parent.Item);
                    SimpleGameStatus cloned = node.Item;
                    cloned.Movement = move;
                    hijos.Add(node);
                }
            }

            return hijos;
        }

        private void ProcessNewNodesP(List<GameTreeSimple> padres, GameTreeSimple root)
        {
            SimpleGameStatus cloned;

            foreach (var parent in padres)
            {
                cloned = parent.Item;

                // Aplicar Movimiento
                MakeMoveNuevo(cloned.Movement, cloned);

                // Borrar pieza de la lista
                DeleteMovedPieceNuevo(cloned.Movement, cloned);
                // Evaluar Movimiento
                cloned.Evaluation = EvaluateMoveNuevo(cloned.Movement, cloned);
                // Chequear por Completamiento
                CheckCompleteAndDeleteNuevo(cloned);
            }

        }

        #endregion

    }
}
