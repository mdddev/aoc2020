using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    public class Day4
    {
        public static async Task OpenSesame()
        {
            var path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Resources", "day04.txt");
            var data = (await File.ReadAllTextAsync(path, Encoding.UTF8));

            _part1(data);
            // _part2(data);
        }

        private static void _part1(string data)
        {
            var items = _passportsData(data);
            var passports = items.Select(s => new Passport(s));
            var validPassports = passports.Where(w => w.IsValid()).Count();
            System.Console.WriteLine($"Result of day4-1= There are {validPassports} valid passports");
        }

        private static void _part2(IEnumerable<string> data)
        {

            System.Console.WriteLine($"Result of day4-2= ");
        }

        private static List<Dictionary<string, string>> _passportsData(string data)
        {
            var list = new List<Dictionary<string, string>>();
            // passports separated by empty line
            var lines = data.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var dict = new Dictionary<string, string>();
                // remove any line breaks in passport record;
                var pairs = (line.Replace(Environment.NewLine, " ")).Split(" ");
                foreach (var pair in pairs)
                {
                    var keyValue = pair.Split(':', 2);
                    dict.Add(keyValue[0].ToLower(), keyValue[1]);
                }
                list.Add(dict);
            }
            return list;
        }
    }

    public class Passport
    {
        public string Byr { get; set; } = string.Empty;
        public string Iyr { get; set; } = string.Empty;
        public string Eyr { get; set; } = string.Empty;
        public string Hgt { get; set; } = string.Empty;
        public string Hcl { get; set; } = string.Empty;
        public string Ecl { get; set; } = string.Empty;
        public string Pid { get; set; } = string.Empty;
        public string? Cid { get; set; }

        public Passport(Dictionary<string, string> keyValuePairs)
        {
            var props = typeof(Passport).GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name.ToLower();
                var pair = keyValuePairs.FirstOrDefault(f => f.Key.ToLower() == propName);
                if (pair.Key != null)
                {
                    prop.SetValue(this, pair.Value);
                }
            }
        }

        public bool IsValid()
        {
            PropertyInfo[] props = typeof(Passport).GetProperties();
            IEnumerable<string?> values = props.Select(s => (string?)s.GetValue(this));
            var invalidValues = values.Where(w => string.IsNullOrEmpty(w));
            if (!invalidValues.Any())
            {
                return true;
            }
            else if (invalidValues.Count() == 1)
            {
                return Cid == null ? true : false; 
            }
            else
            {
                return false;
            }
        }
    }
}
