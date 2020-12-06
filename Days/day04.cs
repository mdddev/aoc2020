using System;
using System.Collections.Generic;
using System.Drawing;
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
            var items = _passportsData(data);
            var passports = items.Select(s => new Passport(s));

            _part1(passports);
            _part2(passports);
        }

        private static void _part1(IEnumerable<Passport> passports)
        {
            var passportsWithRequiredFields = passports.Where(w => w.HasAllRequiredFields()).Count();
            System.Console.WriteLine($"Result of day4-1= There are {passportsWithRequiredFields} valid passports (all required fields)");
        }

        private static void _part2(IEnumerable<Passport> passports)
        {
            var validPassports = passports.Where(w => w.IsValid()).Count();
            System.Console.WriteLine($"Result of day4-2= There are {validPassports} valid passports");
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

        public bool HasAllRequiredFields()
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

        public bool IsValid()
        {
            if (!HasAllRequiredFields())
            {
                return false;
            }
            else
            {
                bool validYear(string year, int min, int max)
                {
                    if (year.Length != 4) return false;
                    var yearInt = Convert.ToInt32(year);
                    if (yearInt < min || yearInt > max) return false;
                    return true;
                }

                // byr, iyr, eyr
                if (!validYear(Byr, 1920, 2002)) return false;
                if (!validYear(Iyr, 2010, 2020)) return false;
                if (!validYear(Eyr, 2020, 2030)) return false;

                // hgt
                var unit = Hgt.Substring(Hgt.Length - 2).ToLower();
                if (unit != "cm" && unit != "in") return false;
                var height = Convert.ToInt32(Hgt.Substring(0, Hgt.Length - 2));
                if (unit == "cm")
                {
                    if (height < 150 || height >193) return false;
                }
                else
                {
                    if (height < 59 || height >76) return false;
                }

                // hcl
                if (Hcl.Length != 7) return false;
                try
                {
                    var colour = ColorTranslator.FromHtml(Hcl);
                }
                catch (System.Exception) // HEX not parseable
                {
                    return false;
                }

                // ecl
                var validColours = new [] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                if (!validColours.Contains(Ecl.ToLower())) return false;
                
                // pid
                if (Pid.Length != 9) return false;

                return true;
            }
        }
    }
}
