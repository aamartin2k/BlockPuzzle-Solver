using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;

using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {
        // Copia el comportamiento de Controller TreeHandler
        // Eliminar field, pasar como argumento
        private const string  RootName = "Cloned Root";

        public SolutionMetaStatus CreateMetaSolution(GameStatus game)
        {
            GameTreeNode treeRoot;

            // crear arbol de games
            treeRoot = CreateSolutionTree(game);

            // crear resumen de soluciones
            var ramas = treeRoot.SelectLeaves();

            List<Solution> solutions = new List<Solution>();

            foreach (GameTreeNode item in ramas)
            {
                // Seleccionar todos hacia arriba e invertir
                var invSol = item.SelectPathUpward().Reverse();

                solutions.Add(CreateSolution(invSol));
            }

            // crear inf de retorno
            SolutionMetaStatus meta = new SolutionMetaStatus(solutions);

            return meta;
        }

        private Solution CreateSolution(IEnumerable<GameTreeNode> seqNodes)
        {
            GameStatus game;

            // crear total eval y dictionary
            Eval TotalEval = Eval.GetTotalEval();
            Dictionary<int, GameStatus> StatusList = new Dictionary<int, GameStatus>();

            foreach (GameTreeNode nod in seqNodes)
            {
                game = nod.Item;
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

        public GameTreeNode CreateSolutionTree(GameStatus game)
        {
            //Crear SolTree y RootNode con copia de GStIni
            GameTreeNode treeRoot;

            // Clon de Estado inicial para no modificarlo
            GameStatus GStIni = CloneGameStatus(game);
            GStIni.Nombre = RootName;

            // Solution Tree Root
            treeRoot = new GameTreeNode(GStIni);

            DMsg("Iniciando proceso");

            ProccessNode(treeRoot);

            return treeRoot;
        }

        #region Solver Paso a paso por niveles
        private void ProccessNode(GameTreeNode parent)
        {
            // version general
            GameTreeNode RootNode;
            GameStatus GameStatus;
            Dictionary<int, GameTreeNode> ListNodes = new Dictionary<int, GameTreeNode>();

            // se repite tres veces
            int index;
            for (index = 0; index < 3; index++)
            {
                GameStatus = CloneGameStatus(parent.Item);
                RootNode = new GameTreeNode(GameStatus);

                if (index == 0)
                {
                    GameStatus.NextPieces.Remove(1);
                    GameStatus.NextPieces.Remove(2);
                }
                if (index == 1)
                {
                    GameStatus.NextPieces.Remove(0);
                    GameStatus.NextPieces.Remove(2);
                }
                if (index == 2)
                {
                    GameStatus.NextPieces.Remove(0);
                    GameStatus.NextPieces.Remove(1);
                }
                List<Movement> lmm = CreateMovements(index, GameStatus.NextPieces[index], GameStatus);

                DMsg(string.Format(" Pieza {0} Movidas: {1}", GameStatus.NextPieces[index], lmm.Count));

                foreach (var move in lmm)
                {
                    ProcessMove(move, RootNode, RootNode);
                }

                int MaxPiece = RootNode.Children.Max(n => n.Item.Evaluation.Total);

                if (!ListNodes.ContainsKey(MaxPiece))
                {
                    ListNodes.Add(MaxPiece, RootNode);
                }
                else
                    throw new Exception("Conflicto de Keys. Evaluaciones de igual valor");

                DMsg(string.Format(" Pieza {0} Max Eval: {1}", GameStatus.NextPieces[index], MaxPiece));

            } // end for

            var maxKey = ListNodes.Keys.ToList().Max();
            RootNode = ListNodes[maxKey];





        }
        #endregion

        #region Solver Basico

        // Realiza iteracion por lista de NextPieces y no emplea Queue
        private void ProccessNodeB (GameTreeNode parent, GameTreeNode root)
        {
            GameStatus gstatus = parent.Item;

            //PrintMsg(string.Format("Procesando {0}.", parent.Item.Nombre));
            foreach (var dkv in gstatus.NextPieces)
            {
                int index = dkv.Key;
                PieceName piece = dkv.Value;

                //Generar lista de  movidas
               List <Movement> lmm = CreateMovements(index, piece, gstatus);

                //PrintMsg(string.Format("Procesando {0} movidas.", lmm.Count ));
                foreach (var move in lmm)
                {
                    ProcessMove(move, parent, root);
                }
            }
            // Eliminar parcial. Reducir lista de hijos
            //ReduceMoves(parent);
      
            if (parent.Children.Count > 20)
                ReduceChildren(parent);

            //Para cada hijo de NodoParent
            //PrintMsg(string.Format("{0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));
            var idList = parent.Children.Select(n => n.Id).ToArray();
            foreach (var id in idList)
            {
                ProccessNodeB(root[id], root);
            } 
           

        }

        // Anidadas
        private void DMsg(string msg)
        {  
            Console.WriteLine( msg);
        }

        // Reducir movidas
        // aplicar "heuristicas"  para reducir movidas 
        private void ReduceMoves(GameTreeNode parent)
        {
            var children = parent.Children;
            //PrintMsg(string.Format("RM: Nodo {0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));

            var toPrune = children.Where(n => n.Item.Evaluation.Neighbors < 1).ToList();
            if (toPrune.Count > 0)
            {
                var idList = toPrune.Select(n => n.Item.Id).ToArray();
                GameStatus game;

                foreach (int id in idList)
                {
                    game = parent[id].Item;
                    DMsg(string.Format("Nodo: {0} Vecinos: {1} Eval: {2} ", game.Nombre, game.Evaluation.Neighbors, game.Evaluation));
                    //parent[id].Detach();
                }
            }
            //PrintMsg(string.Format(" RM: Nodo {0} reducido a {1} hijos", parent.Item.Nombre, parent.Children.Count));
        }

        // Reducir hijos
        // Reducir nodos hijo en base al valor de evaluación
        private void ReduceChildren(GameTreeNode parent) 
        {
            var children = parent.Children;
            //PrintMsg(string.Format("RC: Nodo {0} tiene {1} hijos", parent.Item.Nombre, parent.Children.Count));

            int maxEval = children.Max(n => n.Item.Evaluation.Total);
            int limt = Convert.ToInt32( maxEval * 0.7);
            var toPrune = children.Where(n => n.Item.Evaluation.Total < limt);

            var idList = toPrune.Select(n => n.Id).ToArray();

            // Si la reduccion baja de  limit
            //if (2 < (parent.Children.Count - idList.Length))
            //{
            foreach (var id in idList)
            {
                parent[id].Detach();
            }

            //PrintMsg(string.Format(" RC: Nodo {0} reducido a {1} hijos", parent.Item.Nombre, parent.Children.Count));
            //}

            
        }


        private void ProcessMove(Movement move, GameTreeNode parent, GameTreeNode root) 
        {
            // Obtener gameStatus de Parent y clonar
            GameStatus cloned = CreateCloneChild(root, parent,  parent.Item);
            // Aplicar Movimiento
            cloned.Movement = MakeMove(move, cloned);
            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMove(move, cloned);
            // Chequear por Completamiento
            CheckCompleteAndDelete(cloned);
        }

        //  Auxiliares

        //  Chequear completamiento y eliminar completas
        private void CheckCompleteAndDelete(GameStatus game)
        {
            if (IsAnyCompleted(game))
            {
                ClearCompleted(game);
            }
        }

        // Crear clon de Nodetree
        private GameStatus CreateCloneChild(GameTreeNode root, GameTreeNode parent, GameStatus game)
        {
            GameStatus clonedGame = CloneGameStatus(game);

            // Reset Id
            clonedGame.Id = root.Count();

            // Reset Name
            clonedGame.Nombre = string.Format("Cloned {0}", clonedGame.Id);

            //crear
             parent.AddChild(clonedGame);

            return clonedGame;
        }

        #endregion
    }
}
