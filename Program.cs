using System;
using System.Collections.Generic;
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
    }
}
