using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class GameHandler
    {
        public GameMetaStatus CreateGameMetaStatus()
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

            return meta;
        }
    }
}
