using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeCollections;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Comunicacion con el cliente
        // para funciones de DocHandler
        //
        #region Entradas de Cliente
        public void In_NewFile()
        {
            // Llamada a Document
            OnOut_NewFile();
        }
        public void In_CloseFile()
        {
            // Llamada a Document
            OnOut_CloseFile();
        }
        public void In_LoadFile(string file)
        {
            // Llamada a Document
            OnOut_LoadFile(file);
            // TEMP Cargando old files
            //In_LoadOldFile(file);
        }
        /*
        private void In_LoadOldFile(string file)
        {
            string message;
            bool ret;

            try
            {
                GameSerialNode dataRoot = Serializer.Deserialize<GameSerialNode>(file);
                //GameTreeNode newTree = new GameTreeNode(dataRoot.Status);
                //newTree.Build(dataRoot, n => n.Status);

                // process Doc
                Document newDoc = new Document();
                newDoc.GameTree = dataRoot;
                // act DocHandler
                //DocHandler.CurrentDocument = newDoc;

                ret = true;
                message = file + " File loaded OK";

            }
            catch (Exception ex)
            {
                //notify error
                ret = false;
                message = ex.Message;
            }

            Out_LoadFileResult(ret, message);
        }
        */
        public void In_SaveFile()
        {
            UpdateDocumentFromGameTree();
            OnOut_SaveFile();
        }
        public void In_SaveFileAs(string file)
        {
            UpdateDocumentFromGameTree();
            OnOut_SaveFileAs(file);
        }

        #endregion

        #region Salidas a Cliente
        #region Declaracion de Delegates
        public Action<bool> Out_UserEnable { get; set; }
        public Action<bool, string> Out_NewFileResult { get; set; }
        public Action<bool, string> Out_CloseFileResult { get; set; }
        public Action<bool, string> Out_LoadFileResult { get; set; }
        public Action<bool, string> Out_SaveFileResult { get; set; }
        #endregion

        #region Invocacion de Delegates
        public void OnOut_UserEnable(bool result)
        {
            Out_UserEnable?.Invoke(result);
        }
        public void OnOut_NewFileResult(bool result, string text)
        {
            Out_NewFileResult?.Invoke(result, text);
        }
        public void OnOut_CloseFileResult(bool result, string text)
        {
            Out_CloseFileResult?.Invoke(result, text);
        }

        public void OnOut_LoadFileResult(bool result, string text)
        {
            Out_LoadFileResult?.Invoke(result, text);
        }
        public void OnOut_SaveFileResult(bool result, string text)
        {
            Out_SaveFileResult?.Invoke(result, text);
        }

        #endregion
        #endregion

       

    }


}
