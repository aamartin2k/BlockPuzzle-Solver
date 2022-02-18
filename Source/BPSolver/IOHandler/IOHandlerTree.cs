using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    public partial class IOHandler : IController
    {
        // Comunicacion con el Componente TreeHandler
        #region Outputs
       
        #region Declaration of Delegates


        internal Action Out_MoveFirst { get; set; }
        internal Action Out_MovePrevious { get; set; }
        internal Action Out_MoveNext { get; set; }
        internal Action Out_MoveLast { get; set; }
        internal Action<int> Out_MoveToChild { get; set; }
        internal Action<int, string> Out_Rename { get; set; }

        #endregion

        #region Invocation of Delegates
        private void OnOut_MoveFirst()
        {
            Out_MoveFirst?.Invoke();
        }
        private void OnOut_MovePrevious()
        {
            Out_MovePrevious?.Invoke();
        }
        private void OnOut_MoveNext()
        {
            Out_MoveNext?.Invoke();
        }
        private void OnOut_MoveLast()
        {
            Out_MoveLast?.Invoke();
        }
        private void OnOut_MoveToChild(int index)
        {
            Out_MoveToChild?.Invoke(index);
        }
        private void OnOut_Rename(int index, string name)
        {
            Out_Rename?.Invoke(index, name);
        }
        #endregion
        #endregion

        #region Inputs 
        internal void In_MoveFirst_Result(bool result)
        {
            OnOut_MoveFirst_Result(result);
            OnMove_UpdateGameBoard(result);
        }

        internal void In_MovePrevious_Result(bool result)
        {
            OnOut_MovePrevious_Result(result);
            OnMove_UpdateGameBoard(result);
        }
        internal void In_MoveNext_Result(bool result)
        {
            OnOut_MoveNext_Result(result);
            OnMove_UpdateGameBoard(result);
        }
        internal void In_MoveLast_Result(bool result)
        {
            OnOut_MoveLast_Result(result);
            OnMove_UpdateGameBoard(result);
        }
        internal void In_MoveToChild_Result(bool result)
        {
            OnOut_MoveToChild_Result(result);
            OnMove_UpdateGameBoard(result);
        }
        internal void In_Rename_Result(bool result)
        {
            OnOut_Rename_Result(result);
           
            // No puede responderse con update, se genera excepcion
            // por modificar coleccion en medio de iteracion
        }

        private void OnMove_UpdateGameBoard(bool ret)
        {
            if (ret)
            {
                _GameHandler.CurrentStatus = _TreeHandler.CurrentNode.Item;
                OnOut_UpdateGameBoard();
            }
        }
        #endregion
    }
}
