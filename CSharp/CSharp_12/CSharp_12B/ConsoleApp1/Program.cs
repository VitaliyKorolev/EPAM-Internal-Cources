using LinqTasksReinvent;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> second = new List<int>(new[] { 40, 40, 40, 70, 80, 70 });
            var first = second.GroupBy(
               d => d,
               el => el,
               (d, el) => new
               {
                   Key = d,
                   Group = el.ToList()
               });
        }
    }
}
