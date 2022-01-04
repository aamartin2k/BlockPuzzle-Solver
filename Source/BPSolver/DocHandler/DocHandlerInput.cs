using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class DocHandler : IDocument
    {
        #region Entradas de Controller

       
        public void In_NewFile()
        {
            bool ret;
            string message;

            try
            {
                // crear nuevo Document
                Document newDoc = new Document();

                // guardar ref
                CurrentDocument = newDoc;
                CurrentFilePath = DocHandler.NuevoNombre();
                ret = true;
                message = CurrentFilePath + " File created OK.";
            }
            catch (Exception ex)
            {
                // Fallar en caso de Exception
                //throw;
                ret = false;
                message = ex.Message;
            }

            // notificar exito
            OnOut_NewFileResult(ret, message);
        }

        public void In_CloseFile()
        {
            CurrentDocument = null;
            CurrentFilePath = null;

            OnOut_CloseFileResult(true, "Document closed");
        }

        public void In_LoadFile(string file)
        {
            bool ret;
            string message;

            try
            {

                if (FileExists(file))
                {
                    Document document = Serializer.Deserialize<Document>(file);
                    ret = true;
                    message = file + " File loaded OK.";
                    CurrentFilePath = file;
                    CurrentDocument = document;
                }
                else
                {
                    ret = false;
                    message = file + " File not found!";

                }
            }
            catch (Exception ex)
            {

                ret = false;
                // log
                //throw;
                message = ex.Message;
            }

            // Notify Result
            OnOut_LoadFileResult(ret, message);
        
        }

        public void In_SaveFile()
        {
            In_SaveFileAs(CurrentFilePath);
        }
        public void In_SaveFileAs(string file)
        {
            bool ret;
            string message;

            try
            {
                Serializer.Serialize(CurrentDocument, file);
                ret = true;
                CurrentFilePath = file;
                message = file + " File saved OK.";
                
            }
            catch (Exception ex)
            {

                ret = false;
                // log
                //throw;
                message = ex.Message;
            }

            OnOut_SaveFileResult(ret, message);
        }


        #endregion
    }
}
