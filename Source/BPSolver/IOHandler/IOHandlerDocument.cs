
using BPSolver.Objects;
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
        // Comunicacion con el Componente DocHandler

        #region Salidas 
        #region Declaracion de Delegates
        internal Action Out_NewFile { get; set; }
        internal Action Out_CloseFile { get; set; }
        internal Action<string> Out_LoadFile { get; set; }
        internal Action Out_SaveFile { get; set; }
        internal Action<string> Out_SaveFileAs { get; set; }
        #endregion

        #region Invocacion de Delegates
        private void OnOut_NewFile()
        {
            Out_NewFile?.Invoke();
        }

        private void OnOut_CloseFile()
        {
            Out_CloseFile?.Invoke();
        }

        private void OnOut_LoadFile(string file)
        {
            Out_LoadFile?.Invoke(file);
        }
        private void OnOut_SaveFile()
        {
            Out_SaveFile?.Invoke();
        }
        private void OnOut_SaveFileAs(string file)
        {
            Out_SaveFileAs?.Invoke(file);
        }
        #endregion
        #endregion

        #region Entradas de Document
        internal void In_UserEnableResult(bool result)
        {
            OnOut_UserEnable(result);
        }
        internal void In_NewFileResult(bool result, string text)
        {
            if (result)
            {
                // Crear nuevo Game Status y Game Tree
                UpdateGameTreeFromNewObject();
              
                // Actualizar GUI con Game Status
                OnOut_UpdateGameBoard();
            }
            // Invocar salida a cliente
            // Resultado de la operacion
            OnOut_NewFileResult(result, text);
            // Habilitar GUI
            OnOut_UserEnable(result);
            // Notificar No Undo (Undo Stack vacio)
        }
        
        internal void In_CloseFileResult(bool result, string text)
        {
            
            // Invocar salida a cliente
            OnOut_CloseFileResult(result, text);
            OnOut_UserEnable(!result);
        }

        internal void In_LoadFileResult(bool result, string text)
        {
            if (result)
            {
                UpdateGameTreeFromDocument();
                // Actualizar GUI con Game Status
                OnOut_UpdateGameBoard();
            }

            OnOut_LoadFileResult(result, text);
            OnOut_UserEnable(result);
        }

        internal void In_SaveFileResult(bool result, string text)
        {
            OnOut_SaveFileResult(result, text);
        }
        #endregion

        #region Operaciones
        private void UpdateGameTreeFromNewObject()
        {
            GameStatus rootGame;
            rootGame = Factory.CreateRootGameStatus();

            // actualizando Arbol de juego
            _TreeHandler.CreateRootNode(rootGame);
            // actualizando estado de juego
            _GameHandler.CurrentStatus = rootGame;
        }

        private void UpdateDocumentFromGameTree()
        {
            GameSerialNode sern;

            sern = CreateSerialTreeFromRoot(_TreeHandler.TreeRoot);

            _DocHandler.CurrentDocument.GameTree = sern;
        }

        private void UpdateGameTreeFromDocument()
        {
            // Para LoadFile
            // Actualizar GameHandler y TreeHandler 
            // crear GameTree de SimpleTree
            // 
            GameSerialNode sern = _DocHandler.CurrentDocument.GameTree;
            GameTreeNode trn = CreateRootTreeFromSerial(sern);

            // actualizando Arbol de juego
            _TreeHandler.TreeRoot = trn;
            // actualizando estado de juego
            _GameHandler.CurrentStatus = trn.Item;

        }

        // Para enviar Info a GUI
        private GameSimpleNode CreateSimpleTreeFromRoot(GameTreeNode treeRoot)
        {
            GameSimpleNode dataRoot = new GameSimpleNode(treeRoot.Item.Id, treeRoot.Item.Nombre);

            // Copiando Tree
            treeRoot.CopyTo(dataRoot, (n, d) =>
            {
                d.Id = n.Item.Id;
                d.Nombre = n.Item.Nombre;
            });

            return dataRoot;
        }

        // Para Salvar
        private GameSerialNode CreateSerialTreeFromRoot(GameTreeNode treeRoot)
        {
            // Creando arbol ligero serializable
            // a partir de GameTreeNode tree
            GameSerialNode dataRoot = new GameSerialNode(treeRoot.Item);

            // Copiando Tree
            treeRoot.CopyTo(dataRoot, (n, d) =>
            {
                d.Status = n.Item;
            });

            return dataRoot;
        }

        // Para Cargar
        
        private GameTreeNode CreateRootTreeFromSerial(GameSerialNode dataRoot)
        {
            GameTreeNode newTree = new GameTreeNode(dataRoot.Status);
            newTree.Build(dataRoot, n => n.Status);

            return newTree;
        }
    
        #endregion

    }
}
