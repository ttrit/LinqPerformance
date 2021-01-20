using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace LinqPerformance
{
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net461, baseline: true)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.NetCoreApp50)]
    [MemoryDiagnoser]
    public class BenchmarkLinqRuntime
    {
        private int[] _array;

        [GlobalSetup]
        public void Global()
        {
            _array = Enumerable.Repeat(new Random(42), 100000).Select(r => r.Next()).ToArray();
        }

        [Benchmark]
        public void Select()
        {
            _array.Select(i => i * i).Count();
        }

        [Benchmark]
        public void SelectAndToArray()
        {
            _array.Select(i => i * i).ToArray();
        }

        [Benchmark]
        public void Where()
        {
            _array.Where(i => i % 5 == 0).Count();
        }

        [Benchmark]
        public void WhereAndToArray()
        {
            _array.Where(i => i % 5 == 0).ToArray();
        }

        [Benchmark]
        public void OrderBy()
        {
            _array.OrderBy(i => i).Count();
        }

        [Benchmark]
        public void OrderByAndToArray()
        {
            _array.OrderBy(i => i).ToArray();
        }
    }
}
