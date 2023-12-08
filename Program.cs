

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using aoc2023;

namespace aoc2022;
class Program
{
    static void Main(string[] args)
    {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            
            var c1= new D8();
            int [] i =c1.Solution();
            Console.WriteLine("The Result for Part 1 is " + i[0] + " The Result for Part 2 is " + i[1]);

            
            sw.Stop();

            Console.WriteLine("Elapsed={0}",sw.Elapsed.TotalMilliseconds);
    }
}