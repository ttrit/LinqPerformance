using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqPerformance
{
    [MemoryDiagnoser]
    public class BenchmarkLinq
    {
        private List<int> list;

        [GlobalSetup]
        public void Global()
        {
            list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        [Benchmark(Baseline = true)]
        public int Linq()
        {
            return list.Last();
        }

        [Benchmark]
        public int Last()
        {
            if (list == null) throw new ArgumentNullException();

            if (list.Count > 0)
                return list[list.Count - 1];

            throw new ArgumentException();
        }

        [Benchmark]
        public int LastBetter()
        {
            var idx = list.Count;

            if (list == null || idx == 0) throw new ArgumentNullException();

            return list[idx - 1];

            throw new ArgumentException();
        }
    }
}
