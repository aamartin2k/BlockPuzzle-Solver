using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSolver.Enums;
using BPSolver.Objects;

namespace GSTestSelector
{
    public class BaseGStatus : IGameStatusData
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
