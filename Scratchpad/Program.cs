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
            byte a = 0xff;
            byte b = 0x09;
            byte c =(byte) (a + b);
            Console.WriteLine(c);

        }

        private static void ChangeInnerByte(ref byte imm)
        {
            imm = 0x01;
        }
    }

}