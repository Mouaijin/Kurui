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
            byte bankIndex = 1;
            int value = 128;
           
            bankIndex = (byte)((bankIndex & 0b0011_1111) | (((byte)value) & 0b1100_0000));
            Console.WriteLine(bankIndex);
        }

    }


}
