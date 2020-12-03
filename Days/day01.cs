using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Days
{
    public class Day1
    {
        public Day1(IEnumerable<int> numbers) => _numbers = numbers.ToArray();
        private readonly int _target = 2020;
        private readonly int[] _numbers;

        public int Part1()
        {
            // nested for loop to work with pairs
            for (int i = 0; i < _numbers.Length; i++)
            {
                for (int j = 0; j < _numbers.Length; j++)
                {
                    if (i == j)
                    {
                        continue; // ignore if same index
                    }
                    else
                    {
                        var n1 = _numbers[i];
                        var n2 = _numbers[j];
                        var sumOfBothNumbers =  n1 + n2;
                        if (sumOfBothNumbers == _target)
                        {
                            return n1 * n2;
                        }
                    }
                }
            }

            // if code reaches here
            throw new Exception($"Could not find two numbers that add to {_target}");
        }

        // not dry code :/
        public int Part2()
        {
            // nested for loop to work with pairs
            for (int i = 0; i < _numbers.Length; i++)
            {
                for (int j = 0; j < _numbers.Length; j++)
                {
                    for (int k = 0; k < _numbers.Length; k++)
                    {
                        if (i == j && j == k)
                        {
                            continue; // ignore if same index
                        }
                        else
                        {
                            var n1 = _numbers[i];
                            var n2 = _numbers[j];
                            var n3 = _numbers[k];
                            var sumOfNumbers =  n1 + n2 + n3;
                            if (sumOfNumbers == _target)
                            {
                                return n1 * n2 * n3;
                            }
                        }
                    }
                }
            }

            // if code reaches here
            throw new Exception($"Could not find three numbers that add to {_target}");
        }
    }
}
