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
            form.Out_NewFile = server.In_NewFile;
            form.Out_CloseFile = server.In_CloseFile;
            form.Out_LoadFile = server.In_LoadFile;
            form.Out_SaveFile = server.In_SaveFile;
            form.Out_PutPiece = server.In_PutPiece;
            form.Out_DeleteCell = server.In_DeleteCell;
            form.Out_SetNextPiece = server.In_SetNextPiece;
            form.Out_Undo = server.In_Undo;

            // salidas de Controller
            server.Out_UpdateBoard = form.In_UpdateBoard;
            server.Out_UserEnable = form.In_UserEnable;

            server.Out_NewFileResult = form.In_NewFileResult;
            server.Out_LoadFileResult = form.In_LoadFileResult;
            server.Out_SaveFileResult = form.In_SaveFileResult;

            server.Out_EmptyCommandStack = form.In_EmptyCommandStack;

            server.Out_SelectRows = form.In_SelectRows;
            server.Out_SelectColumns = form.In_SelectColumns;

            //ejecutar Form

            Application.Run(form);

        }
    }
}
