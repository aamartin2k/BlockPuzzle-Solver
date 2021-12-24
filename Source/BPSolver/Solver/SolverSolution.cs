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

        public SolutionMetaStatus CreateSolution(GameStatus game)
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
                    TotalEval.PieceSize += game.Evaluation.PieceSize;
                    TotalEval.Preference += game.Evaluation.Preference;
                    TotalEval.Neighbors += game.Evaluation.Neighbors;
                    TotalEval.CompleteRoC += game.Evaluation.CompleteRoC;
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

            ProccessNode(treeRoot, treeRoot);

            return treeRoot;
        }

        private void ProccessNode (GameTreeNode parent, GameTreeNode root)
        {
            // Obtener GameStatus
            GameStatus gstatus = parent.Item;

            // Para cada Pieza 
            foreach (var dkv in gstatus.NextPieces)
            {
                int index = dkv.Key;
                PieceName piece = dkv.Value;

                //Generar lista de  movidas
                List<Movement> lmm = CreateMovements(index, piece, gstatus);

                // Para cada Movida
                foreach (var move in lmm)
                {
                    //Procesar Movida (move, NodoParent, ListNodeTemp)
                    ProcessMove(move, parent, root);
                }

            }

            //Para cada hijo de NodoParent
            foreach (var child in parent.Children)
            {
                ProccessNode(child, root);
            } 
           

        }

        // Anidadas
        //Procesar Movida (move, NodoParent, ListNodeTemp)
        private void ProcessMove(Movement move, GameTreeNode parent, GameTreeNode root) 
        {
            // Obtener gameStatus de Parent y clonar
            GameStatus cloned = CreateCloneChild(root, parent,  parent.Item);
            // Aplicar Movimiento
            cloned.Movement = MakeMove(move, cloned);
            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMove(move, cloned);
            
        }





        //  Auxiliares
        

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
    }
}
