using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;

namespace Scratchpad
{
    /// <summary>
    /// Lesson learned: get/set indexer is nearly twice as fast as a ref/get indexer (at least for primitives)
    /// </summary>
    public class DataMapBenches
    {
        private const int len = 1_000_000;
        DataMapRef dataMapRef = new DataMapRef(len);

        DataMapSet dataMapSet = new DataMapSet(len);

        [Benchmark]
        public void ModifyRef()
        {
            for (int i = 0; i < len; i++)
            {
                dataMapRef[i] = i;
            }
        }
        [Benchmark]
        public void ModifySet()
        {
            for (int i = 0; i < len; i++)
            {
                dataMapSet[i] = i;
            }
        }
    }

    class DataMapRef
    {
        public int[] nums;

        public DataMapRef(int length)
        {
            this.nums = new int[length];
        }

        public ref int this[int n]
        {
            get => ref nums[n];
        }
    }
    class DataMapSet
    {
        public int[] nums;

        public DataMapSet(int length)
        {
            this.nums = new int[length];
        }

        public int this[int n]
        {
            get => nums[n];
            set => nums[n] = value;
        }
    }
}
