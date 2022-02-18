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
        #region Inputs de Cliente
        public void In_MoveFirst()
        {
            OnOut_MoveFirst();
        }

        public void In_MoveLast()
        {
            OnOut_MoveLast();
        }

        public void In_MoveNext()
        {
            OnOut_MoveNext();
        }

        public void In_MovePrevious()
        {
            OnOut_MovePrevious();
        }

        public void In_MoveToChild(int id)
        {
            OnOut_MoveToChild(id);
        }

        public void In_Rename(int id, string name)
        {
            OnOut_Rename(id, name);
        }

        #endregion

        #region Salidas a cliente
       
        #region Declaration of Delegates
        public Action<bool> Out_MoveFirst_Result { get; set; }
        public Action<bool> Out_MovePrevious_Result { get; set; }
        public Action<bool> Out_MoveNext_Result { get; set; }
        public Action<bool> Out_MoveLast_Result { get; set; }
        public Action<bool> Out_MoveToChild_Result { get; set; }
        public Action<bool> Out_Rename_Result { get; set; }
        #endregion

        #region Invocation of Delegates
        public void OnOut_MoveFirst_Result(bool result)
        {
            Out_MoveFirst_Result?.Invoke(result);
        }
        public void OnOut_MovePrevious_Result(bool result)
        {
            Out_MovePrevious_Result?.Invoke(result);
        }
        public void OnOut_MoveNext_Result(bool result)
        {
            Out_MoveNext_Result?.Invoke(result);
        }
        public void OnOut_MoveLast_Result(bool result)
        {
            Out_MoveLast_Result?.Invoke(result);
        }

        public void OnOut_MoveToChild_Result(bool result)
        {
            Out_MoveToChild_Result?.Invoke(result);
        }

        public void OnOut_Rename_Result(bool result)
        {
            Out_Rename_Result?.Invoke(result);
        }
        #endregion

        #endregion

    }
}
