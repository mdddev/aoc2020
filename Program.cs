using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Days;
using System.Linq;
using System.Drawing;

namespace AdventOfCode2020
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await _solveDay1();
            // await _solveDay2();
            // await Day3.OpenSesame();
            await Day4.OpenSesame();

        }

        static private async Task _solveDay1()
        {
            string path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Resources", "day01.txt");
            var lines = await File.ReadAllLinesAsync(path, Encoding.UTF8);
            var numbers = lines.Select(s => Convert.ToInt32(s));
            var day1 = new Day1(numbers);
            Console.WriteLine($"Result of day1-1= {day1.Part1()}");
            Console.WriteLine($"Result of day1-2= {day1.Part2()}");
        }

        static private async Task _solveDay2()
        {
            string path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Resources", "day02.txt");
            var lines = await File.ReadAllLinesAsync(path, Encoding.UTF8);
            var validations = lines.Select(s => new PasswordValidator(s));
            var valid = validations.Where(w => w.IsValid()).Count();
            var validAtNewWorkplace = validations.Where(w => w.IsValidAtNewWorkplace()).Count();

            System.Console.WriteLine($"Result of day2-1= {valid} passwords are valid");
            System.Console.WriteLine($"Result of day2-2= {validAtNewWorkplace} passwords are valid according to new workplace's rules");

        }
    }
}
