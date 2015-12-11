using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day6
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

        static readonly Regex InputRegex = new Regex(@"(turn off|toggle|turn on) (\d+),(\d+) through (\d+),(\d+)", RegexOptions.Compiled);

        static bool[,] _lightGrid = new bool[1000,1000];
        static uint[,] _brightnessGrid = new uint[1000,1000];

        static void Main(string[] args)
        {
            // Read in the input file
            var input = ReadInputByLine("input.txt");

            // Perform the operations on the grid
            foreach (string s in input)
            {
                Match inputMatch = InputRegex.Match(s);

                // Parse out the dimensions
                int startX = Int32.Parse(inputMatch.Groups[2].Value);
                int startY = Int32.Parse(inputMatch.Groups[3].Value);
                int endX = Int32.Parse(inputMatch.Groups[4].Value);
                int endY = Int32.Parse(inputMatch.Groups[5].Value);

                // Determine what operation we're gonna perform
                switch (inputMatch.Groups[1].Value)
                {
                    case "turn off":
                        PerformGridOperation(startX, startY, endX, endY, (ref bool b) => b = false);
                        PerformGridOperation(startX, startY, endX, endY, (ref uint i) =>
                        {
                            if (i > 0)
                            {
                                i--;
                            }
                        });
                        break;
                    case "toggle":
                        PerformGridOperation(startX, startY, endX, endY, (ref bool b) => b = !b);
                        PerformGridOperation(startX, startY, endX, endY, (ref uint i) => i += 2);
                        break;
                    case "turn on":
                        PerformGridOperation(startX, startY, endX, endY, (ref bool b) => b = true);
                        PerformGridOperation(startX, startY, endX, endY, (ref uint i) => i++);
                        break;
                }
            }

            // Calculate the total number of turned on lights
            int totalLights = 0;
            foreach (bool light in _lightGrid)
            {
                totalLights += light ? 1 : 0;
            }

            uint totalBrightness = 0;
            foreach (uint value in _brightnessGrid)
            {
                totalBrightness += value;
            }

            Console.WriteLine("Part 1: Total turned on lights = {0}", totalLights);
            Console.WriteLine("Part 2: Total brightness = {0}", totalBrightness);
            Console.ReadLine();
        }

        private delegate void BoolOperation(ref bool b);

        private delegate void IntOperation(ref uint i);

        static void PerformGridOperation(int startX, int startY, int endX, int endY, BoolOperation actionToPerform)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    actionToPerform(ref _lightGrid[x, y]);
                }
            }
        }

        static void PerformGridOperation(int startX, int startY, int endX, int endY, IntOperation actionToPerform)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    actionToPerform(ref _brightnessGrid[x, y]);
                }
            }
        }
    }
}
