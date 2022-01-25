using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GSTestSelector
{
    

    class Program
    {
        static private GcMemTest memTest = new GcMemTest();


        static void Main(string[] args)
        {
            Stopwatch time = new Stopwatch();

            int Times = 250000;

            GameStatusListClass gsList;
            GameStatusArrayStruct gsArray;

            List<GameStatusListClass> storeList = new List<GameStatusListClass>();
            List<GameStatusArrayStruct> storeArray = new List<GameStatusArrayStruct>();
            // To compile
            Factory.CreateGameStatusListClass(1, "test");
            Factory.CreateGameStatusArrayStruct(1, "test");

            memTest.Restart();
            time.Restart();
            
            for (int i = 0; i < Times; i++)
            {
                gsList = Factory.CreateGameStatusListClass(i, i.ToString());
                storeList.Add(gsList);
            }
            time.Stop();
            Console.WriteLine("GameStatusListClass Time: " + time.Elapsed);
            Console.WriteLine("GameStatusListClass Mem: " + memTest.FinishMemTest().ToString()  );

            gsList = null;
            storeList = null;

            memTest.Restart();
            time.Restart();

            for (int i = 0; i < Times; i++)
            {
                gsArray = Factory.CreateGameStatusArrayStruct(i, i.ToString());
                storeArray.Add(gsArray);
            }

            time.Stop();
            Console.WriteLine("GameStatusArrayStruct Time: " + time.Elapsed);
            Console.WriteLine("GameStatusArrayStruct Mem: " + memTest.FinishMemTest().ToString() );

            //Console.ReadLine();
        }
    }
}
