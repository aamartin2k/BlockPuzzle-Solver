using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPSolver
{
    [Serializable]
    public class DocData
    {
        public string Description { get; set; }


        //default values
        public DocData()
        {
            Description = "Description";
        }
    }
}
