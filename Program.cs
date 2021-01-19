using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LinqPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            //DeferredExecutionAndLazyEvaluation();

            var rnd = new Random(42);
            var array = Enumerable.Repeat(rnd, 100000).Select(r => r.Next()).ToArray();

            var benchmark = new Benchmark(array);
            Console.WriteLine($"{"Test",-20}\t{"Iterations",-20}\t{"Average",-20}\t{"Total",-20}");
            var results = benchmark.Run();
            foreach (var r in results)
            {
                Console.WriteLine($"{r.Name,-20}\t{r.Iterations,-20}\t{r.AverageTime.TotalSeconds,-20}\t{r.TotalTime.TotalSeconds,-20}");
            }

            Console.ReadKey();
        }

        static void DeferredExecutionAndLazyEvaluation()
        {
            IList<Student> names = new List<Student>()
            {
                new Student() { Name = "Nam", Age = 24 },
                new Student() { Name = "Cong", Age = 30 },
                new Student() { Name = "Chien", Age = 18 },
                new Student() { Name = "Hai", Age = 20 }
            };

            var cLetterName = from n in names
                                                  where n.Name.StartsWith("C")
                                                  select n;

            foreach (var student in cLetterName)
            {
                Console.WriteLine(student.Name);
            }

        }

        private class Benchmark
        {
            private readonly int[] _array;
            public Benchmark(int[] array)
            {
                _array = array;
            }

            public IEnumerable<TestResult> Run()
            {
                yield return Run(Select, 5000);
                yield return Run(SelectAndToArray, 5000);
                yield return Run(Where, 5000);
                yield return Run(WhereAndToArray, 5000);
                yield return Run(OrderBy, 500);
                yield return Run(OrderByAndToArray, 500);
            }

            private TestResult Run(Action action, int iterations)
            {
                // JIT warmup
                action();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                var stopWatch = Stopwatch.StartNew();
                for (int i = 0; i < iterations; i++)
                {
                    action();
                }
                stopWatch.Stop();
                return new TestResult(action.GetMethodInfo().Name, iterations, stopWatch.Elapsed);
            }

            private void Select()
            {
                _array.Select(i => i * i).Count();
            }

            private void SelectAndToArray()
            {
                _array.Select(i => i * i).ToArray();
            }

            private void Where()
            {
                _array.Where(i => i % 5 == 0).Count();
            }

            private void WhereAndToArray()
            {
                _array.Where(i => i % 5 == 0).ToArray();
            }

            private void OrderBy()
            {
                _array.OrderBy(i => i).Count();
            }

            private void OrderByAndToArray()
            {
                _array.OrderBy(i => i).ToArray();
            }
        }

        private class TestResult
        {
            public TestResult(string name, int iterations, TimeSpan totalTime)
            {
                Name = name;
                Iterations = iterations;
                TotalTime = totalTime;
                AverageTime = TimeSpan.FromTicks(totalTime.Ticks / iterations);
            }

            public string Name { get; }
            public int Iterations { get; }
            public TimeSpan TotalTime { get; }
            public TimeSpan AverageTime { get; }
        }

        public static long Measure(Action a)
        {
            //
            // Quick Wormup.
            //
            Console.WriteLine(" [1] Warm Up  ... ");

            for (int i = 0; i < 10; i++)
            {
                a();
            }

            double avg = 0;

            Console.WriteLine(" [2] Running ... ");

            for (int i = 0; i < 10; i++)
            {
                Stopwatch w = new Stopwatch();
                w.Start();
                {
                    a();
                }
                w.Stop();

                avg += w.ElapsedMilliseconds;
                Console.WriteLine($" [3] Took: {w.ElapsedMilliseconds} ms");
            }

            Console.WriteLine($" [4] AVG: {avg / 10} ms");

            return 0;
        }
    }
}
