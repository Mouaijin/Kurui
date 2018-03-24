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
            Imm reg = new Imm(){wide = 0xFEEF};
            Console.WriteLine(reg.lo);
            ChangeInnerByte(ref reg.lo);
            Console.WriteLine(reg.lo);

        }

        private static void ChangeInnerByte(ref byte imm)
        {
            imm = 0x01;
        }
    }

}