using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public class PasswordValidator
    {
        public char RequiredChar { get; set; }
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        public List<char> Password { get; set; } = new List<char>();

        public PasswordValidator(string line)
        {
            var substrings = line.Split(' ', 3);
            var bounds = substrings[0]
                .Split('-', 2)
                .Select(s => Convert.ToInt32(s))
                .ToArray();

            LowerBound = bounds[0];
            UpperBound = bounds[1];
            RequiredChar = substrings[1][0];
            var pass = substrings[2];
            foreach (var c in pass)
            {
                Password.Add(c);
            }
        }

        public bool IsValid()
        {
            var charCount = Password.Where(w => w.Equals(RequiredChar)).Count();
            var lboundOkay = charCount >= LowerBound;
            var uboundOkay = charCount <= UpperBound;
            return lboundOkay && uboundOkay;
        }

        public bool IsValidAtNewWorkplace()
        {
            var index1 = LowerBound - 1;
            var index2 = UpperBound - 1;
            var pos1MatchesChar = Password[index1] == RequiredChar;
            var pos2MatchesChar = Password[index2] == RequiredChar;
            return pos1MatchesChar ^ pos2MatchesChar;
        }
    }
}
