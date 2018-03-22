using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace Scratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            int sp = 0;
            Console.WriteLine(bytes[sp = 1]); //why is this legal
            Console.WriteLine(bytes[sp]);

        }
    }

}