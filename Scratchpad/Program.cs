using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= 0xFF; i++)
            {
                sb.AppendLine($"case 0x{i:X2}: {{}}; break;");
            }
            File.WriteAllText("gen.txt", sb.ToString());
        }

        
    }

}