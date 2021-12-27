using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPSolver.Solver
{
    public partial class Solver
    {

        #region Solver Paso a paso por niveles

        public GameTreeNode CreateSolutionTreePPaso(GameStatus game)
        {
            //Crear SolTree y RootNode con copia de GStIni
            GameTreeNode treeRoot;

            // Clon de Estado inicial para no modificarlo
            GameStatus GStIni = CloneGameStatus(game);
            GStIni.Nombre = RootName;

            // Solution Tree Root
            treeRoot = new GameTreeNode(GStIni);

            DMsg("Iniciando proceso");

            //Llamada a proceso basico por paso
            List<GameTreeNode> padres = new List<GameTreeNode>();
            padres.Add(treeRoot);

            DMsg(string.Format("Total de hijos de Root {0}.", treeRoot.Children.Count));
            padres = ProccessNodeP(padres, treeRoot);

            DMsg(string.Format("Total de hijos de Root {0}.", treeRoot.Children.Count));
            padres = ProccessNodeP(padres, treeRoot);

            DMsg(string.Format("Total de hijos de Root {0}.", treeRoot.Children.Count));
            padres = ProccessNodeP(padres, treeRoot);

            return treeRoot;
        }

        private List<GameTreeNode> ProccessNodeP(List<GameTreeNode> padres, GameTreeNode root)
        {
            int index;
            PieceName piece;
            List<GameTreeNode> hijos = new List<GameTreeNode>();

            foreach (var parent in padres)
            {

                List<Movement> lmm = new List<Movement>();

                GameStatus gstatus = parent.Item;

                DMsg(string.Format("Procesando {0}.", gstatus.Nombre));
                DMsg(string.Format("Procesando {0} piezas.", gstatus.NextPieces.Count));
                foreach (var dkv in gstatus.NextPieces)
                {
                    index = dkv.Key;
                    piece = dkv.Value;

                    //Generar lista de  movidas
                    lmm.AddRange(CreateMovements(index, piece, gstatus));
                }
                DMsg(string.Format("Procesando {0} movidas.", lmm.Count));

                foreach (var move in lmm)
                {
                    ProcessMove(move, parent, root);
                }

                // Reduciendo hijos
                //if (parent.Children.Count > 30)  //50
                //    ReduceChildren(parent);

                DMsg(string.Format("Total de hijos de Root {0}.", root.Children.Count));

                // Procesando hijos
                hijos.AddRange( parent.Children);
            }

            return hijos;

        }


   
        private void ProccessNodePX(GameTreeNode parent)
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
    }
}
