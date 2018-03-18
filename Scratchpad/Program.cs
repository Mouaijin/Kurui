using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace Scratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<DataMapBenches>();
            DataMapRef dm = new DataMapRef(1000);
            Console.ReadKey();
            for (int i = 0; i < 1000; i++)
            {
                dm[i] = i;
            }
        }

    }


}
