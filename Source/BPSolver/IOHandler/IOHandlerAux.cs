
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        private Document newDocFromOldFile(GameTreeNode bigTree)
        {
            Document newDoc = new Document();

            newDoc.DocumentData = new DocData();
            newDoc.GameData = new GameData();

            //  Creando arbol ligero serializable
            GameSerialNode dataRoot = new GameSerialNode(bigTree.Item);

            // Copiando Tree
            bigTree.CopyTo(dataRoot, (n, d) =>
            {
                d.Status = n.Item;
            });

            newDoc.GameTree = dataRoot;


            return newDoc;
        }

    }
}
