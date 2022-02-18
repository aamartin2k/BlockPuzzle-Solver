using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTestSelector
{
    public class GcMemTest
    {
        private long _kbAtExecution;

        public void Restart()
        {
            // Needed for being sure that GC will not clean some information due to test run.
            GC.Collect();

            _kbAtExecution = GC.GetTotalMemory(false) / 1024;
        }

        /// <summary>
        /// Finish memory test
        /// </summary>
        /// <returns>difference between test start and test finish in Kb</returns>
        public long FinishMemTest()
        {
            return GC.GetTotalMemory(true) / 1024 - _kbAtExecution;
        }
    }
}
