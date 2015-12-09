using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day5
{
    class Program
    {
        private delegate bool NiceCalculator(string s);

        // Use this to change which comparator is used
        private static NiceCalculator IsNice = IsNicePart2Rules;

        static IEnumerable<string> ReadInputByLine(string filename)
        {
            FileStream file = File.OpenRead(filename);
            StreamReader fileReader = new StreamReader(file);
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        static void Main(string[] args)
        {
            var strings = ReadInputByLine("input.txt");

            // Determine which strings are naughty and which are nice
            int niceStrings = 0;
            foreach (string s in strings)
            {
                if (IsNice(s))
                {
                    niceStrings++;
                    Console.WriteLine(" = Nice");
                }
                else
                {
                    Console.WriteLine(" = Naughty");
                }
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Total Nice: {0}", niceStrings);
            Console.ReadLine();
        }

        static readonly Regex NicePt1VowelRegex = new Regex(@"[aeiou]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static readonly Regex NicePt1DoubleRegex = new Regex(@"(\w)\1", RegexOptions.Compiled);
        static readonly Regex NaughtyPt1Regex = new Regex(@"(ab|cd|pq|xy)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        static bool IsNicePart1Rules(string s)
        {
            // Check for niceness
            bool niceVowelMatch = NicePt1VowelRegex.Matches(s).Count >= 3;
            bool niceDoubleMatch = NicePt1DoubleRegex.IsMatch(s);
            bool naughtyMatch = NaughtyPt1Regex.IsMatch(s);

            Console.Write("{0} -> {1} {2} {3} ", s, niceVowelMatch, niceDoubleMatch, !naughtyMatch);

            return niceVowelMatch && niceDoubleMatch && !naughtyMatch;
        }

        static readonly Regex NicePt2PairRegex = new Regex(@"(\w{2}).*\1", RegexOptions.Compiled);
        static readonly Regex NicePt2RepeatsRegex = new Regex(@"(\w)\w\1", RegexOptions.Compiled);

        static bool IsNicePart2Rules(string s)
        {
            // Check for niceness
            bool nicePairMatch = NicePt2PairRegex.IsMatch(s);
            bool niceRepeatsMatch = NicePt2RepeatsRegex.IsMatch(s);

            Console.Write("{0} -> {1} {2}", s, nicePairMatch, niceRepeatsMatch);

            return nicePairMatch && niceRepeatsMatch;
        }
    }
}
