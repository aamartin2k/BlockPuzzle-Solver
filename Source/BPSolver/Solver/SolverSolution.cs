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
        private GameTreeNode _treeRoot;


        public GameTreeNode CreateSolutionTree(GameStatus game)
        {
            //Crear SolTree y RootNode con copia de GStIni
            //Añadir RootNode a ListNodeP.

            // Clon de Estado inicial para no modificarlo
            GameStatus GStIni = CloneGameStatus(game);
            GStIni.Nombre = "Cloned Root";

            // Solution Tree
            _treeRoot = new GameTreeNode(GStIni);

            ProccessNode(_treeRoot);

            return _treeRoot;
        }


        private void ProccessNode (GameTreeNode parent)
        {
            //Console.WriteLine("Inicio Proccess Node");
            
            // Obtener GameStatus
            GameStatus gstatus = parent.Item;
            //Console.WriteLine(" Gstatus " + gstatus.Nombre);

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
                    ProcessMove(move, parent);
                }

            }

            //Para cada hijo de NodoParent
            foreach (var child in parent.Children)
            {
                ProccessNode(child);
            } 
           

        }

        // Anidadas
        //Procesar Movida (move, NodoParent, ListNodeTemp)
        private void ProcessMove(Movement move, GameTreeNode parent) //, List<GameTreeNode> listNodeTemp)
        {
            // Obtener gameStatus de Parent y clonar
            GameStatus cloned = CreateCloneChild(parent,  parent.Item);
            // Aplicar Movimiento
            cloned.Movement = MakeMove(move, cloned);
            // Evaluar Movimiento
            cloned.Evaluation = EvaluateMove(move, cloned);
            
        }





        //  Auxiliares
        private int Count()
        {
            return _treeRoot.Count();
        }

        // Crear clon de Nodetree
        private GameStatus CreateCloneChild(GameTreeNode parent, GameStatus game)
        {
            GameStatus clonedGame = CloneGameStatus(game);

            // Reset Id
            clonedGame.Id = Count();

            // Reset Name
            clonedGame.Nombre = string.Format("Cloned {0}", clonedGame.Id);

            //crear
             parent.AddChild(clonedGame);

            return clonedGame;
        }
    }
}
