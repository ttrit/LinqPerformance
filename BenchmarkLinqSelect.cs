using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqPerformance
{
    [MemoryDiagnoser]
    public class BenchmarkLinqSelect
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
            list.Select(i => i * i);
        }

        [Benchmark]
        public void ForLoop()
        {
            if (list == null) throw new ArgumentNullException();

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = i * i;
            }
        }

        [Benchmark]
        public void ForEach()
        {
            if (list == null) throw new ArgumentNullException();

            var currentIndex = 0;

            foreach (var number in list)
            {
                list[currentIndex] = currentIndex * currentIndex;
                currentIndex++;
            }
        }
    }
}
