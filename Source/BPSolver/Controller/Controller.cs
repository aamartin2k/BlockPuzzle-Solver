using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Objects;
using BPSolver.Solver;
using System;
using System.Collections.Generic;

namespace BPSolver
{
    public partial class Controller : IBPServer
    {
        
        // Salidas
        public Action<GameMetaStatus> Out_UpdateGameBoard { get; set; }
        public Action<SolutionMetaStatus> Out_UpdateSolutionBoard { get; set; }

        public Action<bool> Out_UserEnable { get; set; }
        public Action<bool> Out_NewFileResult { get; set; }
        public Action<bool, string> Out_LoadFileResult { get; set; }
        public Action<bool, string> Out_SaveFileResult { get; set; }
        public Action<bool> Out_EmptyCommandStack { get; set; }
        public Action<int[]> Out_SelectRows { get; set; }
        public Action<int[]> Out_SelectColumns { get; set; }

        public Action<bool> Out_MoveFirst_Result { get; set; }
        public Action<bool> Out_MovePrevious_Result { get; set; }
        public Action<bool> Out_MoveNext_Result { get; set; }
        public Action<bool> Out_MoveLast_Result { get; set; }

        // Out_UpdateBoard On_Out_UpdateBoard
        private void On_Out_UpdateBoard()
        {
            // creando inf simple sobre el tree
            GameSimpleNode dataRoot = new GameSimpleNode(_treeRoot.Item.Id, _treeRoot.Item.Nombre);

            // Copiando Tree
            _treeRoot.CopyTo(dataRoot, (n, d) =>
                        {
                            d.Id = n.Item.Id;
                            d.Nombre = n.Item.Nombre;
                        });
            //

            // new GameMetaStatus
            GameMetaStatus meta = new GameMetaStatus(CurrentStatus,
                                                     CurrentChilds,
                                                     CurrentIsIsLeaf,
                                                     dataRoot);
            // create update Stats
            Out_UpdateGameBoard(meta);
        }
    }
}
