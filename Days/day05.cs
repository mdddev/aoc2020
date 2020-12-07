using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public static class Day5
    {
        public static async Task OpenSesame()
        {
            var path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Resources", "day05.txt");
            var data = (await File.ReadAllLinesAsync(path, Encoding.UTF8));
            var seats = data.Select(s => new Seat(s));

            _part1(seats);
            // _part2(passports);
        }

        private static void _part1(IEnumerable<Seat> seats)
        {
            var seatIds = seats.Select(s => s.Id);
            System.Console.WriteLine($"Result of day5-1= The max seat id is {seatIds.Max()}");
        }

        private static void _part2()
        {
        }

    }

    public class Seat
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Id { get => Row * 8 + Column; }
        public Seat(string seat)
        {
            int[] splitArray(int[] arr, char keep)
            {
                if (keep == 'L' || keep == 'F')
                {
                    return arr.Take(arr.Length / 2).ToArray();
                }
                else
                {
                    return arr.Skip(arr.Length / 2).ToArray();
                }
            }

            int getRow()
            {
                var frontBack = seat.Substring(0, 6).ToCharArray().Select(s => char.ToUpper(s));
                int[] rows = Enumerable.Range(0, 128).ToArray();
                char keep = 'x';
                foreach (var c in frontBack)
                {
                    keep = c;
                    rows = splitArray(rows, keep);
                }
                return rows[0];
            }

            int getColumn()
            {
                var leftRight = seat.Substring(7, 3).ToCharArray().Select(s => char.ToUpper(s));
                int[] columns = Enumerable.Range(0, 8).ToArray();
                char keep = 'x';
                foreach (var c in leftRight)
                {
                    keep = c;
                    columns = splitArray(columns, keep);
                }
                return columns[0];
            }
        
            Row = getRow();
            Column = getColumn();
        }
    }
}
