using BPSolver.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    /// <summary>
    /// Implement handling .
    /// </summary>
    internal partial class TreeHandler : ITree
    {

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

    }
}
