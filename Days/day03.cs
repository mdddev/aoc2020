using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day3
    {
        public static async Task OpenSesame()
        {
            var  path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Resources", "day03.txt");
            var lines = (await File.ReadAllLinesAsync(path, Encoding.UTF8)).ToList();
            lines = lines.Select(s => s = string.Concat(Enumerable.Repeat<string>(s, 500))).ToList();

            _part1(lines);
            _part2(lines);
        }

        private static void _part1(List<string> lines)
        {
            var slope = (right: 3, down: 1);
            var trees = _treesHitFallingDown(lines, slope);

            System.Console.WriteLine($"Result of day3-1= ran into {trees} trees when falling down the slope");
        }

        private static void _part2(List<string> lines)
        {
            var treesPerSlopeList = new List<long>();
            var slopes = new [] {
                (right: 1, down: 1),
                (right: 3, down: 1),
                (right: 5, down: 1),
                (right: 7, down: 1),
                (right: 1, down: 2)
            };
            foreach (var slope in slopes)
            {
                var trees = _treesHitFallingDown(lines, slope);
                treesPerSlopeList.Add(trees);
            }
            long productOfTreesHit = treesPerSlopeList.Aggregate((long)1, (x,y) => x * y);
            System.Console.WriteLine($"Result of day3-2= product of trees hit per slope is {productOfTreesHit}");
        }

        private static int _treesHitFallingDown(List<string> lines, (int right, int down) slope)
        {
            var treesEncountered = 0;
            for (int i = slope.down; i < lines.Count; i+=slope.down)
            {
                var y = i;
                var x = i * slope.right;
                if (lines[y][x] == '#') treesEncountered++;
            }

            return treesEncountered;
        }
    }
}
