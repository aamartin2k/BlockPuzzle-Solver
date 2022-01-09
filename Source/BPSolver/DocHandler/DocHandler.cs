using System.IO;

namespace BPSolver
{
    internal partial class DocHandler : IDocument

    {
        // Referencia al documento activo
        public Document CurrentDocument { get; private set; }

        // Ruta al documento activo
        internal string CurrentFilePath { get; set; }


        #region Utils
        internal static bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        private static int NameCount = 1;
        private static string NuevoNombre()
        {
         
            string fileName = string.Format("{0}{1}{2}", Constants.NewDocumentName, NameCount, Constants.DocumentExtension);
            NameCount++;

            return fileName;
        }

        #endregion

        #region Serial Temporal 
        /*
        // Serialize to Disk
        private void BinSerialize(string file, Document tree)
        {
            // Serializando con Binary Formatter
            Serializer.Serialize(tree, file);

        }

        private GameTreeNode BinDeserialize(string file)
        {
            GameSerialNode dataRoot = Serializer.Deserialize<GameSerialNode>(file);

            GameTreeNode newTree = new GameTreeNode(dataRoot.Status);

            newTree.Build(dataRoot, n => n.Status);

            return newTree;
        }

       */
        #endregion

    }
}
