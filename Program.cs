using System;
using System.Threading.Tasks;
using AdventOfCode2020.Days;

namespace AdventOfCode2020
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var resultOfDay1Part1 = new Day1().Part1();
            Console.WriteLine($"Result of day1-1= {resultOfDay1Part1}");

            var resultOfDay1Part2 = new Day1().Part2();
            Console.WriteLine($"Result of day1-2= {resultOfDay1Part2}");

        }
    }
}
