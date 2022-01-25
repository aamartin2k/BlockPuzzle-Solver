using System.IO;

namespace BPSolver
{
    /// <summary>
    /// Implement handling game and solution data as a document.
    /// </summary>
    internal partial class DocHandler : IDocument

    {
        // Current document reference.
        public Document CurrentDocument { get; private set; }

        // Current document file path.
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

        

    }
}
