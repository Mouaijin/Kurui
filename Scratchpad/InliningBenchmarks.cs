using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Scratchpad
{
    //I have no clear evidence that inlining these little bit-screwy methods helps, but it makes me feel fuzzy inside anyway
    public class InliningBenchmarks
    {
        private const int iter = 10_000;
        public int variable = 0;
        [Benchmark]
        public void WithCall()
        {
            for (int x = 0; x < iter; x++)
            {
                for (int y = iter; y > 0; y--)
                {
                    CallBitStuff(x,y);
                }
            }
        }
        [Benchmark]
        public void WithInline()
        {
            for (int x = 0; x < iter; x++)
            {
                for (int y = iter; y > 0; y--)
                {
                    CallBitStuff(x, y);
                }
            }
        }


        void CallBitStuff(int num, int val)
        {
            int xor = num ^ val;
            if (xor > num)
            {
                variable = xor;
            }

            if (xor < num)
            {
                variable = num;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void InlineBitStuff(int num, int val)
        {
            int xor = num ^ val;
            if (xor > num)
            {
                variable = xor;
            }

            if (xor < num)
            {
                variable = num;
            }
        }
    }
}
