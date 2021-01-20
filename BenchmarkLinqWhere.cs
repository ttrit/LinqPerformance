using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqPerformance
{
    [MemoryDiagnoser]
    public class BenchmarkLinqWhere
    {
        private List<int> list;

        [GlobalSetup]
        public void Global()
        {
            list = Enumerable.Repeat(new Random(42), 100000).Select(r => r.Next()).ToList();
        }

        [Benchmark(Baseline = true)]
        public void Linq()
        {
            list.Where(i => i % 5 == 0).Count();
        }

        [Benchmark]
        public void ForLoop()
        {
            if (list == null) throw new ArgumentNullException();

            var num = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] % 5 == 0)
                {
                    num++;
                }
            }
        }

        [Benchmark]
        public void ForEach()
        {
            if (list == null) throw new ArgumentNullException();

            var num = 0;

            foreach (var number in list)
            {
                if (number % 5 == 0)
                {
                    num++;
                }
            }
        }
    }
}
