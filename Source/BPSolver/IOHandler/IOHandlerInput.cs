using BPSolver.Enums;
using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class IOHandler : IBPServer
    {


        public void In_AddChild(GameStatus child)
        {
            //
        }

        public void In_AddChildStay(GameStatus child)
        {
            //
        }

        public void In_CloseFile()
        {
            //
        }

        public void In_DeleteGridCell(Coord coord)
        {
            //
        }

        public void In_DeleteNextPiece(int index)
        {
            //
        }

        public void In_DrawGridPlay(Coord coord, PieceName name, int index)
        {
            //
        }

        public void In_DrawNextPiece(int index, PieceName name)
        {
            //
        }

        public void In_DrawPiece(Coord coord, PieceName name)
        {
            //
        }

        public void In_LoadFile(string file)
        {
            In_LoadOldFile(file);
            return;
            //
            bool ret;
            Document newDoc;
            string message;

            ret = DocHandler.In_LoadFile(file, out newDoc, out message);

            if (ret)
            {
                // process Doc
                CurrentDocument = newDoc;

                // notify ok

            }
            else
            {
                //notify error
                
            }

            Out_LoadFileResult(ret, message);
        }

        public void In_LoadOldFile(string file)
        {
            //
            bool ret;
            GameTreeNode newDoc;
            string message;

            ret = DocHandler.In_LoadOldFile(file, out newDoc, out message);

            if (ret)
            {
                // process Doc
                GameHandler.TreeRoot = newDoc;
                // notify ok
                OnOut_LoadFileResult(true, file);
                OnOut_UserEnable(true);
                OnOut_UpdateGameBoard(GameHandler.CreateGameMetaStatus());

                // new command stack
                //ResetCommandStack();

            }
            else
            {
                //notify error

            }

            Out_LoadFileResult(ret, message);
        }

        public void In_MoveFirst()
        {
            //
        }

        public void In_MoveLast()
        {
            //
        }

        public void In_MoveNext()
        {
            //
        }

        public void In_MovePrevious()
        {
            //
        }

        public void In_MoveToChild(int id)
        {
            //
        }

        public void In_NewFile()
        {
            //
        }

        public void In_Rename(int id, string name)
        {
            //
        }

        public void In_SaveFile(string file)
        {
            bool ret;
            string message;


            if (file == null)
            {
                // imp temp para cambio de formato
                Document newDoc = newDocFromOldFile(GameHandler.TreeRoot);
                ret = DocHandler.In_SaveFile(newDoc, out message);

                //save with current name
                // ret = DocHandler.In_SaveFile(CurrentDocument, out message);
            }
            else
            {
                //save with new name
                ret = DocHandler.In_SaveAsFile(file, CurrentDocument, out message);
            }

            OnOut_LoadFileResult(ret, message);
        }

        public void In_Solution()
        {
            //
        }

        public void In_Undo()
        {
            //
        }
    }
}
