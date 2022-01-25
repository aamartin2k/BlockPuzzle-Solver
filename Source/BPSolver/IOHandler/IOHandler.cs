
using BPSolver.Command;
using BPSolver.Enums;
using BPSolver.Game;
using BPSolver.Objects;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Components
        private IDocument _DocHandler { get;  set; }
        private ISolver _SolHandler { get;  set; }
        private IGame _GameHandler { get;  set; }
        private ITree _TreeHandler { get; set; }

        // Private Constructor 
        private IOHandler(IDocument docHandler, ISolver solver,
                          IGame game, ITree tree)
        {
            _DocHandler = docHandler;
            _SolHandler = solver;
            _GameHandler = game;
            _TreeHandler = tree;
        }

        // Static method as Factory
        // Builder and Binder
        public static IController CreateServer()
        {
            IDocument docHandler = new DocHandler();
            ISolver solver = new SolHandler();
            ICommandStack commandStack = new CommandStack();
            IGame game = new GameHandler(commandStack);
            ITree tree = new TreeHandler();

            IOHandler server = new IOHandler(docHandler, solver, game, tree);

            // Wiring Up
            #region Wiring Up
            #region Document
            // In
            docHandler.Out_NewFileResult = server.In_NewFileResult;
            docHandler.Out_CloseFileResult = server.In_CloseFileResult;
            docHandler.Out_LoadFileResult = server.In_LoadFileResult;
            docHandler.Out_UserEnable = server.In_UserEnableResult;
            docHandler.Out_SaveFileResult = server.In_SaveFileResult;

            // Out
            server.Out_NewFile = docHandler.In_NewFile;
            server.Out_CloseFile = docHandler.In_CloseFile;
            server.Out_LoadFile = docHandler.In_LoadFile;
            server.Out_SaveFile = docHandler.In_SaveFile;
            server.Out_SaveFileAs = docHandler.In_SaveFileAs;
            // End Document
            #endregion

            #region Tree
            // In
            tree.Out_MoveFirst_Result = server.In_MoveFirst_Result;
            tree.Out_MoveLast_Result = server.In_MoveLast_Result;
            tree.Out_MoveNext_Result = server.In_MoveNext_Result;
            tree.Out_MovePrevious_Result = server.In_MovePrevious_Result;
            tree.Out_MoveToChild_Result = server.In_MoveToChild_Result;
            tree.Out_Rename_Result = server.In_Rename_Result;

            // Out
            server.Out_MoveFirst = tree.In_MoveFirst;
            server.Out_MoveLast = tree.In_MoveLast;
            server.Out_MoveNext = tree.In_MoveNext;
            server.Out_MovePrevious = tree.In_MovePrevious;
            server.Out_MoveToChild = tree.In_MoveToChild;
            server.Out_Rename = tree.In_Rename;
            #endregion

            #region Game
            // In
            game.Out_DeleteGridCell_Result = server.In_DeleteGridCell_Result;
            game.Out_DeleteNextPiece_Result = server.In_DeleteNextPiece_Result;
            game.Out_DrawGridPlay_Result = server.In_DrawGridPlay_Result;
            game.Out_DrawNextPiece_Result = server.In_DrawNextPiece_Result;
            game.Out_Draw_Result = server.In_Draw_Result;
            game.Out_Undo_Result = server.In_Undo_Result;
            game.Out_EmptyCommandStack = server.In_EmptyCommandStack;
            // Out
            server.Out_DeleteGridCell = game.In_DeleteGridCell;
            server.Out_DeleteNextPiece = game.In_DeleteNextPiece;
            server.Out_DrawGridPlay = game.In_DrawGridPlay;
            server.Out_DrawNextPiece = game.In_DrawNextPiece;
            server.Out_Draw = game.In_Draw;
            server.Out_DrawPiece = game.In_DrawPiece;
            server.Out_Undo = game.In_Undo;
            #endregion

            #region Solver
            // In
            solver.Out_UpdateSolutionBoard = server.In_UpdateSolutionBoard;

            // Out
            server.Out_Solution = solver.In_Solution;
            server.Out_SelectIterative = solver.In_SelectIterative;
            server.Out_SelectRecursive = solver.In_SelectRecursive;
            #endregion
            #endregion

            // Forcing execution of static constructor
            var piece = PieceSet.GetPiece(PieceName.One);

            return server;
        }

    }
}
