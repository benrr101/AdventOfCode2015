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
            long wrappingPaper = 0;
            long ribbon = 0;

            foreach (string line in inputLines)
            {
                // Split the different dimensions out of the line
                Match dimensionsMatch = dimensionsRegex.Match(line);
                if (!dimensionsMatch.Success)
                {
                    Console.WriteLine("Invalid line '{0}'", line);
                    continue;
                }
                List<int> dimensions = new List<int>
                {
                    Int32.Parse(dimensionsMatch.Groups[1].Value),
                    Int32.Parse(dimensionsMatch.Groups[2].Value),
                    Int32.Parse(dimensionsMatch.Groups[3].Value)
                };
                dimensions.Sort();

                // Calculate the square footage of the faces
                int f1 = dimensions[0]*dimensions[1];
                int f2 = dimensions[1]*dimensions[2];
                int f3 = dimensions[0]*dimensions[2];

                // Minimum face will always be f1 since the dimensions are sorted
                // Calculate min face's perimeter
                int minFacePerimeter = (dimensions[0]*2) + (dimensions[1]*2);
                int volume = dimensions[0]*dimensions[1]*dimensions[2];

                // Add the totals up for this package
                wrappingPaper += (2*f1) + (2*f2) + (2*f3) + f1;
                ribbon += minFacePerimeter + volume;

                Console.WriteLine("--- {0}x{1}x{2}", dimensions[0], dimensions[1], dimensions[2]);
                Console.WriteLine("Wrapping: (2*{0}) + (2*{1}) + (2*{2}) + {3}", f1, f2, f3, f1);
                Console.WriteLine("Ribbon: {0}*2 + {1}*2 + {2}", dimensions[0], dimensions[1], volume);
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Final Wrapping Paper SqFf: {0}", wrappingPaper);
            Console.WriteLine("Final Ribbon Length: {0}", ribbon);
            Console.ReadLine();
        }
    }
}
