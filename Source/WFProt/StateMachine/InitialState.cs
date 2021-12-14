using System;
using BPSolver.Enums;
using BPSolver.Objects;

namespace WFProt
{
    class InitialState : BaseState, IGuiState
    {

 
        public InitialState(StContext context): base(context)
        {
            Console.WriteLine("InitialState created");
        }







    }

}
