using BPSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFProt
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Preparacion
            IBPServer server = new Controller();
            Form1 form = new Form1();

            // Conexion
            // salidas de Form
            //IO
            form.Out_NewFile = server.In_NewFile;
            form.Out_CloseFile = server.In_CloseFile;
            form.Out_LoadFile = server.In_LoadFile;
            form.Out_SaveFile = server.In_SaveFile;
            //Actions
            form.Out_Solution = server.In_Solution;
            form.Out_Undo = server.In_Undo;
       
            form.Out_DrawPiece = server.In_DrawPiece;
            form.Out_DrawNextPiece = server.In_DrawNextPiece;
            form.Out_DeleteGridCell = server.In_DeleteGridCell;

            form.Out_DeleteNextPiece = server.In_DeleteNextPiece;
            form.Out_DrawGridPlay = server.In_DrawGridPlay;

            // control de secuencia
            // Browsing
            form.Out_MoveFirst = server.In_MoveFirst;
            form.Out_MovePrevious = server.In_MovePrevious;
            form.Out_MoveNext = server.In_MoveNext;
            form.Out_MoveLast = server.In_MoveLast;

            // Childs
            form.Out_AddChild = server.In_AddChild;
            form.Out_AddChildStay = server.In_AddChildStay;
            form.Out_MoveToChild = server.In_MoveToChild;
            form.Out_Rename = server.In_Rename;

            // Entradas desde de Controller
            server.Out_UpdateGameBoard = form.In_UpdateBoard;
            server.Out_UpdateSolutionBoard = form.In_UpdateSolutionBoard;

            server.Out_UserEnable = form.In_UserEnable;

            server.Out_NewFileResult = form.In_NewFileResult;
            server.Out_LoadFileResult = form.In_LoadFileResult;
            server.Out_SaveFileResult = form.In_SaveFileResult;

            server.Out_EmptyCommandStack = form.In_EmptyCommandStack;

            server.Out_SelectRows = form.In_SelectRows;
            server.Out_SelectColumns = form.In_SelectColumns;

            server.Out_MoveFirst_Result = form.In_MoveFirst_Result;
            server.Out_MovePrevious_Result = form.In_MovePrevious_Result;
            server.Out_MoveNext_Result = form.In_MoveNext_Result;
            server.Out_MoveLast_Result = form.In_MoveLast_Result;
            
            
            //ejecutar Form
            Application.Run(form);

        }
    }
}
