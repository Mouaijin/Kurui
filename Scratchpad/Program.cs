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
            Byl[] bls = new[]
                {new Byl(BoolFlags.None), new Byl(BoolFlags.Zero), new Byl(BoolFlags.Three), new Byl(BoolFlags.Five),};
            foreach (Byl byl in bls)
            {
                Console.WriteLine($"Bool: {byl.bul}");
                Console.WriteLine($"Byte: {byl.byt}");
            }
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct Byl
    {
        [FieldOffset(0)] public byte byt;
        [FieldOffset(0)] public bool bul;

        public Byl(BoolFlags b)
        {
            bul = false;
            byt = (byte) b;
        }
    }

    enum BoolFlags
    {
        None = 0,
        Zero  = 0b0000_0001,
        One   = 0b0000_0010,
        Two   = 0b0000_0100,
        Three = 0b0000_1000,
        Four  = 0b0001_0000,
        Five  = 0b0010_0000,
        Six   = 0b0100_0000,
        Seven = 0b1000_0000
    }
}