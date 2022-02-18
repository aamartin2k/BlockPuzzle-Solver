using System;
using BPSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTestBPSolver
{
    [TestClass]
    public class DocHandlerTest
    {
        private DocHandler _docHandler;

        public DocHandlerTest()
        {
            _docHandler = new DocHandler();

            //Wiring
            _docHandler.Out_NewFileResult = In_NewFileResult;
            _docHandler.Out_CloseFileResult = In_CloseFileResult;
            _docHandler.Out_LoadFileResult = In_LoadFileResult;
            _docHandler.Out_SaveFileResult = In_SaveFileResult;
        }

        [TestMethod]
        public void CreationTest()
        {
            Assert.IsNotNull(_docHandler);
        }

        [TestMethod]
        public void In_NewFileTest()
        {
            _docHandler.In_NewFile();
            Assert.IsNotNull(_docHandler.CurrentDocument);
            Assert.IsTrue(_in_NewFileResult);
            Assert.AreEqual("Nuevo1.bmd File created OK.", _in_NewFileText);

            string fileName = string.Format("{0}{1}{2}", Constants.NewDocumentName, 1, Constants.DocumentExtension);
            Assert.AreEqual(fileName, _docHandler.CurrentFilePath);
        }

        [TestMethod]
        public void In_SaveFileAsTest()
        {
            string fileName = "New_Renamed.bmd";

            _docHandler.In_NewFile();
            _docHandler.In_SaveFileAs(fileName);

            Assert.IsNotNull(_docHandler.CurrentDocument);
            Assert.IsTrue(_in_SaveFileResult);

            string message = fileName + " File saved OK.";
            Assert.AreEqual(message, _in_SaveFileText);
            Assert.AreEqual(fileName, _docHandler.CurrentFilePath);
        }

        [TestMethod]
        public void In_CloseFileTest()
        {
            _docHandler.In_CloseFile();

            Assert.IsNull(_docHandler.CurrentDocument);
            Assert.IsTrue(_in_CloseFileResult);
            Assert.AreEqual("Document closed", _in_CloseFileText);
        }

        [TestMethod]
        public void In_LoadFileFailTest()
        {
            string fileName = "NoSuchFileName";

            _docHandler.In_LoadFile(fileName);

            string message = fileName + " File not found!";

            Assert.IsFalse(_in_LoadFileResult);
            Assert.AreEqual(message, _in_LoadFileText);
        }

        [TestMethod]
        public void In_LoadFileOkTest()
        {
            string fileName = "Conv_solver.bmd";

            _docHandler.In_LoadFile(fileName);

            string message = fileName + " File loaded OK."; ;

            Assert.IsTrue(_in_LoadFileResult);
            Assert.AreEqual(message, _in_LoadFileText);
        }



        // Receptores
        bool _in_NewFileResult;
        string _in_NewFileText;
        public void In_NewFileResult(bool result, string text)
        {
            _in_NewFileResult = result;
            _in_NewFileText = text;
        }

        bool _in_SaveFileResult;
        string _in_SaveFileText;
        public void In_SaveFileResult(bool result, string text)
        {
             _in_SaveFileResult = result;
            _in_SaveFileText = text;
        }

        bool _in_CloseFileResult;
        string _in_CloseFileText;
        public void In_CloseFileResult(bool result, string text)
        {
            _in_CloseFileResult = result;
            _in_CloseFileText = text;
        }

        bool _in_LoadFileResult;
        string _in_LoadFileText;
        public void In_LoadFileResult(bool result, string text)
        {
            _in_LoadFileResult = result;
            _in_LoadFileText = text;

        }
    }
}
