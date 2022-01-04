namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Componentes
        

        //Acceso a Componentes
        private IDocument _DocHandler { get;  set; }
        private ISolver _SolHandler { get;  set; }
        private IGame _GameHandler { get;  set; }
        private ITree _TreeHandler { get; set; }


        // Constructor privado
        private IOHandler(IDocument docHandler, ISolver solver,
                          IGame game, ITree tree)
        {
            _DocHandler = docHandler;
            _SolHandler = solver;
            _GameHandler = game;
            _TreeHandler = tree;
        }

        // Metodo estatico Factory
        public static IController CreateServer()
        {
            IDocument docHandler = new DocHandler();
            ISolver solver = new SolHandler();
            IGame game = new GameHandler();
            ITree tree = new TreeHandler();

         
            IOHandler cont = new IOHandler(docHandler, solver, game, tree);

            // Wiring Up
            #region Wiring Up
            #region Document
            // In
            docHandler.Out_NewFileResult = cont.In_NewFileResult;
            docHandler.Out_CloseFileResult = cont.In_CloseFileResult;
            docHandler.Out_LoadFileResult = cont.In_LoadFileResult;
            docHandler.Out_UserEnable = cont.In_UserEnableResult;
            docHandler.Out_SaveFileResult = cont.In_SaveFileResult;

            // Out
            cont.Out_NewFile = docHandler.In_NewFile;
            cont.Out_CloseFile = docHandler.In_CloseFile;
            cont.Out_LoadFile = docHandler.In_LoadFile;

            cont.Out_SaveFile = docHandler.In_SaveFile;
            cont.Out_SaveFileAs = docHandler.In_SaveFileAs;
            // End Document
            #endregion

            #region Tree
            // In
            tree.Out_MoveFirst_Result = cont.In_MoveFirst_Result;
            tree.Out_MoveLast_Result = cont.In_MoveLast_Result;
            tree.Out_MoveNext_Result = cont.In_MoveNext_Result;
            tree.Out_MovePrevious_Result = cont.In_MovePrevious_Result;
            tree.Out_MoveToChild_Result = cont.In_MoveToChild_Result;
            tree.Out_Rename_Result = cont.In_Rename_Result;

            // Out
            cont.Out_MoveFirst = tree.In_MoveFirst;
            cont.Out_MoveLast = tree.In_MoveLast;
            cont.Out_MoveNext = tree.In_MoveNext;
            cont.Out_MovePrevious = tree.In_MovePrevious;
            cont.Out_MoveToChild = tree.In_MoveToChild;
            cont.Out_Rename = tree.In_Rename;
            #endregion

            #region Game
            // In
            game.Out_DeleteGridCell_Result = cont.In_DeleteGridCell_Result;
            game.Out_DeleteNextPiece_Result = cont.In_DeleteNextPiece_Result;
            game.Out_DrawGridPlay_Result = cont.In_DrawGridPlay_Result;
            game.Out_DrawNextPiece_Result = cont.In_DrawNextPiece_Result;
            game.Out_DrawPiece_Result = cont.In_DrawPiece_Result;
            game.Out_Undo_Result = cont.In_Undo_Result;

            // Out
            cont.Out_DeleteGridCell = game.In_DeleteGridCell;
            cont.Out_DeleteNextPiece = game.In_DeleteNextPiece;
            cont.Out_DrawGridPlay = game.In_DrawGridPlay;
            cont.Out_DrawNextPiece = game.In_DrawNextPiece;
            cont.Out_DrawPiece = game.In_DrawPiece;
            cont.Out_Undo = game.In_Undo;
            #endregion

            #region Solver
            // In
            solver.Out_UpdateSolutionBoard = cont.In_UpdateSolutionBoard;

            // Out
            #endregion
            cont.Out_Solution = solver.In_Solution;
            #endregion


            return cont;
        }

    }
}
