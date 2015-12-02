using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
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
            // Read in the input file
            var inputLines = ReadInputByLine("input.txt");

            // Build a regular expression to match the line
            Regex dimensionsRegex = new Regex(@"(\d+)x(\d+)x(\d+)", RegexOptions.Compiled);
            long totalSqFt = 0;

            foreach (string line in inputLines)
            {
                // Split the different dimensions out of the line
                Match dimensionsMatch = dimensionsRegex.Match(line);
                if (!dimensionsMatch.Success)
                {
                    Console.WriteLine("Invalid line '{0}'", line);
                    continue;
                }
                int d1 = Int32.Parse(dimensionsMatch.Groups[1].Value);
                int d2 = Int32.Parse(dimensionsMatch.Groups[2].Value);
                int d3 = Int32.Parse(dimensionsMatch.Groups[3].Value);

                // Calculate the square footage of the faces
                int f1 = d1*d2;
                int f2 = d2*d3;
                int f3 = d1*d3;

                // Find the minimum of the faces
                int minFace = Math.Min(Math.Min(f1, f2), f3);

                // Add the totals up for this package
                totalSqFt += (2*f1) + (2*f2) + (2*f3) + minFace;
                Console.WriteLine("(2*{0}) + (2*{1}) + (2*{2}) + {3}", f1, f2, f3, minFace);
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Final SqFt: {0}", totalSqFt);
            Console.ReadLine();
        }
    }
}
