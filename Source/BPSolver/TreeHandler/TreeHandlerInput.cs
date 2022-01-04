using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    internal partial class TreeHandler : ITree
    {

        public void In_MoveFirst()
        {
            bool ret;
            if (!CurrentNode.IsRoot)
            {
                CurrentNode = _treeRoot;
                ret = true;
            }
            else
            {
                ret = false;
            }
            OnOut_MoveFirst_Result(ret);
        }

        public void In_MoveLast()
        {
            bool ret = false;

            while (CurrentNode.Children.Count == 1)
            {
                CurrentNode = CurrentNode.Children[0];
                ret = true;
            }

            OnOut_MoveLast_Result( ret);
        }

        public void In_MoveNext()
        {
            bool ret;
            if (CurrentNode.Children.Count == 1)
            {
                CurrentNode = CurrentNode.Children[0];
                ret = true;
            }
            else
            {
                ret = false;
            }

            OnOut_MoveNext_Result(ret);
        }

        public void In_MovePrevious()
        {
            bool ret;
            if (!CurrentNode.IsRoot)
            { 
                CurrentNode = CurrentNode.Parent;
                ret = true;
            }
            else
            {
                ret = false;
            }

            OnOut_MovePrevious_Result(ret);
        }

        public void In_MoveToChild(int id)
        {
            bool ret;

            if (id != CurrentNode.Item.Id)
            {
                CurrentNode = TreeRoot[id];
                ret = true;
            }
            else
            {
                ret = false;
            }

            OnOut_MoveToChild_Result(ret);
        }

        public void In_Rename(int id, string name)
        {
            TreeRoot[id].Item.Nombre = name;
            OnOut_Rename_Result(true);
        }

    }
}
