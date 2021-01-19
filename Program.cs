using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinqPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            DeferredExecutionAndLazyEvaluation();

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
                                                  where n.Name.StartsWith('C')
                                                  select n;

            foreach (var student in cLetterName)
            {
                Console.WriteLine(student.Name);
            }

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
